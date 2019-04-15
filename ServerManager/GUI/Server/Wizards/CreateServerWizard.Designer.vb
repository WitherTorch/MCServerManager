<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CreateServerWizard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CreateServerWizard))
        Me.CreateProgress = New System.Windows.Forms.ProgressBar()
        Me.CreateStatusLabel = New System.Windows.Forms.Label()
        Me.WizardPanel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.IPBox = New System.Windows.Forms.TextBox()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ServerDirBrowseBtn = New System.Windows.Forms.Button()
        Me.ServerDirBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.VersionTypeBox = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.VersionBox = New System.Windows.Forms.ComboBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.NextButton = New System.Windows.Forms.Button()
        Me.WizardPanel2 = New System.Windows.Forms.Panel()
        Me.AdvancedPropertyGrid = New System.Windows.Forms.PropertyGrid()
        Me.WizardPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WizardPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CreateProgress
        '
        Me.CreateProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CreateProgress.Location = New System.Drawing.Point(12, 33)
        Me.CreateProgress.Maximum = 2
        Me.CreateProgress.Name = "CreateProgress"
        Me.CreateProgress.Size = New System.Drawing.Size(498, 23)
        Me.CreateProgress.TabIndex = 0
        '
        'CreateStatusLabel
        '
        Me.CreateStatusLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CreateStatusLabel.Font = New System.Drawing.Font("新細明體", 10.0!)
        Me.CreateStatusLabel.Location = New System.Drawing.Point(12, 9)
        Me.CreateStatusLabel.Name = "CreateStatusLabel"
        Me.CreateStatusLabel.Size = New System.Drawing.Size(498, 21)
        Me.CreateStatusLabel.TabIndex = 1
        Me.CreateStatusLabel.Text = "Status"
        Me.CreateStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WizardPanel1
        '
        Me.WizardPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.WizardPanel1.Controls.Add(Me.GroupBox1)
        Me.WizardPanel1.Controls.Add(Me.NumericUpDown1)
        Me.WizardPanel1.Controls.Add(Me.Label5)
        Me.WizardPanel1.Controls.Add(Me.ServerDirBrowseBtn)
        Me.WizardPanel1.Controls.Add(Me.ServerDirBox)
        Me.WizardPanel1.Controls.Add(Me.Label4)
        Me.WizardPanel1.Controls.Add(Me.Label1)
        Me.WizardPanel1.Controls.Add(Me.VersionTypeBox)
        Me.WizardPanel1.Controls.Add(Me.Label2)
        Me.WizardPanel1.Controls.Add(Me.VersionBox)
        Me.WizardPanel1.Location = New System.Drawing.Point(12, 71)
        Me.WizardPanel1.Name = "WizardPanel1"
        Me.WizardPanel1.Size = New System.Drawing.Size(498, 239)
        Me.WizardPanel1.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.IPBox)
        Me.GroupBox1.Controls.Add(Me.RadioButton3)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 61)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(490, 89)
        Me.GroupBox1.TabIndex = 40
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "IP 選項"
        '
        'IPBox
        '
        Me.IPBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IPBox.Location = New System.Drawing.Point(77, 58)
        Me.IPBox.Name = "IPBox"
        Me.IPBox.ReadOnly = True
        Me.IPBox.Size = New System.Drawing.Size(406, 22)
        Me.IPBox.TabIndex = 34
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(12, 21)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(47, 16)
        Me.RadioButton3.TabIndex = 33
        Me.RadioButton3.Text = "浮動"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(12, 61)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(59, 16)
        Me.RadioButton1.TabIndex = 31
        Me.RadioButton1.Text = "自訂："
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.Location = New System.Drawing.Point(12, 41)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(47, 16)
        Me.RadioButton2.TabIndex = 32
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "預設"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(83, 33)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {65534, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(120, 22)
        Me.NumericUpDown1.TabIndex = 55
        Me.NumericUpDown1.Value = New Decimal(New Integer() {25565, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 38)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 8, 3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "連接埠："
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ServerDirBrowseBtn
        '
        Me.ServerDirBrowseBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerDirBrowseBtn.Location = New System.Drawing.Point(433, 4)
        Me.ServerDirBrowseBtn.Name = "ServerDirBrowseBtn"
        Me.ServerDirBrowseBtn.Size = New System.Drawing.Size(51, 23)
        Me.ServerDirBrowseBtn.TabIndex = 52
        Me.ServerDirBrowseBtn.Text = "瀏覽..."
        Me.ServerDirBrowseBtn.UseVisualStyleBackColor = True
        '
        'SolutionDirBox
        '
        Me.ServerDirBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerDirBox.Location = New System.Drawing.Point(83, 5)
        Me.ServerDirBox.Name = "SolutionDirBox"
        Me.ServerDirBox.Size = New System.Drawing.Size(344, 22)
        Me.ServerDirBox.TabIndex = 51
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 10)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 12)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "伺服器路徑："
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 160)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "遊戲版本類型："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'VersionTypeBox
        '
        Me.VersionTypeBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VersionTypeBox.FormattingEnabled = True
        Me.VersionTypeBox.Items.AddRange(New Object() {"Java - 原版", "Java - Forge", "Java - Spigot", "Java - CraftBukkit", "Bedrock - Nukkit"})
        Me.VersionTypeBox.Location = New System.Drawing.Point(107, 156)
        Me.VersionTypeBox.Name = "VersionTypeBox"
        Me.VersionTypeBox.Size = New System.Drawing.Size(386, 20)
        Me.VersionTypeBox.TabIndex = 47
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 185)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "遊戲版本："
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'VersionBox
        '
        Me.VersionBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VersionBox.FormattingEnabled = True
        Me.VersionBox.Location = New System.Drawing.Point(107, 182)
        Me.VersionBox.Name = "VersionBox"
        Me.VersionBox.Size = New System.Drawing.Size(386, 20)
        Me.VersionBox.TabIndex = 49
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.LightGray
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Location = New System.Drawing.Point(12, 62)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(498, 3)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'NextButton
        '
        Me.NextButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NextButton.Location = New System.Drawing.Point(438, 313)
        Me.NextButton.Margin = New System.Windows.Forms.Padding(0)
        Me.NextButton.Name = "NextButton"
        Me.NextButton.Size = New System.Drawing.Size(75, 23)
        Me.NextButton.TabIndex = 4
        Me.NextButton.Text = "下一步(&N)"
        Me.NextButton.UseVisualStyleBackColor = True
        '
        'WizardPanel2
        '
        Me.WizardPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.WizardPanel2.Controls.Add(Me.AdvancedPropertyGrid)
        Me.WizardPanel2.Location = New System.Drawing.Point(12, 71)
        Me.WizardPanel2.Name = "WizardPanel2"
        Me.WizardPanel2.Size = New System.Drawing.Size(498, 239)
        Me.WizardPanel2.TabIndex = 5
        '
        'AdvancedPropertyGrid
        '
        Me.AdvancedPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdvancedPropertyGrid.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.AdvancedPropertyGrid.Location = New System.Drawing.Point(0, 0)
        Me.AdvancedPropertyGrid.Margin = New System.Windows.Forms.Padding(0)
        Me.AdvancedPropertyGrid.Name = "AdvancedPropertyGrid"
        Me.AdvancedPropertyGrid.Size = New System.Drawing.Size(496, 237)
        Me.AdvancedPropertyGrid.TabIndex = 2
        '
        'CreateServerWizard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 345)
        Me.Controls.Add(Me.NextButton)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.CreateStatusLabel)
        Me.Controls.Add(Me.CreateProgress)
        Me.Controls.Add(Me.WizardPanel1)
        Me.Controls.Add(Me.WizardPanel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CreateServerWizard"
        Me.Text = "建立世界"
        Me.WizardPanel1.ResumeLayout(False)
        Me.WizardPanel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WizardPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CreateProgress As ProgressBar
    Friend WithEvents CreateStatusLabel As Label
    Friend WithEvents WizardPanel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents VersionTypeBox As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents VersionBox As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents IPBox As TextBox
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents ServerDirBrowseBtn As Button
    Friend WithEvents ServerDirBox As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents NextButton As Button
    Friend WithEvents WizardPanel2 As Panel
    Friend WithEvents AdvancedPropertyGrid As PropertyGrid
End Class
