<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpigotGitStatus
    Inherits Global.ServerManager.ServerStatus

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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.RunButton2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'SettingButton
        '
        MyBase.SettingButton.Location = New System.Drawing.Point(595, 101)
        '
        'ShowDirButton
        '
        MyBase.ShowDirButton.Location = New System.Drawing.Point(555, 101)
        '
        'RunButton
        '
        MyBase.RunButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        MyBase.RunButton.BackColor = System.Drawing.Color.Transparent
        MyBase.RunButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        MyBase.RunButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(203, Byte), Integer))
        MyBase.RunButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        MyBase.RunButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        MyBase.RunButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        MyBase.RunButton.Image = Global.ServerManager.My.Resources.Resources.Run32Spigot
        MyBase.RunButton.Location = New System.Drawing.Point(675, 101)
        MyBase.RunButton.Name = "RunButton"
        MyBase.RunButton.Size = New System.Drawing.Size(34, 34)
        MyBase.RunButton.TabIndex = 3
        MyBase.RunButton.TabStop = False
        MyBase.RunButton.Text = ""
        MyBase.ToolTip1.SetToolTip(Me.RunButton, "啟動 Spigot 伺服器")
        MyBase.RunButton.UseVisualStyleBackColor = False
        '
        'RunButton2
        '
        Me.RunButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RunButton2.BackColor = System.Drawing.Color.Transparent
        Me.RunButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.RunButton2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(203, Byte), Integer))
        Me.RunButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RunButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.RunButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RunButton2.Image = Global.ServerManager.My.Resources.Resources.Run32_Bukkit
        Me.RunButton2.Location = New System.Drawing.Point(635, 101)
        Me.RunButton2.Name = "RunButton2"
        Me.RunButton2.Size = New System.Drawing.Size(34, 34)
        Me.RunButton2.TabIndex = 4
        Me.RunButton2.TabStop = False
        Me.RunButton2.Text = ""
        Me.ToolTip1.SetToolTip(Me.RunButton2, "啟動 CraftBukkit 伺服器")
        Me.RunButton2.UseVisualStyleBackColor = False
        '
        'SpigotGitStatus
        '
        Me.Controls.Add(Me.RunButton2)
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "SpigotGitStatus"
        Me.ResumeLayout(False)
    End Sub
    Friend WithEvents RunButton2 As Button
End Class
