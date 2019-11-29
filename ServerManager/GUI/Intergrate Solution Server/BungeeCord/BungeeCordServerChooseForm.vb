Public Class BungeeCordServerChooseForm
    Dim _parent As BungeeCordSetting
    Sub New(parent As BungeeCordSetting)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _parent = parent
    End Sub
    Friend Sub AddServer(serverStatus As BungeeCordServerStatus)
        If serverStatus.Server.ServerAlias = "" Then
            Dim aName As String = ""
            Do
                aName = InputBox("請指定要用在伺服器指令上的別名(不可以有空格)", Application.ProductName, serverStatus.Server.Server.ServerPathName)
                If aName = "" Then
                    MsgBox("別名不可以是空的!")
                ElseIf aName.Contains(" ") Or aName.Contains("　") Then
                    MsgBox("別名不可以有空格!")
                Else
                    Exit Do
                End If
            Loop While (aName <> "")
            serverStatus.SetAlias(aName)
        End If
        _parent.host.Servers.Add(serverStatus.Server)
        ServerListPanel.Controls.Remove(serverStatus)
        Dim index = _parent.ServerListPanel.RowStyles.Count
        _parent.ServerListPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        _parent.ServerListPanel.Controls.Add(serverStatus, 0, index)
        serverStatus.SetSettingMode(BungeeSettingMode.Remove)
    End Sub

    Private Sub BungeeCordServerChooseForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Text = "選擇要加入" & _parent.host.BungeeType.ToString & "的伺服器"
        For Each server In GlobalModule.Manager.ServerEntityList
            If server.ServerType = Server.EServerType.Java Then
                Threading.Tasks.Task.Run(Sub()
                                             Dim isContained As Boolean = False
                                             For Each hostedServer In _parent.host.Servers
                                                 If hostedServer.Server.ServerPath = server.ServerPath Then
                                                     isContained = True
                                                     Exit For
                                                 End If
                                             Next
                                             BeginInvoke(Sub()
                                                             If isContained = False Then
                                                                 Dim index = ServerListPanel.RowStyles.Count
                                                                 Dim status As New BungeeCordServerStatus(New BungeeCordHost.BungeeServer(server), BungeeSettingMode.Add)
                                                                 ServerListPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))
                                                                 ServerListPanel.Controls.Add(status, 0, index)
                                                             End If
                                                         End Sub)
                                         End Sub)
            End If
        Next
    End Sub

End Class