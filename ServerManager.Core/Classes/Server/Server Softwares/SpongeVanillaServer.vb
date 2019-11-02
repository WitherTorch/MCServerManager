Imports System.Text.RegularExpressions
Imports System.Xml
Imports ServerManager

Public Class SpongeVanillaServer
    Inherits VanillaServer
    Private Shadows Property Server2ndVersion As String
    Private Shadows Property SpongeVersionType As String
    Private Property Server3rdVersion As String
    Private Shared SpongeVanillaVersionList As New Dictionary(Of String, SpongeVersion)
    Friend Shared Shadows Sub GetVersionList()
        SpongeVanillaVersionList.Clear()
        Dim manifestListURL As String = "https://repo.spongepowered.org/maven/org/spongepowered/spongevanilla/maven-metadata.xml"
        Try
            Dim client As New Net.WebClient()
            client.Encoding = System.Text.Encoding.UTF8
            Dim docHtml = client.DownloadString(manifestListURL)
            Dim xmlDocument = New XmlDocument()
            xmlDocument.Load(New IO.StringReader(docHtml))
            Dim versionNodes = xmlDocument.SelectNodes("//metadata/versioning/versions/*")
            Dim versionList As New Dictionary(Of String, List(Of SpongeVersion))
            For Each versionNode As XmlNode In versionNodes
                Dim version As String = versionNode.InnerText
                Dim versionRegex1 As New Regex("[0-9.]{1,}-[0-9.]{1,}-(DEV|BETA)-[0-9]{1,4}") 'Dev/Beta Detect
                Dim versionRegex2 As New Regex("[0-9.]{1,}-[0-9.]{1,}-RC[0-9]{1,4}") 'RC Detect
                Dim versionRegex3 As New Regex("[0-9.]{1,}-[0-9.]{1,}") 'Release Detect
                Dim versionRegexIgnore As New Regex("[0-9.]{1,}-[0-9.]{1,}(DEV|BETA)-[0-9]{1,4}") 'Old Version(Official Doesn't Supported)
                If (versionRegex1.IsMatch(version) _
                                                                   OrElse versionRegex2.IsMatch(version) _
                                                                   OrElse versionRegex3.IsMatch(version)) _
                                                                   And versionRegexIgnore.IsMatch(version) = False Then
                    If versionRegex1.IsMatch(version) Then
                        Dim match = versionRegex1.Match(version).Value
                        Dim versionData = match.Split("-")
                        Dim ChildVersionList As List(Of SpongeVersion)
                        If versionList.ContainsKey(versionData(0)) Then
                            ChildVersionList = versionList(versionData(0))
                        Else
                            ChildVersionList = New List(Of SpongeVersion)
                        End If
                        Select Case versionData(2)
                            Case "DEV"
                                ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(1), ServerManager.SpongeVersionType.Dev, versionData(3)))
                            Case "BETA"
                                ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(1), ServerManager.SpongeVersionType.Beta, versionData(3)))
                        End Select
                        versionList(versionData(0)) = ChildVersionList
                    ElseIf versionRegex2.IsMatch(version) Then
                        Dim match = versionRegex2.Match(version).Value
                        Dim versionData = match.Split("-")
                        Dim ChildVersionList As List(Of SpongeVersion)
                        If versionList.ContainsKey(versionData(0)) Then
                            ChildVersionList = versionList(versionData(0))
                        Else
                            ChildVersionList = New List(Of SpongeVersion)
                        End If
                        ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(1), ServerManager.SpongeVersionType.RC, versionData(2).Substring(2)))
                        versionList(versionData(0)) = ChildVersionList
                    ElseIf versionRegex3.IsMatch(version) Then
                        Dim match = versionRegex3.Match(version).Value
                        Dim versionData = match.Split("-")
                        Dim ChildVersionList As List(Of SpongeVersion)
                        If versionList.ContainsKey(versionData(0)) Then
                            ChildVersionList = versionList(versionData(0))
                        Else
                            ChildVersionList = New List(Of SpongeVersion)
                        End If
                        ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(1), ServerManager.SpongeVersionType.Release))
                        versionList(versionData(0)) = ChildVersionList
                    End If
                End If
            Next
            For Each keyValuePair In versionList
                If SpongeVanillaVersionList.ContainsKey(keyValuePair.Key) = False Then SpongeVanillaVersionList.Add(keyValuePair.Key, keyValuePair.Value.Max)
            Next
            docHtml = Nothing
            client.Dispose()
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Public Overrides Function GetInternalName() As String
        Return "SpongeVanilla"
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "spongeVanilla-" & ServerVersion & ".jar"
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return ServerVersion & " (Sponge " & Server2ndVersion & " " & [Enum].Parse(GetType(SpongeVersionType), SpongeVersionType).ToString & " " & Server3rdVersion & ")"
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "server-memory-max"
                ServerMemoryMax = IIf(Integer.TryParse(value, Nothing), value, 0)
            Case "server-memory-min"
                ServerMemoryMin = IIf(Integer.TryParse(value, Nothing), value, 0)
            Case "spongeVanilla-version"
                Server2ndVersion = value
            Case "spongeVanilla-type"
                SpongeVersionType = value
            Case "spongeVanilla-build-version"
                Server3rdVersion = value
        End Select
    End Sub
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {"server-memory-max=" & ServerMemoryMax,
                                                  "server-memory-min=" & ServerMemoryMin,
                                                  "spongeVanilla-version=" & Server2ndVersion,
                                                  "spongeVanilla-type=" & SpongeVersionType,
                                                  "spongeVanilla-build-version=" & Server3rdVersion}
    End Function
    Public Overrides Function DownloadAndInstallServer(targetServerVersion As String) As ServerDownloadTask
        Dim task As New ServerDownloadTask
        Dim spongeVersion As SpongeVersion = SpongeVanillaVersionList(targetServerVersion)
        task.Download(spongeVersion.GetDownloadUrl, IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "spongeVanilla-" & targetServerVersion & ".jar"))
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnServerDownloading(percent)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnServerDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               ServerVersion = spongeVersion.MinecraftVersion.ToString
                                               Server2ndVersion = spongeVersion.SpongeVersion.ToString
                                               SpongeVersionType = spongeVersion.SpongeVersionType.ToString
                                               spongeVersion = Nothing
                                               GenerateServerEULA()
                                               Call OnServerDownloadEnd(False)
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnServerDownloadStart()
                                         End Sub
        Return task
    End Function
    Public Overrides Function GetAvailableVersions() As String()
        Dim result As List(Of String) = SpongeVanillaVersionList.Keys.ToList()
        result.Reverse()
        Return result.ToArray()
    End Function
    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvailableVersions()
    End Function
    Public Overrides Function RunServer() As Process
        If ProcessID = 0 Then
            Dim processInfo As New ProcessStartInfo(GetJavaPath(),
                                                String.Format("-Xms{0}M -Xmx{1}M {2} -jar ""{3}""",
                                                              IIf(ServerMemoryMin > 0, ServerMemoryMin, GlobalModule.ServerMemoryMin),
                                                              IIf(ServerMemoryMax > 0, ServerMemoryMin, GlobalModule.ServerMemoryMax),
                                                               JavaArguments, ServerPath.TrimEnd(seperator) & seperator & GetServerFileName()))
            processInfo.UseShellExecute = False
            processInfo.CreateNoWindow = True
            processInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
            processInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
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
    Public Overrides Function UpdateServer() As ServerDownloadTask
        Return DownloadAndInstallServer(ServerVersion)
    End Function
    Public Overrides Function CanUpdate() As Boolean
        If SpongeVanillaVersionList.ContainsKey(ServerVersion) Then
            Return New SpongeVersion("", ServerVersion, Server2ndVersion, [Enum].Parse(GetType(SpongeVersionType), SpongeVersionType), Server3rdVersion) < SpongeVanillaVersionList(ServerVersion)
        Else
            Return False
        End If
    End Function
