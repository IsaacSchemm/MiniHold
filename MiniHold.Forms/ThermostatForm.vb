Imports MiniHold.Abstractions

Public Class ThermostatForm
    Public ThermostatClient As ThermostatClient = Nothing

    Private LastDesired As TempRange = Nothing

    Private Async Function RefreshRuntimeAsync() As Task
        ThermostatName.Text = ThermostatClient.Name
        Brand.Text = ThermostatClient.Brand
        Version.Text = ThermostatClient.Version

        Dim information = Await ThermostatClient.GetInformationAsync()

        OutdoorCondition.Text = information.Weather.Condition
        OutdoorTemp.Text = information.Weather.Temperature.FarenheitString
        OutdoorHumidity.Text = information.Weather.Humidity.PercentageString
        IndoorTemp.Text = information.Actual.Temperature.Select(Function(x) x.FarenheitString).SingleOrDefault()
        IndoorHumidity.Text = information.Actual.Humidity.Select(Function(x) x.PercentageString).SingleOrDefault()
        HeatAt.Text = information.Desired.HeatTemp.FarenheitString
        CoolAt.Text = information.Desired.CoolTemp.FarenheitString
        FanState.Text = $"{information.Desired.Fan}"

        ProgramName.Text = information.Program.Name
        ProgramHeat.Text = information.Program.HeatTemp.FarenheitString
        ProgramCool.Text = information.Program.CoolTemp.FarenheitString

        DataGridView1.Rows.Clear()
        For Each e In information.Events
            Dim temp = e.AbsoluteTemperatureRanges.SingleOrDefault()
            DataGridView1.Rows.Add({e.EventType, temp?.HeatTemp, temp?.CoolTemp, temp?.Fan, e.StartDate, e.EndDate})
        Next

        DataGridView2.Rows.Clear()
        For Each s In information.Sensors
            Dim average = information.Actual.Temperature.Select(Function(x) x.Farenheit).SingleOrDefault()
            Dim specific = s.Readings.Temperature.Select(Function(x) x.Farenheit).SingleOrDefault()
            Dim offset = specific - average
            DataGridView2.Rows.Add({s.Name, $"{specific:0.0}°F", s.Occupied, $"{offset:0.0}°F"})
        Next

        Dim firstEvent = information.Events.DefaultIfEmpty(Nothing).First()
        If firstEvent Is Nothing OrElse firstEvent.EventType <> "hold" Then
            GroupBox7.Visible = False
        Else
            GroupBox7.Visible = True
            Dim temp = firstEvent.AbsoluteTemperatureRanges.SingleOrDefault()
            HoldHeat.Text = temp?.HeatTemp?.FarenheitString
            HoldCool.Text = temp?.CoolTemp?.FarenheitString
            HoldFan.Text = temp?.Fan

            HoldStartLabel.Text = $"{firstEvent.StartDate:h:mm tt M/d/yy}"
            HoldEndLabel.Text = $"{firstEvent.EndDate:h:mm tt M/d/yy}"
        End If

        FlowLayoutPanel1.Controls.Clear()
        For Each e In information.EquipmentStatus
            FlowLayoutPanel1.Controls.Add(New ActiveEquipmentIndicator With {.Text = e})
        Next

        TextBox1.Text = $"{information}".Replace(vbLf, vbCrLf)

        LastDesired = information.Desired
    End Function

    Private Async Sub ThermostatForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Await RefreshRuntimeAsync()
    End Sub

    Private Async Function HoldAsync(range As TempRange, duration As TimeSpan) As Task
        TabControl1.Enabled = False
        Dim dt = ThermostatClient.ToThermostatTime(Date.Now)
        Await ThermostatClient.HoldAsync(range, dt, dt + duration)
        Await RefreshRuntimeAsync()
        TabControl1.Enabled = True
    End Function

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Await HoldAsync(LastDesired.WithHeatTemp(LastDesired.HeatTemp.AddFarenheit(2)), TimeSpan.FromMinutes(10))
    End Sub

    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Await HoldAsync(LastDesired.WithCoolTemp(LastDesired.CoolTemp.AddFarenheit(-2)), TimeSpan.FromMinutes(10))
    End Sub

    Private Async Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Await HoldAsync(LastDesired.WithFan("on"), TimeSpan.FromMinutes(10))
    End Sub

    Private Async Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Await HoldAsync(LastDesired.WithHeatTemp(LastDesired.HeatTemp.AddFarenheit(2)), TimeSpan.FromMinutes(30))
    End Sub

    Private Async Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Await HoldAsync(LastDesired.WithCoolTemp(LastDesired.CoolTemp.AddFarenheit(-2)), TimeSpan.FromMinutes(30))
    End Sub

    Private Async Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Await HoldAsync(LastDesired.WithFan("on"), TimeSpan.FromMinutes(30))
    End Sub

    Private Async Function HoldComfortSettingAsync(holdClimateRef As String, duration As TimeSpan) As Task
        TabControl1.Enabled = False
        Dim dt = ThermostatClient.ToThermostatTime(Date.Now)
        Await ThermostatClient.HoldComfortSettingAsync(holdClimateRef, dt, dt + duration)
        Await RefreshRuntimeAsync()
        TabControl1.Enabled = True
    End Function

    Private Async Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Await HoldComfortSettingAsync("away", TimeSpan.FromMinutes(10))
    End Sub

    Private Async Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Await HoldComfortSettingAsync("away", TimeSpan.FromMinutes(30))
    End Sub

    Private Async Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Await HoldComfortSettingAsync("away", TimeSpan.FromHours(2))
    End Sub

    Private Async Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Await HoldComfortSettingAsync("away", TimeSpan.FromHours(4))
    End Sub

    Private Async Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Await HoldComfortSettingAsync("away", TimeSpan.FromHours(6))
    End Sub

    Private Async Function AwayUntilTimeAsync(time As Date) As Task
        TabControl1.Enabled = False
        Dim startPoint = ThermostatClient.ToThermostatTime(Date.Now)
        Dim endPoint = startPoint.Date + time.TimeOfDay
        If endPoint < startPoint Then
            endPoint += TimeSpan.FromDays(1)
        End If
        Await ThermostatClient.HoldComfortSettingAsync("away", startPoint, endPoint)
        Await RefreshRuntimeAsync()
        TabControl1.Enabled = True
    End Function

    Private Async Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Await AwayUntilTimeAsync(#7:00 AM#)
    End Sub

    Private Async Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Await AwayUntilTimeAsync(#4:00 PM#)
    End Sub

    Private Async Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Await AwayUntilTimeAsync(#9:00 PM#)
    End Sub

    Private Async Sub CancelHoldButton_Click(sender As Object, e As EventArgs) Handles CancelHoldButton.Click
        TabControl1.Enabled = False
        Await ThermostatClient.CancelHoldAsync()
        Await RefreshRuntimeAsync()
        TabControl1.Enabled = True
    End Sub

    Private Async Sub SendAlertButton_Click(sender As Object, e As EventArgs) Handles SendAlertButton.Click
        TabControl1.Enabled = False
        Await ThermostatClient.SendMessageAsync(TextBox2.Text)
        TextBox2.Text = ""
        Await RefreshRuntimeAsync()
        TabControl1.Enabled = True
    End Sub
End Class