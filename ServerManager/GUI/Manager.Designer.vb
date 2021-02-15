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
        Dim ListViewItem12 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("CatServer")
        Dim ListViewItem13 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("NukkitX")
        Dim ListViewItem14 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("PocketMine-MP")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Manager))
        Me.MainTabControl = New MetroFramework.Controls.MetroTabControl()
        Me.MainPage = New MetroFramework.Controls.MetroTabPage()
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
        Me.CatServerLoadingLabel = New System.Windows.Forms.Label()
        Me.PocketMineLoadingLabel = New System.Windows.Forms.Label()
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
        Me.ServerListPage = New MetroFramework.Controls.MetroTabPage()
        Me.ServerListPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.BottomButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PackServerListPage = New MetroFramework.Controls.MetroTabPage()
        Me.ModpackServerListPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.ServerIntergratePage = New MetroFramework.Controls.MetroTabPage()
        Me.SolutionListPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ConnectionTabPage = New MetroFramework.Controls.MetroTabPage()
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
        Me.NoIPAccountBox = New MetroFramework.Controls.MetroTextBox()
        Me.NoIPPasswordBox = New MetroFramework.Controls.MetroTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.NoIPUpdateTimeSpanLabel = New System.Windows.Forms.Label()
        Me.HostCheckList = New System.Windows.Forms.CheckedListBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SettingTabPage = New MetroFramework.Controls.MetroTabPage()
        Me.SettingTabControl = New MetroFramework.Controls.MetroTabControl()
        Me.TabPage4 = New MetroFramework.Controls.MetroTabPage()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.ModpackServerMemoryMaxBox = New System.Windows.Forms.NumericUpDown()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.ModpackServerMemoryMinBox = New System.Windows.Forms.NumericUpDown()
        Me.Label26 = New System.Windows.Forms.Label()
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
        Me.ArguBox = New MetroFramework.Controls.MetroTextBox()
        Me.ArguLabel = New System.Windows.Forms.Label()
        Me.JavaVersionLabel = New System.Windows.Forms.Label()
        Me.TabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.SnapshotCheckBox = New System.Windows.Forms.CheckBox()
        Me.TabPage3 = New MetroFramework.Controls.MetroTabPage()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.PHPBrowseButton = New System.Windows.Forms.Button()
        Me.PHPPathBox = New MetroFramework.Controls.MetroTextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GitGroupBox = New System.Windows.Forms.GroupBox()
        Me.GitBashBrowseButton = New System.Windows.Forms.Button()
        Me.GitBashPathBox = New MetroFramework.Controls.MetroTextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.AboutPage = New MetroFramework.Controls.MetroTabPage()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ServerSoftwareLinkList = New System.Windows.Forms.ListView()
        Me.ServerSoftwareImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
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
        Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
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
        Me.ServerListPage.SuspendLayout()
        Me.BottomButtons.SuspendLayout()
        Me.PackServerListPage.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.ServerIntergratePage.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.ConnectionTabPage.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SettingTabPage.SuspendLayout()
        Me.SettingTabControl.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        CType(Me.ModpackServerMemoryMaxBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModpackServerMemoryMinBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.BungeeMemoryMaxBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BungeeMemoryMinBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ServerGroupBox1.SuspendLayout()
        CType(Me.ServerMemoryMaxBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServerMemoryMinBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GitGroupBox.SuspendLayout()
        Me.AboutPage.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PerformanceCounter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainTabControl
        '
        Me.MainTabControl.Controls.Add(Me.MainPage)
        Me.MainTabControl.Controls.Add(Me.ServerListPage)
        Me.MainTabControl.Controls.Add(Me.PackServerListPage)
        Me.MainTabControl.Controls.Add(Me.ServerIntergratePage)
        Me.MainTabControl.Controls.Add(Me.ConnectionTabPage)
        Me.MainTabControl.Controls.Add(Me.SettingTabPage)
        Me.MainTabControl.Controls.Add(Me.AboutPage)
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular
        Me.MainTabControl.Location = New System.Drawing.Point(20, 30)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(744, 465)
        Me.MainTabControl.Style = MetroFramework.MetroColorStyle.Green
        Me.MainTabControl.TabIndex = 0
        Me.MainTabControl.UseSelectable = True
        '
        'MainPage
        '
        Me.MainPage.BackColor = System.Drawing.Color.Transparent
        Me.MainPage.Controls.Add(Me.MainPanel)
        Me.MainPage.HorizontalScrollbarBarColor = True
        Me.MainPage.HorizontalScrollbarHighlightOnWheel = False
        Me.MainPage.HorizontalScrollbarSize = 10
        Me.MainPage.Location = New System.Drawing.Point(4, 38)
        Me.MainPage.Name = "MainPage"
        Me.MainPage.Padding = New System.Windows.Forms.Padding(3)
        Me.MainPage.Size = New System.Drawing.Size(736, 423)
        Me.MainPage.TabIndex = 0
        Me.MainPage.Text = "概觀 "
        Me.MainPage.VerticalScrollbarBarColor = True
        Me.MainPage.VerticalScrollbarHighlightOnWheel = False
        Me.MainPage.VerticalScrollbarSize = 10
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
        Me.MainPanel.Size = New System.Drawing.Size(730, 417)
        Me.MainPanel.TabIndex = 5
        '
        'GroupBox8
        '
        Me.GroupBox8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox8.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox8.Controls.Add(Me.MemoryGroupBox)
        Me.GroupBox8.Controls.Add(Me.NetworkGroupBox)
        Me.GroupBox8.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox8.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(727, 110)
        Me.GroupBox8.TabIndex = 14
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "系統"
        '
        'MemoryGroupBox
        '
        Me.MemoryGroupBox.Controls.Add(Me.Label20)
        Me.MemoryGroupBox.Controls.Add(Me.Label19)
        Me.MemoryGroupBox.Controls.Add(Me.Label18)
        Me.MemoryGroupBox.Location = New System.Drawing.Point(6, 21)
        Me.MemoryGroupBox.Name = "MemoryGroupBox"
        Me.MemoryGroupBox.Size = New System.Drawing.Size(377, 84)
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
        Me.Label20.Size = New System.Drawing.Size(128, 16)
        Me.Label20.TabIndex = 15
        Me.Label20.Text = "CPU 使用率：載入中..."
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 59)
        Me.Label19.Margin = New System.Windows.Forms.Padding(3, 3, 3, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(125, 16)
        Me.Label19.TabIndex = 14
        Me.Label19.Text = "虛擬記憶體：載入中..."
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 40)
        Me.Label18.Margin = New System.Windows.Forms.Padding(3, 3, 3, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(125, 16)
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
        Me.NetworkGroupBox.Size = New System.Drawing.Size(337, 84)
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
        Me.ExternalIPLabel.Size = New System.Drawing.Size(123, 16)
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
        Me.IPALabel.Size = New System.Drawing.Size(113, 16)
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
        Me.IPLabel.Size = New System.Drawing.Size(123, 16)
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
        Me.GroupBox7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.Controls.Add(Me.VListLoadingBox)
        Me.GroupBox7.Location = New System.Drawing.Point(3, 119)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(724, 226)
        Me.GroupBox7.TabIndex = 32
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "列表載入進度"
        '
        'VListLoadingBox
        '
        Me.VListLoadingBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VListLoadingBox.BackColor = System.Drawing.Color.Transparent
        Me.VListLoadingBox.Controls.Add(Me.CatServerLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.PocketMineLoadingLabel)
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
        Me.VListLoadingBox.Size = New System.Drawing.Size(711, 193)
        Me.VListLoadingBox.TabIndex = 30
        Me.VListLoadingBox.TabStop = False
        Me.VListLoadingBox.Text = "伺服器軟體"
        '
        'CatServerLoadingLabel
        '
        Me.CatServerLoadingLabel.Location = New System.Drawing.Point(410, 18)
        Me.CatServerLoadingLabel.Name = "CatServerLoadingLabel"
        Me.CatServerLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.CatServerLoadingLabel.TabIndex = 16
        Me.CatServerLoadingLabel.Text = "CatServer："
        Me.CatServerLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.CatServerLoadingLabel, "由Laohuayu 所寫的混核伺服器軟體，可同時使用插件及模組的功能。")
        '
        'PocketMineLoadingLabel
        '
        Me.PocketMineLoadingLabel.Location = New System.Drawing.Point(410, 64)
        Me.PocketMineLoadingLabel.Name = "PocketMineLoadingLabel"
        Me.PocketMineLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.PocketMineLoadingLabel.TabIndex = 15
        Me.PocketMineLoadingLabel.Text = "PocketMine-MP："
        Me.PocketMineLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.PocketMineLoadingLabel, "用PHP 撰寫而成，適用於基岩版的伺服器軟體，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "可以使用插件來擴展伺服器彈性。")
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
        Me.VanillaBedrockLoadingLabel.Location = New System.Drawing.Point(410, 41)
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
        Me.NukkitLoadingLabel.Location = New System.Drawing.Point(410, 87)
        Me.NukkitLoadingLabel.Name = "NukkitLoadingLabel"
        Me.NukkitLoadingLabel.Size = New System.Drawing.Size(197, 23)
        Me.NukkitLoadingLabel.TabIndex = 5
        Me.NukkitLoadingLabel.Text = "NukkitX："
        Me.NukkitLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.NukkitLoadingLabel, "攜帶版(基岩版)的伺服器軟體，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "使用方法類似於Bukkit(水桶)，" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "能夠安裝用Nukkit的API所寫出來的插件。")
        '
        'VersionListReloadButton
        '
        Me.VersionListReloadButton.Location = New System.Drawing.Point(6, 159)
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
        'ServerListPage
        '
        Me.ServerListPage.BackColor = System.Drawing.Color.Transparent
        Me.ServerListPage.Controls.Add(Me.ServerListPanel)
        Me.ServerListPage.Controls.Add(Me.BottomButtons)
        Me.ServerListPage.HorizontalScrollbarBarColor = True
        Me.ServerListPage.HorizontalScrollbarHighlightOnWheel = False
        Me.ServerListPage.HorizontalScrollbarSize = 10
        Me.ServerListPage.Location = New System.Drawing.Point(4, 36)
        Me.ServerListPage.Name = "ServerListPage"
        Me.ServerListPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ServerListPage.Size = New System.Drawing.Size(736, 425)
        Me.ServerListPage.TabIndex = 1
        Me.ServerListPage.Text = "伺服器列表"
        Me.ServerListPage.VerticalScrollbarBarColor = True
        Me.ServerListPage.VerticalScrollbarHighlightOnWheel = False
        Me.ServerListPage.VerticalScrollbarSize = 10
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
        Me.ServerListPanel.Size = New System.Drawing.Size(730, 383)
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
        Me.BottomButtons.Location = New System.Drawing.Point(3, 386)
        Me.BottomButtons.Name = "BottomButtons"
        Me.BottomButtons.RowCount = 1
        Me.BottomButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.BottomButtons.Size = New System.Drawing.Size(730, 36)
        Me.BottomButtons.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.Location = New System.Drawing.Point(11, 4)
        Me.Button1.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(343, 28)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "建立伺服器"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.Location = New System.Drawing.Point(375, 4)
        Me.Button2.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(344, 28)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "加入伺服器"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PackServerListPage
        '
        Me.PackServerListPage.BackColor = System.Drawing.Color.Transparent
        Me.PackServerListPage.Controls.Add(Me.ModpackServerListPanel)
        Me.PackServerListPage.Controls.Add(Me.TableLayoutPanel5)
        Me.PackServerListPage.HorizontalScrollbarBarColor = True
        Me.PackServerListPage.HorizontalScrollbarHighlightOnWheel = False
        Me.PackServerListPage.HorizontalScrollbarSize = 10
        Me.PackServerListPage.Location = New System.Drawing.Point(4, 36)
        Me.PackServerListPage.Name = "PackServerListPage"
        Me.PackServerListPage.Padding = New System.Windows.Forms.Padding(3)
        Me.PackServerListPage.Size = New System.Drawing.Size(736, 425)
        Me.PackServerListPage.TabIndex = 7
        Me.PackServerListPage.Text = "模組包伺服器列表"
        Me.PackServerListPage.VerticalScrollbarBarColor = True
        Me.PackServerListPage.VerticalScrollbarHighlightOnWheel = False
        Me.PackServerListPage.VerticalScrollbarSize = 10
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
        Me.ModpackServerListPanel.Size = New System.Drawing.Size(730, 383)
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
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 386)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(730, 36)
        Me.TableLayoutPanel5.TabIndex = 8
        '
        'Button6
        '
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button6.Location = New System.Drawing.Point(11, 4)
        Me.Button6.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(343, 28)
        Me.Button6.TabIndex = 3
        Me.Button6.Text = "建立伺服器"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button7.Location = New System.Drawing.Point(375, 4)
        Me.Button7.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(344, 28)
        Me.Button7.TabIndex = 2
        Me.Button7.Text = "加入伺服器"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'ServerIntergratePage
        '
        Me.ServerIntergratePage.BackColor = System.Drawing.Color.Transparent
        Me.ServerIntergratePage.Controls.Add(Me.SolutionListPanel)
        Me.ServerIntergratePage.Controls.Add(Me.TableLayoutPanel2)
        Me.ServerIntergratePage.HorizontalScrollbarBarColor = True
        Me.ServerIntergratePage.HorizontalScrollbarHighlightOnWheel = False
        Me.ServerIntergratePage.HorizontalScrollbarSize = 10
        Me.ServerIntergratePage.Location = New System.Drawing.Point(4, 36)
        Me.ServerIntergratePage.Name = "ServerIntergratePage"
        Me.ServerIntergratePage.Padding = New System.Windows.Forms.Padding(3)
        Me.ServerIntergratePage.Size = New System.Drawing.Size(736, 425)
        Me.ServerIntergratePage.TabIndex = 2
        Me.ServerIntergratePage.Text = "伺服器整合方案列表"
        Me.ServerIntergratePage.VerticalScrollbarBarColor = True
        Me.ServerIntergratePage.VerticalScrollbarHighlightOnWheel = False
        Me.ServerIntergratePage.VerticalScrollbarSize = 10
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
        Me.SolutionListPanel.Size = New System.Drawing.Size(730, 383)
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
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 386)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(730, 36)
        Me.TableLayoutPanel2.TabIndex = 7
        '
        'Button3
        '
        Me.Button3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button3.Location = New System.Drawing.Point(11, 4)
        Me.Button3.Margin = New System.Windows.Forms.Padding(10, 3, 10, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(708, 28)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "建立整合方案"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ConnectionTabPage
        '
        Me.ConnectionTabPage.Controls.Add(Me.GroupBox2)
        Me.ConnectionTabPage.Controls.Add(Me.GroupBox3)
        Me.ConnectionTabPage.HorizontalScrollbarBarColor = True
        Me.ConnectionTabPage.HorizontalScrollbarHighlightOnWheel = False
        Me.ConnectionTabPage.HorizontalScrollbarSize = 10
        Me.ConnectionTabPage.Location = New System.Drawing.Point(4, 36)
        Me.ConnectionTabPage.Name = "ConnectionTabPage"
        Me.ConnectionTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ConnectionTabPage.Size = New System.Drawing.Size(736, 425)
        Me.ConnectionTabPage.TabIndex = 4
        Me.ConnectionTabPage.Text = "連接"
        Me.ConnectionTabPage.UseVisualStyleBackColor = True
        Me.ConnectionTabPage.VerticalScrollbarBarColor = True
        Me.ConnectionTabPage.VerticalScrollbarHighlightOnWheel = False
        Me.ConnectionTabPage.VerticalScrollbarSize = 10
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.CheckBox1)
        Me.GroupBox2.Controls.Add(Me.UPnPLabel)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(720, 45)
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
        Me.CheckBox1.Location = New System.Drawing.Point(647, 16)
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
        Me.UPnPLabel.Size = New System.Drawing.Size(122, 16)
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
        Me.GroupBox3.Size = New System.Drawing.Size(723, 125)
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
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 19)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(717, 103)
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
        Me.Panel1.Size = New System.Drawing.Size(249, 97)
        Me.Panel1.TabIndex = 17
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Checked = Global.ServerManager.My.MySettings.Default.NoIPPasswordViewChecked
        Me.CheckBox2.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.ServerManager.My.MySettings.Default, "NoIPPasswordViewChecked", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.CheckBox2.Location = New System.Drawing.Point(50, 67)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(75, 20)
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
        Me.Label5.Size = New System.Drawing.Size(44, 16)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "帳號："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 39)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 3, 3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 16)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "密碼："
        '
        'NoIPAccountBox
        '
        '
        '
        '
        Me.NoIPAccountBox.CustomButton.Image = Nothing
        Me.NoIPAccountBox.CustomButton.Location = New System.Drawing.Point(173, 2)
        Me.NoIPAccountBox.CustomButton.Name = ""
        Me.NoIPAccountBox.CustomButton.Size = New System.Drawing.Size(17, 17)
        Me.NoIPAccountBox.CustomButton.Style = MetroFramework.MetroColorStyle.Green
        Me.NoIPAccountBox.CustomButton.TabIndex = 1
        Me.NoIPAccountBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.NoIPAccountBox.CustomButton.UseSelectable = True
        Me.NoIPAccountBox.CustomButton.Visible = False
        Me.NoIPAccountBox.Lines = New String(-1) {}
        Me.NoIPAccountBox.Location = New System.Drawing.Point(50, 6)
        Me.NoIPAccountBox.MaxLength = 32767
        Me.NoIPAccountBox.Name = "NoIPAccountBox"
        Me.NoIPAccountBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.NoIPAccountBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.NoIPAccountBox.SelectedText = ""
        Me.NoIPAccountBox.SelectionLength = 0
        Me.NoIPAccountBox.SelectionStart = 0
        Me.NoIPAccountBox.ShortcutsEnabled = True
        Me.NoIPAccountBox.Size = New System.Drawing.Size(193, 22)
        Me.NoIPAccountBox.TabIndex = 1
        Me.NoIPAccountBox.UseSelectable = True
        Me.NoIPAccountBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.NoIPAccountBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'NoIPPasswordBox
        '
        '
        '
        '
        Me.NoIPPasswordBox.CustomButton.Image = Nothing
        Me.NoIPPasswordBox.CustomButton.Location = New System.Drawing.Point(173, 2)
        Me.NoIPPasswordBox.CustomButton.Name = ""
        Me.NoIPPasswordBox.CustomButton.Size = New System.Drawing.Size(17, 17)
        Me.NoIPPasswordBox.CustomButton.Style = MetroFramework.MetroColorStyle.Green
        Me.NoIPPasswordBox.CustomButton.TabIndex = 1
        Me.NoIPPasswordBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.NoIPPasswordBox.CustomButton.UseSelectable = True
        Me.NoIPPasswordBox.CustomButton.Visible = False
        Me.NoIPPasswordBox.Lines = New String(-1) {}
        Me.NoIPPasswordBox.Location = New System.Drawing.Point(50, 34)
        Me.NoIPPasswordBox.MaxLength = 32767
        Me.NoIPPasswordBox.Name = "NoIPPasswordBox"
        Me.NoIPPasswordBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.NoIPPasswordBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.NoIPPasswordBox.SelectedText = ""
        Me.NoIPPasswordBox.SelectionLength = 0
        Me.NoIPPasswordBox.SelectionStart = 0
        Me.NoIPPasswordBox.ShortcutsEnabled = True
        Me.NoIPPasswordBox.Size = New System.Drawing.Size(193, 22)
        Me.NoIPPasswordBox.TabIndex = 2
        Me.NoIPPasswordBox.UseSelectable = True
        Me.NoIPPasswordBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.NoIPPasswordBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
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
        Me.Panel2.Size = New System.Drawing.Size(456, 97)
        Me.Panel2.TabIndex = 18
        '
        'NoIPUpdateTimeSpanLabel
        '
        Me.NoIPUpdateTimeSpanLabel.AutoSize = True
        Me.NoIPUpdateTimeSpanLabel.Location = New System.Drawing.Point(48, 77)
        Me.NoIPUpdateTimeSpanLabel.Name = "NoIPUpdateTimeSpanLabel"
        Me.NoIPUpdateTimeSpanLabel.Size = New System.Drawing.Size(0, 16)
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
        Me.HostCheckList.Size = New System.Drawing.Size(400, 40)
        Me.HostCheckList.TabIndex = 19
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.Location = New System.Drawing.Point(375, 68)
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
        Me.Label7.Size = New System.Drawing.Size(44, 16)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "主機："
        '
        'SettingTabPage
        '
        Me.SettingTabPage.Controls.Add(Me.SettingTabControl)
        Me.SettingTabPage.HorizontalScrollbarBarColor = True
        Me.SettingTabPage.HorizontalScrollbarHighlightOnWheel = False
        Me.SettingTabPage.HorizontalScrollbarSize = 10
        Me.SettingTabPage.Location = New System.Drawing.Point(4, 36)
        Me.SettingTabPage.Name = "SettingTabPage"
        Me.SettingTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.SettingTabPage.Size = New System.Drawing.Size(736, 425)
        Me.SettingTabPage.TabIndex = 6
        Me.SettingTabPage.Text = "設定"
        Me.SettingTabPage.UseVisualStyleBackColor = True
        Me.SettingTabPage.VerticalScrollbarBarColor = True
        Me.SettingTabPage.VerticalScrollbarHighlightOnWheel = False
        Me.SettingTabPage.VerticalScrollbarSize = 10
        '
        'SettingTabControl
        '
        Me.SettingTabControl.Controls.Add(Me.TabPage4)
        Me.SettingTabControl.Controls.Add(Me.TabPage1)
        Me.SettingTabControl.Controls.Add(Me.TabPage2)
        Me.SettingTabControl.Controls.Add(Me.TabPage3)
        Me.SettingTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SettingTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular
        Me.SettingTabControl.Location = New System.Drawing.Point(3, 3)
        Me.SettingTabControl.Name = "SettingTabControl"
        Me.SettingTabControl.SelectedIndex = 0
        Me.SettingTabControl.Size = New System.Drawing.Size(730, 419)
        Me.SettingTabControl.Style = MetroFramework.MetroColorStyle.Green
        Me.SettingTabControl.TabIndex = 0
        Me.SettingTabControl.UseSelectable = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.GroupBox11)
        Me.TabPage4.Controls.Add(Me.GroupBox6)
        Me.TabPage4.HorizontalScrollbarBarColor = True
        Me.TabPage4.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage4.HorizontalScrollbarSize = 10
        Me.TabPage4.Location = New System.Drawing.Point(4, 38)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(722, 377)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "一般"
        Me.TabPage4.UseVisualStyleBackColor = True
        Me.TabPage4.VerticalScrollbarBarColor = True
        Me.TabPage4.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage4.VerticalScrollbarSize = 10
        '
        'GroupBox11
        '
        Me.GroupBox11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox11.Controls.Add(Me.RadioButton2)
        Me.GroupBox11.Controls.Add(Me.RadioButton1)
        Me.GroupBox11.Location = New System.Drawing.Point(6, 92)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(710, 80)
        Me.GroupBox11.TabIndex = 7
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "最小化方式"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(9, 48)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(134, 20)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "最小化至右側狀態列"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(9, 22)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(110, 20)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "最小化至工作列"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.ComboBox2)
        Me.GroupBox6.Controls.Add(Me.Label22)
        Me.GroupBox6.Controls.Add(Me.ComboBox1)
        Me.GroupBox6.Controls.Add(Me.Label21)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(710, 80)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "自動更新"
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"僅安裝穩定版本", "穩定版本及預覽版本"})
        Me.ComboBox2.Location = New System.Drawing.Point(77, 47)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(200, 24)
        Me.ComboBox2.TabIndex = 6
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 52)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(68, 16)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "更新頻道："
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"自動偵測更新並安裝", "自動偵測更新並跳出提示視窗", "不自動更新"})
        Me.ComboBox1.Location = New System.Drawing.Point(77, 21)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(200, 24)
        Me.ComboBox1.TabIndex = 4
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(6, 26)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(68, 16)
        Me.Label21.TabIndex = 1
        Me.Label21.Text = "更新方式："
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Controls.Add(Me.JavaDefaultBtn)
        Me.TabPage1.Controls.Add(Me.JavaChooseBtn)
        Me.TabPage1.Controls.Add(Me.ArguBox)
        Me.TabPage1.Controls.Add(Me.ArguLabel)
        Me.TabPage1.Controls.Add(Me.JavaVersionLabel)
        Me.TabPage1.HorizontalScrollbarBarColor = True
        Me.TabPage1.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage1.HorizontalScrollbarSize = 10
        Me.TabPage1.Location = New System.Drawing.Point(4, 36)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(722, 379)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Java"
        Me.TabPage1.UseVisualStyleBackColor = True
        Me.TabPage1.VerticalScrollbarBarColor = True
        Me.TabPage1.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage1.VerticalScrollbarSize = 10
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox9, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ServerGroupBox1, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(6, 63)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(710, 164)
        Me.TableLayoutPanel1.TabIndex = 41
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Label23)
        Me.GroupBox9.Controls.Add(Me.Label24)
        Me.GroupBox9.Controls.Add(Me.ModpackServerMemoryMaxBox)
        Me.GroupBox9.Controls.Add(Me.Label25)
        Me.GroupBox9.Controls.Add(Me.ModpackServerMemoryMinBox)
        Me.GroupBox9.Controls.Add(Me.Label26)
        Me.GroupBox9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox9.Location = New System.Drawing.Point(3, 85)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(349, 76)
        Me.GroupBox9.TabIndex = 33
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "模組包伺服器"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 26)
        Me.Label23.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(92, 16)
        Me.Label23.TabIndex = 25
        Me.Label23.Text = "記憶體最大值："
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(179, 26)
        Me.Label24.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(27, 16)
        Me.Label24.TabIndex = 27
        Me.Label24.Text = "MB"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ModpackServerMemoryMaxBox
        '
        Me.ModpackServerMemoryMaxBox.Location = New System.Drawing.Point(98, 21)
        Me.ModpackServerMemoryMaxBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.ModpackServerMemoryMaxBox.Name = "ModpackServerMemoryMaxBox"
        Me.ModpackServerMemoryMaxBox.Size = New System.Drawing.Size(78, 23)
        Me.ModpackServerMemoryMaxBox.TabIndex = 26
        Me.ModpackServerMemoryMaxBox.Tag = ""
        Me.ModpackServerMemoryMaxBox.Value = New Decimal(New Integer() {4096, 0, 0, 0})
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(179, 53)
        Me.Label25.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(27, 16)
        Me.Label25.TabIndex = 30
        Me.Label25.Text = "MB"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ModpackServerMemoryMinBox
        '
        Me.ModpackServerMemoryMinBox.Location = New System.Drawing.Point(98, 49)
        Me.ModpackServerMemoryMinBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.ModpackServerMemoryMinBox.Name = "ModpackServerMemoryMinBox"
        Me.ModpackServerMemoryMinBox.Size = New System.Drawing.Size(78, 23)
        Me.ModpackServerMemoryMinBox.TabIndex = 29
        Me.ModpackServerMemoryMinBox.Tag = ""
        Me.ModpackServerMemoryMinBox.Value = New Decimal(New Integer() {4096, 0, 0, 0})
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(6, 53)
        Me.Label26.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(92, 16)
        Me.Label26.TabIndex = 28
        Me.Label26.Text = "記憶體最小值："
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.GroupBox1.Location = New System.Drawing.Point(358, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(349, 76)
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
        Me.Label1.Size = New System.Drawing.Size(92, 16)
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
        Me.Label2.Size = New System.Drawing.Size(27, 16)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "MB"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BungeeMemoryMaxBox
        '
        Me.BungeeMemoryMaxBox.Location = New System.Drawing.Point(98, 21)
        Me.BungeeMemoryMaxBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.BungeeMemoryMaxBox.Name = "BungeeMemoryMaxBox"
        Me.BungeeMemoryMaxBox.Size = New System.Drawing.Size(78, 23)
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
        Me.Label3.Size = New System.Drawing.Size(27, 16)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "MB"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BungeeMemoryMinBox
        '
        Me.BungeeMemoryMinBox.Location = New System.Drawing.Point(98, 49)
        Me.BungeeMemoryMinBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.BungeeMemoryMinBox.Name = "BungeeMemoryMinBox"
        Me.BungeeMemoryMinBox.Size = New System.Drawing.Size(78, 23)
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
        Me.Label4.Size = New System.Drawing.Size(92, 16)
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
        Me.ServerGroupBox1.Size = New System.Drawing.Size(349, 76)
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
        Me.Label11.Size = New System.Drawing.Size(92, 16)
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
        Me.Label12.Size = New System.Drawing.Size(27, 16)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "MB"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerMemoryMaxBox
        '
        Me.ServerMemoryMaxBox.Location = New System.Drawing.Point(98, 21)
        Me.ServerMemoryMaxBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.ServerMemoryMaxBox.Name = "ServerMemoryMaxBox"
        Me.ServerMemoryMaxBox.Size = New System.Drawing.Size(78, 23)
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
        Me.Label14.Size = New System.Drawing.Size(27, 16)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "MB"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerMemoryMinBox
        '
        Me.ServerMemoryMinBox.Location = New System.Drawing.Point(98, 49)
        Me.ServerMemoryMinBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.ServerMemoryMinBox.Name = "ServerMemoryMinBox"
        Me.ServerMemoryMinBox.Size = New System.Drawing.Size(78, 23)
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
        Me.Label13.Size = New System.Drawing.Size(92, 16)
        Me.Label13.TabIndex = 28
        Me.Label13.Text = "記憶體最小值："
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'JavaDefaultBtn
        '
        Me.JavaDefaultBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.JavaDefaultBtn.Enabled = False
        Me.JavaDefaultBtn.Location = New System.Drawing.Point(598, 6)
        Me.JavaDefaultBtn.Name = "JavaDefaultBtn"
        Me.JavaDefaultBtn.Size = New System.Drawing.Size(56, 23)
        Me.JavaDefaultBtn.TabIndex = 40
        Me.JavaDefaultBtn.Text = "預設"
        Me.JavaDefaultBtn.UseVisualStyleBackColor = True
        '
        'JavaChooseBtn
        '
        Me.JavaChooseBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.JavaChooseBtn.Location = New System.Drawing.Point(660, 6)
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
        '
        '
        '
        Me.ArguBox.CustomButton.Image = Nothing
        Me.ArguBox.CustomButton.Location = New System.Drawing.Point(597, 2)
        Me.ArguBox.CustomButton.Name = ""
        Me.ArguBox.CustomButton.Size = New System.Drawing.Size(17, 17)
        Me.ArguBox.CustomButton.Style = MetroFramework.MetroColorStyle.Green
        Me.ArguBox.CustomButton.TabIndex = 1
        Me.ArguBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.ArguBox.CustomButton.UseSelectable = True
        Me.ArguBox.CustomButton.Visible = False
        Me.ArguBox.Lines = New String(-1) {}
        Me.ArguBox.Location = New System.Drawing.Point(99, 35)
        Me.ArguBox.MaxLength = 32767
        Me.ArguBox.Name = "ArguBox"
        Me.ArguBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.ArguBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.ArguBox.SelectedText = ""
        Me.ArguBox.SelectionLength = 0
        Me.ArguBox.SelectionStart = 0
        Me.ArguBox.ShortcutsEnabled = True
        Me.ArguBox.Size = New System.Drawing.Size(617, 22)
        Me.ArguBox.TabIndex = 38
        Me.ArguBox.UseSelectable = True
        Me.ArguBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.ArguBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'ArguLabel
        '
        Me.ArguLabel.AutoSize = True
        Me.ArguLabel.Location = New System.Drawing.Point(7, 39)
        Me.ArguLabel.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.ArguLabel.Name = "ArguLabel"
        Me.ArguLabel.Size = New System.Drawing.Size(96, 16)
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
        Me.JavaVersionLabel.Size = New System.Drawing.Size(117, 16)
        Me.JavaVersionLabel.TabIndex = 36
        Me.JavaVersionLabel.Text = "Java 版本：取得中..."
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.CheckBox3)
        Me.TabPage2.Controls.Add(Me.SnapshotCheckBox)
        Me.TabPage2.HorizontalScrollbarBarColor = True
        Me.TabPage2.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage2.HorizontalScrollbarSize = 10
        Me.TabPage2.Location = New System.Drawing.Point(4, 36)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(722, 379)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "伺服器"
        Me.TabPage2.UseVisualStyleBackColor = True
        Me.TabPage2.VerticalScrollbarBarColor = True
        Me.TabPage2.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage2.VerticalScrollbarSize = 10
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(6, 28)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(168, 20)
        Me.CheckBox3.TabIndex = 33
        Me.CheckBox3.Text = "是否可選擇安裝舊版Forge"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'SnapshotCheckBox
        '
        Me.SnapshotCheckBox.AutoSize = True
        Me.SnapshotCheckBox.Location = New System.Drawing.Point(6, 6)
        Me.SnapshotCheckBox.Name = "SnapshotCheckBox"
        Me.SnapshotCheckBox.Size = New System.Drawing.Size(171, 20)
        Me.SnapshotCheckBox.TabIndex = 32
        Me.SnapshotCheckBox.Text = "在列表內顯示原版快照版本"
        Me.SnapshotCheckBox.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox10)
        Me.TabPage3.Controls.Add(Me.GitGroupBox)
        Me.TabPage3.HorizontalScrollbarBarColor = True
        Me.TabPage3.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage3.HorizontalScrollbarSize = 10
        Me.TabPage3.Location = New System.Drawing.Point(4, 36)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(722, 379)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "外部工具"
        Me.TabPage3.UseVisualStyleBackColor = True
        Me.TabPage3.VerticalScrollbarBarColor = True
        Me.TabPage3.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage3.VerticalScrollbarSize = 10
        '
        'GroupBox10
        '
        Me.GroupBox10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox10.Controls.Add(Me.PHPBrowseButton)
        Me.GroupBox10.Controls.Add(Me.PHPPathBox)
        Me.GroupBox10.Controls.Add(Me.Label27)
        Me.GroupBox10.Controls.Add(Me.Label28)
        Me.GroupBox10.Location = New System.Drawing.Point(6, 80)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(750, 68)
        Me.GroupBox10.TabIndex = 4
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "PHP"
        '
        'PHPBrowseButton
        '
        Me.PHPBrowseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PHPBrowseButton.Font = New System.Drawing.Font("新細明體", 11.0!)
        Me.PHPBrowseButton.Location = New System.Drawing.Point(717, 37)
        Me.PHPBrowseButton.Name = "PHPBrowseButton"
        Me.PHPBrowseButton.Size = New System.Drawing.Size(27, 22)
        Me.PHPBrowseButton.TabIndex = 3
        Me.PHPBrowseButton.Text = "..."
        Me.PHPBrowseButton.UseVisualStyleBackColor = True
        '
        'PHPPathBox
        '
        Me.PHPPathBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.PHPPathBox.CustomButton.Image = Nothing
        Me.PHPPathBox.CustomButton.Location = New System.Drawing.Point(644, 2)
        Me.PHPPathBox.CustomButton.Name = ""
        Me.PHPPathBox.CustomButton.Size = New System.Drawing.Size(17, 17)
        Me.PHPPathBox.CustomButton.Style = MetroFramework.MetroColorStyle.Green
        Me.PHPPathBox.CustomButton.TabIndex = 1
        Me.PHPPathBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.PHPPathBox.CustomButton.UseSelectable = True
        Me.PHPPathBox.CustomButton.Visible = False
        Me.PHPPathBox.Lines = New String(-1) {}
        Me.PHPPathBox.Location = New System.Drawing.Point(47, 37)
        Me.PHPPathBox.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.PHPPathBox.MaxLength = 32767
        Me.PHPPathBox.Name = "PHPPathBox"
        Me.PHPPathBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.PHPPathBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.PHPPathBox.SelectedText = ""
        Me.PHPPathBox.SelectionLength = 0
        Me.PHPPathBox.SelectionStart = 0
        Me.PHPPathBox.ShortcutsEnabled = True
        Me.PHPPathBox.Size = New System.Drawing.Size(664, 22)
        Me.PHPPathBox.TabIndex = 2
        Me.PHPPathBox.UseSelectable = True
        Me.PHPPathBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.PHPPathBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(6, 42)
        Me.Label27.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(44, 16)
        Me.Label27.TabIndex = 1
        Me.Label27.Text = "路徑："
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 18)
        Me.Label28.Margin = New System.Windows.Forms.Padding(3, 0, 3, 4)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(237, 16)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "運行 PocketMine-MP 伺服器所需的軟體。"
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
        '
        '
        '
        Me.GitBashPathBox.CustomButton.Image = Nothing
        Me.GitBashPathBox.CustomButton.Location = New System.Drawing.Point(644, 2)
        Me.GitBashPathBox.CustomButton.Name = ""
        Me.GitBashPathBox.CustomButton.Size = New System.Drawing.Size(17, 17)
        Me.GitBashPathBox.CustomButton.Style = MetroFramework.MetroColorStyle.Green
        Me.GitBashPathBox.CustomButton.TabIndex = 1
        Me.GitBashPathBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.GitBashPathBox.CustomButton.UseSelectable = True
        Me.GitBashPathBox.CustomButton.Visible = False
        Me.GitBashPathBox.Lines = New String(-1) {}
        Me.GitBashPathBox.Location = New System.Drawing.Point(47, 37)
        Me.GitBashPathBox.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.GitBashPathBox.MaxLength = 32767
        Me.GitBashPathBox.Name = "GitBashPathBox"
        Me.GitBashPathBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.GitBashPathBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.GitBashPathBox.SelectedText = ""
        Me.GitBashPathBox.SelectionLength = 0
        Me.GitBashPathBox.SelectionStart = 0
        Me.GitBashPathBox.ShortcutsEnabled = True
        Me.GitBashPathBox.Size = New System.Drawing.Size(664, 22)
        Me.GitBashPathBox.TabIndex = 2
        Me.GitBashPathBox.UseSelectable = True
        Me.GitBashPathBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.GitBashPathBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 42)
        Me.Label16.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(44, 16)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "路徑："
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 18)
        Me.Label15.Margin = New System.Windows.Forms.Padding(3, 0, 3, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(200, 16)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "用於建置需手動建置的伺服器軟體。"
        '
        'AboutPage
        '
        Me.AboutPage.Controls.Add(Me.TableLayoutPanel4)
        Me.AboutPage.HorizontalScrollbarBarColor = True
        Me.AboutPage.HorizontalScrollbarHighlightOnWheel = False
        Me.AboutPage.HorizontalScrollbarSize = 10
        Me.AboutPage.Location = New System.Drawing.Point(4, 38)
        Me.AboutPage.Name = "AboutPage"
        Me.AboutPage.Padding = New System.Windows.Forms.Padding(3)
        Me.AboutPage.Size = New System.Drawing.Size(736, 423)
        Me.AboutPage.TabIndex = 5
        Me.AboutPage.Text = "關於"
        Me.AboutPage.UseVisualStyleBackColor = True
        Me.AboutPage.VerticalScrollbarBarColor = True
        Me.AboutPage.VerticalScrollbarHighlightOnWheel = False
        Me.AboutPage.VerticalScrollbarSize = 10
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
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(730, 417)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.ServerSoftwareLinkList)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox5.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(368, 3)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(359, 411)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "各伺服器軟體官網(按兩下即可開啟)"
        '
        'ServerSoftwareLinkList
        '
        Me.ServerSoftwareLinkList.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.ServerSoftwareLinkList.AutoArrange = False
        Me.ServerSoftwareLinkList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ServerSoftwareLinkList.Font = New System.Drawing.Font("微軟正黑體", 10.5!)
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
        ListViewItem12.StateImageIndex = 13
        ListViewItem12.Tag = "http://catserver.moe/"
        ListViewItem13.Checked = True
        ListViewItem13.StateImageIndex = 7
        ListViewItem13.Tag = "https://nukkitx.com/"
        ListViewItem14.Checked = True
        ListViewItem14.StateImageIndex = 12
        ListViewItem14.Tag = "https://pmmp.io/"
        Me.ServerSoftwareLinkList.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9, ListViewItem10, ListViewItem11, ListViewItem12, ListViewItem13, ListViewItem14})
        Me.ServerSoftwareLinkList.Location = New System.Drawing.Point(3, 19)
        Me.ServerSoftwareLinkList.MultiSelect = False
        Me.ServerSoftwareLinkList.Name = "ServerSoftwareLinkList"
        Me.ServerSoftwareLinkList.Size = New System.Drawing.Size(353, 389)
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
        Me.ServerSoftwareImageList.Images.SetKeyName(12, "PocketMine.jpg")
        Me.ServerSoftwareImageList.Images.SetKeyName(13, "catServer.jpg")
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.LinkLabel2)
        Me.Panel3.Controls.Add(Me.LinkLabel1)
        Me.Panel3.Controls.Add(Me.GroupBox4)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(359, 411)
        Me.Panel3.TabIndex = 1
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.LinkLabel2.Location = New System.Drawing.Point(219, 137)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(51, 16)
        Me.LinkLabel2.TabIndex = 6
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Discord"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(160, 137)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(56, 16)
        Me.LinkLabel1.TabIndex = 5
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "巴哈網址"
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
        Me.GroupBox4.Size = New System.Drawing.Size(352, 244)
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
        Me.LibraryListBox.Items.AddRange(New Object() {"Newtonsoft.Json (用於解析JSON)", "HtmlAgilityPack (用於提取HTML 元素)", "HtmlRenderer (用於顯示簡單HTML 網頁)", "NATUPnP (提供UPnP 支援)", "NoIP.DDNS (提供No-IP 連接支援)", "YamlDotNet (用於解析YAML)", "DropDownControls (用於顯示群組項目)", "NBT_Library (用於解析NBT檔案)", "MetroFramework (提供圖形介面)"})
        Me.LibraryListBox.Location = New System.Drawing.Point(3, 19)
        Me.LibraryListBox.Name = "LibraryListBox"
        Me.LibraryListBox.Size = New System.Drawing.Size(346, 222)
        Me.LibraryListBox.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoEllipsis = True
        Me.Label10.Font = New System.Drawing.Font("微軟正黑體", 11.0!)
        Me.Label10.Location = New System.Drawing.Point(159, 57)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(213, 81)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "作者：米茶、冰霜、asd7766zxc"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("微軟正黑體", 11.0!)
        Me.Label9.Location = New System.Drawing.Point(159, 34)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(159, 19)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "版本：1.7 LTS Patch 3"
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
        Me.NotifyIcon1.Text = "Minecraft 伺服器管理員"
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
        'MetroStyleManager1
        '
        Me.MetroStyleManager1.Owner = Me
        Me.MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Green
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'Manager
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(784, 515)
        Me.Controls.Add(Me.MainTabControl)
        Me.DisplayHeader = False
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Manager"
        Me.Padding = New System.Windows.Forms.Padding(20, 30, 20, 20)
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow
        Me.Style = MetroFramework.MetroColorStyle.Green
        Me.Text = "Minecraft 伺服器管理員"
        Me.MainTabControl.ResumeLayout(False)
        Me.MainPage.ResumeLayout(False)
        Me.MainPage.PerformLayout()
        Me.MainPanel.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.MemoryGroupBox.ResumeLayout(False)
        Me.MemoryGroupBox.PerformLayout()
        Me.NetworkGroupBox.ResumeLayout(False)
        Me.NetworkGroupBox.PerformLayout()
        Me.ExternalIPContextMenu.ResumeLayout(False)
        Me.InternalIPContextMenu.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.VListLoadingBox.ResumeLayout(False)
        Me.ServerListPage.ResumeLayout(False)
        Me.ServerListPage.PerformLayout()
        Me.BottomButtons.ResumeLayout(False)
        Me.PackServerListPage.ResumeLayout(False)
        Me.PackServerListPage.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.ServerIntergratePage.ResumeLayout(False)
        Me.ServerIntergratePage.PerformLayout()
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
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        CType(Me.ModpackServerMemoryMaxBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModpackServerMemoryMinBox, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
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
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ServerListPanel As TableLayoutPanel
    Friend WithEvents BottomButtons As TableLayoutPanel
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents MainPanel As TableLayoutPanel
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
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents NoIPPasswordBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents NoIPAccountBox As MetroFramework.Controls.MetroTextBox
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
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents UPnPLabel As Label
    Friend WithEvents JavaDefaultBtn As Button
    Friend WithEvents JavaChooseBtn As Button
    Friend WithEvents ArguBox As MetroFramework.Controls.MetroTextBox
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
    Friend WithEvents GitGroupBox As GroupBox
    Friend WithEvents GitBashBrowseButton As Button
    Friend WithEvents GitBashPathBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents CauldronLoadingLabel As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents ModpackServerListPanel As TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents ContigoLoadingLabel As Label
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
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label22 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label21 As Label
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents ModpackServerMemoryMaxBox As NumericUpDown
    Friend WithEvents Label25 As Label
    Friend WithEvents ModpackServerMemoryMinBox As NumericUpDown
    Friend WithEvents Label26 As Label
    Friend WithEvents GroupBox10 As GroupBox
    Friend WithEvents PHPBrowseButton As Button
    Friend WithEvents PHPPathBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents Label27 As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents PocketMineLoadingLabel As Label
    Friend WithEvents MainTabControl As MetroFramework.Controls.MetroTabControl
    Friend WithEvents MainPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents ServerListPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents ServerIntergratePage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents ConnectionTabPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents AboutPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents SettingTabPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents SettingTabControl As MetroFramework.Controls.MetroTabControl
    Friend WithEvents TabPage1 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents TabPage2 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents TabPage3 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents PackServerListPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents TabPage4 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
    Friend WithEvents CatServerLoadingLabel As Label
    Friend WithEvents KettleLoadingLabel As Label
    Friend WithEvents GroupBox11 As GroupBox
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
End Class
