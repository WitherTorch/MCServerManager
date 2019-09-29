<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModManager
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.瀏覽模組ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.移除模組ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.重新整理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModList = New System.Windows.Forms.ListView()
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
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.瀏覽模組ToolStripMenuItem, Me.移除模組ToolStripMenuItem, Me.重新整理ToolStripMenuItem})
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
        '瀏覽模組ToolStripMenuItem
        '
        Me.瀏覽模組ToolStripMenuItem.Name = "瀏覽模組ToolStripMenuItem"
        Me.瀏覽模組ToolStripMenuItem.Size = New System.Drawing.Size(67, 20)
        Me.瀏覽模組ToolStripMenuItem.Text = "瀏覽模組"
        '
        '移除模組ToolStripMenuItem
        '
        Me.移除模組ToolStripMenuItem.Name = "移除模組ToolStripMenuItem"
        Me.移除模組ToolStripMenuItem.Size = New System.Drawing.Size(67, 20)
        Me.移除模組ToolStripMenuItem.Text = "移除模組"
        '
        '重新整理ToolStripMenuItem
        '
        Me.重新整理ToolStripMenuItem.Name = "重新整理ToolStripMenuItem"
        Me.重新整理ToolStripMenuItem.Size = New System.Drawing.Size(67, 20)
        Me.重新整理ToolStripMenuItem.Text = "重新整理"
        '
        'ModList
        '
        Me.ModList.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ModList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader4, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ModList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ModList.GridLines = True
        Me.ModList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ModList.HideSelection = False
        Me.ModList.Location = New System.Drawing.Point(20, 84)
        Me.ModList.MultiSelect = False
        Me.ModList.Name = "ModList"
        Me.ModList.Size = New System.Drawing.Size(760, 346)
        Me.ModList.TabIndex = 2
        Me.ModList.UseCompatibleStateImageBehavior = False
        Me.ModList.View = System.Windows.Forms.View.Details
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
        'ForgeModManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ModList)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "ForgeModManager"
        Me.ShowIcon = False
        Me.Text = "Forge 模組管理員"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents 瀏覽模組ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 移除模組ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 重新整理ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ModList As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
End Class
