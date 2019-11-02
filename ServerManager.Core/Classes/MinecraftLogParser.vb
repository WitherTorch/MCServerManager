Imports System.Text.RegularExpressions
Imports ServerManager.MinecraftLogParser.MinecraftConsoleMessage

Public Class MinecraftLogParser
    Class MinecraftConsoleMessage
        Enum MCServerMessageType
            Info
            Warning
            [Error]
            Notify
            Debug
            Trace
        End Enum
        Enum MCMessageType
            None
            PlayerLogin
            PlayerLogout
            PlayerConnected
            PlayerLostConnected
            PlayerInputCommand
        End Enum
        Public Property ServerMessageType As MCServerMessageType
        Public Property BungeeCordMessageType As String
        Public Property MessageType As MCMessageType
        Public Property AddtionalMessage As New Dictionary(Of String, String)()
        Public Property Time As Date
        Public Property Message As String
        Public Property Thread As String
        Public Function ServerMessageTypeString() As String
            Select Case ServerMessageType
                Case MCServerMessageType.Info
                    Return "訊息"
                Case MCServerMessageType.Warning
                    Return "警告"
                Case MCServerMessageType.Error
                    Return "錯誤"
                Case MCServerMessageType.Notify
                    Return "通知"
                Case MCServerMessageType.Debug
                    Return "偵錯"
                Case MCServerMessageType.Trace
                    Return "程式碼追朔"
            End Select
        End Function
    End Class
    'Time Regex
    Shared timeRegex As New Regex("[0-9]{2}\:[0-9]{2}\:[0-9]{2}")
    Shared CheckRegexes As Regex() = {New Regex("\[[0-9]{2}\:[0-9]{2}\:[0-9]{2} [A-Z]{4,5}\]"), New Regex("\[[0-9]{2}\:[0-9]{2}\:[0-9]{2}\] \[[A-Za-z0-9 #$_-]*\/[A-Z]{4,8}\]\:"), New Regex("\[[0-9]{2}:[0-9]{2}:[0-9]{2} [A-Z]{4,8}\] \[[A-Za-z0-9 #$_-]*\]\:"), New Regex("\u001b\[[0-9]{2}m[0-9]{2}\:[0-9]{2}\:[0-9]{2}\u001b\[m \[\u001b\[[0-9]{2}m[A-Z]{4,5} \u001b\[m\]"), New Regex("\[[0-9]{2}\:[0-9]{2}\:[0-9]{2}\] \[[A-Za-z0-9 \:#$_-]*\/[A-Z]{4,8}\] [[A-Za-z0-9 _\-+\*\:\/\\\.\[\]]*\]\:"), New Regex("\[[0-9]{2}\:[0-9]{2}\:[0-9]{2}\.[0-9]{3}\] \[[A-Za-z0-9 \:#$_-]*\/[A-Z]{4,8}\] [[A-Za-z0-9 _\-+\*\:\/\\\.\[\]]*\]\:"), New Regex("[0-9]{2}\:[0-9]{2}\:[0-9]{2} \[[^ ]*\]")}
    'Java Other Message Regex 1
    Shared javaOtherMessageRegex1 As New Regex("[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}\:[0-9]{2}\:[0-9]{2}\,[0-9]{1,3} main [A-Z]{4,5} [A-Za-z \.\:\\\/]*")
    'Java Other Message Regex 2
    Shared javaOtherMessageRegex2 As New Regex("[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}\:[0-9]{2}\:[0-9]{2}\,[0-9]{1,3} [A-Z]{4,5} [A-Za-z \.\:\\\/]*")
    Dim LastCheckedIndex As Integer = -1
    Public Function ToConsoleMessage(originalMessage As String, recieveTime As Date) As MinecraftConsoleMessage
        'Console.WriteLine(originalMessage)
        Dim msg As New MinecraftConsoleMessage
        Dim isChecked As Boolean = False
        If LastCheckedIndex <> -1 Then
            If CheckRegexes(LastCheckedIndex).IsMatch(originalMessage) Then
                isChecked = True
                Return ProcessMessage(originalMessage, CheckRegexes(LastCheckedIndex).Match(originalMessage).Value, LastCheckedIndex, recieveTime)
            End If
        End If
        For i As Integer = 0 To CheckRegexes.Count - 1
            If CheckRegexes(i).IsMatch(originalMessage) Then
                LastCheckedIndex = i
                isChecked = True
                Return ProcessMessage(originalMessage, CheckRegexes(LastCheckedIndex).Match(originalMessage).Value, i, recieveTime)
            End If
        Next
        If isChecked = False Then
            If javaOtherMessageRegex1.IsMatch(originalMessage) Then
                Dim message As String = javaOtherMessageRegex1.Match(originalMessage).Value
                Dim [date] As String() = message.Substring(0, 10).Split("-")
                message = message.Remove(0, 10).TrimStart
                Dim time As String() = message.Substring(0, 8).Split(":")
                Dim msecond As String = New Regex(",[0-9]{1,3}").Match(message).Value.Remove(0, 1)
                msg.Time = New Date(CInt([date](0)), CInt([date](1)), CInt([date](2)), CInt(time(0)), CInt(time(1)), CInt(time(2)), CInt(msecond))
                message = message.Remove(0, 8 + msecond.Length + 1).TrimStart
                msg.Thread = ""
                message = message.Remove(0, 4).TrimStart()
                Dim messageType As String = New Regex("[A-Z]{4,5}").Match(message).Value
                Select Case messageType
                    Case "INFO"
                        msg.ServerMessageType = MCServerMessageType.Info
                    Case "WARN"
                        msg.ServerMessageType = MCServerMessageType.Warning
                    Case "ERROR"
                        msg.ServerMessageType = MCServerMessageType.Error
                    Case "DEBUG"
                        msg.ServerMessageType = MCServerMessageType.Debug
                    Case "TRACE"
                        msg.ServerMessageType = MCServerMessageType.Trace
                    Case Else
                        msg.ServerMessageType = MCServerMessageType.Info
                End Select
                msg.Message = message.Remove(0, messageType.Length).TrimStart()
            ElseIf javaOtherMessageRegex2.IsMatch(originalMessage) Then
                Dim message As String = javaOtherMessageRegex2.Match(originalMessage).Value
                Dim [date] As String() = message.Substring(0, 10).Split("-")
                message = message.Remove(0, 10).TrimStart
                Dim time As String() = message.Substring(0, 8).Split(":")
                Dim msecond As String = New Regex(",[0-9]{1,3}").Match(message).Value.Remove(0, 1)
                msg.Time = New Date(CInt([date](0)), CInt([date](1)), CInt([date](2)), CInt(time(0)), CInt(time(1)), CInt(time(2)), CInt(msecond))
                message = message.Remove(0, 8 + msecond.Length + 1).TrimStart
                msg.Thread = ""
                message = message.TrimStart()
                Dim messageType As String = New Regex("[A-Z]{4,5}").Match(message).Value
                Select Case messageType
                    Case "INFO"
                        msg.ServerMessageType = MCServerMessageType.Info
                    Case "WARN"
                        msg.ServerMessageType = MCServerMessageType.Warning
                    Case "ERROR"
                        msg.ServerMessageType = MCServerMessageType.Error
                    Case "DEBUG"
                        msg.ServerMessageType = MCServerMessageType.Debug
                    Case "TRACE"
                        msg.ServerMessageType = MCServerMessageType.Trace
                    Case Else
                        msg.ServerMessageType = MCServerMessageType.Info
                End Select
                msg.Message = message.Remove(0, messageType.Length).TrimStart()
            Else
                msg.Message = originalMessage
                msg.ServerMessageType = MCServerMessageType.Info
                msg.Thread = ""
                msg.Time = recieveTime
            End If
        End If
        Return msg
    End Function
    Shared Function ProcessMessage(originalMessage As String, checkMessage As String, checkedRegexIndex As Integer, recieveTime As Date) As MinecraftConsoleMessage
        Dim msg As New MinecraftConsoleMessage
        If timeRegex.IsMatch(originalMessage) Then
            Dim timeMatch = timeRegex.Match(originalMessage)
            Dim msgTimeArray = timeMatch.Value.Split(":")
            msg.Time = New Date(recieveTime.Year, recieveTime.Month, recieveTime.Day, msgTimeArray(0), msgTimeArray(1), msgTimeArray(2))
        Else
            msg.Time = recieveTime
        End If
        Select Case checkedRegexIndex
            Case 0 'Bukkit
                Dim messageType As String = New Regex("[A-Z]{4,5}").Match(checkMessage).Value
                Select Case messageType
                    Case "INFO"
                        msg.ServerMessageType = MCServerMessageType.Info
                    Case "WARN"
                        msg.ServerMessageType = MCServerMessageType.Warning
                    Case "ERROR"
                        msg.ServerMessageType = MCServerMessageType.Error
                    Case "DEBUG"
                        msg.ServerMessageType = MCServerMessageType.Debug
                    Case "TRACE"
                        msg.ServerMessageType = MCServerMessageType.Trace
                    Case Else
                        msg.ServerMessageType = MCServerMessageType.Info
                End Select
                msg.Thread = ""
                msg.Message = originalMessage.Substring(checkMessage.Length + 1, originalMessage.Length - (checkMessage.Length + 1)).TrimStart
                Dim playerLoginRegex1 As New Regex("\: UUID of player [A-Za-z0-9_-]* is [0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}")
                Dim playerConnectionRegex As New Regex("\: [A-Za-z0-9_-]{1,}[/\d\d?\d?.\d\d?\d?.\d\d?\d?.\d\d?\d?:\d\d?\d?\d?\d?] logged in with entity id \d{1,32} at (-?\d{1,20}.\d{1,32}, -?\d{1,20}.\d{1,32}, -?\d{1,20}.\d{1,32})")
                Dim playerLoginRegex2 As New Regex("\: [A-Za-z0-9_-]* joined the game")
                Dim playerLostConnectionRegex As New Regex("\: [A-Za-z0-9_-] lost connection")
                Dim playerLogoutRegex As New Regex("\: [A-Za-z0-9_-]* left the game")
                Dim playerUseCommandRegex As New Regex("\: [A-Za-z0-9_-]* issued server command\: ")
                If playerLoginRegex1.IsMatch(originalMessage) Then  'Player Login Regex Match   : UUID of player xxx is 00000000-aaaa-bbbb-cccc-123456789def
                    Dim matchString = playerLoginRegex1.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLogin
                    matchString = matchString.Replace(": UUID of player ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                    msg.AddtionalMessage.Add("uuid", New Regex("[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}").Match(matchString).Value)
                ElseIf playerLoginRegex2.IsMatch(originalMessage) Then  'Player Login Regex Match   : xxx joined the game
                    Dim matchString = playerLoginRegex2.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLogin
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerLogoutRegex.IsMatch(originalMessage) Then  'Player Logout Regex Match   : xxx left the game
                    Dim matchString = playerLogoutRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLogout
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerConnectionRegex.IsMatch(originalMessage) Then  'Player Connection Regex Match   : xxx[/192.168.0.1:25596] logged in with entity id 3132 at (0, 0, 0)
                    Dim matchString = playerConnectionRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerConnected
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerLostConnectionRegex.IsMatch(originalMessage) Then  'Player Lost Connection Regex Match   : xxx lost connection
                    Dim matchString = playerConnectionRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLostConnected
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerUseCommandRegex.IsMatch(originalMessage) Then  'Player Use Command Regex Match   : xxx issued server command: /xx
                    Dim matchString = playerUseCommandRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerInputCommand
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                    msg.AddtionalMessage.Add("command", msg.Message.Substring(matchString.Length + 2))
                Else
                    msg.MessageType = MCMessageType.None
                End If
            Case 1 'Vanilla
                Dim messageType As String = New Regex("[A-Z]{4,5}").Match(checkMessage).Value
                Select Case messageType
                    Case "INFO"
                        msg.ServerMessageType = MCServerMessageType.Info
                    Case "WARN"
                        msg.ServerMessageType = MCServerMessageType.Warning
                    Case "ERROR"
                        msg.ServerMessageType = MCServerMessageType.Error
                    Case "DEBUG"
                        msg.ServerMessageType = MCServerMessageType.Debug
                    Case "TRACE"
                        msg.ServerMessageType = MCServerMessageType.Trace
                    Case "NOTICE"
                        msg.ServerMessageType = MCServerMessageType.Info
                    Case Else
                        msg.ServerMessageType = MCServerMessageType.Info
                End Select
                Dim threadMessage As String = New Regex("\[[A-Za-z ][A-Za-z0-9 #$_-]*\/[A-Z]{4,5}\]").Match(checkMessage).Value
                msg.Thread = threadMessage.Substring(1, threadMessage.Length - messageType.Length - 3)
                msg.Message = originalMessage.Substring(checkMessage.Length, originalMessage.Length - checkMessage.Length).TrimStart
                Dim playerConnectionRegex As New Regex("\: [A-Za-z0-9_-]{1,}[/\d\d?\d?.\d\d?\d?.\d\d?\d?.\d\d?\d?:\d\d?\d?\d?\d?] logged in with entity id \d{1,32} at (-?\d{1,20}.\d{1,32}, -?\d{1,20}.\d{1,32}, -?\d{1,20}.\d{1,32})")
                Dim playerLoginRegex As New Regex("\: [A-Za-z0-9_-]* joined the game")
                Dim playerLostConnectionRegex As New Regex("\: [A-Za-z0-9_-] lost connection")
                Dim playerLogoutRegex As New Regex("\: [A-Za-z0-9_-]* left the game")
                If playerLoginRegex.IsMatch(originalMessage) Then  'Player Login Regex Match   : xxx joined the game
                    Dim matchString = playerLoginRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLogin
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerLogoutRegex.IsMatch(originalMessage) Then  'Player Logout Regex Match   : xxx left the game
                    Dim matchString = playerLogoutRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLogout
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerConnectionRegex.IsMatch(originalMessage) Then  'Player Connection Regex Match   : xxx[/192.168.0.1:25596] logged in with entity id 3132 at (0, 0, 0)
                    Dim matchString = playerConnectionRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerConnected
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerLostConnectionRegex.IsMatch(originalMessage) Then  'Player Lost Connection Regex Match   : xxx lost connection
                    Dim matchString = playerConnectionRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLostConnected
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                Else
                    msg.MessageType = MCMessageType.None
                End If
            Case 2 'SpongeVanilla
                Dim messageType As String = New Regex("[A-Z]{4,5}").Match(checkMessage).Value
                Select Case messageType
                    Case "INFO"
                        msg.ServerMessageType = MCServerMessageType.Info
                    Case "WARN"
                        msg.ServerMessageType = MCServerMessageType.Warning
                    Case "ERROR"
                        msg.ServerMessageType = MCServerMessageType.Error
                    Case "DEBUG"
                        msg.ServerMessageType = MCServerMessageType.Debug
                    Case "TRACE"
                        msg.ServerMessageType = MCServerMessageType.Trace
                    Case Else
                        msg.ServerMessageType = MCServerMessageType.Info
                End Select
                Dim threadMessage As String = New Regex("\[[A-Za-z0-9 #$_-]*\]\:").Match(checkMessage).Value
                threadMessage = threadMessage.Substring(1, threadMessage.Length - 3)
                If threadMessage.Trim = "" Then threadMessage = "ServerBase Thread"
                msg.Thread = threadMessage
                msg.Message = originalMessage.Substring(checkMessage.Length, originalMessage.Length - checkMessage.Length).TrimStart
                Dim playerConnectionRegex As New Regex("[A-Za-z0-9_-]{1,}[/\d\d?\d?.\d\d?\d?.\d\d?\d?.\d\d?\d?:\d\d?\d?\d?\d?] logged in with entity id \d{1,32} at (-?\d{1,20}.\d{1,32}, -?\d{1,20}.\d{1,32}, -?\d{1,20}.\d{1,32})")
                Dim playerLoginRegex As New Regex("[A-Za-z0-9_-]* joined the game")
                Dim playerLostConnectionRegex As New Regex("[A-Za-z0-9_-] lost connection")
                Dim playerLogoutRegex As New Regex("[A-Za-z0-9_-]* left the game")
                If playerLoginRegex.IsMatch(msg.Message) Then  'Player Login Regex Match   : xxx joined the game
                    Dim matchString = playerLoginRegex.Match(msg.Message).Value
                    msg.MessageType = MCMessageType.PlayerLogin
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerLogoutRegex.IsMatch(msg.Message) Then  'Player Logout Regex Match   : xxx left the game
                    Dim matchString = playerLogoutRegex.Match(msg.Message).Value
                    msg.MessageType = MCMessageType.PlayerLogout
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerConnectionRegex.IsMatch(msg.Message) Then  'Player Connection Regex Match   : xxx[/192.168.0.1:25596] logged in with entity id 3132 at (0, 0, 0)
                    Dim matchString = playerConnectionRegex.Match(msg.Message).Value
                    msg.MessageType = MCMessageType.PlayerConnected
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerLostConnectionRegex.IsMatch(msg.Message) Then  'Player Lost Connection Regex Match   : xxx lost connection
                    Dim matchString = playerConnectionRegex.Match(msg.Message).Value
                    msg.MessageType = MCMessageType.PlayerLostConnected
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                Else
                    msg.MessageType = MCMessageType.None
                End If
            Case 3 'Nukkit
                Dim messageType As String = New Regex("[A-Z]{4,5}").Match(checkMessage).Value
                Select Case messageType
                    Case "INFO"
                        msg.ServerMessageType = MCServerMessageType.Info
                    Case "WARN"
                        msg.ServerMessageType = MCServerMessageType.Warning
                    Case "ERROR"
                        msg.ServerMessageType = MCServerMessageType.Error
                    Case "DEBUG"
                        msg.ServerMessageType = MCServerMessageType.Debug
                    Case "TRACE"
                        msg.ServerMessageType = MCServerMessageType.Trace
                    Case Else
                        msg.ServerMessageType = MCServerMessageType.Info
                End Select
                msg.Thread = ""
                msg.Message = originalMessage.Substring(checkMessage.Length + 1, originalMessage.Length - (checkMessage.Length + 1)).TrimStart
            Case 4 'Forge
                Dim messageType As String = New Regex("[A-Z]{4,5}").Match(checkMessage).Value
                Select Case messageType
                    Case "INFO"
                        msg.ServerMessageType = MCServerMessageType.Info
                    Case "WARN"
                        msg.ServerMessageType = MCServerMessageType.Warning
                    Case "ERROR"
                        msg.ServerMessageType = MCServerMessageType.Error
                    Case "DEBUG"
                        msg.ServerMessageType = MCServerMessageType.Debug
                    Case "TRACE"
                        msg.ServerMessageType = MCServerMessageType.Trace
                    Case Else
                        msg.ServerMessageType = MCServerMessageType.Info
                End Select
                Dim threadMessage1 As String = New Regex("\[[A-Za-z0-9 #$_-]*\/[A-Z]{4,5}\]").Match(checkMessage).Value
                Dim threadMessage2 As String = New Regex("\[[A-Za-z0-9 \/\\\..]*\]").Matches(checkMessage)(1).Value
                msg.Thread = String.Format("{0} ({1})", threadMessage1.Substring(1, threadMessage1.Length - messageType.Length - 3), threadMessage2.Substring(1, threadMessage2.Length - 2))
                msg.Message = originalMessage.Substring(checkMessage.Length, originalMessage.Length - checkMessage.Length).TrimStart
                Dim playerConnectionRegex As New Regex("\: [A-Za-z0-9_-]{1,}[/\d\d?\d?.\d\d?\d?.\d\d?\d?.\d\d?\d?:\d\d?\d?\d?\d?] logged in with entity id \d{1,32} at (-?\d{1,20}.\d{1,32}, -?\d{1,20}.\d{1,32}, -?\d{1,20}.\d{1,32})")
                Dim playerLoginRegex As New Regex("\: [A-Za-z0-9_-]* joined the game")
                Dim playerLostConnectionRegex As New Regex("\: [A-Za-z0-9_-] lost connection")
                Dim playerLogoutRegex As New Regex("\: [A-Za-z0-9_-]* left the game")
                If playerLoginRegex.IsMatch(originalMessage) Then  'Player Login Regex Match   : xxx joined the game
                    Dim matchString = playerLoginRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLogin
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerLogoutRegex.IsMatch(originalMessage) Then  'Player Logout Regex Match   : xxx left the game
                    Dim matchString = playerLogoutRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLogout
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerConnectionRegex.IsMatch(originalMessage) Then  'Player Connection Regex Match   : xxx[/192.168.0.1:25596] logged in with entity id 3132 at (0, 0, 0)
                    Dim matchString = playerConnectionRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerConnected
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerLostConnectionRegex.IsMatch(originalMessage) Then  'Player Lost Connection Regex Match   : xxx lost connection
                    Dim matchString = playerConnectionRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLostConnected
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                Else
                    msg.MessageType = MCMessageType.None
                End If
            Case 5 'Forge (1.13 up)
                Dim messageType As String = New Regex("[A-Z]{4,5}").Match(checkMessage).Value
                Select Case messageType
                    Case "INFO"
                        msg.ServerMessageType = MCServerMessageType.Info
                    Case "WARN"
                        msg.ServerMessageType = MCServerMessageType.Warning
                    Case "ERROR"
                        msg.ServerMessageType = MCServerMessageType.Error
                    Case "DEBUG"
                        msg.ServerMessageType = MCServerMessageType.Debug
                    Case "TRACE"
                        msg.ServerMessageType = MCServerMessageType.Trace
                    Case Else
                        msg.ServerMessageType = MCServerMessageType.Info
                End Select
                Dim threadMessage1 As String = New Regex("\[[A-Za-z0-9 #$_-]*\/[A-Z]{4,5}\]").Match(checkMessage).Value
                Dim threadMessage2 As String = New Regex("\[[A-Za-z0-9 \/\\\..]*\]").Matches(checkMessage)(1).Value
                msg.Thread = String.Format("{0} ({1})", threadMessage1.Substring(1, threadMessage1.Length - messageType.Length - 3), threadMessage2.Substring(1, threadMessage2.Length - 2))
                msg.Message = originalMessage.Substring(checkMessage.Length, originalMessage.Length - checkMessage.Length).TrimStart
                Dim playerConnectionRegex As New Regex("\: [A-Za-z0-9_-]{1,}[/\d\d?\d?.\d\d?\d?.\d\d?\d?.\d\d?\d?:\d\d?\d?\d?\d?] logged in with entity id \d{1,32} at (-?\d{1,20}.\d{1,32}, -?\d{1,20}.\d{1,32}, -?\d{1,20}.\d{1,32})")
                Dim playerLoginRegex As New Regex("\: [A-Za-z0-9_-]* joined the game")
                Dim playerLostConnectionRegex As New Regex("\: [A-Za-z0-9_-] lost connection")
                Dim playerLogoutRegex As New Regex("\: [A-Za-z0-9_-]* left the game")
                If playerLoginRegex.IsMatch(originalMessage) Then  'Player Login Regex Match   : xxx joined the game
                    Dim matchString = playerLoginRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLogin
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerLogoutRegex.IsMatch(originalMessage) Then  'Player Logout Regex Match   : xxx left the game
                    Dim matchString = playerLogoutRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLogout
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerConnectionRegex.IsMatch(originalMessage) Then  'Player Connection Regex Match   : xxx[/192.168.0.1:25596] logged in with entity id 3132 at (0, 0, 0)
                    Dim matchString = playerConnectionRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerConnected
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                ElseIf playerLostConnectionRegex.IsMatch(originalMessage) Then  'Player Lost Connection Regex Match   : xxx lost connection
                    Dim matchString = playerConnectionRegex.Match(originalMessage).Value
                    msg.MessageType = MCMessageType.PlayerLostConnected
                    matchString = matchString.Replace(": ", "")
                    msg.AddtionalMessage.Add("player", New Regex("[A-Za-z0-9_-]*").Match(matchString).Value)
                Else
                    msg.MessageType = MCMessageType.None
                End If
            Case 6 'Vanilla Bedrock
                Dim messageType As String = New Regex("[A-Z]{4,5}").Match(checkMessage).Value
                Select Case messageType
                    Case "INFO"
                        msg.ServerMessageType = MCServerMessageType.Info
                    Case "WARN"
                        msg.ServerMessageType = MCServerMessageType.Warning
                    Case "ERROR"
                        msg.ServerMessageType = MCServerMessageType.Error
                    Case "DEBUG"
                        msg.ServerMessageType = MCServerMessageType.Debug
                    Case "TRACE"
                        msg.ServerMessageType = MCServerMessageType.Trace
                    Case Else
                        msg.ServerMessageType = MCServerMessageType.Info
                End Select
                msg.Thread = ""
                msg.Message = originalMessage.Substring(checkMessage.Length + 1, originalMessage.Length - (checkMessage.Length + 1)).TrimStart
            Case 7 'BungeeCord
                msg.BungeeCordMessageType = New Regex("\[[^ ]*\]").Match(checkMessage).Value
                msg.Thread = ""
                msg.Message = originalMessage.Substring(checkMessage.Length + 1, originalMessage.Length - (checkMessage.Length + 1)).TrimStart
        End Select
        Return msg
    End Function
End Class
