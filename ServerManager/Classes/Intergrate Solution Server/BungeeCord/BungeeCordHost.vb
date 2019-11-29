Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Enum BungeeCordType
    [Nothing]
    [Error]
    BungeeCord
    Waterfall
End Enum
Public Class BungeeCordHost
    Public Event BungeeInfoUpdated()

    Public ReadOnly Property BungeePath As String
    Public ReadOnly Property BungeePathName As String
    Public ReadOnly Property BungeeVersion As Integer
    Public ReadOnly Property BungeeType As BungeeCordType
    Public Property CanUpdate As Boolean
    Public ReadOnly Property Servers As New List(Of BungeeServer)
    Public Property IsRunning As Boolean
        Get
            Return _IsRunning
        End Get
        Set(value As Boolean)
            RaiseEvent BungeeInfoUpdated()
            _IsRunning = value
        End Set
    End Property
    Public Property BungeeOptions As BungeeCordOptions
    Private _IsRunning As Boolean
    Private Sub New()
    End Sub
    Private Sub New(bungeePath As String)
        _BungeePath = bungeePath
    End Sub
    Friend Shared Function GetEmptyBungeeCordHost(bungeePath As String) As BungeeCordHost
        If bungeePath <> "" Then
            Dim host As New BungeeCordHost
            With host
                If IO.Directory.Exists(bungeePath) = False Then
                    IO.Directory.CreateDirectory(bungeePath)
                End If
                ._BungeePath = bungeePath
                ._BungeePathName = (New IO.DirectoryInfo(bungeePath)).Name
                .BungeeOptions = BungeeCordOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(bungeePath, "config.yml"))
            End With
            Return host
        Else
            Return Nothing
        End If
    End Function
    Sub SetVersion(version As Integer, type As BungeeCordType)
        _BungeeVersion = version
        _BungeeType = type
        RaiseEvent BungeeInfoUpdated()
    End Sub
    Friend Shared Function GetBungeeCordHost(bungeePath As String) As BungeeCordHost
        Try
            If bungeePath <> "" Then
                Dim host As New BungeeCordHost
                With host
                    If My.Computer.FileSystem.FileExists(IO.Path.Combine(bungeePath, "config.yml")) Then
                        .BungeeOptions = BungeeCordOptions.LoadOptions(IO.Path.Combine(bungeePath, "config.yml"))
                    Else
                        .BungeeOptions = BungeeCordOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(bungeePath, "config.yml"))
                    End If
                    ._BungeePath = bungeePath
                    ._BungeePathName = (New IO.DirectoryInfo(bungeePath)).Name
                    If My.Computer.FileSystem.FileExists(IO.Path.Combine(bungeePath, "bungee.info")) Then
                        Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(bungeePath, "bungee.info"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                            Do Until reader.EndOfStream
                                Dim infoText As String = reader.ReadLine
                                Dim info = infoText.Split("=", 2, StringSplitOptions.None)
                                Select Case info(0)
                                    Case "bungee-type"
                                        Select Case info(1).ToLower
                                            Case "bungeecord"
                                                ._BungeeType = BungeeCordType.BungeeCord
                                            Case "waterfall"
                                                ._BungeeType = BungeeCordType.Waterfall
                                            Case Else
                                                ._BungeeType = BungeeCordType.Error
                                        End Select
                                    Case "bungee-version"
                                        ._BungeeVersion = info(1)
                                    Case "servers"
                                        Dim input As String = info(1)
                                        If input = "" Then
                                            input = "[]"
                                        End If
                                        Dim bungeeServerList As New List(Of BungeeServer)()
                                        Dim jsonArray As JArray = JsonConvert.DeserializeObject(input)
                                        For Each jsonObject As JObject In jsonArray
                                            Try
                                                Dim path As String = jsonObject.GetValue("path").ToString
                                                If GlobalModule.Manager.ServerPathList.Contains(path) Then
                                                    Dim server As New BungeeServer()
                                                    server.Server = GlobalModule.Manager.ServerEntityList(GlobalModule.Manager.ServerPathList.IndexOf(path))
                                                    server.ServerAlias = jsonObject.GetValue("alias").ToString
                                                    server.Restricted = GetBoolean(jsonObject.GetValue("restricted").ToString)
                                                    bungeeServerList.Add(server)
                                                End If
                                            Catch ex As Exception
                                            End Try
                                        Next
                                        ._Servers = bungeeServerList
                                End Select
                                If .BungeeType = BungeeCordType.Nothing Then ' 向前相容用
                                    ._BungeeType = BungeeCordType.BungeeCord
                                End If
                            Loop
                        End Using
                    End If
                    ._CanUpdate = BungeeCordUpdater.CheckForUpdate(.BungeeVersion, .BungeeType)
                End With
                Return host
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Sub SaveSolution()
        If BungeeOptions.Servers IsNot Nothing Then BungeeOptions.Servers.Clear()
        If IsNothing(Servers) = False Then
            For Each server In Servers
                BungeeOptions.Servers.Add(server.ToBungeeCordServer)
            Next
        End If
        BungeeOptions.SaveOption()
        Dim writer As IO.StreamWriter
        If IO.File.Exists(IO.Path.Combine(BungeePath, "bungee.info")) Then
            writer = New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(BungeePath, "bungee.info"), IO.FileMode.Truncate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
        Else
            writer = New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(BungeePath, "bungee.info"), IO.FileMode.CreateNew, IO.FileAccess.Write), System.Text.Encoding.UTF8)
        End If
        writer.WriteLine("bungee-version=" & BungeeVersion)
        writer.WriteLine("bungee-type=" & BungeeType.ToString.ToLower)
        Dim jsonArray As New JArray
        If IsNothing(Servers) = False Then
            For Each server In Servers
                Dim jsonObject As New JObject
                jsonObject.Add("alias", server.ServerAlias)
                jsonObject.Add("path", server.Server.ServerPath)
                jsonObject.Add("restricted", server.Restricted)
                jsonArray.Add(jsonObject)
            Next
        End If
        writer.WriteLine("servers=" & JsonConvert.SerializeObject(jsonArray))
        writer.Flush()
        writer.Close()

    End Sub
    Class BungeeServer
        Public Property ServerAlias As String
        Public Property Server As Server
        Public Property Restricted As Boolean
        Sub New(Server As Server, NameAlias As String, Restricted As Boolean)
            Me.Server = Server
            ServerAlias = NameAlias
            Me.Restricted = Restricted
        End Sub
        Sub New(Server As Server)
            Me.Server = Server
            ServerAlias = ""
            Restricted = False
        End Sub
        ''' <summary>
        ''' 後設型New()函數，之後一定要指定伺服器
        ''' </summary>
        Sub New()
            Restricted = False
        End Sub
        Function ToBungeeCordServer() As BungeeCordServer
            Dim ip = Server.ServerOptions.Item("server-ip")
            If ip = "" Then ip = "0.0.0.0"
            Return New BungeeCordServer(ServerAlias, Server.ServerOptions.Item("motd"), ip & ":" & Server.ServerOptions.Item("server-port"), Restricted)
        End Function
        Function RunServer() As Process
            If IO.Directory.Exists(Server.ServerPath) = True Then
                Select Case Server.ServerType
                    Case Server.EServerType.Java
                        Select Case Server.ServerVersionType
                            Case Server.EServerVersionType.Vanilla
                                If Server.Server2ndVersion <> "" Then
                                    Return Run(GetJavaPath(), "-Djline.terminal=jline.UnsupportedTerminal -Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "minecraft_server." & Server.Server2ndVersion & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                                Else
                                    Return Run(GetJavaPath(), "-Djline.terminal=jline.UnsupportedTerminal -Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "minecraft_server." & Server.ServerVersion & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                                End If
                            Case Server.EServerVersionType.Forge
                                ' 1.1~1.2 > Server
                                ' 1.3 ~ > Universal
                                ' 1.13+ -> Nothing
                                ' 1.8.9 and 1.7.10 -> Server Version Twice
                                If New Version(Server.ServerVersion) >= New Version(1, 3) Then
                                    If New Version(Server.ServerVersion) >= New Version(1, 13) Then
                                        Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                                    Else
                                        If Server.ServerVersion = "1.8.9" OrElse Server.ServerVersion = "1.7.10" Then
                                            Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & "-" & Server.ServerVersion & "-universal" & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                                        Else
                                            Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & "-universal" & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                                        End If
                                    End If
                                Else
                                    Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & "-server" & ".jar") & """" & " nogui", Server.ServerPath)
                                End If
                            Case Server.EServerVersionType.Spigot
                                Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spigot-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                            Case Server.EServerVersionType.Spigot_Git
                                Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spigot-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                            Case Server.EServerVersionType.CraftBukkit
                                Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "craftbukkit-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                            Case Server.EServerVersionType.SpongeVanilla
                                Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spongeVanilla-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                            Case Server.EServerVersionType.Paper
                                Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "paper-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                            Case Server.EServerVersionType.Akarin
                                Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "akarin-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                            Case Server.EServerVersionType.Cauldron
                                Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "server.jar") & """", Server.ServerPath)
                            Case Server.EServerVersionType.Thermos
                                Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "Thermos-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                            Case Server.EServerVersionType.Contigo
                                Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "Contigo-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                            Case Server.EServerVersionType.Kettle
                                Return Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "kettle-git-HEAD-" & Server.Server2ndVersion & "-universal.jar") & """", Server.ServerPath)
                        End Select
                    Case Else
                        Return Nothing
                End Select
            Else
                Return Nothing
            End If
        End Function
        Private Overloads Sub Run()
            Run("", "", "")
        End Sub
        Private Overloads Function Run(program As String, serverDir As String) As Process
            Return Run(program, "", serverDir, True, False)
        End Function
        Private Overloads Function Run(program As String, args As String, serverDir As String, Optional nogui As Boolean = True, Optional UTF8 As Boolean = True) As Process
            Dim backgroundProcess = Process.Start(PrepareStartInfo(program, args, serverDir, nogui, UTF8))
            Server.IsRunning = True
            backgroundProcess.EnableRaisingEvents = True
            Return backgroundProcess
        End Function
        Private Overloads Function PrepareStartInfo(program As String, args As String, serverDir As String, Optional nogui As Boolean = True, Optional UTF8Encoding As Boolean = False) As ProcessStartInfo
            Dim processInfo As ProcessStartInfo
            If args = "" Then
                processInfo = New ProcessStartInfo(program)
            Else
                If UTF8Encoding Then args = "-Dfile.encoding=UTF8 " & args
                processInfo = New ProcessStartInfo(program, args)
            End If
            processInfo.UseShellExecute = False
            processInfo.CreateNoWindow = nogui
            If UTF8Encoding And nogui Then
                processInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
                processInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
            End If
            processInfo.RedirectStandardOutput = nogui
            processInfo.RedirectStandardError = nogui
            processInfo.RedirectStandardInput = nogui
            processInfo.WorkingDirectory = serverDir
            Return processInfo
        End Function

    End Class
End Class
