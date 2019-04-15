Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class BungeeCordUpdater
    Const BungeeCordApiJson = "https://ci.md-5.net/job/BungeeCord/lastStableBuild/api/json"
    Const BungeeCordDownloadLink = "https://ci.md-5.net/job/BungeeCord/lastStableBuild/artifact/bootstrap/target/BungeeCord.jar"
    Shared Function CheckForUpdate(currentVersion As Integer) As Boolean
        Try
            Try
                Dim client As New Net.WebClient()
                Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(BungeeCordApiJson))
                Dim latestVersion As Integer = jsonObject.GetValue("id")
                Return latestVersion > currentVersion
            Catch ex As Net.WebException
                Return False
            End Try
        Catch ex As Exception
            Return False
        End Try
    End Function
    Shared Function GetLatestVersionNumber() As Integer
        Dim client As New Net.WebClient()
        Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(BungeeCordApiJson))
        Dim latestVersion As Integer = jsonObject.GetValue("id")
        Return latestVersion
    End Function
    Shared Function DownloadUpdateAsync(host As BungeeCordHost) As Net.WebClient
        Try
            Dim client As New Net.WebClient()
            client.DownloadFileAsync(New Uri(BungeeCordDownloadLink), IO.Path.Combine(host.BungeePath, "BungeeCord.jar"))
            Return client
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
