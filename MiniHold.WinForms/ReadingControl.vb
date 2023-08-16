Imports MiniHold.Abstractions

Public Class ReadingControl
    Public Sub Apply(reading As IUserInterfaceReading)
        Temp1Label.Visible = reading.Temperatures.Length >= 1
        Temp1.Visible = reading.Temperatures.Length >= 1

        If reading.Temperatures.Length >= 1 Then
            Temp1Label.Text = reading.Temperatures(0).Item1
            Temp1.Text = reading.Temperatures(0).Item2.FarenheitString
        End If

        Temp2Label.Visible = reading.Temperatures.Length >= 2
        Temp2.Visible = reading.Temperatures.Length >= 2

        If reading.Temperatures.Length >= 2 Then
            Temp2Label.Text = reading.Temperatures(1).Item1
            Temp2.Text = reading.Temperatures(1).Item2.FarenheitString
        End If

        Reading1Label.Visible = reading.OtherReadings.Length >= 1
        Reading1.Visible = reading.OtherReadings.Length >= 1

        If reading.OtherReadings.Length >= 1 Then
            Reading1Label.Text = reading.OtherReadings(0).Item1
            Reading1.Text = reading.OtherReadings(0).Item2
        End If

        Reading2Label.Visible = reading.OtherReadings.Length >= 2
        Reading2.Visible = reading.OtherReadings.Length >= 2

        If reading.OtherReadings.Length >= 2 Then
            Reading2Label.Text = reading.OtherReadings(1).Item1
            Reading2.Text = reading.OtherReadings(1).Item2
        End If

        Reading3Label.Visible = reading.OtherReadings.Length >= 3
        Reading3.Visible = reading.OtherReadings.Length >= 3

        If reading.OtherReadings.Length >= 3 Then
            Reading3Label.Text = reading.OtherReadings(2).Item1
            Reading3.Text = reading.OtherReadings(2).Item2
        End If
    End Sub
End Class
