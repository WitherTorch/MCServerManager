Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class AkarinServer
    Inherits PaperServer
    Protected akarinOptions As AkarinOptions
    Private Shadows Property Server2ndVersion As String
    Private Property Server3rdVersion As String
    Private Shared AkarinVersionList As New List(Of Version)
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(path As String)
        MyBase.New(path)
    End Sub
    Friend Shared Shadows Sub GetVersionList()
        AkarinVersionList.Clear()
        Dim manifestListURL As String = "https://api.github.com/repos/Akarin-project/Akarin/branches"
        Try
            Dim client As New Net.WebClient()
            client.Encoding = System.Text.Encoding.UTF8
            client.Headers.Add(Net.HttpRequestHeader.UserAgent, "Minecraft-Server-Manager")
            Dim docHtml = client.DownloadString(manifestListURL)
            Dim jsonArray As JArray = JsonConvert.DeserializeObject(Of JArray)(docHtml)
            For Each jsonObject As JObject In jsonArray
                Dim versionString As String = jsonObject.GetValue("name").ToString()
                If versionString.StartsWith("ver/") AndAlso Version.TryParse(versionString.Substring(4), New Version()) Then
                    AkarinVersionList.Add(Version.Parse(versionString.Substring(4)))
                ElseIf versionString = "master" Then
                    AkarinVersionList.Add(New Version(100, 100))
                End If
            Next
            AkarinVersionList.Sort()
            AkarinVersionList.Reverse()
            docHtml = Nothing
            client.Dispose()
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Protected Friend Overrides Sub GetOptions()
        MyBase.GetOptions()
        akarinOptions = New AkarinOptions(IO.Path.Combine(ServerPath, "akarin.yml"))
    End Sub
    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {bukkitOptions, spigotOptions, paperOptions, akarinOptions}
    End Function
    Public Overrides Function GetInternalName() As String
        Return "Akarin"
    End Function
    Public Overrides Function CanUpdate() As Boolean
        Dim webClient As New Net.WebClient
        webClient.Headers.Add(Net.HttpRequestHeader.Accept, "application/json")
        Dim downloadURL = "https://circleci.com/api/v1.1/project/github/Akarin-project/Akarin/tree/" & Server3rdVersion & "?filter=%22successful%22&limit=1"
        Dim docHtml = webClient.DownloadString(downloadURL)
        Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JArray)(docHtml)(0)
        Dim buildNum As Integer = jsonObject.GetValue("build_num")
        Return Server2ndVersion < buildNum
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "akarin-" & ServerVersion & ".jar"
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return ServerVersion
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "server-memory-max"
                MemoryMax = IIf(Of Integer)(Integer.TryParse(value, Nothing), value, 0)
            Case "server-memory-min"
                MemoryMin = IIf(Of Integer)(Integer.TryParse(value, Nothing), value, 0)
            Case "akarin-build-version"
                Server2ndVersion = value
            Case "akarin-branch-name"
                Server3rdVersion = value
        End Select
    End Sub
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {"server-memory-max=" & MemoryMax,
                                                  "server-memory-min=" & MemoryMin,
                                                  "akarin-build-version=" & Server2ndVersion,
                                                  "akarin-branch-name=" & Server3rdVersion}
    End Function
    Public Overrides Function DownloadAndInstallServer(targetVersion As String) As ServerDownloadTask
        Dim seperator As String = IIf(IsUnixLikeSystem, "/", "\")
        Dim subClient As New Net.WebClient
        subClient.Headers.Add(Net.HttpRequestHeader.Accept, "application/json")
        Dim downloadURL As String
        If targetVersion = "最新建置版本" OrElse targetVersion = "master" Then
            downloadURL = "https://circleci.com/api/v1.1/project/github/Akarin-project/Akarin/tree/master" & "?filter=%22successful%22&limit=1"
        Else
            downloadURL = "https://circleci.com/api/v1.1/project/github/Akarin-project/Akarin/tree/ver/" & targetVersion & "?filter=%22successful%22&limit=1"
            targetVersion = "ver/" & targetVersion
        End If
        Dim subDocHtml = subClient.DownloadString(downloadURL)
        Dim subJsonObject As JObject = JsonConvert.DeserializeObject(Of JArray)(subDocHtml)(0)
        Dim buildNum As Integer = subJsonObject.GetValue("build_num")
        subClient.Headers.Add(Net.HttpRequestHeader.Accept, "application/json")
        Dim anotherDocHTML = subClient.DownloadString("https://circleci.com/api/v1.1/project/github/Akarin-project/Akarin/" & buildNum & "/artifacts")
        Dim regex As New Regex("akarin-[0-9].[0-9]{1,2}.[0-9]{1,2}.jar")
        For Each anotherJSONObject As JObject In JsonConvert.DeserializeObject(Of JArray)(anotherDocHTML)
            Dim targetURL As String = anotherJSONObject.GetValue("url")
            If regex.IsMatch(targetURL) Then
                Dim matchString As String = regex.Match(targetURL).Value
                matchString = matchString.Remove(0, 7)
                matchString = matchString.Substring(0, matchString.Length - 4)
                If Version.TryParse(matchString, Nothing) Then
                    ServerVersion = matchString
                    Server2ndVersion = buildNum
                    Server3rdVersion = targetVersion
                    Dim task As New ServerDownloadTask
                    AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                                 Call OnServerDownloading(percent)
                                                             End Sub
                    AddHandler task.DownloadCanceled, Sub()
                                                          Call OnServerDownloadEnd(True)
                                                      End Sub
                    AddHandler task.DownloadCompleted, Sub()
                                                           ServerVersion = targetVersion
                                                           GenerateServerEULA()
                                                           Call OnServerDownloadEnd(False)
                                                       End Sub
                    AddHandler task.DownloadStarted, Sub()
                                                         Call OnServerDownloadStart()
                                                     End Sub
                    Dim DownloadPath As String = IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "akarin-" & matchString & ".jar")
                    task.Download(targetURL, DownloadPath)
                    Return task
                End If
            End If
        Next
        Return Nothing
    End Function
    Public Overrides Function GetAvailableVersions() As String()
        Dim result As New List(Of String)
        For Each item In AkarinVersionList
            Dim buildVer As String = item.Build.ToString
            If buildVer = "-1" Then
                buildVer = "x"
            End If
            If item.Major = 100 And item.Minor = 100 Then
                result.Add("最新建置版本")
            Else
                result.Add(String.Format("{0}.{1}.{2}", item.Major, item.Minor, buildVer))
            End If
        Next
        Return result.ToArray
    End Function
    Public Overrides Function UpdateServer() As ServerDownloadTask
        Return DownloadAndInstallServer(ServerVersion)
    End Function
End Class
