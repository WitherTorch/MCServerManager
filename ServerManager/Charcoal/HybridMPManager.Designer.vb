<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HybridMPManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HybridMPManager))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.瀏覽插件ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.移除插件ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.重新整理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PagesControl = New MetroFramework.Controls.MetroTabControl()
        Me.TabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.ModList = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.PluginList = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MenuStrip1.SuspendLayout()
        Me.PagesControl.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.White
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.瀏覽插件ToolStripMenuItem, Me.移除插件ToolStripMenuItem, Me.重新整理ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(20, 60)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(760, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        '瀏覽插件ToolStripMenuItem
        '
        Me.瀏覽插件ToolStripMenuItem.Name = "瀏覽插件ToolStripMenuItem"
        Me.瀏覽插件ToolStripMenuItem.Size = New System.Drawing.Size(96, 20)
        Me.瀏覽插件ToolStripMenuItem.Text = "瀏覽插件/模組"
        '
        '移除插件ToolStripMenuItem
        '
        Me.移除插件ToolStripMenuItem.Name = "移除插件ToolStripMenuItem"
        Me.移除插件ToolStripMenuItem.Size = New System.Drawing.Size(96, 20)
        Me.移除插件ToolStripMenuItem.Text = "移除插件/模組"
        '
        '重新整理ToolStripMenuItem
        '
        Me.重新整理ToolStripMenuItem.Name = "重新整理ToolStripMenuItem"
        Me.重新整理ToolStripMenuItem.Size = New System.Drawing.Size(67, 20)
        Me.重新整理ToolStripMenuItem.Text = "重新整理"
        '
        'PagesControl
        '
        Me.PagesControl.Controls.Add(Me.TabPage2)
        Me.PagesControl.Controls.Add(Me.TabPage1)
        Me.PagesControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PagesControl.Location = New System.Drawing.Point(20, 84)
        Me.PagesControl.Name = "PagesControl"
        Me.PagesControl.SelectedIndex = 0
        Me.PagesControl.Size = New System.Drawing.Size(760, 346)
        Me.PagesControl.TabIndex = 2
        Me.PagesControl.UseSelectable = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ModList)
        Me.TabPage2.HorizontalScrollbarBarColor = True
        Me.TabPage2.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage2.HorizontalScrollbarSize = 10
        Me.TabPage2.Location = New System.Drawing.Point(4, 38)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(752, 304)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "模組"
        Me.TabPage2.UseVisualStyleBackColor = True
        Me.TabPage2.VerticalScrollbarBarColor = True
        Me.TabPage2.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage2.VerticalScrollbarSize = 10
        '
        'ModList
        '
        Me.ModList.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ModList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader8})
        Me.ModList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ModList.GridLines = True
        Me.ModList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ModList.HideSelection = False
        Me.ModList.Location = New System.Drawing.Point(3, 3)
        Me.ModList.MultiSelect = False
        Me.ModList.Name = "ModList"
        Me.ModList.Size = New System.Drawing.Size(746, 298)
        Me.ModList.TabIndex = 3
        Me.ModList.UseCompatibleStateImageBehavior = False
        Me.ModList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "名稱"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader4.Width = 116
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "版本"
        Me.ColumnHeader5.Width = 76
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "版本發布時間"
        Me.ColumnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader6.Width = 113
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "檔案路徑"
        Me.ColumnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader8.Width = 554
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.PluginList)
        Me.TabPage1.HorizontalScrollbarBarColor = True
        Me.TabPage1.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage1.HorizontalScrollbarSize = 10
        Me.TabPage1.Location = New System.Drawing.Point(4, 38)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(792, 384)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "插件"
        Me.TabPage1.UseVisualStyleBackColor = True
        Me.TabPage1.VerticalScrollbarBarColor = True
        Me.TabPage1.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage1.VerticalScrollbarSize = 10
        '
        'PluginList
        '
        Me.PluginList.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.PluginList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader7})
        Me.PluginList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PluginList.GridLines = True
        Me.PluginList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.PluginList.HideSelection = False
        Me.PluginList.Location = New System.Drawing.Point(3, 3)
        Me.PluginList.MultiSelect = False
        Me.PluginList.Name = "PluginList"
        Me.PluginList.Size = New System.Drawing.Size(786, 378)
        Me.PluginList.TabIndex = 2
        Me.PluginList.UseCompatibleStateImageBehavior = False
        Me.PluginList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "名稱"
        Me.ColumnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader1.Width = 116
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "版本"
        Me.ColumnHeader2.Width = 76
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "版本發布時間"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader3.Width = 113
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "檔案路徑"
        Me.ColumnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader7.Width = 554
        '
        'HybridMPManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.PagesControl)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "HybridMPManager"
        Me.ShowIcon = False
        Me.Text = "Cauldron 插件/模組管理員"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.PagesControl.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents 瀏覽插件ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 移除插件ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 重新整理ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PluginList As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ModList As ListView
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents PagesControl As MetroFramework.Controls.MetroTabControl
    Friend WithEvents TabPage2 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents TabPage1 As MetroFramework.Controls.MetroTabPage
End Class
