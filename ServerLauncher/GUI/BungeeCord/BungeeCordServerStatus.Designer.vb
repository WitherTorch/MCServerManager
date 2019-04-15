<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BungeeCordServerStatus
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
        Me.ServerName = New System.Windows.Forms.Label()
        Me.ServerVersion = New System.Windows.Forms.Label()
        Me.ServerAlias = New System.Windows.Forms.Label()
        Me.ServerIcon = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RemoveOrAddButton = New System.Windows.Forms.Button()
        Me.RestrictedCheckBox = New System.Windows.Forms.CheckBox()
        CType(Me.ServerIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ServerName
        '
        Me.ServerName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerName.AutoEllipsis = True
        Me.ServerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ServerName.Font = New System.Drawing.Font("微軟正黑體", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.ServerName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ServerName.Location = New System.Drawing.Point(73, 3)
        Me.ServerName.Margin = New System.Windows.Forms.Padding(3)
        Me.ServerName.Name = "ServerName"
        Me.ServerName.Size = New System.Drawing.Size(636, 34)
        Me.ServerName.TabIndex = 0
        Me.ServerName.Text = "<ServerName>"
        Me.ServerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerVersion
        '
        Me.ServerVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerVersion.AutoEllipsis = True
        Me.ServerVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ServerVersion.Font = New System.Drawing.Font("微軟正黑體", 13.0!)
        Me.ServerVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ServerVersion.Location = New System.Drawing.Point(73, 43)
        Me.ServerVersion.Margin = New System.Windows.Forms.Padding(3)
        Me.ServerVersion.Name = "ServerVersion"
        Me.ServerVersion.Size = New System.Drawing.Size(636, 23)
        Me.ServerVersion.TabIndex = 1
        Me.ServerVersion.Text = "<ServerVersion>"
        Me.ServerVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerAlias
        '
        Me.ServerAlias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerAlias.AutoEllipsis = True
        Me.ServerAlias.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ServerAlias.Font = New System.Drawing.Font("微軟正黑體", 13.0!)
        Me.ServerAlias.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ServerAlias.Location = New System.Drawing.Point(73, 72)
        Me.ServerAlias.Margin = New System.Windows.Forms.Padding(3)
        Me.ServerAlias.Name = "ServerAlias"
        Me.ServerAlias.Size = New System.Drawing.Size(636, 23)
        Me.ServerAlias.TabIndex = 2
        Me.ServerAlias.Text = "<ServerAlias>"
        Me.ServerAlias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerIcon
        '
        Me.ServerIcon.BackColor = System.Drawing.Color.Transparent
        Me.ServerIcon.Image = Global.ServerManager.My.Resources.Resources.ServerDefaultIcon
        Me.ServerIcon.Location = New System.Drawing.Point(3, 3)
        Me.ServerIcon.Name = "ServerIcon"
        Me.ServerIcon.Size = New System.Drawing.Size(64, 64)
        Me.ServerIcon.TabIndex = 6
        Me.ServerIcon.TabStop = False
        '
        'RemoveOrAddButton
        '
        Me.RemoveOrAddButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RemoveOrAddButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.RemoveOrAddButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.RemoveOrAddButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RemoveOrAddButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RemoveOrAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RemoveOrAddButton.ForeColor = System.Drawing.Color.Gainsboro
        Me.RemoveOrAddButton.Location = New System.Drawing.Point(655, 101)
        Me.RemoveOrAddButton.Name = "RemoveOrAddButton"
        Me.RemoveOrAddButton.Size = New System.Drawing.Size(54, 34)
        Me.RemoveOrAddButton.TabIndex = 3
        Me.RemoveOrAddButton.TabStop = False
        Me.RemoveOrAddButton.Text = "移除"
        Me.ToolTip1.SetToolTip(Me.RemoveOrAddButton, "移除伺服器")
        Me.RemoveOrAddButton.UseVisualStyleBackColor = False
        '
        'RestrictedCheckBox
        '
        Me.RestrictedCheckBox.AutoSize = True
        Me.RestrictedCheckBox.Location = New System.Drawing.Point(3, 111)
        Me.RestrictedCheckBox.Name = "RestrictedCheckBox"
        Me.RestrictedCheckBox.Size = New System.Drawing.Size(144, 16)
        Me.RestrictedCheckBox.TabIndex = 7
        Me.RestrictedCheckBox.Text = "是否需要權限才可傳送"
        Me.RestrictedCheckBox.UseVisualStyleBackColor = True
        '
        'BungeeCordServerStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.Controls.Add(Me.RestrictedCheckBox)
        Me.Controls.Add(Me.ServerIcon)
        Me.Controls.Add(Me.RemoveOrAddButton)
        Me.Controls.Add(Me.ServerAlias)
        Me.Controls.Add(Me.ServerVersion)
        Me.Controls.Add(Me.ServerName)
        Me.Margin = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.Name = "BungeeCordServerStatus"
        Me.Size = New System.Drawing.Size(712, 138)
        CType(Me.ServerIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ServerName As Label
    Friend WithEvents ServerVersion As Label
    Friend WithEvents ServerAlias As Label
    Friend WithEvents ServerIcon As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents RemoveOrAddButton As Button
    Friend WithEvents RestrictedCheckBox As CheckBox
End Class
