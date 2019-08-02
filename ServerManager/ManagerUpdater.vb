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
            Select Case channel
                Case "Master"
                    For Each verString As String In checkVerStriing.Split(vbLf)
                        verString = verString.TrimStart(vbLf)
                        If verString.StartsWith(channel & "=") Then
                            verString = verString.Substring(channel.Length + 1)
                            If verString = SERVER_MANAGER_VER Then
                                Return False
                            Else
                                Dim currentVersionStrings As String() = SERVER_MANAGER_VER.Split(" ")
                                Dim latestVersionStrings As String() = verString.Split(" ")
                                For i As Integer = 0 To currentVersionStrings.Length - 1
                                    Select Case i
                                        Case 0
                                            If New Version(currentVersionStrings(0)) > New Version(latestVersionStrings(i)) Then
                                                Return True
                                            ElseIf New Version(currentVersionStrings(0)) = New Version(latestVersionStrings(i)) AndAlso currentVersionStrings.Length > latestVersionStrings.Length Then
                                                Return True
                                            End If
                                        Case 1
                                            If currentVersionStrings(i) <> latestVersionStrings(i) Then
                                                If currentVersionStrings(i) = "Alpha" AndAlso latestVersionStrings(i) = "Beta" Then
                                                    Return True
                                                Else
                                                    Return False
                                                End If
                                            End If
                                        Case 2
                                            Dim currentLastNumber As String = currentVersionStrings(i)
                                            Dim latestLastNumber As String = latestVersionStrings(i)
                                            Dim numberSideRegex As New Text.RegularExpressions.Regex("[0-9]{1,}")
                                            If numberSideRegex.Match(currentLastNumber).Value > numberSideRegex.Match(latestLastNumber).Value Then
                                                Return False
                                            ElseIf numberSideRegex.Match(currentLastNumber).Value < numberSideRegex.Match(latestLastNumber).Value Then
                                                Return True
                                            Else
                                                Dim subNumberRegex As New Text.RegularExpressions.Regex("[a-z]{1,}")
                                                If subNumberRegex.IsMatch(currentLastNumber) = False Then
                                                    If subNumberRegex.IsMatch(latestLastNumber) Then
                                                        Return True
                                                    Else
                                                        Return False
                                                    End If
                                                Else
                                                    If subNumberRegex.IsMatch(latestLastNumber) Then
                                                        Return Asc(subNumberRegex.Match(currentLastNumber).Value) < Asc(subNumberRegex.Match(latestLastNumber).Value)
                                                    Else
                                                        Return False
                                                    End If
                                                End If
                                            End If
                                    End Select
                                Next
                            End If
                        End If
                    Next
                    channel = "Stable"
                    For Each verString As String In checkVerStriing.Split(vbNewLine)
                        verString = verString.TrimStart(vbLf)
                        If verString.StartsWith(channel & "=") Then
                            Dim curVersionStrings As String() = SERVER_MANAGER_VER.Split(" ")
                            Dim latVersionStrings As String = verString
                            If New Version(curVersionStrings(0)) > New Version(latVersionStrings) Then
                                Return True
                            ElseIf New Version(curVersionStrings(0)) = New Version(latVersionStrings) AndAlso curVersionStrings.Length > 1 Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    Next
                    Return False
                Case "Stable"
                    For Each verString As String In checkVerStriing.Split(vbNewLine)
                        verString = verString.TrimStart(vbLf)
                        If verString.StartsWith(channel & "=") Then
                            Dim curVersionStrings As String() = SERVER_MANAGER_VER.Split(" ")
                            Dim latVersionStrings As String = verString
                            If New Version(curVersionStrings(0)) > New Version(latVersionStrings) Then
                                Return True
                            ElseIf New Version(curVersionStrings(0)) = New Version(latVersionStrings) AndAlso curVersionStrings.Length > 1 Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    Next
                    Return False
                Case Else
                    Return False
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
