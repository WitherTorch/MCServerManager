Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class BDSServer
    Inherits ServerBase
    Protected seperator As String = IIf(IsUnixLikeSystem, "/", "\")
    Protected Shared VanillaBedrockVersion As Version
    Private Shared SnapshotList As List(Of String)
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(path As String)
        MyBase.New(path)
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
    Private _ServerOptions As JavaServerOptions
    ''' <summary>
    ''' 伺服器主設定檔(server.properties)
    ''' </summary>
    ''' <returns></returns>
    Property ServerOptions As JavaServerOptions
        Get
            Return _ServerOptions
        End Get
        Protected Set(value As JavaServerOptions)
            _ServerOptions = value
        End Set
    End Property

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
        Return False
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "bedrock-." & ServerVersion & ".jar"
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
                                                     Call OnServerDownloading(percent)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnServerDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               Call OnServerDownloadEnd(False)
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
End Class
