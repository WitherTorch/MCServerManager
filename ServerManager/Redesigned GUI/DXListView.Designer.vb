﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DXListView
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
        Me.ContextControl = New SharpDX.Windows.RenderControl()
        Me.SuspendLayout()
        '
        'ContextControl
        '
        Me.ContextControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContextControl.Location = New System.Drawing.Point(0, 0)
        Me.ContextControl.Name = "ContextControl"
        Me.ContextControl.Size = New System.Drawing.Size(398, 298)
        Me.ContextControl.TabIndex = 0
        '
        'DXListView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.ContextControl)
        Me.Name = "DXListView"
        Me.Size = New System.Drawing.Size(398, 298)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ContextControl As SharpDX.Windows.RenderControl
End Class
