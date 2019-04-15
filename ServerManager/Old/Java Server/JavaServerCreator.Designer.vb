<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JavaServerCreator
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ProgressText = New System.Windows.Forms.Label()
        Me.Progress = New System.Windows.Forms.ProgressBar()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ProgressText, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Progress, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(800, 123)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ProgressText
        '
        Me.ProgressText.AutoSize = True
        Me.ProgressText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProgressText.Font = New System.Drawing.Font("微軟正黑體", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ProgressText.Location = New System.Drawing.Point(3, 0)
        Me.ProgressText.Name = "ProgressText"
        Me.ProgressText.Size = New System.Drawing.Size(794, 61)
        Me.ProgressText.TabIndex = 0
        Me.ProgressText.Text = "狀態"
        Me.ProgressText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Progress
        '
        Me.Progress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Progress.Location = New System.Drawing.Point(3, 64)
        Me.Progress.Name = "Progress"
        Me.Progress.Size = New System.Drawing.Size(794, 56)
        Me.Progress.TabIndex = 1
        '
        'ServerCreator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 123)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ServerCreator"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ProgressText As Label
    Friend WithEvents Progress As ProgressBar
End Class
