<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DXListView
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
        Me.components = New System.ComponentModel.Container()
        Me.ContextControl = New SharpDX.Windows.RenderControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.VScrollBar1 = New System.Windows.Forms.VScrollBar()
        Me.SuspendLayout()
        '
        'ContextControl
        '
        Me.ContextControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContextControl.Location = New System.Drawing.Point(0, 0)
        Me.ContextControl.Name = "ContextControl"
        Me.ContextControl.Size = New System.Drawing.Size(378, 298)
        Me.ContextControl.TabIndex = 0
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'VScrollBar1
        '
        Me.VScrollBar1.Dock = System.Windows.Forms.DockStyle.Right
        Me.VScrollBar1.Enabled = False
        Me.VScrollBar1.LargeChange = 1
        Me.VScrollBar1.Location = New System.Drawing.Point(378, 0)
        Me.VScrollBar1.Maximum = 0
        Me.VScrollBar1.Name = "VScrollBar1"
        Me.VScrollBar1.Size = New System.Drawing.Size(20, 298)
        Me.VScrollBar1.TabIndex = 1
        '
        'DXListView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.ContextControl)
        Me.Controls.Add(Me.VScrollBar1)
        Me.Name = "DXListView"
        Me.Size = New System.Drawing.Size(398, 298)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ContextControl As SharpDX.Windows.RenderControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents VScrollBar1 As VScrollBar
End Class
