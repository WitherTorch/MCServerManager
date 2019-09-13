Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class ForgeServer
    Inherits VanillaServer
    Implements IForge
    Protected modLists As New List(Of ServerAddons)
    Friend Shadows Property Server2ndVersion As String
    Private Shared ForgeVersionDict As New Dictionary(Of Version, Version)
    Public Sub LoadMods() Implements IForge.LoadMods
        modLists.Clear()
        Dim modPath = IO.Path.Combine(ServerPath, "mods")
        Dim paths As New List(Of String)
        If IO.Directory.Exists(modPath) Then
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(modPath, "modList.json")) Then
                Dim reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(modPath, "modList.json"), IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read, 4096, True))
                Dim jsonArray As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(reader.ReadToEnd())
                If jsonArray IsNot Nothing Then
                    For Each jsonObject As JObject In jsonArray
                        Try
                            If IO.File.Exists(jsonObject.GetValue("Path").ToString) = False Then
                                If jsonObject.ContainsKey("LastAccessedDate") = False Or IO.File.GetLastWriteTime(jsonObject.GetValue("Path").ToString) <> jsonObject.GetValue("LastAccessedDate") Then
                                    Dim item As New ServerAddons(jsonObject.GetValue("Name").ToString, jsonObject.GetValue("Path").ToString, jsonObject.GetValue("Version"), jsonObject.GetValue("VersionDate"), IO.File.GetLastWriteTime(jsonObject.GetValue("Path").ToString))
                                    Using unpatcher As New ForgeModUnpatcher(item.Path)
                                        Dim info = unpatcher.GetModInfo()
                                        If info.IsNull = False Then
                                            item.Name = info.Name
                                            item.Version = info.Version
                                            modLists.Add(item)
                                        End If
                                    End Using
                                    paths.Add(jsonObject.GetValue("Path").ToString)
                                Else
                                    Dim item As New ServerAddons(jsonObject.GetValue("Name").ToString, jsonObject.GetValue("Path").ToString, jsonObject.GetValue("Version"), jsonObject.GetValue("VersionDate"), jsonObject.GetValue("LastAccessedDate"))
                                    paths.Add(jsonObject.GetValue("Path").ToString)
                                    modLists.Add(item)
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                End If
            End If
            Dim modPathInfo As New IO.DirectoryInfo(modPath)
            For Each modFileInfo In modPathInfo.GetFiles("*.jar", IO.SearchOption.AllDirectories)
                Try
                    Dim item As New ServerAddons(modFileInfo.Name, modFileInfo.FullName, "", modFileInfo.CreationTime, modFileInfo.LastWriteTime)
                    If paths.Contains(item.Path) = False Then
                        Using unpatcher As New ForgeModUnpatcher(item.Path)
                            Dim info = unpatcher.GetModInfo()
                            If info.IsNull = False Then
                                item.Name = info.Name
                                item.Version = info.Version
                                modLists.Add(item)
                            End If
                        End Using
                    End If
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub
    Public Sub SaveMods() Implements IForge.SaveMods
        Dim modPath = IO.Path.Combine(ServerPath, "mods")
        If IO.Directory.Exists(modPath) = False Then
            IO.Directory.CreateDirectory(modPath)
        End If
        If My.Computer.FileSystem.FileExists(IO.Path.Combine(modPath, "modList.json")) = False Then
            IO.File.Create(IO.Path.Combine(modPath, "modList.json"))
        End If
        Try
            Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(modPath, "modList.json"), IO.FileMode.Truncate, IO.FileAccess.Write, IO.FileShare.Read, 4096, True))
            Dim jsonArray As New Newtonsoft.Json.Linq.JArray()
            For Each forgeMod In modLists
                Dim jsonObject As New Newtonsoft.Json.Linq.JObject()
                jsonObject.Add("Name", New Newtonsoft.Json.Linq.JValue(forgeMod.Name))
                jsonObject.Add("VersionDate", New Newtonsoft.Json.Linq.JValue(forgeMod.VersionDate))
                jsonObject.Add("Path", New Newtonsoft.Json.Linq.JValue(forgeMod.Path))
                jsonArray.Add(jsonObject)
            Next
            writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(jsonArray))
            writer.Flush()
            writer.Close()
        Catch ex As IO.IOException
        End Try
    End Sub
    Public Function GetMods() As ServerAddons() Implements IForge.GetMods
        Return modLists.ToArray()
    End Function
    Public Overrides Function CanUpdate() As Boolean
        If ForgeVersionDict.ContainsKey(New Version(ServerVersion)) Then
            Return New Version(Server2ndVersion) < ForgeVersionDict(New Version(ServerVersion))
        Else
            Return False
        End If
    End Function
    Public Overrides Function DownloadAndInstallServer(targetServerVersion As String) As ServerDownloadTask
        Dim installer As New ForgeInstaller(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator))
        Dim craftVersion = targetServerVersion
        Dim v As New Version(craftVersion)
        Dim forgeVersion As String = ForgeVersionDict(New Version(craftVersion)).ToString
        Dim task = installer.DownloadForge(craftVersion, forgeVersion)
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnServerDownloading(percent * 0.5)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnServerDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               Call OnServerDownloading(50)
                                               If installer.InstallForge(craftVersion, forgeVersion) = DialogResult.OK Then
                                                   ServerVersion = craftVersion
                                                   Server2ndVersion = forgeVersion
                                                   Call OnServerDownloadEnd(False)
                                               Else
                                                   Call OnServerDownloadEnd(True)
                                               End If
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnServerDownloadStart()
                                         End Sub
        Return task
    End Function
    Public Overrides Function UpdateServer() As ServerDownloadTask
        Return DownloadAndInstallServer(ServerVersion)
    End Function
    Public Overrides Function GetAvaillableVersions() As String()
        Dim keys = ForgeVersionDict.Keys.ToList
        keys.Sort()
        keys.Reverse()
        Dim result As New List(Of String)
        For Each version In keys
            result.Add(version.ToString)
        Next
        Return result.ToArray()
    End Function
    Public Overrides Function GetAvaillableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvaillableVersions()
    End Function
    Public Overrides Function GetInternalName() As String
        Return "Forge"
    End Function
    Public Overrides Function GetServerFileName() As String
        ' 1.1~1.2 > Server
        ' 1.3 ~ > Universal
        ' 1.13+ -> Nothing
        ' 1.8.9 and 1.7.10 -> Server Version Twice
        If New Version(ServerVersion) >= New Version(1, 3) Then
            If New Version(ServerVersion) >= New Version(1, 13) Then
                Return "forge-" & ServerVersion & "-" & Server2ndVersion & ".jar"
            Else
                If ServerVersion = "1.8.9" OrElse ServerVersion = "1.7.10" Then
                    Return "forge-" & ServerVersion & "-" & Server2ndVersion & "-" & ServerVersion & "-universal" & ".jar"
                Else
                    Return "forge-" & ServerVersion & "-" & Server2ndVersion & "-universal" & ".jar"
                End If
            End If
        Else
            Return "forge-" & ServerVersion & "-" & Server2ndVersion & "-server" & ".jar"
        End If
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return ServerVersion & " (Forge " & Server2ndVersion & ")"
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "server-memory-max"
                ServerMemoryMax = IIf(IsNumeric(value), value, 0)
            Case "server-memory-min"
                ServerMemoryMin = IIf(IsNumeric(value), value, 0)
            Case "forge-build-version"
                Server2ndVersion = value
        End Select
    End Sub
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {"server-memory-max=" & ServerMemoryMax,
                                                  "server-memory-min=" & ServerMemoryMin,
                                                  "forge-build-version=" & Server2ndVersion}
    End Function
End Class
