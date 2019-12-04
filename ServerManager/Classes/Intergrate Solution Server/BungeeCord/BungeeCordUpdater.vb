Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class BungeeCordUpdater
    Const BungeeCordApiJson = "https://ci.md-5.net/job/BungeeCord/lastStableBuild/api/json"
    Const WaterfallApiJson = "https://papermc.io/api/v1/waterfall"
    Const TravertineApiJson = "https://papermc.io/api/v1/travertine"
    Const BungeeCordDownloadLink = "https://ci.md-5.net/job/BungeeCord/lastStableBuild/artifact/bootstrap/target/BungeeCord.jar"
    Const WaterfallDownloadLink = "https://papermc.io/api/v1/waterfall/{0}/{1}/download"
    Const TravertineDownloadLink = "https://papermc.io/api/v1/travertine/{0}/{1}/download"
    Shared Function CheckForUpdate(currentVersion As Integer, type As BungeeCordType) As Boolean
        Try
            Try
                Dim client As New Net.WebClient()
                Select Case type
                    Case BungeeCordType.BungeeCord
                        Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(BungeeCordApiJson))
                        Dim latestVersion As Integer = jsonObject.GetValue("id")
                        Return latestVersion > currentVersion
                    Case BungeeCordType.Waterfall
                        Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(WaterfallApiJson & "/" & JsonConvert.DeserializeObject(client.DownloadString(WaterfallApiJson)).GetValue("versions").First.ToString()))
                        Dim latestVersion As Integer = CType(jsonObject.GetValue("builds"), JObject).GetValue("latest")
                        Return latestVersion > currentVersion
                    Case BungeeCordType.Travertine
                        Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(TravertineApiJson & "/" & JsonConvert.DeserializeObject(client.DownloadString(TravertineApiJson)).GetValue("versions").First.ToString()))
                        Dim latestVersion As Integer = CType(jsonObject.GetValue("builds"), JObject).GetValue("latest")
                        Return latestVersion > currentVersion
                    Case Else
                        Return False
                End Select
            Catch ex As Net.WebException
                Return False
            End Try
        Catch ex As Exception
            Return False
        End Try
    End Function
    Shared Function GetLatestVersionNumber(type As BungeeCordType) As Integer
        Dim client As New Net.WebClient()
        Select Case type
            Case BungeeCordType.BungeeCord
                Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(BungeeCordApiJson))
                Dim latestVersion As Integer = jsonObject.GetValue("id")
                Return latestVersion
            Case BungeeCordType.Waterfall
                Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(WaterfallApiJson & "/" & JsonConvert.DeserializeObject(client.DownloadString(WaterfallApiJson)).GetValue("versions").First.ToString()))
                Dim latestVersion As Integer = CType(jsonObject.GetValue("builds"), JObject).GetValue("latest")
                Return latestVersion
            Case BungeeCordType.Travertine
                Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(TravertineApiJson & "/" & JsonConvert.DeserializeObject(client.DownloadString(TravertineApiJson)).GetValue("versions").First.ToString()))
                Dim latestVersion As Integer = CType(jsonObject.GetValue("builds"), JObject).GetValue("latest")
                Return latestVersion
            Case Else
                Return 0
        End Select
    End Function
    Shared Function DownloadUpdateAsync(host As BungeeCordHost, type As BungeeCordType) As Net.WebClient
        Try
            Dim client As New Net.WebClient()
            Select Case type
                Case BungeeCordType.BungeeCord
                    client.DownloadFileAsync(New Uri(BungeeCordDownloadLink), IO.Path.Combine(host.BungeePath, "BungeeCord.jar"))
                Case BungeeCordType.Waterfall
                    Dim t As New System.Threading.Thread(Sub()
                                                             Dim mcID As String = JsonConvert.DeserializeObject(client.DownloadString(WaterfallApiJson)).GetValue("versions").First.ToString()
                                                             Dim ID As String = CType(JsonConvert.DeserializeObject(client.DownloadString(WaterfallApiJson & "/" & mcID)).GetValue("builds"), JObject).GetValue("latest")
                                                             client.DownloadFileAsync(New Uri(String.Format(WaterfallDownloadLink, mcID, ID)), IO.Path.Combine(host.BungeePath, "Waterfall.jar"))
                                                         End Sub)
                    t.IsBackground = True
                    t.Name = "Waterfall Download Thread"
                    t.Start()
                Case BungeeCordType.Travertine
                    Dim t As New System.Threading.Thread(Sub()
                                                             Dim mcID As String = JsonConvert.DeserializeObject(client.DownloadString(TravertineApiJson)).GetValue("versions").First.ToString()
                                                             Dim ID As String = CType(JsonConvert.DeserializeObject(client.DownloadString(TravertineApiJson & "/" & mcID)).GetValue("builds"), JObject).GetValue("latest")
                                                             client.DownloadFileAsync(New Uri(String.Format(TravertineDownloadLink, mcID, ID)), IO.Path.Combine(host.BungeePath, "Travertine.jar"))
                                                         End Sub)
                    t.IsBackground = True
                    t.Name = "Travertine Download Thread"
                    t.Start()
            End Select
            Return client
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
