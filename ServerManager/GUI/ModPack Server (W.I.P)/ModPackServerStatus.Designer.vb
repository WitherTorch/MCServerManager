<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModPackServerStatus
    Inherits System.Windows.Forms.UserControl

    'UserControl 覆寫 Dispose 以清除元件清單。
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ModPackServerStatus))
        Me.ShowDirButton = New System.Windows.Forms.Button()
        Me.ServerIcon = New System.Windows.Forms.PictureBox()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.SettingButton = New System.Windows.Forms.Button()
        Me.RunButton = New System.Windows.Forms.Button()
        Me.ServerRunStatus = New System.Windows.Forms.Label()
        Me.PackInfo = New System.Windows.Forms.Label()
        Me.ServerName = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.ServerIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ShowDirButton
        '
        Me.ShowDirButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowDirButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.ShowDirButton.BackgroundImage = CType(resources.GetObject("ShowDirButton.BackgroundImage"), System.Drawing.Image)
        Me.ShowDirButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ShowDirButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.ShowDirButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ShowDirButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ShowDirButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ShowDirButton.Location = New System.Drawing.Point(595, 101)
        Me.ShowDirButton.Name = "ShowDirButton"
        Me.ShowDirButton.Size = New System.Drawing.Size(34, 34)
        Me.ShowDirButton.TabIndex = 17
        Me.ShowDirButton.UseVisualStyleBackColor = False
        '
        'ServerIcon
        '
        Me.ServerIcon.BackColor = System.Drawing.Color.Transparent
        Me.ServerIcon.Image = Global.ServerManager.My.Resources.Resources.ServerDefaultIcon
        Me.ServerIcon.Location = New System.Drawing.Point(3, 3)
        Me.ServerIcon.Name = "ServerIcon"
        Me.ServerIcon.Size = New System.Drawing.Size(64, 64)
        Me.ServerIcon.TabIndex = 16
        Me.ServerIcon.TabStop = False
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
        Me.CloseButton.TabIndex = 15
        Me.CloseButton.TabStop = False
        Me.CloseButton.Text = ""
        Me.CloseButton.UseVisualStyleBackColor = False
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
        Me.SettingButton.TabIndex = 14
        Me.SettingButton.UseVisualStyleBackColor = False
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
        Me.RunButton.TabIndex = 13
        Me.RunButton.TabStop = False
        Me.RunButton.Text = ""
        Me.RunButton.UseVisualStyleBackColor = False
        '
        'ServerRunStatus
        '
        Me.ServerRunStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerRunStatus.AutoEllipsis = True
        Me.ServerRunStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ServerRunStatus.Font = New System.Drawing.Font("微軟正黑體", 13.0!)
        Me.ServerRunStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ServerRunStatus.Location = New System.Drawing.Point(73, 72)
        Me.ServerRunStatus.Margin = New System.Windows.Forms.Padding(3)
        Me.ServerRunStatus.Name = "ServerRunStatus"
        Me.ServerRunStatus.Size = New System.Drawing.Size(636, 23)
        Me.ServerRunStatus.TabIndex = 12
        Me.ServerRunStatus.Text = "<ServerRunStatus>"
        Me.ServerRunStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PackInfo
        '
        Me.PackInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PackInfo.AutoEllipsis = True
        Me.PackInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.PackInfo.Font = New System.Drawing.Font("微軟正黑體", 13.0!)
        Me.PackInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.PackInfo.Location = New System.Drawing.Point(73, 43)
        Me.PackInfo.Margin = New System.Windows.Forms.Padding(3)
        Me.PackInfo.Name = "PackInfo"
        Me.PackInfo.Size = New System.Drawing.Size(636, 23)
        Me.PackInfo.TabIndex = 11
        Me.PackInfo.Text = "<PackInfo>"
        Me.PackInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.ServerName.Size = New System.Drawing.Size(610, 34)
        Me.ServerName.TabIndex = 10
        Me.ServerName.Text = "<ServerName>"
        Me.ServerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ModPackServerStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.Controls.Add(Me.ShowDirButton)
        Me.Controls.Add(Me.ServerIcon)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.SettingButton)
        Me.Controls.Add(Me.RunButton)
        Me.Controls.Add(Me.ServerRunStatus)
        Me.Controls.Add(Me.PackInfo)
        Me.Controls.Add(Me.ServerName)
        Me.Name = "ModPackServerStatus"
        Me.Size = New System.Drawing.Size(712, 138)
        CType(Me.ServerIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ShowDirButton As Button
    Friend WithEvents ServerIcon As PictureBox
    Friend WithEvents CloseButton As Button
    Friend WithEvents SettingButton As Button
    Friend WithEvents RunButton As Button
    Friend WithEvents ServerRunStatus As Label
    Friend WithEvents PackInfo As Label
    Friend WithEvents ServerName As Label
    Friend WithEvents ToolTip1 As ToolTip
End Class
