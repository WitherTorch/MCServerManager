Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public NotInheritable Class Server
    Public Event Initallised()
    Public Event ServerInfoUpdated()
    Public Event ServerIconUpdated()
    Public Event ServerUpdateStart()
    Public Event ServerUpdating(percent As Integer)
    Public Event ServerUpdateEnd()

    Public ReadOnly Property IsInitallised As Boolean = False
    Public ReadOnly Property ServerPath As String
    Public ReadOnly Property ServerPathName As String
    Public ReadOnly Property ServerVersion As String
    Public ReadOnly Property ServerVersionType As EServerVersionType
    Public ReadOnly Property Server2ndVersion As String
    Public ReadOnly Property ServerType As EServerType
    Public ReadOnly Property ServerIcon As Image = New Bitmap(64, 64)
    Public ReadOnly Property CanUpdate As Boolean
    Public Property ServerTasks As ServerTask()
    Public Property CustomServerRunFile As String
    Public Property IsRunning As Boolean
        Get
            Return _IsRunning
        End Get
        Set(value As Boolean)
            _IsRunning = value
            RaiseEvent ServerInfoUpdated()
        End Set
    End Property
    Public Property ProcessID As Long = 0
    Public Property ServerOptions As New Dictionary(Of String, String)
    Public Property BukkitOptions As BukkitOptions
    Public Property SpigotOptions As SpigotOptions
    Public Property PaperOptions As PaperOptions
    Public Property AkarinOptions As AkarinOptions
    Public Property CauldronOptions As CauldronOptions
    Public Property NukkitOptions As NukkitOptions
    Public Property ServerPlugins As New List(Of BukkitPlugin)
    Public Property ServerMods As New List(Of ForgeMod)
    Public ReadOnly Property SpongeVersionType As String
    Public ReadOnly Property Server3rdVersion As String
    Private _IsRunning As Boolean

    Private Sub New()
    End Sub
    Private Sub New(serverPath As String)
        _ServerPath = serverPath
    End Sub
    Public Enum EServerType
        [Error]
        Custom
        Java
        Bedrock
    End Enum
    Public Enum EServerVersionType
        [Error]
        Custom
        Vanilla
        Forge
        CraftBukkit
        Spigot
        Spigot_Git
        Paper
        Akarin
        Cauldron
        Thermos
        Contigo
        Kettle
        SpongeVanilla
        VanillaBedrock
        Nukkit
    End Enum
    Friend Shared Function CreateServer() As Server
        Return New Server
    End Function
    Friend Overloads Shared Function GetServer(serverPath As String) As Server
        Dim ForgeUpdateCheck As Boolean = True
        If serverPath <> "" Then
            Dim server As New Server(serverPath)
            Try
                Dim taskList As New List(Of ServerTask)
                If My.Computer.FileSystem.FileExists(IO.Path.Combine(serverPath, "server.info")) Then
                    Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(serverPath, "server.info"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                        Do Until reader.EndOfStream
                            Dim infoText As String = reader.ReadLine
                            Dim info = infoText.Split("=", 2, StringSplitOptions.None)
                            If info.Length >= 2 Then
                                Select Case info(0)
                                    Case "server-version"
                                        server._ServerVersion = info(1)
                                    Case "server-version-type"
                                        Select Case info(1).ToLower
                                            Case "vanilla"
                                                server._ServerVersionType = EServerVersionType.Vanilla
                                                server._ServerType = EServerType.Java
                                            Case "forge"
                                                server._ServerVersionType = EServerVersionType.Forge
                                                server._ServerType = EServerType.Java
                                            Case "spigot"
                                                server._ServerVersionType = EServerVersionType.Spigot
                                                server._ServerType = EServerType.Java
                                            Case "spigot_git"
                                                server._ServerVersionType = EServerVersionType.Spigot_Git
                                                server._ServerType = EServerType.Java
                                            Case "craftbukkit"
                                                server._ServerVersionType = EServerVersionType.CraftBukkit
                                                server._ServerType = EServerType.Java
                                            Case "nukkit"
                                                server._ServerVersionType = EServerVersionType.Nukkit
                                                server._ServerType = EServerType.Bedrock
                                            Case "spongevanilla"
                                                server._ServerVersionType = EServerVersionType.SpongeVanilla
                                                server._ServerType = EServerType.Java
                                            Case "vanillabedrock"
                                                server._ServerVersionType = EServerVersionType.VanillaBedrock
                                                server._ServerType = EServerType.Bedrock
                                            Case "paper"
                                                server._ServerVersionType = EServerVersionType.Paper
                                                server._ServerType = EServerType.Java
                                            Case "akarin"
                                                server._ServerVersionType = EServerVersionType.Akarin
                                                server._ServerType = EServerType.Java
                                            Case "cauldron"
                                                server._ServerVersionType = EServerVersionType.Cauldron
                                                server._ServerType = EServerType.Java
                                            Case "thermos"
                                                server._ServerVersionType = EServerVersionType.Thermos
                                                server._ServerType = EServerType.Java
                                            Case "contigo"
                                                server._ServerVersionType = EServerVersionType.Contigo
                                                server._ServerType = EServerType.Java
                                            Case "kettle"
                                                server._ServerVersionType = EServerVersionType.Kettle
                                                server._ServerType = EServerType.Java
                                            Case "custom"
                                                server._ServerVersionType = EServerVersionType.Custom
                                                server._ServerType = EServerType.Custom
                                            Case Else
                                                server._ServerVersionType = EServerVersionType.Error
                                                server._ServerType = EServerType.Error
                                        End Select
                                    Case "spigot-build-version"
                                        server._Server2ndVersion = info(1)
                                    Case "kettle-branch-id"
                                        server._Server2ndVersion = info(1)
                                    Case "forge-build-version"
                                        server._Server2ndVersion = info(1)
                                    Case "nukkit-build-version"
                                        server._Server2ndVersion = info(1)
                                    Case "spongeVanilla-version"
                                        server._Server2ndVersion = info(1)
                                    Case "spongeVanilla-type"
                                        server._SpongeVersionType = info(1)
                                    Case "spongeVanilla-build-version"
                                        server._Server3rdVersion = info(1)
                                    Case "paper-build-version"
                                        server._Server2ndVersion = info(1)
                                    Case "akarin-build-version"
                                        server._Server2ndVersion = info(1)
                                    Case "akarin-branch-name"
                                        server._Server3rdVersion = info(1)
                                    Case "vanilla-build-version"
                                        server._Server2ndVersion = info(1)
                                    Case "server-file"
                                        server.CustomServerRunFile = info(1)
                                    Case "tasks"
                                        Dim jsonArray As JArray = JsonConvert.DeserializeObject(Of JArray)(info(1))
                                        For Each jsonObject As JObject In jsonArray
                                            Dim task As New ServerTask(CInt(jsonObject.GetValue("mode")), jsonObject.GetValue("name"))
                                            task.Enabled = jsonObject.GetValue("enabled")
                                            Select Case task.Mode
                                                Case ServerTask.TaskMode.Repeating
                                                    task.RepeatingPeriod = jsonObject.GetValue("period")
                                                    task.RepeatingPeriodUnit = CInt(jsonObject.GetValue("periodUnit"))
                                                Case ServerTask.TaskMode.Trigger
                                                    task.TriggerEvent = CInt(jsonObject.GetValue("event"))
                                            End Select
                                            Dim command As JObject = jsonObject.GetValue("command")
                                            Dim taskCommand As New ServerTask.TaskCommand()
                                            taskCommand.Action = CInt(command.GetValue("action"))
                                            taskCommand.Data = command.GetValue("data")
                                            task.Command = taskCommand
                                            taskList.Add(task)
                                        Next
                                End Select
                            End If
                        Loop
                    End Using
                End If
                server.ServerTasks = taskList.ToArray
                If server.ServerVersionType = EServerVersionType.Akarin Then
                    If server.Server3rdVersion = "" Then
                        If server.ServerVersion = "1.13" OrElse
                                server.ServerVersion = "1.13.1" Then
                            server._Server3rdVersion = "ver/1.13"
                        ElseIf server.ServerVersion.StartsWith("1.12") Then
                            server._Server3rdVersion = "ver/1.12.2"
                        Else
                            server._Server3rdVersion = "master"
                        End If
                    End If
                End If
            Catch ex As IO.FileNotFoundException
                server._ServerVersionType = EServerVersionType.Error
                server._ServerType = EServerType.Error
            End Try
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(serverPath, "server-icon.png")) Then
                server._ServerIcon = Image.FromFile(IO.Path.Combine(serverPath, "server-icon.png"))
            Else
                server._ServerIcon = My.Resources.ServerDefaultIcon
            End If
            server._ServerPathName = (New IO.DirectoryInfo(serverPath)).Name
            Return server
        Else
            Return Nothing
        End If
    End Function
    Friend Sub AddOrSetOption(optionName As String, optionValue As String)
        If ServerOptions Is Nothing Then ServerOptions = New Dictionary(Of String, String)
        If ServerOptions.ContainsKey(optionName) Then
            ServerOptions(optionName) = optionValue
        Else
            ServerOptions.Add(optionName, optionValue)
        End If
    End Sub
    Friend Sub ReloadServerIcon()
        If My.Computer.FileSystem.FileExists(IO.Path.Combine(ServerPath, "server-icon.png")) Then
            _ServerIcon = Image.FromFile(IO.Path.Combine(ServerPath, "server-icon.png"))
        Else
            _ServerIcon = My.Resources.ServerDefaultIcon
        End If
        RaiseEvent ServerIconUpdated()
    End Sub
    Friend Sub Initallise()
        Dim mainThread As New Thread(Sub()
                                         Try
                                             Dim serverOptions As New Dictionary(Of String, String)
                                             If My.Computer.FileSystem.FileExists(IO.Path.Combine(ServerPath, "server.properties")) Then
                                                 Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(ServerPath, "server.properties"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                                                     Dim optionDict As New Dictionary(Of Integer, Boolean)
                                                     Do Until reader.EndOfStream
                                                         Try
                                                             Dim optionText As String = reader.ReadLine
                                                             If optionText.StartsWith("#") = False Then
                                                                 Dim options = optionText.Split("=", 2, StringSplitOptions.None)
                                                                 If options.Count = 2 Then
                                                                     serverOptions.Add(options(0), options(1))
                                                                 ElseIf options.Count = 1 Then
                                                                     If options(0).Trim(" ") <> "" Then serverOptions.Add(options(0), "")
                                                                 ElseIf options.Count = 0 Then
                                                                 Else
                                                                 End If
                                                             End If
                                                         Catch ex As Exception
                                                             Continue Do
                                                         End Try
                                                     Loop
                                                 End Using
                                             End If
                                             _ServerOptions = serverOptions
                                         Catch fileException As IO.FileNotFoundException
                                         End Try
                                         Select Case ServerVersionType
                                             Case EServerVersionType.CraftBukkit
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "bukkit.yml")) Then
                                                     BukkitOptions = BukkitOptions.LoadOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 Else
                                                     BukkitOptions = BukkitOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 End If
                                             Case EServerVersionType.Spigot
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "bukkit.yml")) Then
                                                     BukkitOptions = BukkitOptions.LoadOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 Else
                                                     BukkitOptions = BukkitOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "spigot.yml")) Then
                                                     SpigotOptions = SpigotOptions.LoadOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 Else
                                                     SpigotOptions = SpigotOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 End If
                                             Case EServerVersionType.Spigot_Git
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "bukkit.yml")) Then
                                                     BukkitOptions = BukkitOptions.LoadOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 Else
                                                     BukkitOptions = BukkitOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "spigot.yml")) Then
                                                     SpigotOptions = SpigotOptions.LoadOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 Else
                                                     SpigotOptions = SpigotOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 End If
                                             Case EServerVersionType.Paper
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "bukkit.yml")) Then
                                                     BukkitOptions = BukkitOptions.LoadOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 Else
                                                     BukkitOptions = BukkitOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "spigot.yml")) Then
                                                     SpigotOptions = SpigotOptions.LoadOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 Else
                                                     SpigotOptions = SpigotOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "paper.yml")) Then
                                                     PaperOptions = PaperOptions.LoadOptions(IO.Path.Combine(ServerPath, "paper.yml"))
                                                 Else
                                                     PaperOptions = PaperOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "paper.yml"))
                                                 End If
                                             Case EServerVersionType.Akarin
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "bukkit.yml")) Then
                                                     BukkitOptions = BukkitOptions.LoadOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 Else
                                                     BukkitOptions = BukkitOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "spigot.yml")) Then
                                                     SpigotOptions = SpigotOptions.LoadOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 Else
                                                     SpigotOptions = SpigotOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "paper.yml")) Then
                                                     PaperOptions = PaperOptions.LoadOptions(IO.Path.Combine(ServerPath, "paper.yml"))
                                                 Else
                                                     PaperOptions = PaperOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "paper.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "akarin.yml")) Then
                                                     AkarinOptions = AkarinOptions.LoadOptions(IO.Path.Combine(ServerPath, "akarin.yml"))
                                                 Else
                                                     AkarinOptions = AkarinOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "akarin.yml"))
                                                 End If
                                             Case EServerVersionType.Nukkit
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "nukkit.yml")) Then
                                                     NukkitOptions = NukkitOptions.LoadOptions(IO.Path.Combine(ServerPath, "nukkit.yml"))
                                                 Else
                                                     NukkitOptions = NukkitOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "nukkit.yml"))
                                                 End If
                                             Case EServerVersionType.Cauldron
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "bukkit.yml")) Then
                                                     BukkitOptions = BukkitOptions.LoadOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 Else
                                                     BukkitOptions = BukkitOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 End If
                                                 Select Case ServerVersion
                                                     Case "1.5.2"
                                                         If IO.File.Exists(IO.Path.Combine(ServerPath, "mcpc.yml")) Then
                                                             CauldronOptions = CauldronOptions.LoadOptions(IO.Path.Combine(ServerPath, "mcpc.yml"))
                                                         Else
                                                             CauldronOptions = CauldronOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "mcpc.yml"))
                                                         End If
                                                     Case "1.6.4"
                                                         If IO.File.Exists(IO.Path.Combine(ServerPath, "spigot.yml")) Then
                                                             SpigotOptions = SpigotOptions.LoadOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                         Else
                                                             SpigotOptions = SpigotOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                         End If
                                                         If IO.File.Exists(IO.Path.Combine(ServerPath, "mcpc.yml")) Then
                                                             CauldronOptions = CauldronOptions.LoadOptions(IO.Path.Combine(ServerPath, "mcpc.yml"))
                                                         Else
                                                             CauldronOptions = CauldronOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "mcpc.yml"))
                                                         End If
                                                     Case "1.7.2"
                                                         If IO.File.Exists(IO.Path.Combine(ServerPath, "spigot.yml")) Then
                                                             SpigotOptions = SpigotOptions.LoadOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                         Else
                                                             SpigotOptions = SpigotOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                         End If
                                                         If IO.File.Exists(IO.Path.Combine(ServerPath, "cauldron.yml")) Then
                                                             CauldronOptions = CauldronOptions.LoadOptions(IO.Path.Combine(ServerPath, "cauldron.yml"))
                                                         Else
                                                             CauldronOptions = CauldronOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "cauldron.yml"))
                                                         End If
                                                     Case "1.7.10"
                                                         If IO.File.Exists(IO.Path.Combine(ServerPath, "cauldron.yml")) Then
                                                             CauldronOptions = CauldronOptions.LoadOptions(IO.Path.Combine(ServerPath, "cauldron.yml"))
                                                         Else
                                                             CauldronOptions = CauldronOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "cauldron.yml"))
                                                         End If
                                                 End Select
                                             Case EServerVersionType.Thermos
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "bukkit.yml")) Then
                                                     BukkitOptions = BukkitOptions.LoadOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 Else
                                                     BukkitOptions = BukkitOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "spigot.yml")) Then
                                                     SpigotOptions = SpigotOptions.LoadOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 Else
                                                     SpigotOptions = SpigotOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "cauldron.yml")) Then
                                                     CauldronOptions = CauldronOptions.LoadOptions(IO.Path.Combine(ServerPath, "cauldron.yml"))
                                                 Else
                                                     CauldronOptions = CauldronOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "cauldron.yml"))
                                                 End If
                                             Case EServerVersionType.Contigo
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "bukkit.yml")) Then
                                                     BukkitOptions = BukkitOptions.LoadOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 Else
                                                     BukkitOptions = BukkitOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "bukkit.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "spigot.yml")) Then
                                                     SpigotOptions = SpigotOptions.LoadOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 Else
                                                     SpigotOptions = SpigotOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "spigot.yml"))
                                                 End If
                                                 If IO.File.Exists(IO.Path.Combine(ServerPath, "cauldron.yml")) Then
                                                     CauldronOptions = CauldronOptions.LoadOptions(IO.Path.Combine(ServerPath, "cauldron.yml"))
                                                 Else
                                                     CauldronOptions = CauldronOptions.CreateOptionsWithDefaultSetting(IO.Path.Combine(ServerPath, "cauldron.yml"))
                                                 End If
                                         End Select
                                         CheckForUpdate()
                                         If _ServerVersionType = EServerVersionType.CraftBukkit OrElse
                                             _ServerVersionType = EServerVersionType.Spigot OrElse
                                             _ServerVersionType = EServerVersionType.Spigot_Git OrElse
                                             _ServerVersionType = EServerVersionType.Paper OrElse
                                             _ServerVersionType = EServerVersionType.Akarin OrElse
                                             _ServerVersionType = EServerVersionType.Cauldron OrElse
                                             _ServerVersionType = EServerVersionType.Thermos OrElse
                                             _ServerVersionType = EServerVersionType.Contigo OrElse
                                             _ServerVersionType = EServerVersionType.Kettle Then
                                             Try
                                                 LoadPlugins()
                                             Catch ex As Exception

                                             End Try
                                         End If
                                         If _ServerVersionType = EServerVersionType.Forge OrElse
                                                 _ServerVersionType = EServerVersionType.Cauldron OrElse
                                                 _ServerVersionType = EServerVersionType.Thermos OrElse
                                                 _ServerVersionType = EServerVersionType.Contigo OrElse
                                                 _ServerVersionType = EServerVersionType.Kettle Then
                                             Try
                                                 LoadMods()
                                             Catch ex As Exception

                                             End Try
                                         End If
                                         If _ServerVersionType = EServerVersionType.Nukkit Then
                                             Try
                                                 LoadPlugins()
                                             Catch ex As Exception

                                             End Try
                                         End If
                                         _IsInitallised = True
                                         RaiseEvent Initallised()
                                     End Sub)
        mainThread.IsBackground = True
        mainThread.Start()
    End Sub
    Friend Sub CheckForUpdate()
        If _ServerVersionType = EServerVersionType.Forge And _Server2ndVersion <> "" Then
            _CanUpdate = False
            Dim thread As New Threading.Thread(Sub()
                                                   If ForgeUpdateCheck() = True Then
                                                       _CanUpdate = True
                                                   Else
                                                       _CanUpdate = False
                                                   End If
                                                   RaiseEvent ServerInfoUpdated()
                                               End Sub)
            thread.Name = "Forge GetUpdate Thread"
            thread.IsBackground = True
            thread.Start()
        ElseIf _ServerVersionType = EServerVersionType.SpongeVanilla And _Server2ndVersion <> "" And _Server3rdVersion <> "" And SpongeVersionType <> "" Then
            _CanUpdate = False
            Dim thread As New Threading.Thread(Sub()
                                                   If SpongeVanillaUpdateCheck() = True Then
                                                       _CanUpdate = True
                                                   Else
                                                       _CanUpdate = False
                                                   End If
                                                   RaiseEvent ServerInfoUpdated()
                                               End Sub)
            thread.Name = "SpongeVanilla GetUpdate Thread"
            thread.IsBackground = True
            thread.Start()
        ElseIf _ServerVersionType = EServerVersionType.Nukkit And _Server2ndVersion <> "" Then
            _CanUpdate = False
            Dim thread As New Threading.Thread(Sub()
                                                   If NukkitUpdateCheck() = True Then
                                                       _CanUpdate = True
                                                   Else
                                                       _CanUpdate = False
                                                   End If
                                                   RaiseEvent ServerInfoUpdated()
                                               End Sub)
            thread.Name = "Nukkit GetUpdate Thread"
            thread.IsBackground = True
            thread.Start()
        ElseIf _ServerVersionType = EServerVersionType.VanillaBedrock Then
            _CanUpdate = False
            Dim thread As New Threading.Thread(Sub()
                                                   If VanillaBedrockUpdateCheck() = True Then
                                                       _CanUpdate = True
                                                   Else
                                                       _CanUpdate = False
                                                   End If
                                                   RaiseEvent ServerInfoUpdated()
                                               End Sub)
            thread.Name = "VanillaBedrock GetUpdate Thread"
            thread.IsBackground = True
            thread.Start()
        ElseIf _ServerVersionType = EServerVersionType.Spigot_Git And _Server2ndVersion <> "" Then
            _CanUpdate = False
            Dim thread As New Threading.Thread(Sub()
                                                   If SpigotGitUpdateCheck() = True Then
                                                       _CanUpdate = True
                                                   Else
                                                       _CanUpdate = False
                                                   End If
                                                   RaiseEvent ServerInfoUpdated()
                                               End Sub)
            thread.Name = "Spigot Git GetUpdate Thread"
            thread.IsBackground = True
            thread.Start()
        ElseIf _ServerVersionType = EServerVersionType.Akarin And _Server2ndVersion <> "" And _Server3rdVersion <> "" Then
            _CanUpdate = False
            Dim thread As New Threading.Thread(Sub()
                                                   If AkarinUpdateCheck() = True Then
                                                       _CanUpdate = True
                                                   Else
                                                       _CanUpdate = False
                                                   End If
                                                   RaiseEvent ServerInfoUpdated()
                                               End Sub)
            thread.Name = "Akarin GetUpdate Thread"
            thread.IsBackground = True
            thread.Start()
        End If
    End Sub
    Private Function ForgeUpdateCheck() As Boolean
        If ForgeVersionDict.ContainsKey(New Version(ServerVersion)) Then
            Return New Version(Server2ndVersion) < ForgeVersionDict(New Version(ServerVersion))
        Else
            Return False
        End If
    End Function
    Private Function SpongeVanillaUpdateCheck() As Boolean
        If SpongeVanillaVersionList.ContainsKey(ServerVersion) Then
            Return New SpongeVersion("", ServerVersion, Server2ndVersion, [Enum].Parse(GetType(SpongeVersionType), SpongeVersionType), Server3rdVersion) < SpongeVanillaVersionList(ServerVersion)
        Else
            Return False
        End If
    End Function
    Private Function NukkitUpdateCheck() As Boolean
        Return Server2ndVersion < NukkitVersion
    End Function
    Private Function VanillaBedrockUpdateCheck() As Boolean
        Return Version.Parse(ServerVersion) < VanillaBedrockVersion
    End Function
    Private Function SpigotGitUpdateCheck() As Boolean
        Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(New Net.WebClient().DownloadString("https://hub.spigotmc.org/versions/" & ServerVersion & ".json"))
        Return Server2ndVersion < jsonObject.GetValue("name").ToString
    End Function
    Private Function AkarinUpdateCheck() As Boolean
        Dim webClient As New Net.WebClient
        webClient.Headers.Add(Net.HttpRequestHeader.Accept, "application/json")
        Dim downloadURL = "https://circleci.com/api/v1.1/project/github/Akarin-project/Akarin/tree/" & Server3rdVersion & "?filter=%22successful%22&limit=1"
        Dim docHtml = webClient.DownloadString(downloadURL)
        Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JArray)(docHtml)(0)
        Dim buildNum As Integer = jsonObject.GetValue("build_num")
        Return Server2ndVersion < buildNum
    End Function
    Sub LoadPlugins()
        ServerPlugins.Clear()
        Dim pluginPath = IO.Path.Combine(ServerPath, "plugins")
        Dim paths As New List(Of String)
        If IO.Directory.Exists(pluginPath) Then
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(pluginPath, "pluginList.json")) Then
                Dim reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(pluginPath, "pluginList.json"), IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read, 4096, True))
                Dim jsonArray As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(reader.ReadToEnd())
                If jsonArray IsNot Nothing Then
                    For Each jsonObject As JObject In jsonArray
                        Try
                            If IO.File.Exists(jsonObject.GetValue("Path").ToString) = False Then
                                Dim item As New BukkitPlugin(jsonObject.GetValue("Name").ToString, jsonObject.GetValue("Path").ToString, jsonObject.GetValue("Version"), jsonObject.GetValue("VersionDate"))
                                paths.Add(jsonObject.GetValue("Path").ToString)
                                ServerPlugins.Add(item)
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                End If
            End If
            Dim pluginPathInfo As New IO.DirectoryInfo(pluginPath)
            For Each pluginFileInfo In pluginPathInfo.GetFiles("*.jar", IO.SearchOption.TopDirectoryOnly)
                Try
                    Dim item As New BukkitPlugin(pluginFileInfo.Name, pluginFileInfo.FullName, "", pluginFileInfo.CreationTime)
                    If paths.Contains(item.Path) = False Then
                        Using unpatcher As New BukkitPluginUnpatcher(item.Path)
                            Dim info = unpatcher.GetPluginInfo()
                            If info.IsNull = False Then
                                item.Name = info.Name
                                item.Version = info.Version
                                ServerPlugins.Add(item)
                            End If
                        End Using
                    End If
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub
    Sub SavePlugins()
        Dim pluginPath = IO.Path.Combine(ServerPath, "plugins")
        If IO.Directory.Exists(pluginPath) = False Then
            IO.Directory.CreateDirectory(pluginPath)
        End If
        If My.Computer.FileSystem.FileExists(IO.Path.Combine(pluginPath, "pluginList.json")) = False Then
            IO.File.Create(IO.Path.Combine(pluginPath, "pluginList.json"))
        End If
        Try
            Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(pluginPath, "pluginList.json"), IO.FileMode.Truncate, IO.FileAccess.Write, IO.FileShare.Read, 4096, True))
            Dim jsonArray As New Newtonsoft.Json.Linq.JArray()
            For Each plugin In ServerPlugins
                Dim jsonObject As New Newtonsoft.Json.Linq.JObject()
                jsonObject.Add("Name", New Newtonsoft.Json.Linq.JValue(plugin.Name))
                jsonObject.Add("Version", New Newtonsoft.Json.Linq.JValue(plugin.Version))
                jsonObject.Add("VersionDate", New Newtonsoft.Json.Linq.JValue(plugin.VersionDate))
                jsonObject.Add("Path", New Newtonsoft.Json.Linq.JValue(plugin.Path))
                jsonArray.Add(jsonObject)
            Next
            writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(jsonArray))
            writer.Flush()
            writer.Close()
        Catch ex As IO.IOException
        End Try
    End Sub
    Sub LoadMods()
        ServerMods.Clear()
        Dim modPath = IO.Path.Combine(ServerPath, "mods")
        Dim paths As New List(Of String)
        If IO.Directory.Exists(modPath) Then
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(modPath, "modList.json")) Then
                Dim reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(modPath, "modList.json"), IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read, 4096, True))
                Dim jsonArray As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(reader.ReadToEnd())
                For Each jsonObject As JObject In jsonArray
                    Try
                        If IO.File.Exists(jsonObject.GetValue("Path").ToString) Then
                            Dim item As New ForgeMod(jsonObject.GetValue("Name").ToString, jsonObject.GetValue("Path").ToString, jsonObject.GetValue("Version"), jsonObject.GetValue("VersionDate"))
                            paths.Add(jsonObject.GetValue("Path").ToString)
                            ServerMods.Add(item)
                        End If
                    Catch ex As Exception

                    End Try
                Next
            End If
            Dim modPathInfo As New IO.DirectoryInfo(modPath)
            For Each modFileInfo In modPathInfo.GetFiles("*.jar", IO.SearchOption.TopDirectoryOnly)
                Try
                    Dim item As New ForgeMod(modFileInfo.Name, modFileInfo.FullName, "", modFileInfo.CreationTime.ToString)
                    If paths.Contains(item.Path) = False Then
                        Using unpatcher As New ForgeModUnpatcher(item.Path)
                            Dim info = unpatcher.GetModInfo()
                            If info.IsNull = False Then
                                item.Name = info.Name
                                item.Version = info.Version
                                ServerMods.Add(item)
                            End If
                        End Using
                    End If
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub
    Sub SaveMods()
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
            For Each forgeMod In ServerMods
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
    Friend Sub UpdateServer()
        Select Case ServerVersionType
            Case EServerVersionType.Forge
                Dim forgeInstaller As New ForgeUtil2(ServerPath)
                Dim newForgeVersion As String = ForgeVersionDict(New Version(ServerVersion)).ToString
                RaiseEvent ServerUpdateStart()
                AddHandler forgeInstaller.ForgeDownloadProgressChanged, Sub(args)
                                                                            RaiseEvent ServerUpdating(args.ProgressPercentage * 0.75)
                                                                        End Sub
                AddHandler forgeInstaller.ForgeDownloadEnd, Sub()
                                                                If forgeInstaller.InstallForge2(ServerVersion, Server2ndVersion, ServerMemoryMin, ServerMemoryMax) = DialogResult.OK Then
                                                                    Try
                                                                        RaiseEvent ServerUpdating(90)
                                                                        forgeInstaller.DeleteForgeInstaller(ServerVersion, Server2ndVersion)
                                                                        GenerateServerInfo(newForgeVersion)
                                                                        RaiseEvent ServerUpdateEnd()
                                                                        _Server2ndVersion = newForgeVersion
                                                                        _CanUpdate = False
                                                                        RaiseEvent ServerInfoUpdated()
                                                                        forgeInstaller.DisposeClient()
                                                                        forgeInstaller = Nothing
                                                                    Catch ex As Exception
                                                                        'MsgBox(ex.StackTrace,,Application.ProductName)
                                                                    End Try
                                                                End If
                                                            End Sub
                forgeInstaller.DownloadForge(ServerVersion, newForgeVersion)
            Case EServerVersionType.SpongeVanilla
                Dim downloadURL As String = SpongeVanillaVersionList(ServerVersion).GetDownloadUrl
                RaiseEvent ServerUpdateStart()
                Dim client As New Net.WebClient
                AddHandler client.DownloadProgressChanged, Sub(obj, args)
                                                               RaiseEvent ServerUpdating(args.ProgressPercentage)
                                                           End Sub
                AddHandler client.DownloadFileCompleted, Sub()
                                                             Try
                                                                 Dim spongeVersion = SpongeVanillaVersionList(ServerVersion)
                                                                 _Server2ndVersion = spongeVersion.SpongeVersion.ToString
                                                                 _Server3rdVersion = spongeVersion.Build
                                                                 _SpongeVersionType = spongeVersion.SpongeVersionType.ToString
                                                                 GenerateServerInfo()
                                                                 RaiseEvent ServerUpdateEnd()
                                                                 _CanUpdate = False
                                                                 RaiseEvent ServerInfoUpdated()
                                                                 client.Dispose()
                                                             Catch ex As Exception

                                                             End Try
                                                         End Sub
                client.DownloadFileAsync(New Uri(downloadURL), IO.Path.Combine(ServerPath, "spongeVanilla-" & ServerVersion & ".jar"))
            Case EServerVersionType.Nukkit
                Dim downloadURL As String = GetNukkitDownloadURL(NukkitVersionUrl)
                RaiseEvent ServerUpdateStart()
                Dim client As New Net.WebClient
                AddHandler client.DownloadProgressChanged, Sub(obj, args)
                                                               RaiseEvent ServerUpdating(args.ProgressPercentage)
                                                           End Sub
                AddHandler client.DownloadFileCompleted, Sub()
                                                             Try
                                                                 GenerateServerInfo(NukkitVersion)
                                                                 RaiseEvent ServerUpdateEnd()
                                                                 _Server2ndVersion = NukkitVersion
                                                                 _CanUpdate = False
                                                                 RaiseEvent ServerInfoUpdated()
                                                                 client.Dispose()
                                                             Catch ex As Exception

                                                             End Try
                                                         End Sub
                client.DownloadFileAsync(New Uri(downloadURL), IO.Path.Combine(ServerPath, "nukkit-" & NukkitVersion & ".jar"))
            Case EServerVersionType.VanillaBedrock
                Dim downloadURL As String = "https://minecraft.azureedge.net/bin-win/bedrock-server-" & VanillaBedrockVersion.ToString & ".zip"
                RaiseEvent ServerUpdateStart()
                Dim client As New Net.WebClient
                AddHandler client.DownloadProgressChanged, Sub(obj, args)
                                                               RaiseEvent ServerUpdating(args.ProgressPercentage * 0.8)
                                                           End Sub
                AddHandler client.DownloadFileCompleted, Sub()
                                                             Using archive As ZipArchive = ZipFile.OpenRead(IO.Path.Combine(ServerPath, "bedrock-" & VanillaBedrockVersion.ToString & ".zip"))
                                                                 For Each entry As ZipArchiveEntry In archive.Entries
                                                                     If (entry.FullName.EndsWith(".properties") OrElse entry.FullName.EndsWith(".json")) AndAlso
                                                                     ((entry.FullName.Contains("/") OrElse entry.FullName.Contains("\")) = False) Then
                                                                     Else
                                                                         If entry.FullName.EndsWith("\") OrElse entry.FullName.EndsWith("/") Then
                                                                             If New IO.DirectoryInfo(IO.Path.Combine(IIf(ServerPath.EndsWith("\"), ServerPath, ServerPath & "\"), entry.FullName)).Exists = False Then
                                                                                 IO.Directory.CreateDirectory(IO.Path.Combine(IIf(ServerPath.EndsWith("\"), ServerPath, ServerPath & "\"), entry.FullName))
                                                                             End If
                                                                         Else
                                                                             If New IO.FileInfo(IO.Path.Combine(ServerPath, entry.FullName)).Directory.Exists = False Then
                                                                                 Dim info = New IO.FileInfo(IO.Path.Combine(IIf(ServerPath.EndsWith("\"), ServerPath, ServerPath & "\"), entry.FullName))
                                                                                 info.Directory.Create()
                                                                             End If
                                                                             If New IO.FileInfo(IO.Path.Combine(ServerPath, entry.FullName)).Exists = False Then
                                                                                 Dim info = New IO.FileInfo(IO.Path.Combine(IIf(ServerPath.EndsWith("\"), ServerPath, ServerPath & "\"), entry.FullName))
                                                                                 info.Delete()
                                                                             End If
                                                                             entry.ExtractToFile(IO.Path.Combine(IIf(ServerPath.EndsWith("\"), ServerPath, ServerPath & "\"), entry.FullName), True)
                                                                         End If
                                                                     End If
                                                                 Next
                                                             End Using
                                                             Try
                                                                 _ServerVersion = VanillaBedrockVersion.ToString
                                                                 GenerateServerInfo()
                                                                 RaiseEvent ServerUpdateEnd()
                                                                 _CanUpdate = False
                                                                 RaiseEvent ServerInfoUpdated()
                                                                 client.Dispose()
                                                             Catch ex As Exception

                                                             End Try
                                                         End Sub
                client.DownloadFileAsync(New Uri(downloadURL), IO.Path.Combine(ServerPath, "bedrock-" & VanillaBedrockVersion.ToString & ".zip"))
            Case EServerVersionType.Spigot_Git
                RaiseEvent ServerUpdateStart()
                Dim client As New Net.WebClient()
                AddHandler client.DownloadProgressChanged, Sub(obj, args)
                                                               Try
                                                                   RaiseEvent ServerUpdating(args.ProgressPercentage * 0.5)
                                                               Catch ex As Exception
                                                               End Try
                                                           End Sub
                AddHandler client.DownloadFileCompleted, Sub()
                                                             client.Dispose()
                                                             Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(New Net.WebClient().DownloadString("https://hub.spigotmc.org/versions/" & ServerVersion & ".json"))
                                                             SetVersion(_ServerVersion, jsonObject.GetValue("name"))
                                                             RaiseEvent ServerUpdating(50)
                                                             Dim watcher As New SpigotGitBuildWindow()
                                                             watcher.Run(GitBashPath, "--login -i -c """ & GetJavaPath() & " -jar BuildTools.jar --rev " & ServerVersion & """", ServerPath)
                                                             If watcher.ShowDialog() = DialogResult.OK Then
                                                                 'Try
                                                                 RaiseEvent ServerUpdating(90)

                                                                 'downloader.DeleteForgeInstaller(craftVersion, forgeVersion)
                                                                 SaveServer(False)
                                                                 GenerateServerInfo()
                                                                 'GlobalModule.Manager.ServerPathList.Add(path)
                                                                 RaiseEvent ServerUpdateEnd()
                                                                 ' Catch ex As Exception
                                                                 'MsgBox(ex.StackTrace)
                                                                 ' End Try
                                                             End If
                                                         End Sub
                client.DownloadFileAsync(New Uri("https://hub.spigotmc.org/jenkins/job/BuildTools/lastSuccessfulBuild/artifact/target/BuildTools.jar"), IO.Path.Combine(ServerPath, "BuildTools.jar"))
            Case EServerVersionType.Akarin
                RaiseEvent ServerUpdateStart()
                Dim subClient As New Net.WebClient
                subClient.Headers.Add(Net.HttpRequestHeader.Accept, "application/json")
                Dim downloadURL = "https://circleci.com/api/v1.1/project/github/Akarin-project/Akarin/tree/" & Server3rdVersion & "?filter=%22successful%22&limit=1"
                Dim subDocHtml = subClient.DownloadString(downloadURL)
                Dim subJsonObject As JObject = JsonConvert.DeserializeObject(Of JArray)(subDocHtml)(0)
                Dim buildNum As Integer = subJsonObject.GetValue("build_num")
                subClient.Headers.Add(Net.HttpRequestHeader.Accept, "application/json")
                Dim anotherDocHTML = subClient.DownloadString("https://circleci.com/api/v1.1/project/github/Akarin-project/Akarin/" & buildNum & "/artifacts")
                Dim regex As New Regex("akarin-[0-9].[0-9]{1,2}.[0-9]{1,2}.jar")
                For Each anotherJSONObject As JObject In JsonConvert.DeserializeObject(Of JArray)(anotherDocHTML)
                    Dim targetURL As String = anotherJSONObject.GetValue("url")
                    If regex.IsMatch(targetURL) Then
                        Dim matchString As String = regex.Match(targetURL).Value
                        matchString = matchString.Remove(0, 7)
                        matchString = matchString.Substring(0, matchString.Length - 4)
                        If Version.TryParse(matchString, Nothing) Then
                            SetVersion(matchString, buildNum, Server3rdVersion)
                            RaiseEvent ServerUpdating(10)
                            Dim client As New Net.WebClient
                            AddHandler client.DownloadProgressChanged, Sub(obj, args)
                                                                           RaiseEvent ServerUpdating(10 + args.ProgressPercentage * 0.9)
                                                                       End Sub
                            AddHandler client.DownloadFileCompleted, Sub()
                                                                         Try
                                                                             GenerateServerInfo(NukkitVersion)
                                                                             RaiseEvent ServerUpdateEnd()
                                                                             _Server2ndVersion = buildNum
                                                                             _CanUpdate = False
                                                                             RaiseEvent ServerInfoUpdated()
                                                                             client.Dispose()
                                                                         Catch ex As Exception

                                                                         End Try
                                                                     End Sub

                            client.DownloadFileAsync(New Uri(targetURL), IO.Path.Combine(ServerPath, "akarin-" & matchString & ".jar"))
                            Exit For
                        End If
                    End If
                Next

        End Select
    End Sub
    Private Overloads Sub GenerateServerInfo()
        GenerateServerInfo(Server2ndVersion)
    End Sub
    Private Overloads Sub GenerateServerInfo(secondVersion As String)
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(ServerPath, "server.info"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(ServerPath, "server.info"), IO.FileMode.Truncate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.AutoFlush = True
            writer.WriteLine("server-version=" & ServerVersion)
            writer.WriteLine("server-version-type=" & ServerVersionType.ToString)
            Select Case ServerVersionType
                Case EServerVersionType.Custom
                    writer.WriteLine("server-file=" & CustomServerRunFile)
                Case EServerVersionType.Vanilla
                    writer.WriteLine("vanilla-build-version=" & secondVersion)
                Case EServerVersionType.Forge
                    writer.WriteLine("forge-build-version=" & secondVersion)
                Case EServerVersionType.Spigot_Git
                    writer.WriteLine("spigot-build-version=" & secondVersion)
                Case EServerVersionType.Paper
                    writer.WriteLine("paper-build-version=" & secondVersion)
                Case EServerVersionType.Akarin
                    writer.WriteLine("akarin-build-version=" & secondVersion)
                    writer.WriteLine("akarin-branch-name=" & Server3rdVersion)
                Case EServerVersionType.Kettle
                    writer.WriteLine("kettle-branch-id=" & secondVersion)
                Case Server.EServerVersionType.SpongeVanilla
                    writer.WriteLine("spongeVanilla-version=" & secondVersion)
                    writer.WriteLine("spongeVanilla-type=" & SpongeVersionType)
                    writer.WriteLine("spongeVanilla-build-version" & Server3rdVersion)
                Case EServerVersionType.Nukkit
                    writer.WriteLine("nukkit-build-version=" & secondVersion)
            End Select
            Dim jsonArray As New JArray
            If IsNothing(ServerTasks) = False Then
                For Each task As ServerTask In ServerTasks
                    Dim jsonObject As New JObject
                    jsonObject.Add("mode", task.Mode)
                    jsonObject.Add("name", task.Name)
                    jsonObject.Add("enabled", task.Enabled)
                    Select Case task.Mode
                        Case ServerTask.TaskMode.Repeating
                            jsonObject.Add("period", task.RepeatingPeriod)
                            jsonObject.Add("periodUnit", task.RepeatingPeriodUnit)
                        Case ServerTask.TaskMode.Trigger
                            jsonObject.Add("event", task.TriggerEvent)
                    End Select
                    Dim command As New JObject
                    Dim taskCommand As ServerTask.TaskCommand = task.Command
                    command.Add("action", taskCommand.Action)
                    command.Add("data", taskCommand.Data)
                    jsonObject.Add("command", command)
                    jsonArray.Add(jsonObject)
                Next
                writer.WriteLine("tasks=" & JsonConvert.SerializeObject(jsonArray))
            Else
                writer.WriteLine("tasks=" & "[]")
            End If
            writer.Flush()
            writer.Close()
        End Using
    End Sub

    Friend Sub SaveServer(Optional SavePluginOrMods As Boolean = True)
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(ServerPath, "server.properties"), "", False)
        Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(ServerPath, "server.properties"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
        writer.WriteLine("# Minecraft server properties")
        writer.WriteLine("#" & Now.ToString("ddd MMM dd HH:mm:ss K yyyy", System.Globalization.CultureInfo.CurrentUICulture))
        For Each [option] In ServerOptions
            writer.WriteLine(String.Format("{0}={1}", [option].Key, [option].Value))
        Next
        writer.WriteLine()
        writer.Flush()
        writer.Close()
        Dim spigotSaveFlag As Boolean = False
        If ServerVersionType = EServerVersionType.Spigot OrElse
                ServerVersionType = EServerVersionType.Spigot_Git OrElse
                ServerVersionType = EServerVersionType.Paper OrElse
                ServerVersionType = EServerVersionType.Akarin OrElse
                ServerVersionType = EServerVersionType.Cauldron OrElse
        ServerVersionType = EServerVersionType.Thermos OrElse
        ServerVersionType = EServerVersionType.Contigo OrElse
        ServerVersionType = EServerVersionType.Kettle Then
            If Version.Parse(_ServerVersion) <= New Version(1, 11, 2) Then
                spigotSaveFlag = True
            End If
        End If
        If SavePluginOrMods Then
            Select Case ServerVersionType
                Case EServerVersionType.CraftBukkit
                    SavePlugins()
                    If BukkitOptions IsNot Nothing Then BukkitOptions.SaveOption()
                Case EServerVersionType.Spigot
                    SavePlugins()
                    If BukkitOptions IsNot Nothing Then BukkitOptions.SaveOption()
                    If SpigotOptions IsNot Nothing Then SpigotOptions.SaveOption(spigotSaveFlag)
                Case EServerVersionType.Spigot_Git
                    SavePlugins()
                    If BukkitOptions IsNot Nothing Then BukkitOptions.SaveOption()
                    If SpigotOptions IsNot Nothing Then SpigotOptions.SaveOption(spigotSaveFlag)
                Case EServerVersionType.Paper
                    SavePlugins()
                    If BukkitOptions IsNot Nothing Then BukkitOptions.SaveOption()
                    If SpigotOptions IsNot Nothing Then SpigotOptions.SaveOption(spigotSaveFlag)
                    If PaperOptions IsNot Nothing Then PaperOptions.SaveOption()
                Case EServerVersionType.Akarin
                    SavePlugins()
                    If BukkitOptions IsNot Nothing Then BukkitOptions.SaveOption()
                    If SpigotOptions IsNot Nothing Then SpigotOptions.SaveOption(spigotSaveFlag)
                    If PaperOptions IsNot Nothing Then PaperOptions.SaveOption()
                    If AkarinOptions IsNot Nothing Then AkarinOptions.SaveOption()
                Case EServerVersionType.Cauldron
                    SavePlugins()
                    SaveMods()
                    If BukkitOptions IsNot Nothing Then BukkitOptions.SaveOption()
                    If SpigotOptions IsNot Nothing Then SpigotOptions.SaveOption(spigotSaveFlag)
                Case EServerVersionType.Thermos
                    SavePlugins()
                    SaveMods()
                    If BukkitOptions IsNot Nothing Then BukkitOptions.SaveOption()
                    If SpigotOptions IsNot Nothing Then SpigotOptions.SaveOption(spigotSaveFlag)
                Case EServerVersionType.Contigo
                    SavePlugins()
                    SaveMods()
                    If BukkitOptions IsNot Nothing Then BukkitOptions.SaveOption()
                    If SpigotOptions IsNot Nothing Then SpigotOptions.SaveOption(spigotSaveFlag)
                Case EServerVersionType.Kettle
                    SavePlugins()
                    SaveMods()
                    If BukkitOptions IsNot Nothing Then BukkitOptions.SaveOption()
                    If SpigotOptions IsNot Nothing Then SpigotOptions.SaveOption(spigotSaveFlag)
                Case EServerVersionType.Forge
                    SaveMods()
                Case EServerVersionType.Nukkit
                    If NukkitOptions IsNot Nothing Then NukkitOptions.SaveOption()
                    SavePlugins()
            End Select
        End If
        GenerateServerInfo()
    End Sub
    Friend Sub SetPath(path As String)
        If My.Computer.FileSystem.DirectoryExists(path) Then
        Else
            My.Computer.FileSystem.CreateDirectory(path)
        End If
        _ServerPath = path
    End Sub
    Friend Sub SetVersion(version As String, Optional secVersion As String = "", Optional thirdVersion As String = "", Optional sp_verType As SpongeVersionType = GlobalModule.SpongeVersionType.None)
        _ServerVersion = version
        _Server2ndVersion = secVersion
        _Server3rdVersion = thirdVersion
        _SpongeVersionType = sp_verType
    End Sub
    Friend Sub SetVersionType(serverType As EServerType, serverVersionType As EServerVersionType)
        _ServerType = serverType
        _ServerVersionType = serverVersionType
    End Sub
    Public Class BukkitPlugin
        Friend Property Name As String
        Friend Property Version As String
        Friend ReadOnly Property Path As String
        Friend ReadOnly Property VersionDate As DateTime
        Sub New(Name As String, Path As String, Version As String, VersionDate As DateTime)
            _Name = Name
            _Version = Version
            _Path = Path
            _VersionDate = VersionDate
        End Sub
        Public Shared Operator =(plugin1 As BukkitPlugin, plugin2 As BukkitPlugin) As Boolean
            Return plugin1.Path = plugin2.Path
        End Operator
        Public Shared Operator <>(plugin1 As BukkitPlugin, plugin2 As BukkitPlugin) As Boolean
            Return plugin1.Path <> plugin2.Path
        End Operator
    End Class
    Public Class ForgeMod
        Friend Property Name As String
        Friend Property Version As String
        Friend ReadOnly Property Path As String
        Friend ReadOnly Property VersionDate As DateTime
        Sub New(Name As String, Path As String, Version As String, VersionDate As DateTime)
            _Name = Name
            _Version = Version
            _Path = Path
            _VersionDate = VersionDate
        End Sub
        Public Shared Operator =(mod1 As ForgeMod, mod2 As ForgeMod) As Boolean
            Return mod1.Path = mod2.Path
        End Operator
        Public Shared Operator <>(mod1 As ForgeMod, mod2 As ForgeMod) As Boolean
            Return mod1.Path <> mod2.Path
        End Operator
    End Class
End Class
