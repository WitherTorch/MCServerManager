<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ServerConsole
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
        Dim Panel1 As System.Windows.Forms.Panel
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerConsole))
        Me.MainTabControl = New System.Windows.Forms.TabControl()
        Me.MainTabPage = New System.Windows.Forms.TabPage()
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
        Me.SettingTabPage = New System.Windows.Forms.TabPage()
        Me.TaskGroupBox = New System.Windows.Forms.GroupBox()
        Me.TaskListBox = New System.Windows.Forms.CheckedListBox()
        Me.TaskControlPanel = New System.Windows.Forms.Panel()
        Me.RemoveTaskButton = New System.Windows.Forms.Button()
        Me.EditTaskButton = New System.Windows.Forms.Button()
        Me.AddTaskButton = New System.Windows.Forms.Button()
        Me.NotifyGroupBox = New System.Windows.Forms.GroupBox()
        Me.NotifyChooseListBox = New System.Windows.Forms.CheckedListBox()
        Me.DataTabPage = New System.Windows.Forms.TabPage()
        Me.DataListView = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CommandTextBox = New System.Windows.Forms.TextBox()
        Me.CloseCheckBox = New System.Windows.Forms.CheckBox()
        Me.StopLoadingCheckBox = New System.Windows.Forms.CheckBox()
        Me.TaskTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ListBoxTImer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Panel1 = New System.Windows.Forms.Panel()
        Me.MainTabControl.SuspendLayout
        Me.MainTabPage.SuspendLayout
        Me.PlayerGroupBox.SuspendLayout
        Me.UserContextMenu.SuspendLayout
        Me.SystemGroupBox.SuspendLayout
        Me.SettingTabPage.SuspendLayout
        Me.TaskGroupBox.SuspendLayout
        Me.TaskControlPanel.SuspendLayout
        Me.NotifyGroupBox.SuspendLayout
        Me.DataTabPage.SuspendLayout
        Me.SuspendLayout
        '
        'Panel1
        '
        Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Panel1.Location = New System.Drawing.Point(0, 396)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(792, 3)
        Panel1.TabIndex = 15
        '
        'MainTabControl
        '
        Me.MainTabControl.Controls.Add(Me.MainTabPage)
        Me.MainTabControl.Controls.Add(Me.SettingTabPage)
        Me.MainTabControl.Controls.Add(Me.DataTabPage)
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.Location = New System.Drawing.Point(0, 0)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(800, 450)
        Me.MainTabControl.TabIndex = 0
        '
        'MainTabPage
        '
        Me.MainTabPage.Controls.Add(Me.ForceCloseButton)
        Me.MainTabPage.Controls.Add(Me.RestartButton)
        Me.MainTabPage.Controls.Add(Me.PlayerGroupBox)
        Me.MainTabPage.Controls.Add(Me.SystemGroupBox)
        Me.MainTabPage.Controls.Add(Me.ServerStatusLabel)
        Me.MainTabPage.Location = New System.Drawing.Point(4, 22)
        Me.MainTabPage.Name = "MainTabPage"
        Me.MainTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.MainTabPage.Size = New System.Drawing.Size(792, 424)
        Me.MainTabPage.TabIndex = 0
        Me.MainTabPage.Text = "伺服器資訊"
        Me.MainTabPage.UseVisualStyleBackColor = True
        '
        'ForceCloseButton
        '
        Me.ForceCloseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ForceCloseButton.Location = New System.Drawing.Point(627, 6)
        Me.ForceCloseButton.Name = "ForceCloseButton"
        Me.ForceCloseButton.Size = New System.Drawing.Size(84, 22)
        Me.ForceCloseButton.TabIndex = 38
        Me.ForceCloseButton.Text = "強制關閉"
        Me.ForceCloseButton.UseVisualStyleBackColor = True
        '
        'RestartButton
        '
        Me.RestartButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RestartButton.Location = New System.Drawing.Point(717, 6)
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
        Me.PlayerGroupBox.Size = New System.Drawing.Size(776, 308)
        Me.PlayerGroupBox.TabIndex = 36
        Me.PlayerGroupBox.TabStop = False
        Me.PlayerGroupBox.Text = "玩家"
        '
        'PlayerListBox
        '
        Me.PlayerListBox.ContextMenuStrip = Me.UserContextMenu
        Me.PlayerListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PlayerListBox.FormattingEnabled = True
        Me.PlayerListBox.ItemHeight = 12
        Me.PlayerListBox.Location = New System.Drawing.Point(3, 18)
        Me.PlayerListBox.Name = "PlayerListBox"
        Me.PlayerListBox.Size = New System.Drawing.Size(770, 287)
        Me.PlayerListBox.TabIndex = 0
        '
        'UserContextMenu
        '
        Me.UserContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.封禁ToolStripMenuItem, Me.踢出ToolStripMenuItem, Me.ToolStripSeparator1, Me.設定OPToolStripMenuItem, Me.解除OPToolStripMenuItem, Me.ToolStripSeparator2, Me.更新列表用listToolStripMenuItem})
        Me.UserContextMenu.Name = "UserContextMenu"
        Me.UserContextMenu.Size = New System.Drawing.Size(123, 126)
        '
        '封禁ToolStripMenuItem
        '
        Me.封禁ToolStripMenuItem.Name = "封禁ToolStripMenuItem"
        Me.封禁ToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.封禁ToolStripMenuItem.Text = "封禁"
        '
        '踢出ToolStripMenuItem
        '
        Me.踢出ToolStripMenuItem.Name = "踢出ToolStripMenuItem"
        Me.踢出ToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.踢出ToolStripMenuItem.Text = "踢出"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(119, 6)
        '
        '設定OPToolStripMenuItem
        '
        Me.設定OPToolStripMenuItem.Name = "設定OPToolStripMenuItem"
        Me.設定OPToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.設定OPToolStripMenuItem.Text = "設定OP"
        '
        '解除OPToolStripMenuItem
        '
        Me.解除OPToolStripMenuItem.Name = "解除OPToolStripMenuItem"
        Me.解除OPToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.解除OPToolStripMenuItem.Text = "解除OP"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(119, 6)
        '
        '更新列表用listToolStripMenuItem
        '
        Me.更新列表用listToolStripMenuItem.Name = "更新列表用listToolStripMenuItem"
        Me.更新列表用listToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
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
        Me.SystemGroupBox.Size = New System.Drawing.Size(776, 69)
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
        Me.IDLabel.Size = New System.Drawing.Size(764, 21)
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
        Me.MemoryLabel.Size = New System.Drawing.Size(764, 21)
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
        Me.ServerStatusLabel.Size = New System.Drawing.Size(615, 21)
        Me.ServerStatusLabel.TabIndex = 29
        Me.ServerStatusLabel.Text = "伺服器狀態：關閉"
        Me.ServerStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SettingTabPage
        '
        Me.SettingTabPage.AutoScroll = True
        Me.SettingTabPage.Controls.Add(Me.TaskGroupBox)
        Me.SettingTabPage.Controls.Add(Me.NotifyGroupBox)
        Me.SettingTabPage.Location = New System.Drawing.Point(4, 22)
        Me.SettingTabPage.Name = "SettingTabPage"
        Me.SettingTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.SettingTabPage.Size = New System.Drawing.Size(792, 424)
        Me.SettingTabPage.TabIndex = 2
        Me.SettingTabPage.Text = "伺服器操作"
        Me.SettingTabPage.UseVisualStyleBackColor = True
        '
        'TaskGroupBox
        '
        Me.TaskGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskGroupBox.Controls.Add(Me.TaskListBox)
        Me.TaskGroupBox.Controls.Add(Me.TaskControlPanel)
        Me.TaskGroupBox.Location = New System.Drawing.Point(8, 96)
        Me.TaskGroupBox.Name = "TaskGroupBox"
        Me.TaskGroupBox.Size = New System.Drawing.Size(776, 92)
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
        Me.TaskListBox.Size = New System.Drawing.Size(672, 72)
        Me.TaskListBox.TabIndex = 1
        '
        'TaskControlPanel
        '
        Me.TaskControlPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskControlPanel.Controls.Add(Me.RemoveTaskButton)
        Me.TaskControlPanel.Controls.Add(Me.EditTaskButton)
        Me.TaskControlPanel.Controls.Add(Me.AddTaskButton)
        Me.TaskControlPanel.Location = New System.Drawing.Point(684, 14)
        Me.TaskControlPanel.Name = "TaskControlPanel"
        Me.TaskControlPanel.Size = New System.Drawing.Size(89, 72)
        Me.TaskControlPanel.TabIndex = 2
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
        Me.NotifyGroupBox.Size = New System.Drawing.Size(776, 84)
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
        Me.NotifyChooseListBox.Location = New System.Drawing.Point(3, 18)
        Me.NotifyChooseListBox.Name = "NotifyChooseListBox"
        Me.NotifyChooseListBox.Size = New System.Drawing.Size(770, 63)
        Me.NotifyChooseListBox.TabIndex = 0
        '
        'DataTabPage
        '
        Me.DataTabPage.Controls.Add(Me.DataListView)
        Me.DataTabPage.Controls.Add(Panel1)
        Me.DataTabPage.Controls.Add(Me.CommandTextBox)
        Me.DataTabPage.Location = New System.Drawing.Point(4, 22)
        Me.DataTabPage.Name = "DataTabPage"
        Me.DataTabPage.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.DataTabPage.Size = New System.Drawing.Size(792, 424)
        Me.DataTabPage.TabIndex = 1
        Me.DataTabPage.Text = "資料列表"
        Me.DataTabPage.UseVisualStyleBackColor = True
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
        Me.DataListView.Size = New System.Drawing.Size(792, 396)
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
        Me.CommandTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.CommandTextBox.Location = New System.Drawing.Point(0, 399)
        Me.CommandTextBox.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.CommandTextBox.Name = "CommandTextBox"
        Me.CommandTextBox.Size = New System.Drawing.Size(792, 22)
        Me.CommandTextBox.TabIndex = 13
        '
        'CloseCheckBox
        '
        Me.CloseCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CloseCheckBox.AutoSize = True
        Me.CloseCheckBox.Location = New System.Drawing.Point(631, 3)
        Me.CloseCheckBox.Name = "CloseCheckBox"
        Me.CloseCheckBox.Size = New System.Drawing.Size(84, 16)
        Me.CloseCheckBox.TabIndex = 7
        Me.CloseCheckBox.Text = "停止時關閉"
        Me.CloseCheckBox.UseVisualStyleBackColor = True
        '
        'StopLoadingCheckBox
        '
        Me.StopLoadingCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StopLoadingCheckBox.AutoSize = True
        Me.StopLoadingCheckBox.Location = New System.Drawing.Point(721, 3)
        Me.StopLoadingCheckBox.Name = "StopLoadingCheckBox"
        Me.StopLoadingCheckBox.Size = New System.Drawing.Size(72, 16)
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
        'ServerConsole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.CloseCheckBox)
        Me.Controls.Add(Me.StopLoadingCheckBox)
        Me.Controls.Add(Me.MainTabControl)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ServerConsole"
        Me.Text = "伺服器控制台"
        Me.MainTabControl.ResumeLayout(False)
        Me.MainTabPage.ResumeLayout(False)
        Me.PlayerGroupBox.ResumeLayout(False)
        Me.UserContextMenu.ResumeLayout(False)
        Me.SystemGroupBox.ResumeLayout(False)
        Me.SettingTabPage.ResumeLayout(False)
        Me.TaskGroupBox.ResumeLayout(False)
        Me.TaskControlPanel.ResumeLayout(False)
        Me.NotifyGroupBox.ResumeLayout(False)
        Me.DataTabPage.ResumeLayout(False)
        Me.DataTabPage.PerformLayout
        Me.ResumeLayout(False)
        Me.PerformLayout

    End Sub

    Friend WithEvents MainTabControl As TabControl
    Friend WithEvents MainTabPage As TabPage
    Friend WithEvents DataTabPage As TabPage
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
    Friend WithEvents CommandTextBox As TextBox
    Friend WithEvents ListBoxTImer As Timer
    Friend WithEvents PlayerGroupBox As GroupBox
    Friend WithEvents PlayerListBox As ListBox
    Friend WithEvents SettingTabPage As TabPage
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
End Class
