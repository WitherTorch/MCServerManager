Imports System.IO.Compression
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class CauldronServer
    Inherits SpigotServer
    Implements IForge
    Protected modLists As New List(Of ServerAddons)
    Protected cauldronOptions As CauldronOptions
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
    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Select Case ServerVersion
            Case "1.5.2"
                Return {bukkitOptions, cauldronOptions}
            Case "1.6.4"
                Return {bukkitOptions, spigotOptions, cauldronOptions}
            Case "1.7.2"
                Return {bukkitOptions, spigotOptions, cauldronOptions}
            Case "1.7.10"
                Return {bukkitOptions, cauldronOptions}
            Case Else
                Return {}
        End Select
    End Function
    Protected Friend Overrides Sub GetOptions()
        bukkitOptions = New BukkitOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
        Select Case ServerVersion
            Case "1.5.2"
                cauldronOptions = New CauldronOptions(IO.Path.Combine(ServerPath, "mcpc.yml"))
            Case "1.6.4"
                spigotOptions = New SpigotOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
                cauldronOptions = New CauldronOptions(IO.Path.Combine(ServerPath, "mcpc.yml"))
            Case "1.7.2"
                spigotOptions = New SpigotOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
                cauldronOptions = New CauldronOptions(IO.Path.Combine(ServerPath, "cauldron.yml"))
            Case "1.7.10"
                cauldronOptions = New CauldronOptions(IO.Path.Combine(ServerPath, "cauldron.yml"))
        End Select
    End Sub
    Friend Shared Shadows Sub GetVersionList()
    End Sub
    Public Overrides Function GetAvaillableVersions() As String()
        Return {"1.5.2", "1.6.4", "1.7.2", "1.7.10"}
    End Function
    Public Overrides Function GetAvaillableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvaillableVersions()
    End Function
    Public Overrides Function GetInternalName() As String
        Return "Cauldron"
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "server.jar"
    End Function
    Public Overrides Function DownloadAndInstallServer(targetVersion As String) As ServerDownloadTask
        Dim task As New ServerDownloadTask
        Select Case ServerVersion
            Case "1.7.10"
                task.Download("https://www.dropbox.com/s/flfkgznkmagikrd/server-1.7.10.zip?raw=1", IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "server-" & targetVersion & ".zip"))
            Case "1.7.2"
                task.Download("https://www.dropbox.com/s/w730znj2y1lskt3/server-1.7.2.zip?raw=1", IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "server-" & targetVersion & ".zip"))
            Case "1.6.4"
                task.Download("https://www.dropbox.com/s/ey01pirj7fw0fk6/server-1.6.4.zip?raw=1", IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "server-" & targetVersion & ".zip"))
            Case "1.5.2"
                task.Download("https://www.dropbox.com/s/jediu69o1sgmmcg/server-1.5.2.zip?raw=1", IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "server-" & targetVersion & ".zip"))
        End Select
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnServerDownloading(percent * 0.8)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnServerDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               UnZipPackage(IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "server-" & targetVersion & ".zip"))
                                               ServerVersion = targetVersion
                                               Call OnServerDownloadEnd(False)
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnServerDownloadStart()
                                         End Sub
        Return task
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
