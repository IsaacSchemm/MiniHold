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
        IndoorTemp.Text = information.Readings.Temperature.Select(Function(x) x.FarenheitString).SingleOrDefault()
        IndoorHumidity.Text = information.Readings.Humidity.Select(Function(x) x.PercentageString).SingleOrDefault()
        HeatAt.Text = information.Runtime.TempRange.HeatTemp.FarenheitString
        CoolAt.Text = information.Runtime.TempRange.CoolTemp.FarenheitString
        FanState.Text = $"{information.Runtime.TempRange.Fan}"

        ProgramName.Text = information.Program.Name
        ProgramHeat.Text = information.Program.HeatTemp.FarenheitString
        ProgramCool.Text = information.Program.CoolTemp.FarenheitString

        DataGridView2.Rows.Clear()
        For Each s In information.Sensors
            Dim average = information.Readings.Temperature.Select(Function(x) x.Farenheit).SingleOrDefault()
            Dim specific = s.Readings.Temperature.Select(Function(x) x.Farenheit).SingleOrDefault()
            Dim offset = specific - average
            DataGridView2.Rows.Add({s.Name, $"{specific:0.0}°F", s.Occupied, $"{offset:0.0}°F"})
        Next

        Dim runningEvent = information.Events.Where(Function(e) e.Running).FirstOrDefault()
        If runningEvent Is Nothing OrElse runningEvent.EventType <> "hold" Then
            GroupBox7.Visible = False
        Else
            GroupBox7.Visible = True
            Dim temp = runningEvent.AbsoluteTemperatureRanges.SingleOrDefault()
            HoldHeat.Text = temp?.HeatTemp?.FarenheitString
            HoldCool.Text = temp?.CoolTemp?.FarenheitString
            HoldFan.Text = temp?.Fan

            HoldStartLabel.Text = $"{runningEvent.StartDate:h:mm tt M/d/yy}"
            HoldEndLabel.Text = $"{runningEvent.EndDate:h:mm tt M/d/yy}"
        End If

        FlowLayoutPanel1.Controls.Clear()
        For Each e In EquipmentModule.FromThermostatInformation(information)
            Dim indicator As New Label With {.Text = e.Name, .AutoSize = True, .Padding = New Padding(3)}
            If e.Heat Then
                indicator.BackColor = Color.Red
                indicator.ForeColor = Color.White
            ElseIf e.Cool Then
                indicator.BackColor = Color.Blue
                indicator.ForeColor = Color.White
            Else
                indicator.BackColor = Color.Gray
                indicator.ForeColor = Color.White
            End If
            If e.Comp Then
                indicator.Text = "⇄ " & indicator.Text
            End If
            If e.AuxHeat Then
                indicator.Text = "🔥 " & indicator.Text
            End If
            FlowLayoutPanel1.Controls.Add(indicator)
        Next

        DataGridView1.Rows.Clear()
        For Each c In information.ComfortLevels
            Dim name = c.Name
            If c.Active Then
                name &= " (active)"
            End If
            DataGridView1.Rows.Add(name, information.ApplyHeatDelta(c.HeatTemp), $"{c.HeatTemp} – {c.CoolTemp}", information.ApplyCoolDelta(c.CoolTemp))
        Next

        TextBox1.Text = $"{information}".Replace(vbLf, vbCrLf)

        LastDesired = information.Runtime.TempRange
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
                Await QuickActions.SetTemperatureOffsetAsync(ThermostatClient, 2, TimeSpan.FromMinutes(15))
            End Function)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Act(Async Function()
                Await QuickActions.SetTemperatureOffsetAsync(ThermostatClient, -2, TimeSpan.FromMinutes(15))
            End Function)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Act(Async Function()
                Await QuickActions.SetFanAsync(ThermostatClient, True, TimeSpan.FromMinutes(15))
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
                Await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromMinutes(15))
            End Function)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Act(Async Function()
                Await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromMinutes(30))
            End Function)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Act(Async Function()
                Await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromHours(1))
            End Function)
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Act(Async Function()
                Await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromDays(1))
            End Function)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Act(Async Function()
                Await QuickActions.SetAwayAsync(ThermostatClient, TimeSpan.FromDays(7))
            End Function)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Act(Async Function()
                Await QuickActions.SetAwayUntilClockAsync(ThermostatClient, #7:00 AM#.TimeOfDay)
            End Function)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Act(Async Function()
                Await QuickActions.SetAwayUntilClockAsync(ThermostatClient, #4:00 PM#.TimeOfDay)
            End Function)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Act(Async Function()
                Await QuickActions.SetAwayUntilClockAsync(ThermostatClient, #9:00 PM#.TimeOfDay)
            End Function)
    End Sub

    Private Sub CancelHoldButton_Click(sender As Object, e As EventArgs) Handles CancelHoldButton.Click
        Act(Async Function()
                Await ThermostatClient.CancelHoldAsync()
            End Function)
    End Sub
End Class