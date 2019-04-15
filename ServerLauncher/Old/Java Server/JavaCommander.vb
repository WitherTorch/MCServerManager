Imports System.ComponentModel
Imports System.Net
Imports System.Net.Sockets
Imports System.Text.RegularExpressions
Imports ServerManager

Public Class JavaCommander
    Dim Process As Process
    Private watcher As JavaServer.JavaServerWatcher

    Sub New()

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
    End Sub

    Friend Sub New(watcher As JavaServer.JavaServerWatcher)
        InitializeComponent()
        Me.watcher = watcher
    End Sub

    Friend Sub Run(program As String, args As String, directory As String)
        Dim processInfo As New ProcessStartInfo(program, args)
        processInfo.UseShellExecute = False
        processInfo.CreateNoWindow = True
        processInfo.RedirectStandardOutput = True
        processInfo.RedirectStandardInput = True
        processInfo.RedirectStandardError = True
        processInfo.WorkingDirectory = directory
        Process = Process.Start(processInfo)
        watcher._parent.ServerStatusLabel.Text = "伺服器狀態：啟動"
        Process.BeginOutputReadLine()
        Process.BeginErrorReadLine()
        Process.EnableRaisingEvents = True
        AddHandler Process.ErrorDataReceived, Sub(sender, e)
                                                  Try
                                                      If IsNothing(Process) = False And Process.HasExited = False Then
                                                          If IsNothing(e.Data) = False Then
                                                              If TextBox1.Text = "" Then
                                                                  TextBox1.AppendText(e.Data)
                                                              Else
                                                                  TextBox1.AppendText(vbCrLf & e.Data)
                                                              End If
                                                              Console.WriteLine("[Java Error Message] " & e.Data)
                                                          End If
                                                      End If
                                                  Catch ex As Exception
                                                  End Try
                                              End Sub
        AddHandler Process.OutputDataReceived, Sub(sender, e)
                                                   Try
                                                       If IsNothing(Process) = False And Process.HasExited = False Then
                                                           If IsNothing(e.Data) = False Then
                                                               If TextBox1.Text = "" Then
                                                                   TextBox1.AppendText(e.Data)
                                                               Else
                                                                   TextBox1.AppendText(vbCrLf & e.Data)
                                                               End If
                                                               Console.WriteLine("[Java Output Message] " & e.Data)
                                                           End If
                                                       End If
                                                   Catch ex As Exception
                                                   End Try
                                               End Sub
        AddHandler Process.Exited, Sub(sender, e)
                                       Try
                                           Process = Nothing
                                           Console.WriteLine("Process Exited")
                                           Close()
                                           watcher._parent.ServerStatusLabel.Text = "伺服器狀態：關閉"
                                       Catch ex As Exception
                                       End Try
                                   End Sub
    End Sub
    Function FitMemoryUnit(byteCount As System.Numerics.BigInteger) As String
        If byteCount >= System.Numerics.BigInteger.Pow(2, 80) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 80)).ToString & " YiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 70) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 70)).ToString & " ZiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 60) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 60)).ToString & " EiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 50) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 50)).ToString & " PiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 40) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 40)).ToString & " TiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 30) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 30)).ToString & " GiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 20) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 20)).ToString & " MiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 10) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 10)).ToString & " KiB"
        Else
            Return byteCount.ToString & " 位元組"
        End If
    End Function
    Private Sub Commander_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            Process.Kill()
        Catch ex As Exception
        Finally
            watcher._parent.ServerStatusLabel.Text = "伺服器狀態：關閉"
        End Try
        If CType(watcher._parent.FindForm, Launcher).CanUPnP Then
            CType(watcher._parent.FindForm(), Launcher).upnpProvider.DestroyPort(watcher._parent.port)
        End If
        watcher.watcher = Nothing
    End Sub


    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If IsNothing(Process) = False And Process.HasExited = False Then
            If e.KeyData = Keys.Enter Then
                Process.StandardInput.WriteLine(TextBox2.Text)
                TextBox2.Clear()
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        BeginInvoke(New Action(Sub()
                                   Label1.Text = "占用記憶體：" & FitMemoryUnit(Process.GetProcessById(Process.Id).WorkingSet64)
                                   Label2.Text = "處理序ID：" & Process.Id
                               End Sub))
    End Sub
End Class
