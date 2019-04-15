Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Xml

Friend Class SpongeForgeProvider
    Dim versionList As New Dictionary(Of String, List(Of SpongeVersion))
    Sub Initallise()
        Dim manifestListURL As String = "https://repo.spongepowered.org/maven/org/spongepowered/spongeforge/maven-metadata.xml"
        Try
            Dim client As New Net.WebClient()
            Dim docHtml = client.DownloadString(manifestListURL)
            Dim xmlDocument = New XmlDocument()
            xmlDocument.Load(New IO.StringReader(docHtml))
            Dim versionNodes = xmlDocument.SelectNodes("//metadata/versioning/versions/*")
            For Each versionNode As XmlNode In versionNodes
                Dim version As String = versionNode.InnerText
                Dim versionRegex1 As New Regex("[0-9.]{1,}-[0-9]{4,5}-[0-9.]{1,}-(DEV|BETA)-[0-9]{1,4}") 'Dev/Beta Detect
                Dim versionRegex2 As New Regex("[0-9.]{1,}-[0-9]{4,5}-[0-9.]{1,}-RC[0-9]{1,4}") 'RC Detect
                Dim versionRegex3 As New Regex("[0-9.]{1,}-[0-9]{4,5}-[0-9.]{1,}") 'Release Detect
                If (versionRegex1.IsMatch(version) _
                                                                OrElse versionRegex2.IsMatch(version) _
                                                                OrElse versionRegex3.IsMatch(version)) Then
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
                                ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(2), SpongeVersionType.Dev, versionData(4), versionData(1), True))
                            Case "BETA"
                                ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(2), SpongeVersionType.Beta, versionData(4), versionData(1), True))
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
                        ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(2), SpongeVersionType.RC, versionData(3).Substring(2), versionData(1), True))
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
                        ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(2), SpongeVersionType.Release, versionData(1), True))
                        versionList(versionData(0)) = ChildVersionList
                    End If
                End If
            Next
            docHtml = Nothing
            client.Dispose()
        Catch ex As Exception
        End Try
    End Sub
    Function GetSpongeForgeVersionsOnBranch(mcVersion As String, forgeBranch As Integer) As SpongeVersion()
        Dim result As New List(Of SpongeVersion)
        If versionList.ContainsKey(mcVersion) Then
            For Each version In versionList(mcVersion)
                If version.ForgeBranch = forgeBranch Then
                    result.Add(version)
                End If
            Next
            result.Reverse()
        End If
        Return result.ToArray
    End Function
End Class
