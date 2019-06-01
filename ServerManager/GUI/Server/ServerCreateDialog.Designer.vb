<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerCreateDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerCreateDialog))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.MapPanel = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.VersionTypeBox = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.VersionBox = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.IPBox = New System.Windows.Forms.TextBox()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.PortBox = New System.Windows.Forms.NumericUpDown()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.將連接埠設成預設值ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ServerDirBrowseBtn = New System.Windows.Forms.Button()
        Me.ServerDirBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.AdvancedPropertyGrid = New System.Windows.Forms.PropertyGrid()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CreateButton = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout
        Me.TabPage1.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.GroupBox4.SuspendLayout
        Me.GroupBox2.SuspendLayout
        Me.GroupBox1.SuspendLayout
        CType(Me.PortBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(3, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(509, 409)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.ServerDirBrowseBtn)
        Me.TabPage1.Controls.Add(Me.ServerDirBox)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(501, 383)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "伺服器"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.VersionTypeBox)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.VersionBox)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 187)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(479, 150)
        Me.GroupBox3.TabIndex = 44
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "遊戲"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.MapPanel)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 43)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(467, 100)
        Me.GroupBox4.TabIndex = 45
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "地圖"
        '
        'MapPanel
        '
        Me.MapPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MapPanel.Location = New System.Drawing.Point(6, 21)
        Me.MapPanel.Name = "MapPanel"
        Me.MapPanel.Size = New System.Drawing.Size(454, 70)
        Me.MapPanel.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 21)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "遊戲類型："
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'VersionTypeBox
        '
        Me.VersionTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VersionTypeBox.FormattingEnabled = True
        Me.VersionTypeBox.Items.AddRange(New Object() {"Java 版 - 原版", "Java 版 - Forge", "Java 版 - Spigot", "Java 版 - Spigot (Git 手動建置)", "Java 版 - CraftBukkit", "Java 版 - Paper", "Java 版 - Akarin", "Java 版 - SpongeVanilla", "Java 版 - MCPC/Cauldron", "Java 版 - Thermos", "Java 版 - Contigo", "Java 版 - Kettle", "基岩版 - 原版", "基岩版 - Nukkit", "自定義"})
        Me.VersionTypeBox.Location = New System.Drawing.Point(77, 17)
        Me.VersionTypeBox.Name = "VersionTypeBox"
        Me.VersionTypeBox.Size = New System.Drawing.Size(145, 20)
        Me.VersionTypeBox.TabIndex = 26
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(228, 21)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 12)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "遊戲版本："
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'VersionBox
        '
        Me.VersionBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VersionBox.FormattingEnabled = True
        Me.VersionBox.Location = New System.Drawing.Point(299, 17)
        Me.VersionBox.Name = "VersionBox"
        Me.VersionBox.Size = New System.Drawing.Size(174, 20)
        Me.VersionBox.TabIndex = 29
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.PortBox)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 34)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(479, 147)
        Me.GroupBox2.TabIndex = 43
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "連接"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.IPBox)
        Me.GroupBox1.Controls.Add(Me.RadioButton3)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 52)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(467, 89)
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
        Me.IPBox.Size = New System.Drawing.Size(383, 22)
        Me.IPBox.TabIndex = 34
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Checked = True
        Me.RadioButton3.Location = New System.Drawing.Point(12, 21)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(47, 16)
        Me.RadioButton3.TabIndex = 33
        Me.RadioButton3.TabStop = True
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
        Me.RadioButton2.Location = New System.Drawing.Point(12, 41)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(47, 16)
        Me.RadioButton2.TabIndex = 32
        Me.RadioButton2.Text = "預設"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'PortBox
        '
        Me.PortBox.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PortBox.Location = New System.Drawing.Point(62, 21)
        Me.PortBox.Maximum = New Decimal(New Integer() {65534, 0, 0, 0})
        Me.PortBox.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.PortBox.Name = "PortBox"
        Me.PortBox.Size = New System.Drawing.Size(120, 22)
        Me.PortBox.TabIndex = 42
        Me.PortBox.Value = New Decimal(New Integer() {25565, 0, 0, 0})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.將連接埠設成預設值ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(183, 26)
        '
        '將連接埠設成預設值ToolStripMenuItem
        '
        Me.將連接埠設成預設值ToolStripMenuItem.Name = "將連接埠設成預設值ToolStripMenuItem"
        Me.將連接埠設成預設值ToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.將連接埠設成預設值ToolStripMenuItem.Text = "將連接埠設成預設值"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 26)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 8, 3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "連接埠："
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ServerDirBrowseBtn
        '
        Me.ServerDirBrowseBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerDirBrowseBtn.Location = New System.Drawing.Point(442, 5)
        Me.ServerDirBrowseBtn.Name = "ServerDirBrowseBtn"
        Me.ServerDirBrowseBtn.Size = New System.Drawing.Size(51, 23)
        Me.ServerDirBrowseBtn.TabIndex = 39
        Me.ServerDirBrowseBtn.Text = "瀏覽..."
        Me.ServerDirBrowseBtn.UseVisualStyleBackColor = True
        '
        'ServerDirBox
        '
        Me.ServerDirBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ServerDirBox.Location = New System.Drawing.Point(90, 6)
        Me.ServerDirBox.Name = "ServerDirBox"
        Me.ServerDirBox.Size = New System.Drawing.Size(346, 22)
        Me.ServerDirBox.TabIndex = 38
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "伺服器路徑："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.AdvancedPropertyGrid)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(501, 383)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "進階設定"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'AdvancedPropertyGrid
        '
        Me.AdvancedPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdvancedPropertyGrid.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.AdvancedPropertyGrid.Location = New System.Drawing.Point(3, 3)
        Me.AdvancedPropertyGrid.Margin = New System.Windows.Forms.Padding(0)
        Me.AdvancedPropertyGrid.Name = "AdvancedPropertyGrid"
        Me.AdvancedPropertyGrid.Size = New System.Drawing.Size(495, 377)
        Me.AdvancedPropertyGrid.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(515, 450)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.CreateButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 418)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(509, 29)
        Me.Panel1.TabIndex = 0
        '
        'CreateButton
        '
        Me.CreateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CreateButton.Location = New System.Drawing.Point(425, 3)
        Me.CreateButton.Name = "CreateButton"
        Me.CreateButton.Size = New System.Drawing.Size(75, 23)
        Me.CreateButton.TabIndex = 0
        Me.CreateButton.Text = "建立"
        Me.CreateButton.UseVisualStyleBackColor = True
        '
        'ServerCreateDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(515, 450)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ServerCreateDialog"
        Me.ShowIcon = False
        Me.Text = "建立伺服器"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PortBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents VersionBox As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents VersionTypeBox As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents IPBox As TextBox
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents PortBox As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents ServerDirBrowseBtn As Button
    Friend WithEvents ServerDirBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents MapPanel As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CreateButton As Button
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents AdvancedPropertyGrid As PropertyGrid
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents 將連接埠設成預設值ToolStripMenuItem As ToolStripMenuItem
End Class
