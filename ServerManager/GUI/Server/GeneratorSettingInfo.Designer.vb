<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GeneratorSettingInfo
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.IconBox = New System.Windows.Forms.PictureBox()
        Me.InfomationBox = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.IconBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.IconBox, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.InfomationBox, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 256.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(426, 548)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'IconBox
        '
        Me.IconBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IconBox.Location = New System.Drawing.Point(3, 3)
        Me.IconBox.Name = "IconBox"
        Me.IconBox.Size = New System.Drawing.Size(420, 250)
        Me.IconBox.TabIndex = 0
        Me.IconBox.TabStop = False
        '
        'InfomationBox
        '
        Me.InfomationBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InfomationBox.Font = New System.Drawing.Font("微軟正黑體", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.InfomationBox.Location = New System.Drawing.Point(3, 259)
        Me.InfomationBox.Margin = New System.Windows.Forms.Padding(3)
        Me.InfomationBox.Name = "InfomationBox"
        Me.InfomationBox.Size = New System.Drawing.Size(420, 286)
        Me.InfomationBox.TabIndex = 1
        '
        'GeneratorSettingInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "GeneratorSettingInfo"
        Me.Size = New System.Drawing.Size(426, 548)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.IconBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents IconBox As PictureBox
    Friend WithEvents InfomationBox As Label
End Class
