Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class SpigotWithGitServer
    Inherits SpigotServer
    Private Server2ndVersion As String
    Private Shared SpigotGitVersionList As New List(Of String)
    Friend Shared Shadows Sub GetVersionList()
        SpigotGitVersionList.Clear()
        Dim listURL As String = "https://hub.spigotmc.org/versions/"
        Try
            Dim client As New Net.WebClient()
            client.Encoding = System.Text.Encoding.UTF8
            Dim versionList = (New HtmlAgilityPack.HtmlWeb).Load(listURL).DocumentNode.SelectNodes("/html[1]/body[1]/pre[1]/a")
            For Each version In versionList
                Dim versionText As String = version.InnerText
                If versionText.EndsWith(".json") Then
                    versionText = versionText.Substring(0, versionText.Length - 5)
                    Dim versionRegex As New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*")
                    If versionRegex.IsMatch(versionText) Then
                        If versionRegex.Match(versionText).Value = versionText Then
                            If versionText.Contains(".") Then
                                SpigotGitVersionList.Add(versionText)
                            End If
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
        SpigotGitVersionList = SpigotGitVersionList.OrderBy(Function(v As String) As Version
                                                                Return New Version(v)
                                                            End Function).ToList
        SpigotGitVersionList.Reverse()
    End Sub
    Public Overrides Function GetAvailableVersions() As String()
        Return SpigotGitVersionList.ToArray()
    End Function
    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvailableVersions()
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "server-memory-max"
                ServerMemoryMax = IIf(IsNumeric(value), value, 0)
            Case "server-memory-min"
                ServerMemoryMin = IIf(IsNumeric(value), value, 0)
            Case "spigot-build-version"
                Server2ndVersion = value
        End Select
    End Sub
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {"server-memory-max=" & ServerMemoryMax,
                                                  "server-memory-min=" & ServerMemoryMin,
                                                  "spigot-build-version=" & Server2ndVersion}
    End Function
    Public Overrides Function DownloadAndInstallServer(targetVersion As String) As ServerDownloadTask
        Dim URL = "https://hub.spigotmc.org/jenkins/job/BuildTools/lastSuccessfulBuild/artifact/target/BuildTools.jar"
        Dim task As New ServerDownloadTask
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnServerDownloading(percent * 0.5)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnServerDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(New Net.WebClient().DownloadString("https://hub.spigotmc.org/versions/" & targetVersion & ".json"))
                                               ServerVersion = targetVersion
                                               Server2ndVersion = jsonObject.GetValue("name")
                                               Call OnServerDownloading(50)
                                               'Dim watcher As New SpigotGitBuildWindow()
                                               'If IsUnixLikeSystem Then
                                               'Shell("git config --global --unset core.autocrlf", AppWinStyle.MinimizedNoFocus, True, 5000)
                                               'watcher.Run(GetJavaPath(), "-jar BuildTools.jar --rev " & targetVersion & """", IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator))
                                               ' Else
                                               ' watcher.Run(GitBashPath, "--login -i -c """ & GetJavaPath() & " -jar BuildTools.jar --rev " & targetVersion & """", IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator))
                                               ' End If
                                               GenerateServerEULA()
                                               Call OnServerDownloadEnd(False)
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnServerDownloadStart()
                                         End Sub
        task.Download(URL, IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "BuildTools.jar"))
        Return task
    End Function
    Public Overrides Function CanUpdate() As Boolean
        Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(New Net.WebClient().DownloadString("https://hub.spigotmc.org/versions/" & ServerVersion & ".json"))
        Return Server2ndVersion < jsonObject.GetValue("name").ToString
    End Function
    Public Overrides Function GetInternalName() As String
        Return "Spigot_Git"
    End Function
    Public Overrides Function UpdateServer() As ServerDownloadTask
        Return DownloadAndInstallServer(ServerVersion)
    End Function
End Class
