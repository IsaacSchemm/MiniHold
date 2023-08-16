Imports System.Text.Json
Imports System.Threading
Imports I8Beef.Ecobee
Imports MiniHold.Abstractions

Public Class Form1
    Private ReadOnly EcobeeClient As New Client(Keys.ApiKey, AddressOf GetToken, AddressOf SetToken)

    Private ReadOnly Property Thermostat As IThermostatClient
        Get
            Return TryCast(ThermostatDropDown.SelectedItem, IThermostatClient)
        End Get
    End Property

    Private Info As ThermostatInformation = Nothing

    Private Async Function GetToken(cancellationToken As CancellationToken) As Task(Of StoredAuthToken)
        Try
            If Not String.IsNullOrEmpty(My.Settings.StoredAuthToken) Then
                Dim obj = JsonSerializer.Deserialize(Of StoredAuthToken)(My.Settings.StoredAuthToken)
                Return obj
            End If
        Catch ex As JsonException
        End Try

        Dim pin = Await EcobeeClient.GetPinAsync(cancellationToken)

        Dim msg = $"Your PIN code is: {pin.EcobeePin}

Enter this code in the My Apps > Add Application section of the customer portal, and press OK when ready."

        Dim result = MsgBox(msg, MsgBoxStyle.OkCancel, Text)
        If result <> MsgBoxResult.Ok Then
            Application.Exit()
        Else
            Await EcobeeClient.GetAccessTokenAsync(pin.Code)
        End If

        Return Await GetToken(cancellationToken)
    End Function

    Private Function SetToken(token As StoredAuthToken, cancellationToken As CancellationToken) As Task
        My.Settings.StoredAuthToken = JsonSerializer.Serialize(token)
        My.Settings.Save()
        Return Task.CompletedTask
    End Function

    Private Async Sub DisableWhile(action As Func(Of Task))
        Try
            MainPanel.Enabled = False
            Await action()
        Finally
            MainPanel.Enabled = True
        End Try
    End Sub

    Private Sub RefreshList()
        DisableWhile(Async Function()
                         ThermostatDropDown.Items.Clear()
                         Dim list = Await ThermostatEnumerator.FindAllAsync(EcobeeClient)
                         For Each t In list
                             ThermostatDropDown.Items.Add(t)
                         Next
                         If ThermostatDropDown.Items.Count > 0 Then
                             ThermostatDropDown.SelectedIndex = 0
                         End If
                     End Function)
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        RefreshList()
    End Sub

    Private Sub RefreshListButton_Click(sender As Object, e As EventArgs) Handles RefreshListButton.Click
        RefreshList()
    End Sub

    Private Async Function RefreshThermostatAsync() As Task
        Dim thermostat = TryCast(ThermostatDropDown.SelectedItem, IThermostatClient)
        If thermostat IsNot Nothing Then
            Info = Await thermostat.GetInformationAsync()

            ModeLabel.Text = Info.DisplayMode
            RunningEquipmentLabel.Text = String.Join(", ", Info.EquipmentStatus.DefaultIfEmpty("None"))
            LastUpdatedLabel.Text = Date.Now.ToString()

            AlertLabel.Visible = Info.Alerts.Any()

            AbstractionBox.Text = Info.ToString().Replace(vbLf, vbCrLf)

            IndoorCurrent.Apply(Info.Readings)
            IndoorRuntime.Apply(Info.RuntimeDisplay)
            OutdoorWeather.Apply(Info.Weather)
            OutdoorForecast.Apply(Info.DailyForecasts.First())

            ClearHoldButton.Enabled = Info.Events.FirstOrDefault()?.EventType = "hold"
        End If
    End Function

    Private Sub RunAndUpdate(action As Func(Of Task))
        DisableWhile(Async Function()
                         Await action()
                         Await Task.Delay(500)
                         Await RefreshThermostatAsync()
                     End Function)
    End Sub

    Private Sub ThermostatDropDown_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ThermostatDropDown.SelectedIndexChanged
        RunAndUpdate(Function() Task.CompletedTask)
    End Sub

    Private Sub RefreshThermostatButton_Click(sender As Object, e As EventArgs) Handles RefreshThermostatButton.Click
        RunAndUpdate(Function() Task.CompletedTask)
    End Sub

    Private Sub HoldHeat(ts As TimeSpan)
        If Info Is Nothing Or Thermostat Is Nothing Then
            Exit Sub
        End If

        RunAndUpdate(Async Function()
                         Dim newSetPoint = Info.Readings.Temperature(0) + Info.HeatDelta + Temperature.FromFarenheit(1.5D)
                         If Not Info.Runtime.TempRange.Contains(newSetPoint) Then
                             Dim msg = $"This will set the heating set point to {newSetPoint}, which is outside the current range. Continue anyway?"
                             If MsgBox(msg, MsgBoxStyle.OkCancel, Text) <> MsgBoxResult.Ok Then
                                 Exit Function
                             End If
                         End If
                         Await QuickActions.SetHoldAsync(
                            Thermostat,
                            Info.Runtime.TempRange.
                                WithHeatTemp(newSetPoint).
                                WithCoolTemp(newSetPoint + Temperature.FromFarenheit(10)),
                            ts)
                     End Function)
    End Sub

    Private Sub HoldCool(ts As TimeSpan)
        If Info Is Nothing Or Thermostat Is Nothing Then
            Exit Sub
        End If

        RunAndUpdate(Async Function()
                         Dim newSetPoint = Info.Readings.Temperature(0) - Info.CoolDelta - Temperature.FromFarenheit(1.5D)
                         If Not Info.Runtime.TempRange.Contains(newSetPoint) Then
                             Dim msg = $"This will set the cooling set point to {newSetPoint}, which is outside the current range. Continue anyway?"
                             If MsgBox(msg, MsgBoxStyle.OkCancel, Text) <> MsgBoxResult.Ok Then
                                 Exit Function
                             End If
                         End If
                         Await QuickActions.SetHoldAsync(
                            Thermostat,
                            Info.Runtime.TempRange.
                                WithHeatTemp(newSetPoint - Temperature.FromFarenheit(10)).
                                WithCoolTemp(newSetPoint),
                            ts)
                     End Function)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        HoldHeat(TimeSpan.FromMinutes(15))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        HoldCool(TimeSpan.FromMinutes(15))
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        RunAndUpdate(Function() QuickActions.SetFanAsync(Thermostat, True, TimeSpan.FromMinutes(15)))
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        HoldHeat(TimeSpan.FromMinutes(30))
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        HoldCool(TimeSpan.FromMinutes(30))
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        RunAndUpdate(Function() QuickActions.SetFanAsync(Thermostat, True, TimeSpan.FromMinutes(30)))
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        RunAndUpdate(Function() QuickActions.SetAwayAsync(Thermostat, TimeSpan.FromHours(1)))
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        RunAndUpdate(Function() QuickActions.SetAwayAsync(Thermostat, TimeSpan.FromDays(1)))
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        RunAndUpdate(Function() QuickActions.SetAwayAsync(Thermostat, TimeSpan.FromDays(7)))
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        RunAndUpdate(Function() QuickActions.SetAwayUntilClockAsync(Thermostat, #7:00 AM#.TimeOfDay))
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        RunAndUpdate(Function() QuickActions.SetAwayUntilClockAsync(Thermostat, #4:00 PM#.TimeOfDay))
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        RunAndUpdate(Function() QuickActions.SetAwayUntilClockAsync(Thermostat, #9:00 PM#.TimeOfDay))
    End Sub

    Private Sub ClearHoldButton_Click(sender As Object, e As EventArgs) Handles ClearHoldButton.Click
        RunAndUpdate(Function() Thermostat.CancelHoldAsync())
    End Sub
End Class
