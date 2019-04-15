Public Class JavaServer
    Friend port As Integer = 25565
    Friend ip As String = ""
    Friend resourcepack As String = ""
    Friend resourcepackSha1 As String = ""
    Friend hasNewResourcePack As Boolean = False
    Friend plugins As New List(Of BukkitPlugin)
    Friend Class BukkitPlugin
        Friend ReadOnly Property Name As String
        Friend ReadOnly Property VersionDate As DateTime
        Friend ReadOnly Property Path As String
        Sub New(Name As String, Path As String, VersionDate As Date)
            _Name = Name
            _VersionDate = VersionDate
            _Path = Path
        End Sub
        Public Shared Operator =(plugin1 As BukkitPlugin, plugin2 As BukkitPlugin) As Boolean
            Return plugin1.Path = plugin2.Path
        End Operator
        Public Shared Operator <>(plugin1 As BukkitPlugin, plugin2 As BukkitPlugin) As Boolean
            Return plugin1.Path <> plugin2.Path
        End Operator
    End Class

    Friend Class JavaServerWatcher

        Friend _parent As JavaServer
        Friend watcher As JavaCommander
        Dim process As Process
        Sub New(parent As JavaServer)
            _parent = parent
        End Sub
        Sub Watch()
            If HaveWatcher() = False Then
                watcher = New JavaCommander(Me)
                Select Case _parent.versionType
                    Case "Vanilla"
                        watcher.Text = "Vanilla 伺服器 - " & _parent.serverDir
                        watcher.Run("java", "-Xms" & CType(_parent.FindForm, Launcher).MemoryMinBox.Value & "M -Xmx" & CType(_parent.FindForm, Launcher).MemoryMaxBox.Value & "M -jar " & My.Resources.SpecialChar1 & IO.Path.Combine(_parent.serverDir, "minecraft_server." & _parent.version & ".jar") & My.Resources.SpecialChar1 & " nogui", _parent.serverDir)
                    Case "Forge"
                        ' 1.1~1.2 > Server
                        ' 1.3 ~ > Universal
                        watcher.Text = "Forge 伺服器 - " & _parent.serverDir
                        If New Version(_parent.version) >= New Version(1, 3) Then
                            watcher.Run("java", "-Xms" & CType(_parent.FindForm, Launcher).MemoryMinBox.Value & "M -Xmx" & CType(_parent.FindForm, Launcher).MemoryMaxBox.Value & "M -jar " & My.Resources.SpecialChar1 & IO.Path.Combine(_parent.serverDir, "forge-" & _parent.version & "-" & _parent.forgeVersion & "-universal" & ".jar") & My.Resources.SpecialChar1 & " nogui", _parent.serverDir)
                        Else
                            watcher.Run("java", "-Xms" & CType(_parent.FindForm, Launcher).MemoryMinBox.Value & "M -Xmx" & CType(_parent.FindForm, Launcher).MemoryMaxBox.Value & "M -jar " & My.Resources.SpecialChar1 & IO.Path.Combine(_parent.serverDir, "forge-" & _parent.version & "-" & _parent.forgeVersion & "-server" & ".jar") & My.Resources.SpecialChar1 & " nogui", _parent.serverDir)
                        End If
                    Case "Spigot"
                        watcher.Text = "Spigot 伺服器 - " & _parent.serverDir
                        watcher.Run("java", "-Xms" & CType(_parent.FindForm, Launcher).MemoryMinBox.Value & "M -Xmx" & CType(_parent.FindForm, Launcher).MemoryMaxBox.Value & "M -jar " & My.Resources.SpecialChar1 & IO.Path.Combine(_parent.serverDir, "spigot-" & _parent.version & ".jar") & My.Resources.SpecialChar1, _parent.serverDir)
                    Case "CraftBukkit"
                        watcher.Text = "CraftBukkit 伺服器 - " & _parent.serverDir
                        watcher.Run("java", "-Xms" & CType(_parent.FindForm, Launcher).MemoryMinBox.Value & "M -Xmx" & CType(_parent.FindForm, Launcher).MemoryMaxBox.Value & "M -jar " & My.Resources.SpecialChar1 & IO.Path.Combine(_parent.serverDir, "craftbukkit-" & _parent.version & ".jar") & My.Resources.SpecialChar1, _parent.serverDir)
                    Case "未知"
                        watcher.Text = "伺服器 - " & _parent.serverDir
                        Using opn As New OpenFileDialog
                            opn.InitialDirectory = _parent.serverDir
                            opn.Title = "開啟程式"
                            opn.Filter = "Java JAR 程式 (*.jar)|*.jar"
                            If opn.ShowDialog = DialogResult.OK Then
                                watcher.Run("java", "-Xms" & CType(_parent.FindForm, Launcher).MemoryMinBox.Value & "M -Xmx" & CType(_parent.FindForm, Launcher).MemoryMaxBox.Value & "M -jar " & My.Resources.SpecialChar1 & IO.Path.Combine(_parent.serverDir, opn.FileName) & My.Resources.SpecialChar1, _parent.serverDir)
                            Else
                                Exit Sub
                            End If
                        End Using
                End Select
                watcher.Show()
            End If
        End Sub

        Private Sub Run(program As String, args As String, executePath As String)
            Dim processInfo As New ProcessStartInfo(program, args)
            processInfo.UseShellExecute = False
            processInfo.CreateNoWindow = False
            processInfo.RedirectStandardOutput = True
            processInfo.RedirectStandardError = True
            processInfo.WorkingDirectory = executePath
            process = Process.Start(processInfo)
            process.BeginOutputReadLine()
            process.BeginErrorReadLine()
            process.EnableRaisingEvents = True
            AddHandler process.ErrorDataReceived, Sub(sender, e)
                                                      Try
                                                          If IsNothing(process) = False And process.HasExited = False Then
                                                              If IsNothing(e.Data) = False Then
                                                                  Console.WriteLine("[Java Error Message] " & e.Data)
                                                              End If
                                                          End If
                                                      Catch ex As Exception
                                                      End Try
                                                  End Sub
            AddHandler process.OutputDataReceived, Sub(sender, e)
                                                       Try
                                                           If IsNothing(process) = False And process.HasExited = False Then
                                                               If IsNothing(e.Data) = False Then
                                                                   Console.WriteLine("[Java Output Message] " & e.Data)
                                                               End If
                                                           End If
                                                       Catch ex As Exception
                                                       End Try
                                                   End Sub
            AddHandler process.Exited, Sub(sender, e)
                                           Try
                                               process = Nothing
                                               Console.WriteLine("Process Exited")
                                           Catch ex As Exception
                                           End Try
                                       End Sub
        End Sub

        Private Function HaveWatcher() As Boolean
            Return (IsNothing(watcher) = False)
        End Function
        Sub Close()
            If HaveWatcher() Then
                watcher.Close()
                watcher.Dispose()
            End If
        End Sub
    End Class
    Friend serverDir As String
    Dim dog As New JavaServerWatcher(Me)
    Friend Event ServerOptionNotFound(sender As JavaServer)
    Friend Event GotServerPort(sender As JavaServer, port As Integer)
    Friend version As String = ""
    Friend forgeVersion As String = ""
    Friend versionType As String = ""
    Friend generate_structures As String = "true"
    Friend generator_settings As String = ""
    Friend level_name As String = "world"
    Friend level_seed As String = ""
    Friend level_type As String = "DEFAULT"


    Sub New(ServerDirectory As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        serverDir = ServerDirectory
    End Sub

    Private Sub Server_Load(sender As Object, e As EventArgs) Handles Me.Load
        For i = 0 To ServerOptionBox.RowCount - 2
            Dim label As New Label
            label.Dock = DockStyle.Fill
            label.Text = ""
            label.TextAlign = ContentAlignment.BottomRight
            label.AutoSize = True
            label.AutoEllipsis = True
            ServerOptionBox.Controls.Add(label, 0, i)
        Next
        CreateBooleanSwitch(0) ' allow-flight
        CreateBooleanSwitch(1) ' allow-nether
        CreateBooleanSwitch(2) ' announce-player-achievements
        CreateOptionSwitch(3, "和平", "簡單", "普通", "困難") ' difficulty
        CreateBooleanSwitch(4) ' enable-query
        CreateBooleanSwitch(5) ' enable-rcon
        CreateBooleanSwitch(6, "啟用", "禁用") ' force-gamemode
        CreateOptionSwitch(7, "生存", "創造", "冒險", "旁觀者")  ' gamemode
        CreateBooleanSwitch(8, "啟用", "禁用") ' hardcore
        CreateNumericUpDown(9, 256, 1) 'max-build-height
        CreateNumericUpDown(10, Integer.MaxValue, 1) ' max-players
        CreateNumericUpDown(11, Math.Pow(2, 63) - 1, -1) ' max-tick-time
        CreateNumericUpDown(12, 29999984, 1) ' max-world-size
        CreateTextbox(13) ' motd
        CreateNumericUpDown(14, Decimal.MaxValue, -1) ' network-compression-threshold
        CreateBooleanSwitch(15, "啟用", "禁用") ' online-mode
        CreateOptionSwitch(16, New String() {"等級 1", "等級 2", "等級 3", "等級 4"}, New String() {"1", "2", "3", "4"})  ' op-permission-level
        CreateNumericUpDown(17, Decimal.MaxValue, 0) ' player-idle-timeout
        CreateBooleanSwitch(18, "不開放", "開放") ' prevent-proxy-connections
        CreateBooleanSwitch(19) ' pvp
        CreateTextbox(20) ' rcon.password
        CreateNumericUpDown(21, UShort.MaxValue - 1, 1) ' rcon.port
        CreateBooleanSwitch(22) ' snooper-enabled
        CreateBooleanSwitch(23) ' spawn-animals
        CreateBooleanSwitch(24) ' spawn-monsters
        CreateBooleanSwitch(25) ' spawn-npcs
        CreateNumericUpDown(26, Integer.MaxValue, 0) ' spawn-protection
        CreateNumericUpDown(27, 15, 3) ' view-distance
        CreateBooleanSwitch(28) ' white-list
        CreateBooleanSwitch(29) ' enable-command-block
        CType(ServerOptionBox.GetControlFromPosition(0, 0), Label).Text = "允許生存模式下飛行"
        CType(ServerOptionBox.GetControlFromPosition(0, 1), Label).Text = "允許玩家進入地獄"
        CType(ServerOptionBox.GetControlFromPosition(0, 2), Label).Text = "玩家獲得成就時在伺服器顯示"
        CType(ServerOptionBox.GetControlFromPosition(0, 3), Label).Text = "遊戲難度"
        CType(ServerOptionBox.GetControlFromPosition(0, 4), Label).Text = "允許使用GameSpy4協議的伺服器監聽器"
        CType(ServerOptionBox.GetControlFromPosition(0, 5), Label).Text = "允許遠端訪問伺服器控制台"
        CType(ServerOptionBox.GetControlFromPosition(0, 6), Label).Text = "強制玩家加入時為預設遊戲模式"
        CType(ServerOptionBox.GetControlFromPosition(0, 7), Label).Text = "預設遊戲模式"
        CType(ServerOptionBox.GetControlFromPosition(0, 8), Label).Text = "極限模式"
        CType(ServerOptionBox.GetControlFromPosition(0, 9), Label).Text = "最大建築高度"
        CType(ServerOptionBox.GetControlFromPosition(0, 10), Label).Text = "最大玩家數量"
        CType(ServerOptionBox.GetControlFromPosition(0, 11), Label).Text = "每tick(刻)的最大毫秒數"
        CType(ServerOptionBox.GetControlFromPosition(0, 12), Label).Text = "世界最大半徑"
        CType(ServerOptionBox.GetControlFromPosition(0, 13), Label).Text = "伺服器簡介"
        CType(ServerOptionBox.GetControlFromPosition(0, 14), Label).Text = "伺服器最大封包大小"
        CType(ServerOptionBox.GetControlFromPosition(0, 15), Label).Text = "線上模式(正版驗證)"
        CType(ServerOptionBox.GetControlFromPosition(0, 16), Label).Text = "管理員(OP)的權限等級"
        CType(ServerOptionBox.GetControlFromPosition(0, 17), Label).Text = "最大空閒(掛機、AFK)時間"
        CType(ServerOptionBox.GetControlFromPosition(0, 18), Label).Text = "開放玩家使用VPN或代理"
        CType(ServerOptionBox.GetControlFromPosition(0, 19), Label).Text = "允許玩家對戰(PvP)"
        CType(ServerOptionBox.GetControlFromPosition(0, 20), Label).Text = "遠端訪問的密碼"
        CType(ServerOptionBox.GetControlFromPosition(0, 21), Label).Text = "遠端訪問的埠號"
        CType(ServerOptionBox.GetControlFromPosition(0, 22), Label).Text = "允許數據採集"
        CType(ServerOptionBox.GetControlFromPosition(0, 23), Label).Text = "允許生成動物"
        CType(ServerOptionBox.GetControlFromPosition(0, 24), Label).Text = "允許生成怪物"
        CType(ServerOptionBox.GetControlFromPosition(0, 25), Label).Text = "允許生成NPC(村民)"
        CType(ServerOptionBox.GetControlFromPosition(0, 26), Label).Text = "出生點的保護半徑"
        CType(ServerOptionBox.GetControlFromPosition(0, 27), Label).Text = "視線距離"
        CType(ServerOptionBox.GetControlFromPosition(0, 28), Label).Text = "允許白名單"
        CType(ServerOptionBox.GetControlFromPosition(0, 29), Label).Text = "允許指令方塊執行"

        ' Boolean Option:
        ' 0=true
        ' 1=false

        SetOptionValue(0, "false")
        SetOptionValue(1, "true")
        SetOptionValue(2, "true")
        SetOptionValue(3, 1)
        SetOptionValue(4, "false")
        SetOptionValue(5, "false")
        SetOptionValue(6, "false")
        SetOptionValue(7, 0)
        SetOptionValue(8, "false")
        SetOptionValue(9, 256)
        SetOptionValue(10, 20)
        SetOptionValue(11, 60000)
        SetOptionValue(12, 29999984)
        SetOptionValue(13, "A Minecraft Server")
        SetOptionValue(14, 256)
        SetOptionValue(15, "true")
        SetOptionValue(16, 4)
        SetOptionValue(17, 0)
        SetOptionValue(18, "false")
        SetOptionValue(19, "true")
        SetOptionValue(20, "")
        SetOptionValue(21, 25575)
        SetOptionValue(22, "true")
        SetOptionValue(23, "true")
        SetOptionValue(24, "true")
        SetOptionValue(25, "true")
        SetOptionValue(26, 16)
        SetOptionValue(27, 10)
        SetOptionValue(28, "false")
        SetOptionValue(29, "false")
        Try
            LocationLabel.Text = "伺服器路徑：" & serverDir
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(serverDir, "server.info")) Then
                Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(serverDir, "server.info"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)

                    Do Until reader.EndOfStream
                        Dim infoText As String = reader.ReadLine
                        Dim info = infoText.Split("=", 2, StringSplitOptions.None)
                        Select Case info(0)
                            Case "server-version"
                                version = info(1)
                            Case "server-version-type"
                                versionType = info(1)
                            Case "forge-build-version"
                                forgeVersion = info(1)
                        End Select
                    Loop
                    SetVersionLabel()
                End Using

            End If
        Catch ex As IO.FileNotFoundException
            version = ""
            versionType = "未知"
        End Try
        Try
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(serverDir, "server.info")) Then
                Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(serverDir, "server.properties"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                    Dim optionDict As New Dictionary(Of Integer, Boolean)
                    Do Until reader.EndOfStream
                        Dim optionText As String = reader.ReadLine
                        If optionText.StartsWith("#") = False Then
                            Dim options = optionText.Split("=", 2, StringSplitOptions.None)
                            If options.Count = 2 Then
                                SetOptionValue(ToRow(options(0)), options(1))
                            ElseIf options.Count = 1 Then
                                SetOptionValue(ToRow(options(0)), "")
                            ElseIf options.Count = 0 Then
                            Else
                            End If
                        End If
                    Loop
                End Using
            Else
                CType(FindForm(), Launcher).UnRegisterJavaServer(Parent)
                RaiseEvent ServerOptionNotFound(Me)
            End If
        Catch fileException As IO.FileNotFoundException
            RaiseEvent ServerOptionNotFound(Me)
        End Try
        If My.Computer.FileSystem.FileExists(IO.Path.Combine(serverDir, "server-icon.png")) Then
            Button4.BackgroundImage = Image.FromFile(IO.Path.Combine(serverDir, "server-icon.png"))
            Button4.BackgroundImageLayout = ImageLayout.Stretch
        End If
        If versionType = "Forge" And forgeVersion <> "" Then
            If ForgeUpdateCheck() = True Then
                UpdateServerButton.Enabled = True
                SetVersionLabel("[有更新的版本]")
                ToolTip1.SetToolTip(VersionLabel, String.Format("目前的版本：{0}" & vbNewLine & "更新的版本：{1}", forgeVersion, ForgeNewestBranchVersionList(version)))
            Else
                UpdateServerButton.Enabled = False
                SetVersionLabel()
                ToolTip1.SetToolTip(VersionLabel, Nothing)
            End If
        End If
        If versionType = "CraftBukkit" OrElse versionType = "Spigot" Then
            LoadPlugins()
            Button6.Enabled = True
        Else
            Button6.Enabled = False
        End If
    End Sub
    Sub LoadPlugins()
        Dim pluginPath = IO.Path.Combine(serverDir, "plugins")
        If My.Computer.FileSystem.FileExists(IO.Path.Combine(pluginPath, "pluginList.json")) Then
            Dim reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(pluginPath, "pluginList.json"), IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read, 4096, True))
            Do Until reader.EndOfStream
                Dim jsonArray As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(reader.ReadToEnd())
                For Each jsonToken In jsonArray.Children
                    Dim jsonObject = jsonToken.ToObject(Of Newtonsoft.Json.Linq.JObject)
                    Dim item As New BukkitPlugin(jsonObject.GetValue("Name").ToString, jsonObject.GetValue("Path").ToString, DateTime.Parse(jsonObject.GetValue("VersionDate").ToString))
                    plugins.Add(item)
                Next
            Loop
        End If
        Dim pluginPathInfo As New IO.DirectoryInfo(pluginPath)
        For Each pluginFileInfo In pluginPathInfo.GetFiles("*.jar", IO.SearchOption.TopDirectoryOnly)
            Dim item As New BukkitPlugin(pluginFileInfo.Name, pluginFileInfo.FullName, pluginFileInfo.CreationTime.ToString)
            If plugins.Contains(item) = False Then
                plugins.Add(item)
            End If
        Next
    End Sub
    Sub SavePlugins()
        Dim pluginPath = IO.Path.Combine(serverDir, "plugins")
        If My.Computer.FileSystem.FileExists(IO.Path.Combine(pluginPath, "pluginList.json")) Then
            Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(pluginPath, "pluginList.json"), IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.Read, 4096, True))
            Dim jsonArray As New Newtonsoft.Json.Linq.JArray()
            For Each plugin In plugins
                Dim jsonObject As New Newtonsoft.Json.Linq.JObject()
                jsonObject.Add("Name", New Newtonsoft.Json.Linq.JValue(plugin.Name))
                jsonObject.Add("VersionDate", New Newtonsoft.Json.Linq.JValue(plugin.VersionDate))
                jsonObject.Add("Path", New Newtonsoft.Json.Linq.JValue(plugin.Path))
                jsonArray.Add(jsonObject)
            Next
            writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(jsonArray))
        End If
    End Sub
