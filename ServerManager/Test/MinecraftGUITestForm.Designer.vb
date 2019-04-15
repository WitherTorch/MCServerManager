<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MinecraftGUITestForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MinecraftGUITestForm))
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New MinecraftGUI.CheckBox()
        Me.Button2 = New MinecraftGUI.Button()
        Me.Button1 = New MinecraftGUI.Button()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 134)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 22)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = "HI"
        '
        'CheckBox1
        '
        Me.CheckBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CheckBox1.Checked = True
        Me.CheckBox1.ForeColor = System.Drawing.Color.Black
        Me.CheckBox1.Location = New System.Drawing.Point(12, 98)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(129, 30)
        Me.CheckBox1.TabIndex = 2
        Me.CheckBox1.Text = "這是個測試"
        '
        'Button2
        '
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button2.Enabled = False
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Image = Nothing
        Me.Button2.Location = New System.Drawing.Point(12, 55)
        Me.Button2.MaximumSize = New System.Drawing.Size(146, 37)
        Me.Button2.MinimumSize = New System.Drawing.Size(146, 37)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(146, 37)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "按鈕(停用)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.SystemColors.Control
        Me.Button1.Image = Nothing
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(146, 37)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "按鈕(啟用)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MinecraftGUITestForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "MinecraftGUITestForm"
        Me.Text = "MinecraftGUITestForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As MinecraftGUI.Button
    Friend WithEvents Button2 As MinecraftGUI.Button
    Friend WithEvents CheckBox1 As MinecraftGUI.CheckBox
    Friend WithEvents TextBox1 As TextBox
End Class
