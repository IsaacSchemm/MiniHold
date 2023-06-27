<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ThermostatForm
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
        GroupBox1 = New GroupBox()
        ThermostatName = New Label()
        Version = New Label()
        Brand = New Label()
        GroupBox10 = New GroupBox()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        TabPage2 = New TabPage()
        TextBox1 = New TextBox()
        Panel1 = New Panel()
        Label2 = New Label()
        TextBox2 = New TextBox()
        SendAlertButton = New Button()
        TabPage3 = New TabPage()
        DataGridView1 = New DataGridView()
        TypeColumn = New DataGridViewTextBoxColumn()
        HeatColumn = New DataGridViewTextBoxColumn()
        CoolColumn = New DataGridViewTextBoxColumn()
        FanColumn = New DataGridViewTextBoxColumn()
        StartTimeColumn = New DataGridViewTextBoxColumn()
        EndTimeColumn = New DataGridViewTextBoxColumn()
        TabPage5 = New TabPage()
        DataGridView2 = New DataGridView()
        SensorColumn1 = New DataGridViewTextBoxColumn()
        SensorColumn2 = New DataGridViewTextBoxColumn()
        SensorColumn3 = New DataGridViewTextBoxColumn()
        SensorColumn4 = New DataGridViewTextBoxColumn()
        TabPage1 = New TabPage()
        GroupBox2 = New GroupBox()
        OutdoorHumidity = New Label()
        Label6 = New Label()
        OutdoorCondition = New Label()
        OutdoorTemp = New Label()
        GroupBox3 = New GroupBox()
        IndoorHumidity = New Label()
        Label9 = New Label()
        IndoorTemp = New Label()
        GroupBox7 = New GroupBox()
        HoldEndLabel = New Label()
        HoldStartLabel = New Label()
        Label5 = New Label()
        Label3 = New Label()
        Label11 = New Label()
        CancelHoldButton = New Button()
        HoldFan = New Label()
        Label16 = New Label()
        HoldHeat = New Label()
        Label18 = New Label()
        HoldCool = New Label()
        GroupBox8 = New GroupBox()
        Label1 = New Label()
        FanState = New Label()
        Label10 = New Label()
        HeatAt = New Label()
        Label14 = New Label()
        CoolAt = New Label()
        GroupBox6 = New GroupBox()
        ProgramName = New Label()
        Label4 = New Label()
        ProgramHeat = New Label()
        Label7 = New Label()
        ProgramCool = New Label()
        GroupBox5 = New GroupBox()
        Button6 = New Button()
        Button4 = New Button()
        Button2 = New Button()
        Button1 = New Button()
        GroupBox4 = New GroupBox()
        Button12 = New Button()
        Button5 = New Button()
        Button7 = New Button()
        Button8 = New Button()
        GroupBox9 = New GroupBox()
        Button9 = New Button()
        Button16 = New Button()
        Button15 = New Button()
        Button14 = New Button()
        Button11 = New Button()
        Button10 = New Button()
        TabControl1 = New TabControl()
        GroupBox1.SuspendLayout()
        GroupBox10.SuspendLayout()
        TabPage2.SuspendLayout()
        Panel1.SuspendLayout()
        TabPage3.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        TabPage5.SuspendLayout()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        TabPage1.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox3.SuspendLayout()
        GroupBox7.SuspendLayout()
        GroupBox8.SuspendLayout()
        GroupBox6.SuspendLayout()
        GroupBox5.SuspendLayout()
        GroupBox4.SuspendLayout()
        GroupBox9.SuspendLayout()
        TabControl1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox1.Controls.Add(ThermostatName)
        GroupBox1.Controls.Add(Version)
        GroupBox1.Controls.Add(Brand)
        GroupBox1.Location = New Point(12, 12)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(339, 83)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Information"
        ' 
        ' ThermostatName
        ' 
        ThermostatName.AutoSize = True
        ThermostatName.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        ThermostatName.Location = New Point(6, 19)
        ThermostatName.Name = "ThermostatName"
        ThermostatName.Size = New Size(22, 15)
        ThermostatName.TabIndex = 0
        ThermostatName.Text = "---"
        ' 
        ' Version
        ' 
        Version.AutoSize = True
        Version.Location = New Point(6, 49)
        Version.Name = "Version"
        Version.Size = New Size(22, 15)
        Version.TabIndex = 2
        Version.Text = "---"
        ' 
        ' Brand
        ' 
        Brand.AutoSize = True
        Brand.Location = New Point(6, 34)
        Brand.Name = "Brand"
        Brand.Size = New Size(22, 15)
        Brand.TabIndex = 1
        Brand.Text = "---"
        ' 
        ' GroupBox10
        ' 
        GroupBox10.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        GroupBox10.Controls.Add(FlowLayoutPanel1)
        GroupBox10.Location = New Point(357, 12)
        GroupBox10.Name = "GroupBox10"
        GroupBox10.Size = New Size(153, 83)
        GroupBox10.TabIndex = 13
        GroupBox10.TabStop = False
        GroupBox10.Text = "Equipment"
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.Location = New Point(3, 19)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(147, 61)
        FlowLayoutPanel1.TabIndex = 0
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(TextBox1)
        TabPage2.Controls.Add(Panel1)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(490, 309)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Abstraction"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Dock = DockStyle.Fill
        TextBox1.Font = New Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point)
        TextBox1.Location = New Point(3, 3)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.ScrollBars = ScrollBars.Both
        TextBox1.Size = New Size(484, 274)
        TextBox1.TabIndex = 10
        TextBox1.WordWrap = False
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(TextBox2)
        Panel1.Controls.Add(SendAlertButton)
        Panel1.Dock = DockStyle.Bottom
        Panel1.Location = New Point(3, 277)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(484, 29)
        Panel1.TabIndex = 11
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(3, 7)
        Label2.Name = "Label2"
        Label2.Size = New Size(87, 15)
        Label2.TabIndex = 1
        Label2.Text = "Send new alert:"
        ' 
        ' TextBox2
        ' 
        TextBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox2.Location = New Point(96, 3)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(304, 23)
        TextBox2.TabIndex = 1
        ' 
        ' SendAlertButton
        ' 
        SendAlertButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        SendAlertButton.Location = New Point(406, 3)
        SendAlertButton.Name = "SendAlertButton"
        SendAlertButton.Size = New Size(75, 23)
        SendAlertButton.TabIndex = 2
        SendAlertButton.Text = "Send"
        SendAlertButton.UseVisualStyleBackColor = True
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(DataGridView1)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(490, 309)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Hold Stack"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToOrderColumns = True
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {TypeColumn, HeatColumn, CoolColumn, FanColumn, StartTimeColumn, EndTimeColumn})
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(3, 3)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.RowTemplate.Height = 25
        DataGridView1.Size = New Size(484, 303)
        DataGridView1.TabIndex = 0
        ' 
        ' TypeColumn
        ' 
        TypeColumn.HeaderText = "Type"
        TypeColumn.Name = "TypeColumn"
        TypeColumn.ReadOnly = True
        ' 
        ' HeatColumn
        ' 
        HeatColumn.HeaderText = "Heat"
        HeatColumn.Name = "HeatColumn"
        HeatColumn.ReadOnly = True
        ' 
        ' CoolColumn
        ' 
        CoolColumn.HeaderText = "Cool"
        CoolColumn.Name = "CoolColumn"
        CoolColumn.ReadOnly = True
        ' 
        ' FanColumn
        ' 
        FanColumn.HeaderText = "Fan"
        FanColumn.Name = "FanColumn"
        FanColumn.ReadOnly = True
        ' 
        ' StartTimeColumn
        ' 
        StartTimeColumn.HeaderText = "Start"
        StartTimeColumn.Name = "StartTimeColumn"
        StartTimeColumn.ReadOnly = True
        ' 
        ' EndTimeColumn
        ' 
        EndTimeColumn.HeaderText = "End"
        EndTimeColumn.Name = "EndTimeColumn"
        EndTimeColumn.ReadOnly = True
        ' 
        ' TabPage5
        ' 
        TabPage5.Controls.Add(DataGridView2)
        TabPage5.Location = New Point(4, 24)
        TabPage5.Name = "TabPage5"
        TabPage5.Padding = New Padding(3)
        TabPage5.Size = New Size(490, 309)
        TabPage5.TabIndex = 4
        TabPage5.Text = "Sensors"
        TabPage5.UseVisualStyleBackColor = True
        ' 
        ' DataGridView2
        ' 
        DataGridView2.AllowUserToAddRows = False
        DataGridView2.AllowUserToDeleteRows = False
        DataGridView2.AllowUserToOrderColumns = True
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Columns.AddRange(New DataGridViewColumn() {SensorColumn1, SensorColumn2, SensorColumn3, SensorColumn4})
        DataGridView2.Dock = DockStyle.Fill
        DataGridView2.Location = New Point(3, 3)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.ReadOnly = True
        DataGridView2.RowTemplate.Height = 25
        DataGridView2.Size = New Size(484, 303)
        DataGridView2.TabIndex = 0
        ' 
        ' SensorColumn1
        ' 
        SensorColumn1.HeaderText = "Name"
        SensorColumn1.Name = "SensorColumn1"
        SensorColumn1.ReadOnly = True
        ' 
        ' SensorColumn2
        ' 
        SensorColumn2.HeaderText = "Temperature"
        SensorColumn2.Name = "SensorColumn2"
        SensorColumn2.ReadOnly = True
        ' 
        ' SensorColumn3
        ' 
        SensorColumn3.HeaderText = "Occupied"
        SensorColumn3.Name = "SensorColumn3"
        SensorColumn3.ReadOnly = True
        ' 
        ' SensorColumn4
        ' 
        SensorColumn4.HeaderText = "Differential"
        SensorColumn4.Name = "SensorColumn4"
        SensorColumn4.ReadOnly = True
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(GroupBox2)
        TabPage1.Controls.Add(GroupBox3)
        TabPage1.Controls.Add(GroupBox7)
        TabPage1.Controls.Add(GroupBox8)
        TabPage1.Controls.Add(GroupBox6)
        TabPage1.Controls.Add(GroupBox5)
        TabPage1.Controls.Add(GroupBox4)
        TabPage1.Controls.Add(GroupBox9)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(490, 309)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Controls"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' GroupBox2
        ' 
        GroupBox2.AutoSize = True
        GroupBox2.Controls.Add(OutdoorHumidity)
        GroupBox2.Controls.Add(Label6)
        GroupBox2.Controls.Add(OutdoorCondition)
        GroupBox2.Controls.Add(OutdoorTemp)
        GroupBox2.Location = New Point(6, 6)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(143, 100)
        GroupBox2.TabIndex = 0
        GroupBox2.TabStop = False
        GroupBox2.Text = "Outdoor"
        ' 
        ' OutdoorHumidity
        ' 
        OutdoorHumidity.AutoSize = True
        OutdoorHumidity.Location = New Point(69, 66)
        OutdoorHumidity.Name = "OutdoorHumidity"
        OutdoorHumidity.Size = New Size(22, 15)
        OutdoorHumidity.TabIndex = 4
        OutdoorHumidity.Text = "---"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(6, 66)
        Label6.Name = "Label6"
        Label6.Size = New Size(57, 15)
        Label6.TabIndex = 3
        Label6.Text = "Humidity"
        ' 
        ' OutdoorCondition
        ' 
        OutdoorCondition.AutoSize = True
        OutdoorCondition.Location = New Point(6, 19)
        OutdoorCondition.Name = "OutdoorCondition"
        OutdoorCondition.Size = New Size(22, 15)
        OutdoorCondition.TabIndex = 2
        OutdoorCondition.Text = "---"
        ' 
        ' OutdoorTemp
        ' 
        OutdoorTemp.AutoSize = True
        OutdoorTemp.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        OutdoorTemp.Location = New Point(6, 34)
        OutdoorTemp.Name = "OutdoorTemp"
        OutdoorTemp.Size = New Size(44, 32)
        OutdoorTemp.TabIndex = 1
        OutdoorTemp.Text = "---"
        ' 
        ' GroupBox3
        ' 
        GroupBox3.AutoSize = True
        GroupBox3.Controls.Add(IndoorHumidity)
        GroupBox3.Controls.Add(Label9)
        GroupBox3.Controls.Add(IndoorTemp)
        GroupBox3.Location = New Point(6, 112)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(143, 85)
        GroupBox3.TabIndex = 1
        GroupBox3.TabStop = False
        GroupBox3.Text = "Indoor"
        ' 
        ' IndoorHumidity
        ' 
        IndoorHumidity.AutoSize = True
        IndoorHumidity.Location = New Point(69, 51)
        IndoorHumidity.Name = "IndoorHumidity"
        IndoorHumidity.Size = New Size(22, 15)
        IndoorHumidity.TabIndex = 5
        IndoorHumidity.Text = "---"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(6, 51)
        Label9.Name = "Label9"
        Label9.Size = New Size(57, 15)
        Label9.TabIndex = 3
        Label9.Text = "Humidity"
        ' 
        ' IndoorTemp
        ' 
        IndoorTemp.AutoSize = True
        IndoorTemp.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        IndoorTemp.Location = New Point(6, 19)
        IndoorTemp.Name = "IndoorTemp"
        IndoorTemp.Size = New Size(44, 32)
        IndoorTemp.TabIndex = 1
        IndoorTemp.Text = "---"
        ' 
        ' GroupBox7
        ' 
        GroupBox7.AutoSize = True
        GroupBox7.Controls.Add(HoldEndLabel)
        GroupBox7.Controls.Add(HoldStartLabel)
        GroupBox7.Controls.Add(Label5)
        GroupBox7.Controls.Add(Label3)
        GroupBox7.Controls.Add(Label11)
        GroupBox7.Controls.Add(CancelHoldButton)
        GroupBox7.Controls.Add(HoldFan)
        GroupBox7.Controls.Add(Label16)
        GroupBox7.Controls.Add(HoldHeat)
        GroupBox7.Controls.Add(Label18)
        GroupBox7.Controls.Add(HoldCool)
        GroupBox7.Location = New Point(341, 112)
        GroupBox7.Name = "GroupBox7"
        GroupBox7.Size = New Size(143, 164)
        GroupBox7.TabIndex = 8
        GroupBox7.TabStop = False
        GroupBox7.Text = "Hold"
        ' 
        ' HoldEndLabel
        ' 
        HoldEndLabel.AutoSize = True
        HoldEndLabel.Location = New Point(43, 96)
        HoldEndLabel.Name = "HoldEndLabel"
        HoldEndLabel.Size = New Size(22, 15)
        HoldEndLabel.TabIndex = 16
        HoldEndLabel.Text = "---"
        ' 
        ' HoldStartLabel
        ' 
        HoldStartLabel.AutoSize = True
        HoldStartLabel.Location = New Point(43, 81)
        HoldStartLabel.Name = "HoldStartLabel"
        HoldStartLabel.Size = New Size(22, 15)
        HoldStartLabel.TabIndex = 15
        HoldStartLabel.Text = "---"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(6, 96)
        Label5.Name = "Label5"
        Label5.Size = New Size(27, 15)
        Label5.TabIndex = 14
        Label5.Text = "End"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(6, 81)
        Label3.Name = "Label3"
        Label3.Size = New Size(31, 15)
        Label3.TabIndex = 13
        Label3.Text = "Start"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(6, 66)
        Label11.Name = "Label11"
        Label11.Size = New Size(26, 15)
        Label11.TabIndex = 6
        Label11.Text = "Fan"
        ' 
        ' CancelHoldButton
        ' 
        CancelHoldButton.Location = New Point(6, 118)
        CancelHoldButton.Name = "CancelHoldButton"
        CancelHoldButton.Size = New Size(131, 24)
        CancelHoldButton.TabIndex = 6
        CancelHoldButton.Text = "Cancel"
        CancelHoldButton.UseVisualStyleBackColor = True
        ' 
        ' HoldFan
        ' 
        HoldFan.AutoSize = True
        HoldFan.Location = New Point(43, 66)
        HoldFan.Name = "HoldFan"
        HoldFan.Size = New Size(22, 15)
        HoldFan.TabIndex = 6
        HoldFan.Text = "---"
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(6, 19)
        Label16.Name = "Label16"
        Label16.Size = New Size(45, 15)
        Label16.TabIndex = 9
        Label16.Text = "Heat at"
        ' 
        ' HoldHeat
        ' 
        HoldHeat.AutoSize = True
        HoldHeat.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        HoldHeat.Location = New Point(6, 34)
        HoldHeat.Name = "HoldHeat"
        HoldHeat.Size = New Size(44, 32)
        HoldHeat.TabIndex = 10
        HoldHeat.Text = "---"
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(63, 19)
        Label18.Name = "Label18"
        Label18.Size = New Size(45, 15)
        Label18.TabIndex = 11
        Label18.Text = "Cool at"
        ' 
        ' HoldCool
        ' 
        HoldCool.AutoSize = True
        HoldCool.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        HoldCool.Location = New Point(63, 34)
        HoldCool.Name = "HoldCool"
        HoldCool.Size = New Size(74, 32)
        HoldCool.TabIndex = 12
        HoldCool.Text = "------"
        ' 
        ' GroupBox8
        ' 
        GroupBox8.AutoSize = True
        GroupBox8.Controls.Add(Label1)
        GroupBox8.Controls.Add(FanState)
        GroupBox8.Controls.Add(Label10)
        GroupBox8.Controls.Add(HeatAt)
        GroupBox8.Controls.Add(Label14)
        GroupBox8.Controls.Add(CoolAt)
        GroupBox8.Location = New Point(6, 203)
        GroupBox8.Name = "GroupBox8"
        GroupBox8.Size = New Size(143, 100)
        GroupBox8.TabIndex = 2
        GroupBox8.TabStop = False
        GroupBox8.Text = "Runtime"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 66)
        Label1.Name = "Label1"
        Label1.Size = New Size(26, 15)
        Label1.TabIndex = 6
        Label1.Text = "Fan"
        ' 
        ' FanState
        ' 
        FanState.AutoSize = True
        FanState.Location = New Point(69, 66)
        FanState.Name = "FanState"
        FanState.Size = New Size(22, 15)
        FanState.TabIndex = 6
        FanState.Text = "---"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(6, 19)
        Label10.Name = "Label10"
        Label10.Size = New Size(45, 15)
        Label10.TabIndex = 9
        Label10.Text = "Heat at"
        ' 
        ' HeatAt
        ' 
        HeatAt.AutoSize = True
        HeatAt.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        HeatAt.Location = New Point(6, 34)
        HeatAt.Name = "HeatAt"
        HeatAt.Size = New Size(44, 32)
        HeatAt.TabIndex = 10
        HeatAt.Text = "---"
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(63, 19)
        Label14.Name = "Label14"
        Label14.Size = New Size(45, 15)
        Label14.TabIndex = 11
        Label14.Text = "Cool at"
        ' 
        ' CoolAt
        ' 
        CoolAt.AutoSize = True
        CoolAt.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        CoolAt.Location = New Point(63, 34)
        CoolAt.Name = "CoolAt"
        CoolAt.Size = New Size(74, 32)
        CoolAt.TabIndex = 12
        CoolAt.Text = "------"
        ' 
        ' GroupBox6
        ' 
        GroupBox6.AutoSize = True
        GroupBox6.Controls.Add(ProgramName)
        GroupBox6.Controls.Add(Label4)
        GroupBox6.Controls.Add(ProgramHeat)
        GroupBox6.Controls.Add(Label7)
        GroupBox6.Controls.Add(ProgramCool)
        GroupBox6.Location = New Point(341, 6)
        GroupBox6.Name = "GroupBox6"
        GroupBox6.Size = New Size(143, 100)
        GroupBox6.TabIndex = 7
        GroupBox6.TabStop = False
        GroupBox6.Text = "Program"
        ' 
        ' ProgramName
        ' 
        ProgramName.AutoSize = True
        ProgramName.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point)
        ProgramName.Location = New Point(6, 19)
        ProgramName.Name = "ProgramName"
        ProgramName.Size = New Size(22, 15)
        ProgramName.TabIndex = 13
        ProgramName.Text = "---"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(6, 34)
        Label4.Name = "Label4"
        Label4.Size = New Size(45, 15)
        Label4.TabIndex = 9
        Label4.Text = "Heat at"
        ' 
        ' ProgramHeat
        ' 
        ProgramHeat.AutoSize = True
        ProgramHeat.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        ProgramHeat.Location = New Point(6, 49)
        ProgramHeat.Name = "ProgramHeat"
        ProgramHeat.Size = New Size(44, 32)
        ProgramHeat.TabIndex = 10
        ProgramHeat.Text = "---"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(63, 34)
        Label7.Name = "Label7"
        Label7.Size = New Size(45, 15)
        Label7.TabIndex = 11
        Label7.Text = "Cool at"
        ' 
        ' ProgramCool
        ' 
        ProgramCool.AutoSize = True
        ProgramCool.Font = New Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point)
        ProgramCool.Location = New Point(63, 49)
        ProgramCool.Name = "ProgramCool"
        ProgramCool.Size = New Size(74, 32)
        ProgramCool.TabIndex = 12
        ProgramCool.Text = "------"
        ' 
        ' GroupBox5
        ' 
        GroupBox5.AutoSize = True
        GroupBox5.Controls.Add(Button6)
        GroupBox5.Controls.Add(Button4)
        GroupBox5.Controls.Add(Button2)
        GroupBox5.Controls.Add(Button1)
        GroupBox5.Location = New Point(155, 6)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Size = New Size(87, 154)
        GroupBox5.TabIndex = 3
        GroupBox5.TabStop = False
        GroupBox5.Text = "10m Hold"
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(6, 109)
        Button6.Name = "Button6"
        Button6.Size = New Size(75, 23)
        Button6.TabIndex = 10
        Button6.Text = "Away"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(6, 80)
        Button4.Name = "Button4"
        Button4.Size = New Size(75, 23)
        Button4.TabIndex = 9
        Button4.Text = "Fan"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.LightCyan
        Button2.ForeColor = Color.Black
        Button2.Location = New Point(6, 51)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 7
        Button2.Text = "-2°"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.MistyRose
        Button1.ForeColor = Color.Black
        Button1.Location = New Point(6, 22)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 6
        Button1.Text = "+2°"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' GroupBox4
        ' 
        GroupBox4.AutoSize = True
        GroupBox4.Controls.Add(Button12)
        GroupBox4.Controls.Add(Button5)
        GroupBox4.Controls.Add(Button7)
        GroupBox4.Controls.Add(Button8)
        GroupBox4.Location = New Point(248, 6)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(87, 154)
        GroupBox4.TabIndex = 4
        GroupBox4.TabStop = False
        GroupBox4.Text = "30m Hold"
        ' 
        ' Button12
        ' 
        Button12.Location = New Point(6, 109)
        Button12.Name = "Button12"
        Button12.Size = New Size(75, 23)
        Button12.TabIndex = 11
        Button12.Text = "Away"
        Button12.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(6, 80)
        Button5.Name = "Button5"
        Button5.Size = New Size(75, 23)
        Button5.TabIndex = 9
        Button5.Text = "Fan"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button7
        ' 
        Button7.BackColor = Color.LightCyan
        Button7.ForeColor = Color.Black
        Button7.Location = New Point(6, 51)
        Button7.Name = "Button7"
        Button7.Size = New Size(75, 23)
        Button7.TabIndex = 7
        Button7.Text = "-2°"
        Button7.UseVisualStyleBackColor = False
        ' 
        ' Button8
        ' 
        Button8.BackColor = Color.MistyRose
        Button8.ForeColor = Color.Black
        Button8.Location = New Point(6, 22)
        Button8.Name = "Button8"
        Button8.Size = New Size(75, 23)
        Button8.TabIndex = 6
        Button8.Text = "+2°"
        Button8.UseVisualStyleBackColor = False
        ' 
        ' GroupBox9
        ' 
        GroupBox9.AutoSize = True
        GroupBox9.Controls.Add(Button9)
        GroupBox9.Controls.Add(Button16)
        GroupBox9.Controls.Add(Button15)
        GroupBox9.Controls.Add(Button14)
        GroupBox9.Controls.Add(Button11)
        GroupBox9.Controls.Add(Button10)
        GroupBox9.Location = New Point(155, 166)
        GroupBox9.Name = "GroupBox9"
        GroupBox9.Size = New Size(180, 125)
        GroupBox9.TabIndex = 5
        GroupBox9.TabStop = False
        GroupBox9.Text = "Away"
        ' 
        ' Button9
        ' 
        Button9.Location = New Point(6, 80)
        Button9.Name = "Button9"
        Button9.Size = New Size(75, 23)
        Button9.TabIndex = 17
        Button9.Text = "for 6 hours"
        Button9.UseVisualStyleBackColor = True
        ' 
        ' Button16
        ' 
        Button16.Location = New Point(6, 51)
        Button16.Name = "Button16"
        Button16.Size = New Size(75, 23)
        Button16.TabIndex = 16
        Button16.Text = "for 4 hours"
        Button16.UseVisualStyleBackColor = True
        ' 
        ' Button15
        ' 
        Button15.Location = New Point(99, 80)
        Button15.Name = "Button15"
        Button15.Size = New Size(75, 23)
        Button15.TabIndex = 15
        Button15.Text = "until 9 pm"
        Button15.UseVisualStyleBackColor = True
        ' 
        ' Button14
        ' 
        Button14.Location = New Point(99, 51)
        Button14.Name = "Button14"
        Button14.Size = New Size(75, 23)
        Button14.TabIndex = 14
        Button14.Text = "until 4 pm"
        Button14.UseVisualStyleBackColor = True
        ' 
        ' Button11
        ' 
        Button11.Location = New Point(99, 22)
        Button11.Name = "Button11"
        Button11.Size = New Size(75, 23)
        Button11.TabIndex = 13
        Button11.Text = "until 7 am"
        Button11.UseVisualStyleBackColor = True
        ' 
        ' Button10
        ' 
        Button10.Location = New Point(6, 22)
        Button10.Name = "Button10"
        Button10.Size = New Size(75, 23)
        Button10.TabIndex = 12
        Button10.Text = "for 2 hours"
        Button10.UseVisualStyleBackColor = True
        ' 
        ' TabControl1
        ' 
        TabControl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage5)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Location = New Point(12, 101)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(498, 337)
        TabControl1.TabIndex = 0
        ' 
        ' ThermostatForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(522, 450)
        Controls.Add(GroupBox10)
        Controls.Add(TabControl1)
        Controls.Add(GroupBox1)
        Name = "ThermostatForm"
        Text = "Thermostat"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox10.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        TabPage3.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        TabPage5.ResumeLayout(False)
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        GroupBox7.ResumeLayout(False)
        GroupBox7.PerformLayout()
        GroupBox8.ResumeLayout(False)
        GroupBox8.PerformLayout()
        GroupBox6.ResumeLayout(False)
        GroupBox6.PerformLayout()
        GroupBox5.ResumeLayout(False)
        GroupBox4.ResumeLayout(False)
        GroupBox9.ResumeLayout(False)
        TabControl1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ThermostatName As Label
    Friend WithEvents Version As Label
    Friend WithEvents Brand As Label
    Friend WithEvents GroupBox10 As GroupBox
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents SendAlertButton As Button
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents SensorColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents SensorColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents SensorColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents SensorColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents OutdoorHumidity As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents OutdoorCondition As Label
    Friend WithEvents OutdoorTemp As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents IndoorHumidity As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents IndoorTemp As Label
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents HoldEndLabel As Label
    Friend WithEvents HoldStartLabel As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents CancelHoldButton As Button
    Friend WithEvents HoldFan As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents HoldHeat As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents HoldCool As Label
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents FanState As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents HeatAt As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents CoolAt As Label
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents ProgramName As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ProgramHeat As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents ProgramCool As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents Button6 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Button12 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TypeColumn As DataGridViewTextBoxColumn
    Friend WithEvents HeatColumn As DataGridViewTextBoxColumn
    Friend WithEvents CoolColumn As DataGridViewTextBoxColumn
    Friend WithEvents FanColumn As DataGridViewTextBoxColumn
    Friend WithEvents StartTimeColumn As DataGridViewTextBoxColumn
    Friend WithEvents EndTimeColumn As DataGridViewTextBoxColumn
    Friend WithEvents Button15 As Button
    Friend WithEvents Button14 As Button
    Friend WithEvents Button11 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents Button16 As Button
    Friend WithEvents Button9 As Button
End Class
