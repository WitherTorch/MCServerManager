Imports System.IO.Compression
Imports System.Threading.Tasks

Public Class ModPackServerCreateHelper
    Dim server As ModPackServer
    Dim client As New Net.WebClient()
    Dim path As String
    Const FTBServerDownloadLink As String = "http://ftb.cursecdn.com/FTB2/modpacks/{0}/{1}/{2}"
    Dim filename As String
    Dim dirname As String
    Public Sub New(server As ModPackServer, serverPath As String, serverFileName As String, serverDirName As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
        path = serverPath
        filename = serverFileName
        dirname = serverDirName
    End Sub
    Private Sub ModPackServerCreateHelper_Load(sender As Object, e As EventArgs) Handles Me.Load

        If My.Computer.FileSystem.DirectoryExists(path) = False Then
            BeginInvoke(New Action(Sub()
                                       StatusLabel.Text = "狀態：建立目錄中……"
                                       ProgressBar.Value = 0
                                   End Sub))
            My.Computer.FileSystem.CreateDirectory(path)



        End If
        Task.Run(New Action(Sub()
                                DownloadServer()
                            End Sub))





    End Sub
    Sub DownloadServer()

        Select Case server.PackType
            Case ModPackServer.ModPackType.FeedTheBeast
                Dim url = String.Format(FTBServerDownloadLink, dirname, server.PackVersion.Replace(".", "_"), filename)
                DownloadFile(url, IO.Path.Combine(IIf(path.EndsWith("\"), path, path & "\"), filename), ModPackServer.ModPackType.FeedTheBeast, server.PackVersion)
        End Select
    End Sub
    Sub DownloadFile(path As String, dist As String, packType As ModPackServer.ModPackType, version As String)
        BeginInvoke(New Action(Sub()
                                   StatusLabel.Text = "狀態：正在下載 " & server.PackName & " " & version & " ……"
                               End Sub))
        client.DownloadFileAsync(New Uri(path), dist)
        AddHandler client.DownloadProgressChanged, Sub(sender, e)
                                                       Try
                                                           BeginInvoke(New Action(Sub()
                                                                                      If e.TotalBytesToReceive = -1 Then
                                                                                          ProgressBar.Style = ProgressBarStyle.Marquee
                                                                                          ProgressBar.Value = 10
                                                                                      Else
                                                                                          If packType = ModPackServer.ModPackType.FeedTheBeast Then
                                                                                              ProgressBar.Value = e.ProgressPercentage * 0.8
                                                                                          Else
                                                                                              ProgressBar.Value = e.ProgressPercentage
                                                                                          End If
                                                                                      End If
                                                                                  End Sub))
                                                       Catch ex As Exception
                                                       End Try
                                                   End Sub
        AddHandler client.DownloadFileCompleted, Sub(sender, e)
                                                     If e.Cancelled = False Then
                                                         If packType = ModPackServer.ModPackType.FeedTheBeast Then
                                                             Invoke(Sub()
                                                                        StatusLabel.Text = "狀態：正在解壓縮模組包 ......"
                                                                        ProgressBar.Value = 80
                                                                    End Sub)
                                                             Using archive As ZipArchive = ZipFile.OpenRead(dist)
                                                                 For Each entry As ZipArchiveEntry In archive.Entries
                                                                     If entry.FullName.EndsWith("\") OrElse entry.FullName.EndsWith("/") Then
                                                                         If New IO.DirectoryInfo(IO.Path.Combine(IIf(Me.path.EndsWith("\"), Me.path, Me.path & "\"), entry.FullName)).Exists = False Then
                                                                             IO.Directory.CreateDirectory(IO.Path.Combine(IIf(Me.path.EndsWith("\"), Me.path, Me.path & "\"), entry.FullName))
                                                                         End If
                                                                     Else
                                                                         If New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith("\"), Me.path, Me.path & "\"), entry.FullName)).Directory.Exists = False Then
                                                                             Dim info = New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith("\"), Me.path, Me.path & "\"), entry.FullName))
                                                                             info.Directory.Create()
                                                                         End If
                                                                         If New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith("\"), Me.path, Me.path & "\"), entry.FullName)).Exists = False Then
                                                                             Dim info = New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith("\"), Me.path, Me.path & "\"), entry.FullName))
                                                                             info.Delete()
                                                                         End If
                                                                         entry.ExtractToFile(IO.Path.Combine(IIf(Me.path.EndsWith("\"), Me.path, Me.path & "\"), entry.FullName), True)
                                                                     End If
                                                                 Next
                                                             End Using
                                                         End If
                                                         BeginInvoke(New Action(Sub()
                                                                                    StatusLabel.Text = "狀態：正在配置伺服器 ......"
                                                                                    server.SaveServer()
                                                                                    GenerateServerEULA()
                                                                                    StatusLabel.Text = "狀態：完成!"
                                                                                    ProgressBar.Value = 100
                                                                                    ' GlobalModule.Manager.AddServer(Me.path, True)
                                                                                    'GlobalModule.Manager.ModpackServerPathList.Add(Me.path)
                                                                                    Me.Close()
                                                                                End Sub))
                                                     End If
                                                 End Sub
    End Sub
    Sub GenerateServerEULA()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(path, "eula.txt"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(path, "eula.txt"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.WriteLine("#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).")
            writer.WriteLine("#" & Now.ToString("ddd MMM dd HH:mm:ss K yyyy"), System.Globalization.CultureInfo.CurrentUICulture)
            writer.WriteLine("eula=true")
            writer.Flush()
            writer.Close()
        End Using
    End Sub
End Class