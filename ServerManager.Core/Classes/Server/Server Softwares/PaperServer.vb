Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class PaperServer
    Inherits SpigotServer
    Protected paperOptions As PaperOptions
    Private Shadows Property Server2ndVersion As String
    Private Shared PaperVersionDict As New Dictionary(Of Version, String)
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(path As String)
        MyBase.New(path)
    End Sub
    Friend Shared Shadows Sub GetVersionList()
        PaperVersionDict.Clear()
        Dim manifestListURL As String = "https://papermc.io/api/v1/paper"
        Try
            Dim client As New Net.WebClient()
            client.Encoding = System.Text.Encoding.UTF8
            Dim docHtml = client.DownloadString(manifestListURL)
            Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(docHtml)
            Dim versions As JArray = jsonObject.GetValue("versions")
            For Each version In versions
                Dim mcVersion As Version = Nothing
                If System.Version.TryParse(version, mcVersion) Then
                    PaperVersionDict.Add(mcVersion, "https://papermc.io/api/v1/paper/" & version.ToString)
                End If
            Next
            docHtml = Nothing
            client.Dispose()
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Protected Friend Overrides Sub GetOptions()
        MyBase.GetOptions()
        paperOptions = New PaperOptions(IO.Path.Combine(ServerPath, "paper.yml"))
    End Sub
    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {bukkitOptions, spigotOptions, paperOptions}
    End Function
    Public Overrides Function GetInternalName() As String
        Return "Paper"
    End Function
    Public Overrides Function CanUpdate() As Boolean
        Return False
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "paper-" & ServerVersion & ".jar"
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return ServerVersion
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "server-memory-max"
                ServerMemoryMax = IIf(Integer.TryParse(value, Nothing), value, 0)
            Case "server-memory-min"
                ServerMemoryMin = IIf(Integer.TryParse(value, Nothing), value, 0)
            Case "paper-build-version"
                Server2ndVersion = value
        End Select
    End Sub
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {"server-memory-max=" & ServerMemoryMax,
                                                  "server-memory-min=" & ServerMemoryMin,
                                                  "paper-build-version=" & Server2ndVersion}
    End Function
    Public Overrides Function DownloadAndInstallServer(targetVersion As String) As ServerDownloadTask
        Dim seperator As String = IIf(IsUnixLikeSystem, "/", "\")
        Dim subClient As New Net.WebClient
        Dim subDocHtml = subClient.DownloadString(PaperVersionDict(Version.Parse(targetVersion)))
        Dim subJsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(subDocHtml)
        Dim subJsonToken As JToken = CType(subJsonObject.GetValue("builds"), JObject).GetValue("latest")
        ServerVersion = targetVersion
        Server2ndVersion = subJsonToken
        Dim targetURL As String = String.Format("https://papermc.io/api/v1/paper/{0}/{1}/download", ServerVersion, subJsonToken)
        Dim DownloadPath As String = IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "paper-" & ServerVersion & ".jar")
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
        task.Download(targetURL, DownloadPath)
        Return task
    End Function
    Public Overrides Function GetAvailableVersions() As String()
        Dim keys = PaperVersionDict.Keys.ToList
        keys.Sort()
        keys.Reverse()
        Dim result As New List(Of String)
        For Each version In keys
            result.Add(version.ToString)
        Next
        keys = Nothing
        Return result.ToArray
    End Function
End Class
