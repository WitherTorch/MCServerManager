<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CreateMap
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CreateMap))
        Me.LevelTypeBox = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LevelSeedBox = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.LevelNameBox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GeneratorSettingBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LevelTypeBox
        '
        Me.LevelTypeBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LevelTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LevelTypeBox.FormattingEnabled = True
        Me.LevelTypeBox.Location = New System.Drawing.Point(80, 60)
        Me.LevelTypeBox.Name = "LevelTypeBox"
        Me.LevelTypeBox.Size = New System.Drawing.Size(367, 20)
        Me.LevelTypeBox.TabIndex = 45
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 64)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 12)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "地圖類型："
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LevelSeedBox
        '
        Me.LevelSeedBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LevelSeedBox.Location = New System.Drawing.Point(80, 32)
        Me.LevelSeedBox.Name = "LevelSeedBox"
        Me.LevelSeedBox.Size = New System.Drawing.Size(367, 22)
        Me.LevelSeedBox.TabIndex = 43
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 37)
        Me.Label8.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 12)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "世界種子："
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LevelNameBox
        '
        Me.LevelNameBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LevelNameBox.Location = New System.Drawing.Point(80, 4)
        Me.LevelNameBox.Name = "LevelNameBox"
        Me.LevelNameBox.Size = New System.Drawing.Size(367, 22)
        Me.LevelNameBox.TabIndex = 41
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 9)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 12)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "世界名稱："
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GeneratorSettingBox
        '
        Me.GeneratorSettingBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GeneratorSettingBox.BackColor = System.Drawing.Color.White
        Me.GeneratorSettingBox.Enabled = False
        Me.GeneratorSettingBox.Location = New System.Drawing.Point(80, 86)
        Me.GeneratorSettingBox.Name = "GeneratorSettingBox"
        Me.GeneratorSettingBox.Size = New System.Drawing.Size(296, 22)
        Me.GeneratorSettingBox.TabIndex = 39
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(0, 91)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 12)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "生成器設置："
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(382, 85)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(65, 23)
        Me.Button1.TabIndex = 46
        Me.Button1.Text = "預設配置"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(301, 114)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 27)
        Me.TableLayoutPanel1.TabIndex = 47
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 21)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "確定"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 21)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "取消"
        '
        'CreateMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(459, 153)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.LevelTypeBox)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.LevelSeedBox)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.LevelNameBox)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GeneratorSettingBox)
        Me.Controls.Add(Me.Label6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CreateMap"
        Me.ShowIcon = False
        Me.Text = "生成新地圖"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LevelTypeBox As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents LevelSeedBox As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents LevelNameBox As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents GeneratorSettingBox As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents OK_Button As Button
    Friend WithEvents Cancel_Button As Button
End Class
