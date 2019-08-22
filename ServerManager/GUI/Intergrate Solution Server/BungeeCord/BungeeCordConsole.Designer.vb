<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BungeeCordConsole
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BungeeCordConsole))
        Me.MainTabControl = New MetroFramework.Controls.MetroTabControl()
        Me.MainTabPage = New MetroFramework.Controls.MetroTabPage()
        Me.PlayerGroupBox = New System.Windows.Forms.GroupBox()
        Me.PlayerListBox = New System.Windows.Forms.ListBox()
        Me.SystemGroupBox = New System.Windows.Forms.GroupBox()
        Me.IDLabel = New System.Windows.Forms.Label()
        Me.MemoryLabel = New System.Windows.Forms.Label()
        Me.ServerStatusLabel = New System.Windows.Forms.Label()
        Me.SettingTabPage = New MetroFramework.Controls.MetroTabPage()
        Me.NotifyGroupBox = New System.Windows.Forms.GroupBox()
        Me.NotifyChooseListBox = New System.Windows.Forms.CheckedListBox()
        Me.DataStreamTabPage = New MetroFramework.Controls.MetroTabPage()
        Me.ContentPanel = New System.Windows.Forms.Panel()
        Me.DataListView = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.StopLoadingCheckBox = New System.Windows.Forms.CheckBox()
        Me.CloseCheckBox = New System.Windows.Forms.CheckBox()
        Me.CommandTextBox = New MetroFramework.Controls.MetroTextBox()
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
        Me.MainTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular
        Me.MainTabControl.Location = New System.Drawing.Point(20, 30)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(760, 420)
        Me.MainTabControl.Style = MetroFramework.MetroColorStyle.Green
        Me.MainTabControl.TabIndex = 0
        Me.MainTabControl.UseSelectable = True
        '
        'MainTabPage
        '
        Me.MainTabPage.Controls.Add(Me.PlayerGroupBox)
        Me.MainTabPage.Controls.Add(Me.SystemGroupBox)
        Me.MainTabPage.Controls.Add(Me.ServerStatusLabel)
        Me.MainTabPage.HorizontalScrollbarBarColor = True
        Me.MainTabPage.HorizontalScrollbarHighlightOnWheel = False
        Me.MainTabPage.HorizontalScrollbarSize = 10
        Me.MainTabPage.Location = New System.Drawing.Point(4, 38)
        Me.MainTabPage.Name = "MainTabPage"
        Me.MainTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.MainTabPage.Size = New System.Drawing.Size(752, 378)
        Me.MainTabPage.TabIndex = 0
        Me.MainTabPage.Text = "BungeeCord 資訊"
        Me.MainTabPage.UseVisualStyleBackColor = True
        Me.MainTabPage.VerticalScrollbarBarColor = True
        Me.MainTabPage.VerticalScrollbarHighlightOnWheel = False
        Me.MainTabPage.VerticalScrollbarSize = 10
        '
        'PlayerGroupBox
        '
        Me.PlayerGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PlayerGroupBox.Controls.Add(Me.PlayerListBox)
        Me.PlayerGroupBox.Location = New System.Drawing.Point(8, 108)
        Me.PlayerGroupBox.Name = "PlayerGroupBox"
        Me.PlayerGroupBox.Size = New System.Drawing.Size(736, 274)
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
        Me.PlayerListBox.Size = New System.Drawing.Size(730, 253)
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
        Me.MemoryLabel.Size = New System.Drawing.Size(730, 21)
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
        Me.ServerStatusLabel.Size = New System.Drawing.Size(657, 21)
        Me.ServerStatusLabel.TabIndex = 29
        Me.ServerStatusLabel.Text = "BungeeCord 狀態：關閉"
        Me.ServerStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SettingTabPage
        '
        Me.SettingTabPage.Controls.Add(Me.NotifyGroupBox)
        Me.SettingTabPage.HorizontalScrollbarBarColor = True
        Me.SettingTabPage.HorizontalScrollbarHighlightOnWheel = False
        Me.SettingTabPage.HorizontalScrollbarSize = 10
        Me.SettingTabPage.Location = New System.Drawing.Point(4, 38)
        Me.SettingTabPage.Name = "SettingTabPage"
        Me.SettingTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.SettingTabPage.Size = New System.Drawing.Size(752, 378)
        Me.SettingTabPage.TabIndex = 3
        Me.SettingTabPage.Text = "BungeeCord 操作"
        Me.SettingTabPage.UseVisualStyleBackColor = True
        Me.SettingTabPage.VerticalScrollbarBarColor = True
        Me.SettingTabPage.VerticalScrollbarHighlightOnWheel = False
        Me.SettingTabPage.VerticalScrollbarSize = 10
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
        Me.DataStreamTabPage.HorizontalScrollbarBarColor = True
        Me.DataStreamTabPage.HorizontalScrollbarHighlightOnWheel = False
        Me.DataStreamTabPage.HorizontalScrollbarSize = 10
        Me.DataStreamTabPage.Location = New System.Drawing.Point(4, 38)
        Me.DataStreamTabPage.Name = "DataStreamTabPage"
        Me.DataStreamTabPage.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.DataStreamTabPage.Size = New System.Drawing.Size(752, 378)
        Me.DataStreamTabPage.TabIndex = 1
        Me.DataStreamTabPage.Text = "BungeeCord 主資料列表"
        Me.DataStreamTabPage.UseVisualStyleBackColor = True
        Me.DataStreamTabPage.VerticalScrollbarBarColor = True
        Me.DataStreamTabPage.VerticalScrollbarHighlightOnWheel = False
        Me.DataStreamTabPage.VerticalScrollbarSize = 10
        '
        'ContentPanel
        '
        Me.ContentPanel.Controls.Add(Me.DataListView)
        Me.ContentPanel.Controls.Add(Me.TopPanel)
        Me.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContentPanel.Location = New System.Drawing.Point(0, 0)
        Me.ContentPanel.Name = "ContentPanel"
        Me.ContentPanel.Size = New System.Drawing.Size(752, 353)
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
        Me.DataListView.Size = New System.Drawing.Size(752, 331)
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
        Me.TopPanel.Size = New System.Drawing.Size(752, 22)
        Me.TopPanel.TabIndex = 16
        '
        'StopLoadingCheckBox
        '
        Me.StopLoadingCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StopLoadingCheckBox.AutoSize = True
        Me.StopLoadingCheckBox.Location = New System.Drawing.Point(587, 3)
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
        Me.CloseCheckBox.Location = New System.Drawing.Point(665, 3)
        Me.CloseCheckBox.Name = "CloseCheckBox"
        Me.CloseCheckBox.Size = New System.Drawing.Size(84, 16)
        Me.CloseCheckBox.TabIndex = 7
        Me.CloseCheckBox.Text = "停止時關閉"
        Me.CloseCheckBox.UseVisualStyleBackColor = True
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
        Me.CommandTextBox.Location = New System.Drawing.Point(0, 353)
        Me.CommandTextBox.MaxLength = 32767
        Me.CommandTextBox.Name = "CommandTextBox"
        Me.CommandTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.CommandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.CommandTextBox.SelectedText = ""
        Me.CommandTextBox.SelectionLength = 0
        Me.CommandTextBox.SelectionStart = 0
        Me.CommandTextBox.ShortcutsEnabled = True
        Me.CommandTextBox.Size = New System.Drawing.Size(752, 22)
        Me.CommandTextBox.TabIndex = 12
        Me.CommandTextBox.UseSelectable = True
        Me.CommandTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.CommandTextBox.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'TaskTimer
        '
        Me.TaskTimer.Interval = 50
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(701, -30)
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
        Me.ClientSize = New System.Drawing.Size(800, 470)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.MainTabControl)
        Me.DisplayHeader = False
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BungeeCordConsole"
        Me.Padding = New System.Windows.Forms.Padding(20, 30, 20, 20)
        Me.Style = MetroFramework.MetroColorStyle.Green
        Me.Text = "BungeeCord 控制台"
        Me.MainTabControl.ResumeLayout(False)
        Me.MainTabPage.ResumeLayout(False)
        Me.PlayerGroupBox.ResumeLayout(False)
        Me.SystemGroupBox.ResumeLayout(False)
        Me.SettingTabPage.ResumeLayout(False)
        Me.NotifyGroupBox.ResumeLayout(False)
        Me.DataStreamTabPage.ResumeLayout(False)
        Me.ContentPanel.ResumeLayout(False)
        Me.ContentPanel.PerformLayout()
        Me.TopPanel.ResumeLayout(False)
        Me.TopPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StopLoadingCheckBox As CheckBox
    Friend WithEvents ServerStatusLabel As Label
    Friend WithEvents TaskTimer As Timer
    Friend WithEvents CloseCheckBox As CheckBox
    Friend WithEvents SystemGroupBox As GroupBox
    Friend WithEvents IDLabel As Label
    Friend WithEvents MemoryLabel As Label
    Friend WithEvents CommandTextBox As MetroFramework.Controls.MetroTextBox
    Friend WithEvents ContentPanel As Panel
    Friend WithEvents DataListView As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents TopPanel As Panel
    Friend WithEvents NotifyGroupBox As GroupBox
    Friend WithEvents NotifyChooseListBox As CheckedListBox
    Friend WithEvents PlayerGroupBox As GroupBox
    Friend WithEvents PlayerListBox As ListBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Button1 As Button
    Friend WithEvents MainTabControl As MetroFramework.Controls.MetroTabControl
    Friend WithEvents MainTabPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents DataStreamTabPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents SettingTabPage As MetroFramework.Controls.MetroTabPage
End Class
