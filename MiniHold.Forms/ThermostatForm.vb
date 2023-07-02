Imports I8Beef.Ecobee.Protocol.Objects
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
        HeatFan.Text = information.Program.HeatFan
        CoolFan.Text = information.Program.CoolFan

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

    Private Async Sub Act(action As Func(Of Task))
        TabControl1.Enabled = False
        Await action()
        Await RefreshRuntimeAsync()
        TabControl1.Enabled = True
    End Sub

    Private Sub ThermostatForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Act(Async Function()
                Await Task.Yield()
            End Function)
    End Sub

    Private Async Function HoldAsync(range As TempRange, duration As TimeSpan) As Task
        TabControl1.Enabled = False
        Dim dt = ThermostatClient.ToThermostatTime(Date.Now)
        Await ThermostatClient.HoldAsync(range, dt, dt + duration)
        Await RefreshRuntimeAsync()
        TabControl1.Enabled = True
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Act(Async Function()
                Await QuickActions.SetTemperatureOffsetAsync(ThermostatClient, 2, TimeSpan.FromMinutes(10))
            End Function)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Act(Async Function()
                Await QuickActions.SetTemperatureOffsetAsync(ThermostatClient, -2, TimeSpan.FromMinutes(10))
            End Function)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Act(Async Function()
                Await QuickActions.SetFanAsync(ThermostatClient, True, TimeSpan.FromMinutes(10))
            End Function)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Act(Async Function()
                Await QuickActions.SetTemperatureOffsetAsync(ThermostatClient, 2, TimeSpan.FromMinutes(30))
            End Function)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Act(Async Function()
                Await QuickActions.SetTemperatureOffsetAsync(ThermostatClient, -2, TimeSpan.FromMinutes(30))
            End Function)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Act(Async Function()
                Await QuickActions.SetFanAsync(ThermostatClient, True, TimeSpan.FromMinutes(30))
            End Function)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Act(Async Function()
                Await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromMinutes(10))
            End Function)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Act(Async Function()
                Await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromMinutes(30))
            End Function)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Act(Async Function()
                Await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromHours(2))
            End Function)
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Act(Async Function()
                Await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromHours(4))
            End Function)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Act(Async Function()
                Await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromHours(6))
            End Function)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Act(Async Function()
                Await QuickActions.SetAwayUntilTimeAsync(ThermostatClient, #7:00 AM#)
            End Function)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Act(Async Function()
                Await QuickActions.SetAwayUntilTimeAsync(ThermostatClient, #4:00 PM#)
            End Function)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Act(Async Function()
                Await QuickActions.SetAwayUntilTimeAsync(ThermostatClient, #9:00 PM#)
            End Function)
    End Sub

    Private Sub CancelHoldButton_Click(sender As Object, e As EventArgs) Handles CancelHoldButton.Click
        Act(Async Function()
                Await ThermostatClient.CancelHoldAsync()
            End Function)
    End Sub
End Class