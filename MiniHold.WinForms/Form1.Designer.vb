<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        ThermostatDropDown = New ComboBox()
        RefreshListButton = New Button()
        MainPanel = New Panel()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        ClearHoldButton = New Button()
        GroupBox4 = New GroupBox()
        Button12 = New Button()
        Button11 = New Button()
        Button10 = New Button()
        Button9 = New Button()
        Button8 = New Button()
        Button7 = New Button()
        GroupBox5 = New GroupBox()
        OutdoorForecast = New ReadingControl()
        GroupBox1 = New GroupBox()
        OutdoorWeather = New ReadingControl()
        GroupBox7 = New GroupBox()
        IndoorRuntime = New ReadingControl()
        GroupBox6 = New GroupBox()
        IndoorCurrent = New ReadingControl()
        GroupBox3 = New GroupBox()
        Button4 = New Button()
        Button5 = New Button()
        Button6 = New Button()
        GroupBox2 = New GroupBox()
        Button3 = New Button()
        Button2 = New Button()
        Button1 = New Button()
        TabPage2 = New TabPage()
        AbstractionBox = New TextBox()
        Label2 = New Label()
        LastUpdatedLabel = New Label()
        RunningEquipmentLabel = New Label()
        RefreshThermostatButton = New Button()
        ModeLabel = New Label()
        Label4 = New Label()
        Label5 = New Label()
        AlertLabel = New Label()
        MainPanel.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        GroupBox4.SuspendLayout()
        GroupBox5.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox7.SuspendLayout()
        GroupBox6.SuspendLayout()
        GroupBox3.SuspendLayout()
        GroupBox2.SuspendLayout()
        TabPage2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 15)
        Label1.Name = "Label1"
        Label1.Size = New Size(67, 15)
        Label1.TabIndex = 0
        Label1.Text = "Thermostat"
        ' 
        ' ThermostatDropDown
        ' 
        ThermostatDropDown.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ThermostatDropDown.DropDownStyle = ComboBoxStyle.DropDownList
        ThermostatDropDown.FormattingEnabled = True
        ThermostatDropDown.Location = New Point(85, 12)
        ThermostatDropDown.Name = "ThermostatDropDown"
        ThermostatDropDown.Size = New Size(460, 23)
        ThermostatDropDown.TabIndex = 1
        ' 
        ' RefreshListButton
        ' 
        RefreshListButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        RefreshListButton.Location = New Point(551, 12)
        RefreshListButton.Name = "RefreshListButton"
        RefreshListButton.Size = New Size(75, 23)
        RefreshListButton.TabIndex = 2
        RefreshListButton.Text = "Refresh"
        RefreshListButton.UseVisualStyleBackColor = True
        ' 
        ' MainPanel
        ' 
        MainPanel.Controls.Add(TabControl1)
        MainPanel.Controls.Add(Label2)
        MainPanel.Controls.Add(LastUpdatedLabel)
        MainPanel.Controls.Add(RunningEquipmentLabel)
        MainPanel.Controls.Add(RefreshThermostatButton)
        MainPanel.Controls.Add(ModeLabel)
        MainPanel.Controls.Add(Label4)
        MainPanel.Controls.Add(ThermostatDropDown)
        MainPanel.Controls.Add(Label5)
        MainPanel.Controls.Add(RefreshListButton)
        MainPanel.Controls.Add(Label1)
        MainPanel.Dock = DockStyle.Fill
        MainPanel.Location = New Point(0, 0)
        MainPanel.Name = "MainPanel"
        MainPanel.Size = New Size(638, 439)
        MainPanel.TabIndex = 0
        ' 
        ' TabControl1
        ' 
        TabControl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Location = New Point(3, 97)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(632, 339)
        TabControl1.TabIndex = 11
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(ClearHoldButton)
        TabPage1.Controls.Add(GroupBox4)
        TabPage1.Controls.Add(GroupBox5)
        TabPage1.Controls.Add(GroupBox1)
        TabPage1.Controls.Add(GroupBox7)
        TabPage1.Controls.Add(GroupBox6)
        TabPage1.Controls.Add(GroupBox3)
        TabPage1.Controls.Add(GroupBox2)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(624, 311)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Thermostat"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' ClearHoldButton
        ' 
        ClearHoldButton.Location = New Point(6, 258)
        ClearHoldButton.Name = "ClearHoldButton"
        ClearHoldButton.Size = New Size(180, 23)
        ClearHoldButton.TabIndex = 10
        ClearHoldButton.Text = "Clear hold"
        ClearHoldButton.UseVisualStyleBackColor = True
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(Button12)
        GroupBox4.Controls.Add(Button11)
        GroupBox4.Controls.Add(Button10)
        GroupBox4.Controls.Add(Button9)
        GroupBox4.Controls.Add(Button8)
        GroupBox4.Controls.Add(Button7)
        GroupBox4.Location = New Point(6, 132)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(180, 120)
        GroupBox4.TabIndex = 3
        GroupBox4.TabStop = False
        GroupBox4.Text = "Away"
        ' 
        ' Button12
        ' 
        Button12.Location = New Point(99, 80)
        Button12.Name = "Button12"
        Button12.Size = New Size(75, 23)
        Button12.TabIndex = 6
        Button12.Text = "until 9 pm"
        Button12.UseVisualStyleBackColor = True
        ' 
        ' Button11
        ' 
        Button11.Location = New Point(99, 51)
        Button11.Name = "Button11"
        Button11.Size = New Size(75, 23)
        Button11.TabIndex = 5
        Button11.Text = "until 4 pm"
        Button11.UseVisualStyleBackColor = True
        ' 
        ' Button10
        ' 
        Button10.Location = New Point(99, 22)
        Button10.Name = "Button10"
        Button10.Size = New Size(75, 23)
        Button10.TabIndex = 4
        Button10.Text = "until 7 am"
        Button10.UseVisualStyleBackColor = True
        ' 
        ' Button9
        ' 
        Button9.Location = New Point(6, 80)
        Button9.Name = "Button9"
        Button9.Size = New Size(75, 23)
        Button9.TabIndex = 3
        Button9.Text = "for 1 week"
        Button9.UseVisualStyleBackColor = True
        ' 
        ' Button8
        ' 
        Button8.Location = New Point(6, 51)
        Button8.Name = "Button8"
        Button8.Size = New Size(75, 23)
        Button8.TabIndex = 2
        Button8.Text = "for 1 day"
        Button8.UseVisualStyleBackColor = True
        ' 
        ' Button7
        ' 
        Button7.Location = New Point(6, 22)
        Button7.Name = "Button7"
        Button7.Size = New Size(75, 23)
        Button7.TabIndex = 1
        Button7.Text = "for 1 hour"
        Button7.UseVisualStyleBackColor = True
        ' 
        ' GroupBox5
        ' 
        GroupBox5.Controls.Add(OutdoorForecast)
        GroupBox5.Location = New Point(380, 132)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Size = New Size(182, 120)
        GroupBox5.TabIndex = 9
        GroupBox5.TabStop = False
        GroupBox5.Text = "Forecast"
        ' 
        ' OutdoorForecast
        ' 
        OutdoorForecast.Location = New Point(6, 22)
        OutdoorForecast.Name = "OutdoorForecast"
        OutdoorForecast.Size = New Size(170, 92)
        OutdoorForecast.TabIndex = 0
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(OutdoorWeather)
        GroupBox1.Location = New Point(380, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(182, 120)
        GroupBox1.TabIndex = 8
        GroupBox1.TabStop = False
        GroupBox1.Text = "Weather"
        ' 
        ' OutdoorWeather
        ' 
        OutdoorWeather.Location = New Point(6, 22)
        OutdoorWeather.Name = "OutdoorWeather"
        OutdoorWeather.Size = New Size(170, 92)
        OutdoorWeather.TabIndex = 0
        ' 
        ' GroupBox7
        ' 
        GroupBox7.Controls.Add(IndoorRuntime)
        GroupBox7.Location = New Point(192, 132)
        GroupBox7.Name = "GroupBox7"
        GroupBox7.Size = New Size(182, 120)
        GroupBox7.TabIndex = 7
        GroupBox7.TabStop = False
        GroupBox7.Text = "Runtime"
        ' 
        ' IndoorRuntime
        ' 
        IndoorRuntime.Location = New Point(6, 22)
        IndoorRuntime.Name = "IndoorRuntime"
        IndoorRuntime.Size = New Size(170, 92)
        IndoorRuntime.TabIndex = 0
        ' 
        ' GroupBox6
        ' 
        GroupBox6.Controls.Add(IndoorCurrent)
        GroupBox6.Location = New Point(192, 6)
        GroupBox6.Name = "GroupBox6"
        GroupBox6.Size = New Size(182, 120)
        GroupBox6.TabIndex = 6
        GroupBox6.TabStop = False
        GroupBox6.Text = "Current"
        ' 
        ' IndoorCurrent
        ' 
        IndoorCurrent.Location = New Point(6, 22)
        IndoorCurrent.Name = "IndoorCurrent"
        IndoorCurrent.Size = New Size(170, 92)
        IndoorCurrent.TabIndex = 0
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(Button4)
        GroupBox3.Controls.Add(Button5)
        GroupBox3.Controls.Add(Button6)
        GroupBox3.Location = New Point(99, 6)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(87, 120)
        GroupBox3.TabIndex = 1
        GroupBox3.TabStop = False
        GroupBox3.Text = "30m Hold"
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(6, 80)
        Button4.Name = "Button4"
        Button4.Size = New Size(75, 23)
        Button4.TabIndex = 2
        Button4.Text = "Fan"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.BackColor = Color.LightCyan
        Button5.Location = New Point(6, 51)
        Button5.Name = "Button5"
        Button5.Size = New Size(75, 23)
        Button5.TabIndex = 1
        Button5.Text = "Cool"
        Button5.UseVisualStyleBackColor = False
        ' 
        ' Button6
        ' 
        Button6.BackColor = Color.MistyRose
        Button6.Location = New Point(6, 22)
        Button6.Name = "Button6"
        Button6.Size = New Size(75, 23)
        Button6.TabIndex = 0
        Button6.Text = "Heat"
        Button6.UseVisualStyleBackColor = False
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Button3)
        GroupBox2.Controls.Add(Button2)
        GroupBox2.Controls.Add(Button1)
        GroupBox2.Location = New Point(6, 6)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(87, 120)
        GroupBox2.TabIndex = 0
        GroupBox2.TabStop = False
        GroupBox2.Text = "15m Hold"
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(6, 80)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 23)
        Button3.TabIndex = 2
        Button3.Text = "Fan"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.LightCyan
        Button2.Location = New Point(6, 51)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 1
        Button2.Text = "Cool"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.MistyRose
        Button1.Location = New Point(6, 22)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 0
        Button1.Text = "Heat"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(AbstractionBox)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(624, 311)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Abstraction"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' AbstractionBox
        ' 
        AbstractionBox.Dock = DockStyle.Fill
        AbstractionBox.Location = New Point(3, 3)
        AbstractionBox.Multiline = True
        AbstractionBox.Name = "AbstractionBox"
        AbstractionBox.ReadOnly = True
        AbstractionBox.ScrollBars = ScrollBars.Both
        AbstractionBox.Size = New Size(618, 305)
        AbstractionBox.TabIndex = 0
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        Label2.Location = New Point(12, 41)
        Label2.Name = "Label2"
        Label2.Size = New Size(42, 15)
        Label2.TabIndex = 3
        Label2.Text = "Mode:"
        ' 
        ' LastUpdatedLabel
        ' 
        LastUpdatedLabel.AutoSize = True
        LastUpdatedLabel.Location = New Point(138, 71)
        LastUpdatedLabel.Name = "LastUpdatedLabel"
        LastUpdatedLabel.Size = New Size(32, 15)
        LastUpdatedLabel.TabIndex = 8
        LastUpdatedLabel.Text = "-----"
        ' 
        ' RunningEquipmentLabel
        ' 
        RunningEquipmentLabel.AutoSize = True
        RunningEquipmentLabel.Location = New Point(138, 56)
        RunningEquipmentLabel.Name = "RunningEquipmentLabel"
        RunningEquipmentLabel.Size = New Size(32, 15)
        RunningEquipmentLabel.TabIndex = 6
        RunningEquipmentLabel.Text = "-----"
        ' 
        ' RefreshThermostatButton
        ' 
        RefreshThermostatButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        RefreshThermostatButton.Location = New Point(551, 41)
        RefreshThermostatButton.Name = "RefreshThermostatButton"
        RefreshThermostatButton.Size = New Size(75, 50)
        RefreshThermostatButton.TabIndex = 10
        RefreshThermostatButton.Text = "Refresh"
        RefreshThermostatButton.UseVisualStyleBackColor = True
        ' 
        ' ModeLabel
        ' 
        ModeLabel.AutoSize = True
        ModeLabel.Location = New Point(138, 41)
        ModeLabel.Name = "ModeLabel"
        ModeLabel.Size = New Size(32, 15)
        ModeLabel.TabIndex = 4
        ModeLabel.Text = "-----"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        Label4.Location = New Point(12, 56)
        Label4.Name = "Label4"
        Label4.Size = New Size(120, 15)
        Label4.TabIndex = 5
        Label4.Text = "Running equipment:"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        Label5.Location = New Point(12, 71)
        Label5.Name = "Label5"
        Label5.Size = New Size(81, 15)
        Label5.TabIndex = 7
        Label5.Text = "Last updated:"
        ' 
        ' AlertLabel
        ' 
        AlertLabel.BackColor = SystemColors.Highlight
        AlertLabel.Dock = DockStyle.Bottom
        AlertLabel.ForeColor = SystemColors.HighlightText
        AlertLabel.Location = New Point(0, 439)
        AlertLabel.Name = "AlertLabel"
        AlertLabel.Size = New Size(638, 20)
        AlertLabel.TabIndex = 9
        AlertLabel.Text = "Alert active (see Abstraction tab)"
        AlertLabel.TextAlign = ContentAlignment.MiddleCenter
        AlertLabel.Visible = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(638, 459)
        Controls.Add(MainPanel)
        Controls.Add(AlertLabel)
        Name = "Form1"
        Text = "Form1"
        MainPanel.ResumeLayout(False)
        MainPanel.PerformLayout()
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        GroupBox4.ResumeLayout(False)
        GroupBox5.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox7.ResumeLayout(False)
        GroupBox6.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents ThermostatDropDown As ComboBox
    Friend WithEvents RefreshListButton As Button
    Friend WithEvents MainPanel As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents RunningEquipmentLabel As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ModeLabel As Label
    Friend WithEvents LastUpdatedLabel As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents RefreshThermostatButton As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents AbstractionBox As TextBox
    Friend WithEvents AlertLabel As Label
    Friend WithEvents HumidityLabel As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents WeatherHumidityLabel As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents WeatherConditionLabel As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents ForecastGroup As GroupBox
    Friend WithEvents ForecastConditionLabel As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents RuntimeFanLabel As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents RuntimeMaxHumidityLabel As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents RuntimeMinHumidityLabel As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents IndoorCurrent As ReadingControl
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents OutdoorForecast As ReadingControl
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents OutdoorWeather As ReadingControl
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents IndoorRuntime As ReadingControl
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Button12 As Button
    Friend WithEvents Button11 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents ClearHoldButton As Button
End Class