#Region "Create Function"
    Overloads Sub CreateBooleanSwitch(row As Integer)
        Dim switch As New CustomComboBox
        switch.Dock = DockStyle.Fill
        switch.OutputMode = OutputMode.String
        switch.DropDownStyle = ComboBoxStyle.DropDownList
        switch.Items.AddRange(New String() {"允許", "不允許"})
        switch.OutputValues = (New String() {"true", "false"})
        ServerOptionBox.Controls.Add(switch, 1, row)
    End Sub
    Overloads Sub CreateBooleanSwitch(row As Integer, trueText As String, falseText As String)
        Dim switch As New CustomComboBox
        switch.Dock = DockStyle.Fill
        switch.OutputMode = OutputMode.String
        switch.DropDownStyle = ComboBoxStyle.DropDownList
        switch.Items.AddRange(New String() {trueText, falseText})
        switch.OutputValues = (New String() {"true", "false"})
        ServerOptionBox.Controls.Add(switch, 1, row)
    End Sub
    Overloads Sub CreateOptionSwitch(row As Integer, ParamArray items As String())
        Dim switch As New CustomComboBox
        switch.Dock = DockStyle.Fill
        switch.OutputMode = OutputMode.Integer
        switch.DropDownStyle = ComboBoxStyle.DropDownList
        switch.Items.AddRange(items)
        ServerOptionBox.Controls.Add(switch, 1, row)
    End Sub
    Overloads Sub CreateOptionSwitch(row As Integer, items As String(), outputValues As String())
        Dim switch As New CustomComboBox
        switch.Dock = DockStyle.Fill
        switch.OutputMode = OutputMode.String
        switch.DropDownStyle = ComboBoxStyle.DropDownList
        switch.OutputValues = outputValues
        switch.Items.AddRange(items)
        ServerOptionBox.Controls.Add(switch, 1, row)
    End Sub
    Sub CreateNumericUpDown(row As Integer, max As Decimal, Optional min As Decimal = 0)
        Dim box As New NumericUpDown
        box.Dock = DockStyle.Fill
        box.Maximum = max
        box.Minimum = min
        ServerOptionBox.Controls.Add(box, 1, row)
    End Sub
    Sub CreateTextbox(row As Integer)
        ServerOptionBox.Controls.Add(New TextBox With {.Dock = DockStyle.Fill}, 1, row)
    End Sub
