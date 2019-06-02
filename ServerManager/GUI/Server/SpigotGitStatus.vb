Public Class SpigotGitStatus
    Sub New(ByRef server As Server)
        MyBase.New(server)
        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
    End Sub
    Sub New(serverDir As String)
        MyBase.New(serverDir) : InitializeComponent()
        isOverrides = True
        MyBase.ToolTip1.SetToolTip(Me.RunButton, "啟動 Spigot 伺服器")
    End Sub
    Protected Overrides Sub RunButton_Click(sender As Object, e As EventArgs) Handles RunButton.Click
        If IO.Directory.Exists(Server.ServerPath) = False Then
            MsgBox("伺服器資料夾消失了...",, Application.ProductName)
        Else
            If GlobalModule.Manager.HasJava = False Then
                MsgBox("未安裝Java 或 正在偵測",, Application.ProductName)
                Exit Sub
            End If
            Select Case Server.IsRunning
                Case True
                    If Server.ProcessID <> 0 Then
                        Try
                            Dim process As Process = Process.GetProcessById(Server.ProcessID)
                            Dim thread As New Threading.Thread(Sub()
                                                                   If process IsNot Nothing Then
                                                                       If process.HasExited = False Then
                                                                           Try
                                                                               process.StandardInput.WriteLine("stop")
                                                                               Dim dog As New Watchdog(process)
                                                                               dog.Run()
                                                                           Catch ex As Exception
                                                                           End Try
                                                                           process.WaitForExit()
                                                                       End If
                                                                       Server.IsRunning = False
                                                                   End If
                                                               End Sub)
                        Catch ex As Exception
                            Server.IsRunning = False
                        End Try
                    End If
                Case False
                    If IsNothing(console) = False AndAlso console.IsDisposed = False Then
                        BeginInvokeIfRequired(console, Sub()
                                                           console.ReloadUsesType(Server.EServerVersionType.Spigot)
                                                           console.Run()
                                                       End Sub)
                    Else
                        If IsNothing(setter) = False AndAlso setter.IsDisposed = False Then
                            MsgBox("請先關閉伺服器設定視窗!",, Application.ProductName)
                        Else
                            If IsNothing(console) Then
                                console = New ServerConsole(Server, Server.EServerVersionType.Spigot)
                            Else
                                If console.IsDisposed Then
                                    console = New ServerConsole(Server, Server.EServerVersionType.Spigot)
                                End If
                            End If
                            If console.Visible = False Then
                                FindForm.Invoke(Sub() console.Show())
                            End If
                        End If
                    End If
            End Select
        End If
    End Sub
    Private Sub RunButton2_Click(sender As Object, e As EventArgs) Handles RunButton2.Click
        If IO.Directory.Exists(Server.ServerPath) = False Then
            MsgBox("伺服器資料夾消失了...",, Application.ProductName)
        Else
            If GlobalModule.Manager.HasJava = False Then
                MsgBox("未安裝Java 或 正在偵測",, Application.ProductName)
                Exit Sub
            End If
            Select Case Server.IsRunning
                Case True
                    If Server.ProcessID <> 0 Then
                        Try
                            Dim process As Process = Process.GetProcessById(Server.ProcessID)
                            Dim thread As New Threading.Thread(Sub()
                                                                   If process IsNot Nothing Then
                                                                       If process.HasExited = False Then
                                                                           Try
                                                                               process.StandardInput.WriteLine("stop")
                                                                               Dim dog As New Watchdog(process)
                                                                               dog.Run()
                                                                           Catch ex As Exception
                                                                           End Try
                                                                           process.WaitForExit()
                                                                       End If
                                                                       Server.IsRunning = False
                                                                   End If
                                                               End Sub)
                        Catch ex As Exception
                            Server.IsRunning = False
                        End Try
                    End If
                Case False
                    If IsNothing(console) = False AndAlso console.IsDisposed = False Then
                        BeginInvokeIfRequired(console, Sub()
                                                           console.ReloadUsesType(Server.EServerVersionType.CraftBukkit)
                                                           console.Run()
                                                       End Sub)
                    Else
                        If IsNothing(setter) = False AndAlso setter.IsDisposed = False Then
                            MsgBox("請先關閉伺服器設定視窗!",, Application.ProductName)
                        Else
                            If IsNothing(console) Then
                                console = New ServerConsole(Server, Server.EServerVersionType.CraftBukkit)
                            Else
                                If console.IsDisposed Then
                                    console = New ServerConsole(Server, Server.EServerVersionType.CraftBukkit)
                                End If
                            End If
                            If console.Visible = False Then
                                FindForm.Invoke(Sub() console.Show())
                            End If
                        End If
                    End If
            End Select
        End If
    End Sub

    Protected Overrides Sub UpdateComponent()
        BeginInvokeIfRequired(Me, New Action(Sub()
                                                 ServerIcon.Image = Server.ServerIcon

                                                 ServerName.Text = Server.ServerPathName
                                                 If Server.IsRunning Then
                                                     ServerRunStatus.Text = "啟動狀態：已啟動"
                                                     SettingButton.Enabled = False
                                                     RunButton.Image = My.Resources.Stop32
                                                     RunButton2.Image = My.Resources.Stop32
                                                     ToolTip1.SetToolTip(RunButton, "停止伺服器")
                                                     ToolTip1.SetToolTip(RunButton2, "停止伺服器")
                                                 ElseIf isnothing(console) = False AndAlso console.isDisposed = False Then
                                                     ServerRunStatus.Text = "啟動狀態：未啟動(主控台運作中)"
                                                     SettingButton.Enabled = True
                                                     ToolTip1.SetToolTip(RunButton, "以Spigot模式重新啟動伺服器")
                                                     ToolTip1.SetToolTip(RunButton2, "以CraftBukkit模式重新啟動伺服器")
                                                     RunButton.Image = My.Resources.Run32Spigot
                                                     RunButton2.Image = My.Resources.Run32_Bukkit
                                                 Else
                                                     ServerRunStatus.Text = "啟動狀態：未啟動"
                                                     SettingButton.Enabled = True
                                                     ToolTip1.SetToolTip(RunButton, "啟動 Spigot 伺服器")
                                                     ToolTip1.SetToolTip(RunButton2, "啟動 CraftBukkit 伺服器")
                                                     RunButton.Image = My.Resources.Run32Spigot
                                                     RunButton2.Image = My.Resources.Run32_Bukkit
                                                 End If
                                                 SetVersionLabel()
                                                 Select Case Server.ServerType
                                                     Case Server.EServerType.Java
                                                         VersionTypeLabel.Text = "Java 版"
                                                     Case Server.EServerType.Bedrock
                                                         VersionTypeLabel.Text = "基岩版"
                                                 End Select
                                                 If GlobalModule.Manager.CanUPnP Then
                                                     Try
                                                         If GlobalModule.Manager.ip.Contains(Server.ServerOptions("server-ip")) OrElse Server.ServerOptions("server-ip") = "" Then
                                                             UPnPStatusLabel.Text = "支援 UPnP"
                                                         Else
                                                             UPnPStatusLabel.Text = ""
                                                         End If
                                                     Catch ex As Exception
                                                     End Try
                                                 End If


                                             End Sub))
    End Sub

End Class
