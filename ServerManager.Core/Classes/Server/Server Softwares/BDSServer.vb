Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class BDSServer
    Inherits ServerBase
    Protected seperator As String = IIf(IsUnixLikeSystem, "/", "\")
    Protected Shared VanillaBedrockVersion As Version
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(path As String)
        MyBase.New()
        GetOptions()
    End Sub
    Public Overrides Sub ReloadServer()
        MyBase.ReloadServer()
        GetOptions()
    End Sub
    Friend Shared Sub GetVersionList()
        VanillaBedrockVersion = Nothing
        Dim listURL As String = "https://minecraft.net/zh-hant/download/server/bedrock/"
        Try
            Dim versionNode = (New HtmlAgilityPack.HtmlWeb).Load(listURL).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[3]/div[1]/a[1]")
            Dim downloadLink As String = versionNode.GetAttributeValue("href", "")
            'bedrock-server-1.9.0.15.zip
            Dim regex As New Regex("[0-9]{1}.[0-9]{1,2}.[0-9]{1,2}.[0-9]{1,2}")
            If regex.IsMatch(downloadLink) Then
                Dim versionString As String = regex.Match(downloadLink).Value
                VanillaBedrockVersion = New Version(versionString)
            Else
                Throw New GetAvailableVersionsException
            End If
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Private ServerOptions As New BDSServerOptions
    Public Overrides Function GetServerProperties() As IServerProperties
        Return ServerOptions
    End Function
    Protected Friend Overridable Sub GetOptions()
        If String.IsNullOrWhiteSpace(ServerPath) Then
            Dim serverOptions As New BDSServerOptions()
            Me.ServerOptions = serverOptions.OutputOption
        Else
            Try
                If My.Computer.FileSystem.FileExists(IO.Path.Combine(ServerPath, "server.properties")) Then
                    Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(ServerPath, "server.properties"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                        Dim optionDict As New Dictionary(Of Integer, Boolean)
                        Do Until reader.EndOfStream
                            Try
                                Dim optionText As String = reader.ReadLine
                                If optionText.StartsWith("#") = False Then
                                    Dim options = optionText.Split("=", 2, StringSplitOptions.None)
                                    If options.Count = 2 Then
                                        ServerOptions.SetValue(options(0), options(1))
                                    ElseIf options.Count = 1 Then
                                        If options(0).Trim(" ") <> "" Then ServerOptions.SetValue(options(0), "")
                                    ElseIf options.Count = 0 Then
                                    Else
                                    End If
                                End If
                            Catch ex As Exception
                                Continue Do
                            End Try
                        Loop
                    End Using
                End If
            Catch fileException As IO.FileNotFoundException
            End Try
        End If
    End Sub
    Public Overrides Function CanUpdate() As Boolean
        Return Version.Parse(ServerVersion) < VanillaBedrockVersion
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "bedrock-" & ServerVersion & ".jar"
    End Function
    Public Overrides Function BeforeRunServer() As Boolean
        If Environment.OSVersion.Version.Major < 10 Then
            MsgBox("此伺服器類型只能在Windows 10系統上運行!",, APP_NAME)
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' 啟動伺服器
    ''' </summary>
    Public Overrides Function RunServer() As Process
        If ProcessID = 0 Then
            Dim processInfo As New ProcessStartInfo(GetServerFileName()) With {.WorkingDirectory = ServerPath}
            processInfo.UseShellExecute = False
            processInfo.CreateNoWindow = True
            processInfo.RedirectStandardOutput = True
            processInfo.RedirectStandardError = True
            processInfo.RedirectStandardInput = True
            processInfo.WorkingDirectory = ServerPath
            Dim returnProcess As Process = Process.Start(processInfo) '回傳一個處理序
            ProcessID = returnProcess.Id
            IsRunning = True
            Return returnProcess
        Else
            Return Process.GetProcessById(ProcessID)
        End If
    End Function
    Public Overrides Sub SaveServer()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(ServerPath, "server.properties"), "", False)
        Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(ServerPath, "server.properties"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), New System.Text.UTF8Encoding(False))
        writer.WriteLine("# Minecraft server properties")
        writer.WriteLine("#" & Now.ToString("ddd MMM dd HH:mm:ss K yyyy", System.Globalization.CultureInfo.CurrentUICulture))
        For Each [option] In ServerOptions.OutputOption
            writer.WriteLine(String.Format("{0}={1}", [option].Key, [option].Value))
        Next
        writer.WriteLine()
        writer.Flush()
        writer.Close()
        GenerateServerInfo()
    End Sub
    Public Overrides Sub ShutdownServer()
        If ProcessID <> 0 Then
            Dim process As Process = RunServer() '取得目前的處理序
            If process IsNot Nothing Then
                If process.HasExited = False Then
                    Try
                        process.StandardInput.WriteLine("stop")
                        Dim dog As New Watchdog(process) ' 呼叫看門狗程序去監測伺服器是否關閉
                        dog.Run()
                    Catch ex As Exception
                    End Try
                    process.WaitForExit()
                End If
            End If
            ProcessID = 0
            IsRunning = False
        End If
    End Sub
    Public Overrides Function GetInternalName() As String
        Return "VanillaBedrock"
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return ServerVersion
    End Function
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {}
    End Function
    Public Overrides Function DownloadAndInstallServer(targetServerVersion As String) As ServerDownloadTask
        Dim URL = "https://minecraft.azureedge.net/bin-win/bedrock-server-" & targetServerVersion & ".zip"
        Dim DownloadPath As String = IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "bedrock-" & targetServerVersion & ".zip")
        Dim task As New ServerDownloadTask
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnServerDownloading(percent * 0.8)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnServerDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               Call OnServerDownloadEnd(False)
                                               UnZipPackage(DownloadPath)
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnServerDownloadStart()
                                         End Sub
        task.Download(URL, DownloadPath)
        Return task
    End Function
    Public Overrides Function UpdateServer() As ServerDownloadTask
        Return DownloadAndInstallServer(VanillaBedrockVersion.ToString)
    End Function

    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {}
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
    End Sub

    Public Overrides Function GetAvailableVersions() As String()
        Return {VanillaBedrockVersion.ToString}
    End Function

    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvailableVersions()
    End Function
    Protected Sub UnZipPackage(dist As String)
        Using archive As ZipArchive = ZipFile.OpenRead(dist)
            Dim i As Integer = 0
            For Each entry As ZipArchiveEntry In archive.Entries
                i += 1
                OnServerDownloading(80 + i / archive.Entries.Count * 100 * 0.2)
                Try
                    If entry.FullName.EndsWith("\") OrElse entry.FullName.EndsWith("/") Then
                        If New IO.DirectoryInfo(IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), entry.FullName)).Exists = False Then
                            IO.Directory.CreateDirectory(IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), entry.FullName))
                        End If
                    Else
                        If New IO.FileInfo(IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), entry.FullName)).Directory.Exists = False Then
                            Dim info = New IO.FileInfo(IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), entry.FullName))
                            info.Directory.Create()
                        End If
                        If New IO.FileInfo(IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), entry.FullName)).Exists = False Then
                            Dim info = New IO.FileInfo(IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), entry.FullName))
                            info.Delete()
                        End If
                        entry.ExtractToFile(IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), entry.FullName), True)
                    End If
                Catch ex As Exception
                End Try
            Next
        End Using
    End Sub

End Class
