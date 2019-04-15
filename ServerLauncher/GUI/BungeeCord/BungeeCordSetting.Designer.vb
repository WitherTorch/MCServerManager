<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BungeeCordSetting
    Inherits System.Windows.Forms.Form

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
        Me.BungeeSetTab = New System.Windows.Forms.TabControl()
        Me.ServerManagePage = New System.Windows.Forms.TabPage()
        Me.ServerListPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.AddServerButton = New System.Windows.Forms.Button()
        Me.ListenerManageTab = New System.Windows.Forms.TabPage()
        Me.RemoveListenerButton = New System.Windows.Forms.Button()
        Me.AddListenerButton = New System.Windows.Forms.Button()
        Me.ListenerPropertyTab = New System.Windows.Forms.TabControl()
        Me.OtherSettingPage = New System.Windows.Forms.TabPage()
        Me.BungeeSettingGrid = New System.Windows.Forms.PropertyGrid()
        Me.BungeeSetTab.SuspendLayout()
        Me.ServerManagePage.SuspendLayout()
        Me.ListenerManageTab.SuspendLayout()
        Me.OtherSettingPage.SuspendLayout()
        Me.SuspendLayout()
        '
        'BungeeSetTab
        '
        Me.BungeeSetTab.Controls.Add(Me.ServerManagePage)
        Me.BungeeSetTab.Controls.Add(Me.ListenerManageTab)
        Me.BungeeSetTab.Controls.Add(Me.OtherSettingPage)
        Me.BungeeSetTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BungeeSetTab.Location = New System.Drawing.Point(0, 0)
        Me.BungeeSetTab.Name = "BungeeSetTab"
        Me.BungeeSetTab.SelectedIndex = 0
        Me.BungeeSetTab.Size = New System.Drawing.Size(584, 461)
        Me.BungeeSetTab.TabIndex = 0
        '
        'ServerManagePage
        '
        Me.ServerManagePage.Controls.Add(Me.ServerListPanel)
        Me.ServerManagePage.Controls.Add(Me.AddServerButton)
        Me.ServerManagePage.Location = New System.Drawing.Point(4, 22)
        Me.ServerManagePage.Name = "ServerManagePage"
        Me.ServerManagePage.Padding = New System.Windows.Forms.Padding(3)
        Me.ServerManagePage.Size = New System.Drawing.Size(576, 435)
        Me.ServerManagePage.TabIndex = 0
        Me.ServerManagePage.Text = "伺服器"
        Me.ServerManagePage.UseVisualStyleBackColor = True
        '
        'ServerListPanel
        '
        Me.ServerListPanel.AutoScroll = True
        Me.ServerListPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ServerListPanel.ColumnCount = 1
        Me.ServerListPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ServerListPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ServerListPanel.Location = New System.Drawing.Point(3, 3)
        Me.ServerListPanel.Name = "ServerListPanel"
        Me.ServerListPanel.RowCount = 1
        Me.ServerListPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ServerListPanel.Size = New System.Drawing.Size(570, 400)
        Me.ServerListPanel.TabIndex = 7
        '
        'AddServerButton
        '
        Me.AddServerButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.AddServerButton.Location = New System.Drawing.Point(3, 403)
        Me.AddServerButton.Name = "AddServerButton"
        Me.AddServerButton.Size = New System.Drawing.Size(570, 29)
        Me.AddServerButton.TabIndex = 8
        Me.AddServerButton.Text = "加入伺服器"
        Me.AddServerButton.UseVisualStyleBackColor = True
        '
        'ListenerManageTab
        '
        Me.ListenerManageTab.Controls.Add(Me.RemoveListenerButton)
        Me.ListenerManageTab.Controls.Add(Me.AddListenerButton)
        Me.ListenerManageTab.Controls.Add(Me.ListenerPropertyTab)
        Me.ListenerManageTab.Location = New System.Drawing.Point(4, 22)
        Me.ListenerManageTab.Name = "ListenerManageTab"
        Me.ListenerManageTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ListenerManageTab.Size = New System.Drawing.Size(576, 435)
        Me.ListenerManageTab.TabIndex = 1
        Me.ListenerManageTab.Text = "監聽器"
        Me.ListenerManageTab.UseVisualStyleBackColor = True
        '
        'RemoveListenerButton
        '
        Me.RemoveListenerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RemoveListenerButton.Location = New System.Drawing.Point(522, 6)
        Me.RemoveListenerButton.Name = "RemoveListenerButton"
        Me.RemoveListenerButton.Size = New System.Drawing.Size(46, 22)
        Me.RemoveListenerButton.TabIndex = 2
        Me.RemoveListenerButton.Text = "刪除"
        Me.RemoveListenerButton.UseVisualStyleBackColor = True
        '
        'AddListenerButton
        '
        Me.AddListenerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddListenerButton.Location = New System.Drawing.Point(470, 6)
        Me.AddListenerButton.Name = "AddListenerButton"
        Me.AddListenerButton.Size = New System.Drawing.Size(46, 22)
        Me.AddListenerButton.TabIndex = 1
        Me.AddListenerButton.Text = "建立"
        Me.AddListenerButton.UseVisualStyleBackColor = True
        '
        'ListenerPropertyTab
        '
        Me.ListenerPropertyTab.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListenerPropertyTab.Location = New System.Drawing.Point(3, 6)
        Me.ListenerPropertyTab.Name = "ListenerPropertyTab"
        Me.ListenerPropertyTab.SelectedIndex = 0
        Me.ListenerPropertyTab.Size = New System.Drawing.Size(570, 426)
        Me.ListenerPropertyTab.TabIndex = 0
        '
        'OtherSettingPage
        '
        Me.OtherSettingPage.Controls.Add(Me.BungeeSettingGrid)
        Me.OtherSettingPage.Location = New System.Drawing.Point(4, 22)
        Me.OtherSettingPage.Name = "OtherSettingPage"
        Me.OtherSettingPage.Padding = New System.Windows.Forms.Padding(3)
        Me.OtherSettingPage.Size = New System.Drawing.Size(576, 435)
        Me.OtherSettingPage.TabIndex = 2
        Me.OtherSettingPage.Text = "其他設定"
        Me.OtherSettingPage.UseVisualStyleBackColor = True
        '
        'BungeeSettingGrid
        '
        Me.BungeeSettingGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BungeeSettingGrid.Location = New System.Drawing.Point(3, 3)
        Me.BungeeSettingGrid.Name = "BungeeSettingGrid"
        Me.BungeeSettingGrid.Size = New System.Drawing.Size(570, 429)
        Me.BungeeSettingGrid.TabIndex = 0
        '
        'BungeeCordSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 461)
        Me.Controls.Add(Me.BungeeSetTab)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BungeeCordSetting"
        Me.ShowIcon = False
        Me.Text = "BungeeCord 設定"
        Me.BungeeSetTab.ResumeLayout(False)
        Me.ServerManagePage.ResumeLayout(False)
        Me.ListenerManageTab.ResumeLayout(False)
        Me.OtherSettingPage.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BungeeSetTab As TabControl
    Friend WithEvents ServerManagePage As TabPage
    Friend WithEvents ListenerManageTab As TabPage
    Friend WithEvents OtherSettingPage As TabPage
    Friend WithEvents BungeeSettingGrid As PropertyGrid
    Friend WithEvents RemoveListenerButton As Button
    Friend WithEvents AddListenerButton As Button
    Friend WithEvents ListenerPropertyTab As TabControl
    Friend WithEvents ServerListPanel As TableLayoutPanel
    Friend WithEvents AddServerButton As Button
End Class
