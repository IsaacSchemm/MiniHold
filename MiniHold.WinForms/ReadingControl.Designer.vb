<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReadingControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Temp1Label = New Label()
        Temp1 = New Label()
        Temp2 = New Label()
        Temp2Label = New Label()
        Reading1Label = New Label()
        Reading1 = New Label()
        Reading2 = New Label()
        Reading2Label = New Label()
        Reading3 = New Label()
        Reading3Label = New Label()
        SuspendLayout()
        ' 
        ' Temp1Label
        ' 
        Temp1Label.AutoSize = True
        Temp1Label.Location = New Point(0, 0)
        Temp1Label.Margin = New Padding(0)
        Temp1Label.Name = "Temp1Label"
        Temp1Label.Size = New Size(70, 15)
        Temp1Label.TabIndex = 1
        Temp1Label.Text = "Temp1Label"
        Temp1Label.Visible = False
        ' 
        ' Temp1
        ' 
        Temp1.AutoSize = True
        Temp1.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        Temp1.Location = New Point(-3, 15)
        Temp1.Margin = New Padding(0)
        Temp1.Name = "Temp1"
        Temp1.Size = New Size(77, 32)
        Temp1.TabIndex = 2
        Temp1.Text = "188°C"
        Temp1.Visible = False
        ' 
        ' Temp2
        ' 
        Temp2.AutoSize = True
        Temp2.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        Temp2.Location = New Point(81, 15)
        Temp2.Margin = New Padding(0)
        Temp2.Name = "Temp2"
        Temp2.Size = New Size(77, 32)
        Temp2.TabIndex = 4
        Temp2.Text = "188°C"
        Temp2.Visible = False
        ' 
        ' Temp2Label
        ' 
        Temp2Label.AutoSize = True
        Temp2Label.Location = New Point(84, 0)
        Temp2Label.Margin = New Padding(0)
        Temp2Label.Name = "Temp2Label"
        Temp2Label.Size = New Size(70, 15)
        Temp2Label.TabIndex = 3
        Temp2Label.Text = "Temp2Label"
        Temp2Label.Visible = False
        ' 
        ' Reading1Label
        ' 
        Reading1Label.AutoSize = True
        Reading1Label.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        Reading1Label.Location = New Point(0, 47)
        Reading1Label.Margin = New Padding(0)
        Reading1Label.Name = "Reading1Label"
        Reading1Label.Size = New Size(22, 15)
        Reading1Label.TabIndex = 5
        Reading1Label.Text = "---"
        Reading1Label.Visible = False
        ' 
        ' Reading1
        ' 
        Reading1.AutoSize = True
        Reading1.Location = New Point(84, 47)
        Reading1.Margin = New Padding(0)
        Reading1.Name = "Reading1"
        Reading1.Size = New Size(22, 15)
        Reading1.TabIndex = 6
        Reading1.Text = "---"
        Reading1.Visible = False
        ' 
        ' Reading2
        ' 
        Reading2.AutoSize = True
        Reading2.Location = New Point(84, 62)
        Reading2.Margin = New Padding(0)
        Reading2.Name = "Reading2"
        Reading2.Size = New Size(22, 15)
        Reading2.TabIndex = 8
        Reading2.Text = "---"
        Reading2.Visible = False
        ' 
        ' Reading2Label
        ' 
        Reading2Label.AutoSize = True
        Reading2Label.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        Reading2Label.Location = New Point(0, 62)
        Reading2Label.Margin = New Padding(0)
        Reading2Label.Name = "Reading2Label"
        Reading2Label.Size = New Size(22, 15)
        Reading2Label.TabIndex = 7
        Reading2Label.Text = "---"
        Reading2Label.Visible = False
        ' 
        ' Reading3
        ' 
        Reading3.AutoSize = True
        Reading3.Location = New Point(84, 77)
        Reading3.Margin = New Padding(0)
        Reading3.Name = "Reading3"
        Reading3.Size = New Size(22, 15)
        Reading3.TabIndex = 10
        Reading3.Text = "---"
        Reading3.Visible = False
        ' 
        ' Reading3Label
        ' 
        Reading3Label.AutoSize = True
        Reading3Label.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        Reading3Label.Location = New Point(0, 77)
        Reading3Label.Margin = New Padding(0)
        Reading3Label.Name = "Reading3Label"
        Reading3Label.Size = New Size(22, 15)
        Reading3Label.TabIndex = 9
        Reading3Label.Text = "---"
        Reading3Label.Visible = False
        ' 
        ' ReadingControl
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Reading3)
        Controls.Add(Reading3Label)
        Controls.Add(Reading2)
        Controls.Add(Reading2Label)
        Controls.Add(Reading1)
        Controls.Add(Reading1Label)
        Controls.Add(Temp2)
        Controls.Add(Temp2Label)
        Controls.Add(Temp1)
        Controls.Add(Temp1Label)
        Name = "ReadingControl"
        Size = New Size(172, 92)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Temp1Label As Label
    Friend WithEvents Temp1 As Label
    Friend WithEvents Temp2 As Label
    Friend WithEvents Temp2Label As Label
    Friend WithEvents Reading1Label As Label
    Friend WithEvents Reading1 As Label
    Friend WithEvents Reading2 As Label
    Friend WithEvents Reading2Label As Label
    Friend WithEvents Reading3 As Label
    Friend WithEvents Reading3Label As Label
End Class
