<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MapView
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
        Me.IconBox = New System.Windows.Forms.PictureBox()
        Me.MapNameLabel = New System.Windows.Forms.Label()
        Me.MapOption = New System.Windows.Forms.TableLayoutPanel()
        Me.BrowseButton = New System.Windows.Forms.Button()
        CType(Me.IconBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MapOption.SuspendLayout()
        Me.SuspendLayout()
        '
        'IconBox
        '
        Me.IconBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.IconBox.Location = New System.Drawing.Point(3, 3)
        Me.IconBox.Name = "IconBox"
        Me.IconBox.Size = New System.Drawing.Size(64, 64)
        Me.IconBox.TabIndex = 0
        Me.IconBox.TabStop = False
        '
        'MapNameLabel
        '
        Me.MapNameLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MapNameLabel.Font = New System.Drawing.Font("微軟正黑體", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.MapNameLabel.Location = New System.Drawing.Point(73, 3)
        Me.MapNameLabel.Name = "MapNameLabel"
        Me.MapNameLabel.Size = New System.Drawing.Size(433, 30)
        Me.MapNameLabel.TabIndex = 1
        Me.MapNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MapOption
        '
        Me.MapOption.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MapOption.ColumnCount = 1
        Me.MapOption.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.MapOption.Controls.Add(Me.BrowseButton, 0, 1)
        Me.MapOption.Location = New System.Drawing.Point(512, 3)
        Me.MapOption.Name = "MapOption"
        Me.MapOption.RowCount = 2
        Me.MapOption.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.MapOption.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.MapOption.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.MapOption.Size = New System.Drawing.Size(64, 64)
        Me.MapOption.TabIndex = 3
        '
        'BrowseButton
        '
        Me.BrowseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BrowseButton.Location = New System.Drawing.Point(0, 35)
        Me.BrowseButton.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.BrowseButton.Name = "BrowseButton"
        Me.BrowseButton.Size = New System.Drawing.Size(64, 26)
        Me.BrowseButton.TabIndex = 4
        Me.BrowseButton.Text = "設定"
        Me.BrowseButton.UseVisualStyleBackColor = True
        '
        'MapView
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.MapOption)
        Me.Controls.Add(Me.MapNameLabel)
        Me.Controls.Add(Me.IconBox)
        Me.Font = New System.Drawing.Font("微軟正黑體", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Name = "MapView"
        Me.Size = New System.Drawing.Size(579, 70)
        CType(Me.IconBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MapOption.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IconBox As PictureBox
    Friend WithEvents MapNameLabel As Label
    Friend WithEvents MapOption As TableLayoutPanel
    Friend WithEvents BrowseButton As Button
End Class
