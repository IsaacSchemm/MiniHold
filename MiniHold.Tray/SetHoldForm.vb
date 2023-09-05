Imports MiniHold.Abstractions

Public Class SetHoldForm
    Public Thermostat As IThermostatClient

    Private Async Sub SetHoldForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Enabled = False

        HoldHeat.Minimum = Decimal.MinValue
        HoldHeat.Maximum = Decimal.MaxValue
        HoldCool.Minimum = Decimal.MinValue
        HoldCool.Maximum = Decimal.MaxValue

        Dim now = Date.Now
        HoldStartTimePicker.Value = now
        HoldEndTimePicker.Value = now.AddMinutes(30)

        Dim information = Await Thermostat.GetInformationAsync()
        HoldHeat.Value = information.Runtime.TempRange.HeatTemp.Farenheit
        HoldCool.Value = information.Runtime.TempRange.CoolTemp.Farenheit
        HoldFanCheckBox.Checked = information.Runtime.TempRange.Fan = "on"

        HoldHeat.Minimum = information.HeatRangeLow.Farenheit
        HoldHeat.Maximum = information.HeatRangeHigh.Farenheit
        HoldCool.Minimum = information.CoolRangeLow.Farenheit
        HoldCool.Maximum = information.CoolRangeHigh.Farenheit

        Enabled = True
    End Sub

    Private Async Sub SetHoldButton_Click(sender As Object, e As EventArgs) Handles SetHoldButton.Click
        Enabled = False

        Await Thermostat.HoldAsync(
            New TempRange(
                Temperature.FromFarenheit(HoldHeat.Value),
                Temperature.FromFarenheit(HoldCool.Value),
                If(HoldFanCheckBox.Checked, "on", "auto")),
            HoldStartTimePicker.Value,
            HoldEndTimePicker.Value)

        Enabled = True

        Close()
    End Sub
End Class