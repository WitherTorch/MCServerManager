<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SolutionStatus
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
        Me.ServerVersionLabel = New MetroFramework.Controls.MetroLabel()
        Me.PathNameLabel = New MetroFramework.Controls.MetroLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.StartServerButton = New System.Windows.Forms.Button()
        Me.ShowFolderButton = New System.Windows.Forms.Button()
        Me.SettingServerButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ServerVersionLabel
        '
        Me.ServerVersionLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerVersionLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.ServerVersionLabel.Location = New System.Drawing.Point(3, 41)
        Me.ServerVersionLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.ServerVersionLabel.Name = "ServerVersionLabel"
        Me.ServerVersionLabel.Size = New System.Drawing.Size(440, 23)
        Me.ServerVersionLabel.TabIndex = 11
        Me.ServerVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PathNameLabel
        '
        Me.PathNameLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PathNameLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.PathNameLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.PathNameLabel.Location = New System.Drawing.Point(3, 5)
        Me.PathNameLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.PathNameLabel.Name = "PathNameLabel"
        Me.PathNameLabel.Size = New System.Drawing.Size(440, 30)
        Me.PathNameLabel.TabIndex = 10
        Me.PathNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolTip1
        '
        Me.ToolTip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.ToolTip1.ForeColor = System.Drawing.Color.White
        Me.ToolTip1.OwnerDraw = True
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
        Me.StartServerButton.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.StartServerButton, "啟動方案")
        Me.StartServerButton.UseVisualStyleBackColor = False
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
        Me.ShowFolderButton.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.ShowFolderButton, "開啟資料夾")
        Me.ShowFolderButton.UseVisualStyleBackColor = False
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
        Me.SettingServerButton.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.SettingServerButton, "設定方案")
        Me.SettingServerButton.UseVisualStyleBackColor = False
        '
        'SolutionStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.StartServerButton)
        Me.Controls.Add(Me.ServerVersionLabel)
        Me.Controls.Add(Me.PathNameLabel)
        Me.Controls.Add(Me.ShowFolderButton)
        Me.Controls.Add(Me.SettingServerButton)
        Me.Name = "SolutionStatus"
        Me.Size = New System.Drawing.Size(448, 98)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents StartServerButton As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ServerVersionLabel As MetroFramework.Controls.MetroLabel
    Friend WithEvents PathNameLabel As MetroFramework.Controls.MetroLabel
    Friend WithEvents ShowFolderButton As Button
    Friend WithEvents SettingServerButton As Button
End Class
