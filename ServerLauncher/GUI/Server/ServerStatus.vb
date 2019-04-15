Imports System.Threading.Tasks

Public Class ServerStatus
    Public Event DeleteServer(NoUI As Boolean)
    Public ReadOnly Property Server As Server
    Protected console As ServerConsole
    Protected setter As ServerSetter
    Public Event ServerLoaded()
    Protected Sub New(ByRef server As Server)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Server = server
        AddHandler _Server.Initallised, AddressOf UpdateComponentOnFirstRun
        GlobalModule.Manager.ServerEntityList.Add(_Server)

    End Sub
    Sub New(serverDir As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()
        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Server = Server.GetServer(serverDir, Me)
        AddHandler _Server.Initallised, AddressOf UpdateComponentOnFirstRun
        GlobalModule.Manager.ServerEntityList.Add(_Server)
    End Sub
    Friend Sub LoadStatus()
        Task.Run(Sub()
                     If _Server.IsInitallised = False Then
                         _Server.Initallise()
                     End If
                 End Sub)
        AddHandler _Server.ServerInfoUpdated, AddressOf UpdateComponent
        AddHandler _Server.ServerIconUpdated, Sub() ServerIcon.Image = Server.ServerIcon
        AddHandler _Server.ServerUpdateStart, Sub() SetVersionLabel(True)
        AddHandler _Server.ServerUpdating, Sub(progress) SetVersionLabel(True, progress)
        AddHandler _Server.ServerUpdateEnd, Sub() SetVersionLabel()
        System.Threading.Tasks.Task.Run(Sub()
                                            If Not (IsNothing(_Server) = False AndAlso _Server.ServerType <> Server.EServerType.Error) Then
                                                RaiseEvent DeleteServer(True)
                                            Else
                                                If InvokeRequired Then
                                                    BeginInvoke(Sub()
                                                                    ServerName.Text = Server.ServerPathName
                                                                    ServerIcon.Image = Server.ServerIcon
                                                                    SettingButton.Enabled = False
                                                                    RunButton.Enabled = False
                                                                    SetVersionLabel()
                                                                    ServerRunStatus.Text = "正在加載伺服器..."
                                                                    Select Case Server.ServerType
                                                                        Case Server.EServerType.Java
                                                                            VersionTypeLabel.Text = "Java 版"
                                                                        Case Server.EServerType.Bedrock
                                                                            VersionTypeLabel.Text = "基岩版"
                                                                    End Select
                                                                End Sub)
                                                Else
                                                    ServerName.Text = Server.ServerPathName
                                                    ServerIcon.Image = Server.ServerIcon
                                                    SettingButton.Enabled = False
                                                    RunButton.Enabled = False
                                                    SetVersionLabel()
                                                    ServerRunStatus.Text = "正在加載伺服器..."
                                                    Select Case Server.ServerType
                                                        Case Server.EServerType.Java
                                                            VersionTypeLabel.Text = "Java 版"
                                                        Case Server.EServerType.Bedrock
                                                            VersionTypeLabel.Text = "基岩版"
                                                    End Select
                                                End If

                                            End If
                                        End Sub)
    End Sub
    Private Sub UpdateComponentOnFirstRun()
        BeginInvokeIfRequired(Me, Sub()
                                      SettingButton.Enabled = True
                                      RunButton.Enabled = True
                                      UpdateComponent()
                                      RaiseEvent ServerLoaded()
                                      RemoveHandler _Server.Initallised, AddressOf UpdateComponentOnFirstRun
                                  End Sub)
    End Sub
    Protected Overridable Sub UpdateComponent()
        BeginInvokeIfRequired(Me, New Action(Sub()


                                                 ServerIcon.Image = Server.ServerIcon

                                                 ServerName.Text = Server.ServerPathName
                                                 If Server.IsRunning Then
                                                     ServerRunStatus.Text = "啟動狀態：已啟動"
                                                     SettingButton.Enabled = False
                                                     RunButton.Image = My.Resources.Stop32
                                                     ToolTip1.SetToolTip(RunButton, "停止伺服器")
                                                 Else
                                                     ServerRunStatus.Text = "啟動狀態：未啟動"
                                                     SettingButton.Enabled = True
                                                     ToolTip1.SetToolTip(RunButton, "啟動伺服器")
                                                     RunButton.Image = My.Resources.Run32
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
                                                         If Server.ServerOptions("server-ip") = GlobalModule.Manager.ip OrElse Server.ServerOptions("server-ip") = "" Then
                                                             UPnPStatusLabel.Text = "支援 UPnP"
                                                         Else
                                                             UPnPStatusLabel.Text = ""
                                                         End If
                                                     Catch ex As Exception
                                                     End Try
                                                 End If

                                                 Update()
                                             End Sub))
    End Sub
    Friend Overloads Sub SetVersionLabel(Optional updating As Boolean = False, Optional updatingPercent As Integer = 0)
        If Server.ServerVersionType = Server.EServerVersionType.Forge Then
            ServerVersion.Text = "伺服器版本：" & "原版" & " " & Server.ServerVersion & " (Forge 版本：" & Server.Server2ndVersion & ")"
        ElseIf Server.ServerVersionType = Server.EServerVersionType.Vanilla Then
            If Server.Server2ndVersion <> "" Then
                ServerVersion.Text = "伺服器版本：" & "原版" & " " & Server.Server2ndVersion
            Else
                ServerVersion.Text = "伺服器版本：" & "原版" & " " & Server.ServerVersion
            End If
        ElseIf Server.ServerVersionType = Server.EServerVersionType.Nukkit Then
            ServerVersion.Text = "伺服器版本：" & GetSimpleVersionName(Server.ServerVersionType, Server.ServerVersion) & " " & Server.ServerVersion & " (" & Server.Server2ndVersion & ")"
        Else
            ServerVersion.Text = "伺服器版本：" & GetSimpleVersionName(Server.ServerVersionType, Server.ServerVersion) & " " & Server.ServerVersion
        End If
        If updating Then
            ServerVersion.Text &= " [更新進度：" & updatingPercent & " %]"
        End If
    End Sub
    Friend Overloads Sub SetVersionLabel(addtionText As String)
        If Server.ServerVersionType = Server.EServerVersionType.Forge Then
            ServerVersion.Text = "伺服器版本：" & "原版" & " " & Server.ServerVersion & " (Forge 版本：" & Server.Server2ndVersion & ")"
        ElseIf Server.ServerVersionType = Server.EServerVersionType.Vanilla Then
            If Server.Server2ndVersion <> "" Then
                ServerVersion.Text = "伺服器版本：" & "原版" & " " & Server.Server2ndVersion
            Else
                ServerVersion.Text = "伺服器版本：" & "原版" & " " & Server.ServerVersion
            End If
        ElseIf Server.ServerVersionType = Server.EServerVersionType.Nukkit Then
            ServerVersion.Text = "伺服器版本：" & GetSimpleVersionName(Server.ServerVersionType, Server.ServerVersion) & " " & Server.ServerVersion & " (#" & Server.Server2ndVersion & ")"
        Else
            ServerVersion.Text = "伺服器版本：" & GetSimpleVersionName(Server.ServerVersionType, Server.ServerVersion) & " " & Server.ServerVersion
        End If
        If addtionText <> "" Then
            ServerVersion.Text &= (" " & addtionText)
        End If
    End Sub





    Private Sub SettingButton_Click(sender As Object, e As EventArgs) Handles SettingButton.Click
        If IO.Directory.Exists(Server.ServerPath) = False Then
            MsgBox("伺服器資料夾消失了...",, Application.ProductName)
        Else
            If IsNothing(setter) Then
                setter = New ServerSetter(Server)
            Else
                If setter.IsDisposed Then
                    setter = New ServerSetter(Server)
                End If
            End If
            setter.Show()
        End If
    End Sub
    Protected isOverrides As Boolean
    Protected Overridable Sub RunButton_Click(sender As Object, e As EventArgs) Handles RunButton.Click
        If isOverrides = False Then
            If IO.Directory.Exists(Server.ServerPath) = False Then
                MsgBox("伺服器資料夾消失了...",, Application.ProductName)
            Else
                If Server.ServerVersionType = Server.EServerVersionType.VanillaBedrock AndAlso
                Environment.OSVersion.Version.Major < 10 Then
                    MsgBox("此伺服器類型只能在Windows 10系統上運行!",, Application.ProductName)
                Else
                    Select Case Server.IsRunning
                        Case True
                            If Server.ProcessID <> 0 Then
                                Try
                                    Dim process As Process = Process.GetProcessById(Server.ProcessID)
                                    Dim thread As New Threading.Thread(Sub()
                                                                           If process IsNot Nothing Then
                                                                               If process.HasExited = False Then
                                                                                   Try
                                                                                       console.InputToConsole("stop")
                                                                                       Dim dog As New Watchdog(process)
                                                                                       dog.Run()
                                                                                   Catch ex As Exception
                                                                                   End Try
                                                                                   process.WaitForExit()
                                                                                   Server.ProcessID = 0
                                                                               End If
                                                                               Server.IsRunning = False
                                                                           End If
                                                                       End Sub)
                                    thread.IsBackground = True
                                    thread.Start()
                                Catch ex As Exception
                                    Server.IsRunning = False
                                End Try
                            End If
                        Case False
                            If IsNothing(setter) = False AndAlso setter.IsDisposed = False Then
                                MsgBox("請先關閉伺服器設定視窗!",, Application.ProductName)
                            Else
                                If IsNothing(console) Then
                                    console = New ServerConsole(Server)
                                Else
                                    If console.IsDisposed Then
                                        console = New ServerConsole(Server)
                                    End If
                                End If
                                If console.Visible = False Then
                                    FindForm.Invoke(Sub() console.Show())
                                End If
                            End If
                    End Select
                End If
            End If
        End If
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        CloseServer()
        RaiseEvent DeleteServer(False)
    End Sub
    Sub CloseServer()
        GlobalModule.Manager.ServerEntityList.Remove(Server)
        If IsNothing(console) Then
        Else
            If console.IsDisposed Then
            Else
                console.Close()
            End If
        End If
        If IsNothing(setter) Then
        Else
            If setter.IsDisposed Then
            Else
                setter.Close()
            End If
        End If
        Try
            Server.SaveServer()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ShowDirButton_Click(sender As Object, e As EventArgs) Handles ShowDirButton.Click
        If IO.Directory.Exists(Server.ServerPath) = False Then
            MsgBox("伺服器資料夾消失了...",, Application.ProductName)
        Else
            Process.Start(Server.ServerPath)
        End If
    End Sub
End Class
