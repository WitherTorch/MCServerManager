<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BungeeCordConsole
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BungeeCordConsole))
        Me.MainTabControl = New System.Windows.Forms.TabControl()
        Me.MainTabPage = New System.Windows.Forms.TabPage()
        Me.PlayerGroupBox = New System.Windows.Forms.GroupBox()
        Me.PlayerListBox = New System.Windows.Forms.ListBox()
        Me.SystemGroupBox = New System.Windows.Forms.GroupBox()
        Me.IDLabel = New System.Windows.Forms.Label()
        Me.MemoryLabel = New System.Windows.Forms.Label()
        Me.ServerStatusLabel = New System.Windows.Forms.Label()
        Me.SettingTabPage = New System.Windows.Forms.TabPage()
        Me.NotifyGroupBox = New System.Windows.Forms.GroupBox()
        Me.NotifyChooseListBox = New System.Windows.Forms.CheckedListBox()
        Me.DataStreamTabPage = New System.Windows.Forms.TabPage()
        Me.ContentPanel = New System.Windows.Forms.Panel()
        Me.DataListView = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.StopLoadingCheckBox = New System.Windows.Forms.CheckBox()
        Me.CloseCheckBox = New System.Windows.Forms.CheckBox()
        Me.CommandTextBox = New System.Windows.Forms.TextBox()
        Me.TaskTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.MainTabControl.SuspendLayout()
        Me.MainTabPage.SuspendLayout()
        Me.PlayerGroupBox.SuspendLayout()
        Me.SystemGroupBox.SuspendLayout()
        Me.SettingTabPage.SuspendLayout()
        Me.NotifyGroupBox.SuspendLayout()
        Me.DataStreamTabPage.SuspendLayout()
        Me.ContentPanel.SuspendLayout()
        Me.TopPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainTabControl
        '
        Me.MainTabControl.Controls.Add(Me.MainTabPage)
        Me.MainTabControl.Controls.Add(Me.SettingTabPage)
        Me.MainTabControl.Controls.Add(Me.DataStreamTabPage)
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.Location = New System.Drawing.Point(0, 0)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(800, 450)
        Me.MainTabControl.TabIndex = 0
        '
        'MainTabPage
        '
        Me.MainTabPage.Controls.Add(Me.PlayerGroupBox)
        Me.MainTabPage.Controls.Add(Me.SystemGroupBox)
        Me.MainTabPage.Controls.Add(Me.ServerStatusLabel)
        Me.MainTabPage.Location = New System.Drawing.Point(4, 22)
        Me.MainTabPage.Name = "MainTabPage"
        Me.MainTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.MainTabPage.Size = New System.Drawing.Size(792, 424)
        Me.MainTabPage.TabIndex = 0
        Me.MainTabPage.Text = "BungeeCord 資訊"
        Me.MainTabPage.UseVisualStyleBackColor = True
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
        Me.PlayerGroupBox.TabIndex = 37
        Me.PlayerGroupBox.TabStop = False
        Me.PlayerGroupBox.Text = "玩家"
        '
        'PlayerListBox
        '
        Me.PlayerListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PlayerListBox.FormattingEnabled = True
        Me.PlayerListBox.ItemHeight = 12
        Me.PlayerListBox.Location = New System.Drawing.Point(3, 18)
        Me.PlayerListBox.Name = "PlayerListBox"
        Me.PlayerListBox.Size = New System.Drawing.Size(770, 287)
        Me.PlayerListBox.TabIndex = 0
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
        Me.MemoryLabel.Size = New System.Drawing.Size(770, 21)
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
        Me.ServerStatusLabel.Size = New System.Drawing.Size(697, 21)
        Me.ServerStatusLabel.TabIndex = 29
        Me.ServerStatusLabel.Text = "BungeeCord 狀態：關閉"
        Me.ServerStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SettingTabPage
        '
        Me.SettingTabPage.Controls.Add(Me.NotifyGroupBox)
        Me.SettingTabPage.Location = New System.Drawing.Point(4, 22)
        Me.SettingTabPage.Name = "SettingTabPage"
        Me.SettingTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.SettingTabPage.Size = New System.Drawing.Size(792, 424)
        Me.SettingTabPage.TabIndex = 3
        Me.SettingTabPage.Text = "BungeeCord 操作"
        Me.SettingTabPage.UseVisualStyleBackColor = True
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
        Me.NotifyChooseListBox.Items.AddRange(New Object() {"玩家進入一個伺服器", "玩家離開一個伺服器", "伺服器發出警告訊息", "伺服器發出錯誤訊息"})
        Me.NotifyChooseListBox.Location = New System.Drawing.Point(3, 18)
        Me.NotifyChooseListBox.Name = "NotifyChooseListBox"
        Me.NotifyChooseListBox.Size = New System.Drawing.Size(770, 63)
        Me.NotifyChooseListBox.TabIndex = 0
        '
        'DataStreamTabPage
        '
        Me.DataStreamTabPage.Controls.Add(Me.ContentPanel)
        Me.DataStreamTabPage.Controls.Add(Me.CommandTextBox)
        Me.DataStreamTabPage.Location = New System.Drawing.Point(4, 22)
        Me.DataStreamTabPage.Name = "DataStreamTabPage"
        Me.DataStreamTabPage.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.DataStreamTabPage.Size = New System.Drawing.Size(792, 424)
        Me.DataStreamTabPage.TabIndex = 1
        Me.DataStreamTabPage.Text = "BungeeCord 主資料列表"
        Me.DataStreamTabPage.UseVisualStyleBackColor = True
        '
        'ContentPanel
        '
        Me.ContentPanel.Controls.Add(Me.DataListView)
        Me.ContentPanel.Controls.Add(Me.TopPanel)
        Me.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContentPanel.Location = New System.Drawing.Point(0, 0)
        Me.ContentPanel.Name = "ContentPanel"
        Me.ContentPanel.Size = New System.Drawing.Size(792, 399)
        Me.ContentPanel.TabIndex = 13
        '
        'DataListView
        '
        Me.DataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.DataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataListView.FullRowSelect = True
        Me.DataListView.GridLines = True
        Me.DataListView.HideSelection = False
        Me.DataListView.Location = New System.Drawing.Point(0, 22)
        Me.DataListView.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.DataListView.MultiSelect = False
        Me.DataListView.Name = "DataListView"
        Me.DataListView.Size = New System.Drawing.Size(792, 377)
        Me.DataListView.TabIndex = 15
        Me.DataListView.UseCompatibleStateImageBehavior = False
        Me.DataListView.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "類型"
        Me.ColumnHeader1.Width = 90
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.DisplayIndex = 2
        Me.ColumnHeader3.Text = "訊息"
        Me.ColumnHeader3.Width = 656
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.DisplayIndex = 1
        Me.ColumnHeader4.Text = "時間"
        Me.ColumnHeader4.Width = 69
        '
        'TopPanel
        '
        Me.TopPanel.AutoSize = True
        Me.TopPanel.Controls.Add(Me.StopLoadingCheckBox)
        Me.TopPanel.Controls.Add(Me.CloseCheckBox)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(792, 22)
        Me.TopPanel.TabIndex = 16
        '
        'StopLoadingCheckBox
        '
        Me.StopLoadingCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StopLoadingCheckBox.AutoSize = True
        Me.StopLoadingCheckBox.Location = New System.Drawing.Point(716, 3)
        Me.StopLoadingCheckBox.Name = "StopLoadingCheckBox"
        Me.StopLoadingCheckBox.Size = New System.Drawing.Size(72, 16)
        Me.StopLoadingCheckBox.TabIndex = 4
        Me.StopLoadingCheckBox.Text = "暫停載入"
        Me.StopLoadingCheckBox.UseVisualStyleBackColor = True
        '
        'CloseCheckBox
        '
        Me.CloseCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CloseCheckBox.AutoSize = True
        Me.CloseCheckBox.Location = New System.Drawing.Point(626, 3)
        Me.CloseCheckBox.Name = "CloseCheckBox"
        Me.CloseCheckBox.Size = New System.Drawing.Size(84, 16)
        Me.CloseCheckBox.TabIndex = 7
        Me.CloseCheckBox.Text = "停止時關閉"
        Me.CloseCheckBox.UseVisualStyleBackColor = True
        '
        'CommandTextBox
        '
        Me.CommandTextBox.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.CommandTextBox.Location = New System.Drawing.Point(0, 399)
        Me.CommandTextBox.Name = "CommandTextBox"
        Me.CommandTextBox.Size = New System.Drawing.Size(792, 22)
        Me.CommandTextBox.TabIndex = 12
        '
        'TaskTimer
        '
        Me.TaskTimer.Interval = 50
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(701, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 22)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "開啟子控制台"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'BungeeCordConsole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.MainTabControl)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BungeeCordConsole"
        Me.Text = "BungeeCord 控制台"
        Me.MainTabControl.ResumeLayout(False)
        Me.MainTabPage.ResumeLayout(False)
        Me.PlayerGroupBox.ResumeLayout(False)
        Me.SystemGroupBox.ResumeLayout(False)
        Me.SettingTabPage.ResumeLayout(False)
        Me.NotifyGroupBox.ResumeLayout(False)
        Me.DataStreamTabPage.ResumeLayout(False)
        Me.DataStreamTabPage.PerformLayout()
        Me.ContentPanel.ResumeLayout(False)
        Me.ContentPanel.PerformLayout()
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MainTabControl As TabControl
    Friend WithEvents MainTabPage As TabPage
    Friend WithEvents DataStreamTabPage As TabPage
    Friend WithEvents StopLoadingCheckBox As CheckBox
    Friend WithEvents ServerStatusLabel As Label
    Friend WithEvents TaskTimer As Timer
    Friend WithEvents CloseCheckBox As CheckBox
    Friend WithEvents SystemGroupBox As GroupBox
    Friend WithEvents IDLabel As Label
    Friend WithEvents MemoryLabel As Label
    Friend WithEvents CommandTextBox As TextBox
    Friend WithEvents ContentPanel As Panel
    Friend WithEvents DataListView As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents TopPanel As Panel
    Friend WithEvents SettingTabPage As TabPage
    Friend WithEvents NotifyGroupBox As GroupBox
    Friend WithEvents NotifyChooseListBox As CheckedListBox
    Friend WithEvents PlayerGroupBox As GroupBox
    Friend WithEvents PlayerListBox As ListBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Button1 As Button
End Class
