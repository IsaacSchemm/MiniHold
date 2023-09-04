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
        components = New ComponentModel.Container()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(Form1))
        NotifyIcon1 = New NotifyIcon(components)
        ThermostatDropDown = New ComboBox()
        RefreshListButton = New Button()
        Label1 = New Label()
        ResetButton = New Button()
        GroupBox1 = New GroupBox()
        IndoorHumidityLabel = New Label()
        Label3 = New Label()
        IndoorTemperatureLabel = New Label()
        GroupBox2 = New GroupBox()
        LowLabel = New Label()
        Label7 = New Label()
        HighLabel = New Label()
        Label5 = New Label()
        OutdoorHumidityLabel = New Label()
        Label4 = New Label()
        OutdoorTemperatureLabel = New Label()
        GroupBox3 = New GroupBox()
        DataGridView1 = New DataGridView()
        Column1 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        TableLayoutPanel1 = New TableLayoutPanel()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        TabPage2 = New TabPage()
        ActiveHoldLabel = New Label()
        ClearHoldButton = New Button()
        GroupBox5 = New GroupBox()
        TableLayoutPanel2 = New TableLayoutPanel()
        AwayButton = New Button()
        HeatButton = New Button()
        CoolButton = New Button()
        FanButton = New Button()
        GroupBox4 = New GroupBox()
        AbstractionTextBox = New TextBox()
        Timer1 = New Timer(components)
        Panel1 = New Panel()
        AlertLabel = New Label()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox3.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        TableLayoutPanel1.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        TabPage2.SuspendLayout()
        GroupBox5.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        GroupBox4.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' NotifyIcon1
        ' 
        NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), Icon)
        NotifyIcon1.Text = "MiniHold Tray Utility - No information loaded"
        NotifyIcon1.Visible = True
        ' 
        ' ThermostatDropDown
        ' 
        ThermostatDropDown.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        ThermostatDropDown.DropDownStyle = ComboBoxStyle.DropDownList
        ThermostatDropDown.FormattingEnabled = True
        ThermostatDropDown.Location = New Point(85, 12)
        ThermostatDropDown.Name = "ThermostatDropDown"
        ThermostatDropDown.Size = New Size(207, 23)
        ThermostatDropDown.TabIndex = 4
        ' 
        ' RefreshListButton
        ' 
        RefreshListButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        RefreshListButton.Location = New Point(111, 41)
        RefreshListButton.Name = "RefreshListButton"
        RefreshListButton.Size = New Size(100, 23)
        RefreshListButton.TabIndex = 5
        RefreshListButton.Text = "Get From API"
        RefreshListButton.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 15)
        Label1.Name = "Label1"
        Label1.Size = New Size(67, 15)
        Label1.TabIndex = 3
        Label1.Text = "Thermostat"
        ' 
        ' ResetButton
        ' 
        ResetButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ResetButton.Location = New Point(217, 41)
        ResetButton.Name = "ResetButton"
        ResetButton.Size = New Size(75, 23)
        ResetButton.TabIndex = 6
        ResetButton.Text = "Reset"
        ResetButton.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(IndoorHumidityLabel)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(IndoorTemperatureLabel)
        GroupBox1.Dock = DockStyle.Fill
        GroupBox1.Location = New Point(3, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(124, 124)
        GroupBox1.TabIndex = 7
        GroupBox1.TabStop = False
        GroupBox1.Text = "Indoor"
        ' 
        ' IndoorHumidityLabel
        ' 
        IndoorHumidityLabel.AutoSize = True
        IndoorHumidityLabel.Location = New Point(76, 67)
        IndoorHumidityLabel.Name = "IndoorHumidityLabel"
        IndoorHumidityLabel.Size = New Size(16, 15)
        IndoorHumidityLabel.TabIndex = 2
        IndoorHumidityLabel.Text = "   "
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(10, 67)
        Label3.Name = "Label3"
        Label3.Size = New Size(60, 15)
        Label3.TabIndex = 1
        Label3.Text = "Humidity:"
        ' 
        ' IndoorTemperatureLabel
        ' 
        IndoorTemperatureLabel.AutoSize = True
        IndoorTemperatureLabel.Font = New Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point)
        IndoorTemperatureLabel.Location = New Point(6, 19)
        IndoorTemperatureLabel.Name = "IndoorTemperatureLabel"
        IndoorTemperatureLabel.Size = New Size(50, 48)
        IndoorTemperatureLabel.TabIndex = 0
        IndoorTemperatureLabel.Text = "   "
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(LowLabel)
        GroupBox2.Controls.Add(Label7)
        GroupBox2.Controls.Add(HighLabel)
        GroupBox2.Controls.Add(Label5)
        GroupBox2.Controls.Add(OutdoorHumidityLabel)
        GroupBox2.Controls.Add(Label4)
        GroupBox2.Controls.Add(OutdoorTemperatureLabel)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(133, 3)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(124, 124)
        GroupBox2.TabIndex = 8
        GroupBox2.TabStop = False
        GroupBox2.Text = "Outdoor"
        ' 
        ' LowLabel
        ' 
        LowLabel.AutoSize = True
        LowLabel.Location = New Point(52, 97)
        LowLabel.Name = "LowLabel"
        LowLabel.Size = New Size(16, 15)
        LowLabel.TabIndex = 6
        LowLabel.Text = "   "
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(10, 97)
        Label7.Name = "Label7"
        Label7.Size = New Size(32, 15)
        Label7.TabIndex = 5
        Label7.Text = "Low:"
        ' 
        ' HighLabel
        ' 
        HighLabel.AutoSize = True
        HighLabel.Location = New Point(52, 82)
        HighLabel.Name = "HighLabel"
        HighLabel.Size = New Size(16, 15)
        HighLabel.TabIndex = 4
        HighLabel.Text = "   "
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(10, 82)
        Label5.Name = "Label5"
        Label5.Size = New Size(36, 15)
        Label5.TabIndex = 3
        Label5.Text = "High:"
        ' 
        ' OutdoorHumidityLabel
        ' 
        OutdoorHumidityLabel.AutoSize = True
        OutdoorHumidityLabel.Location = New Point(76, 67)
        OutdoorHumidityLabel.Name = "OutdoorHumidityLabel"
        OutdoorHumidityLabel.Size = New Size(16, 15)
        OutdoorHumidityLabel.TabIndex = 2
        OutdoorHumidityLabel.Text = "   "
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(10, 67)
        Label4.Name = "Label4"
        Label4.Size = New Size(60, 15)
        Label4.TabIndex = 1
        Label4.Text = "Humidity:"
        ' 
        ' OutdoorTemperatureLabel
        ' 
        OutdoorTemperatureLabel.AutoSize = True
        OutdoorTemperatureLabel.Font = New Font("Segoe UI", 27F, FontStyle.Regular, GraphicsUnit.Point)
        OutdoorTemperatureLabel.Location = New Point(6, 19)
        OutdoorTemperatureLabel.Name = "OutdoorTemperatureLabel"
        OutdoorTemperatureLabel.Size = New Size(50, 48)
        OutdoorTemperatureLabel.TabIndex = 0
        OutdoorTemperatureLabel.Text = "   "
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox3.Controls.Add(DataGridView1)
        GroupBox3.Location = New Point(6, 142)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(260, 83)
        GroupBox3.TabIndex = 9
        GroupBox3.TabStop = False
        GroupBox3.Text = "Sensors"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {Column1, Column2})
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(3, 19)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowTemplate.Height = 25
        DataGridView1.Size = New Size(254, 61)
        DataGridView1.TabIndex = 0
        ' 
        ' Column1
        ' 
        Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Column1.HeaderText = "Name"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        ' 
        ' Column2
        ' 
        Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Column2.HeaderText = "Temperature"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Controls.Add(GroupBox1, 0, 0)
        TableLayoutPanel1.Controls.Add(GroupBox2, 1, 0)
        TableLayoutPanel1.Location = New Point(6, 6)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Size = New Size(260, 130)
        TableLayoutPanel1.TabIndex = 10
        ' 
        ' TabControl1
        ' 
        TabControl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Location = New Point(12, 70)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(280, 259)
        TabControl1.TabIndex = 12
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(GroupBox3)
        TabPage1.Controls.Add(TableLayoutPanel1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(272, 231)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Temperature"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(ActiveHoldLabel)
        TabPage2.Controls.Add(ClearHoldButton)
        TabPage2.Controls.Add(GroupBox5)
        TabPage2.Controls.Add(GroupBox4)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(272, 231)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Advanced"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' ActiveHoldLabel
        ' 
        ActiveHoldLabel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ActiveHoldLabel.AutoSize = True
        ActiveHoldLabel.Location = New Point(6, 206)
        ActiveHoldLabel.Name = "ActiveHoldLabel"
        ActiveHoldLabel.Size = New Size(16, 15)
        ActiveHoldLabel.TabIndex = 5
        ActiveHoldLabel.Text = "   "
        ' 
        ' ClearHoldButton
        ' 
        ClearHoldButton.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ClearHoldButton.Location = New Point(201, 202)
        ClearHoldButton.Name = "ClearHoldButton"
        ClearHoldButton.Size = New Size(59, 23)
        ClearHoldButton.TabIndex = 4
        ClearHoldButton.Text = "Clear Hold"
        ClearHoldButton.UseVisualStyleBackColor = True
        ' 
        ' GroupBox5
        ' 
        GroupBox5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox5.Controls.Add(TableLayoutPanel2)
        GroupBox5.Location = New Point(6, 145)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Size = New Size(260, 51)
        GroupBox5.TabIndex = 1
        GroupBox5.TabStop = False
        GroupBox5.Text = "30-Minute Hold"
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 4
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.Controls.Add(AwayButton, 3, 0)
        TableLayoutPanel2.Controls.Add(HeatButton, 0, 0)
        TableLayoutPanel2.Controls.Add(CoolButton, 1, 0)
        TableLayoutPanel2.Controls.Add(FanButton, 2, 0)
        TableLayoutPanel2.Dock = DockStyle.Fill
        TableLayoutPanel2.Location = New Point(3, 19)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 1
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel2.Size = New Size(254, 29)
        TableLayoutPanel2.TabIndex = 4
        ' 
        ' AwayButton
        ' 
        AwayButton.Dock = DockStyle.Fill
        AwayButton.Location = New Point(192, 3)
        AwayButton.Name = "AwayButton"
        AwayButton.Size = New Size(59, 23)
        AwayButton.TabIndex = 3
        AwayButton.Text = "Away"
        AwayButton.UseVisualStyleBackColor = True
        ' 
        ' HeatButton
        ' 
        HeatButton.Dock = DockStyle.Fill
        HeatButton.Location = New Point(3, 3)
        HeatButton.Name = "HeatButton"
        HeatButton.Size = New Size(57, 23)
        HeatButton.TabIndex = 0
        HeatButton.Text = "Heat"
        HeatButton.UseVisualStyleBackColor = True
        ' 
        ' CoolButton
        ' 
        CoolButton.Dock = DockStyle.Fill
        CoolButton.Location = New Point(66, 3)
        CoolButton.Name = "CoolButton"
        CoolButton.Size = New Size(57, 23)
        CoolButton.TabIndex = 1
        CoolButton.Text = "Cool"
        CoolButton.UseVisualStyleBackColor = True
        ' 
        ' FanButton
        ' 
        FanButton.Dock = DockStyle.Fill
        FanButton.Location = New Point(129, 3)
        FanButton.Name = "FanButton"
        FanButton.Size = New Size(57, 23)
        FanButton.TabIndex = 2
        FanButton.Text = "Fan"
        FanButton.UseVisualStyleBackColor = True
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox4.Controls.Add(AbstractionTextBox)
        GroupBox4.Location = New Point(6, 6)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(260, 133)
        GroupBox4.TabIndex = 0
        GroupBox4.TabStop = False
        GroupBox4.Text = "Abstraction"
        ' 
        ' AbstractionTextBox
        ' 
        AbstractionTextBox.Dock = DockStyle.Fill
        AbstractionTextBox.Location = New Point(3, 19)
        AbstractionTextBox.Multiline = True
        AbstractionTextBox.Name = "AbstractionTextBox"
        AbstractionTextBox.ReadOnly = True
        AbstractionTextBox.ScrollBars = ScrollBars.Both
        AbstractionTextBox.Size = New Size(254, 111)
        AbstractionTextBox.TabIndex = 0
        AbstractionTextBox.WordWrap = False
        ' 
        ' Timer1
        ' 
        Timer1.Enabled = True
        Timer1.Interval = 1800000
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(TabControl1)
        Panel1.Controls.Add(ThermostatDropDown)
        Panel1.Controls.Add(ResetButton)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(RefreshListButton)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(304, 341)
        Panel1.TabIndex = 13
        ' 
        ' AlertLabel
        ' 
        AlertLabel.BackColor = SystemColors.Highlight
        AlertLabel.Dock = DockStyle.Bottom
        AlertLabel.ForeColor = SystemColors.HighlightText
        AlertLabel.Location = New Point(0, 341)
        AlertLabel.Name = "AlertLabel"
        AlertLabel.Size = New Size(304, 20)
        AlertLabel.TabIndex = 14
        AlertLabel.Text = "Alert active (see Abstraction tab)"
        AlertLabel.TextAlign = ContentAlignment.MiddleCenter
        AlertLabel.Visible = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(304, 361)
        Controls.Add(Panel1)
        Controls.Add(AlertLabel)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form1"
        Text = "MiniHold Tray Utility"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox3.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        TableLayoutPanel1.ResumeLayout(False)
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        GroupBox5.ResumeLayout(False)
        TableLayoutPanel2.ResumeLayout(False)
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents ThermostatDropDown As ComboBox
    Friend WithEvents RefreshListButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ResetButton As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents IndoorHumidityLabel As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents IndoorTemperatureLabel As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents OutdoorHumidityLabel As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents OutdoorTemperatureLabel As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LowLabel As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents HighLabel As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents ClearHoldButton As Button
    Friend WithEvents AwayButton As Button
    Friend WithEvents FanButton As Button
    Friend WithEvents CoolButton As Button
    Friend WithEvents HeatButton As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents AbstractionTextBox As TextBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents ActiveHoldLabel As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents AlertLabel As Label
End Class
