<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JavaServer
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
        Me.VersionLabel = New System.Windows.Forms.Label()
        Me.ServerProcess = New System.Diagnostics.Process()
        Me.ServerOption = New System.Windows.Forms.GroupBox()
        Me.ServerOptionBox = New System.Windows.Forms.TableLayoutPanel()
        Me.StartBtn = New System.Windows.Forms.Button()
        Me.LocationLabel = New System.Windows.Forms.Label()
        Me.ResourcePackLabel = New System.Windows.Forms.Label()
        Me.BrowseResourcePack = New System.Windows.Forms.Button()
        Me.ServerStatusLabel = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UpdateServerButton = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.ServerOption.SuspendLayout()
        Me.SuspendLayout()
        '
        'VersionLabel
        '
        Me.VersionLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionLabel.Location = New System.Drawing.Point(3, 29)
        Me.VersionLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.VersionLabel.Name = "VersionLabel"
        Me.VersionLabel.Size = New System.Drawing.Size(505, 21)
        Me.VersionLabel.TabIndex = 0
        Me.VersionLabel.Text = "伺服器版本："
        Me.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ServerProcess
        '
        Me.ServerProcess.EnableRaisingEvents = True
        Me.ServerProcess.StartInfo.Domain = ""
        Me.ServerProcess.StartInfo.LoadUserProfile = False
        Me.ServerProcess.StartInfo.Password = Nothing
        Me.ServerProcess.StartInfo.StandardErrorEncoding = Nothing
        Me.ServerProcess.StartInfo.StandardOutputEncoding = Nothing
        Me.ServerProcess.StartInfo.UserName = ""
        Me.ServerProcess.SynchronizingObject = Me
        '
        'ServerOption
        '
        Me.ServerOption.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerOption.Controls.Add(Me.ServerOptionBox)
        Me.ServerOption.Location = New System.Drawing.Point(5, 160)
        Me.ServerOption.Name = "ServerOption"
        Me.ServerOption.Size = New System.Drawing.Size(649, 412)
        Me.ServerOption.TabIndex = 11
        Me.ServerOption.TabStop = False
        Me.ServerOption.Text = "伺服器選項"
        '
        'ServerOptionBox
        '
        Me.ServerOptionBox.AutoScroll = True
        Me.ServerOptionBox.AutoSize = True
        Me.ServerOptionBox.ColumnCount = 2
        Me.ServerOptionBox.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.ServerOptionBox.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ServerOptionBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ServerOptionBox.Location = New System.Drawing.Point(3, 18)
        Me.ServerOptionBox.Name = "ServerOptionBox"
        Me.ServerOptionBox.RowCount = 31
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.ServerOptionBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ServerOptionBox.Size = New System.Drawing.Size(643, 391)
        Me.ServerOptionBox.TabIndex = 1
        '
        'StartBtn
        '
        Me.StartBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StartBtn.Location = New System.Drawing.Point(573, 3)
        Me.StartBtn.Name = "StartBtn"
        Me.StartBtn.Size = New System.Drawing.Size(75, 101)
        Me.StartBtn.TabIndex = 1
        Me.StartBtn.Text = "啟動伺服器"
        Me.StartBtn.UseVisualStyleBackColor = True
        '
        'LocationLabel
        '
        Me.LocationLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LocationLabel.Location = New System.Drawing.Point(3, 3)
        Me.LocationLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.LocationLabel.Name = "LocationLabel"
        Me.LocationLabel.Size = New System.Drawing.Size(564, 21)
        Me.LocationLabel.TabIndex = 12
        Me.LocationLabel.Text = "伺服器路徑："
        Me.LocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ResourcePackLabel
        '
        Me.ResourcePackLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResourcePackLabel.Location = New System.Drawing.Point(3, 83)
        Me.ResourcePackLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.ResourcePackLabel.Name = "ResourcePackLabel"
        Me.ResourcePackLabel.Size = New System.Drawing.Size(505, 21)
        Me.ResourcePackLabel.TabIndex = 13
        Me.ResourcePackLabel.Text = "伺服器資源包："
        Me.ResourcePackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BrowseResourcePack
        '
        Me.BrowseResourcePack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BrowseResourcePack.Location = New System.Drawing.Point(514, 82)
        Me.BrowseResourcePack.Name = "BrowseResourcePack"
        Me.BrowseResourcePack.Size = New System.Drawing.Size(53, 23)
        Me.BrowseResourcePack.TabIndex = 14
        Me.BrowseResourcePack.Text = "選擇..."
        Me.BrowseResourcePack.UseVisualStyleBackColor = True
        '
        'ServerStatusLabel
        '
        Me.ServerStatusLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerStatusLabel.Location = New System.Drawing.Point(3, 56)
        Me.ServerStatusLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.ServerStatusLabel.Name = "ServerStatusLabel"
        Me.ServerStatusLabel.Size = New System.Drawing.Size(564, 21)
        Me.ServerStatusLabel.TabIndex = 15
        Me.ServerStatusLabel.Text = "伺服器狀態：關閉"
        Me.ServerStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(0, 110)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 44)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "查看EULA"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(86, 110)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(80, 44)
        Me.Button3.TabIndex = 17
        Me.Button3.Text = "開啟伺服器資料夾"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(172, 110)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(80, 44)
        Me.Button4.TabIndex = 18
        Me.Button4.Text = "選擇伺服器圖示"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(258, 110)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(80, 44)
        Me.Button5.TabIndex = 19
        Me.Button5.Text = "變更地圖"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(573, 110)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 44)
        Me.Button1.TabIndex = 20
        Me.Button1.Text = "儲存設定"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'UpdateServerButton
        '
        Me.UpdateServerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UpdateServerButton.Enabled = False
        Me.UpdateServerButton.Location = New System.Drawing.Point(514, 28)
        Me.UpdateServerButton.Name = "UpdateServerButton"
        Me.UpdateServerButton.Size = New System.Drawing.Size(53, 23)
        Me.UpdateServerButton.TabIndex = 21
        Me.UpdateServerButton.Text = "更新"
        Me.UpdateServerButton.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(344, 110)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(80, 44)
        Me.Button6.TabIndex = 22
        Me.Button6.Text = "管理插件"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'JavaServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.UpdateServerButton)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ServerStatusLabel)
        Me.Controls.Add(Me.BrowseResourcePack)
        Me.Controls.Add(Me.ResourcePackLabel)
        Me.Controls.Add(Me.LocationLabel)
        Me.Controls.Add(Me.StartBtn)
        Me.Controls.Add(Me.ServerOption)
        Me.Controls.Add(Me.VersionLabel)
        Me.Name = "JavaServer"
        Me.Size = New System.Drawing.Size(657, 572)
        Me.ServerOption.ResumeLayout(False)
        Me.ServerOption.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents VersionLabel As Label
    Private WithEvents ServerProcess As Process
    Friend WithEvents ServerOption As GroupBox
    Friend WithEvents StartBtn As Button
    Friend WithEvents LocationLabel As Label
    Friend WithEvents BrowseResourcePack As Button
    Friend WithEvents ResourcePackLabel As Label
    Friend WithEvents ServerOptionBox As TableLayoutPanel
    Friend WithEvents ServerStatusLabel As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents UpdateServerButton As Button
    Friend WithEvents Button6 As Button
End Class
