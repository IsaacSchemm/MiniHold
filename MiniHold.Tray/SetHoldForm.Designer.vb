<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetHoldForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Label2 = New Label()
        SetHoldButton = New Button()
        HoldStartTimePicker = New DateTimePicker()
        HoldFanCheckBox = New CheckBox()
        Label6 = New Label()
        HoldCool = New NumericUpDown()
        HoldEndTimePicker = New DateTimePicker()
        Label9 = New Label()
        Label8 = New Label()
        HoldHeat = New NumericUpDown()
        CType(HoldCool, ComponentModel.ISupportInitialize).BeginInit()
        CType(HoldHeat, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 9)
        Label2.Name = "Label2"
        Label2.Size = New Size(58, 15)
        Label2.TabIndex = 10
        Label2.Text = "Start time"
        ' 
        ' SetHoldButton
        ' 
        SetHoldButton.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        SetHoldButton.Location = New Point(12, 176)
        SetHoldButton.Name = "SetHoldButton"
        SetHoldButton.Size = New Size(210, 23)
        SetHoldButton.TabIndex = 19
        SetHoldButton.Text = "Set Hold"
        SetHoldButton.UseVisualStyleBackColor = True
        ' 
        ' HoldStartTimePicker
        ' 
        HoldStartTimePicker.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        HoldStartTimePicker.Checked = False
        HoldStartTimePicker.CustomFormat = "h:mm:ss tt (MMM/dd/yyyy)"
        HoldStartTimePicker.Format = DateTimePickerFormat.Custom
        HoldStartTimePicker.Location = New Point(12, 27)
        HoldStartTimePicker.Name = "HoldStartTimePicker"
        HoldStartTimePicker.Size = New Size(210, 23)
        HoldStartTimePicker.TabIndex = 11
        ' 
        ' HoldFanCheckBox
        ' 
        HoldFanCheckBox.AutoSize = True
        HoldFanCheckBox.Location = New Point(12, 144)
        HoldFanCheckBox.Name = "HoldFanCheckBox"
        HoldFanCheckBox.Size = New Size(45, 19)
        HoldFanCheckBox.TabIndex = 18
        HoldFanCheckBox.Text = "Fan"
        HoldFanCheckBox.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(12, 53)
        Label6.Name = "Label6"
        Label6.Size = New Size(54, 15)
        Label6.TabIndex = 12
        Label6.Text = "End time"
        ' 
        ' HoldCool
        ' 
        HoldCool.Location = New Point(93, 115)
        HoldCool.Name = "HoldCool"
        HoldCool.Size = New Size(75, 23)
        HoldCool.TabIndex = 17
        ' 
        ' HoldEndTimePicker
        ' 
        HoldEndTimePicker.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        HoldEndTimePicker.Checked = False
        HoldEndTimePicker.CustomFormat = "h:mm:ss tt (MMM/dd/yyyy)"
        HoldEndTimePicker.Format = DateTimePickerFormat.Custom
        HoldEndTimePicker.Location = New Point(12, 71)
        HoldEndTimePicker.Name = "HoldEndTimePicker"
        HoldEndTimePicker.Size = New Size(210, 23)
        HoldEndTimePicker.TabIndex = 13
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(93, 97)
        Label9.Name = "Label9"
        Label9.Size = New Size(54, 15)
        Label9.TabIndex = 16
        Label9.Text = "Cool (°F)"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(12, 97)
        Label8.Name = "Label8"
        Label8.Size = New Size(54, 15)
        Label8.TabIndex = 14
        Label8.Text = "Heat (°F)"
        ' 
        ' HoldHeat
        ' 
        HoldHeat.Location = New Point(12, 115)
        HoldHeat.Name = "HoldHeat"
        HoldHeat.Size = New Size(75, 23)
        HoldHeat.TabIndex = 15
        ' 
        ' SetHoldForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(234, 211)
        Controls.Add(Label2)
        Controls.Add(SetHoldButton)
        Controls.Add(HoldStartTimePicker)
        Controls.Add(HoldFanCheckBox)
        Controls.Add(Label6)
        Controls.Add(HoldCool)
        Controls.Add(HoldEndTimePicker)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(HoldHeat)
        FormBorderStyle = FormBorderStyle.SizableToolWindow
        Name = "SetHoldForm"
        StartPosition = FormStartPosition.CenterParent
        Text = "Set Hold"
        CType(HoldCool, ComponentModel.ISupportInitialize).EndInit()
        CType(HoldHeat, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents SetHoldButton As Button
    Friend WithEvents HoldStartTimePicker As DateTimePicker
    Friend WithEvents HoldFanCheckBox As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents HoldCool As NumericUpDown
    Friend WithEvents HoldEndTimePicker As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents HoldHeat As NumericUpDown
End Class
