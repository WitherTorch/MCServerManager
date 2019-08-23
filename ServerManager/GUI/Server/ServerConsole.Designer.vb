<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ServerConsole
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
        Dim Panel1 As System.Windows.Forms.Panel
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerConsole))
        Me.MainTabControl = New MetroFramework.Controls.MetroTabControl()
        Me.MainTabPage = New MetroFramework.Controls.MetroTabPage()
        Me.ForceCloseButton = New System.Windows.Forms.Button()
        Me.RestartButton = New System.Windows.Forms.Button()
        Me.PlayerGroupBox = New System.Windows.Forms.GroupBox()
        Me.PlayerListBox = New System.Windows.Forms.ListBox()
        Me.UserContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.封禁ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.踢出ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.設定OPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.解除OPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.更新列表用listToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SystemGroupBox = New System.Windows.Forms.GroupBox()
        Me.IDLabel = New System.Windows.Forms.Label()
        Me.MemoryLabel = New System.Windows.Forms.Label()
        Me.ServerStatusLabel = New System.Windows.Forms.Label()
        Me.SettingTabPage = New MetroFramework.Controls.MetroTabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CMDButton = New System.Windows.Forms.Button()
        Me.TaskGroupBox = New System.Windows.Forms.GroupBox()
        Me.TaskListBox = New System.Windows.Forms.CheckedListBox()
        Me.TaskControlPanel = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.RemoveTaskButton = New System.Windows.Forms.Button()
        Me.EditTaskButton = New System.Windows.Forms.Button()
        Me.AddTaskButton = New System.Windows.Forms.Button()
        Me.NotifyGroupBox = New System.Windows.Forms.GroupBox()
        Me.NotifyChooseListBox = New System.Windows.Forms.CheckedListBox()
        Me.DataTabPage = New MetroFramework.Controls.MetroTabPage()
        Me.DataListView = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CommandTextBox = New MetroFramework.Controls.MetroTextBox()
        Me.CloseCheckBox = New System.Windows.Forms.CheckBox()
        Me.StopLoadingCheckBox = New System.Windows.Forms.CheckBox()
        Me.TaskTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ListBoxTImer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ContextTaskMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
        Panel1 = New System.Windows.Forms.Panel()
        Me.MainTabControl.SuspendLayout()
        Me.MainTabPage.SuspendLayout()
        Me.PlayerGroupBox.SuspendLayout()
        Me.UserContextMenu.SuspendLayout()
        Me.SystemGroupBox.SuspendLayout()
        Me.SettingTabPage.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TaskGroupBox.SuspendLayout()
        Me.TaskControlPanel.SuspendLayout()
        Me.NotifyGroupBox.SuspendLayout()
        Me.DataTabPage.SuspendLayout()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel1.Location = New System.Drawing.Point(0, 386)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(752, 3)
        Panel1.TabIndex = 15
        '
        'MainTabControl
        '
        Me.MainTabControl.Controls.Add(Me.MainTabPage)
        Me.MainTabControl.Controls.Add(Me.SettingTabPage)
        Me.MainTabControl.Controls.Add(Me.DataTabPage)
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular
        Me.MainTabControl.Location = New System.Drawing.Point(20, 30)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(760, 456)
        Me.MainTabControl.Style = MetroFramework.MetroColorStyle.Green
        Me.MainTabControl.TabIndex = 0
        Me.MainTabControl.UseSelectable = True
        '
        'MainTabPage
        '
        Me.MainTabPage.Controls.Add(Me.ForceCloseButton)
        Me.MainTabPage.Controls.Add(Me.RestartButton)
        Me.MainTabPage.Controls.Add(Me.PlayerGroupBox)
        Me.MainTabPage.Controls.Add(Me.SystemGroupBox)
        Me.MainTabPage.Controls.Add(Me.ServerStatusLabel)
        Me.MainTabPage.HorizontalScrollbarBarColor = True
        Me.MainTabPage.HorizontalScrollbarHighlightOnWheel = False
        Me.MainTabPage.HorizontalScrollbarSize = 10
        Me.MainTabPage.Location = New System.Drawing.Point(4, 38)
        Me.MainTabPage.Name = "MainTabPage"
        Me.MainTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.MainTabPage.Size = New System.Drawing.Size(752, 414)
        Me.MainTabPage.TabIndex = 0
        Me.MainTabPage.Text = "伺服器資訊"
        Me.MainTabPage.UseVisualStyleBackColor = True
        Me.MainTabPage.VerticalScrollbarBarColor = True
        Me.MainTabPage.VerticalScrollbarHighlightOnWheel = False
        Me.MainTabPage.VerticalScrollbarSize = 10
        '
        'ForceCloseButton
        '
        Me.ForceCloseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ForceCloseButton.Location = New System.Drawing.Point(587, 6)
        Me.ForceCloseButton.Name = "ForceCloseButton"
        Me.ForceCloseButton.Size = New System.Drawing.Size(84, 22)
        Me.ForceCloseButton.TabIndex = 38
        Me.ForceCloseButton.Text = "強制關閉"
        Me.ForceCloseButton.UseVisualStyleBackColor = True
        '
        'RestartButton
        '
        Me.RestartButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RestartButton.Location = New System.Drawing.Point(677, 6)
        Me.RestartButton.Name = "RestartButton"
        Me.RestartButton.Size = New System.Drawing.Size(61, 22)
        Me.RestartButton.TabIndex = 37
        Me.RestartButton.Text = "重啟"
        Me.RestartButton.UseVisualStyleBackColor = True
        '
        'PlayerGroupBox
        '
        Me.PlayerGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PlayerGroupBox.Controls.Add(Me.PlayerListBox)
        Me.PlayerGroupBox.Location = New System.Drawing.Point(8, 108)
        Me.PlayerGroupBox.Name = "PlayerGroupBox"
        Me.PlayerGroupBox.Size = New System.Drawing.Size(736, 307)
        Me.PlayerGroupBox.TabIndex = 36
        Me.PlayerGroupBox.TabStop = False
        Me.PlayerGroupBox.Text = "玩家"
        '
        'PlayerListBox
        '
        Me.PlayerListBox.ContextMenuStrip = Me.UserContextMenu
        Me.PlayerListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PlayerListBox.FormattingEnabled = True
        Me.PlayerListBox.ItemHeight = 16
        Me.PlayerListBox.Location = New System.Drawing.Point(3, 19)
        Me.PlayerListBox.Name = "PlayerListBox"
        Me.PlayerListBox.Size = New System.Drawing.Size(730, 285)
        Me.PlayerListBox.TabIndex = 0
        '
        'UserContextMenu
        '
        Me.UserContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.封禁ToolStripMenuItem, Me.踢出ToolStripMenuItem, Me.ToolStripSeparator1, Me.設定OPToolStripMenuItem, Me.解除OPToolStripMenuItem, Me.ToolStripSeparator2, Me.更新列表用listToolStripMenuItem})
        Me.UserContextMenu.Name = "UserContextMenu"
        Me.UserContextMenu.Size = New System.Drawing.Size(125, 126)
        '
        '封禁ToolStripMenuItem
        '
        Me.封禁ToolStripMenuItem.Name = "封禁ToolStripMenuItem"
        Me.封禁ToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.封禁ToolStripMenuItem.Text = "封禁"
        '
        '踢出ToolStripMenuItem
        '
        Me.踢出ToolStripMenuItem.Name = "踢出ToolStripMenuItem"
        Me.踢出ToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.踢出ToolStripMenuItem.Text = "踢出"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(121, 6)
        '
        '設定OPToolStripMenuItem
        '
        Me.設定OPToolStripMenuItem.Name = "設定OPToolStripMenuItem"
        Me.設定OPToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.設定OPToolStripMenuItem.Text = "設定OP"
        '
        '解除OPToolStripMenuItem
        '
        Me.解除OPToolStripMenuItem.Name = "解除OPToolStripMenuItem"
        Me.解除OPToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.解除OPToolStripMenuItem.Text = "解除OP"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(121, 6)
        '
        '更新列表用listToolStripMenuItem
        '
        Me.更新列表用listToolStripMenuItem.Name = "更新列表用listToolStripMenuItem"
        Me.更新列表用listToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.更新列表用listToolStripMenuItem.Text = "更新列表"
        '
        'SystemGroupBox
        '
        Me.SystemGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SystemGroupBox.Controls.Add(Me.IDLabel)
        Me.SystemGroupBox.Controls.Add(Me.MemoryLabel)
        Me.SystemGroupBox.Location = New System.Drawing.Point(8, 33)
        Me.SystemGroupBox.Name = "SystemGroupBox"
        Me.SystemGroupBox.Size = New System.Drawing.Size(736, 69)
        Me.SystemGroupBox.TabIndex = 31
        Me.SystemGroupBox.TabStop = False
        Me.SystemGroupBox.Text = "系統"
        '
        'IDLabel
        '
        Me.IDLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IDLabel.Location = New System.Drawing.Point(6, 39)
        Me.IDLabel.Name = "IDLabel"
        Me.IDLabel.Size = New System.Drawing.Size(724, 21)
        Me.IDLabel.TabIndex = 4
        Me.IDLabel.Text = "處理序ID："
        Me.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MemoryLabel
        '
        Me.MemoryLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MemoryLabel.Location = New System.Drawing.Point(6, 18)
        Me.MemoryLabel.Name = "MemoryLabel"
        Me.MemoryLabel.Size = New System.Drawing.Size(724, 21)
        Me.MemoryLabel.TabIndex = 3
        Me.MemoryLabel.Text = "占用記憶體："
        Me.MemoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerStatusLabel
        '
        Me.ServerStatusLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerStatusLabel.Location = New System.Drawing.Point(6, 6)
        Me.ServerStatusLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.ServerStatusLabel.Name = "ServerStatusLabel"
        Me.ServerStatusLabel.Size = New System.Drawing.Size(575, 21)
        Me.ServerStatusLabel.TabIndex = 29
        Me.ServerStatusLabel.Text = "伺服器狀態：關閉"
        Me.ServerStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SettingTabPage
        '
        Me.SettingTabPage.AutoScroll = True
        Me.SettingTabPage.Controls.Add(Me.GroupBox1)
        Me.SettingTabPage.Controls.Add(Me.TaskGroupBox)
        Me.SettingTabPage.Controls.Add(Me.NotifyGroupBox)
        Me.SettingTabPage.HorizontalScrollbar = True
        Me.SettingTabPage.HorizontalScrollbarBarColor = True
        Me.SettingTabPage.HorizontalScrollbarHighlightOnWheel = False
        Me.SettingTabPage.HorizontalScrollbarSize = 10
        Me.SettingTabPage.Location = New System.Drawing.Point(4, 38)
        Me.SettingTabPage.Name = "SettingTabPage"
        Me.SettingTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.SettingTabPage.Size = New System.Drawing.Size(752, 414)
        Me.SettingTabPage.TabIndex = 2
        Me.SettingTabPage.Text = "伺服器操作"
        Me.SettingTabPage.UseVisualStyleBackColor = True
        Me.SettingTabPage.VerticalScrollbar = True
        Me.SettingTabPage.VerticalScrollbarBarColor = True
        Me.SettingTabPage.VerticalScrollbarHighlightOnWheel = False
        Me.SettingTabPage.VerticalScrollbarSize = 10
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.CMDButton)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 239)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(736, 51)
        Me.GroupBox1.TabIndex = 36
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "輔助功能"
        '
        'CMDButton
        '
        Me.CMDButton.Location = New System.Drawing.Point(6, 21)
        Me.CMDButton.Name = "CMDButton"
        Me.CMDButton.Size = New System.Drawing.Size(125, 23)
        Me.CMDButton.TabIndex = 0
        Me.CMDButton.Text = "開啟輔助CMD視窗"
        Me.CMDButton.UseVisualStyleBackColor = True
        '
        'TaskGroupBox
        '
        Me.TaskGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskGroupBox.Controls.Add(Me.TaskListBox)
        Me.TaskGroupBox.Controls.Add(Me.TaskControlPanel)
        Me.TaskGroupBox.Location = New System.Drawing.Point(8, 96)
        Me.TaskGroupBox.Name = "TaskGroupBox"
        Me.TaskGroupBox.Size = New System.Drawing.Size(736, 137)
        Me.TaskGroupBox.TabIndex = 35
        Me.TaskGroupBox.TabStop = False
        Me.TaskGroupBox.Text = "排定工作"
        '
        'TaskListBox
        '
        Me.TaskListBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskListBox.CheckOnClick = True
        Me.TaskListBox.FormattingEnabled = True
        Me.TaskListBox.Location = New System.Drawing.Point(6, 14)
        Me.TaskListBox.Name = "TaskListBox"
        Me.TaskListBox.Size = New System.Drawing.Size(632, 106)
        Me.TaskListBox.TabIndex = 1
        '
        'TaskControlPanel
        '
        Me.TaskControlPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskControlPanel.Controls.Add(Me.Button2)
        Me.TaskControlPanel.Controls.Add(Me.Button1)
        Me.TaskControlPanel.Controls.Add(Me.RemoveTaskButton)
        Me.TaskControlPanel.Controls.Add(Me.EditTaskButton)
        Me.TaskControlPanel.Controls.Add(Me.AddTaskButton)
        Me.TaskControlPanel.Location = New System.Drawing.Point(644, 14)
        Me.TaskControlPanel.Name = "TaskControlPanel"
        Me.TaskControlPanel.Size = New System.Drawing.Size(89, 117)
        Me.TaskControlPanel.TabIndex = 2
        '
        'Button2
        '
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button2.Location = New System.Drawing.Point(0, 92)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(89, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "匯出工作..."
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button1.Location = New System.Drawing.Point(0, 69)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(89, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "匯入工作..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'RemoveTaskButton
        '
        Me.RemoveTaskButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.RemoveTaskButton.Location = New System.Drawing.Point(0, 46)
        Me.RemoveTaskButton.Name = "RemoveTaskButton"
        Me.RemoveTaskButton.Size = New System.Drawing.Size(89, 23)
        Me.RemoveTaskButton.TabIndex = 2
        Me.RemoveTaskButton.Text = "移除工作..."
        Me.RemoveTaskButton.UseVisualStyleBackColor = True
        '
        'EditTaskButton
        '
        Me.EditTaskButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.EditTaskButton.Location = New System.Drawing.Point(0, 23)
        Me.EditTaskButton.Name = "EditTaskButton"
        Me.EditTaskButton.Size = New System.Drawing.Size(89, 23)
        Me.EditTaskButton.TabIndex = 1
        Me.EditTaskButton.Text = "修改工作..."
        Me.EditTaskButton.UseVisualStyleBackColor = True
        '
        'AddTaskButton
        '
        Me.AddTaskButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.AddTaskButton.Location = New System.Drawing.Point(0, 0)
        Me.AddTaskButton.Name = "AddTaskButton"
        Me.AddTaskButton.Size = New System.Drawing.Size(89, 23)
        Me.AddTaskButton.TabIndex = 0
        Me.AddTaskButton.Text = "新增工作..."
        Me.AddTaskButton.UseVisualStyleBackColor = True
        '
        'NotifyGroupBox
        '
        Me.NotifyGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NotifyGroupBox.Controls.Add(Me.NotifyChooseListBox)
        Me.NotifyGroupBox.Location = New System.Drawing.Point(8, 6)
        Me.NotifyGroupBox.Name = "NotifyGroupBox"
        Me.NotifyGroupBox.Size = New System.Drawing.Size(736, 84)
        Me.NotifyGroupBox.TabIndex = 34
        Me.NotifyGroupBox.TabStop = False
        Me.NotifyGroupBox.Text = "通知"
        '
        'NotifyChooseListBox
        '
        Me.NotifyChooseListBox.CheckOnClick = True
        Me.NotifyChooseListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NotifyChooseListBox.FormattingEnabled = True
        Me.NotifyChooseListBox.Items.AddRange(New Object() {"玩家登入", "玩家登出", "伺服器發出警告訊息", "伺服器發出錯誤訊息"})
        Me.NotifyChooseListBox.Location = New System.Drawing.Point(3, 19)
        Me.NotifyChooseListBox.Name = "NotifyChooseListBox"
        Me.NotifyChooseListBox.Size = New System.Drawing.Size(730, 62)
        Me.NotifyChooseListBox.TabIndex = 0
        '
        'DataTabPage
        '
        Me.DataTabPage.Controls.Add(Me.DataListView)
        Me.DataTabPage.Controls.Add(Panel1)
        Me.DataTabPage.Controls.Add(Me.CommandTextBox)
        Me.DataTabPage.HorizontalScrollbarBarColor = False
        Me.DataTabPage.HorizontalScrollbarHighlightOnWheel = False
        Me.DataTabPage.HorizontalScrollbarSize = 0
        Me.DataTabPage.Location = New System.Drawing.Point(4, 38)
        Me.DataTabPage.Name = "DataTabPage"
        Me.DataTabPage.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.DataTabPage.Size = New System.Drawing.Size(752, 414)
        Me.DataTabPage.TabIndex = 1
        Me.DataTabPage.Text = "資料列表"
        Me.DataTabPage.UseVisualStyleBackColor = True
        Me.DataTabPage.VerticalScrollbarBarColor = False
        Me.DataTabPage.VerticalScrollbarHighlightOnWheel = False
        Me.DataTabPage.VerticalScrollbarSize = 0
        '
        'DataListView
        '
        Me.DataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.DataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataListView.FullRowSelect = True
        Me.DataListView.GridLines = True
        Me.DataListView.HideSelection = False
        Me.DataListView.Location = New System.Drawing.Point(0, 0)
        Me.DataListView.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.DataListView.MultiSelect = False
        Me.DataListView.Name = "DataListView"
        Me.DataListView.Size = New System.Drawing.Size(752, 386)
        Me.DataListView.TabIndex = 14
        Me.DataListView.UseCompatibleStateImageBehavior = False
        Me.DataListView.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "類型"
        Me.ColumnHeader1.Width = 90
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.DisplayIndex = 2
        Me.ColumnHeader2.Text = "執行緒"
        Me.ColumnHeader2.Width = 116
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.DisplayIndex = 3
        Me.ColumnHeader3.Text = "訊息"
        Me.ColumnHeader3.Width = 534
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.DisplayIndex = 1
        Me.ColumnHeader4.Text = "時間"
        Me.ColumnHeader4.Width = 69
        '
        'CommandTextBox
        '
        '
        '
        '
        Me.CommandTextBox.CustomButton.Image = Nothing
        Me.CommandTextBox.CustomButton.Location = New System.Drawing.Point(732, 2)
        Me.CommandTextBox.CustomButton.Name = ""
        Me.CommandTextBox.CustomButton.Size = New System.Drawing.Size(17, 17)
        Me.CommandTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.CommandTextBox.CustomButton.TabIndex = 1
        Me.CommandTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.CommandTextBox.CustomButton.UseSelectable = True
        Me.CommandTextBox.CustomButton.Visible = False
        Me.CommandTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.CommandTextBox.Lines = New String(-1) {}
        Me.CommandTextBox.Location = New System.Drawing.Point(0, 389)
        Me.CommandTextBox.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.CommandTextBox.MaxLength = 32767
        Me.CommandTextBox.Name = "CommandTextBox"
        Me.CommandTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.CommandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.CommandTextBox.SelectedText = ""
        Me.CommandTextBox.SelectionLength = 0
        Me.CommandTextBox.SelectionStart = 0
        Me.CommandTextBox.ShortcutsEnabled = True
        Me.CommandTextBox.Size = New System.Drawing.Size(752, 22)
        Me.CommandTextBox.TabIndex = 13
        Me.CommandTextBox.UseSelectable = True
        Me.CommandTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.CommandTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'CloseCheckBox
        '
        Me.CloseCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CloseCheckBox.AutoSize = True
        Me.CloseCheckBox.Location = New System.Drawing.Point(685, 38)
        Me.CloseCheckBox.Name = "CloseCheckBox"
        Me.CloseCheckBox.Size = New System.Drawing.Size(87, 20)
        Me.CloseCheckBox.TabIndex = 7
        Me.CloseCheckBox.Text = "停止時關閉"
        Me.CloseCheckBox.UseVisualStyleBackColor = True
        '
        'StopLoadingCheckBox
        '
        Me.StopLoadingCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StopLoadingCheckBox.AutoSize = True
        Me.StopLoadingCheckBox.Location = New System.Drawing.Point(610, 38)
        Me.StopLoadingCheckBox.Name = "StopLoadingCheckBox"
        Me.StopLoadingCheckBox.Size = New System.Drawing.Size(75, 20)
        Me.StopLoadingCheckBox.TabIndex = 4
        Me.StopLoadingCheckBox.Text = "暫停載入"
        Me.StopLoadingCheckBox.UseVisualStyleBackColor = True
        '
        'TaskTimer
        '
        Me.TaskTimer.Interval = 50
        '
        'ListBoxTImer
        '
        Me.ListBoxTImer.Enabled = True
        Me.ListBoxTImer.Interval = 250
        '
        'ContextTaskMenu
        '
        Me.ContextTaskMenu.Name = "ContextTaskMenu"
        Me.ContextTaskMenu.Size = New System.Drawing.Size(61, 4)
        '
        'MetroStyleManager1
        '
        Me.MetroStyleManager1.Owner = Me
        Me.MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Green
        '
        'ServerConsole
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(800, 506)
        Me.Controls.Add(Me.StopLoadingCheckBox)
        Me.Controls.Add(Me.CloseCheckBox)
        Me.Controls.Add(Me.MainTabControl)
        Me.DisplayHeader = False
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "ServerConsole"
        Me.Padding = New System.Windows.Forms.Padding(20, 30, 20, 20)
        Me.Style = MetroFramework.MetroColorStyle.Green
        Me.Text = "伺服器控制台"
        Me.MainTabControl.ResumeLayout(False)
        Me.MainTabPage.ResumeLayout(False)
        Me.PlayerGroupBox.ResumeLayout(False)
        Me.UserContextMenu.ResumeLayout(False)
        Me.SystemGroupBox.ResumeLayout(False)
        Me.SettingTabPage.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TaskGroupBox.ResumeLayout(False)
        Me.TaskControlPanel.ResumeLayout(False)
        Me.NotifyGroupBox.ResumeLayout(False)
        Me.DataTabPage.ResumeLayout(False)
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StopLoadingCheckBox As CheckBox
    Friend WithEvents ServerStatusLabel As Label
    Friend WithEvents SystemGroupBox As GroupBox
    Friend WithEvents IDLabel As Label
    Friend WithEvents MemoryLabel As Label
    Friend WithEvents TaskTimer As Timer
    Friend WithEvents CloseCheckBox As CheckBox
    Friend WithEvents DataListView As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ListBoxTImer As Timer
    Friend WithEvents PlayerGroupBox As GroupBox
    Friend WithEvents PlayerListBox As ListBox
    Friend WithEvents NotifyGroupBox As GroupBox
    Friend WithEvents NotifyChooseListBox As CheckedListBox
    Friend WithEvents TaskGroupBox As GroupBox
    Friend WithEvents TaskListBox As CheckedListBox
    Friend WithEvents TaskControlPanel As Panel
    Friend WithEvents RemoveTaskButton As Button
    Friend WithEvents EditTaskButton As Button
    Friend WithEvents AddTaskButton As Button
    Friend WithEvents UserContextMenu As ContextMenuStrip
    Friend WithEvents 封禁ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 踢出ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents 設定OPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 解除OPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents RestartButton As Button
    Friend WithEvents ForceCloseButton As Button
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents 更新列表用listToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CMDButton As Button
    Friend WithEvents CommandTextBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents ContextTaskMenu As ContextMenuStrip
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents MainTabControl As MetroFramework.Controls.MetroTabControl
    Friend WithEvents MainTabPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents DataTabPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents SettingTabPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
End Class
