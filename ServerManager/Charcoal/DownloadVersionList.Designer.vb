<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DownloadVersionList
    Inherits System.Windows.Forms.UserControl

    'UserControl 覆寫 Dispose 以清除元件清單。
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
        Me.VersionList = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.NaviBar = New System.Windows.Forms.ToolStrip()
        Me.SuspendLayout()
        '
        'VersionList
        '
        Me.VersionList.Activation = System.Windows.Forms.ItemActivation.TwoClick
        Me.VersionList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6})
        Me.VersionList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionList.FullRowSelect = True
        Me.VersionList.GridLines = True
        Me.VersionList.HideSelection = False
        Me.VersionList.Location = New System.Drawing.Point(0, 25)
        Me.VersionList.MultiSelect = False
        Me.VersionList.Name = "VersionList"
        Me.VersionList.ShowItemToolTips = True
        Me.VersionList.Size = New System.Drawing.Size(546, 392)
        Me.VersionList.TabIndex = 0
        Me.VersionList.UseCompatibleStateImageBehavior = False
        Me.VersionList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "發布類型"
        Me.ColumnHeader1.Width = 62
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "名稱"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader2.Width = 291
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "檔案大小"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "發布時間"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader4.Width = 94
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "遊戲版本"
        Me.ColumnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader5.Width = 100
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "下載次數"
        Me.ColumnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader6.Width = 104
        '
        'NaviBar
        '
        Me.NaviBar.BackColor = System.Drawing.Color.White
        Me.NaviBar.Location = New System.Drawing.Point(0, 0)
        Me.NaviBar.Name = "NaviBar"
        Me.NaviBar.Size = New System.Drawing.Size(546, 25)
        Me.NaviBar.TabIndex = 1
        Me.NaviBar.Text = "ToolStrip1"
        '
        'DownloadVersionList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.VersionList)
        Me.Controls.Add(Me.NaviBar)
        Me.Name = "DownloadVersionList"
        Me.Size = New System.Drawing.Size(546, 417)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents VersionList As ListView
    Private WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents NaviBar As ToolStrip
End Class
