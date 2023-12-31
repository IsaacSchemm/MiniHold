﻿Imports System.Text.Json
Imports System.Threading
Imports I8Beef.Ecobee
Imports MiniHold.Abstractions

Public Class Form1
    Private ReadOnly EcobeeClient As New Client(Keys.ApiKey, AddressOf GetToken, AddressOf SetToken)

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
            Await EcobeeClient.GetAccessTokenAsync(pin.Code, cancellationToken)
        End If

        Return Await GetToken(cancellationToken)
    End Function

    Private Function SetToken(token As StoredAuthToken, cancellationToken As CancellationToken) As Task
        My.Settings.StoredAuthToken = JsonSerializer.Serialize(token)
        My.Settings.Save()
        Return Task.CompletedTask
    End Function

    Private LastUpdate As Date = Date.MinValue

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If WindowState = FormWindowState.Minimized Then
            ShowInTaskbar = False
        Else
            If Not ShowInTaskbar Then
                ShowInTaskbar = True
                If Date.Now - LastUpdate > TimeSpan.FromMinutes(1) Then
                    LastUpdate = Date.Now
                    UpdateCurrent()
                End If
            End If
        End If
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = MouseButtons.Left Then
            WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Async Sub AddRealThermostats()
        Dim list = Await ThermostatEnumerator.FindAllAsync(EcobeeClient)
        ThermostatDropDown.Items.Clear()
        For Each t In list
            ThermostatDropDown.Items.Add(t)
        Next
        If ThermostatDropDown.Items.Count > 0 Then
            ThermostatDropDown.SelectedIndex = 0
        End If
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If Not String.IsNullOrEmpty(My.Settings.StoredAuthToken) Then
            AddRealThermostats()
        End If
    End Sub

    Private Sub RefreshListButton_Click(sender As Object, e As EventArgs) Handles RefreshListButton.Click
        AddRealThermostats()
    End Sub

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        Dim result = MsgBox("This will clear your API credentials.", MsgBoxStyle.OkCancel, Text)
        If result = MsgBoxResult.Ok Then
            My.Settings.Reset()
            ThermostatDropDown.Items.Clear()
        End If
    End Sub

    Private LastThermostat As IThermostatClient = Nothing
    Private LastInformation As ThermostatInformation = Nothing

    Private Async Sub UpdateCurrent()
        NotifyIcon1.Text = "No information loaded"

        LastInformation = Nothing
        LastThermostat = Nothing

        Dim thermostat = TryCast(ThermostatDropDown.SelectedItem, IThermostatClient)
        If thermostat Is Nothing Then
            Exit Sub
        End If

        Dim info = Await thermostat.GetInformationAsync()

        LastThermostat = thermostat
        LastInformation = info

        IndoorTemperatureLabel.Text = info.Readings.FarenheitString
        IndoorHumidityLabel.Text = info.Readings.HumidityString
        OutdoorTemperatureLabel.Text = info.Weather.Temperature.FarenheitString
        OutdoorHumidityLabel.Text = info.Weather.Humidity.PercentageString

        Dim forecast = info.DailyForecasts.DefaultIfEmpty(Nothing).First()

        HighLabel.Text = forecast?.High?.FarenheitString
        LowLabel.Text = forecast?.Low?.FarenheitString

        DataGridView1.Rows.Clear()
        For Each sensor In info.Sensors
            DataGridView1.Rows.Add(sensor.NameWithOccupancyIndicator, sensor.Readings.PreciseFarenheitString)
        Next

        AbstractionTextBox.Text = LastInformation.ToString().Replace(vbLf, vbCrLf)

        Dim evt = LastInformation.Events.DefaultIfEmpty(Nothing).First()
        If evt IsNot Nothing AndAlso evt.EventType = "hold" Then
            ActiveHoldLabel.Text = $"Hold: {evt.Description}"
            If Not evt.Running Then
                ActiveHoldLabel.Text &= $" (at {evt.StartDate})"
            End If
            ClearHoldButton.Enabled = True
        Else
            ActiveHoldLabel.Text = ""
            ClearHoldButton.Enabled = False
        End If

        If info.Alerts.Length > 0 Then
            AlertPanel.Visible = True
            NotifyIcon1.Text = "Alert active"
        Else
            AlertPanel.Visible = False
            NotifyIcon1.Text = $"{thermostat.Name} ({Date.Now.ToShortTimeString()}): {info.Readings.FarenheitString} (Weather: {info.Weather.Temperature.FarenheitString})"
        End If

        If info.EquipmentStatus.IsEmpty Then
            EquipmentLabel.Visible = False
        Else
            Dim equipment = EquipmentModule.FromThermostatInformation(info)

            EquipmentLabel.Visible = True
            EquipmentLabel.Text = Date.Now.ToShortTimeString() & ": " & String.Join(", ", equipment.Select(Function(e) e.Name))
        End If
    End Sub

    Private Sub ThermostatDropDown_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ThermostatDropDown.SelectedIndexChanged
        UpdateCurrent()
    End Sub

    Private Async Sub HeatButton_Click(sender As Object, e As EventArgs) Handles HeatButton.Click
        If LastThermostat Is Nothing OrElse LastInformation Is Nothing Then
            Exit Sub
        End If

        If Not LastInformation.Heat Then
            MsgBox("Cannot apply heating when the thermostat is not in auto, heat, or aux mode.", MsgBoxStyle.Critical, Text)
            Exit Sub
        End If

        Dim newRange = QuickActions.DetermineNewRangeToApplyHeat(LastInformation)
        If MsgBox($"This will set a hold for {newRange.ShortDescription}.", MsgBoxStyle.OkCancel, Text) <> MsgBoxResult.Ok Then
            Exit Sub
        End If

        Await QuickActions.SetHoldAsync(
           LastThermostat,
           newRange,
           TimeSpan.FromMinutes(30))

        UpdateCurrent()
    End Sub

    Private Async Sub CoolButton_Click(sender As Object, e As EventArgs) Handles CoolButton.Click
        If LastThermostat Is Nothing OrElse LastInformation Is Nothing Then
            Exit Sub
        End If

        If Not LastInformation.Cool Then
            MsgBox("Cannot apply cooling when the thermostat is not in auto or cool mode.", MsgBoxStyle.Critical, Text)
            Exit Sub
        End If

        Dim newRange = QuickActions.DetermineNewRangeToApplyCool(LastInformation)
        If MsgBox($"This will set a hold for {newRange.ShortDescription}.", MsgBoxStyle.OkCancel, Text) <> MsgBoxResult.Ok Then
            Exit Sub
        End If

        Await QuickActions.SetHoldAsync(
           LastThermostat,
           newRange,
           TimeSpan.FromMinutes(30))

        UpdateCurrent()
    End Sub

    Private Async Sub FanButton_Click(sender As Object, e As EventArgs) Handles FanButton.Click
        If LastThermostat Is Nothing OrElse LastInformation Is Nothing Then
            Exit Sub
        End If

        Await QuickActions.SetFanAsync(LastThermostat, True, TimeSpan.FromMinutes(30))

        UpdateCurrent()
    End Sub

    Private Async Sub ClearHoldButton_Click(sender As Object, e As EventArgs) Handles ClearHoldButton.Click
        If LastThermostat Is Nothing OrElse LastInformation Is Nothing Then
            Exit Sub
        End If

        Await LastThermostat.CancelHoldAsync()

        UpdateCurrent()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If WindowState = FormWindowState.Minimized Then
            UpdateCurrent()
        End If
    End Sub

    Private Sub OtherHoldButton_Click(sender As Object, e As EventArgs) Handles OtherHoldButton.Click
        SetHoldForm.Thermostat = LastThermostat
        SetHoldForm.ShowDialog(Me)
        UpdateCurrent()
    End Sub

    Private Sub RestoreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestoreToolStripMenuItem.Click
        WindowState = FormWindowState.Normal
    End Sub

    Private Sub WebPortalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WebPortalToolStripMenuItem.Click
        Process.Start("explorer.exe", "https://www.ecobee.com/consumerportal/index.html")
    End Sub

    Private Sub BeestatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BeestatToolStripMenuItem.Click
        Process.Start("explorer.exe", "https://app.beestat.io")
    End Sub

    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub AlertButton_Click(sender As Object, e As EventArgs) Handles AlertButton.Click
        Using form As New Form
            form.FormBorderStyle = FormBorderStyle.SizableToolWindow
            form.Text = Me.Text

            Dim textBox As New TextBox With {
                .ScrollBars = ScrollBars.Both,
                .Multiline = True,
                .ReadOnly = True,
                .Dock = DockStyle.Fill,
                .Text = LastInformation.Alerts.ToString().Replace(vbLf, vbCrLf)
            }

            form.Controls.Add(textBox)
            form.ShowDialog(Me)
        End Using
    End Sub
End Class
