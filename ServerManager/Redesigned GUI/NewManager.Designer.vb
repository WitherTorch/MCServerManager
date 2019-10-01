<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NewManager
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
        Me.StyleManager = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MetroTile2 = New MetroFramework.Controls.MetroTile()
        Me.MetroTile1 = New MetroFramework.Controls.MetroTile()
        Me.CPUPerformanceCounter = New System.Diagnostics.PerformanceCounter()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.BorderPanel = New MetroFramework.Controls.MetroPanel()
        Me.MetroPanel2 = New MetroFramework.Controls.MetroPanel()
        Me.LoadingProgressPanel = New MetroFramework.Controls.MetroPanel()
        Me.LoadingProgressView = New MetroFramework.Controls.MetroListView()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.CPUCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.NetworkCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.VRAMCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.MetroPanel1 = New MetroFramework.Controls.MetroPanel()
        Me.MetroPanel3 = New MetroFramework.Controls.MetroPanel()
        Me.MetroPanel4 = New MetroFramework.Controls.MetroPanel()
        Me.MetroPanel5 = New MetroFramework.Controls.MetroPanel()
        Me.RAMCircularBar = New CircularProgressBar.CircularProgressBar()
        CType(Me.StyleManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.CPUPerformanceCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MetroPanel2.SuspendLayout()
        Me.LoadingProgressPanel.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.MetroPanel1.SuspendLayout()
        Me.MetroPanel3.SuspendLayout()
        Me.MetroPanel4.SuspendLayout()
        Me.MetroPanel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'StyleManager
        '
        Me.StyleManager.Owner = Me
        Me.StyleManager.Style = MetroFramework.MetroColorStyle.Green
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MetroTile2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.MetroTile1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(20, 30)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(150, 400)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'MetroTile2
        '
        Me.MetroTile2.ActiveControl = Nothing
        Me.MetroTile2.Dock = System.Windows.Forms.DockStyle.Top
        Me.MetroTile2.Location = New System.Drawing.Point(3, 78)
        Me.MetroTile2.Name = "MetroTile2"
        Me.MetroTile2.Size = New System.Drawing.Size(144, 69)
        Me.MetroTile2.TabIndex = 5
        Me.MetroTile2.Text = "伺服器"
        Me.MetroTile2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.MetroTile2.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall
        Me.MetroTile2.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular
        Me.MetroTile2.UseSelectable = True
        Me.MetroTile2.UseStyleColors = True
        '
        'MetroTile1
        '
        Me.MetroTile1.ActiveControl = Nothing
        Me.MetroTile1.Dock = System.Windows.Forms.DockStyle.Top
        Me.MetroTile1.Location = New System.Drawing.Point(3, 3)
        Me.MetroTile1.Name = "MetroTile1"
        Me.MetroTile1.Size = New System.Drawing.Size(144, 69)
        Me.MetroTile1.TabIndex = 4
        Me.MetroTile1.Text = "概觀"
        Me.MetroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.MetroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall
        Me.MetroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular
        Me.MetroTile1.UseSelectable = True
        Me.MetroTile1.UseStyleColors = True
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
        'BorderPanel
        '
        Me.BorderPanel.BackgroundImage = Global.ServerManager.My.Resources.Resources.Border
        Me.BorderPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BorderPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.BorderPanel.HorizontalScrollbarBarColor = True
        Me.BorderPanel.HorizontalScrollbarHighlightOnWheel = False
        Me.BorderPanel.HorizontalScrollbarSize = 10
        Me.BorderPanel.Location = New System.Drawing.Point(170, 30)
        Me.BorderPanel.Name = "BorderPanel"
        Me.BorderPanel.Size = New System.Drawing.Size(8, 400)
        Me.BorderPanel.TabIndex = 4
        Me.BorderPanel.VerticalScrollbarBarColor = True
        Me.BorderPanel.VerticalScrollbarHighlightOnWheel = False
        Me.BorderPanel.VerticalScrollbarSize = 10
        '
        'MetroPanel2
        '
        Me.MetroPanel2.Controls.Add(Me.FlowLayoutPanel1)
        Me.MetroPanel2.Controls.Add(Me.LoadingProgressPanel)
        Me.MetroPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetroPanel2.HorizontalScrollbarBarColor = True
        Me.MetroPanel2.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel2.HorizontalScrollbarSize = 10
        Me.MetroPanel2.Location = New System.Drawing.Point(178, 30)
        Me.MetroPanel2.Name = "MetroPanel2"
        Me.MetroPanel2.Size = New System.Drawing.Size(602, 400)
        Me.MetroPanel2.TabIndex = 12
        Me.MetroPanel2.VerticalScrollbarBarColor = True
        Me.MetroPanel2.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel2.VerticalScrollbarSize = 10
        '
        'LoadingProgressPanel
        '
        Me.LoadingProgressPanel.Controls.Add(Me.LoadingProgressView)
        Me.LoadingProgressPanel.Controls.Add(Me.MetroLabel1)
        Me.LoadingProgressPanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.LoadingProgressPanel.HorizontalScrollbarBarColor = True
        Me.LoadingProgressPanel.HorizontalScrollbarHighlightOnWheel = False
        Me.LoadingProgressPanel.HorizontalScrollbarSize = 10
        Me.LoadingProgressPanel.Location = New System.Drawing.Point(275, 0)
        Me.LoadingProgressPanel.Name = "LoadingProgressPanel"
        Me.LoadingProgressPanel.Padding = New System.Windows.Forms.Padding(6, 0, 3, 3)
        Me.LoadingProgressPanel.Size = New System.Drawing.Size(327, 400)
        Me.LoadingProgressPanel.TabIndex = 13
        Me.LoadingProgressPanel.VerticalScrollbarBarColor = True
        Me.LoadingProgressPanel.VerticalScrollbarHighlightOnWheel = False
        Me.LoadingProgressPanel.VerticalScrollbarSize = 10
        '
        'LoadingProgressView
        '
        Me.LoadingProgressView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LoadingProgressView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LoadingProgressView.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LoadingProgressView.FullRowSelect = True
        Me.LoadingProgressView.Location = New System.Drawing.Point(6, 19)
        Me.LoadingProgressView.Name = "LoadingProgressView"
        Me.LoadingProgressView.OwnerDraw = True
        Me.LoadingProgressView.Size = New System.Drawing.Size(318, 378)
        Me.LoadingProgressView.TabIndex = 3
        Me.LoadingProgressView.UseCompatibleStateImageBehavior = False
        Me.LoadingProgressView.UseSelectable = True
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
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.MetroPanel1)
        Me.FlowLayoutPanel1.Controls.Add(Me.MetroPanel5)
        Me.FlowLayoutPanel1.Controls.Add(Me.MetroPanel4)
        Me.FlowLayoutPanel1.Controls.Add(Me.MetroPanel3)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(275, 400)
        Me.FlowLayoutPanel1.TabIndex = 12
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
        Me.CPUCircularBar.Size = New System.Drawing.Size(125, 125)
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
        Me.NetworkCircularBar.Size = New System.Drawing.Size(125, 125)
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
        Me.VRAMCircularBar.Size = New System.Drawing.Size(125, 125)
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
        'MetroPanel1
        '
        Me.MetroPanel1.Controls.Add(Me.CPUCircularBar)
        Me.MetroPanel1.HorizontalScrollbarBarColor = True
        Me.MetroPanel1.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel1.HorizontalScrollbarSize = 10
        Me.MetroPanel1.Location = New System.Drawing.Point(3, 3)
        Me.MetroPanel1.Name = "MetroPanel1"
        Me.MetroPanel1.Size = New System.Drawing.Size(130, 125)
        Me.MetroPanel1.TabIndex = 9
        Me.MetroPanel1.VerticalScrollbarBarColor = True
        Me.MetroPanel1.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel1.VerticalScrollbarSize = 10
        '
        'MetroPanel3
        '
        Me.MetroPanel3.Controls.Add(Me.NetworkCircularBar)
        Me.MetroPanel3.HorizontalScrollbarBarColor = True
        Me.MetroPanel3.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel3.HorizontalScrollbarSize = 10
        Me.MetroPanel3.Location = New System.Drawing.Point(139, 134)
        Me.MetroPanel3.Name = "MetroPanel3"
        Me.MetroPanel3.Size = New System.Drawing.Size(130, 125)
        Me.MetroPanel3.TabIndex = 10
        Me.MetroPanel3.VerticalScrollbarBarColor = True
        Me.MetroPanel3.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel3.VerticalScrollbarSize = 10
        '
        'MetroPanel4
        '
        Me.MetroPanel4.Controls.Add(Me.VRAMCircularBar)
        Me.MetroPanel4.HorizontalScrollbarBarColor = True
        Me.MetroPanel4.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel4.HorizontalScrollbarSize = 10
        Me.MetroPanel4.Location = New System.Drawing.Point(3, 134)
        Me.MetroPanel4.Name = "MetroPanel4"
        Me.MetroPanel4.Size = New System.Drawing.Size(130, 125)
        Me.MetroPanel4.TabIndex = 11
        Me.MetroPanel4.VerticalScrollbarBarColor = True
        Me.MetroPanel4.VerticalScrollbarHighlightOnWheel = False
        Me.MetroPanel4.VerticalScrollbarSize = 10
        '
        'MetroPanel5
        '
        Me.MetroPanel5.Controls.Add(Me.RAMCircularBar)
        Me.MetroPanel5.HorizontalScrollbarBarColor = True
        Me.MetroPanel5.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroPanel5.HorizontalScrollbarSize = 10
        Me.MetroPanel5.Location = New System.Drawing.Point(139, 3)
        Me.MetroPanel5.Name = "MetroPanel5"
        Me.MetroPanel5.Size = New System.Drawing.Size(130, 125)
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
        Me.RAMCircularBar.Size = New System.Drawing.Size(125, 125)
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
        'NewManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.MetroPanel2)
        Me.Controls.Add(Me.BorderPanel)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.DisplayHeader = False
        Me.Name = "NewManager"
        Me.Padding = New System.Windows.Forms.Padding(20, 30, 20, 20)
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow
        Me.Style = MetroFramework.MetroColorStyle.Green
        Me.Text = "Minecraft 伺服器管理員"
        CType(Me.StyleManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.CPUPerformanceCounter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MetroPanel2.ResumeLayout(False)
        Me.LoadingProgressPanel.ResumeLayout(False)
        Me.LoadingProgressPanel.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.MetroPanel1.ResumeLayout(False)
        Me.MetroPanel3.ResumeLayout(False)
        Me.MetroPanel4.ResumeLayout(False)
        Me.MetroPanel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StyleManager As MetroFramework.Components.MetroStyleManager
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents MetroTile2 As MetroFramework.Controls.MetroTile
    Friend WithEvents MetroTile1 As MetroFramework.Controls.MetroTile
    Friend WithEvents CPUPerformanceCounter As PerformanceCounter
    Friend WithEvents Timer As Timer
    Friend WithEvents BorderPanel As MetroFramework.Controls.MetroPanel
    Friend WithEvents MetroPanel2 As MetroFramework.Controls.MetroPanel
    Friend WithEvents LoadingProgressPanel As MetroFramework.Controls.MetroPanel
    Friend WithEvents LoadingProgressView As MetroFramework.Controls.MetroListView
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
End Class
