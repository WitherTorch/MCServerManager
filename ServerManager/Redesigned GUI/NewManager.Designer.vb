<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewManager
    Inherits MetroFramework.Forms.MetroForm

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.StyleManager = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MetroTile2 = New MetroFramework.Controls.MetroTile()
        Me.MetroTile1 = New MetroFramework.Controls.MetroTile()
        Me.CPUCircularBar = New CircularProgressBar.CircularProgressBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CPUPerformanceCounter = New System.Diagnostics.PerformanceCounter()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.StyleManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
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
        Me.CPUCircularBar.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.CPUCircularBar.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CPUCircularBar.ProgressWidth = 10
        Me.CPUCircularBar.SecondaryFont = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.CPUCircularBar.Size = New System.Drawing.Size(125, 125)
        Me.CPUCircularBar.StartAngle = 270
        Me.CPUCircularBar.SubscriptColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.CPUCircularBar.SubscriptMargin = New System.Windows.Forms.Padding(-17, 10, 0, 0)
        Me.CPUCircularBar.SubscriptText = "CPU"
        Me.CPUCircularBar.SuperscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.CPUCircularBar.SuperscriptMargin = New System.Windows.Forms.Padding(0, 35, 0, 0)
        Me.CPUCircularBar.SuperscriptText = "%"
        Me.CPUCircularBar.TabIndex = 5
        Me.CPUCircularBar.Text = "25"
        Me.CPUCircularBar.TextMargin = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.CPUCircularBar.Value = 25
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CPUCircularBar)
        Me.Panel1.Location = New System.Drawing.Point(176, 33)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(125, 125)
        Me.Panel1.TabIndex = 6
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
        'NewManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.DisplayHeader = False
        Me.Name = "NewManager"
        Me.Padding = New System.Windows.Forms.Padding(20, 30, 20, 20)
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow
        Me.Style = MetroFramework.MetroColorStyle.Green
        Me.Text = "Minecraft 伺服器管理員"
        CType(Me.StyleManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.CPUPerformanceCounter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StyleManager As MetroFramework.Components.MetroStyleManager
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents MetroTile2 As MetroFramework.Controls.MetroTile
    Friend WithEvents MetroTile1 As MetroFramework.Controls.MetroTile
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CPUCircularBar As CircularProgressBar.CircularProgressBar
    Friend WithEvents CPUPerformanceCounter As PerformanceCounter
    Friend WithEvents Timer As Timer
End Class
