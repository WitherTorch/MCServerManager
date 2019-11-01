Imports System.Threading.Tasks

Public Class ServerConsole
    Dim _server As ServerBase
    Dim hub As New ProcessMessageHub()
    Dim process As Process
    Public Sub New(server As ServerBase)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("類型", Color.Black, New Font(DxListView1.Font.FontFamily, 14), 75))
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("時間", Color.Black, New Font(DxListView1.Font.FontFamily, 14), 75))
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("訊息", Color.Black, New Font(DxListView1.Font.FontFamily, 14), 400))
        _server = server
    End Sub

    Private Sub ServerConsole_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If _server.BeforeRunServer() Then
            process = _server.RunServer()
            process.EnableRaisingEvents = True
            If process.HasExited Then
                _server.IsRunning = False
                _server.ProcessID = 0
            Else
                AddHandler process.OutputDataReceived, Sub(obj, args) hub.AddMessage(args.Data)
                AddHandler process.ErrorDataReceived, Sub(obj, args) hub.AddMessage(args.Data)
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
                                                         DxListView1.Items.Add(New DXListView.DXListViewItem(New DXListView.DXListViewSubItem(item.ServerMessageTypeString), New DXListView.DXListViewSubItem(item.Time.Hour.ToString.PadLeft(2, "0") & ":" & item.Time.Minute.ToString.PadLeft(2, "0") & ":" & item.Time.Second.ToString.PadLeft(2, "0")), New DXListView.DXListViewSubItem(item.Message)))
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
        GC.Collect()
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
            If TypeOf _server Is Memoryable Then
                If DirectCast(_server, Memoryable).ServerMemoryMax > 0 Then
                    memorymax = DirectCast(_server, Memoryable).ServerMemoryMax
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
End Class