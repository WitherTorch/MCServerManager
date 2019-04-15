<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GeneratorMapping
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GeneratorMapping))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ChooseBox = New System.Windows.Forms.ListBox()
        Me.InfoPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66667!))
        Me.TableLayoutPanel1.Controls.Add(Me.ChooseBox, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.InfoPanel, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(800, 480)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ChooseBox
        '
        Me.ChooseBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChooseBox.Font = New System.Drawing.Font("新細明體", 12.0!)
        Me.ChooseBox.FormattingEnabled = True
        Me.ChooseBox.ItemHeight = 16
        Me.ChooseBox.Items.AddRange(New Object() {"水世界", "天空島世界", "探洞者世界", "懸崖峭壁", "極度乾旱", "混沌地穴", "祝你好運"})
        Me.ChooseBox.Location = New System.Drawing.Point(6, 6)
        Me.ChooseBox.Name = "ChooseBox"
        Me.ChooseBox.Size = New System.Drawing.Size(257, 430)
        Me.ChooseBox.TabIndex = 0
        '
        'InfoPanel
        '
        Me.InfoPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InfoPanel.Location = New System.Drawing.Point(272, 6)
        Me.InfoPanel.Name = "InfoPanel"
        Me.InfoPanel.Size = New System.Drawing.Size(522, 430)
        Me.InfoPanel.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.OKButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(272, 445)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(522, 29)
        Me.Panel1.TabIndex = 2
        '
        'OKButton
        '
        Me.OKButton.AutoSize = True
        Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OKButton.Location = New System.Drawing.Point(438, 3)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(78, 23)
        Me.OKButton.TabIndex = 2
        Me.OKButton.Text = "確定"
        Me.OKButton.UseVisualStyleBackColor = True
        '
        'GeneratorMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 480)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "GeneratorMapping"
        Me.ShowIcon = False
        Me.Text = "選擇地圖配置"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ChooseBox As ListBox
    Friend WithEvents InfoPanel As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents OKButton As Button
End Class
