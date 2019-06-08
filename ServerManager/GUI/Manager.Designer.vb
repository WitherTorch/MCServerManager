<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Manager
    Inherits System.Windows.Forms.Form

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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("原版(Vanilla)")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Forge")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("CraftBukkit")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Spigot")
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("SpongeVanilla(Sponge in Vanilla)")
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Paper")
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Akarin")
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("MCPC / Cauldron (無官網)")
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Thermos")
        Dim ListViewItem10 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Contigo")
        Dim ListViewItem11 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Kettle")
        Dim ListViewItem12 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Nukkit(NukkitX)")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Manager))
        Me.MainTabControl = New System.Windows.Forms.TabControl()
        Me.MainPage = New System.Windows.Forms.TabPage()
        Me.MainPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.MemoryGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.NetworkGroupBox = New System.Windows.Forms.GroupBox()
        Me.ExternalIPLabel = New System.Windows.Forms.LinkLabel()
        Me.ExternalIPContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.重新載入外部IPRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IPALabel = New System.Windows.Forms.Label()
        Me.IPLabel = New System.Windows.Forms.LinkLabel()
        Me.InternalIPContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.VListLoadingBox = New System.Windows.Forms.GroupBox()
        Me.ContigoLoadingLabel = New System.Windows.Forms.Label()
        Me.KettleLoadingLabel = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.CauldronLoadingLabel = New System.Windows.Forms.Label()
        Me.SpigotGitLoadingLabel = New System.Windows.Forms.Label()
        Me.AkarinLoadingLabel = New System.Windows.Forms.Label()
        Me.PaperLoadingLabel = New System.Windows.Forms.Label()
        Me.VanillaBedrockLoadingLabel = New System.Windows.Forms.Label()
        Me.SpongeVanillaLoadingLabel = New System.Windows.Forms.Label()
        Me.NukkitLoadingLabel = New System.Windows.Forms.Label()
        Me.VersionListReloadButton = New System.Windows.Forms.Button()
        Me.CraftBukkitLoadingLabel = New System.Windows.Forms.Label()
        Me.SpigotLoadingLabel = New System.Windows.Forms.Label()
        Me.ForgeLoadingLabel = New System.Windows.Forms.Label()
        Me.VanillaLoadingLabel = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.FTBLoadingLabel = New System.Windows.Forms.Label()
        Me.ServerListPage = New System.Windows.Forms.TabPage()
        Me.ServerListPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.BottomButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PackServerListPage = New System.Windows.Forms.TabPage()
        Me.ModpackServerListPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.BungeeCordPage = New System.Windows.Forms.TabPage()
        Me.SolutionListPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ConnectionTabPage = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.UPnPLabel = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NoIPAccountBox = New System.Windows.Forms.TextBox()
        Me.NoIPPasswordBox = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.NoIPUpdateTimeSpanLabel = New System.Windows.Forms.Label()
        Me.HostCheckList = New System.Windows.Forms.CheckedListBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SettingTabPage = New System.Windows.Forms.TabPage()
        Me.SettingTabControl = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BungeeMemoryMaxBox = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BungeeMemoryMinBox = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ServerGroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ServerMemoryMaxBox = New System.Windows.Forms.NumericUpDown()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ServerMemoryMinBox = New System.Windows.Forms.NumericUpDown()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.JavaDefaultBtn = New System.Windows.Forms.Button()
        Me.JavaChooseBtn = New System.Windows.Forms.Button()
        Me.ArguBox = New System.Windows.Forms.TextBox()
        Me.ArguLabel = New System.Windows.Forms.Label()
        Me.JavaVersionLabel = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.SnapshotCheckBox = New System.Windows.Forms.CheckBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GitGroupBox = New System.Windows.Forms.GroupBox()
        Me.GitBashBrowseButton = New System.Windows.Forms.Button()
        Me.GitBashPathBox = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.AboutPage = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ServerSoftwareLinkList = New System.Windows.Forms.ListView()
        Me.ServerSoftwareImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.LibraryListBox = New System.Windows.Forms.ListBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.NoIPTimer = New System.Windows.Forms.Timer(Me.components)
        Me.CheckingTimer = New System.Windows.Forms.Timer(Me.components)
        Me.PerformanceCounter1 = New System.Diagnostics.PerformanceCounter()
        Me.MainTabControl.SuspendLayout()
        Me.MainPage.SuspendLayout()
        Me.MainPanel.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.MemoryGroupBox.SuspendLayout()
        Me.NetworkGroupBox.SuspendLayout()
        Me.ExternalIPContextMenu.SuspendLayout()
        Me.InternalIPContextMenu.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.VListLoadingBox.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.ServerListPage.SuspendLayout()
        Me.BottomButtons.SuspendLayout()
        Me.PackServerListPage.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.BungeeCordPage.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.ConnectionTabPage.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SettingTabPage.SuspendLayout()
        Me.SettingTabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.BungeeMemoryMaxBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BungeeMemoryMinBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ServerGroupBox1.SuspendLayout()
        CType(Me.ServerMemoryMaxBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServerMemoryMinBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GitGroupBox.SuspendLayout()
        Me.AboutPage.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PerformanceCounter1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainTabControl
        '
        Me.MainTabControl.Controls.Add(Me.MainPage)
        Me.MainTabControl.Controls.Add(Me.ServerListPage)
        Me.MainTabControl.Controls.Add(Me.PackServerListPage)
        Me.MainTabControl.Controls.Add(Me.BungeeCordPage)
        Me.MainTabControl.Controls.Add(Me.ConnectionTabPage)
        Me.MainTabControl.Controls.Add(Me.SettingTabPage)
        Me.MainTabControl.Controls.Add(Me.AboutPage)
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.Location = New System.Drawing.Point(0, 0)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(784, 515)
        Me.MainTabControl.TabIndex = 0
        '
        'MainPage
        '
        Me.MainPage.BackColor = System.Drawing.Color.Transparent
        Me.MainPage.Controls.Add(Me.MainPanel)
        Me.MainPage.Location = New System.Drawing.Point(4, 22)
        Me.MainPage.Name = "MainPage"
        Me.MainPage.Padding = New System.Windows.Forms.Padding(3)
        Me.MainPage.Size = New System.Drawing.Size(776, 489)
        Me.MainPage.TabIndex = 0
        Me.MainPage.Text = "概觀 "
        '
        'MainPanel
        '
        Me.MainPanel.AutoSize = True
        Me.MainPanel.BackColor = System.Drawing.Color.White
        Me.MainPanel.ColumnCount = 1
        Me.MainPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.MainPanel.Controls.Add(Me.GroupBox8, 0, 0)
        Me.MainPanel.Controls.Add(Me.GroupBox7, 0, 1)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(3, 3)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.RowCount = 2
        Me.MainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.MainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.MainPanel.Size = New System.Drawing.Size(770, 483)
        Me.MainPanel.TabIndex = 5
        '
        'GroupBox8
        '
        Me.GroupBox8.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox8.Controls.Add(Me.MemoryGroupBox)
        Me.GroupBox8.Controls.Add(Me.NetworkGroupBox)
        Me.GroupBox8.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(762, 110)
        Me.GroupBox8.TabIndex = 14
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "系統"
        '
        'MemoryGroupBox
        '
        Me.MemoryGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MemoryGroupBox.Controls.Add(Me.Label20)
        Me.MemoryGroupBox.Controls.Add(Me.Label19)
        Me.MemoryGroupBox.Controls.Add(Me.Label18)
        Me.MemoryGroupBox.Location = New System.Drawing.Point(6, 21)
        Me.MemoryGroupBox.Name = "MemoryGroupBox"
        Me.MemoryGroupBox.Size = New System.Drawing.Size(372, 84)
        Me.MemoryGroupBox.TabIndex = 14
        Me.MemoryGroupBox.TabStop = False
        Me.MemoryGroupBox.Text = "CPU及記憶體"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 21)
        Me.Label20.Margin = New System.Windows.Forms.Padding(3, 3, 3, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(123, 12)
        Me.Label20.TabIndex = 15
        Me.Label20.Text = "CPU 使用率：載入中..."
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 59)
        Me.Label19.Margin = New System.Windows.Forms.Padding(3, 3, 3, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(122, 12)
        Me.Label19.TabIndex = 14
        Me.Label19.Text = "虛擬記憶體：載入中..."
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 40)
        Me.Label18.Margin = New System.Windows.Forms.Padding(3, 3, 3, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(122, 12)
        Me.Label18.TabIndex = 13
        Me.Label18.Text = "實體記憶體：載入中..."
        '
        'NetworkGroupBox
        '
        Me.NetworkGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NetworkGroupBox.BackColor = System.Drawing.Color.Transparent
        Me.NetworkGroupBox.Controls.Add(Me.ExternalIPLabel)
        Me.NetworkGroupBox.Controls.Add(Me.IPALabel)
        Me.NetworkGroupBox.Controls.Add(Me.IPLabel)
        Me.NetworkGroupBox.Location = New System.Drawing.Point(384, 21)
        Me.NetworkGroupBox.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.NetworkGroupBox.Name = "NetworkGroupBox"
        Me.NetworkGroupBox.Size = New System.Drawing.Size(372, 84)
        Me.NetworkGroupBox.TabIndex = 4
        Me.NetworkGroupBox.TabStop = False
        Me.NetworkGroupBox.Text = "網路"
        '
        'ExternalIPLabel
        '
        Me.ExternalIPLabel.AutoSize = True
        Me.ExternalIPLabel.ContextMenuStrip = Me.ExternalIPContextMenu
        Me.ExternalIPLabel.LinkArea = New System.Windows.Forms.LinkArea(0, 0)
        Me.ExternalIPLabel.Location = New System.Drawing.Point(6, 59)
        Me.ExternalIPLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.ExternalIPLabel.Name = "ExternalIPLabel"
        Me.ExternalIPLabel.Size = New System.Drawing.Size(120, 12)
        Me.ExternalIPLabel.TabIndex = 12
        Me.ExternalIPLabel.Text = "外部IP位址：取得中..."
        '
        'ExternalIPContextMenu
        '
        Me.ExternalIPContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.重新載入外部IPRToolStripMenuItem})
        Me.ExternalIPContextMenu.Name = "ExternalIPContextMenu"
        Me.ExternalIPContextMenu.Size = New System.Drawing.Size(173, 26)
        '
        '重新載入外部IPRToolStripMenuItem
        '
        Me.重新載入外部IPRToolStripMenuItem.Name = "重新載入外部IPRToolStripMenuItem"
        Me.重新載入外部IPRToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.重新載入外部IPRToolStripMenuItem.Text = "重新載入外部IP(&R)"
        '
        'IPALabel
        '
        Me.IPALabel.AutoSize = True
        Me.IPALabel.Location = New System.Drawing.Point(6, 21)
        Me.IPALabel.Margin = New System.Windows.Forms.Padding(3, 3, 3, 4)
        Me.IPALabel.Name = "IPALabel"
        Me.IPALabel.Size = New System.Drawing.Size(110, 12)
        Me.IPALabel.TabIndex = 13
        Me.IPALabel.Text = "網路狀態：檢查中..."
        '
        'IPLabel
        '
        Me.IPLabel.AutoEllipsis = True
        Me.IPLabel.AutoSize = True
        Me.IPLabel.ContextMenuStrip = Me.InternalIPContextMenu
        Me.IPLabel.LinkArea = New System.Windows.Forms.LinkArea(0, 0)
        Me.IPLabel.Location = New System.Drawing.Point(6, 40)
        Me.IPLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.IPLabel.Name = "IPLabel"
        Me.IPLabel.Size = New System.Drawing.Size(120, 12)
        Me.IPLabel.TabIndex = 9
        Me.IPLabel.Text = "內部IP位址：取得中..."
        '
        'InternalIPContextMenu
        '
        Me.InternalIPContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.InternalIPContextMenu.Name = "ExternalIPContextMenu"
        Me.InternalIPContextMenu.Size = New System.Drawing.Size(173, 26)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(172, 22)
        Me.ToolStripMenuItem1.Text = "重新載入內部IP(&R)"
        '
        'GroupBox7
        '
        Me.GroupBox7.AutoSize = True
        Me.GroupBox7.Controls.Add(Me.VListLoadingBox)
        Me.GroupBox7.Controls.Add(Me.GroupBox6)
        Me.GroupBox7.Location = New System.Drawing.Point(3, 119)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(762, 256)
        Me.GroupBox7.TabIndex = 32
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "列表載入進度"
        '
        'VListLoadingBox
        '
        Me.VListLoadingBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VListLoadingBox.BackColor = System.Drawing.Color.Transparent
        Me.VListLoadingBox.Controls.Add(Me.ContigoLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.KettleLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.Label17)
        Me.VListLoadingBox.Controls.Add(Me.CauldronLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.SpigotGitLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.AkarinLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.PaperLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.VanillaBedrockLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.SpongeVanillaLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.NukkitLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.VersionListReloadButton)
        Me.VListLoadingBox.Controls.Add(Me.CraftBukkitLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.SpigotLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.ForgeLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.VanillaLoadingLabel)
        Me.VListLoadingBox.Location = New System.Drawing.Point(6, 21)
        Me.VListLoadingBox.Name = "VListLoadingBox"
        Me.VListLoadingBox.Size = New System.Drawing.Size(416, 214)
        Me.VListLoadingBox.TabIndex = 30
        Me.VListLoadingBox.TabStop = False
        Me.VListLoadingBox.Text = "伺服器軟體"
        '
        'ContigoLoadingLabel
        '
        Me.ContigoLoadingLabel.Location = New System.Drawing.Point(207, 110)
        Me.ContigoLoadingLabel.Name = "ContigoLoadingLabel"
        Me.ContigoLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.ContigoLoadingLabel.TabIndex = 14
        Me.ContigoLoadingLabel.Text = "Contigo：載入完成"
        Me.ContigoLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.ContigoLoadingLabel, "Thermos 的分支，已經停止開發。")
        '
        'KettleLoadingLabel
        '
        Me.KettleLoadingLabel.Location = New System.Drawing.Point(207, 133)
        Me.KettleLoadingLabel.Name = "KettleLoadingLabel"
        Me.KettleLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.KettleLoadingLabel.TabIndex = 13
        Me.KettleLoadingLabel.Text = "Kettle："
        Me.KettleLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.KettleLoadingLabel, "開發中的Minecraft伺服器版本，能夠同時使用模組及插件。")
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(207, 87)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(197, 23)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "Thermos：載入完成"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.Label17, "Cauldron 的繼任者，目前已停止開發。")
        '
        'CauldronLoadingLabel
        '
        Me.CauldronLoadingLabel.Location = New System.Drawing.Point(207, 64)
        Me.CauldronLoadingLabel.Name = "CauldronLoadingLabel"
        Me.CauldronLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.CauldronLoadingLabel.TabIndex = 11
        Me.CauldronLoadingLabel.Text = "MCPC / Cauldron：載入完成"
        Me.CauldronLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.CauldronLoadingLabel, "已經停止開發的Minecraft伺服器版本，可同時使用插件及模組。")
        '
        'SpigotGitLoadingLabel
        '
        Me.SpigotGitLoadingLabel.Location = New System.Drawing.Point(6, 64)
        Me.SpigotGitLoadingLabel.Name = "SpigotGitLoadingLabel"
        Me.SpigotGitLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.SpigotGitLoadingLabel.TabIndex = 10
        Me.SpigotGitLoadingLabel.Text = "Spigot (Git 手動組建)："
        Me.SpigotGitLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.SpigotGitLoadingLabel, "Spigot 官方的手動組建版本，可使用Spigot與CraftBukkit兩種伺服器。")
        '
        'AkarinLoadingLabel
        '
        Me.AkarinLoadingLabel.Location = New System.Drawing.Point(6, 133)
        Me.AkarinLoadingLabel.Name = "AkarinLoadingLabel"
        Me.AkarinLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.AkarinLoadingLabel.TabIndex = 9
        Me.AkarinLoadingLabel.Text = "Akarin："
        Me.AkarinLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.AkarinLoadingLabel, "基於Paper 開發，最大特色是能夠用多核心的方式去運行整個伺服器。")
        '
        'PaperLoadingLabel
        '
        Me.PaperLoadingLabel.Location = New System.Drawing.Point(6, 110)
        Me.PaperLoadingLabel.Name = "PaperLoadingLabel"
        Me.PaperLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.PaperLoadingLabel.TabIndex = 8
        Me.PaperLoadingLabel.Text = "Paper："
        Me.PaperLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.PaperLoadingLabel, "Spigot 的第三方修改版本，允許使用者調控更多的設定。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "某些Spigot 插件無法在Paper上運作。")
        '
        'VanillaBedrockLoadingLabel
        '
        Me.VanillaBedrockLoadingLabel.Location = New System.Drawing.Point(6, 156)
        Me.VanillaBedrockLoadingLabel.Name = "VanillaBedrockLoadingLabel"
        Me.VanillaBedrockLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.VanillaBedrockLoadingLabel.TabIndex = 7
        Me.VanillaBedrockLoadingLabel.Text = "原版(基岩)："
        Me.VanillaBedrockLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.VanillaBedrockLoadingLabel, "官方所發布的基岩版 Minecraft 伺服器軟體(目前處於Alpha 階段)，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "因為使用C++來撰寫，因而無法使用伺服器設定調控使用的記憶體量。" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "備註：由於" &
        "需要Microsoft Edge 的部分程式庫，所以只能在Windows 10上運行。")
        '
        'SpongeVanillaLoadingLabel
        '
        Me.SpongeVanillaLoadingLabel.Location = New System.Drawing.Point(207, 41)
        Me.SpongeVanillaLoadingLabel.Name = "SpongeVanillaLoadingLabel"
        Me.SpongeVanillaLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.SpongeVanillaLoadingLabel.TabIndex = 6
        Me.SpongeVanillaLoadingLabel.Text = "SpongeVanilla："
        Me.SpongeVanillaLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.SpongeVanillaLoadingLabel, "建立在原版的基礎上，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "能夠安裝用Sponge API寫成的特殊插件。")
        '
        'NukkitLoadingLabel
        '
        Me.NukkitLoadingLabel.Location = New System.Drawing.Point(207, 156)
        Me.NukkitLoadingLabel.Name = "NukkitLoadingLabel"
        Me.NukkitLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.NukkitLoadingLabel.TabIndex = 5
        Me.NukkitLoadingLabel.Text = "Nukkit："
        Me.NukkitLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.NukkitLoadingLabel, "攜帶版(基岩版)的伺服器軟體，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "使用方法類似於Bukkit(水桶)，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "能夠安裝用Nukkit的API所寫出來的插件。")
        '
        'VersionListReloadButton
        '
        Me.VersionListReloadButton.Location = New System.Drawing.Point(8, 182)
        Me.VersionListReloadButton.Name = "VersionListReloadButton"
        Me.VersionListReloadButton.Size = New System.Drawing.Size(195, 23)
        Me.VersionListReloadButton.TabIndex = 4
        Me.VersionListReloadButton.Text = "重新載入"
        Me.VersionListReloadButton.UseVisualStyleBackColor = False
        '
        'CraftBukkitLoadingLabel
        '
        Me.CraftBukkitLoadingLabel.Location = New System.Drawing.Point(6, 87)
        Me.CraftBukkitLoadingLabel.Name = "CraftBukkitLoadingLabel"
        Me.CraftBukkitLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.CraftBukkitLoadingLabel.TabIndex = 3
        Me.CraftBukkitLoadingLabel.Text = "CraftBukkit："
        Me.CraftBukkitLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.CraftBukkitLoadingLabel, "俗稱水桶服，可以安裝插件來豐富玩法，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "與Forge不同的是，插件不能添加新的方塊或物品。")
        '
        'SpigotLoadingLabel
        '
        Me.SpigotLoadingLabel.Location = New System.Drawing.Point(6, 41)
        Me.SpigotLoadingLabel.Name = "SpigotLoadingLabel"
        Me.SpigotLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.SpigotLoadingLabel.TabIndex = 2
        Me.SpigotLoadingLabel.Text = "Spigot："
        Me.SpigotLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.SpigotLoadingLabel, "水桶服的分支，為目前最多人使用的伺服器軟體。")
        '
        'ForgeLoadingLabel
        '
        Me.ForgeLoadingLabel.Location = New System.Drawing.Point(207, 18)
        Me.ForgeLoadingLabel.Name = "ForgeLoadingLabel"
        Me.ForgeLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.ForgeLoadingLabel.TabIndex = 1
        Me.ForgeLoadingLabel.Text = "Forge："
        Me.ForgeLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.ForgeLoadingLabel, "建立在原版的基礎上，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "能夠透過安裝模組來豐富伺服器玩法的伺服器。")
        '
        'VanillaLoadingLabel
        '
        Me.VanillaLoadingLabel.Location = New System.Drawing.Point(6, 18)
        Me.VanillaLoadingLabel.Name = "VanillaLoadingLabel"
        Me.VanillaLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.VanillaLoadingLabel.TabIndex = 0
        Me.VanillaLoadingLabel.Text = "原版(Java)："
        Me.VanillaLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.VanillaLoadingLabel, "Mojang 官方發布的Java 版Minecraft 伺服器軟體，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "為大多數伺服器軟體的基礎。")
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.Controls.Add(Me.Button8)
        Me.GroupBox6.Controls.Add(Me.FTBLoadingLabel)
        Me.GroupBox6.Location = New System.Drawing.Point(428, 25)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(328, 210)
        Me.GroupBox6.TabIndex = 31
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "模組包"
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(10, 178)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(195, 23)
        Me.Button8.TabIndex = 13
        Me.Button8.Text = "重新載入"
        Me.Button8.UseVisualStyleBackColor = False
        '
        'FTBLoadingLabel
        '
        Me.FTBLoadingLabel.Location = New System.Drawing.Point(8, 18)
        Me.FTBLoadingLabel.Name = "FTBLoadingLabel"
        Me.FTBLoadingLabel.Size = New System.Drawing.Size(195, 23)
        Me.FTBLoadingLabel.TabIndex = 1
        Me.FTBLoadingLabel.Text = "Feed The Beast："
        Me.FTBLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerListPage
        '
        Me.ServerListPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ServerListPage.Controls.Add(Me.ServerListPanel)
        Me.ServerListPage.Controls.Add(Me.BottomButtons)
        Me.ServerListPage.Location = New System.Drawing.Point(4, 22)
        Me.ServerListPage.Name = "ServerListPage"
        Me.ServerListPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ServerListPage.Size = New System.Drawing.Size(776, 489)
        Me.ServerListPage.TabIndex = 1
        Me.ServerListPage.Text = "伺服器列表"
        '
        'ServerListPanel
        '
        Me.ServerListPanel.AutoScroll = True
        Me.ServerListPanel.ColumnCount = 1
        Me.ServerListPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ServerListPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ServerListPanel.Location = New System.Drawing.Point(3, 3)
        Me.ServerListPanel.Name = "ServerListPanel"
        Me.ServerListPanel.RowCount = 1
        Me.ServerListPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ServerListPanel.Size = New System.Drawing.Size(770, 447)
        Me.ServerListPanel.TabIndex = 6
        '
        'BottomButtons
        '
        Me.BottomButtons.AutoSize = True
        Me.BottomButtons.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.BottomButtons.ColumnCount = 2
        Me.BottomButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.BottomButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.BottomButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.BottomButtons.Controls.Add(Me.Button1, 0, 0)
        Me.BottomButtons.Controls.Add(Me.Button2, 1, 0)
        Me.BottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BottomButtons.Location = New System.Drawing.Point(3, 450)
        Me.BottomButtons.Name = "BottomButtons"
        Me.BottomButtons.RowCount = 1
        Me.BottomButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.BottomButtons.Size = New System.Drawing.Size(770, 36)
        Me.BottomButtons.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.Location = New System.Drawing.Point(11, 4)
        Me.Button1.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(363, 28)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "建立伺服器"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.Location = New System.Drawing.Point(395, 4)
        Me.Button2.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(364, 28)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "加入伺服器"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PackServerListPage
        '
        Me.PackServerListPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.PackServerListPage.Controls.Add(Me.ModpackServerListPanel)
        Me.PackServerListPage.Controls.Add(Me.TableLayoutPanel5)
        Me.PackServerListPage.Location = New System.Drawing.Point(4, 22)
        Me.PackServerListPage.Name = "PackServerListPage"
        Me.PackServerListPage.Padding = New System.Windows.Forms.Padding(3)
        Me.PackServerListPage.Size = New System.Drawing.Size(776, 489)
        Me.PackServerListPage.TabIndex = 7
        Me.PackServerListPage.Text = "模組包伺服器列表"
        '
        'ModpackServerListPanel
        '
        Me.ModpackServerListPanel.AutoScroll = True
        Me.ModpackServerListPanel.ColumnCount = 1
        Me.ModpackServerListPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ModpackServerListPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ModpackServerListPanel.Location = New System.Drawing.Point(3, 3)
        Me.ModpackServerListPanel.Name = "ModpackServerListPanel"
        Me.ModpackServerListPanel.RowCount = 1
        Me.ModpackServerListPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ModpackServerListPanel.Size = New System.Drawing.Size(770, 447)
        Me.ModpackServerListPanel.TabIndex = 6
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.AutoSize = True
        Me.TableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Button6, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Button7, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 450)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(770, 36)
        Me.TableLayoutPanel5.TabIndex = 8
        '
        'Button6
        '
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button6.Location = New System.Drawing.Point(11, 4)
        Me.Button6.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(363, 28)
        Me.Button6.TabIndex = 3
        Me.Button6.Text = "建立伺服器"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button7.Location = New System.Drawing.Point(395, 4)
        Me.Button7.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(364, 28)
        Me.Button7.TabIndex = 2
        Me.Button7.Text = "加入伺服器"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'BungeeCordPage
        '
        Me.BungeeCordPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.BungeeCordPage.Controls.Add(Me.SolutionListPanel)
        Me.BungeeCordPage.Controls.Add(Me.TableLayoutPanel2)
        Me.BungeeCordPage.Location = New System.Drawing.Point(4, 22)
        Me.BungeeCordPage.Name = "BungeeCordPage"
        Me.BungeeCordPage.Padding = New System.Windows.Forms.Padding(3)
        Me.BungeeCordPage.Size = New System.Drawing.Size(776, 489)
        Me.BungeeCordPage.TabIndex = 2
        Me.BungeeCordPage.Text = "BungeeCord"
        '
        'SolutionListPanel
        '
        Me.SolutionListPanel.AutoScroll = True
        Me.SolutionListPanel.ColumnCount = 1
        Me.SolutionListPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.SolutionListPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SolutionListPanel.Location = New System.Drawing.Point(3, 3)
        Me.SolutionListPanel.Name = "SolutionListPanel"
        Me.SolutionListPanel.RowCount = 1
        Me.SolutionListPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.SolutionListPanel.Size = New System.Drawing.Size(770, 447)
        Me.SolutionListPanel.TabIndex = 8
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Button3, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 450)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(770, 36)
        Me.TableLayoutPanel2.TabIndex = 7
        '
        'Button3
        '
        Me.Button3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button3.Location = New System.Drawing.Point(11, 4)
        Me.Button3.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(748, 28)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "建立方案"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ConnectionTabPage
        '
        Me.ConnectionTabPage.Controls.Add(Me.GroupBox2)
        Me.ConnectionTabPage.Controls.Add(Me.GroupBox3)
        Me.ConnectionTabPage.Location = New System.Drawing.Point(4, 22)
        Me.ConnectionTabPage.Name = "ConnectionTabPage"
        Me.ConnectionTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ConnectionTabPage.Size = New System.Drawing.Size(776, 489)
        Me.ConnectionTabPage.TabIndex = 4
        Me.ConnectionTabPage.Text = "連接"
        Me.ConnectionTabPage.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.CheckBox1)
        Me.GroupBox2.Controls.Add(Me.UPnPLabel)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(760, 45)
        Me.GroupBox2.TabIndex = 33
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "UPnP"
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CheckBox1.Enabled = False
        Me.CheckBox1.Location = New System.Drawing.Point(687, 16)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(67, 23)
        Me.CheckBox1.TabIndex = 12
        Me.CheckBox1.Text = "啟用UPnP"
        Me.CheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'UPnPLabel
        '
        Me.UPnPLabel.AutoSize = True
        Me.UPnPLabel.Location = New System.Drawing.Point(6, 21)
        Me.UPnPLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.UPnPLabel.Name = "UPnPLabel"
        Me.UPnPLabel.Size = New System.Drawing.Size(115, 12)
        Me.UPnPLabel.TabIndex = 11
        Me.UPnPLabel.Text = "UPnP 狀態：檢查中..."
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 57)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(763, 125)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "No-IP 動態DNS服務"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(757, 104)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CheckBox2)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.NoIPAccountBox)
        Me.Panel1.Controls.Add(Me.NoIPPasswordBox)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(249, 98)
        Me.Panel1.TabIndex = 17
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Checked = Global.ServerManager.My.MySettings.Default.NoIPPasswordViewChecked
        Me.CheckBox2.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.ServerManager.My.MySettings.Default, "NoIPPasswordViewChecked", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.CheckBox2.Location = New System.Drawing.Point(50, 67)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(72, 16)
        Me.CheckBox2.TabIndex = 17
        Me.CheckBox2.Text = "顯示密碼"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(168, 67)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 16
        Me.Button4.Text = "登入"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 11)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 3, 3, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 12)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "帳號："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 39)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 3, 3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 12)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "密碼："
        '
        'NoIPAccountBox
        '
        Me.NoIPAccountBox.Location = New System.Drawing.Point(50, 6)
        Me.NoIPAccountBox.Name = "NoIPAccountBox"
        Me.NoIPAccountBox.Size = New System.Drawing.Size(193, 22)
        Me.NoIPAccountBox.TabIndex = 1
        '
        'NoIPPasswordBox
        '
        Me.NoIPPasswordBox.Location = New System.Drawing.Point(50, 34)
        Me.NoIPPasswordBox.Name = "NoIPPasswordBox"
        Me.NoIPPasswordBox.Size = New System.Drawing.Size(193, 22)
        Me.NoIPPasswordBox.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.NoIPUpdateTimeSpanLabel)
        Me.Panel2.Controls.Add(Me.HostCheckList)
        Me.Panel2.Controls.Add(Me.Button5)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Enabled = False
        Me.Panel2.Location = New System.Drawing.Point(258, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(496, 98)
        Me.Panel2.TabIndex = 18
        '
        'NoIPUpdateTimeSpanLabel
        '
        Me.NoIPUpdateTimeSpanLabel.AutoSize = True
        Me.NoIPUpdateTimeSpanLabel.Location = New System.Drawing.Point(48, 77)
        Me.NoIPUpdateTimeSpanLabel.Name = "NoIPUpdateTimeSpanLabel"
        Me.NoIPUpdateTimeSpanLabel.Size = New System.Drawing.Size(0, 12)
        Me.NoIPUpdateTimeSpanLabel.TabIndex = 20
        '
        'HostCheckList
        '
        Me.HostCheckList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HostCheckList.FormattingEnabled = True
        Me.HostCheckList.Location = New System.Drawing.Point(50, 6)
        Me.HostCheckList.Name = "HostCheckList"
        Me.HostCheckList.Size = New System.Drawing.Size(440, 55)
        Me.HostCheckList.TabIndex = 19
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.Location = New System.Drawing.Point(415, 69)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 23)
        Me.Button5.TabIndex = 18
        Me.Button5.Text = "連接"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 8)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 3, 3, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "主機："
        '
        'SettingTabPage
        '
        Me.SettingTabPage.Controls.Add(Me.SettingTabControl)
        Me.SettingTabPage.Location = New System.Drawing.Point(4, 22)
        Me.SettingTabPage.Name = "SettingTabPage"
        Me.SettingTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.SettingTabPage.Size = New System.Drawing.Size(776, 489)
        Me.SettingTabPage.TabIndex = 6
        Me.SettingTabPage.Text = "設定"
        Me.SettingTabPage.UseVisualStyleBackColor = True
        '
        'SettingTabControl
        '
        Me.SettingTabControl.Controls.Add(Me.TabPage1)
        Me.SettingTabControl.Controls.Add(Me.TabPage2)
        Me.SettingTabControl.Controls.Add(Me.TabPage3)
        Me.SettingTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SettingTabControl.Location = New System.Drawing.Point(3, 3)
        Me.SettingTabControl.Name = "SettingTabControl"
        Me.SettingTabControl.SelectedIndex = 0
        Me.SettingTabControl.Size = New System.Drawing.Size(770, 483)
        Me.SettingTabControl.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Controls.Add(Me.JavaDefaultBtn)
        Me.TabPage1.Controls.Add(Me.JavaChooseBtn)
        Me.TabPage1.Controls.Add(Me.ArguBox)
        Me.TabPage1.Controls.Add(Me.ArguLabel)
        Me.TabPage1.Controls.Add(Me.JavaVersionLabel)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(762, 457)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Java"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ServerGroupBox1, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(6, 63)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(750, 82)
        Me.TableLayoutPanel1.TabIndex = 41
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.BungeeMemoryMaxBox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.BungeeMemoryMinBox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(378, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(369, 76)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "BungeeCord 主程式"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "記憶體最大值："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(179, 26)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 12)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "MB"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BungeeMemoryMaxBox
        '
        Me.BungeeMemoryMaxBox.Location = New System.Drawing.Point(98, 21)
        Me.BungeeMemoryMaxBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.BungeeMemoryMaxBox.Name = "BungeeMemoryMaxBox"
        Me.BungeeMemoryMaxBox.Size = New System.Drawing.Size(78, 22)
        Me.BungeeMemoryMaxBox.TabIndex = 26
        Me.BungeeMemoryMaxBox.Tag = ""
        Me.BungeeMemoryMaxBox.Value = New Decimal(New Integer() {16, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(179, 53)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 12)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "MB"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BungeeMemoryMinBox
        '
        Me.BungeeMemoryMinBox.Location = New System.Drawing.Point(98, 49)
        Me.BungeeMemoryMinBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.BungeeMemoryMinBox.Name = "BungeeMemoryMinBox"
        Me.BungeeMemoryMinBox.Size = New System.Drawing.Size(78, 22)
        Me.BungeeMemoryMinBox.TabIndex = 29
        Me.BungeeMemoryMinBox.Tag = ""
        Me.BungeeMemoryMinBox.Value = New Decimal(New Integer() {16, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 53)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 12)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "記憶體最小值："
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ServerGroupBox1
        '
        Me.ServerGroupBox1.Controls.Add(Me.Label11)
        Me.ServerGroupBox1.Controls.Add(Me.Label12)
        Me.ServerGroupBox1.Controls.Add(Me.ServerMemoryMaxBox)
        Me.ServerGroupBox1.Controls.Add(Me.Label14)
        Me.ServerGroupBox1.Controls.Add(Me.ServerMemoryMinBox)
        Me.ServerGroupBox1.Controls.Add(Me.Label13)
        Me.ServerGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ServerGroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.ServerGroupBox1.Name = "ServerGroupBox1"
        Me.ServerGroupBox1.Size = New System.Drawing.Size(369, 76)
        Me.ServerGroupBox1.TabIndex = 30
        Me.ServerGroupBox1.TabStop = False
        Me.ServerGroupBox1.Text = "伺服器"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 26)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 12)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "記憶體最大值："
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(179, 26)
        Me.Label12.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(23, 12)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "MB"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerMemoryMaxBox
        '
        Me.ServerMemoryMaxBox.Location = New System.Drawing.Point(98, 21)
        Me.ServerMemoryMaxBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.ServerMemoryMaxBox.Name = "ServerMemoryMaxBox"
        Me.ServerMemoryMaxBox.Size = New System.Drawing.Size(78, 22)
        Me.ServerMemoryMaxBox.TabIndex = 26
        Me.ServerMemoryMaxBox.Tag = ""
        Me.ServerMemoryMaxBox.Value = New Decimal(New Integer() {1024, 0, 0, 0})
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(179, 53)
        Me.Label14.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(23, 12)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "MB"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerMemoryMinBox
        '
        Me.ServerMemoryMinBox.Location = New System.Drawing.Point(98, 49)
        Me.ServerMemoryMinBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.ServerMemoryMinBox.Name = "ServerMemoryMinBox"
        Me.ServerMemoryMinBox.Size = New System.Drawing.Size(78, 22)
        Me.ServerMemoryMinBox.TabIndex = 29
        Me.ServerMemoryMinBox.Tag = ""
        Me.ServerMemoryMinBox.Value = New Decimal(New Integer() {1024, 0, 0, 0})
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 53)
        Me.Label13.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(89, 12)
        Me.Label13.TabIndex = 28
        Me.Label13.Text = "記憶體最小值："
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'JavaDefaultBtn
        '
        Me.JavaDefaultBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.JavaDefaultBtn.Enabled = False
        Me.JavaDefaultBtn.Location = New System.Drawing.Point(638, 6)
        Me.JavaDefaultBtn.Name = "JavaDefaultBtn"
        Me.JavaDefaultBtn.Size = New System.Drawing.Size(56, 23)
        Me.JavaDefaultBtn.TabIndex = 40
        Me.JavaDefaultBtn.Text = "預設"
        Me.JavaDefaultBtn.UseVisualStyleBackColor = True
        '
        'JavaChooseBtn
        '
        Me.JavaChooseBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.JavaChooseBtn.Location = New System.Drawing.Point(700, 6)
        Me.JavaChooseBtn.Name = "JavaChooseBtn"
        Me.JavaChooseBtn.Size = New System.Drawing.Size(56, 23)
        Me.JavaChooseBtn.TabIndex = 39
        Me.JavaChooseBtn.Text = "選擇"
        Me.JavaChooseBtn.UseVisualStyleBackColor = True
        '
        'ArguBox
        '
        Me.ArguBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ArguBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ArguBox.Location = New System.Drawing.Point(99, 35)
        Me.ArguBox.Name = "ArguBox"
        Me.ArguBox.Size = New System.Drawing.Size(657, 22)
        Me.ArguBox.TabIndex = 38
        '
        'ArguLabel
        '
        Me.ArguLabel.AutoSize = True
        Me.ArguLabel.Location = New System.Drawing.Point(7, 39)
        Me.ArguLabel.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.ArguLabel.Name = "ArguLabel"
        Me.ArguLabel.Size = New System.Drawing.Size(88, 12)
        Me.ArguLabel.TabIndex = 37
        Me.ArguLabel.Text = "Java 額外參數："
        Me.ArguLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'JavaVersionLabel
        '
        Me.JavaVersionLabel.AutoSize = True
        Me.JavaVersionLabel.Location = New System.Drawing.Point(7, 11)
        Me.JavaVersionLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.JavaVersionLabel.Name = "JavaVersionLabel"
        Me.JavaVersionLabel.Size = New System.Drawing.Size(109, 12)
        Me.JavaVersionLabel.TabIndex = 36
        Me.JavaVersionLabel.Text = "Java 版本：取得中..."
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.CheckBox3)
        Me.TabPage2.Controls.Add(Me.SnapshotCheckBox)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(762, 457)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "伺服器"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(6, 28)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(159, 16)
        Me.CheckBox3.TabIndex = 33
        Me.CheckBox3.Text = "是否可選擇安裝舊版Forge"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'SnapshotCheckBox
        '
        Me.SnapshotCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SnapshotCheckBox.AutoSize = True
        Me.SnapshotCheckBox.Location = New System.Drawing.Point(6, 6)
        Me.SnapshotCheckBox.Name = "SnapshotCheckBox"
        Me.SnapshotCheckBox.Size = New System.Drawing.Size(168, 16)
        Me.SnapshotCheckBox.TabIndex = 32
        Me.SnapshotCheckBox.Text = "在列表內顯示原版快照版本"
        Me.SnapshotCheckBox.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GitGroupBox)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(762, 457)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "外部工具"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'GitGroupBox
        '
        Me.GitGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GitGroupBox.Controls.Add(Me.GitBashBrowseButton)
        Me.GitGroupBox.Controls.Add(Me.GitBashPathBox)
        Me.GitGroupBox.Controls.Add(Me.Label16)
        Me.GitGroupBox.Controls.Add(Me.Label15)
        Me.GitGroupBox.Location = New System.Drawing.Point(6, 6)
        Me.GitGroupBox.Name = "GitGroupBox"
        Me.GitGroupBox.Size = New System.Drawing.Size(750, 68)
        Me.GitGroupBox.TabIndex = 0
        Me.GitGroupBox.TabStop = False
        Me.GitGroupBox.Text = "Git Bash"
        '
        'GitBashBrowseButton
        '
        Me.GitBashBrowseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GitBashBrowseButton.Font = New System.Drawing.Font("新細明體", 11.0!)
        Me.GitBashBrowseButton.Location = New System.Drawing.Point(717, 37)
        Me.GitBashBrowseButton.Name = "GitBashBrowseButton"
        Me.GitBashBrowseButton.Size = New System.Drawing.Size(27, 22)
        Me.GitBashBrowseButton.TabIndex = 3
        Me.GitBashBrowseButton.Text = "..."
        Me.GitBashBrowseButton.UseVisualStyleBackColor = True
        '
        'GitBashPathBox
        '
        Me.GitBashPathBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GitBashPathBox.Location = New System.Drawing.Point(47, 37)
        Me.GitBashPathBox.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.GitBashPathBox.Name = "GitBashPathBox"
        Me.GitBashPathBox.Size = New System.Drawing.Size(664, 22)
        Me.GitBashPathBox.TabIndex = 2
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 42)
        Me.Label16.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(41, 12)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "路徑："
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 18)
        Me.Label15.Margin = New System.Windows.Forms.Padding(3, 0, 3, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(197, 12)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "用於建置需手動建置的伺服器軟體。"
        '
        'AboutPage
        '
        Me.AboutPage.Controls.Add(Me.TableLayoutPanel4)
        Me.AboutPage.Location = New System.Drawing.Point(4, 22)
        Me.AboutPage.Name = "AboutPage"
        Me.AboutPage.Padding = New System.Windows.Forms.Padding(3)
        Me.AboutPage.Size = New System.Drawing.Size(776, 489)
        Me.AboutPage.TabIndex = 5
        Me.AboutPage.Text = "關於"
        Me.AboutPage.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.GroupBox5, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Panel3, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(770, 483)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.ServerSoftwareLinkList)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox5.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(388, 3)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(379, 477)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "各伺服器軟體官網(按兩下即可開啟)"
        '
        'ServerSoftwareLinkList
        '
        Me.ServerSoftwareLinkList.AutoArrange = False
        Me.ServerSoftwareLinkList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ServerSoftwareLinkList.Font = New System.Drawing.Font("微軟正黑體", 11.0!)
        Me.ServerSoftwareLinkList.HideSelection = False
        ListViewItem1.Checked = True
        ListViewItem1.StateImageIndex = 2
        ListViewItem1.Tag = "https://www.minecraft.net/zh-hant/"
        ListViewItem2.Checked = True
        ListViewItem2.StateImageIndex = 3
        ListViewItem2.Tag = "https://files.minecraftforge.net/"
        ListViewItem3.StateImageIndex = 0
        ListViewItem3.Tag = "https://bukkit.org/"
        ListViewItem4.Checked = True
        ListViewItem4.StateImageIndex = 1
        ListViewItem4.Tag = "https://www.spigotmc.org/"
        ListViewItem5.Checked = True
        ListViewItem5.StateImageIndex = 4
        ListViewItem5.Tag = "https://www.spongepowered.org/"
        ListViewItem6.Checked = True
        ListViewItem6.StateImageIndex = 5
        ListViewItem6.Tag = "https://papermc.io/"
        ListViewItem7.Checked = True
        ListViewItem7.StateImageIndex = 6
        ListViewItem7.Tag = "https://akarin.io/"
        ListViewItem8.Checked = True
        ListViewItem8.StateImageIndex = 9
        ListViewItem9.Checked = True
        ListViewItem9.StateImageIndex = 8
        ListViewItem9.Tag = "https://cyberdynecc.github.io/Thermos/"
        ListViewItem10.Checked = True
        ListViewItem10.StateImageIndex = 10
        ListViewItem10.Tag = "https://github.com/djoveryde/Contigo"
        ListViewItem11.Checked = True
        ListViewItem11.StateImageIndex = 11
        ListViewItem11.Tag = "https://github.com/KettleFoundation/Kettle"
        ListViewItem12.Checked = True
        ListViewItem12.StateImageIndex = 7
        ListViewItem12.Tag = "https://nukkitx.com/"
        Me.ServerSoftwareLinkList.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9, ListViewItem10, ListViewItem11, ListViewItem12})
        Me.ServerSoftwareLinkList.Location = New System.Drawing.Point(3, 19)
        Me.ServerSoftwareLinkList.MultiSelect = False
        Me.ServerSoftwareLinkList.Name = "ServerSoftwareLinkList"
        Me.ServerSoftwareLinkList.Size = New System.Drawing.Size(373, 455)
        Me.ServerSoftwareLinkList.StateImageList = Me.ServerSoftwareImageList
        Me.ServerSoftwareLinkList.TabIndex = 0
        Me.ServerSoftwareLinkList.UseCompatibleStateImageBehavior = False
        Me.ServerSoftwareLinkList.View = System.Windows.Forms.View.List
        '
        'ServerSoftwareImageList
        '
        Me.ServerSoftwareImageList.ImageStream = CType(resources.GetObject("ServerSoftwareImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ServerSoftwareImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ServerSoftwareImageList.Images.SetKeyName(0, "bukkit.png")
        Me.ServerSoftwareImageList.Images.SetKeyName(1, "spigot.png")
        Me.ServerSoftwareImageList.Images.SetKeyName(2, "vanilla.png")
        Me.ServerSoftwareImageList.Images.SetKeyName(3, "forge.png")
        Me.ServerSoftwareImageList.Images.SetKeyName(4, "SpongeIcon.ico")
        Me.ServerSoftwareImageList.Images.SetKeyName(5, "paper.png")
        Me.ServerSoftwareImageList.Images.SetKeyName(6, "akarin.png")
        Me.ServerSoftwareImageList.Images.SetKeyName(7, "Nukkit.png")
        Me.ServerSoftwareImageList.Images.SetKeyName(8, "thermos_icon.png")
        Me.ServerSoftwareImageList.Images.SetKeyName(9, "cauldron.png")
        Me.ServerSoftwareImageList.Images.SetKeyName(10, "Contigo.png")
        Me.ServerSoftwareImageList.Images.SetKeyName(11, "kettle.png")
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.GroupBox4)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(379, 477)
        Me.Panel3.TabIndex = 1
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.LibraryListBox)
        Me.GroupBox4.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(3, 159)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(372, 310)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "使用的函式庫"
        '
        'LibraryListBox
        '
        Me.LibraryListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LibraryListBox.Font = New System.Drawing.Font("微軟正黑體", 10.25!)
        Me.LibraryListBox.FormattingEnabled = True
        Me.LibraryListBox.ItemHeight = 18
        Me.LibraryListBox.Items.AddRange(New Object() {"Newtonsoft.Json (用於解析JSON)", "HtmlAgilityPack (用於提取HTML 元素)", "HtmlRenderer (用於顯示簡單HTML 網頁)", "NATUPnP (提供UPnP 支援)", "NoIP.DDNS (提供No-IP 連接支援)", "YamlDotNet (用於解析YAML)", "DropDownControls (用於顯示群組項目)"})
        Me.LibraryListBox.Location = New System.Drawing.Point(3, 19)
        Me.LibraryListBox.Name = "LibraryListBox"
        Me.LibraryListBox.Size = New System.Drawing.Size(366, 288)
        Me.LibraryListBox.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoEllipsis = True
        Me.Label10.Font = New System.Drawing.Font("微軟正黑體", 11.0!)
        Me.Label10.Location = New System.Drawing.Point(159, 57)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(213, 96)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "作者：Error_404、冰霜、asd7766zxc"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("微軟正黑體", 11.0!)
        Me.Label9.Location = New System.Drawing.Point(159, 34)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(129, 19)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "版本：<Version>"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ServerManager.My.Resources.Resources.Icon
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(150, 150)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label8.Location = New System.Drawing.Point(159, 10)
        Me.Label8.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(181, 20)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Minecraft 伺服器管理員"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Minecraft 伺服器管理員 by Error_404"
        Me.NotifyIcon1.Visible = True
        '
        'NoIPTimer
        '
        Me.NoIPTimer.Interval = 1000
        '
        'CheckingTimer
        '
        Me.CheckingTimer.Enabled = True
        Me.CheckingTimer.Interval = 200
        '
        'PerformanceCounter1
        '
        Me.PerformanceCounter1.CategoryName = "Processor"
        Me.PerformanceCounter1.CounterName = "% Processor Time"
        Me.PerformanceCounter1.InstanceName = "_Total"
        '
        'Manager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(784, 515)
        Me.Controls.Add(Me.MainTabControl)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Manager"
        Me.Text = "Minecraft 伺服器管理員"
        Me.MainTabControl.ResumeLayout(False)
        Me.MainPage.ResumeLayout(False)
        Me.MainPage.PerformLayout()
        Me.MainPanel.ResumeLayout(False)
        Me.MainPanel.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.MemoryGroupBox.ResumeLayout(False)
        Me.MemoryGroupBox.PerformLayout()
        Me.NetworkGroupBox.ResumeLayout(False)
        Me.NetworkGroupBox.PerformLayout()
        Me.ExternalIPContextMenu.ResumeLayout(False)
        Me.InternalIPContextMenu.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.VListLoadingBox.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.ServerListPage.ResumeLayout(False)
        Me.ServerListPage.PerformLayout()
        Me.BottomButtons.ResumeLayout(False)
        Me.PackServerListPage.ResumeLayout(False)
        Me.PackServerListPage.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.BungeeCordPage.ResumeLayout(False)
        Me.BungeeCordPage.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ConnectionTabPage.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.SettingTabPage.ResumeLayout(False)
        Me.SettingTabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.BungeeMemoryMaxBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BungeeMemoryMinBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ServerGroupBox1.ResumeLayout(False)
        Me.ServerGroupBox1.PerformLayout()
        CType(Me.ServerMemoryMaxBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServerMemoryMinBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.GitGroupBox.ResumeLayout(False)
        Me.GitGroupBox.PerformLayout()
        Me.AboutPage.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PerformanceCounter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MainTabControl As TabControl
    Friend WithEvents MainPage As TabPage
    Friend WithEvents ServerListPage As TabPage
    Friend WithEvents ServerListPanel As TableLayoutPanel
    Friend WithEvents BottomButtons As TableLayoutPanel
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents MainPanel As TableLayoutPanel
    Friend WithEvents BungeeCordPage As TabPage
    Friend WithEvents SolutionListPanel As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Button3 As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents VListLoadingBox As GroupBox
    Friend WithEvents SpongeVanillaLoadingLabel As Label
    Friend WithEvents NukkitLoadingLabel As Label
    Friend WithEvents VersionListReloadButton As Button
    Friend WithEvents CraftBukkitLoadingLabel As Label
    Friend WithEvents SpigotLoadingLabel As Label
    Friend WithEvents ForgeLoadingLabel As Label
    Friend WithEvents VanillaLoadingLabel As Label
    Friend WithEvents ConnectionTabPage As TabPage
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents NoIPPasswordBox As TextBox
    Friend WithEvents NoIPAccountBox As TextBox
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents NoIPTimer As Timer
    Friend WithEvents HostCheckList As CheckedListBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents NoIPUpdateTimeSpanLabel As Label
    Friend WithEvents VanillaBedrockLoadingLabel As Label
    Friend WithEvents PaperLoadingLabel As Label
    Friend WithEvents AkarinLoadingLabel As Label
    Friend WithEvents AboutPage As TabPage
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents LibraryListBox As ListBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents ServerSoftwareLinkList As ListView
    Friend WithEvents ServerSoftwareImageList As ImageList
    Friend WithEvents SettingTabPage As TabPage
    Friend WithEvents SettingTabControl As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents UPnPLabel As Label
    Friend WithEvents JavaDefaultBtn As Button
    Friend WithEvents JavaChooseBtn As Button
    Friend WithEvents ArguBox As TextBox
    Friend WithEvents ArguLabel As Label
    Friend WithEvents JavaVersionLabel As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents BungeeMemoryMaxBox As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents BungeeMemoryMinBox As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents ServerGroupBox1 As GroupBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents ServerMemoryMaxBox As NumericUpDown
    Friend WithEvents Label14 As Label
    Friend WithEvents ServerMemoryMinBox As NumericUpDown
    Friend WithEvents Label13 As Label
    Friend WithEvents SnapshotCheckBox As CheckBox
    Friend WithEvents SpigotGitLoadingLabel As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents GitGroupBox As GroupBox
    Friend WithEvents GitBashBrowseButton As Button
    Friend WithEvents GitBashPathBox As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents CauldronLoadingLabel As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents PackServerListPage As TabPage
    Friend WithEvents ModpackServerListPanel As TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Button8 As Button
    Friend WithEvents FTBLoadingLabel As Label
    Friend WithEvents ContigoLoadingLabel As Label
    Friend WithEvents KettleLoadingLabel As Label
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents MemoryGroupBox As GroupBox
    Friend WithEvents Label18 As Label
    Friend WithEvents NetworkGroupBox As GroupBox
    Friend WithEvents ExternalIPLabel As LinkLabel
    Friend WithEvents IPALabel As Label
    Friend WithEvents IPLabel As LinkLabel
    Friend WithEvents CheckingTimer As Timer
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents ExternalIPContextMenu As ContextMenuStrip
    Friend WithEvents 重新載入外部IPRToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InternalIPContextMenu As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents PerformanceCounter1 As PerformanceCounter
End Class
