<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BedrockServer
    Inherits System.Windows.Forms.UserControl

    'Control 覆寫 Dispose 以清除元件清單。
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

    '為控制項設計工具的必要項
    Private components As System.ComponentModel.IContainer

    ' 注意: 以下為元件設計工具的所需的程序
    ' 您可以使用元件設計工具進行修改。請不要使用程式碼編輯器
    ' 進行修改。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.VersionLabel = New System.Windows.Forms.Label()
        Me.ServerProcess = New System.Diagnostics.Process()
        Me.ServerOption = New System.Windows.Forms.GroupBox()
        Me.ServerOptionBox = New System.Windows.Forms.TableLayoutPanel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ServerStatusLabel = New System.Windows.Forms.Label()
        Me.LocationLabel = New System.Windows.Forms.Label()
        Me.StartBtn = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
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
        Me.VersionLabel.TabIndex = 22
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
        Me.ServerOption.TabIndex = 24
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
        Me.ServerOptionBox.RowCount = 21
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
        Me.ServerOptionBox.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(573, 110)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 44)
        Me.Button1.TabIndex = 33
        Me.Button1.Text = "儲存設定"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(86, 110)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(80, 44)
        Me.Button5.TabIndex = 32
        Me.Button5.Text = "變更地圖"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(0, 110)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(80, 44)
        Me.Button3.TabIndex = 30
        Me.Button3.Text = "開啟伺服器資料夾"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ServerStatusLabel
        '
        Me.ServerStatusLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerStatusLabel.Location = New System.Drawing.Point(3, 56)
        Me.ServerStatusLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.ServerStatusLabel.Name = "ServerStatusLabel"
        Me.ServerStatusLabel.Size = New System.Drawing.Size(564, 21)
        Me.ServerStatusLabel.TabIndex = 28
        Me.ServerStatusLabel.Text = "伺服器狀態：關閉"
        Me.ServerStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LocationLabel
        '
        Me.LocationLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LocationLabel.Location = New System.Drawing.Point(3, 3)
        Me.LocationLabel.Margin = New System.Windows.Forms.Padding(3)
        Me.LocationLabel.Name = "LocationLabel"
        Me.LocationLabel.Size = New System.Drawing.Size(564, 21)
        Me.LocationLabel.TabIndex = 25
        Me.LocationLabel.Text = "伺服器路徑："
        Me.LocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StartBtn
        '
        Me.StartBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StartBtn.Location = New System.Drawing.Point(573, 3)
        Me.StartBtn.Name = "StartBtn"
        Me.StartBtn.Size = New System.Drawing.Size(75, 101)
        Me.StartBtn.TabIndex = 23
        Me.StartBtn.Text = "啟動伺服器"
        Me.StartBtn.UseVisualStyleBackColor = True
        '
        'BedrockServer
        '
        Me.Controls.Add(Me.VersionLabel)
        Me.Controls.Add(Me.ServerOption)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.ServerStatusLabel)
        Me.Controls.Add(Me.LocationLabel)
        Me.Controls.Add(Me.StartBtn)
        Me.Name = "BedrockServer"
        Me.Size = New System.Drawing.Size(657, 572)
        Me.ServerOption.ResumeLayout(False)
        Me.ServerOption.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents VersionLabel As Label
    Private WithEvents ServerProcess As Process
    Friend WithEvents ServerOption As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents ServerStatusLabel As Label
    Friend WithEvents LocationLabel As Label
    Friend WithEvents StartBtn As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ServerOptionBox As TableLayoutPanel
End Class

