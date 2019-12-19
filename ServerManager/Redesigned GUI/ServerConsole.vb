Imports System.Threading.Tasks
Imports SharpDX.Direct3D11
Imports SharpDX.DXGI
Imports Device = SharpDX.Direct3D11.Device

Public Class ServerConsole
    Dim _server As ServerBase
    Dim hub As New ProcessMessageHub()
    Dim process As Process
#Region "DirectX Variants"
    Dim d As Device
    Dim sc As SwapChain
    Dim target As Texture2D
    Dim targetView As RenderTargetView
#End Region
    Public Sub New(ByRef server As ServerBase)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("類型", Color.Black, New Font(DxListView1.Font.FontFamily, 14), 75))
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("時間", Color.Black, New Font(DxListView1.Font.FontFamily, 14), 75))
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("訊息", Color.Black, New Font(DxListView1.Font.FontFamily, 14), 400))
        _server = server
        Dim scd As New SwapChainDescription() With {
        .BufferCount = 1,
        .Flags = SwapChainFlags.None,
        .IsWindowed = True,
        .ModeDescription = New ModeDescription(ClientSize.Width, ClientSize.Height, New Rational(60, 1), Format.R8G8B8A8_UNorm),
        .OutputHandle = Handle,
        .SampleDescription = New SampleDescription(1, 0),
        .SwapEffect = SwapEffect.Discard,
        .Usage = Usage.RenderTargetOutput
        }
        Try
            Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, scd, d, sc)
        Catch ex As Exception
            scd.ModeDescription.RefreshRate = New Rational(30, 1)
            Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, scd, d, sc)
        End Try
        target = Texture2D.FromSwapChain(Of Texture2D)(sc, 0)
        targetView = New RenderTargetView(d, target)
        d.ImmediateContext.OutputMerger.SetRenderTargets(targetView)
    End Sub

    Private Sub ServerConsole_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If _server.BeforeRunServer() AndAlso ConsoleBindings(_server) Is Me Then
            process = _server.RunServer()
            process.EnableRaisingEvents = True
            If process.HasExited Then
                _server.IsRunning = False
                _server.ProcessID = 0
            Else
                AddHandler process.OutputDataReceived, Sub(obj, args)
                                                           hub.AddMessage(args.Data)
                                                       End Sub
                AddHandler process.ErrorDataReceived, Sub(obj, args)
                                                          hub.AddErrorMessage(args.Data)
                                                      End Sub
                AddHandler process.Exited, Sub()
                                               _server.IsRunning = False
                                               _server.ProcessID = 0
                                           End Sub
                RAMPerformanceCounter.InstanceName = process.ProcessName
                process.BeginOutputReadLine()
                process.BeginErrorReadLine()
                AddHandler hub.MessageProcessed, Sub(msg)
                                                     For Each item In msg
                                                         If item Is Nothing Then Continue For
                                                         Dim fColor As Color
                                                         Select Case item.MessageType
                                                             Case MinecraftProcessMessage.ProcessMessageType.Info
                                                                 fColor = Color.Black
                                                             Case MinecraftProcessMessage.ProcessMessageType.Warning
                                                                 fColor = Color.Orange
                                                             Case MinecraftProcessMessage.ProcessMessageType.Error
                                                                 fColor = Color.Red
                                                             Case MinecraftProcessMessage.ProcessMessageType.Debug
                                                                 fColor = Color.YellowGreen
                                                             Case MinecraftProcessMessage.ProcessMessageType.Trace
                                                                 fColor = Color.Green
                                                             Case MinecraftProcessMessage.ProcessMessageType.Notify
                                                                 fColor = Color.Blue
                                                         End Select
                                                         DxListView1.Items.Add(New DXListView.DXListViewItem(New DXListView.DXListViewSubItem(item.ServerMessageTypeString) With {.ForeColor = fColor}, New DXListView.DXListViewSubItem(item.Time.Hour.ToString.PadLeft(2, "0") & ":" & item.Time.Minute.ToString.PadLeft(2, "0") & ":" & item.Time.Second.ToString.PadLeft(2, "0")) With {.ForeColor = fColor}, New DXListView.DXListViewSubItem(item.Message) With {.ForeColor = fColor}))
                                                     Next
                                                 End Sub
                Timer1.Start()
                Timer1.Enabled = True
            End If
        End If
    End Sub
    Private Sub ServerConsole_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Y < 30 OrElse e.X = Width - Margin.Right Then
            WinAPI.MoveForm(Handle)
        End If
    End Sub

    Private Sub ServerConsole_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim t As New Threading.Thread(Sub()
                                          Try
                                              _server.ShutdownServer()
                                          Catch ex As Exception

                                          End Try
                                      End Sub)
        t.IsBackground = False
        t.Start()
        hub.Dispose()
        If ConsoleBindings.ContainsKey(_server) Then ConsoleBindings.Remove(_server)
        GC.Collect()
        d.Dispose()
        sc.Dispose()
        target.Dispose()
        targetView.Dispose()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter AndAlso String.IsNullOrWhiteSpace(TextBox1.Text) = False Then
            Try
                process.StandardInput.WriteLine(TextBox1.Text)
                TextBox1.Clear()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If process IsNot Nothing AndAlso process.HasExited = False Then
            Dim memorymax As Decimal = ServerMemoryMax
            If TypeOf _server Is IMemoryChange Then
                If DirectCast(_server, IMemoryChange).MemoryMax > 0 Then
                    memorymax = DirectCast(_server, IMemoryChange).MemoryMax
                End If
            End If
            BeginInvokeIfRequired(Me, Sub()
                                          MetroLabel1.Text = String.Format("CPU 占用：{0} %", Math.Round(GetCpuUsageForProcess(process) * 10) / 10)
                                      End Sub)
            Dim vMem As Long = process.PeakPagedMemorySize64
            If vMem > 0 Then
                MetroLabel2.Text = String.Format("記憶體占用：{0}/{1} + {2}/{3}", GetReadableMemoryString(RAMPerformanceCounter.RawValue), GetReadableMemoryString(memorymax * 1048576), GetReadableMemoryString(vMem), GetReadableMemoryString(process.VirtualMemorySize64))
            Else
                MetroLabel2.Text = String.Format("記憶體占用：{0}/{1}", GetReadableMemoryString(RAMPerformanceCounter.RawValue), GetReadableMemoryString(memorymax * 1048576))
            End If
        End If
    End Sub
    Function GetReadableMemoryString(memory As Long) As String
        Dim Unit As String
        Dim ResultMems As Single
        If memory > 2 ^ 30 Then
            Unit = "GB"
            ResultMems = Math.Round(memory / (2 ^ 30) * 10) / 10
        ElseIf memory > 2 ^ 20 Then
            Unit = "MB"
            ResultMems = Math.Round(memory / (2 ^ 20) * 10) / 10
        ElseIf memory > 2 ^ 10 Then
            Unit = "KB"
            ResultMems = Math.Round(memory / (2 ^ 10) * 10) / 10
        Else
            Unit = "位元組"
        End If
        If Unit = "GB" AndAlso ResultMems > 1024 Then
            Unit = "TB"
            ResultMems = Math.Round(ResultMems / 1024 * 10) / 10
        End If
        Return ResultMems & " " & Unit
    End Function
    Dim oldCpuTime As Date = DateTime.UtcNow
    Dim oldCpuUsage As TimeSpan = New TimeSpan(0)
    Private Function GetCpuUsageForProcess(process As Process) As Double
        Dim cpuTIme = DateTime.UtcNow
        Dim CpuUsage = process.TotalProcessorTime
        Dim cpuUsedMs = (CpuUsage - oldCpuUsage).TotalMilliseconds
        Dim totalMsPassed = (cpuTIme - oldCpuTime).TotalMilliseconds
        Dim cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed)
        oldCpuUsage = CpuUsage
        oldCpuTime = cpuTIme
        Return cpuUsageTotal * 100
    End Function

    Private Sub ServerConsole_Load(sender As Object, e As EventArgs) Handles Me.Load
        If ConsoleBindings.ContainsKey(_server) Then
            Close()
        Else
            ConsoleBindings.Add(_server, Me)
        End If
    End Sub

    Private Sub ServerConsole_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        d.ImmediateContext.ClearRenderTargetView(targetView, SharpDXConverter.ConvertColor(Color.White))
        sc.Present(0, PresentFlags.None)
    End Sub
End Class