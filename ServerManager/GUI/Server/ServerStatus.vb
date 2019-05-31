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
        _Server = Server.GetServer(serverDir)
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
                                                         If GlobalModule.Manager.ip.Contains(Server.ServerOptions("server-ip")) OrElse Server.ServerOptions("server-ip") = "" Then
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
        If Server.ServerVersionType = Server.EServerVersionType.Custom Then
            ServerVersion.Text = GetSimpleVersionName(Server.ServerVersionType, Server.ServerVersion) & "(程式檔案:" & New IO.FileInfo(Server.CustomServerRunFile).Name & ")"
        ElseIf Server.ServerVersionType = Server.EServerVersionType.Forge Then
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
        If Server.ServerVersionType = Server.EServerVersionType.Custom Then
            ServerVersion.Text = GetSimpleVersionName(Server.ServerVersionType, Server.ServerVersion) & "(程式檔案:" & New IO.FileInfo(Server.CustomServerRunFile).Name & ")"
        ElseIf Server.ServerVersionType = Server.EServerVersionType.Forge Then
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
                Dim filename As String
                Select Case Server.ServerType
                    Case Server.EServerType.Java
                        Select Case Server.ServerVersionType
                            Case Server.EServerVersionType.Vanilla
                                If Server.Server2ndVersion <> "" Then
                                    filename = IO.Path.Combine(Server.ServerPath, "minecraft_server." & Server.Server2ndVersion & ".jar")
                                Else
                                    filename = IO.Path.Combine(Server.ServerPath, "minecraft_server." & Server.ServerVersion & ".jar")
                                End If
                            Case Server.EServerVersionType.Forge
                                ' 1.1~1.2 > Server
                                ' 1.3 ~ > Universal
                                If New Version(Server.ServerVersion) >= New Version(1, 3) Then
                                    If New Version(Server.ServerVersion) >= New Version(1, 13) Then
                                        filename = IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & ".jar")
                                    Else
                                        filename = IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & "-universal" & ".jar")
                                    End If
                                Else
                                    filename = IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & "-server" & ".jar")
                                End If
                            Case Server.EServerVersionType.Spigot
                                filename = IO.Path.Combine(Server.ServerPath, "spigot-" & Server.ServerVersion & ".jar")
                            Case Server.EServerVersionType.CraftBukkit
                                filename = IO.Path.Combine(Server.ServerPath, "craftbukkit-" & Server.ServerVersion & ".jar")
                            Case Server.EServerVersionType.SpongeVanilla
                                filename = IO.Path.Combine(Server.ServerPath, "spongeVanilla-" & Server.ServerVersion & ".jar")
                            Case Server.EServerVersionType.Paper
                                filename = IO.Path.Combine(Server.ServerPath, "paper-" & Server.ServerVersion & ".jar")
                            Case Server.EServerVersionType.Akarin
                                filename = IO.Path.Combine(Server.ServerPath, "akarin-" & Server.ServerVersion & ".jar")
                            Case Server.EServerVersionType.Cauldron
                                filename = IO.Path.Combine(Server.ServerPath, "server.jar")
                            Case Server.EServerVersionType.Thermos
                                filename = IO.Path.Combine(Server.ServerPath, "Thermos-" & Server.ServerVersion & ".jar")
                            Case Server.EServerVersionType.Contigo
                                filename = IO.Path.Combine(Server.ServerPath, "Contigo-" & Server.ServerVersion & ".jar")
                            Case Server.EServerVersionType.Kettle
                                filename = IO.Path.Combine(Server.ServerPath, "kettle-git-HEAD-" & Server.Server2ndVersion & "-universal.jar")
                        End Select
                    Case Server.EServerType.Bedrock
                        Select Case Server.ServerVersionType
                            Case Server.EServerVersionType.Nukkit
                                '-Djline.terminal=jline.UnsupportedTerminal <- JLine Support
                                filename = IO.Path.Combine(Server.ServerPath, "nukkit-" & Server.Server2ndVersion & ".jar")
                            Case Server.EServerVersionType.VanillaBedrock
                                filename = IO.Path.Combine(Server.ServerPath, "bedrock_server.exe")
                        End Select
                    Case Server.EServerType.Custom
                        Select Case Server.ServerVersionType
                            Case Server.EServerVersionType.Custom
                                filename = Server.CustomServerRunFile
                        End Select
                End Select
                If String.IsNullOrEmpty(filename) = False AndAlso
                    String.IsNullOrWhiteSpace(filename) = False AndAlso
                    IO.File.Exists(filename) = False Then
                    Select Case MsgBox("找不到伺服器軟體，是否重新下載？", MsgBoxStyle.YesNo, Application.ProductName)
                        Case MsgBoxResult.Yes
                            If Server.ServerVersionType = Server.EServerVersionType.Forge Then
                                Dim helper As New ServerCreateHelper(Server, Server.ServerPath, Server.Server2ndVersion, True)
                                helper.ShowDialog()
                            Else
                                Dim helper As New ServerCreateHelper(Server, Server.ServerPath, True)
                                helper.ShowDialog()
                            End If
                        Case MsgBoxResult.No
                            Exit Sub
                    End Select
                End If
                If IsUnixLikeSystem = False Then
                    If Server.ServerVersionType = Server.EServerVersionType.VanillaBedrock Then
                        If Environment.OSVersion.Version.Major < 10 Then
                            MsgBox("此伺服器類型只能在Windows 10系統上運行!",, Application.ProductName)
                            Exit Sub
                        End If
                    Else
                        If GlobalModule.Manager.HasJava = False Then
                            MsgBox("未安裝Java 或 正在偵測",, Application.ProductName)
                            Exit Sub
                        End If
                    End If
                Else
                    If Server.ServerVersionType = Server.EServerVersionType.VanillaBedrock Then
                        MsgBox("此伺服器類型只能在Windows 10系統上運行!",, Application.ProductName)
                        Exit Sub
                    Else
                        If GlobalModule.Manager.HasJava = False Then
                            MsgBox("未安裝Java 或 正在偵測",, Application.ProductName)
                            Exit Sub
                        End If
                    End If
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
