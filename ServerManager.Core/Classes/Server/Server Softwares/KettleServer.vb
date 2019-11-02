Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class KettleServer
    Inherits ThermosServer
    Private Shared KettleVersionDict As New Dictionary(Of String, (String, String, String))
    Dim Server2ndVersion As String
    Public Overrides Function GetInternalName() As String
        Return "Kettle"
    End Function
    Friend Shared Shadows Sub GetVersionList()
        KettleVersionDict.Clear()
        Dim manifestListURL As String = "https://api.github.com/repos/KettleFoundation/Kettle/releases"
        Try
            Dim client As New Net.WebClient()
            client.Encoding = System.Text.Encoding.UTF8
            client.Headers.Add(Net.HttpRequestHeader.UserAgent, "Minecraft-Server-Manager")
            Dim docHtml = client.DownloadString(manifestListURL)
            Dim jsonArray As JArray = JsonConvert.DeserializeObject(Of JArray)(docHtml)
            For Each jsonObject As JObject In jsonArray
                Try
                    Dim name As String = ""
                    jsonObject.TryGetValue("name", name)
                    If name = "" Then Continue For
                    name = name.Replace("Kettle ", "")
                    Dim assets As JArray = Nothing
                    jsonObject.TryGetValue("assets", assets)
                    If assets IsNot Nothing AndAlso assets.Count > 1 Then
                        For Each subJsonObject As JObject In assets
                            Dim regex As New Regex("kettle-git-HEAD-[0-9a-f]{7}-universal.jar", RegexOptions.IgnoreCase)
                            If regex.IsMatch(subJsonObject.GetValue("name")) AndAlso
                                                                    regex.Match(subJsonObject.GetValue("name")).Value = subJsonObject.GetValue("name") Then
                                Dim url As String = subJsonObject.GetValue("browser_download_url")
                                If assets.Count = 2 Then
                                    KettleVersionDict.Add(name, (url, subJsonObject.GetValue("name"), Nothing))
                                Else
                                    For i As Integer = 2 To assets.Count - 1
                                        If CType(assets(i), JObject).GetValue("name") = "libraries.zip" Then
                                            KettleVersionDict.Add(name, (url, subJsonObject.GetValue("name"), CType(assets(i), JObject).GetValue("browser_download_url")))
                                            Exit For
                                        End If
                                    Next
                                    If KettleVersionDict.ContainsKey(name) = False Then KettleVersionDict.Add(name, (url, subJsonObject.GetValue("name"), Nothing))
                                End If
                            End If
                        Next
                    End If
                Catch ex As Exception

                End Try
            Next
            docHtml = Nothing
            client.Dispose()
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Public Overrides Function DownloadAndInstallServer(targetVersion As String) As ServerDownloadTask
        Dim task As New ServerDownloadTask
        Dim pharse As Integer = 0
        AddHandler task.DownloadProgressChanged, Sub(args)
                                                     Try
                                                         OnServerDownloading(args * 0.2)
                                                     Catch ex As Exception
                                                     End Try
                                                 End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               Dim vanillaClient As New Net.WebClient()
                                               AddHandler vanillaClient.DownloadProgressChanged, Sub(obj, args)
                                                                                                     Try
                                                                                                         If args.ProgressPercentage = -1 Then
                                                                                                             OnServerDownloading(-1)
                                                                                                         Else
                                                                                                             OnServerDownloading(20 + args.ProgressPercentage * 0.2)
                                                                                                         End If
                                                                                                     Catch ex As Exception
                                                                                                     End Try
                                                                                                 End Sub
                                               AddHandler vanillaClient.DownloadFileCompleted, Sub()
                                                                                                   vanillaClient.Dispose()
                                                                                                   OnServerDownloading(40)
                                                                                                   For i As Integer = KettleVersionDict.Keys.ToList.IndexOf(targetVersion) To KettleVersionDict.Count - 1
                                                                                                       If String.IsNullOrEmpty(KettleVersionDict.Values.ToArray(i).Item3) = False Then
                                                                                                           Dim subClient As New Net.WebClient
                                                                                                           AddHandler subClient.DownloadProgressChanged, Sub(obj, args)
                                                                                                                                                             If args.ProgressPercentage = -1 Then
                                                                                                                                                                 OnServerDownloading(-1)
                                                                                                                                                             Else
                                                                                                                                                                 OnServerDownloading(40 + args.ProgressPercentage * 0.3)
                                                                                                                                                             End If
                                                                                                                                                         End Sub
                                                                                                           AddHandler subClient.DownloadFileCompleted, Sub()
                                                                                                                                                           subClient.Dispose()
                                                                                                                                                           OnServerDownloading(70)
                                                                                                                                                           If IO.File.Exists(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator) & "libraries.zip") Then
                                                                                                                                                               UnZipPackage(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator) & "libraries.zip")
                                                                                                                                                           End If
                                                                                                                                                           GenerateServerEULA()
                                                                                                                                                           OnServerDownloadEnd(False)
                                                                                                                                                       End Sub
                                                                                                           subClient.DownloadFileAsync(New Uri(KettleVersionDict.Values.ToArray(i).Item3), IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator) & "libraries.zip")
                                                                                                           Exit For
                                                                                                       End If
                                                                                                   Next
                                                                                               End Sub
                                               Dim jsonClient As New Net.WebClient()
                                               Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(jsonClient.DownloadString(VanillaVersionDict("1.12.2")))
                                               If vanilla_isPre OrElse vanilla_isSnap Then
                                                   Dim assets As String = jsonObject.GetValue("assets").ToString
                                                   assets = New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*").Match(assets).Value
                                                   ServerVersion = assets
                                               End If
                                               vanillaClient.DownloadFileAsync(New Uri(jsonObject.GetValue("downloads").Item("server").Item("url").ToString), IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "minecraft_server.1.12.2.jar"))
                                           End Sub
        Dim branchID As String = New Regex("[0-9a-f]{7}", RegexOptions.IgnoreCase).Match(KettleVersionDict(targetVersion).Item2).Value
        ServerVersion = targetVersion
        Server2ndVersion = branchID
        task.Download(KettleVersionDict(targetVersion).Item1, IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), KettleVersionDict(targetVersion).Item2))
        Return task
    End Function
    Public Overrides Function GetAvailableVersions() As String()
        Return KettleVersionDict.Keys.ToArray
    End Function
    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvailableVersions()
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "server-memory-max"
                ServerMemoryMax = IIf(Integer.TryParse(value, Nothing), value, 0)
            Case "server-memory-min"
                ServerMemoryMin = IIf(Integer.TryParse(value, Nothing), value, 0)
            Case "kettle-branch-id"
                Server2ndVersion = value
        End Select
    End Sub
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {"server-memory-max=" & ServerMemoryMax,
                                                  "server-memory-min=" & ServerMemoryMin,
                                                  "kettle-branch-id=" & Server2ndVersion}
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "kettle-git-HEAD-" & Server2ndVersion & "-universal.jar"
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return ServerVersion
    End Function
    Public Overrides Function CanUpdate() As Boolean
        Return False
    End Function
End Class
