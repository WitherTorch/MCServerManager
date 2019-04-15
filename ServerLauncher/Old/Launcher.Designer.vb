<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Launcher
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Launcher))
        Me.Tip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ServerManagerTabs = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.JavaServerManager = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.BedrockServerManager = New System.Windows.Forms.TabControl()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ServerGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.MemoryMaxBox = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.MemoryMinBox = New System.Windows.Forms.NumericUpDown()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.MemoryBox = New System.Windows.Forms.GroupBox()
        Me.VanillaLoadingLabel = New System.Windows.Forms.Label()
        Me.ForgeLoadingLabel = New System.Windows.Forms.Label()
        Me.SpigotLoadingLabel = New System.Windows.Forms.Label()
        Me.CraftBukkitLoadingLabel = New System.Windows.Forms.Label()
        Me.VersionListReloadButton = New System.Windows.Forms.Button()
        Me.NukkitLoadingLabel = New System.Windows.Forms.Label()
        Me.VListLoadingBox = New System.Windows.Forms.GroupBox()
        Me.IPLabel = New System.Windows.Forms.Label()
        Me.RouterLabel = New System.Windows.Forms.Label()
        Me.ChangeRouterBtn = New System.Windows.Forms.Button()
        Me.UPnPLabel = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.UPnPIPLabel = New System.Windows.Forms.Label()
        Me.NetworkGroupBox = New System.Windows.Forms.GroupBox()
        Me.ServerManagerTabs.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.ServerGroupBox.SuspendLayout()
        CType(Me.MemoryMaxBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MemoryMinBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MemoryBox.SuspendLayout()
        Me.VListLoadingBox.SuspendLayout()
        Me.NetworkGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'ServerManagerTabs
        '
        Me.ServerManagerTabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerManagerTabs.Controls.Add(Me.TabPage1)
        Me.ServerManagerTabs.Controls.Add(Me.TabPage2)
        Me.ServerManagerTabs.ImageList = Me.ImageList1
        Me.ServerManagerTabs.Location = New System.Drawing.Point(1, 1)
        Me.ServerManagerTabs.Margin = New System.Windows.Forms.Padding(0, 1, 1, 1)
        Me.ServerManagerTabs.Name = "ServerManagerTabs"
        Me.ServerManagerTabs.SelectedIndex = 0
        Me.ServerManagerTabs.Size = New System.Drawing.Size(627, 546)
        Me.ServerManagerTabs.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.JavaServerManager)
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 31)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(619, 511)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Java 版"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'JavaServerManager
        '
        Me.JavaServerManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JavaServerManager.Location = New System.Drawing.Point(3, 3)
        Me.JavaServerManager.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.JavaServerManager.Name = "JavaServerManager"
        Me.JavaServerManager.SelectedIndex = 0
        Me.JavaServerManager.Size = New System.Drawing.Size(613, 505)
        Me.JavaServerManager.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.BedrockServerManager)
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 31)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(619, 511)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "基岩版"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'BedrockServerManager
        '
        Me.BedrockServerManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BedrockServerManager.Location = New System.Drawing.Point(3, 3)
        Me.BedrockServerManager.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.BedrockServerManager.Name = "BedrockServerManager"
        Me.BedrockServerManager.SelectedIndex = 0
        Me.BedrockServerManager.Size = New System.Drawing.Size(613, 505)
        Me.BedrockServerManager.TabIndex = 2
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Crafting_Table.png")
        Me.ImageList1.Images.SetKeyName(1, "Bedrock.png")
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(696, 520)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "NewGUITest"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(6, 18)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(172, 28)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "建立伺服器"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(6, 52)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(172, 28)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "加入伺服器"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(6, 86)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(172, 28)
        Me.Button3.TabIndex = 1
        Me.Button3.Text = "移除伺服器"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ServerGroupBox
        '
        Me.ServerGroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerGroupBox.Controls.Add(Me.VListLoadingBox)
        Me.ServerGroupBox.Controls.Add(Me.MemoryBox)
        Me.ServerGroupBox.Controls.Add(Me.Button3)
        Me.ServerGroupBox.Controls.Add(Me.Button2)
        Me.ServerGroupBox.Controls.Add(Me.Button1)
        Me.ServerGroupBox.Location = New System.Drawing.Point(632, 140)
        Me.ServerGroupBox.Name = "ServerGroupBox"
        Me.ServerGroupBox.Size = New System.Drawing.Size(184, 372)
        Me.ServerGroupBox.TabIndex = 2
        Me.ServerGroupBox.TabStop = False
        Me.ServerGroupBox.Text = "伺服器"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(147, 18)
        Me.Label12.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(23, 12)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "MB"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MemoryMaxBox
        '
        Me.MemoryMaxBox.Location = New System.Drawing.Point(66, 13)
        Me.MemoryMaxBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.MemoryMaxBox.Name = "MemoryMaxBox"
        Me.MemoryMaxBox.Size = New System.Drawing.Size(78, 22)
        Me.MemoryMaxBox.TabIndex = 26
        Me.MemoryMaxBox.Tag = ""
        Me.MemoryMaxBox.Value = New Decimal(New Integer() {1024, 0, 0, 0})
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 18)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 12)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "最大值："
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(147, 45)
        Me.Label14.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(23, 12)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "MB"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MemoryMinBox
        '
        Me.MemoryMinBox.Location = New System.Drawing.Point(66, 41)
        Me.MemoryMinBox.Maximum = New Decimal(New Integer() {1048576, 0, 0, 0})
        Me.MemoryMinBox.Name = "MemoryMinBox"
        Me.MemoryMinBox.Size = New System.Drawing.Size(78, 22)
        Me.MemoryMinBox.TabIndex = 29
        Me.MemoryMinBox.Tag = ""
        Me.MemoryMinBox.Value = New Decimal(New Integer() {1024, 0, 0, 0})
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(10, 45)
        Me.Label13.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 12)
        Me.Label13.TabIndex = 28
        Me.Label13.Text = "最小值："
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'MemoryBox
        '
        Me.MemoryBox.Controls.Add(Me.Label13)
        Me.MemoryBox.Controls.Add(Me.MemoryMinBox)
        Me.MemoryBox.Controls.Add(Me.Label14)
        Me.MemoryBox.Controls.Add(Me.Label11)
        Me.MemoryBox.Controls.Add(Me.MemoryMaxBox)
        Me.MemoryBox.Controls.Add(Me.Label12)
        Me.MemoryBox.Location = New System.Drawing.Point(8, 120)
        Me.MemoryBox.Name = "MemoryBox"
        Me.MemoryBox.Size = New System.Drawing.Size(170, 73)
        Me.MemoryBox.TabIndex = 28
        Me.MemoryBox.TabStop = False
        Me.MemoryBox.Text = "Java 執行記憶體"
        '
        'VanillaLoadingLabel
        '
        Me.VanillaLoadingLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VanillaLoadingLabel.Location = New System.Drawing.Point(6, 18)
        Me.VanillaLoadingLabel.Name = "VanillaLoadingLabel"
        Me.VanillaLoadingLabel.Size = New System.Drawing.Size(158, 23)
        Me.VanillaLoadingLabel.TabIndex = 0
        Me.VanillaLoadingLabel.Text = "Vanilla："
        Me.VanillaLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ForgeLoadingLabel
        '
        Me.ForgeLoadingLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ForgeLoadingLabel.Location = New System.Drawing.Point(6, 41)
        Me.ForgeLoadingLabel.Name = "ForgeLoadingLabel"
        Me.ForgeLoadingLabel.Size = New System.Drawing.Size(158, 23)
        Me.ForgeLoadingLabel.TabIndex = 1
        Me.ForgeLoadingLabel.Text = "Forge："
        Me.ForgeLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SpigotLoadingLabel
        '
        Me.SpigotLoadingLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SpigotLoadingLabel.Location = New System.Drawing.Point(6, 64)
        Me.SpigotLoadingLabel.Name = "SpigotLoadingLabel"
        Me.SpigotLoadingLabel.Size = New System.Drawing.Size(158, 23)
        Me.SpigotLoadingLabel.TabIndex = 2
        Me.SpigotLoadingLabel.Text = "Spigot："
        Me.SpigotLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CraftBukkitLoadingLabel
        '
        Me.CraftBukkitLoadingLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CraftBukkitLoadingLabel.Location = New System.Drawing.Point(6, 87)
        Me.CraftBukkitLoadingLabel.Name = "CraftBukkitLoadingLabel"
        Me.CraftBukkitLoadingLabel.Size = New System.Drawing.Size(158, 23)
        Me.CraftBukkitLoadingLabel.TabIndex = 3
        Me.CraftBukkitLoadingLabel.Text = "CraftBukkit："
        Me.CraftBukkitLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'VersionListReloadButton
        '
        Me.VersionListReloadButton.Location = New System.Drawing.Point(6, 136)
        Me.VersionListReloadButton.Name = "VersionListReloadButton"
        Me.VersionListReloadButton.Size = New System.Drawing.Size(158, 23)
        Me.VersionListReloadButton.TabIndex = 4
        Me.VersionListReloadButton.Text = "重新載入"
        Me.VersionListReloadButton.UseVisualStyleBackColor = True
        '
        'NukkitLoadingLabel
        '
        Me.NukkitLoadingLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NukkitLoadingLabel.Location = New System.Drawing.Point(6, 110)
        Me.NukkitLoadingLabel.Name = "NukkitLoadingLabel"
        Me.NukkitLoadingLabel.Size = New System.Drawing.Size(158, 23)
        Me.NukkitLoadingLabel.TabIndex = 5
        Me.NukkitLoadingLabel.Text = "Nukkit："
        Me.NukkitLoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'VListLoadingBox
        '
        Me.VListLoadingBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VListLoadingBox.Controls.Add(Me.NukkitLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.VersionListReloadButton)
        Me.VListLoadingBox.Controls.Add(Me.CraftBukkitLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.SpigotLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.ForgeLoadingLabel)
        Me.VListLoadingBox.Controls.Add(Me.VanillaLoadingLabel)
        Me.VListLoadingBox.Location = New System.Drawing.Point(8, 199)
        Me.VListLoadingBox.Name = "VListLoadingBox"
        Me.VListLoadingBox.Size = New System.Drawing.Size(170, 167)
        Me.VListLoadingBox.TabIndex = 29
        Me.VListLoadingBox.TabStop = False
        Me.VListLoadingBox.Text = "版本列表載入進度"
        '
        'IPLabel
        '
        Me.IPLabel.AutoSize = True
        Me.IPLabel.Location = New System.Drawing.Point(6, 18)
        Me.IPLabel.Name = "IPLabel"
        Me.IPLabel.Size = New System.Drawing.Size(51, 12)
        Me.IPLabel.TabIndex = 2
        Me.IPLabel.Text = "IP位址："
        '
        'RouterLabel
        '
        Me.RouterLabel.AutoSize = True
        Me.RouterLabel.Location = New System.Drawing.Point(6, 89)
        Me.RouterLabel.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.RouterLabel.Name = "RouterLabel"
        Me.RouterLabel.Size = New System.Drawing.Size(116, 12)
        Me.RouterLabel.TabIndex = 3
        Me.RouterLabel.Text = "分享器/路由器位址："
        '
        'ChangeRouterBtn
        '
        Me.ChangeRouterBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChangeRouterBtn.Location = New System.Drawing.Point(111, 104)
        Me.ChangeRouterBtn.Name = "ChangeRouterBtn"
        Me.ChangeRouterBtn.Size = New System.Drawing.Size(67, 23)
        Me.ChangeRouterBtn.TabIndex = 4
        Me.ChangeRouterBtn.Text = "變更設定"
        Me.Tip.SetToolTip(Me.ChangeRouterBtn, "變更分享器或路由器的設定。")
        Me.ChangeRouterBtn.UseVisualStyleBackColor = True
        '
        'UPnPLabel
        '
        Me.UPnPLabel.AutoSize = True
        Me.UPnPLabel.Location = New System.Drawing.Point(6, 33)
        Me.UPnPLabel.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.UPnPLabel.Name = "UPnPLabel"
        Me.UPnPLabel.Size = New System.Drawing.Size(70, 12)
        Me.UPnPLabel.TabIndex = 5
        Me.UPnPLabel.Text = "UPnP 狀態："
        '
        'CheckBox1
        '
        Me.CheckBox1.Appearance = System.Windows.Forms.Appearance.Button
        Me.CheckBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CheckBox1.Location = New System.Drawing.Point(111, 48)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(67, 23)
        Me.CheckBox1.TabIndex = 6
        Me.CheckBox1.Text = "啟用UPnP"
        Me.CheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'UPnPIPLabel
        '
        Me.UPnPIPLabel.AutoSize = True
        Me.UPnPIPLabel.Location = New System.Drawing.Point(6, 74)
        Me.UPnPIPLabel.Name = "UPnPIPLabel"
        Me.UPnPIPLabel.Size = New System.Drawing.Size(81, 12)
        Me.UPnPIPLabel.TabIndex = 7
        Me.UPnPIPLabel.Text = "UPnP-IP位址："
        '
        'NetworkGroupBox
        '
        Me.NetworkGroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NetworkGroupBox.Controls.Add(Me.UPnPIPLabel)
        Me.NetworkGroupBox.Controls.Add(Me.CheckBox1)
        Me.NetworkGroupBox.Controls.Add(Me.UPnPLabel)
        Me.NetworkGroupBox.Controls.Add(Me.ChangeRouterBtn)
        Me.NetworkGroupBox.Controls.Add(Me.RouterLabel)
        Me.NetworkGroupBox.Controls.Add(Me.IPLabel)
        Me.NetworkGroupBox.Location = New System.Drawing.Point(632, 1)
        Me.NetworkGroupBox.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.NetworkGroupBox.Name = "NetworkGroupBox"
        Me.NetworkGroupBox.Size = New System.Drawing.Size(184, 133)
        Me.NetworkGroupBox.TabIndex = 1
        Me.NetworkGroupBox.TabStop = False
        Me.NetworkGroupBox.Text = "網路"
        '
        'Launcher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(825, 547)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.ServerGroupBox)
        Me.Controls.Add(Me.NetworkGroupBox)
        Me.Controls.Add(Me.ServerManagerTabs)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Launcher"
        Me.Text = "Minecraft 伺服器管理員"
        Me.ServerManagerTabs.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ServerGroupBox.ResumeLayout(False)
        CType(Me.MemoryMaxBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MemoryMinBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MemoryBox.ResumeLayout(False)
        Me.MemoryBox.PerformLayout()
        Me.VListLoadingBox.ResumeLayout(False)
        Me.NetworkGroupBox.ResumeLayout(False)
        Me.NetworkGroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tip As ToolTip
    Friend WithEvents ServerManagerTabs As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents JavaServerManager As TabControl
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents BedrockServerManager As TabControl
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Button4 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents ServerGroupBox As GroupBox
    Friend WithEvents VListLoadingBox As GroupBox
    Friend WithEvents NukkitLoadingLabel As Label
    Friend WithEvents VersionListReloadButton As Button
    Friend WithEvents CraftBukkitLoadingLabel As Label
    Friend WithEvents SpigotLoadingLabel As Label
    Friend WithEvents ForgeLoadingLabel As Label
    Friend WithEvents VanillaLoadingLabel As Label
    Friend WithEvents MemoryBox As GroupBox
    Friend WithEvents Label13 As Label
    Friend WithEvents MemoryMinBox As NumericUpDown
    Friend WithEvents Label14 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents MemoryMaxBox As NumericUpDown
    Friend WithEvents Label12 As Label
    Friend WithEvents IPLabel As Label
    Friend WithEvents RouterLabel As Label
    Friend WithEvents ChangeRouterBtn As Button
    Friend WithEvents UPnPLabel As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents UPnPIPLabel As Label
    Friend WithEvents NetworkGroupBox As GroupBox
End Class
