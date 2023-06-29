Imports System.Threading
Imports I8Beef.Ecobee
Imports I8Beef.Ecobee.Exceptions
Imports MiniHold.Abstractions
Imports Newtonsoft.Json

#Disable Warning BC42356

Public Class SelectionForm
    Private Async Function GetTokenAsync(cancellationToken As CancellationToken) As Task(Of StoredAuthToken)
        If String.IsNullOrEmpty(My.Settings.EcobeeToken) Then
            Return Nothing
        End If

        Return JsonConvert.DeserializeObject(Of StoredAuthToken)(My.Settings.EcobeeToken)
    End Function

    Private Async Function SetTokenAsync(token As StoredAuthToken, cancellationToken As CancellationToken) As Task
        My.Settings.EcobeeToken = JsonConvert.SerializeObject(token)
        My.Settings.Save()
    End Function

    Private Async Sub SelectionForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        My.Settings.Upgrade()

        If String.IsNullOrEmpty(My.Settings.EcobeeApiKey) Then
            Dim promptResult = InputBox($"Enter the API key of an ecobee app you've created in the Developer section of the ecobee portal. This key will be stored in plaintext in your AppData folder.", Text)
            If String.IsNullOrEmpty(promptResult) Then
                Close()
                Exit Sub
            Else
                My.Settings.EcobeeApiKey = promptResult
                My.Settings.Save()
            End If
        End If

        Dim client As New Client(My.Settings.EcobeeApiKey, AddressOf GetTokenAsync, AddressOf SetTokenAsync, TimeSpan.FromSeconds(45))
        If Await GetTokenAsync(CancellationToken.None) Is Nothing Then
            Dim pin = Await client.GetPinAsync()
            Clipboard.SetData(DataFormats.Text, pin.EcobeePin)
            Do
                Dim wait = Task.Delay(TimeSpan.FromSeconds(pin.Interval))

                Dim promptResult = MsgBox($"Your PIN code ({pin.EcobeePin}) has been copied to the clipboard. Enter this code in the My Apps > Add Application section of the customer portal, and press OK when ready. This token will be stored in plaintext in your AppData folder.", MsgBoxStyle.OkCancel, Text)
                If promptResult = MsgBoxResult.Cancel Then
                    Close()
                    Exit Sub
                End If

                Await wait

                Try
                    Await client.GetAccessTokenAsync(pin.Code)
                    Exit Do
                Catch ex As ApiAuthException
                    Console.Error.WriteLine(ex)
                End Try
            Loop
        End If
        Dim thermostats = Await ThermostatClient.GetAllAsListAsync(client)
        For Each t In thermostats
            ComboBox1.Items.Add(t)
        Next
        If ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0
            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox1.SelectedItem IsNot Nothing Then
            ThermostatForm.ThermostatClient = ComboBox1.SelectedItem
            ThermostatForm.ShowDialog(Me)
        End If
    End Sub
End Class