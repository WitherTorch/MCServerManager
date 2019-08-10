<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BungeeCordServerChooseForm
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
        Me.ServerListPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.SuspendLayout()
        '
        'ServerListPanel
        '
        Me.ServerListPanel.AutoScroll = True
        Me.ServerListPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ServerListPanel.ColumnCount = 1
        Me.ServerListPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ServerListPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ServerListPanel.Location = New System.Drawing.Point(0, 0)
        Me.ServerListPanel.Name = "ServerListPanel"
        Me.ServerListPanel.RowCount = 1
        Me.ServerListPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.ServerListPanel.Size = New System.Drawing.Size(372, 440)
        Me.ServerListPanel.TabIndex = 8
        '
        'BungeeCordServerChooseForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(372, 440)
        Me.Controls.Add(Me.ServerListPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BungeeCordServerChooseForm"
        Me.Text = "選擇要加入BungeeCord的伺服器"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ServerListPanel As TableLayoutPanel
End Class
