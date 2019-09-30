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
        Me.CPUCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.CPUPerformanceCounter = New System.Diagnostics.PerformanceCounter()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.NetworkCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.RAMCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.VRAMCircularBar = New CircularProgressBar.CircularProgressBar()
        CType(Me.StyleManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.CPUPerformanceCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StyleManager
        '
        Me.StyleManager.Owner = Me
        Me.StyleManager.Style = MetroFramework.MetroColorStyle.Green
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MetroTile2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.MetroTile1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(20, 30)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(150, 400)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'MetroTile2
        '
        Me.MetroTile2.ActiveControl = Nothing
        Me.MetroTile2.Dock = System.Windows.Forms.DockStyle.Top
        Me.MetroTile2.Location = New System.Drawing.Point(3, 103)
        Me.MetroTile2.Name = "MetroTile2"
        Me.MetroTile2.Size = New System.Drawing.Size(144, 93)
        Me.MetroTile2.TabIndex = 5
        Me.MetroTile2.Text = "伺服器"
        Me.MetroTile2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.MetroTile2.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall
        Me.MetroTile2.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular
        Me.MetroTile2.UseSelectable = True
        '
        'MetroTile1
        '
        Me.MetroTile1.ActiveControl = Nothing
        Me.MetroTile1.Dock = System.Windows.Forms.DockStyle.Top
        Me.MetroTile1.Location = New System.Drawing.Point(3, 3)
        Me.MetroTile1.Name = "MetroTile1"
        Me.MetroTile1.Size = New System.Drawing.Size(144, 93)
        Me.MetroTile1.TabIndex = 4
        Me.MetroTile1.Text = "概觀"
        Me.MetroTile1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.MetroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall
        Me.MetroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular
        Me.MetroTile1.UseSelectable = True
        '
        'CPUCircularBar
        '
        Me.CPUCircularBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.CPUCircularBar.AnimationSpeed = 500
        Me.CPUCircularBar.BackColor = System.Drawing.Color.Transparent
        Me.CPUCircularBar.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.CPUCircularBar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CPUCircularBar.InnerColor = System.Drawing.Color.WhiteSmoke
        Me.CPUCircularBar.InnerMargin = 2
        Me.CPUCircularBar.InnerWidth = -1
        Me.CPUCircularBar.Location = New System.Drawing.Point(176, 30)
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
        'NetworkCircularBar
        '
        Me.NetworkCircularBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.NetworkCircularBar.AnimationSpeed = 500
        Me.NetworkCircularBar.BackColor = System.Drawing.Color.Transparent
        Me.NetworkCircularBar.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.NetworkCircularBar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.NetworkCircularBar.InnerColor = System.Drawing.Color.White
        Me.NetworkCircularBar.InnerMargin = 2
        Me.NetworkCircularBar.InnerWidth = -1
        Me.NetworkCircularBar.Location = New System.Drawing.Point(307, 30)
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
        'RAMCircularBar
        '
        Me.RAMCircularBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.RAMCircularBar.AnimationSpeed = 500
        Me.RAMCircularBar.BackColor = System.Drawing.Color.Transparent
        Me.RAMCircularBar.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.RAMCircularBar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.RAMCircularBar.InnerColor = System.Drawing.Color.WhiteSmoke
        Me.RAMCircularBar.InnerMargin = 2
        Me.RAMCircularBar.InnerWidth = -1
        Me.RAMCircularBar.Location = New System.Drawing.Point(176, 161)
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
        Me.RAMCircularBar.TabIndex = 7
        Me.RAMCircularBar.Text = "25"
        Me.RAMCircularBar.TextMargin = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.RAMCircularBar.Value = 25
        '
        'VRAMCircularBar
        '
        Me.VRAMCircularBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.VRAMCircularBar.AnimationSpeed = 500
        Me.VRAMCircularBar.BackColor = System.Drawing.Color.Transparent
        Me.VRAMCircularBar.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.VRAMCircularBar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.VRAMCircularBar.InnerColor = System.Drawing.Color.WhiteSmoke
        Me.VRAMCircularBar.InnerMargin = 2
        Me.VRAMCircularBar.InnerWidth = -1
        Me.VRAMCircularBar.Location = New System.Drawing.Point(307, 161)
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
        'NewManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.VRAMCircularBar)
        Me.Controls.Add(Me.RAMCircularBar)
        Me.Controls.Add(Me.NetworkCircularBar)
        Me.Controls.Add(Me.CPUCircularBar)
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
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StyleManager As MetroFramework.Components.MetroStyleManager
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents MetroTile2 As MetroFramework.Controls.MetroTile
    Friend WithEvents MetroTile1 As MetroFramework.Controls.MetroTile
    Friend WithEvents CPUCircularBar As CircularProgressBar.CircularProgressBar
    Friend WithEvents CPUPerformanceCounter As PerformanceCounter
    Friend WithEvents Timer As Timer
    Friend WithEvents VRAMCircularBar As CircularProgressBar.CircularProgressBar
    Friend WithEvents RAMCircularBar As CircularProgressBar.CircularProgressBar
    Friend WithEvents NetworkCircularBar As CircularProgressBar.CircularProgressBar
End Class
