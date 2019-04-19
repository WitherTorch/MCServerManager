Imports System.Threading.Tasks

Public Class ModPackServerStatus
    Public Event DeleteServer(NoUI As Boolean)
    Public ReadOnly Property Server As ModPackServer
    Public Event ServerLoaded()
    Protected Sub New(ByRef server As ModPackServer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Server = server
        AddHandler _Server.Initallised, AddressOf UpdateComponentOnFirstRun

    End Sub
    Sub New(serverDir As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()
        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Server = ModPackServer.GetServer(serverDir)
        AddHandler _Server.Initallised, AddressOf UpdateComponentOnFirstRun
    End Sub
    Friend Sub LoadStatus()
        Task.Run(Sub()
                     If _Server.IsInitallised = False Then
                         _Server.Initallise()
                     End If
                 End Sub)
        AddHandler _Server.ServerInfoUpdated, AddressOf UpdateComponent
        AddHandler _Server.ServerIconUpdated, Sub() ServerIcon.Image = Server.ServerIcon
        System.Threading.Tasks.Task.Run(Sub()
                                            If Not (IsNothing(_Server) = False AndAlso _Server.PackType <> ModPackServer.ModPackType.Error) Then
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
                                                                End Sub)
                                                Else
                                                    ServerName.Text = Server.ServerPathName
                                                    ServerIcon.Image = Server.ServerIcon
                                                    SettingButton.Enabled = False
                                                    RunButton.Enabled = False
                                                    SetVersionLabel()
                                                    ServerRunStatus.Text = "正在加載伺服器..."
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
                                                 Update()
                                             End Sub))
    End Sub
    Friend Overloads Sub SetVersionLabel()
        PackInfo.Text = "模組包：" & Server.PackName & " " & Server.PackVersion
    End Sub
    Friend Overloads Sub SetVersionLabel(addtionText As String)
        PackInfo.Text = "模組包：" & Server.PackName & " " & Server.PackVersion
        If addtionText <> "" Then
            PackInfo.Text &= (" " & addtionText)
        End If
    End Sub
End Class
