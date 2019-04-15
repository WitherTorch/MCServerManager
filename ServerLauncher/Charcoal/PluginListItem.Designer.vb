<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PluginListItem
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
        Me.pluginIcon = New System.Windows.Forms.PictureBox()
        Me.pluginName = New System.Windows.Forms.Label()
        Me.DescriptionLabel = New System.Windows.Forms.Label()
        CType(Me.pluginIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pluginIcon
        '
        Me.pluginIcon.Location = New System.Drawing.Point(11, 11)
        Me.pluginIcon.Margin = New System.Windows.Forms.Padding(11, 11, 3, 11)
        Me.pluginIcon.Name = "pluginIcon"
        Me.pluginIcon.Size = New System.Drawing.Size(64, 64)
        Me.pluginIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pluginIcon.TabIndex = 0
        Me.pluginIcon.TabStop = False
        '
        'PluginName
        '
        Me.pluginName.AutoSize = True
        Me.pluginName.Font = New System.Drawing.Font("新細明體", 18.0!)
        Me.pluginName.Location = New System.Drawing.Point(81, 11)
        Me.pluginName.Name = "PluginName"
        Me.pluginName.Size = New System.Drawing.Size(0, 24)
        Me.pluginName.TabIndex = 1
        Me.pluginName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DescriptionLabel
        '
        Me.DescriptionLabel.AutoSize = True
        Me.DescriptionLabel.Font = New System.Drawing.Font("新細明體", 13.0!)
        Me.DescriptionLabel.Location = New System.Drawing.Point(81, 57)
        Me.DescriptionLabel.Name = "DescriptionLabel"
        Me.DescriptionLabel.Size = New System.Drawing.Size(0, 18)
        Me.DescriptionLabel.TabIndex = 2
        Me.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PluginListItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DescriptionLabel)
        Me.Controls.Add(Me.pluginName)
        Me.Controls.Add(Me.pluginIcon)
        Me.Name = "PluginListItem"
        Me.Size = New System.Drawing.Size(598, 86)
        CType(Me.pluginIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pluginIcon As PictureBox
    Friend WithEvents pluginName As Label
    Friend WithEvents DescriptionLabel As Label
End Class
