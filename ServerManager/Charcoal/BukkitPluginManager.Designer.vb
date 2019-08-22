<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BukkitPluginManager
    Inherits MetroFramework.Forms.MetroForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BukkitPluginManager))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.瀏覽插件ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.移除插件ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.重新整理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PluginList = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MenuStrip1.SuspendLayout()
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
        Me.瀏覽插件ToolStripMenuItem.Size = New System.Drawing.Size(67, 20)
        Me.瀏覽插件ToolStripMenuItem.Text = "瀏覽插件"
        '
        '移除插件ToolStripMenuItem
        '
        Me.移除插件ToolStripMenuItem.Name = "移除插件ToolStripMenuItem"
        Me.移除插件ToolStripMenuItem.Size = New System.Drawing.Size(67, 20)
        Me.移除插件ToolStripMenuItem.Text = "移除插件"
        '
        '重新整理ToolStripMenuItem
        '
        Me.重新整理ToolStripMenuItem.Name = "重新整理ToolStripMenuItem"
        Me.重新整理ToolStripMenuItem.Size = New System.Drawing.Size(67, 20)
        Me.重新整理ToolStripMenuItem.Text = "重新整理"
        '
        'PluginList
        '
        Me.PluginList.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.PluginList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader4, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.PluginList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PluginList.GridLines = True
        Me.PluginList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.PluginList.HideSelection = False
        Me.PluginList.Location = New System.Drawing.Point(20, 84)
        Me.PluginList.MultiSelect = False
        Me.PluginList.Name = "PluginList"
        Me.PluginList.Size = New System.Drawing.Size(760, 346)
        Me.PluginList.TabIndex = 1
        Me.PluginList.UseCompatibleStateImageBehavior = False
        Me.PluginList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "名稱"
        Me.ColumnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader1.Width = 116
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "版本"
        Me.ColumnHeader4.Width = 76
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "版本發布時間"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader2.Width = 113
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "檔案路徑"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader3.Width = 554
        '
        'BukkitPluginManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.PluginList)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "BukkitPluginManager"
        Me.ShowIcon = False
        Me.Text = "Bukkit 插件管理員"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents 瀏覽插件ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 移除插件ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PluginList As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents 重新整理ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ColumnHeader4 As ColumnHeader
End Class
