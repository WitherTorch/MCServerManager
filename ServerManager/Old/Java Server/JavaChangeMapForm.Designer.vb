<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JavaChangeMapForm
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
        Me.Layout = New System.Windows.Forms.TableLayoutPanel()
        Me.ChooseMapButton = New System.Windows.Forms.Button()
        Me.CreateNewMap = New System.Windows.Forms.Button()
        Me.Layout.SuspendLayout()
        Me.SuspendLayout()
        '
        'Layout
        '
        Me.Layout.ColumnCount = 2
        Me.Layout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Layout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.Layout.Controls.Add(Me.ChooseMapButton, 0, 0)
        Me.Layout.Controls.Add(Me.CreateNewMap, 1, 0)
        Me.Layout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Layout.Location = New System.Drawing.Point(0, 0)
        Me.Layout.Name = "Layout"
        Me.Layout.RowCount = 1
        Me.Layout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Layout.Size = New System.Drawing.Size(384, 161)
        Me.Layout.TabIndex = 0
        '
        'ChooseMapButton
        '
        Me.ChooseMapButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChooseMapButton.Location = New System.Drawing.Point(3, 3)
        Me.ChooseMapButton.Name = "ChooseMapButton"
        Me.ChooseMapButton.Size = New System.Drawing.Size(186, 155)
        Me.ChooseMapButton.TabIndex = 0
        Me.ChooseMapButton.Text = "選擇現有的地圖"
        Me.ChooseMapButton.UseVisualStyleBackColor = True
        '
        'CreateNewMap
        '
        Me.CreateNewMap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CreateNewMap.Location = New System.Drawing.Point(195, 3)
        Me.CreateNewMap.Name = "CreateNewMap"
        Me.CreateNewMap.Size = New System.Drawing.Size(186, 155)
        Me.CreateNewMap.TabIndex = 1
        Me.CreateNewMap.Text = "建立新的地圖"
        Me.CreateNewMap.UseVisualStyleBackColor = True
        '
        'ChangeMapForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 161)
        Me.Controls.Add(Me.Layout)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ChangeMapForm"
        Me.Text = "變更地圖"
        Me.Layout.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend Shadows WithEvents Layout As TableLayoutPanel
    Friend WithEvents ChooseMapButton As Button
    Friend WithEvents CreateNewMap As Button
End Class
