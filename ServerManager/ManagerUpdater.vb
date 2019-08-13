Imports System.Runtime.InteropServices

Public Class ManagerUpdater
    Friend Const StableChannelURL As String = "https://github.com/new1271/MCServerManagerResources/raw/master/stable.exe"
    Friend Const MasterChannelURL As String = "https://github.com/new1271/MCServerManagerResources/raw/master/master.exe"
    Private Const UpdateTextURL As String = "https://raw.githubusercontent.com/new1271/MCServerManagerResources/master/check4Version.txt"
    Friend Shared Function CheckForUpdate(channelInt As Integer, ByRef channel As String) As Boolean
        Try
            channel = {"Stable", "Master", "Nothing"}(channelInt)
            Dim client As New Net.WebClient()
            Dim checkVerStriing As String = client.DownloadString(UpdateTextURL)
            Dim unstableRegex As New Text.RegularExpressions.Regex("[0-9]{1,].[0-9]{1,}(.[0-9]{1,}){0,1} (Alpha|Beta) [0-9]{1,}([a-z]{1})")
            Dim checkVersions As New Dictionary(Of String, String)
            For Each verString As String In checkVerStriing.Split(vbLf)
                verString = verString.TrimStart(vbLf)
                verString = verString.TrimStart(vbCr)
                If verString.Contains("=") Then
                    Dim splitString As String() = verString.Split(New Char() {"="}, 2)
                    checkVersions.Add(splitString(0), splitString(1))
                End If
            Next
            Select Case channel
                Case "Master"
                    Dim latestVersion As String
                    If checkVersions.ContainsKey("Master") Then
                        latestVersion = checkVersions("Master")
                    ElseIf checkVersions.ContainsKey("Stable") Then
                        latestVersion = checkVersions("Stable")
                    Else
                        Return False
                    End If
                    If latestVersion.Contains(" ") Then ' Check if the latest version is a unstable version
                        If SERVER_MANAGER_VER.Contains(" ") Then
                            Dim CurrentVersionSplit As String() = SERVER_MANAGER_VER.Split(" ")
                            Dim ItemCounter As Integer = 0
                            For Each versionItem As String In latestVersion.Split(" ")
                                versionItem = versionItem.Trim(" ")
                                Select Case ItemCounter
                                    Case 0 ' Compare main version number
                                        Dim currentVersion1 As Version = Version.Parse(CurrentVersionSplit(ItemCounter).Trim(" "))
                                        Dim latestVersion1 As Version = Version.Parse(versionItem)
                                        If currentVersion1 < latestVersion1 Then
                                            Return True
                                        ElseIf currentVersion1 > latestVersion1 Then
                                            Return False
                                        Else
                                            ItemCounter += 1
                                            Continue For ' Check second item
                                        End If
                                    Case 1 ' Compare second version number
                                        If CurrentVersionSplit.Length > ItemCounter Then
                                            Dim currentVersion2 As Integer = IIf(CurrentVersionSplit(ItemCounter).Trim(" ") = "Alpha", 1, IIf(CurrentVersionSplit(ItemCounter).Trim(" ") = "Beta", 2, 0))
                                            Dim latestVersion2 As Integer = IIf(versionItem = "Alpha", 1, IIf(versionItem = "Beta", 2, 0))
                                            If currentVersion2 < latestVersion2 Then
                                                Return True
                                            ElseIf currentVersion2 > latestVersion2 Then
                                                Return False
                                            Else
                                                ItemCounter += 1
                                                Continue For ' Check third item
                                            End If
                                        Else
                                            Return False
                                        End If
                                    Case 2 ' Compare thid version number
                                        If CurrentVersionSplit.Length > ItemCounter Then
                                            If IsNumeric(latestVersion) = False Then
                                                Dim currentVersion3 As Single = CInt(CurrentVersionSplit(ItemCounter).Trim(" ").Substring(0, CurrentVersionSplit(ItemCounter).Trim(" ").Length - 1)) + (Asc(CurrentVersionSplit(ItemCounter).Trim(" ").Last) - Asc("a") + 1) / 100
                                                Dim latestVersion3 As Single = CInt(versionItem.Substring(0, versionItem.Length - 1)) + (Asc(versionItem.Last) - Asc("a") + 1) / 100
                                                If currentVersion3 < latestVersion3 Then
                                                    Return True
                                                ElseIf currentVersion3 > latestVersion3 Then
                                                    Return False
                                                Else
                                                    Return False
                                                End If
                                            End If
                                        End If
                                    Case Else ' Wait,How do you go here?
                                        Return False
                                End Select
                            Next
                        Else
                            Dim currentVersion As Version = Version.Parse(SERVER_MANAGER_VER.Trim)
                            Dim latestVersion1 As Version = Version.Parse(latestVersion.Split(" ")(0).Trim)
                            If currentVersion < latestVersion1 Then
                                Return True
                            ElseIf currentVersion > latestVersion1 Then
                                Return False
                            Else
                                Return False
                            End If
                        End If
                    Else
                        If SERVER_MANAGER_VER.Contains(" ") Then
                            Dim currentVersion As Version = Version.Parse(SERVER_MANAGER_VER.Split(" ")(0).Trim)
                            Dim latestVersion1 As Version = Version.Parse(latestVersion.Trim)
                            If currentVersion < latestVersion1 Then
                                Return True
                            ElseIf currentVersion > latestVersion1 Then
                                Return False
                            Else
                                Return True ' First item is same = this is a stable, stable > unstable
                            End If
                        Else
                            Dim currentVersion As Version = Version.Parse(SERVER_MANAGER_VER.Trim)
                            Dim latestVersion1 As Version = Version.Parse(latestVersion.Trim)
                            If currentVersion < latestVersion1 Then
                                Return True
                            ElseIf currentVersion > latestVersion1 Then
                                Return False
                            Else
                                Return False
                            End If
                        End If
                    End If
                    Return False ' Default Choose
                Case "Stable"
                    Dim latestVersion As String
                    If checkVersions.ContainsKey("Stable") Then
                        latestVersion = checkVersions("Stable")
                    Else
                        Return False
                    End If
                    If SERVER_MANAGER_VER.Contains(" ") Then
                        Dim currentVersion As Version = Version.Parse(SERVER_MANAGER_VER.Split(" ")(0).Trim)
                        Dim latestVersion1 As Version = Version.Parse(latestVersion.Trim)
                        If currentVersion < latestVersion1 Then
                            Return True
                        ElseIf currentVersion > latestVersion1 Then
                            Return False
                        Else
                            Return True ' First item is same = this is a stable, stable > unstable
                        End If
                    Else
                        Dim currentVersion As Version = Version.Parse(SERVER_MANAGER_VER.Trim)
                        Dim latestVersion1 As Version = Version.Parse(latestVersion.Trim)
                        If currentVersion < latestVersion1 Then
                            Return True
                        ElseIf currentVersion > latestVersion1 Then
                            Return False
                        Else
                            Return False
                        End If
                    End If
                    Return False ' Default Choose
                Case Else
                    Return False ' Default Choose
            End Select
        Catch ex As Exception
            Return False
        End Try
    End Function
    Friend Shared Sub UpdateProgram(channel As String)
        Select Case channel
            Case "Stable"
                Dim exePath As String = Application.ExecutablePath
                If IO.File.Exists(exePath & ".old") Then IO.File.Delete(exePath & ".old")
                IO.File.Move(exePath, exePath & ".old")
                Dim client As New Net.WebClient()
                client.DownloadFile(StableChannelURL, exePath)
            Case "Master"
                Dim exePath As String = Application.ExecutablePath
                If IO.File.Exists(exePath & ".old") Then IO.File.Delete(exePath & ".old")
                IO.File.Move(exePath, exePath & ".old")
                Dim client As New Net.WebClient()
                Try
                    client.DownloadFile(MasterChannelURL, exePath)
                Catch ex As Net.WebException
                    client.DownloadFile(StableChannelURL, exePath)
                Catch ex As Exception

                End Try
        End Select
    End Sub
End Class
