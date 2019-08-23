<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ServerSetter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerSetter))
        Me.SettingTabControl = New MetroFramework.Controls.MetroTabControl()
        Me.NormalTabPage = New MetroFramework.Controls.MetroTabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ServerMemoryMaxBox = New System.Windows.Forms.NumericUpDown()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ServerMemoryMinBox = New System.Windows.Forms.NumericUpDown()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TaskGroupBox = New System.Windows.Forms.GroupBox()
        Me.TaskListBox = New System.Windows.Forms.CheckedListBox()
        Me.TaskControlPanel = New System.Windows.Forms.Panel()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.RemoveTaskButton = New System.Windows.Forms.Button()
        Me.EditTaskButton = New System.Windows.Forms.Button()
        Me.AddTaskButton = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.MapPanel = New System.Windows.Forms.Panel()
        Me.UpdateGroupBox = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.UpdateButton = New System.Windows.Forms.Button()
        Me.VersionLabel = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PluginManageButton = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.AdvancedTabPage = New MetroFramework.Controls.MetroTabPage()
        Me.AdvancedPropertyGrid = New System.Windows.Forms.PropertyGrid()
        Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.SettingTabControl.SuspendLayout()
        Me.NormalTabPage.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ServerMemoryMaxBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServerMemoryMinBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TaskGroupBox.SuspendLayout()
        Me.TaskControlPanel.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.UpdateGroupBox.SuspendLayout()
        Me.AdvancedTabPage.SuspendLayout()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SettingTabControl
        '
        Me.SettingTabControl.Controls.Add(Me.NormalTabPage)
        Me.SettingTabControl.Controls.Add(Me.AdvancedTabPage)
        Me.SettingTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SettingTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular
        Me.SettingTabControl.Location = New System.Drawing.Point(20, 30)
        Me.SettingTabControl.Name = "SettingTabControl"
        Me.SettingTabControl.SelectedIndex = 0
        Me.SettingTabControl.Size = New System.Drawing.Size(544, 515)
        Me.SettingTabControl.Style = MetroFramework.MetroColorStyle.Green
        Me.SettingTabControl.TabIndex = 0
        Me.SettingTabControl.UseSelectable = True
        '
        'NormalTabPage
        '
        Me.NormalTabPage.AutoScroll = True
        Me.NormalTabPage.Controls.Add(Me.GroupBox1)
        Me.NormalTabPage.Controls.Add(Me.TaskGroupBox)
        Me.NormalTabPage.Controls.Add(Me.GroupBox4)
        Me.NormalTabPage.Controls.Add(Me.UpdateGroupBox)
        Me.NormalTabPage.Controls.Add(Me.Button1)
        Me.NormalTabPage.Controls.Add(Me.PluginManageButton)
        Me.NormalTabPage.Controls.Add(Me.Button4)
        Me.NormalTabPage.HorizontalScrollbar = True
        Me.NormalTabPage.HorizontalScrollbarBarColor = True
        Me.NormalTabPage.HorizontalScrollbarHighlightOnWheel = False
        Me.NormalTabPage.HorizontalScrollbarSize = 10
        Me.NormalTabPage.Location = New System.Drawing.Point(4, 38)
        Me.NormalTabPage.Name = "NormalTabPage"
        Me.NormalTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.NormalTabPage.Size = New System.Drawing.Size(536, 473)
        Me.NormalTabPage.TabIndex = 0
        Me.NormalTabPage.Text = "一般"
        Me.NormalTabPage.UseVisualStyleBackColor = True
        Me.NormalTabPage.VerticalScrollbar = True
        Me.NormalTabPage.VerticalScrollbarBarColor = True
        Me.NormalTabPage.VerticalScrollbarHighlightOnWheel = False
        Me.NormalTabPage.VerticalScrollbarSize = 10
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.ServerMemoryMaxBox)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.ServerMemoryMinBox)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 69)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(508, 74)
        Me.GroupBox1.TabIndex = 49
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "執行記憶體(如果最大或最小值小於等於0的話的話就參照預設)"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 21)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 16)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "最大值："
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(143, 22)
        Me.Label12.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(27, 16)
        Me.Label12.TabIndex = 33
        Me.Label12.Text = "MB"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerMemoryMaxBox
        '
        Me.ServerMemoryMaxBox.Location = New System.Drawing.Point(62, 17)
        Me.ServerMemoryMaxBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.ServerMemoryMaxBox.Name = "ServerMemoryMaxBox"
        Me.ServerMemoryMaxBox.Size = New System.Drawing.Size(78, 23)
        Me.ServerMemoryMaxBox.TabIndex = 32
        Me.ServerMemoryMaxBox.Tag = ""
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(143, 49)
        Me.Label14.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(27, 16)
        Me.Label14.TabIndex = 36
        Me.Label14.Text = "MB"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerMemoryMinBox
        '
        Me.ServerMemoryMinBox.Location = New System.Drawing.Point(62, 45)
        Me.ServerMemoryMinBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.ServerMemoryMinBox.Name = "ServerMemoryMinBox"
        Me.ServerMemoryMinBox.Size = New System.Drawing.Size(78, 23)
        Me.ServerMemoryMinBox.TabIndex = 35
        Me.ServerMemoryMinBox.Tag = ""
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 48)
        Me.Label13.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 16)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "最小值："
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TaskGroupBox
        '
        Me.TaskGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskGroupBox.Controls.Add(Me.TaskListBox)
        Me.TaskGroupBox.Controls.Add(Me.TaskControlPanel)
        Me.TaskGroupBox.Location = New System.Drawing.Point(3, 251)
        Me.TaskGroupBox.Name = "TaskGroupBox"
        Me.TaskGroupBox.Size = New System.Drawing.Size(508, 137)
        Me.TaskGroupBox.TabIndex = 53
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
        Me.TaskListBox.Size = New System.Drawing.Size(404, 94)
        Me.TaskListBox.TabIndex = 1
        '
        'TaskControlPanel
        '
        Me.TaskControlPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskControlPanel.Controls.Add(Me.Button5)
        Me.TaskControlPanel.Controls.Add(Me.Button3)
        Me.TaskControlPanel.Controls.Add(Me.RemoveTaskButton)
        Me.TaskControlPanel.Controls.Add(Me.EditTaskButton)
        Me.TaskControlPanel.Controls.Add(Me.AddTaskButton)
        Me.TaskControlPanel.Location = New System.Drawing.Point(416, 14)
        Me.TaskControlPanel.Name = "TaskControlPanel"
        Me.TaskControlPanel.Size = New System.Drawing.Size(89, 117)
        Me.TaskControlPanel.TabIndex = 2
        '
        'Button5
        '
        Me.Button5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button5.Location = New System.Drawing.Point(0, 92)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(89, 23)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "匯出工作..."
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button3.Location = New System.Drawing.Point(0, 69)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(89, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "匯入工作..."
        Me.Button3.UseVisualStyleBackColor = True
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
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.MapPanel)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 145)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(508, 100)
        Me.GroupBox4.TabIndex = 52
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "地圖"
        '
        'MapPanel
        '
        Me.MapPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MapPanel.Location = New System.Drawing.Point(6, 21)
        Me.MapPanel.Name = "MapPanel"
        Me.MapPanel.Size = New System.Drawing.Size(495, 70)
        Me.MapPanel.TabIndex = 0
        '
        'UpdateGroupBox
        '
        Me.UpdateGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UpdateGroupBox.Controls.Add(Me.Button2)
        Me.UpdateGroupBox.Controls.Add(Me.UpdateButton)
        Me.UpdateGroupBox.Controls.Add(Me.VersionLabel)
        Me.UpdateGroupBox.Location = New System.Drawing.Point(3, 6)
        Me.UpdateGroupBox.Name = "UpdateGroupBox"
        Me.UpdateGroupBox.Size = New System.Drawing.Size(508, 57)
        Me.UpdateGroupBox.TabIndex = 48
        Me.UpdateGroupBox.TabStop = False
        Me.UpdateGroupBox.Text = "軟體"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.AutoSize = True
        Me.Button2.Location = New System.Drawing.Point(384, 18)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(66, 29)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "檢查更新"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'UpdateButton
        '
        Me.UpdateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UpdateButton.AutoSize = True
        Me.UpdateButton.Location = New System.Drawing.Point(456, 18)
        Me.UpdateButton.Name = "UpdateButton"
        Me.UpdateButton.Size = New System.Drawing.Size(46, 29)
        Me.UpdateButton.TabIndex = 1
        Me.UpdateButton.Text = "更新"
        Me.UpdateButton.UseVisualStyleBackColor = True
        '
        'VersionLabel
        '
        Me.VersionLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionLabel.Location = New System.Drawing.Point(6, 18)
        Me.VersionLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.VersionLabel.Name = "VersionLabel"
        Me.VersionLabel.Size = New System.Drawing.Size(444, 29)
        Me.VersionLabel.TabIndex = 0
        Me.VersionLabel.Text = "<ServerVersionType>：<ServerVersionStatus>"
        Me.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button1
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(3, 494)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(508, 44)
        Me.Button1.TabIndex = 51
        Me.Button1.Text = "設定白名單"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PluginManageButton
        '
        Me.PluginManageButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PluginManageButton.Location = New System.Drawing.Point(3, 444)
        Me.PluginManageButton.Name = "PluginManageButton"
        Me.PluginManageButton.Size = New System.Drawing.Size(508, 44)
        Me.PluginManageButton.TabIndex = 50
        Me.PluginManageButton.Text = "管理插件"
        Me.PluginManageButton.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(3, 394)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(508, 44)
        Me.Button4.TabIndex = 49
        Me.Button4.Text = "選擇伺服器圖示"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'AdvancedTabPage
        '
        Me.AdvancedTabPage.Controls.Add(Me.AdvancedPropertyGrid)
        Me.AdvancedTabPage.HorizontalScrollbarBarColor = True
        Me.AdvancedTabPage.HorizontalScrollbarHighlightOnWheel = False
        Me.AdvancedTabPage.HorizontalScrollbarSize = 10
        Me.AdvancedTabPage.Location = New System.Drawing.Point(4, 38)
        Me.AdvancedTabPage.Name = "AdvancedTabPage"
        Me.AdvancedTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.AdvancedTabPage.Size = New System.Drawing.Size(536, 473)
        Me.AdvancedTabPage.TabIndex = 1
        Me.AdvancedTabPage.Text = "進階"
        Me.AdvancedTabPage.UseVisualStyleBackColor = True
        Me.AdvancedTabPage.VerticalScrollbarBarColor = True
        Me.AdvancedTabPage.VerticalScrollbarHighlightOnWheel = False
        Me.AdvancedTabPage.VerticalScrollbarSize = 10
        '
        'AdvancedPropertyGrid
        '
        Me.AdvancedPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdvancedPropertyGrid.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.AdvancedPropertyGrid.Location = New System.Drawing.Point(3, 3)
        Me.AdvancedPropertyGrid.Margin = New System.Windows.Forms.Padding(0)
        Me.AdvancedPropertyGrid.Name = "AdvancedPropertyGrid"
        Me.AdvancedPropertyGrid.Size = New System.Drawing.Size(530, 467)
        Me.AdvancedPropertyGrid.TabIndex = 0
        '
        'MetroStyleManager1
        '
        Me.MetroStyleManager1.Owner = Me
        Me.MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Green
        '
        'ServerSetter
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(584, 565)
        Me.Controls.Add(Me.SettingTabControl)
        Me.DisplayHeader = False
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ServerSetter"
        Me.Padding = New System.Windows.Forms.Padding(20, 30, 20, 20)
        Me.ShowIcon = False
        Me.Style = MetroFramework.MetroColorStyle.Green
        Me.Text = "伺服器設定"
        Me.SettingTabControl.ResumeLayout(False)
        Me.NormalTabPage.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ServerMemoryMaxBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServerMemoryMinBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TaskGroupBox.ResumeLayout(False)
        Me.TaskControlPanel.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.UpdateGroupBox.ResumeLayout(False)
        Me.UpdateGroupBox.PerformLayout()
        Me.AdvancedTabPage.ResumeLayout(False)
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AdvancedPropertyGrid As PropertyGrid
    Friend WithEvents TaskGroupBox As GroupBox
    Friend WithEvents TaskListBox As CheckedListBox
    Friend WithEvents TaskControlPanel As Panel
    Friend WithEvents RemoveTaskButton As Button
    Friend WithEvents EditTaskButton As Button
    Friend WithEvents AddTaskButton As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents MapPanel As Panel
    Friend WithEvents UpdateGroupBox As GroupBox
    Friend WithEvents UpdateButton As Button
    Friend WithEvents VersionLabel As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents PluginManageButton As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents ServerMemoryMaxBox As NumericUpDown
    Friend WithEvents Label14 As Label
    Friend WithEvents ServerMemoryMinBox As NumericUpDown
    Friend WithEvents Label13 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents SettingTabControl As MetroFramework.Controls.MetroTabControl
    Friend WithEvents NormalTabPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents AdvancedTabPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
End Class
