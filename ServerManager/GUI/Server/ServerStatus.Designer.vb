<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ServerStatus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerStatus))
        Me.ServerName = New System.Windows.Forms.Label()
        Me.ServerVersion = New System.Windows.Forms.Label()
        Me.ServerRunStatus = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ShowDirButton = New System.Windows.Forms.Button()
        Me.SettingButton = New System.Windows.Forms.Button()
        Me.RunButton = New System.Windows.Forms.Button()
        Me.VersionTypeLabel = New System.Windows.Forms.Label()
        Me.UPnPStatusLabel = New System.Windows.Forms.Label()
        Me.ServerIcon = New System.Windows.Forms.PictureBox()
        Me.CloseButton = New System.Windows.Forms.Button()
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
        Me.ServerName.Size = New System.Drawing.Size(610, 34)
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
        Me.ServerRunStatus.TabIndex = 2
        Me.ServerRunStatus.Text = "<ServerRunStatus>"
        Me.ServerRunStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.ShowDirButton.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.ShowDirButton, "開啟伺服器資料夾")
        Me.ShowDirButton.UseVisualStyleBackColor = False
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
        Me.ToolTip1.SetToolTip(Me.SettingButton, "設定伺服器")
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
        Me.RunButton.TabIndex = 3
        Me.RunButton.TabStop = False
        Me.RunButton.Text = ""
        Me.ToolTip1.SetToolTip(Me.RunButton, "啟動伺服器")
        Me.RunButton.UseVisualStyleBackColor = False
        '
        'VersionTypeLabel
        '
        Me.VersionTypeLabel.AutoSize = True
        Me.VersionTypeLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.VersionTypeLabel.Location = New System.Drawing.Point(3, 72)
        Me.VersionTypeLabel.Name = "VersionTypeLabel"
        Me.VersionTypeLabel.Size = New System.Drawing.Size(77, 12)
        Me.VersionTypeLabel.TabIndex = 7
        Me.VersionTypeLabel.Text = "<VersionType>"
        '
        'UPnPStatusLabel
        '
        Me.UPnPStatusLabel.AutoSize = True
        Me.UPnPStatusLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.UPnPStatusLabel.Location = New System.Drawing.Point(3, 90)
        Me.UPnPStatusLabel.Name = "UPnPStatusLabel"
        Me.UPnPStatusLabel.Size = New System.Drawing.Size(0, 12)
        Me.UPnPStatusLabel.TabIndex = 8
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
        'ServerStatus
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.Controls.Add(Me.ShowDirButton)
        Me.Controls.Add(Me.UPnPStatusLabel)
        Me.Controls.Add(Me.VersionTypeLabel)
        Me.Controls.Add(Me.ServerIcon)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.SettingButton)
        Me.Controls.Add(Me.RunButton)
        Me.Controls.Add(Me.ServerRunStatus)
        Me.Controls.Add(Me.ServerVersion)
        Me.Controls.Add(Me.ServerName)
        Me.Margin = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.Name = "ServerStatus"
        Me.Size = New System.Drawing.Size(712, 138)
        CType(Me.ServerIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ServerName As Label
    Friend WithEvents ServerVersion As Label
    Friend WithEvents ServerRunStatus As Label
    Friend WithEvents RunButton As Button
    Friend WithEvents SettingButton As Button
    Friend WithEvents CloseButton As Button
    Friend WithEvents ServerIcon As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents VersionTypeLabel As Label
    Friend WithEvents UPnPStatusLabel As Label
    Friend WithEvents ShowDirButton As Button
End Class