#End Region
#Region "Input & Output Function"
    Function ToRow(optionName As String) As Integer
        Select Case optionName
            Case "allow-flight" : Return 0
            Case "allow-nether" : Return 1
            Case "announce-player-achievements" : Return 2
            Case "difficulty" : Return 3
            Case "enable-query" : Return 4
            Case "enable-rcon" : Return 5
            Case "force-gamemode" : Return 6
            Case "gamemode" : Return 7
            Case "hardcore" : Return 8
            Case "max-build-height" : Return 9
            Case "max-players" : Return 10
            Case "max-tick-time" : Return 11
            Case "max-world-size" : Return 12
            Case "motd" : Return 13
            Case "network-compression-threshold" : Return 14
            Case "online-mode" : Return 15
            Case "op-permission-level" : Return 16
            Case "player-idle-timeout" : Return 17
            Case "prevent-proxy-connections" : Return 18
            Case "pvp" : Return 19
            Case "rcon.password" : Return 20
            Case "rcon.port" : Return 21
            Case "snooper-enabled" : Return 22
            Case "spawn-animals" : Return 23
            Case "spawn-monsters" : Return 24
            Case "spawn-npcs" : Return 25
            Case "spawn-protection" : Return 26
            Case "view-distance" : Return 27
            Case "white-list" : Return 28
            Case "enable-command-block" : Return 29

                'Special (1000+)
            Case "server-port" : Return 1000
            Case "server-ip" : Return 1001
            Case "resource-pack" : Return 1002
            Case "resource-pack-sha1" : Return 1003
            Case "generate-structures" : Return 1004
            Case "generator-settings" : Return 1005
            Case "level-name" : Return 1006
            Case "level-seed" : Return 1007
            Case "level-type" : Return 1008

            Case Else : Return -1
        End Select

    End Function
    Private Sub SetOptionValue(row As Integer, value As String)
        If row = 1000 Then ' server-port
            RaiseEvent GotServerPort(Me, value)
            port = value
        ElseIf row = 1001 Then ' server-ip
            ip = value
        ElseIf row = 1002 Then ' resource-pack
            resourcepack = value
        ElseIf row = 1003 Then ' resource-pack-sha1
            resourcepackSha1 = value
        ElseIf row = 1004 Then
            generate_structures = value
        ElseIf row = 1005 Then
            generator_settings = value
        ElseIf row = 1006 Then
            level_name = value
        ElseIf row = 1007 Then
            level_seed = value
        ElseIf row = 1008 Then
            level_type = value
        Else
            If row <> -1 Then
                Dim control As Windows.Forms.Control = ServerOptionBox.GetControlFromPosition("1", row)
                Select Case control.GetType
                    Case GetType(CustomComboBox)
                        Select Case CType(control, CustomComboBox).OutputMode
                            Case OutputMode.Integer
                                If IsNumeric(value) Then
                                    CType(control, CustomComboBox).SelectedIndex = value
                                Else
                                    If CType(control, CustomComboBox).Items.Contains(value) Then
                                        CType(control, CustomComboBox).SelectedIndex =
                                            CType(control, CustomComboBox).Items.IndexOf(value)
                                    End If
                                End If
                            Case OutputMode.String
                                If CType(control, CustomComboBox).OutputValues.Contains(value) Then
                                    CType(control, CustomComboBox).SelectedIndex =
                                        CType(control, CustomComboBox).OutputValues.ToList.IndexOf(value)
                                End If
                        End Select
                    Case GetType(NumericUpDown)
                        If IsNumeric(value) Then
                            If (CType(control, NumericUpDown).Maximum >= value) And (CType(control, NumericUpDown).Minimum <= value) Then
                                CType(control, NumericUpDown).Value = value
                            End If
                        End If
                    Case GetType(TextBox)
                        CType(control, TextBox).Text = value
                End Select
            End If

        End If
    End Sub
    Function ToOptionName(row As Integer) As String
        Select Case row
            Case 0 : Return "allow-flight"
            Case 1 : Return "allow-nether"
            Case 2 : Return "announce-player-achievements"
            Case 3 : Return "difficulty"
            Case 4 : Return "enable-query"
            Case 5 : Return "enable-rcon"
            Case 6 : Return "force-gamemode"
            Case 7 : Return "gamemode"
            Case 8 : Return "hardcore"
            Case 9 : Return "max-build-height"
            Case 10 : Return "max-players"
            Case 11 : Return "max-tick-time"
            Case 12 : Return "max-world-size"
            Case 13 : Return "motd"
            Case 14 : Return "network-compression-threshold"
            Case 15 : Return "online-mode"
            Case 16 : Return "op-permission-level"
            Case 17 : Return "player-idle-timeout"
            Case 18 : Return "prevent-proxy-connections"
            Case 19 : Return "pvp"
            Case 20 : Return "rcon.password"
            Case 21 : Return "rcon.port"
            Case 22 : Return "snooper-enabled"
            Case 23 : Return "spawn-animals"
            Case 24 : Return "spawn-monsters"
            Case 25 : Return "spawn-npcs"
            Case 26 : Return "spawn-protection"
            Case 27 : Return "view-distance"
            Case 28 : Return "white-list"
            Case 29 : Return "enable-command-block"
            Case Else : Return ""
        End Select
    End Function
    Private Function GetOptionValue(row As Integer) As String
        If row = 1000 Then ' server-port
            Return port
        ElseIf row = 1001 Then ' server-ip
            Return ip
        ElseIf row = 1002 Then ' resource-pack
            Return resourcepack
        ElseIf row = 1003 Then ' resource-pack-sha1
            Return resourcepackSha1
        Else
            If row <> -1 Then
                Dim control As Windows.Forms.Control = ServerOptionBox.GetControlFromPosition(1, row)
                Console.WriteLine(row)
                Select Case control.GetType
                    Case GetType(CustomComboBox)
                        Return CType(control, CustomComboBox).GetSelectedValue()
                    Case GetType(NumericUpDown)
                        Return CType(control, NumericUpDown).Value
                    Case GetType(TextBox)
                        Return CType(control, TextBox).Text
                End Select
            End If
        End If
    End Function
