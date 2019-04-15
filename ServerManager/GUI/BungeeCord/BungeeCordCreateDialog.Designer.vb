<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BungeeCordCreateDialog
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
        Me.SolutionDirBrowseBtn = New System.Windows.Forms.Button()
        Me.SolutionDirBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CreateButton = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SolutionDirBrowseBtn
        '
        Me.SolutionDirBrowseBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SolutionDirBrowseBtn.Location = New System.Drawing.Point(381, 11)
        Me.SolutionDirBrowseBtn.Name = "SolutionDirBrowseBtn"
        Me.SolutionDirBrowseBtn.Size = New System.Drawing.Size(75, 23)
        Me.SolutionDirBrowseBtn.TabIndex = 42
        Me.SolutionDirBrowseBtn.Text = "瀏覽..."
        Me.SolutionDirBrowseBtn.UseVisualStyleBackColor = True
        '
        'SolutionDirBox
        '
        Me.SolutionDirBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SolutionDirBox.Location = New System.Drawing.Point(122, 12)
        Me.SolutionDirBox.Name = "SolutionDirBox"
        Me.SolutionDirBox.Size = New System.Drawing.Size(253, 22)
        Me.SolutionDirBox.TabIndex = 41
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(51, 17)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "方案路徑："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.CreateButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 69)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(459, 31)
        Me.Panel1.TabIndex = 43
        '
        'CreateButton
        '
        Me.CreateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CreateButton.Location = New System.Drawing.Point(381, 5)
        Me.CreateButton.Name = "CreateButton"
        Me.CreateButton.Size = New System.Drawing.Size(75, 23)
        Me.CreateButton.TabIndex = 0
        Me.CreateButton.Text = "建立"
        Me.CreateButton.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 45)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(104, 12)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "BungeeCord 版本："
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BungeeCordCreateDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(459, 100)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.SolutionDirBrowseBtn)
        Me.Controls.Add(Me.SolutionDirBox)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BungeeCordCreateDialog"
        Me.Text = "建立方案"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SolutionDirBrowseBtn As Button
    Friend WithEvents SolutionDirBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CreateButton As Button
    Friend WithEvents Label10 As Label
End Class
