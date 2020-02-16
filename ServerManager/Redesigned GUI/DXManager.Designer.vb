<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DXManager
    Inherits SharpDX.Windows.RenderForm

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DXManager))
        Me.RenderControl1 = New SharpDX.Windows.RenderControl()
        Me.CPUPerformanceCounter = New System.Diagnostics.PerformanceCounter()
        Me.DisplayTimer = New System.Windows.Forms.Timer(Me.components)
        Me.InputTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.CPUPerformanceCounter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RenderControl1
        '
        Me.RenderControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RenderControl1.Location = New System.Drawing.Point(0, 0)
        Me.RenderControl1.Name = "RenderControl1"
        Me.RenderControl1.Size = New System.Drawing.Size(800, 500)
        Me.RenderControl1.TabIndex = 0
        '
        'CPUPerformanceCounter
        '
        Me.CPUPerformanceCounter.CategoryName = "Processor"
        Me.CPUPerformanceCounter.CounterName = "% Processor Time"
        Me.CPUPerformanceCounter.InstanceName = "_Total"
        '
        'DisplayTimer
        '
        Me.DisplayTimer.Interval = 16
        '
        'InputTimer
        '
        Me.InputTimer.Enabled = True
        Me.InputTimer.Interval = 10
        '
        'DXManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 500)
        Me.Controls.Add(Me.RenderControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DXManager"
        Me.Text = "Minecraft 伺服器管理員"
        Me.TransparencyKey = System.Drawing.Color.Lavender
        CType(Me.CPUPerformanceCounter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RenderControl1 As SharpDX.Windows.RenderControl
    Friend WithEvents CPUPerformanceCounter As PerformanceCounter
    Friend WithEvents DisplayTimer As Timer
    Friend WithEvents InputTimer As Timer
End Class