End Class
Friend Enum SpongeVersionType
    None
    Dev
    Beta
    RC
    Release
End Enum
Friend Class SpongeVersion
    Implements IComparable(Of SpongeVersion)

    Public Property OriginalString As String
    Public Property MinecraftVersion As Version
    Public Property ForgeBranch As Integer
    Public Property SpongeVersion As Version
    Public Property SpongeVersionType As SpongeVersionType
    Public Property isSpongeForge As Boolean = False
    Public Property Build As Integer
    Sub New(originalString As String,
                minecraftVersion As String,
                spongeVersion As String,
                versionType As SpongeVersionType,
                Optional spongeBuildVersion As Integer = 0,
                Optional forgeBranch As Integer = 0,
                Optional isSpongeForge As Boolean = False)

        Me.MinecraftVersion = New Version(minecraftVersion)
        Me.SpongeVersion = New Version(spongeVersion)
        SpongeVersionType = versionType
        Build = spongeBuildVersion
        _OriginalString = originalString
        Me.ForgeBranch = forgeBranch
        Me.isSpongeForge = isSpongeForge
    End Sub
    Shared Operator =(left As SpongeVersion, right As SpongeVersion)
        Return (left.MinecraftVersion = right.MinecraftVersion) _
            And (left.SpongeVersion = right.SpongeVersion) _
            And (left.SpongeVersionType = right.SpongeVersionType) _
            And (left.Build = right.Build)
    End Operator
    Shared Operator <>(left As SpongeVersion, right As SpongeVersion)
        Return Not ((left.MinecraftVersion = right.MinecraftVersion) _
            And (left.SpongeVersion = right.SpongeVersion) _
            And (left.SpongeVersionType = right.SpongeVersionType) _
            And (left.Build = right.Build))
    End Operator
    Shared Operator <(left As SpongeVersion, right As SpongeVersion)
        If left.MinecraftVersion > right.MinecraftVersion Then
            Return False
        ElseIf left.MinecraftVersion > right.MinecraftVersion Then
            If left.SpongeVersion > right.SpongeVersion Then
                Return False
            ElseIf left.SpongeVersion = right.SpongeVersion Then
                If left.SpongeVersionType > right.SpongeVersionType Then
                    Return False
                ElseIf left.SpongeVersionType = right.SpongeVersionType Then
                    If left.Build >= right.Build Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Operator
    Shared Operator >(left As SpongeVersion, right As SpongeVersion)
        If left.MinecraftVersion > right.MinecraftVersion Then
            Return True
        ElseIf left.MinecraftVersion > right.MinecraftVersion Then
            If left.SpongeVersion > right.SpongeVersion Then
                Return True
            ElseIf left.SpongeVersion = right.SpongeVersion Then
                If left.SpongeVersionType > right.SpongeVersionType Then
                    Return True
                ElseIf left.SpongeVersionType = right.SpongeVersionType Then
                    If left.Build > right.Build Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If

            Else
                Return False
            End If
        Else
            Return False
        End If
    End Operator
    Function GetDownloadUrl() As String
        If isSpongeForge Then
            Return String.Format("https://repo.spongepowered.org/maven/org/spongepowered/spongeforge/{0}/spongeforge-{0}.jar", _OriginalString)
        Else
            Return String.Format("https://repo.spongepowered.org/maven/org/spongepowered/spongevanilla/{0}/spongevanilla-{0}.jar", _OriginalString)
        End If
    End Function

    Public Function CompareTo(other As SpongeVersion) As Integer Implements IComparable(Of SpongeVersion).CompareTo
        If Me > other Then
            Return -1
        ElseIf Me = other Then
            Return 0
        Else
            Return 1
        End If
    End Function
End Class
