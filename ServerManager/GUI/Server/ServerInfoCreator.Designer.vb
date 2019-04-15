<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerInfoCreator
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.VersionTypeBox = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.VersionBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(53, 12)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "遊戲類型："
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'VersionTypeBox
        '
        Me.VersionTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VersionTypeBox.FormattingEnabled = True
        Me.VersionTypeBox.Items.AddRange(New Object() {"Java - 原版", "Java - Forge", "Java - Spigot", "Java - CraftBukkit", "Java - SpongeVanilla", "Bedrock - Nukkit"})
        Me.VersionTypeBox.Location = New System.Drawing.Point(124, 8)
        Me.VersionTypeBox.Name = "VersionTypeBox"
        Me.VersionTypeBox.Size = New System.Drawing.Size(320, 20)
        Me.VersionTypeBox.TabIndex = 31
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(53, 38)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 12)
        Me.Label10.TabIndex = 32
        Me.Label10.Text = "遊戲版本："
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'VersionBox
        '
        Me.VersionBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VersionBox.FormattingEnabled = True
        Me.VersionBox.Location = New System.Drawing.Point(124, 34)
        Me.VersionBox.Name = "VersionBox"
        Me.VersionBox.Size = New System.Drawing.Size(320, 20)
        Me.VersionBox.TabIndex = 33
        '
        'Label1
        '
        Me.Label1.Enabled = False
        Me.Label1.Location = New System.Drawing.Point(12, 59)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 22)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "次要版本："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(369, 91)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 36
        Me.Button1.Text = "完成"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(124, 60)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(320, 20)
        Me.ComboBox1.TabIndex = 37
        '
        'ServerInfoCreator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 117)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.VersionTypeBox)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.VersionBox)
        Me.Name = "ServerInfoCreator"
        Me.Text = "建立伺服器資訊"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents VersionTypeBox As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents VersionBox As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents ComboBox1 As ComboBox
End Class
