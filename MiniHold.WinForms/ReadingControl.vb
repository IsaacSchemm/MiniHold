Imports MiniHold.Abstractions

Public Class ReadingControl
    Public Sub Apply(reading As IUserInterfaceReading)
        If reading.Temperatures.Length >= 1 Then
            Temp1Label.Text = reading.Temperatures(0).Item1
            Temp1Label.Visible = True
            Temp1.Text = reading.Temperatures(0).Item2.FarenheitString
            Temp1.Visible = True
        End If
        If reading.Temperatures.Length >= 2 Then
            Temp2Label.Text = reading.Temperatures(1).Item1
            Temp2Label.Visible = True
            Temp2.Text = reading.Temperatures(1).Item2.FarenheitString
            Temp2.Visible = True
        End If
        If reading.OtherReadings.Length >= 1 Then
            Reading1Label.Text = reading.OtherReadings(0).Item1
            Reading1Label.Visible = True
            Reading1.Text = reading.OtherReadings(0).Item2
            Reading1.Visible = True
        End If
        If reading.OtherReadings.Length >= 2 Then
            Reading2Label.Text = reading.OtherReadings(1).Item1
            Reading2Label.Visible = True
            Reading2.Text = reading.OtherReadings(1).Item2
            Reading2.Visible = True
        End If
        If reading.OtherReadings.Length >= 3 Then
            Reading3Label.Text = reading.OtherReadings(2).Item1
            Reading3Label.Visible = True
            Reading3.Text = reading.OtherReadings(2).Item2
            Reading3.Visible = True
        End If
    End Sub
End Class
