<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerStatus
    Inherits MetroFramework.Controls.MetroUserControl

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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PathNameLabel = New MetroFramework.Controls.MetroLabel()
        Me.ServerVersionLabel = New MetroFramework.Controls.MetroLabel()
        Me.StartServerButton = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SettingServerButton = New System.Windows.Forms.Button()
        Me.ShowFolderButton = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(64, 64)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'PathNameLabel
        '
        Me.PathNameLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PathNameLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.PathNameLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.PathNameLabel.Location = New System.Drawing.Point(75, 5)
        Me.PathNameLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.PathNameLabel.Name = "PathNameLabel"
        Me.PathNameLabel.Size = New System.Drawing.Size(368, 30)
        Me.PathNameLabel.TabIndex = 1
        Me.PathNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerVersionLabel
        '
        Me.ServerVersionLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerVersionLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.ServerVersionLabel.Location = New System.Drawing.Point(75, 41)
        Me.ServerVersionLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.ServerVersionLabel.Name = "ServerVersionLabel"
        Me.ServerVersionLabel.Size = New System.Drawing.Size(368, 23)
        Me.ServerVersionLabel.TabIndex = 2
        Me.ServerVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StartServerButton
        '
        Me.StartServerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StartServerButton.BackColor = System.Drawing.Color.Transparent
        Me.StartServerButton.BackgroundImage = Global.ServerManager.My.Resources.Resources.Run32
        Me.StartServerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.StartServerButton.FlatAppearance.BorderSize = 0
        Me.StartServerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.StartServerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.StartServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.StartServerButton.Location = New System.Drawing.Point(420, 70)
        Me.StartServerButton.Name = "StartServerButton"
        Me.StartServerButton.Size = New System.Drawing.Size(23, 23)
        Me.StartServerButton.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.StartServerButton, "啟動伺服器")
        Me.StartServerButton.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.ToolTip1.ForeColor = System.Drawing.Color.White
        Me.ToolTip1.OwnerDraw = True
        '
        'SettingServerButton
        '
        Me.SettingServerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SettingServerButton.BackColor = System.Drawing.Color.Transparent
        Me.SettingServerButton.BackgroundImage = Global.ServerManager.My.Resources.Resources.Setting32
        Me.SettingServerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.SettingServerButton.FlatAppearance.BorderSize = 0
        Me.SettingServerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.SettingServerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.SettingServerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SettingServerButton.Location = New System.Drawing.Point(391, 70)
        Me.SettingServerButton.Name = "SettingServerButton"
        Me.SettingServerButton.Size = New System.Drawing.Size(23, 23)
        Me.SettingServerButton.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.SettingServerButton, "設定伺服器")
        Me.SettingServerButton.UseVisualStyleBackColor = False
        '
        'ShowFolderButton
        '
        Me.ShowFolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowFolderButton.BackColor = System.Drawing.Color.Transparent
        Me.ShowFolderButton.BackgroundImage = Global.ServerManager.My.Resources.Resources.showDir
        Me.ShowFolderButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ShowFolderButton.FlatAppearance.BorderSize = 0
        Me.ShowFolderButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.ShowFolderButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ShowFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ShowFolderButton.Location = New System.Drawing.Point(362, 70)
        Me.ShowFolderButton.Name = "ShowFolderButton"
        Me.ShowFolderButton.Size = New System.Drawing.Size(23, 23)
        Me.ShowFolderButton.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.ShowFolderButton, "開啟資料夾")
        Me.ShowFolderButton.UseVisualStyleBackColor = False
        '
        'ServerStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.ShowFolderButton)
        Me.Controls.Add(Me.SettingServerButton)
        Me.Controls.Add(Me.StartServerButton)
        Me.Controls.Add(Me.ServerVersionLabel)
        Me.Controls.Add(Me.PathNameLabel)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "ServerStatus"
        Me.Padding = New System.Windows.Forms.Padding(2)
        Me.Size = New System.Drawing.Size(448, 98)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PathNameLabel As MetroFramework.Controls.MetroLabel
    Friend WithEvents ServerVersionLabel As MetroFramework.Controls.MetroLabel
    Friend WithEvents StartServerButton As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SettingServerButton As Button
    Friend WithEvents ShowFolderButton As Button
End Class
