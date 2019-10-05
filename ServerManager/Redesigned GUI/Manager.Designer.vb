<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Manager
    Inherits MetroFramework.Forms.MetroForm

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ColumnHeader1 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader2 As System.Windows.Forms.ColumnHeader
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Manager))
        Me.StyleManager = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.CPUPerformanceCounter = New System.Diagnostics.PerformanceCounter()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.OverviewPanel = New MetroFramework.Controls.MetroPanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.MetroPanel1 = New MetroFramework.Controls.MetroPanel()
        Me.CPUCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.MetroPanel5 = New MetroFramework.Controls.MetroPanel()
        Me.RAMCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.MetroPanel4 = New MetroFramework.Controls.MetroPanel()
        Me.VRAMCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.MetroPanel3 = New MetroFramework.Controls.MetroPanel()
        Me.NetworkCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.LoadingProgressPanel = New MetroFramework.Controls.MetroPanel()
        Me.LoadingProgressView = New System.Windows.Forms.ListView()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.ServerPanel = New MetroFramework.Controls.MetroPanel()
        Me.ServerListLayout = New System.Windows.Forms.FlowLayoutPanel()
        Me.JoinServerButton = New System.Windows.Forms.Button()
        Me.AddServerButton = New System.Windows.Forms.Button()
        Me.SearchBox = New MetroFramework.Controls.MetroTextBox()
        Me.ModpackServerPanel = New MetroFramework.Controls.MetroPanel()
        Me.ControlPanel = New MetroFramework.Controls.MetroPanel()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CType(Me.StyleManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CPUPerformanceCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.OverviewPanel.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.MetroPanel1.SuspendLayout()
        Me.MetroPanel5.SuspendLayout()
        Me.MetroPanel4.SuspendLayout()
        Me.MetroPanel3.SuspendLayout()
        Me.LoadingProgressPanel.SuspendLayout()
        Me.ServerPanel.SuspendLayout()
        Me.ControlPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = "軟體名稱"
        ColumnHeader1.Width = 106
        '
        'ColumnHeader2
        '
        ColumnHeader2.Text = "載入進度"
        ColumnHeader2.Width = 101
        '
        'StyleManager
        '
        Me.StyleManager.Owner = Me
        Me.StyleManager.Style = MetroFramework.MetroColorStyle.Green
        '
        'CPUPerformanceCounter
        '
        Me.CPUPerformanceCounter.CategoryName = "Processor"
        Me.CPUPerformanceCounter.CounterName = "% Processor Time"
        Me.CPUPerformanceCounter.InstanceName = "_Total"
        '
        'Timer
        '
        Me.Timer.Enabled = True
        Me.Timer.Interval = 450
        '
        'OverviewPanel
        '
        Me.OverviewPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OverviewPanel.Controls.Add(Me.FlowLayoutPanel1)
        Me.OverviewPanel.Controls.Add(Me.LoadingProgressPanel)
        Me.OverviewPanel.HorizontalScrollbarBarColor = True
        Me.OverviewPanel.HorizontalScrollbarHighlightOnWheel = False
        Me.OverviewPanel.HorizontalScrollbarSize = 10
        Me.OverviewPanel.Location = New System.Drawing.Point(42, 25)
        Me.OverviewPanel.Name = "OverviewPanel"
        Me.OverviewPanel.Size = New System.Drawing.Size(731, 422)
        Me.OverviewPanel.TabIndex = 12
        Me.OverviewPanel.VerticalScrollbarBarColor = True
        Me.OverviewPanel.VerticalScrollbarHighlightOnWheel = False
        Me.OverviewPanel.VerticalScrollbarSize = 10
        Me.OverviewPanel.Visible = False
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoSize = True
        Me.FlowLayoutPanel1.Controls.Add(Me.MetroPanel1)
        Me.FlowLayoutPanel1.Controls.Add(Me.MetroPanel5)
        Me.FlowLayoutPanel1.Controls.Add(Me.MetroPanel4)
        Me.FlowLayoutPanel1.Controls.Add(Me.MetroPanel3)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(404, 422)
        Me.FlowLayoutPanel1.TabIndex = 12
        '
        'MetroPanel1
        '
        Me.MetroPanel1.Controls.Add(Me.CPUCircularBar)
        Me.MetroPanel1.HorizontalScrollbarBarColor = True
        Me.MetroPanel1.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel1.HorizontalScrollbarSize = 10
        Me.MetroPanel1.Location = New System.Drawing.Point(3, 3)
        Me.MetroPanel1.Name = "MetroPanel1"
        Me.MetroPanel1.Size = New System.Drawing.Size(155, 150)
        Me.MetroPanel1.TabIndex = 9
        Me.MetroPanel1.VerticalScrollbarBarColor = True
        Me.MetroPanel1.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel1.VerticalScrollbarSize = 10
        '
        'CPUCircularBar
        '
        Me.CPUCircularBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.CPUCircularBar.AnimationSpeed = 500
        Me.CPUCircularBar.BackColor = System.Drawing.Color.Transparent
        Me.CPUCircularBar.Dock = System.Windows.Forms.DockStyle.Left
        Me.CPUCircularBar.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.CPUCircularBar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CPUCircularBar.InnerColor = System.Drawing.Color.WhiteSmoke
        Me.CPUCircularBar.InnerMargin = 2
        Me.CPUCircularBar.InnerWidth = -1
        Me.CPUCircularBar.Location = New System.Drawing.Point(0, 0)
        Me.CPUCircularBar.MarqueeAnimationSpeed = 2000
        Me.CPUCircularBar.Name = "CPUCircularBar"
        Me.CPUCircularBar.OuterColor = System.Drawing.Color.LightGray
        Me.CPUCircularBar.OuterMargin = -25
        Me.CPUCircularBar.OuterWidth = 26
        Me.CPUCircularBar.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(29, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.CPUCircularBar.ProgressWidth = 10
        Me.CPUCircularBar.SecondaryFont = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.CPUCircularBar.Size = New System.Drawing.Size(150, 150)
        Me.CPUCircularBar.StartAngle = 270
        Me.CPUCircularBar.SubscriptColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.CPUCircularBar.SubscriptMargin = New System.Windows.Forms.Padding(-22, 10, 0, 0)
        Me.CPUCircularBar.SubscriptText = "CPU"
        Me.CPUCircularBar.SuperscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.CPUCircularBar.SuperscriptMargin = New System.Windows.Forms.Padding(0, 35, 0, 0)
        Me.CPUCircularBar.SuperscriptText = "%"
        Me.CPUCircularBar.TabIndex = 5
        Me.CPUCircularBar.Text = "25"
        Me.CPUCircularBar.TextMargin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.CPUCircularBar.Value = 25
        '
        'MetroPanel5
        '
        Me.MetroPanel5.Controls.Add(Me.RAMCircularBar)
        Me.MetroPanel5.HorizontalScrollbarBarColor = True
        Me.MetroPanel5.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel5.HorizontalScrollbarSize = 10
        Me.MetroPanel5.Location = New System.Drawing.Point(3, 159)
        Me.MetroPanel5.Name = "MetroPanel5"
        Me.MetroPanel5.Size = New System.Drawing.Size(155, 150)
        Me.MetroPanel5.TabIndex = 12
        Me.MetroPanel5.VerticalScrollbarBarColor = True
        Me.MetroPanel5.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel5.VerticalScrollbarSize = 10
        '
        'RAMCircularBar
        '
        Me.RAMCircularBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.RAMCircularBar.AnimationSpeed = 500
        Me.RAMCircularBar.BackColor = System.Drawing.Color.Transparent
        Me.RAMCircularBar.Dock = System.Windows.Forms.DockStyle.Left
        Me.RAMCircularBar.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.RAMCircularBar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.RAMCircularBar.InnerColor = System.Drawing.Color.WhiteSmoke
        Me.RAMCircularBar.InnerMargin = 2
        Me.RAMCircularBar.InnerWidth = -1
        Me.RAMCircularBar.Location = New System.Drawing.Point(0, 0)
        Me.RAMCircularBar.MarqueeAnimationSpeed = 2000
        Me.RAMCircularBar.Name = "RAMCircularBar"
        Me.RAMCircularBar.OuterColor = System.Drawing.Color.LightGray
        Me.RAMCircularBar.OuterMargin = -25
        Me.RAMCircularBar.OuterWidth = 26
        Me.RAMCircularBar.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.RAMCircularBar.ProgressWidth = 10
        Me.RAMCircularBar.SecondaryFont = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.RAMCircularBar.Size = New System.Drawing.Size(150, 150)
        Me.RAMCircularBar.StartAngle = 270
        Me.RAMCircularBar.SubscriptColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.RAMCircularBar.SubscriptMargin = New System.Windows.Forms.Padding(-20, 10, 0, 0)
        Me.RAMCircularBar.SubscriptText = "實體記憶體"
        Me.RAMCircularBar.SuperscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.RAMCircularBar.SuperscriptMargin = New System.Windows.Forms.Padding(0, 35, 0, 0)
        Me.RAMCircularBar.SuperscriptText = "%"
        Me.RAMCircularBar.TabIndex = 11
        Me.RAMCircularBar.Text = "25"
        Me.RAMCircularBar.TextMargin = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.RAMCircularBar.Value = 25
        '
        'MetroPanel4
        '
        Me.MetroPanel4.Controls.Add(Me.VRAMCircularBar)
        Me.MetroPanel4.HorizontalScrollbarBarColor = True
        Me.MetroPanel4.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel4.HorizontalScrollbarSize = 10
        Me.MetroPanel4.Location = New System.Drawing.Point(164, 3)
        Me.MetroPanel4.Name = "MetroPanel4"
        Me.MetroPanel4.Size = New System.Drawing.Size(155, 150)
        Me.MetroPanel4.TabIndex = 11
        Me.MetroPanel4.VerticalScrollbarBarColor = True
        Me.MetroPanel4.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel4.VerticalScrollbarSize = 10
        '
        'VRAMCircularBar
        '
        Me.VRAMCircularBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.VRAMCircularBar.AnimationSpeed = 500
        Me.VRAMCircularBar.BackColor = System.Drawing.Color.Transparent
        Me.VRAMCircularBar.Dock = System.Windows.Forms.DockStyle.Left
        Me.VRAMCircularBar.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.VRAMCircularBar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.VRAMCircularBar.InnerColor = System.Drawing.Color.WhiteSmoke
        Me.VRAMCircularBar.InnerMargin = 2
        Me.VRAMCircularBar.InnerWidth = -1
        Me.VRAMCircularBar.Location = New System.Drawing.Point(0, 0)
        Me.VRAMCircularBar.MarqueeAnimationSpeed = 2000
        Me.VRAMCircularBar.Name = "VRAMCircularBar"
        Me.VRAMCircularBar.OuterColor = System.Drawing.Color.LightGray
        Me.VRAMCircularBar.OuterMargin = -25
        Me.VRAMCircularBar.OuterWidth = 26
        Me.VRAMCircularBar.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(196, Byte), Integer), CType(CType(13, Byte), Integer))
        Me.VRAMCircularBar.ProgressWidth = 10
        Me.VRAMCircularBar.SecondaryFont = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.VRAMCircularBar.Size = New System.Drawing.Size(150, 150)
        Me.VRAMCircularBar.StartAngle = 270
        Me.VRAMCircularBar.SubscriptColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.VRAMCircularBar.SubscriptMargin = New System.Windows.Forms.Padding(-20, 10, 0, 0)
        Me.VRAMCircularBar.SubscriptText = "虛擬記憶體"
        Me.VRAMCircularBar.SuperscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.VRAMCircularBar.SuperscriptMargin = New System.Windows.Forms.Padding(0, 35, 0, 0)
        Me.VRAMCircularBar.SuperscriptText = "%"
        Me.VRAMCircularBar.TabIndex = 8
        Me.VRAMCircularBar.Text = "25"
        Me.VRAMCircularBar.TextMargin = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.VRAMCircularBar.Value = 25
        '
        'MetroPanel3
        '
        Me.MetroPanel3.Controls.Add(Me.NetworkCircularBar)
        Me.MetroPanel3.HorizontalScrollbarBarColor = True
        Me.MetroPanel3.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel3.HorizontalScrollbarSize = 10
        Me.MetroPanel3.Location = New System.Drawing.Point(164, 159)
        Me.MetroPanel3.Name = "MetroPanel3"
        Me.MetroPanel3.Size = New System.Drawing.Size(155, 150)
        Me.MetroPanel3.TabIndex = 10
        Me.MetroPanel3.VerticalScrollbarBarColor = True
        Me.MetroPanel3.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel3.VerticalScrollbarSize = 10
        '
        'NetworkCircularBar
        '
        Me.NetworkCircularBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.NetworkCircularBar.AnimationSpeed = 500
        Me.NetworkCircularBar.BackColor = System.Drawing.Color.Transparent
        Me.NetworkCircularBar.Dock = System.Windows.Forms.DockStyle.Left
        Me.NetworkCircularBar.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.NetworkCircularBar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.NetworkCircularBar.InnerColor = System.Drawing.Color.White
        Me.NetworkCircularBar.InnerMargin = 2
        Me.NetworkCircularBar.InnerWidth = -1
        Me.NetworkCircularBar.Location = New System.Drawing.Point(0, 0)
        Me.NetworkCircularBar.MarqueeAnimationSpeed = 2000
        Me.NetworkCircularBar.Name = "NetworkCircularBar"
        Me.NetworkCircularBar.OuterColor = System.Drawing.Color.LightGray
        Me.NetworkCircularBar.OuterMargin = -25
        Me.NetworkCircularBar.OuterWidth = 26
        Me.NetworkCircularBar.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.NetworkCircularBar.ProgressWidth = 10
        Me.NetworkCircularBar.SecondaryFont = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.NetworkCircularBar.Size = New System.Drawing.Size(150, 150)
        Me.NetworkCircularBar.StartAngle = 270
        Me.NetworkCircularBar.SubscriptColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.NetworkCircularBar.SubscriptMargin = New System.Windows.Forms.Padding(-22, 10, 0, 0)
        Me.NetworkCircularBar.SubscriptText = "網路"
        Me.NetworkCircularBar.SuperscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.NetworkCircularBar.SuperscriptMargin = New System.Windows.Forms.Padding(0, 35, 0, 0)
        Me.NetworkCircularBar.SuperscriptText = "%"
        Me.NetworkCircularBar.TabIndex = 6
        Me.NetworkCircularBar.Text = "25"
        Me.NetworkCircularBar.TextMargin = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.NetworkCircularBar.Value = 25
        '
        'LoadingProgressPanel
        '
        Me.LoadingProgressPanel.Controls.Add(Me.LoadingProgressView)
        Me.LoadingProgressPanel.Controls.Add(Me.MetroLabel1)
        Me.LoadingProgressPanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.LoadingProgressPanel.HorizontalScrollbarBarColor = True
        Me.LoadingProgressPanel.HorizontalScrollbarHighlightOnWheel = False
        Me.LoadingProgressPanel.HorizontalScrollbarSize = 10
        Me.LoadingProgressPanel.Location = New System.Drawing.Point(404, 0)
        Me.LoadingProgressPanel.Name = "LoadingProgressPanel"
        Me.LoadingProgressPanel.Padding = New System.Windows.Forms.Padding(6, 0, 3, 3)
        Me.LoadingProgressPanel.Size = New System.Drawing.Size(327, 422)
        Me.LoadingProgressPanel.TabIndex = 13
        Me.LoadingProgressPanel.VerticalScrollbarBarColor = True
        Me.LoadingProgressPanel.VerticalScrollbarHighlightOnWheel = False
        Me.LoadingProgressPanel.VerticalScrollbarSize = 10
        '
        'LoadingProgressView
        '
        Me.LoadingProgressView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LoadingProgressView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1, ColumnHeader2})
        Me.LoadingProgressView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoadingProgressView.Font = New System.Drawing.Font("微軟正黑體", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.LoadingProgressView.FullRowSelect = True
        Me.LoadingProgressView.Location = New System.Drawing.Point(6, 19)
        Me.LoadingProgressView.Name = "LoadingProgressView"
        Me.LoadingProgressView.Size = New System.Drawing.Size(318, 400)
        Me.LoadingProgressView.TabIndex = 3
        Me.LoadingProgressView.UseCompatibleStateImageBehavior = False
        Me.LoadingProgressView.View = System.Windows.Forms.View.Details
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.MetroLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.MetroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.MetroLabel1.Location = New System.Drawing.Point(6, 0)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(121, 19)
        Me.MetroLabel1.TabIndex = 2
        Me.MetroLabel1.Text = "版本列表擷取進度"
        '
        'ServerPanel
        '
        Me.ServerPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerPanel.Controls.Add(Me.ServerListLayout)
        Me.ServerPanel.Controls.Add(Me.JoinServerButton)
        Me.ServerPanel.Controls.Add(Me.AddServerButton)
        Me.ServerPanel.Controls.Add(Me.SearchBox)
        Me.ServerPanel.HorizontalScrollbarBarColor = True
        Me.ServerPanel.HorizontalScrollbarHighlightOnWheel = False
        Me.ServerPanel.HorizontalScrollbarSize = 10
        Me.ServerPanel.Location = New System.Drawing.Point(42, 25)
        Me.ServerPanel.Name = "ServerPanel"
        Me.ServerPanel.Size = New System.Drawing.Size(731, 422)
        Me.ServerPanel.TabIndex = 12
        Me.ServerPanel.VerticalScrollbarBarColor = True
        Me.ServerPanel.VerticalScrollbarHighlightOnWheel = False
        Me.ServerPanel.VerticalScrollbarSize = 10
        Me.ServerPanel.Visible = False
        '
        'ServerListLayout
        '
        Me.ServerListLayout.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerListLayout.AutoScroll = True
        Me.ServerListLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.ServerListLayout.Location = New System.Drawing.Point(6, 40)
        Me.ServerListLayout.Name = "ServerListLayout"
        Me.ServerListLayout.Size = New System.Drawing.Size(719, 378)
        Me.ServerListLayout.TabIndex = 3
        Me.ServerListLayout.WrapContents = False
        '
        'JoinServerButton
        '
        Me.JoinServerButton.BackgroundImage = CType(resources.GetObject("JoinServerButton.BackgroundImage"), System.Drawing.Image)
        Me.JoinServerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.JoinServerButton.FlatAppearance.BorderSize = 0
        Me.JoinServerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.JoinServerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.JoinServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.JoinServerButton.Location = New System.Drawing.Point(702, 6)
        Me.JoinServerButton.Name = "JoinServerButton"
        Me.JoinServerButton.Size = New System.Drawing.Size(23, 23)
        Me.JoinServerButton.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.JoinServerButton, "加入伺服器")
        Me.JoinServerButton.UseVisualStyleBackColor = True
        '
        'AddServerButton
        '
        Me.AddServerButton.BackgroundImage = Global.ServerManager.My.Resources.Resources.add
        Me.AddServerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.AddServerButton.FlatAppearance.BorderSize = 0
        Me.AddServerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.AddServerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.AddServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddServerButton.Location = New System.Drawing.Point(673, 6)
        Me.AddServerButton.Name = "AddServerButton"
        Me.AddServerButton.Size = New System.Drawing.Size(23, 23)
        Me.AddServerButton.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.AddServerButton, "新增伺服器")
        Me.AddServerButton.UseVisualStyleBackColor = True
        '
        'SearchBox
        '
        Me.SearchBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.SearchBox.CustomButton.Image = Nothing
        Me.SearchBox.CustomButton.Location = New System.Drawing.Point(639, 1)
        Me.SearchBox.CustomButton.Name = ""
        Me.SearchBox.CustomButton.Size = New System.Drawing.Size(21, 21)
        Me.SearchBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.SearchBox.CustomButton.TabIndex = 1
        Me.SearchBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.SearchBox.CustomButton.UseSelectable = True
        Me.SearchBox.CustomButton.Visible = False
        Me.SearchBox.Lines = New String(-1) {}
        Me.SearchBox.Location = New System.Drawing.Point(6, 6)
        Me.SearchBox.MaxLength = 32767
        Me.SearchBox.Name = "SearchBox"
        Me.SearchBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.SearchBox.PromptText = "輸入伺服器的資料夾名稱"
        Me.SearchBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.SearchBox.SelectedText = ""
        Me.SearchBox.SelectionLength = 0
        Me.SearchBox.SelectionStart = 0
        Me.SearchBox.ShortcutsEnabled = True
        Me.SearchBox.Size = New System.Drawing.Size(661, 23)
        Me.SearchBox.TabIndex = 2
        Me.SearchBox.UseSelectable = True
        Me.SearchBox.WaterMark = "輸入伺服器的資料夾名稱"
        Me.SearchBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.SearchBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'ModpackServerPanel
        '
        Me.ModpackServerPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ModpackServerPanel.HorizontalScrollbarBarColor = True
        Me.ModpackServerPanel.HorizontalScrollbarHighlightOnWheel = False
        Me.ModpackServerPanel.HorizontalScrollbarSize = 10
        Me.ModpackServerPanel.Location = New System.Drawing.Point(42, 25)
        Me.ModpackServerPanel.Name = "ModpackServerPanel"
        Me.ModpackServerPanel.Size = New System.Drawing.Size(731, 422)
        Me.ModpackServerPanel.TabIndex = 12
        Me.ModpackServerPanel.VerticalScrollbarBarColor = True
        Me.ModpackServerPanel.VerticalScrollbarHighlightOnWheel = False
        Me.ModpackServerPanel.VerticalScrollbarSize = 10
        Me.ModpackServerPanel.Visible = False
        '
        'ControlPanel
        '
        Me.ControlPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ControlPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ControlPanel.Controls.Add(Me.RadioButton3)
        Me.ControlPanel.Controls.Add(Me.RadioButton2)
        Me.ControlPanel.Controls.Add(Me.RadioButton1)
        Me.ControlPanel.Controls.Add(Me.Panel1)
        Me.ControlPanel.HorizontalScrollbarBarColor = True
        Me.ControlPanel.HorizontalScrollbarHighlightOnWheel = False
        Me.ControlPanel.HorizontalScrollbarSize = 10
        Me.ControlPanel.Location = New System.Drawing.Point(0, 3)
        Me.ControlPanel.Name = "ControlPanel"
        Me.ControlPanel.Size = New System.Drawing.Size(40, 448)
        Me.ControlPanel.TabIndex = 13
        Me.ControlPanel.UseCustomBackColor = True
        Me.ControlPanel.VerticalScrollbarBarColor = True
        Me.ControlPanel.VerticalScrollbarHighlightOnWheel = False
        Me.ControlPanel.VerticalScrollbarSize = 10
        '
        'RadioButton3
        '
        Me.RadioButton3.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButton3.BackgroundImage = CType(resources.GetObject("RadioButton3.BackgroundImage"), System.Drawing.Image)
        Me.RadioButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.RadioButton3.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadioButton3.FlatAppearance.BorderSize = 0
        Me.RadioButton3.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(89, Byte), Integer))
        Me.RadioButton3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.RadioButton3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.RadioButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButton3.Location = New System.Drawing.Point(0, 120)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(40, 40)
        Me.RadioButton3.TabIndex = 6
        Me.RadioButton3.Tag = "模組包伺服器列表"
        Me.RadioButton3.UseVisualStyleBackColor = False
        '
        'RadioButton2
        '
        Me.RadioButton2.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButton2.BackgroundImage = CType(resources.GetObject("RadioButton2.BackgroundImage"), System.Drawing.Image)
        Me.RadioButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.RadioButton2.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadioButton2.FlatAppearance.BorderSize = 0
        Me.RadioButton2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(89, Byte), Integer))
        Me.RadioButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.RadioButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.RadioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButton2.Location = New System.Drawing.Point(0, 80)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(40, 40)
        Me.RadioButton2.TabIndex = 3
        Me.RadioButton2.Tag = "伺服器列表"
        Me.RadioButton2.UseVisualStyleBackColor = False
        '
        'RadioButton1
        '
        Me.RadioButton1.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButton1.BackgroundImage = Global.ServerManager.My.Resources.Resources.home
        Me.RadioButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadioButton1.FlatAppearance.BorderSize = 0
        Me.RadioButton1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(89, Byte), Integer))
        Me.RadioButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.RadioButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.RadioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButton1.Location = New System.Drawing.Point(0, 40)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(40, 40)
        Me.RadioButton1.TabIndex = 2
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Tag = "主頁"
        Me.RadioButton1.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(89, Byte), Integer))
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel1.Size = New System.Drawing.Size(40, 40)
        Me.Panel1.TabIndex = 5
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.ServerManager.My.Resources.Resources.IconTemplate
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(2, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(36, 36)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Tag = "Minecraft 伺服器管理員"
        '
        'ToolTip1
        '
        Me.ToolTip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.ToolTip1.ForeColor = System.Drawing.Color.White
        Me.ToolTip1.OwnerDraw = True
        '
        'Manager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ControlPanel)
        Me.Controls.Add(Me.ServerPanel)
        Me.Controls.Add(Me.ModpackServerPanel)
        Me.Controls.Add(Me.OverviewPanel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Manager"
        Me.Padding = New System.Windows.Forms.Padding(0, 60, 20, 0)
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow
        Me.Style = MetroFramework.MetroColorStyle.Green
        Me.Text = "Minecraft 伺服器管理員"
        CType(Me.StyleManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CPUPerformanceCounter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.OverviewPanel.ResumeLayout(False)
        Me.OverviewPanel.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.MetroPanel1.ResumeLayout(False)
        Me.MetroPanel5.ResumeLayout(False)
        Me.MetroPanel4.ResumeLayout(False)
        Me.MetroPanel3.ResumeLayout(False)
        Me.LoadingProgressPanel.ResumeLayout(False)
        Me.LoadingProgressPanel.PerformLayout()
        Me.ServerPanel.ResumeLayout(False)
        Me.ControlPanel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StyleManager As MetroFramework.Components.MetroStyleManager
    Friend WithEvents CPUPerformanceCounter As PerformanceCounter
    Friend WithEvents Timer As Timer
    Friend WithEvents OverviewPanel As MetroFramework.Controls.MetroPanel
    Friend WithEvents ServerPanel As MetroFramework.Controls.MetroPanel
    Friend WithEvents ModpackServerPanel As MetroFramework.Controls.MetroPanel
    Friend WithEvents LoadingProgressPanel As MetroFramework.Controls.MetroPanel
    Friend WithEvents LoadingProgressView As System.Windows.Forms.ListView
    Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents CPUCircularBar As CircularProgressBar.CircularProgressBar
    Friend WithEvents NetworkCircularBar As CircularProgressBar.CircularProgressBar
    Friend WithEvents VRAMCircularBar As CircularProgressBar.CircularProgressBar
    Friend WithEvents MetroPanel1 As MetroFramework.Controls.MetroPanel
    Friend WithEvents MetroPanel4 As MetroFramework.Controls.MetroPanel
    Friend WithEvents MetroPanel5 As MetroFramework.Controls.MetroPanel
    Friend WithEvents RAMCircularBar As CircularProgressBar.CircularProgressBar
    Friend WithEvents MetroPanel3 As MetroFramework.Controls.MetroPanel
    Friend WithEvents ControlPanel As MetroFramework.Controls.MetroPanel
    Private WithEvents RadioButton1 As RadioButton
    Private WithEvents RadioButton2 As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Private WithEvents RadioButton3 As RadioButton
    Friend WithEvents ServerListLayout As FlowLayoutPanel
    Friend WithEvents SearchBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents AddServerButton As Button
    Friend WithEvents JoinServerButton As Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
