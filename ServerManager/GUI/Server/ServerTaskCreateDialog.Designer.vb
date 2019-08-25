<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerTaskCreateDialog
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerTaskCreateDialog))
        Me.TaskTypeComboBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.InputArgsButton = New System.Windows.Forms.Button()
        Me.RunCommandArgBox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.RunComboBox = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.EventComboBox = New System.Windows.Forms.ComboBox()
        Me.TaskPeriodUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TaskPeriodUnitCombo = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TaskNameTextBox = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout
        Me.GroupBox2.SuspendLayout
        CType(Me.TaskPeriodUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout
        Me.SuspendLayout
        '
        'TaskTypeComboBox
        '
        Me.TaskTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TaskTypeComboBox.FormattingEnabled = True
        Me.TaskTypeComboBox.Items.AddRange(New Object() {"定時執行", "事件觸發", "快捷欄手動觸發"})
        Me.TaskTypeComboBox.Location = New System.Drawing.Point(80, 40)
        Me.TaskTypeComboBox.Name = "TaskTypeComboBox"
        Me.TaskTypeComboBox.Size = New System.Drawing.Size(292, 24)
        Me.TaskTypeComboBox.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "行程類型："
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.InputArgsButton)
        Me.GroupBox1.Controls.Add(Me.RunCommandArgBox)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.RunComboBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 203)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(360, 160)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'InputArgsButton
        '
        Me.InputArgsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InputArgsButton.Location = New System.Drawing.Point(324, 40)
        Me.InputArgsButton.Name = "InputArgsButton"
        Me.InputArgsButton.Size = New System.Drawing.Size(30, 23)
        Me.InputArgsButton.TabIndex = 18
        Me.InputArgsButton.Text = "^"
        Me.ToolTip1.SetToolTip(Me.InputArgsButton, "插入參數")
        Me.InputArgsButton.UseVisualStyleBackColor = True
        '
        'RunCommandArgBox
        '
        Me.RunCommandArgBox.Location = New System.Drawing.Point(71, 40)
        Me.RunCommandArgBox.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.RunCommandArgBox.Multiline = True
        Me.RunCommandArgBox.Name = "RunCommandArgBox"
        Me.RunCommandArgBox.Size = New System.Drawing.Size(247, 114)
        Me.RunCommandArgBox.TabIndex = 17
        Me.RunCommandArgBox.WordWrap = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 44)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 16)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "執行參數："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 18)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 16)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "執行："
        '
        'RunComboBox
        '
        Me.RunComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RunComboBox.FormattingEnabled = True
        Me.RunComboBox.Items.AddRange(New Object() {"停止伺服器", "重啟伺服器", "執行指令", "備份伺服器"})
        Me.RunComboBox.Location = New System.Drawing.Point(71, 14)
        Me.RunComboBox.Name = "RunComboBox"
        Me.RunComboBox.Size = New System.Drawing.Size(283, 24)
        Me.RunComboBox.TabIndex = 14
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.EventComboBox)
        Me.GroupBox2.Controls.Add(Me.TaskPeriodUpDown)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TaskPeriodUnitCombo)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 66)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(360, 67)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Enabled = False
        Me.Label5.Location = New System.Drawing.Point(280, 44)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 16)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = " 觸發時執行"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Enabled = False
        Me.Label4.Location = New System.Drawing.Point(6, 44)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(23, 16)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "當 "
        '
        'EventComboBox
        '
        Me.EventComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EventComboBox.Enabled = False
        Me.EventComboBox.FormattingEnabled = True
        Me.EventComboBox.Items.AddRange(New Object() {"玩家登入", "玩家登出", "伺服器啟動", "伺服器關閉", "玩家輸入指令"})
        Me.EventComboBox.Location = New System.Drawing.Point(29, 40)
        Me.EventComboBox.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.EventComboBox.Name = "EventComboBox"
        Me.EventComboBox.Size = New System.Drawing.Size(251, 24)
        Me.EventComboBox.TabIndex = 14
        '
        'TaskPeriodUpDown
        '
        Me.TaskPeriodUpDown.Enabled = False
        Me.TaskPeriodUpDown.Location = New System.Drawing.Point(241, 13)
        Me.TaskPeriodUpDown.Maximum = New Decimal(New Integer() {-1981284352, -1966660860, 0, 0})
        Me.TaskPeriodUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.TaskPeriodUpDown.Name = "TaskPeriodUpDown"
        Me.TaskPeriodUpDown.Size = New System.Drawing.Size(107, 23)
        Me.TaskPeriodUpDown.TabIndex = 13
        Me.TaskPeriodUpDown.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Enabled = False
        Me.Label3.Location = New System.Drawing.Point(170, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 16)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "觸發間隔："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Enabled = False
        Me.Label2.Location = New System.Drawing.Point(6, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "計時單位："
        '
        'TaskPeriodUnitCombo
        '
        Me.TaskPeriodUnitCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TaskPeriodUnitCombo.Enabled = False
        Me.TaskPeriodUnitCombo.FormattingEnabled = True
        Me.TaskPeriodUnitCombo.Items.AddRange(New Object() {"遊戲刻", "秒鐘", "分鐘", "小時", "天"})
        Me.TaskPeriodUnitCombo.Location = New System.Drawing.Point(77, 14)
        Me.TaskPeriodUnitCombo.Name = "TaskPeriodUnitCombo"
        Me.TaskPeriodUnitCombo.Size = New System.Drawing.Size(87, 24)
        Me.TaskPeriodUnitCombo.TabIndex = 10
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(300, 369)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 23)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "排定"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 16)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "行程名稱："
        '
        'TaskNameTextBox
        '
        Me.TaskNameTextBox.Location = New System.Drawing.Point(80, 12)
        Me.TaskNameTextBox.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.TaskNameTextBox.Name = "TaskNameTextBox"
        Me.TaskNameTextBox.Size = New System.Drawing.Size(292, 23)
        Me.TaskNameTextBox.TabIndex = 18
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TextBox1)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Enabled = False
        Me.GroupBox3.Location = New System.Drawing.Point(12, 139)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(360, 58)
        Me.GroupBox3.TabIndex = 17
        Me.GroupBox3.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(113, 22)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(241, 23)
        Me.TextBox1.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(-3, 20)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(116, 32)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "　輸入指令比對：" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "（使用正規表示式）"
        '
        'ServerTaskCreateDialog
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(384, 404)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.TaskNameTextBox)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TaskTypeComboBox)
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ServerTaskCreateDialog"
        Me.ShowIcon = False
        Me.Text = "排定行程"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout
        CType(Me.TaskPeriodUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout
        Me.ResumeLayout(False)
        Me.PerformLayout

    End Sub

    Friend WithEvents TaskTypeComboBox As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RunCommandArgBox As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents RunComboBox As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents EventComboBox As ComboBox
    Friend WithEvents TaskPeriodUpDown As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TaskPeriodUnitCombo As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents TaskNameTextBox As TextBox
    Friend WithEvents InputArgsButton As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TextBox1 As TextBox
End Class
