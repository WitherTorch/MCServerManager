Public Class BungeeCordStatus
    Public Event DeleteBungeeCordSolution(NoUI As Boolean)
    Public ReadOnly Property Host As BungeeCordHost
    Dim console As BungeeCordConsole
    Dim setter As BungeeCordSetting
    Public Event BungeeCordLoaded()
    Sub New(serverDir As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()
        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Host = BungeeCordHost.GetBungeeCordHost(serverDir)
    End Sub
    Private Sub ServerStatus_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(_Host) Then
            RaiseEvent DeleteBungeeCordSolution(True)
        Else
            UpdateComponentOnFirstRun()
            AddHandler _Host.BungeeInfoUpdated, AddressOf UpdateComponent

        End If
    End Sub
    Private Sub UpdateComponentOnFirstRun()
        UpdateComponent()
        RaiseEvent BungeeCordLoaded()
    End Sub
    Private Sub UpdateComponent()
        BeginInvoke(New Action(Sub()



                                   BungeeCordName.Text = Host.BungeePathName
                                   If Host.IsRunning Then
                                       BungeeCordRunStatus.Text = "啟動狀態：已啟動"
                                       RunButton.Enabled = False
                                       SettingButton.Enabled = False
                                   Else
                                       BungeeCordRunStatus.Text = "啟動狀態：未啟動"
                                       RunButton.Enabled = True
                                       SettingButton.Enabled = True
                                   End If
                                   SetVersionLabel()


                               End Sub))
    End Sub
    Friend Overloads Sub SetVersionLabel(Optional updating As Boolean = False, Optional updatingPercent As Integer = 0)
        If updating Then
            BungeeCordVersion.Text = "BungeeCord 版本：#" & Host.BungeeVersion & " [更新進度：" & updatingPercent & " %]"
        Else
            BungeeCordVersion.Text = "BungeeCord 版本：#" & Host.BungeeVersion
        End If

    End Sub
    Friend Overloads Sub SetVersionLabel(addtionText As String)
        If addtionText <> "" Then
            BungeeCordVersion.Text = "BungeeCord 版本：#" & Host.BungeeVersion & " " & addtionText
        Else
            BungeeCordVersion.Text = "BungeeCord 版本：#" & Host.BungeeVersion
        End If
    End Sub





    Private Sub SettingButton_Click(sender As Object, e As EventArgs) Handles SettingButton.Click
        If IsNothing(setter) Then
            setter = New BungeeCordSetting(Host)
        Else
            If setter.IsDisposed Then
                setter = New BungeeCordSetting(Host)
            End If
        End If
        If setter.Visible = False Then setter.Show(FindForm)
    End Sub

    Private Sub RunButton_Click(sender As Object, e As EventArgs) Handles RunButton.Click
        Threading.Tasks.Task.Run(Sub()
                                     If Host.CanUpdate Then
                                         Select Case MsgBox("檢查到BungeeCord有新的版本，要先更新再啟動嗎?", MsgBoxStyle.YesNo, Application.ProductName)
                                             Case MsgBoxResult.Yes
                                                 Dim isUpdated As Boolean = False
                                                 Using w = BungeeCordUpdater.DownloadUpdateAsync(Host)
                                                     Host.CanUpdate = False
                                                     AddHandler w.DownloadProgressChanged, Sub(obj, args)
                                                                                               BeginInvoke(Sub() SetVersionLabel(True, args.ProgressPercentage))
                                                                                           End Sub
                                                     AddHandler w.DownloadFileCompleted, Sub()
                                                                                             BeginInvoke(Sub() SetVersionLabel())
                                                                                             Host.SetVersion(BungeeCordUpdater.GetLatestVersionNumber)
                                                                                             isUpdated = True
                                                                                         End Sub
                                                     Do
                                                     Loop Until isUpdated
                                                 End Using
                                         End Select
                                     End If
                                     BeginInvoke(Sub()
                                                     Select Case Host.IsRunning
                                                         Case True
                                                             If IsNothing(console) = False Then
                                                                 If console.IsDisposed = False Then
                                                                     console.Close()
                                                                 End If
                                                             End If
                                                         Case False
                                                             If GlobalModule.Manager.RunningBungeeCord = False Then
                                                                 If IsNothing(console) Then
                                                                     console = New BungeeCordConsole(Host)
                                                                 Else
                                                                     If console.IsDisposed Then
                                                                         console = New BungeeCordConsole(Host)
                                                                     End If
                                                                 End If
                                                                 console.Show()
                                                                 GlobalModule.Manager.RunningBungeeCord = True
                                                             Else
                                                                 MsgBox("只能啟動一個BungeeCord 方案!")
                                                             End If

                                                     End Select
                                                 End Sub)
                                 End Sub)
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        CloseSolution()
        RaiseEvent DeleteBungeeCordSolution(False)
    End Sub
    Sub CloseSolution()
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
            Host.SaveSolution()
        Catch ex As Exception
        End Try
    End Sub
End Class