#End Region

    Private Function IsBool(boolString As String) As Boolean
        Return ((boolString.ToLower = "true") Or (boolString.ToLower = "false"))
    End Function

    Private Function ToBool(boolString As String) As Boolean
        Select Case boolString.ToLower
            Case "true"
                Return True
            Case "false"
                Return False
            Case Else
                Return False
        End Select
    End Function

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        If CType(FindForm(), Launcher).CanUPnP Then
            CType(FindForm(), Launcher).upnpProvider.PortForward(port, GetOptionValue(ToRow("motd")))
        End If
        dog.Watch()
    End Sub
    Sub CloseServer()
        Try
            dog.Close()
            dog = Nothing
            SaveServer()
        Catch ex As Exception
        End Try
        GenerateServerInfo(forgeVersion)
    End Sub

    Private Sub BrowseResourcePack_Click(sender As Object, e As EventArgs) Handles BrowseResourcePack.Click
        Dim dialog As New ChooseResourcePack()
        If dialog.ShowDialog = DialogResult.OK Then
            System.Threading.Tasks.Task.Factory.StartNew(New Action(Sub()
                                                                        ResourcePackLabel.Text = "伺服器資源包：" & "(儲存資源包設定...)"
                                                                        If My.Computer.Network.Ping(dialog.GetResourcePackURI.Host) = True Then
                                                                            Dim destPath As String = IO.Path.Combine(GetATempDirectory(), "resource-pack.zip")
                                                                            My.Computer.Network.DownloadFile(dialog.GetResourcePackURI.ToString, destPath)
                                                                            resourcepackSha1 = CryptoProvider.GetSHA1(destPath)
                                                                            resourcepack = dialog.GetResourcePackURI.AbsoluteUri
                                                                            hasNewResourcePack = True
                                                                            ResourcePackLabel.Text = "伺服器資源包：" & resourcepack
                                                                        End If
                                                                    End Sub))

        End If
    End Sub
    Friend Sub SaveServer()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(serverDir, "server.properties"), "", False)
        Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(serverDir, "server.properties"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
        writer.WriteLine("#Minecraft server properties")
        writer.WriteLine("#" & Now.ToString("ddd MMM dd HH:mm:ss K yyyy", New System.Globalization.CultureInfo("en-US")))
        Dim optionDict As New Dictionary(Of Integer, Boolean)
        For i = 0 To ServerOptionBox.RowCount - 2
            writer.WriteLine(String.Format("{0}={1}", ToOptionName(i), GetOptionValue(i)))
        Next
        writer.WriteLine("query.port" & "=" & port)
        writer.WriteLine("server-ip" & "=" & ip)
        writer.WriteLine("server-port" & "=" & port)
        writer.WriteLine("resource-pack" & "=" & resourcepack)
        writer.WriteLine("resource-pack-sha1" & "=" & resourcepackSha1)
        writer.WriteLine("generate-structures" & "=" & generate_structures)
        writer.WriteLine("generator-settings" & "=" & generator_settings)
        writer.WriteLine("level-name" & "=" & level_name)
        writer.WriteLine("level-seed" & "=" & level_seed)
        writer.WriteLine("level-type" & "=" & level_type)

        writer.WriteLine()
        writer.Flush()
        writer.Close()
        SavePlugins()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveServer()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start(IO.Path.Combine(serverDir, "eula.txt"))
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Process.Start(serverDir)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Using open As New OpenFileDialog()
            open.Title = "開啟圖片..."
            open.Filter = "JPEG 圖片 (*.jpeg,*.jpe,*.jpg)|*.jpeg,*.jpe,*.jpg|PNG 圖片 (*.png)|*.png|GIF 圖像 (*.gif)|*.gif|Windows 點陣圖 (*.bmp)|*.bmp"
            If open.ShowDialog() = DialogResult.OK Then
                Dim bit As Bitmap = Image.FromFile(open.FileName)
                If bit.Size = New Size(64, 64) Then
                    bit.Save(IO.Path.Combine(serverDir, "server-icon.png"), Imaging.ImageFormat.Png)
                Else
                    Dim bit2 As New Bitmap(bit, New Size(64, 64))
                    bit2.Save(open.FileName, Imaging.ImageFormat.Png)
                End If
                'SaveAs64x64PNG(open.FileName, IO.Path.Combine(serverDir, "server-icon.png"))
                Button4.BackgroundImage = Image.FromFile(IO.Path.Combine(serverDir, "server-icon.png"))
                Button4.BackgroundImageLayout = ImageLayout.Zoom
            End If
        End Using
    End Sub
    Sub SaveAs64x64PNG(originalPath As String, outputPath As String)
        Dim original As Bitmap = Image.FromFile(originalPath)
        Dim originalSize As Size = original.Size
        If (originalSize.Height = 64 And originalSize.Width = 64) Then
            original.Save(outputPath, Imaging.ImageFormat.Png)
        Else
            If (originalSize.Height > 64 And originalSize.Width > 64) Then
                If (originalSize.Height = originalSize.Width) Then
                    Dim output As New Bitmap(original, New Size(64, 64))
                    output.Save(outputPath, Imaging.ImageFormat.Png)
                ElseIf (originalSize.Height > originalSize.Width) Then
                    Dim output As New Bitmap(64, 64)
                    Dim outputGraphics As Graphics = Graphics.FromImage(output)
                    outputGraphics.DrawRectangle(New Pen(New SolidBrush(Color.Transparent)), New Rectangle(0, 0, 64, 64))
                    Dim newHeight As Integer = originalSize.Height * (64 / originalSize.Height)
                    Dim newWidth As Integer = originalSize.Width * (64 / originalSize.Height)
                    Dim newX As Integer = (64 - newWidth) / 2
                    Dim newY As Integer = (64 - newHeight) / 2
                    outputGraphics.DrawImage(original, New Rectangle(New Point(newX, newY), New Size(newWidth, newHeight)))
                    outputGraphics.Dispose()
                    output.Save(outputPath, Imaging.ImageFormat.Png)
                ElseIf (originalSize.Height < originalSize.Width) Then
                    Dim output As New Bitmap(64, 64)
                    Dim outputGraphics As Graphics = Graphics.FromImage(output)
                    outputGraphics.DrawRectangle(New Pen(New SolidBrush(Color.Transparent)), New Rectangle(0, 0, 64, 64))
                    Dim newWidth As Integer = originalSize.Width * (64 / originalSize.Width)
                    Dim newHeight As Integer = originalSize.Height * (64 / originalSize.Height)
                    Dim newX As Integer = (64 - newWidth) / 2
                    Dim newY As Integer = (64 - newHeight) / 2
                    outputGraphics.DrawImage(original, New Rectangle(New Point(newX, newY), New Size(newWidth, newHeight)))
                    output.Save(outputPath, Imaging.ImageFormat.Png)
                End If
            ElseIf (originalSize.Height > 64 And originalSize.Width > 64) Then
                Dim output As New Bitmap(64, 64)
                Dim outputGraphics As Graphics = Graphics.FromImage(output)
                outputGraphics.DrawRectangle(New Pen(New SolidBrush(Color.Transparent)), New Rectangle(0, 0, 64, 64))
                Dim newX As Integer = (64 - originalSize.Width) / 2
                Dim newY As Integer = (64 - originalSize.Height) / 2
                outputGraphics.DrawImage(original, New Rectangle(New Point(newX, newY), originalSize))
                output.Save(outputPath, Imaging.ImageFormat.Png)
            ElseIf (originalSize.Height > 64 And originalSize.Width <= 64) Then
                Dim output As New Bitmap(64, 64)
                Dim outputGraphics As Graphics = Graphics.FromImage(output)
                outputGraphics.DrawRectangle(New Pen(New SolidBrush(Color.Transparent)), New Rectangle(0, 0, 64, 64))
                Dim newHeight As Integer = originalSize.Height * (64 / originalSize.Height)
                Dim newWidth As Integer = originalSize.Width * (64 / originalSize.Height)
                Dim newX As Integer = (64 - newWidth) / 2
                Dim newY As Integer = (64 - newHeight) / 2
                outputGraphics.DrawImage(original, New Rectangle(New Point(newX, newY), New Size(newWidth, newHeight)))
                output.Save(outputPath, Imaging.ImageFormat.Png)
            ElseIf (originalSize.Height <= 64 And originalSize.Width > 64) Then
                Dim output As New Bitmap(64, 64)
                Dim outputGraphics As Graphics = Graphics.FromImage(output)
                outputGraphics.DrawRectangle(New Pen(New SolidBrush(Color.Transparent)), New Rectangle(0, 0, 64, 64))
                Dim newWidth As Integer = originalSize.Width * (64 / originalSize.Width)
                Dim newHeight As Integer = originalSize.Height * (64 / originalSize.Height)
                Dim newX As Integer = (64 - newWidth) / 2
                Dim newY As Integer = (64 - newHeight) / 2
                outputGraphics.DrawImage(original, New Rectangle(New Point(newX, newY), New Size(newWidth, newHeight)))
                output.Save(outputPath, Imaging.ImageFormat.Png)
            End If
        End If
    End Sub

    Private Sub UpdateServerButton_Click(sender As Object, e As EventArgs) Handles UpdateServerButton.Click
        Dim forgeInstaller As New ForgeUtil2(serverDir)
        Dim newForgeVersion As String = ForgeNewestBranchVersionList(version)
        SetVersionLabel(True)
        AddHandler forgeInstaller.ForgeDownloadProgressChanged, Sub(args)
                                                                    SetVersionLabel(True, args.ProgressPercentage * 0.5)
                                                                End Sub
        AddHandler forgeInstaller.ForgeDownloadEnd, Sub()
                                                        If forgeInstaller.InstallForge2(version, forgeVersion, CType(FindForm(), Launcher).MemoryMinBox.Value, CType(FindForm(), Launcher).MemoryMaxBox.Value) = DialogResult.OK Then
                                                            Try
                                                                SetVersionLabel(True, 90)
                                                                forgeInstaller.DeleteForgeInstaller(version, forgeVersion)
                                                                GenerateServerInfo(newForgeVersion)
                                                                SetVersionLabel()
                                                                ToolTip1.SetToolTip(VersionLabel, Nothing)
                                                                forgeVersion = newForgeVersion
                                                            Catch ex As Exception
                                                                'MsgBox(ex.StackTrace)
                                                            End Try
                                                        End If
                                                    End Sub
        forgeInstaller.DownloadForge(version, newForgeVersion)
    End Sub

    Private Sub GenerateServerInfo(forgeVersion As String)
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(serverDir, "server.info"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(serverDir, "server.info"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.WriteLine("server-version=" & version)
            writer.WriteLine("server-version-type=" & versionType)
            If versionType = "Forge" Then
                writer.WriteLine("forge-build-version=" & forgeVersion)
            End If
            writer.Flush()
            writer.Close()
        End Using
    End Sub

    Overloads Sub SetVersionLabel(Optional updating As Boolean = False, Optional updatingPercent As Integer = 0)
        BeginInvoke(New Action(Sub()
                                   If versionType = "Forge" Then
                                       If updating Then
                                           VersionLabel.Text = "伺服器版本：" & "原版" & " " & version & " (Forge 版本：" & forgeVersion & ") [更新進度：" & updatingPercent & " %]"
                                       Else
                                           VersionLabel.Text = "伺服器版本：" & "原版" & " " & version & " (Forge 版本：" & forgeVersion & ")"
                                       End If
                                   ElseIf versionType = "Vanilla" Then
                                       VersionLabel.Text = "伺服器版本：" & "原版" & " " & version
                                   Else
                                       VersionLabel.Text = "伺服器版本：" & versionType & " " & version
                                   End If
                               End Sub))
    End Sub
    Overloads Sub SetVersionLabel(addtionText As String)
        BeginInvoke(New Action(Sub()
                                   If versionType = "Forge" Then
                                       If addtionText <> "" Then
                                           VersionLabel.Text = "伺服器版本：" & "原版" & " " & version & " (Forge 版本：" & forgeVersion & ") " & addtionText
                                       Else
                                           VersionLabel.Text = "伺服器版本：" & "原版" & " " & version & " (Forge 版本：" & forgeVersion & ")"
                                       End If
                                   ElseIf versionType = "Vanilla" Then
                                       VersionLabel.Text = "伺服器版本：" & "原版" & " " & version
                                   Else
                                       VersionLabel.Text = "伺服器版本：" & versionType & " " & version
                                   End If
                               End Sub))
    End Sub
    Function ForgeUpdateCheck() As Boolean
        Return (New Version(forgeVersion) < New Version(ForgeNewestBranchVersionList(version)))
    End Function

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim changeMapDialog As New JavaChangeMapForm(Me)
        changeMapDialog.ShowDialog()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub
End Class
