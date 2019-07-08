Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks

Public Class ModPackServerCreateHelper
    Dim server As ModPackServer
    Dim client As New Net.WebClient()
    Dim path As String
    Dim url As String
    Public Sub New(server As ModPackServer, serverDownloadURL As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
        path = server.ServerPath
        Select Case server.PackType
            Case ModPackServer.ModPackType.FeedTheBeast
                url = New Uri(New Uri("https://www.feed-the-beast.com"), serverDownloadURL).AbsoluteUri
            Case ModPackServer.ModPackType.CurseForge
                url = New Uri(New Uri("https://www.curseforge.com"), serverDownloadURL).AbsoluteUri
        End Select
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
                If IsUnixLikeSystem Then
                    DownloadFile(url, IO.Path.Combine(IIf(path.EndsWith("/"), path, path & "/"), "modpack_" & server.PackName & ".zip"), ModPackServer.ModPackType.FeedTheBeast)
                Else
                    DownloadFile(url, IO.Path.Combine(IIf(path.EndsWith("\"), path, path & "\"), "modpack_" & server.PackName & ".zip"), ModPackServer.ModPackType.FeedTheBeast)
                End If
            Case ModPackServer.ModPackType.CurseForge
                Dim request As Net.HttpWebRequest = Net.WebRequest.Create(url)
                Dim version = System.Environment.OSVersion.Version
                request.UserAgent = "Mozilla/5.0 (Windows NT " & version.Major & "." & version.Minor & ") Charcoal/" & CharcoalEngine.CHARCOAL_VER
                Dim realURI As Uri = request.GetResponse().ResponseUri
                If IsUnixLikeSystem Then
                    DownloadFile(realURI.AbsoluteUri, IO.Path.Combine(IIf(path.EndsWith("/"), path, path & "/"), "modpack_" & server.PackName & ".zip"), ModPackServer.ModPackType.CurseForge)
                Else
                    DownloadFile(realURI.AbsoluteUri, IO.Path.Combine(IIf(path.EndsWith("\"), path, path & "\"), "modpack_" & server.PackName & ".zip"), ModPackServer.ModPackType.CurseForge)
                End If
        End Select
    End Sub
    Sub DownloadFile(path As String, dist As String, packType As ModPackServer.ModPackType)
        BeginInvoke(New Action(Sub()
                                   StatusLabel.Text = "狀態：正在下載 " & server.PackName & " ……"
                               End Sub))
        client.DownloadFileAsync(New Uri(path), dist)
        AddHandler client.DownloadProgressChanged, Sub(sender, e)
                                                       Try
                                                           BeginInvoke(New Action(Sub()
                                                                                      If e.TotalBytesToReceive = -1 Then
                                                                                          ProgressBar.Style = ProgressBarStyle.Marquee
                                                                                          ProgressBar.Value = 10
                                                                                      Else
                                                                                          If packType = ModPackServer.ModPackType.FeedTheBeast OrElse packType = ModPackServer.ModPackType.CurseForge Then
                                                                                              ProgressBar.Value = e.ProgressPercentage * 0.5
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
                                                         If packType = ModPackServer.ModPackType.FeedTheBeast OrElse packType = ModPackServer.ModPackType.CurseForge Then
                                                             Invoke(Sub()
                                                                        StatusLabel.Text = "狀態：正在解壓縮模組包 ......"
                                                                        ProgressBar.Value = 50
                                                                    End Sub)
                                                             Dim seperator As String = IIf(IsUnixLikeSystem, "/", "\")
                                                             Using archive As ZipArchive = ZipFile.OpenRead(dist)
                                                                 For Each entry As ZipArchiveEntry In archive.Entries
                                                                     Try
                                                                         If entry.FullName.EndsWith("\") OrElse entry.FullName.EndsWith("/") Then
                                                                             If New IO.DirectoryInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName)).Exists = False Then
                                                                                 IO.Directory.CreateDirectory(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName))
                                                                             End If
                                                                         Else
                                                                             If New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName)).Directory.Exists = False Then
                                                                                 Dim info = New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName))
                                                                                 info.Directory.Create()
                                                                             End If
                                                                             If New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName)).Exists = False Then
                                                                                 Dim info = New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName))
                                                                                 info.Delete()
                                                                             End If
                                                                             entry.ExtractToFile(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName), True)
                                                                         End If
                                                                     Catch ex As Exception
                                                                     End Try
                                                                 Next
                                                             End Using
                                                             Invoke(Sub()
                                                                        StatusLabel.Text = "狀態：正在安裝模組包 ......"
                                                                        ProgressBar.Value = 70
                                                                    End Sub)
                                                             Try
                                                                 RunBatch(Me.path, packType)
                                                             Catch ex As Exception
                                                             End Try
                                                             Dim dinfo As New IO.DirectoryInfo(Me.path)
                                                             If IO.Directory.Exists(IO.Path.Combine(server.ServerPath, "libraries")) = False Then
                                                                 Dim files = dinfo.GetFiles("forge*-installer.jar", IO.SearchOption.TopDirectoryOnly)
                                                                 If files IsNot Nothing And files.Count > 0 Then
                                                                     RunAndWait(New ProcessStartInfo("java", String.Format("-jar ""{0}"" --installServer", files(0).FullName)) With {.WorkingDirectory = dinfo.FullName})
                                                                 End If
                                                             End If
                                                             If server.ServerRunJAR = "" Then
                                                                 Dim files = dinfo.GetFiles("FTB*.jar", IO.SearchOption.TopDirectoryOnly)
                                                                 If files IsNot Nothing And files.Count > 0 Then
                                                                     server.ServerRunJAR = files(0).FullName
                                                                 Else
                                                                     Erase files
                                                                     files = dinfo.GetFiles("forge*-universal.jar", IO.SearchOption.TopDirectoryOnly)
                                                                     If files IsNot Nothing And files.Count > 0 Then
                                                                         server.ServerRunJAR = files(0).Name
                                                                     End If
                                                                 End If
                                                             End If
                                                             If String.IsNullOrWhiteSpace(server.InternalJavaArguments) Then
                                                                 If IO.File.Exists(IO.Directory.Exists(IO.Path.Combine(server.ServerPath, "settings.cfg"))) Then
                                                                     Dim reader As New IO.StreamReader(IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "settings.cfg"))
                                                                     Do Until reader.EndOfStream
                                                                         Dim command As String = reader.ReadLine.Trim
                                                                         If command.ToUpper().StartsWith("JAVA_ARGS=") Then
                                                                             server.InternalJavaArguments = command.Substring(9)
                                                                             Exit Do
                                                                         End If
                                                                     Loop
                                                                 End If
                                                             End If
                                                         End If
                                                         BeginInvoke(New Action(Sub()
                                                                                    StatusLabel.Text = "狀態：正在配置伺服器 ......"
                                                                                    server.SaveServer()
                                                                                    GenerateServerEULA()
                                                                                    StatusLabel.Text = "狀態：完成!"
                                                                                    ProgressBar.Value = 100
                                                                                    GlobalModule.Manager.AddModpackServer(Me.path, True)
                                                                                    GlobalModule.Manager.ModpackServerPathList.Add(Me.path)
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
    Sub RunBatch(path As String, packtype As ModPackServer.ModPackType)
        Dim seperator As String = IIf(IsUnixLikeSystem, "/", "\")
        Dim batchVariantDictionary As New Dictionary(Of String, String)
        Dim filename As String = ""
        Select Case packtype
            Case ModPackServer.ModPackType.FeedTheBeast
                filename = "FTBInstall.bat"
            Case ModPackServer.ModPackType.CurseForge
                filename = "Install.bat"
                If IO.File.Exists(IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), filename)) = False Then
                    filename = "FTBInstall.bat"
                End If
        End Select
        Dim reader As New IO.StreamReader(IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), filename))
        Do Until reader.EndOfStream
            Dim command As String = reader.ReadLine.Trim
            For Each variantPair In batchVariantDictionary
                If command.Contains(variantPair.Key) Then _
                                                                         command = command.Replace(variantPair.Key, variantPair.Value)
            Next
            If command.EndsWith("> NUL 2>&1") Then
                command = command.Substring(0, command.Length - 10).TrimEnd
            End If
            Select Case command
                Case ""
                                                                         ' Do Nothing
                Case "call settings.bat"
                    Dim settingsReader As New IO.StreamReader(IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "settings.bat"))
                    Do Until settingsReader.EndOfStream
                        Dim settingCode As String = settingsReader.ReadLine
                        If String.IsNullOrEmpty(settingCode) = False Then
                            settingCode = settingCode.Trim()
                            If settingCode.StartsWith("set ") Then
                                settingCode = settingCode.Substring(4)
                                If settingCode.Contains("=") AndAlso settingCode.IndexOf("=") <> settingCode.Length - 1 Then
                                    Dim keyValueSplit As String() = settingCode.Split(New Char() {"="}, 2)
                                    Dim Value = keyValueSplit(1)
                                    For Each variantPair In batchVariantDictionary
                                        If Value.Contains(variantPair.Key) Then _
                                                                         Value = Value.Replace(variantPair.Key, variantPair.Value)
                                    Next
                                    batchVariantDictionary.Add(String.Format("%{0}%", keyValueSplit(0)), Value)
                                End If
                            End If
                        End If
                    Loop
                Case Else
                    If command.StartsWith("@echo") OrElse command.StartsWith("echo") OrElse command.StartsWith("REM") OrElse command.StartsWith(":") Then Continue Do
                    If command.Contains(" ") AndAlso command.IndexOf(" ") <> command.Length - 1 Then
                        Dim commandArgs As String() = command.Split(New String() {" "}, 2, StringSplitOptions.RemoveEmptyEntries)
                        If commandArgs.Count >= 2 Then
                            If IO.File.Exists(IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), commandArgs(0))) Then
                                RunAndWait(New ProcessStartInfo(IIf(path.EndsWith(seperator), path, path & seperator) & commandArgs(0), commandArgs(1)) With {.WorkingDirectory = IIf(path.EndsWith(seperator), path, path & seperator)})
                            ElseIf IO.Directory.Exists(IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), commandArgs(0))) Then
                                Process.Start(New ProcessStartInfo(IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), commandArgs(0))) With {.WorkingDirectory = IIf(path.EndsWith(seperator), path, path & seperator)})
                            Else
                                RunAndWait(New ProcessStartInfo(commandArgs(0), commandArgs(1)) With {.WorkingDirectory = IIf(path.EndsWith(seperator), path, path & seperator)})
                            End If
                        Else
                            If IO.File.Exists(IIf(path.EndsWith(seperator), path, path & seperator) & commandArgs(0)) Then
                                RunAndWait(New ProcessStartInfo(IIf(path.EndsWith(seperator), path, path & seperator) & commandArgs(0)) With {.WorkingDirectory = IIf(path.EndsWith(seperator), path, path & seperator)})
                            ElseIf IO.Directory.Exists(IIf(path.EndsWith(seperator), path, path) & seperator & commandArgs(0)) Then
                                Process.Start(New ProcessStartInfo(IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), commandArgs(0))) With {.WorkingDirectory = IIf(path.EndsWith(seperator), path, path & seperator)})
                            Else
                                RunAndWait(New ProcessStartInfo(commandArgs(0)) With {.WorkingDirectory = IIf(path.EndsWith(seperator), path, path & seperator)})
                            End If
                        End If
                    Else
                        If IO.File.Exists(IIf(path.EndsWith(seperator), path, path & seperator) & command) Then
                            RunAndWait(New ProcessStartInfo(IIf(path.EndsWith(seperator), path, path & seperator) & command) With {.WorkingDirectory = IIf(path.EndsWith(seperator), path, path & seperator)})
                        ElseIf IO.Directory.Exists(IIf(path.EndsWith(seperator), path, path & seperator) & command) Then
                            Process.Start(New ProcessStartInfo(IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), command)) With {.WorkingDirectory = IIf(path.EndsWith(seperator), path, path & seperator)})
                        Else
                            RunAndWait(New ProcessStartInfo(command) With {.WorkingDirectory = IIf(path.EndsWith(seperator), path, path & seperator)})
                        End If
                    End If
            End Select
        Loop
        If batchVariantDictionary.ContainsKey("%FORGEJAR%") Then
            server.ServerRunJAR = batchVariantDictionary("%FORGEJAR%")
        End If
        If batchVariantDictionary.ContainsKey("%JAVA_PARAMETERS%") Then
            server.InternalJavaArguments = batchVariantDictionary("%JAVA_PARAMETERS%")
        End If
    End Sub
    Sub RunAndWait(processInfo As ProcessStartInfo)
        If processInfo.FileName = "mkdir" Then
            Try
                MkDir(processInfo.Arguments)
            Catch ex As Exception

            End Try
        Else
            processInfo.UseShellExecute = False
            Dim process As Process = Process.Start(processInfo)
            process.EnableRaisingEvents = True
            Dim flag As Boolean = False
                AddHandler process.OutputDataReceived, Sub(sender, e)
                                                           Console.WriteLine(IIf(String.IsNullOrEmpty(e.Data), "", e.Data))
                                                       End Sub
                AddHandler process.ErrorDataReceived, Sub(sender, e)
                                                          Console.WriteLine(IIf(String.IsNullOrEmpty(e.Data), "", "[Batch Error] " & e.Data))
                                                      End Sub
                AddHandler process.Exited, Sub()
                                               flag = True
                                           End Sub
                Do Until flag

                Loop
            End If
    End Sub
End Class