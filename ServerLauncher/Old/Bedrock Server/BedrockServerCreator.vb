Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Friend Class BedrockServerCreator
    Friend _owner As CreateBedrockServerDialog
    Private serverDir As String

    Private Sub ServerCreator_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Computer.FileSystem.DirectoryExists(_owner.ServerDirBox.Text) = False Then
            BeginInvoke(New Action(Sub()
                                       ProgressText.Text = "建立目錄中……"
                                       Progress.Value = 0
                                   End Sub))
            My.Computer.FileSystem.CreateDirectory(_owner.ServerDirBox.Text)



        End If
        Task.Factory.StartNew(New Action(Sub()
                                             DownloadServer()
                                         End Sub))





    End Sub
    Sub New(owner As CreateBedrockServerDialog, serverDir As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _owner = owner
        Me.serverDir = serverDir
    End Sub



    Sub DownloadServer()

        Select Case _owner.ServerTypeBox.SelectedIndex
            Case 0 ' Nukkit
                DownloadFile(GetNukkitDownloadURL(_owner._owner.NukkitVersionUrl), IO.Path.Combine(_owner.ServerDirBox.Text, "nukkit-" & _owner._owner.NukkitVersion & ".jar"))
        End Select
    End Sub

    Sub DownloadFile(path As String, dist As String)

        BeginInvoke(New Action(Sub()
                                   ProgressText.Text = "下載伺服器主程式中……"
                               End Sub))
        Dim client As New Net.WebClient()
        client.DownloadFileAsync(New Uri(path), dist)
        AddHandler client.DownloadProgressChanged, Sub(sender, e)
                                                       BeginInvoke(New Action(Sub()
                                                                                  Progress.Value = e.ProgressPercentage
                                                                              End Sub))
                                                   End Sub
        AddHandler client.DownloadFileCompleted, Sub(sender, e)
                                                     BeginInvoke(New Action(Sub()
                                                                                If _owner.ServerTypeBox.SelectedIndex = 0 Then
                                                                                    GenerateServerStartCMD()
                                                                                End If
                                                                                GenerateServerProperties()
                                                                                GenerateServerInfo()
                                                                                ProgressText.Text = "完成!"
                                                                                Progress.Value = 100
                                                                                _owner._owner.RegisterBedrockServer(serverDir)
                                                                                _owner._owner.AddBedrockServer(serverDir)
                                                                                _owner._owner.servers.Add(serverDir)
                                                                                Me.Close()
                                                                            End Sub))
                                                 End Sub

    End Sub

    Private Sub GenerateServerStartCMD()
        BeginInvoke(New Action(Sub()
                                   ProgressText.Text = "正在生成伺服器啟動指令……"
                               End Sub))
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(_owner.ServerDirBox.Text, "run.cmd"), "", False)
        Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(_owner.ServerDirBox.Text, "run.cmd"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
        writer.WriteLine("chcp 65001")
        writer.WriteLine("java.exe -Dfile.encoding = UTF8 - jar nukkit-" & _owner._owner.NukkitVersion & ".jar")
        writer.Flush()
        writer.Close()
    End Sub

    Sub GenerateServerProperties()
        Try
            BeginInvoke(New Action(Sub()
                                       ProgressText.Text = "正在生成伺服器設定……"
                                   End Sub))
            My.Computer.FileSystem.WriteAllText(IO.Path.Combine(_owner.ServerDirBox.Text, "server.properties"), "", False)
            Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(_owner.ServerDirBox.Text, "server.properties"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            Dim optionDict As New Dictionary(Of Integer, Boolean)
            For i = 0 To _owner.ServerOptionBox.RowCount - 2
                writer.WriteLine(String.Format("{0}={1}", _owner.ToOptionName(i), _owner.GetOptionValue(i)))
            Next
            Select Case _owner.ipType
                Case CreateBedrockServerDialog.ServerIPType.Default
                    writer.WriteLine("server-ip" & "=" & _owner.IPBox.Text)
                Case CreateBedrockServerDialog.ServerIPType.Custom
                    writer.WriteLine("server-ip" & "=" & _owner.IPBox.Text)
                Case CreateBedrockServerDialog.ServerIPType.UPnP
                    writer.WriteLine("server-ip" & "=" & _owner.IPBox.Text)
            End Select
            writer.WriteLine("server-port" & "=" & _owner.PortBox.Value)
            writer.WriteLine("generator-settings" & "=" & _owner.GeneratorSettingBox.Text)
            writer.WriteLine("level-name" & "=" & _owner.LevelNameBox.Text)
            writer.WriteLine("level-seed" & "=" & _owner.LevelSeedBox.Text)
            writer.WriteLine("level-type" & "=" & GetLevelType(_owner.LevelTypeBox.SelectedIndex))
            writer.Flush()
            writer.Close()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.Source & vbNewLine & ex.StackTrace & vbNewLine & ex.TargetSite.ToString,, ex.GetType.ToString)
        End Try
    End Sub

    Private Function GetLevelType(Index As Integer) As String
        Select Case Index
            Case 0 : Return "DEFAULT"
            Case 1 : Return "FLAT"
            Case 2 : Return "LARGEBIOMES"
            Case 3 : Return "AMPLIFIED"
            Case 4 : Return "CUSTOMIZED"
            Case Else : Return ""
        End Select
    End Function

    Sub GenerateServerInfo()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(_owner.ServerDirBox.Text, "server.info"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(_owner.ServerDirBox.Text, "server.info"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            Select Case _owner.ServerTypeBox.SelectedIndex
                Case 0
                    writer.WriteLine("server-version=" & "1.0")
                Case 1
                    writer.WriteLine("server-version=" & _owner.VersionBox.Text)
            End Select
            writer.WriteLine("server-version-type=" & GetServerType())
            If _owner.ipType = CreateBedrockServerDialog.ServerIPType.UPnP Then
                writer.WriteLine("server-upnp-enable=" & "true")
            Else
                writer.WriteLine("server-upnp-enable=" & "false")
            End If
            If _owner.ServerTypeBox.SelectedIndex = 0 Then
                writer.WriteLine("nukkit-build-version=" & _owner._owner.NukkitVersion)
            End If
            writer.Flush()
            writer.Close()
        End Using
    End Sub
    Function GetServerType() As String
        Select Case _owner.ServerTypeBox.SelectedIndex
            Case 0
                Return "Nukkit"
            Case 1
                Return "PocketMine"
        End Select
    End Function
    Private Sub ServerCreator_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        _owner.Close()
    End Sub
End Class