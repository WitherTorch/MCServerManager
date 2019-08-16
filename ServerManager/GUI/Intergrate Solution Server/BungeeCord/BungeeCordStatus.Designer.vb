<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BungeeCordStatus
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
        Me.BungeeCordName = New System.Windows.Forms.Label()
        Me.BungeeCordVersion = New System.Windows.Forms.Label()
        Me.BungeeCordRunStatus = New System.Windows.Forms.Label()
        Me.RunButton = New System.Windows.Forms.Button()
        Me.SettingButton = New System.Windows.Forms.Button()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'BungeeCordName
        '
        Me.BungeeCordName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BungeeCordName.AutoEllipsis = True
        Me.BungeeCordName.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.BungeeCordName.Font = New System.Drawing.Font("微軟正黑體", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.BungeeCordName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BungeeCordName.Location = New System.Drawing.Point(3, 3)
        Me.BungeeCordName.Margin = New System.Windows.Forms.Padding(3)
        Me.BungeeCordName.Name = "BungeeCordName"
        Me.BungeeCordName.Size = New System.Drawing.Size(680, 34)
        Me.BungeeCordName.TabIndex = 0
        Me.BungeeCordName.Text = "<BungeeCordName>"
        Me.BungeeCordName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BungeeCordVersion
        '
        Me.BungeeCordVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BungeeCordVersion.AutoEllipsis = True
        Me.BungeeCordVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.BungeeCordVersion.Font = New System.Drawing.Font("微軟正黑體", 13.0!)
        Me.BungeeCordVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BungeeCordVersion.Location = New System.Drawing.Point(3, 43)
        Me.BungeeCordVersion.Margin = New System.Windows.Forms.Padding(3)
        Me.BungeeCordVersion.Name = "BungeeCordVersion"
        Me.BungeeCordVersion.Size = New System.Drawing.Size(706, 23)
        Me.BungeeCordVersion.TabIndex = 1
        Me.BungeeCordVersion.Text = "<BungeeCordVersion>"
        Me.BungeeCordVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BungeeCordRunStatus
        '
        Me.BungeeCordRunStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BungeeCordRunStatus.AutoEllipsis = True
        Me.BungeeCordRunStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.BungeeCordRunStatus.Font = New System.Drawing.Font("微軟正黑體", 13.0!)
        Me.BungeeCordRunStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BungeeCordRunStatus.Location = New System.Drawing.Point(3, 72)
        Me.BungeeCordRunStatus.Margin = New System.Windows.Forms.Padding(3)
        Me.BungeeCordRunStatus.Name = "BungeeCordRunStatus"
        Me.BungeeCordRunStatus.Size = New System.Drawing.Size(706, 23)
        Me.BungeeCordRunStatus.TabIndex = 2
        Me.BungeeCordRunStatus.Text = "<BungeeCordRunStatus>"
        Me.BungeeCordRunStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RunButton
        '
        Me.RunButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RunButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.RunButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.RunButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.RunButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RunButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RunButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RunButton.Image = Global.ServerManager.My.Resources.Resources.Run32
        Me.RunButton.Location = New System.Drawing.Point(675, 101)
        Me.RunButton.Name = "RunButton"
        Me.RunButton.Size = New System.Drawing.Size(34, 34)
        Me.RunButton.TabIndex = 3
        Me.RunButton.TabStop = False
        Me.RunButton.Text = ""
        Me.ToolTip1.SetToolTip(Me.RunButton, "啟動整合方案")
        Me.RunButton.UseVisualStyleBackColor = False
        '
        'SettingButton
        '
        Me.SettingButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SettingButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.SettingButton.BackgroundImage = Global.ServerManager.My.Resources.Resources.Setting32
        Me.SettingButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.SettingButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.SettingButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.SettingButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.SettingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SettingButton.Location = New System.Drawing.Point(635, 101)
        Me.SettingButton.Name = "SettingButton"
        Me.SettingButton.Size = New System.Drawing.Size(34, 34)
        Me.SettingButton.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.SettingButton, "設定整合方案")
        Me.SettingButton.UseVisualStyleBackColor = False
        '
        'CloseButton
        '
        Me.CloseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CloseButton.BackgroundImage = Global.ServerManager.My.Resources.Resources.close32
        Me.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CloseButton.Location = New System.Drawing.Point(689, 3)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(20, 20)
        Me.CloseButton.TabIndex = 5
        Me.CloseButton.TabStop = False
        Me.CloseButton.Text = ""
        Me.CloseButton.UseVisualStyleBackColor = False
        '
        'BungeeCordStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.SettingButton)
        Me.Controls.Add(Me.RunButton)
        Me.Controls.Add(Me.BungeeCordRunStatus)
        Me.Controls.Add(Me.BungeeCordVersion)
        Me.Controls.Add(Me.BungeeCordName)
        Me.Margin = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.Name = "BungeeCordStatus"
        Me.Size = New System.Drawing.Size(712, 138)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BungeeCordName As Label
    Friend WithEvents BungeeCordVersion As Label
    Friend WithEvents BungeeCordRunStatus As Label
    Friend WithEvents RunButton As Button
    Friend WithEvents SettingButton As Button
    Friend WithEvents CloseButton As Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
