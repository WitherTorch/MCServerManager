Imports System.ComponentModel
Imports System.Net
Imports System.Net.Sockets
Imports System.Text.RegularExpressions
Imports ServerManager

Public Class BedrockCommander
    Dim _process As Process
    Private watcher As BedrockServer.BedrockServerWatcher

    Sub New()

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
    End Sub

    Friend Sub New(watcher As BedrockServer.BedrockServerWatcher)
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
        processInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
        processInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
        processInfo.WorkingDirectory = directory
        processInfo.LoadUserProfile = True
        _process = Process.Start(processInfo)
        watcher._parent.ServerStatusLabel.Text = "伺服器狀態：啟動"
        _process.BeginOutputReadLine()
        _process.BeginErrorReadLine()
        _process.EnableRaisingEvents = True
        _process.StandardInput.AutoFlush = False

        AddHandler _process.ErrorDataReceived, Sub(sender, e)
                                                   Try
                                                       If IsNothing(_process) = False And _process.HasExited = False Then
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
        AddHandler _process.OutputDataReceived, Sub(sender, e)
                                                    Try
                                                        If IsNothing(_process) = False And _process.HasExited = False Then
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
        AddHandler _process.Exited, Sub(sender, e)
                                        Try
                                            _process = Nothing
                                            Console.WriteLine("Process Exited")
                                            Close()
                                            watcher._parent.ServerStatusLabel.Text = "伺服器狀態：關閉"
                                            If CType(watcher._parent.FindForm, Launcher).CanUPnP Then
                                                watcher._parent.ip = ""
                                                CType(watcher._parent.FindForm(), Launcher).upnpProvider.DestroyPort(watcher._parent.port)
                                            End If
                                        Catch ex As Exception
                                        End Try
                                    End Sub
    End Sub

    Private Sub Commander_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            If IsNothing(_process) = False OrElse _process.HasExited = False Then
                _process.Kill()
            End If
        Catch ex As Exception
        End Try
        watcher.watcher = Nothing
    End Sub


    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If IsNothing(_process) = False And _process.HasExited = False Then
            If e.KeyData = Keys.Enter Then
                _process.StandardInput.WriteLine(TextBox2.Text)
                TextBox2.Clear()
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        BeginInvoke(New Action(Sub()
                                   Label1.Text = "占用記憶體：" & Process.GetProcessById(_process.Id).WorkingSet64 & " 位元組"
                                   Label2.Text = "處理序ID：" & _process.Id
                               End Sub))
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub



    Private Sub Commander_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class
