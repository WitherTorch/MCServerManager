<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BungeeCordSetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BungeeCordSetting))
        Me.BungeeSetTab = New MetroFramework.Controls.MetroTabControl()
        Me.ServerManagePage = New MetroFramework.Controls.MetroTabPage()
        Me.ServerListPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.AddServerButton = New System.Windows.Forms.Button()
        Me.ListenerManageTab = New MetroFramework.Controls.MetroTabPage()
        Me.RemoveListenerButton = New System.Windows.Forms.Button()
        Me.AddListenerButton = New System.Windows.Forms.Button()
        Me.ListenerPropertyTab = New MetroFramework.Controls.MetroTabControl()
        Me.OtherSettingPage = New MetroFramework.Controls.MetroTabPage()
        Me.BungeeSettingGrid = New System.Windows.Forms.PropertyGrid()
        Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.BungeeSetTab.SuspendLayout()
        Me.ServerManagePage.SuspendLayout()
        Me.ListenerManageTab.SuspendLayout()
        Me.OtherSettingPage.SuspendLayout()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BungeeSetTab
        '
        Me.BungeeSetTab.Controls.Add(Me.ServerManagePage)
        Me.BungeeSetTab.Controls.Add(Me.ListenerManageTab)
        Me.BungeeSetTab.Controls.Add(Me.OtherSettingPage)
        Me.BungeeSetTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BungeeSetTab.FontWeight = MetroFramework.MetroTabControlWeight.Regular
        Me.BungeeSetTab.Location = New System.Drawing.Point(20, 30)
        Me.BungeeSetTab.Name = "BungeeSetTab"
        Me.BungeeSetTab.SelectedIndex = 0
        Me.BungeeSetTab.Size = New System.Drawing.Size(544, 411)
        Me.BungeeSetTab.Style = MetroFramework.MetroColorStyle.Green
        Me.BungeeSetTab.TabIndex = 0
        Me.BungeeSetTab.UseSelectable = True
        '
        'ServerManagePage
        '
        Me.ServerManagePage.Controls.Add(Me.ServerListPanel)
        Me.ServerManagePage.Controls.Add(Me.AddServerButton)
        Me.ServerManagePage.HorizontalScrollbarBarColor = True
        Me.ServerManagePage.HorizontalScrollbarHighlightOnWheel = False
        Me.ServerManagePage.HorizontalScrollbarSize = 10
        Me.ServerManagePage.Location = New System.Drawing.Point(4, 38)
        Me.ServerManagePage.Name = "ServerManagePage"
        Me.ServerManagePage.Padding = New System.Windows.Forms.Padding(3)
        Me.ServerManagePage.Size = New System.Drawing.Size(536, 369)
        Me.ServerManagePage.TabIndex = 0
        Me.ServerManagePage.Text = "伺服器"
        Me.ServerManagePage.UseVisualStyleBackColor = True
        Me.ServerManagePage.VerticalScrollbarBarColor = True
        Me.ServerManagePage.VerticalScrollbarHighlightOnWheel = False
        Me.ServerManagePage.VerticalScrollbarSize = 10
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
        Me.ServerListPanel.Size = New System.Drawing.Size(530, 334)
        Me.ServerListPanel.TabIndex = 7
        '
        'AddServerButton
        '
        Me.AddServerButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.AddServerButton.Location = New System.Drawing.Point(3, 337)
        Me.AddServerButton.Name = "AddServerButton"
        Me.AddServerButton.Size = New System.Drawing.Size(530, 29)
        Me.AddServerButton.TabIndex = 8
        Me.AddServerButton.Text = "加入伺服器"
        Me.AddServerButton.UseVisualStyleBackColor = True
        '
        'ListenerManageTab
        '
        Me.ListenerManageTab.Controls.Add(Me.RemoveListenerButton)
        Me.ListenerManageTab.Controls.Add(Me.AddListenerButton)
        Me.ListenerManageTab.Controls.Add(Me.ListenerPropertyTab)
        Me.ListenerManageTab.HorizontalScrollbarBarColor = True
        Me.ListenerManageTab.HorizontalScrollbarHighlightOnWheel = False
        Me.ListenerManageTab.HorizontalScrollbarSize = 10
        Me.ListenerManageTab.Location = New System.Drawing.Point(4, 36)
        Me.ListenerManageTab.Name = "ListenerManageTab"
        Me.ListenerManageTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ListenerManageTab.Size = New System.Drawing.Size(536, 371)
        Me.ListenerManageTab.TabIndex = 1
        Me.ListenerManageTab.Text = "監聽器"
        Me.ListenerManageTab.UseVisualStyleBackColor = True
        Me.ListenerManageTab.VerticalScrollbarBarColor = True
        Me.ListenerManageTab.VerticalScrollbarHighlightOnWheel = False
        Me.ListenerManageTab.VerticalScrollbarSize = 10
        '
        'RemoveListenerButton
        '
        Me.RemoveListenerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RemoveListenerButton.Location = New System.Drawing.Point(487, 6)
        Me.RemoveListenerButton.Name = "RemoveListenerButton"
        Me.RemoveListenerButton.Size = New System.Drawing.Size(46, 22)
        Me.RemoveListenerButton.TabIndex = 2
        Me.RemoveListenerButton.Text = "刪除"
        Me.RemoveListenerButton.UseVisualStyleBackColor = True
        '
        'AddListenerButton
        '
        Me.AddListenerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddListenerButton.Location = New System.Drawing.Point(435, 6)
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
        Me.ListenerPropertyTab.Size = New System.Drawing.Size(533, 360)
        Me.ListenerPropertyTab.TabIndex = 0
        Me.ListenerPropertyTab.UseSelectable = True
        '
        'OtherSettingPage
        '
        Me.OtherSettingPage.Controls.Add(Me.BungeeSettingGrid)
        Me.OtherSettingPage.HorizontalScrollbarBarColor = True
        Me.OtherSettingPage.HorizontalScrollbarHighlightOnWheel = False
        Me.OtherSettingPage.HorizontalScrollbarSize = 10
        Me.OtherSettingPage.Location = New System.Drawing.Point(4, 36)
        Me.OtherSettingPage.Name = "OtherSettingPage"
        Me.OtherSettingPage.Padding = New System.Windows.Forms.Padding(3)
        Me.OtherSettingPage.Size = New System.Drawing.Size(536, 371)
        Me.OtherSettingPage.TabIndex = 2
        Me.OtherSettingPage.Text = "其他設定"
        Me.OtherSettingPage.UseVisualStyleBackColor = True
        Me.OtherSettingPage.VerticalScrollbarBarColor = True
        Me.OtherSettingPage.VerticalScrollbarHighlightOnWheel = False
        Me.OtherSettingPage.VerticalScrollbarSize = 10
        '
        'BungeeSettingGrid
        '
        Me.BungeeSettingGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BungeeSettingGrid.Location = New System.Drawing.Point(3, 3)
        Me.BungeeSettingGrid.Name = "BungeeSettingGrid"
        Me.BungeeSettingGrid.Size = New System.Drawing.Size(530, 365)
        Me.BungeeSettingGrid.TabIndex = 0
        '
        'MetroStyleManager1
        '
        Me.MetroStyleManager1.Owner = Me
        Me.MetroStyleManager1.Style = MetroFramework.MetroColorStyle.Green
        '
        'BungeeCordSetting
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(584, 461)
        Me.Controls.Add(Me.BungeeSetTab)
        Me.DisplayHeader = False
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BungeeCordSetting"
        Me.Padding = New System.Windows.Forms.Padding(20, 30, 20, 20)
        Me.Resizable = False
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow
        Me.ShowIcon = False
        Me.Style = MetroFramework.MetroColorStyle.Green
        Me.Text = "BungeeCord 設定"
        Me.BungeeSetTab.ResumeLayout(False)
        Me.ServerManagePage.ResumeLayout(False)
        Me.ListenerManageTab.ResumeLayout(False)
        Me.OtherSettingPage.ResumeLayout(False)
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BungeeSettingGrid As PropertyGrid
    Friend WithEvents RemoveListenerButton As Button
    Friend WithEvents AddListenerButton As Button
    Friend WithEvents ServerListPanel As TableLayoutPanel
    Friend WithEvents AddServerButton As Button
    Friend WithEvents BungeeSetTab As MetroFramework.Controls.MetroTabControl
    Friend WithEvents ServerManagePage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents ListenerManageTab As MetroFramework.Controls.MetroTabPage
    Friend WithEvents OtherSettingPage As MetroFramework.Controls.MetroTabPage
    Friend WithEvents ListenerPropertyTab As MetroFramework.Controls.MetroTabControl
    Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
End Class
