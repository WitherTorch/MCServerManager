Public Class BedrockServer
    Friend port As Integer = 25565
    Friend ip As String = ""

    Friend Class BedrockServerWatcher
        Friend _parent As BedrockServer
        Friend watcher As BedrockCommander
        Dim process As Process
        Sub New(parent As BedrockServer)
            _parent = parent
        End Sub
        Sub Watch()
            If HaveWatcherOrProcess() = False Then
                Select Case _parent.versionType
                    Case "Nukkit"
                        'watcher.Text = "Nukkit 伺服器 - " & _parent.serverDir
                        'watcher.Run("java", "-Dfile.encoding=UTF8 -Xms" & CType(_parent.FindForm, Launcher).MemoryMinBox.Value & "M -Xmx" & CType(_parent.FindForm, Launcher).MemoryMaxBox.Value & "M -jar " & My.Resources.SpecialChar1 & IO.Path.Combine(_parent.serverDir, "nukkit-" & _parent.buildVersion & ".jar") & My.Resources.SpecialChar1 & " nogui", _parent.serverDir)
                        Run(IO.Path.Combine(_parent.serverDir, "run.cmd"), _parent.serverDir)
                    Case Else
                        watcher = New BedrockCommander(Me)
                        watcher.Text = "伺服器 - " & _parent.serverDir
                        Using opn As New OpenFileDialog
                            opn.InitialDirectory = _parent.serverDir
                            opn.Title = "開啟程式"
                            opn.Filter = "Java JAR 程式 (*.jar)|*.jar"
                            If opn.ShowDialog = DialogResult.OK Then
                                watcher.Run("java", "-Dfile.encoding=UTF8 -Xms" & CType(_parent.FindForm, Launcher).MemoryMinBox.Value & "M -Xmx" & CType(_parent.FindForm, Launcher).MemoryMaxBox.Value & "M -jar " & My.Resources.SpecialChar1 & IO.Path.Combine(_parent.serverDir, opn.FileName) & My.Resources.SpecialChar1, _parent.serverDir)
                                watcher.Show()
                            Else
                                Exit Sub
                            End If
                        End Using
                End Select

            End If
        End Sub

        Private Sub Run(program As String, executePath As String)
            Dim processInfo As New ProcessStartInfo(program)
            processInfo.UseShellExecute = True
            processInfo.CreateNoWindow = False
            processInfo.RedirectStandardInput = False
            processInfo.WorkingDirectory = executePath
            _parent.ServerStatusLabel.Text = "伺服器狀態：啟動"
            process = Process.Start(processInfo)
            process.EnableRaisingEvents = True
            AddHandler process.Exited, Sub(sender, e)
                                           Try
                                               process = Nothing
                                               Console.WriteLine("Process Exited")
                                               _parent.ServerStatusLabel.Text = "伺服器狀態：關閉"
                                               If CType(_parent.FindForm, Launcher).CanUPnP Then
                                                   CType(_parent.FindForm(), Launcher).upnpProvider.DestroyPort(_parent.port)
                                               End If
                                           Catch ex As Exception
                                           End Try
                                       End Sub
        End Sub

        Private Function HaveWatcherOrProcess() As Boolean
            Return (IsNothing(watcher) = False And IsNothing(process) = False)
        End Function
        Sub Close()
            If HaveWatcherOrProcess() Then
                watcher.Close()
                watcher.Dispose()
            End If
        End Sub
    End Class
    Friend serverDir As String
    Dim dog As New BedrockServerWatcher(Me)
    Friend Event ServerOptionNotFound(sender As BedrockServer)
    Friend Event GotServerPort(sender As BedrockServer, port As Integer)
    Friend version As String = ""
    Friend buildVersion As String = ""
    Friend versionType As String = ""
    Friend generator_settings As String = ""
    Friend level_name As String = "world"
    Friend level_seed As String = ""
    Friend level_type As String = "DEFAULT"

    Sub New(serverDirectory As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        serverDir = serverDirectory
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
        CreateBooleanSwitch(1) ' announce-player-achievements
        CreateBooleanSwitch(2) ' auto-save
        CreateOptionSwitch(3, "和平", "簡單", "普通", "困難") ' difficulty
        CreateBooleanSwitch(4) ' enable-query
        CreateBooleanSwitch(5) ' enable-rcon
        CreateBooleanSwitch(6, "啟用", "禁用") ' force-gamemode
        CreateOptionSwitch(7, "生存", "創造", "冒險", "旁觀者")  ' gamemode
        CreateBooleanSwitch(8, "啟用", "禁用") ' hardcore
        CreateNumericUpDown(9, Integer.MaxValue, 1) ' max-players
        CreateTextbox(10) ' motd
        CreateBooleanSwitch(11) ' pvp
        CreateTextbox(12) ' rcon.password
        CreateBooleanSwitch(13) ' spawn-animals
        CreateBooleanSwitch(14) ' spawn-monsters
        CreateNumericUpDown(15, Integer.MaxValue, 0) ' spawn-protection
        CreateTextbox(16) ' sub-motd
        CreateNumericUpDown(17, 15, 3) ' view-distance
        CreateBooleanSwitch(18) ' white-list
        CreateBooleanSwitch(19) ' xbox-auth
        CType(ServerOptionBox.GetControlFromPosition(0, 0), Label).Text = "允許生存模式下飛行"
        CType(ServerOptionBox.GetControlFromPosition(0, 1), Label).Text = "玩家獲得成就時在伺服器顯示"
        CType(ServerOptionBox.GetControlFromPosition(0, 2), Label).Text = "伺服器自動儲存"
        CType(ServerOptionBox.GetControlFromPosition(0, 3), Label).Text = "遊戲難度"
        CType(ServerOptionBox.GetControlFromPosition(0, 4), Label).Text = "允許使用GameSpy4協議的伺服器監聽器"
        CType(ServerOptionBox.GetControlFromPosition(0, 5), Label).Text = "允許遠端訪問伺服器控制台"
        CType(ServerOptionBox.GetControlFromPosition(0, 6), Label).Text = "強制玩家加入時為預設遊戲模式"
        CType(ServerOptionBox.GetControlFromPosition(0, 7), Label).Text = "預設遊戲模式"
        CType(ServerOptionBox.GetControlFromPosition(0, 8), Label).Text = "極限模式"
        CType(ServerOptionBox.GetControlFromPosition(0, 9), Label).Text = "最大玩家數量"
        CType(ServerOptionBox.GetControlFromPosition(0, 10), Label).Text = "伺服器簡介"
        CType(ServerOptionBox.GetControlFromPosition(0, 11), Label).Text = "允許玩家對戰(PvP)"
        CType(ServerOptionBox.GetControlFromPosition(0, 12), Label).Text = "遠端訪問的密碼"
        CType(ServerOptionBox.GetControlFromPosition(0, 13), Label).Text = "允許生成動物"
        CType(ServerOptionBox.GetControlFromPosition(0, 14), Label).Text = "允許生成怪物"
        CType(ServerOptionBox.GetControlFromPosition(0, 15), Label).Text = "出生點的保護半徑"
        CType(ServerOptionBox.GetControlFromPosition(0, 16), Label).Text = "伺服器副簡介"
        CType(ServerOptionBox.GetControlFromPosition(0, 17), Label).Text = "視線距離"
        CType(ServerOptionBox.GetControlFromPosition(0, 18), Label).Text = "允許白名單"
        CType(ServerOptionBox.GetControlFromPosition(0, 19), Label).Text = "Xbox 驗證"

        ' Boolean Option:
        ' 0=on
        ' 1=off

        SetOptionValue(0, "off")
        SetOptionValue(1, "on")
        SetOptionValue(2, "on")
        SetOptionValue(3, 1)
        SetOptionValue(4, "on")
        SetOptionValue(5, "off")
        SetOptionValue(6, "off")
        SetOptionValue(7, 0)
        SetOptionValue(8, "off")
        SetOptionValue(9, 20)
        SetOptionValue(10, "Server For Minecraft PE")
        SetOptionValue(11, "on")
        SetOptionValue(12, "")
        SetOptionValue(13, "on")
        SetOptionValue(14, "on")
        SetOptionValue(15, 16)
        SetOptionValue(16, "")
        SetOptionValue(17, 10)
        SetOptionValue(18, "off")
        SetOptionValue(19, "on")

        Try
            LocationLabel.Text = "伺服器路徑：" & serverDir
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(serverDir, "server.info")) Then
                Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(serverDir, "server.info"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)

                    Do Until reader.EndOfStream
                        Dim infoText As String = reader.ReadLine
                        Dim info = infoText.Split("=", 2, StringSplitOptions.None)
                        Select Case info(0)
                            Case "server-version"
                                Version = info(1)
                            Case "server-version-type"
                                versionType = info(1)
                            Case "nukkit-build-version"
                                buildVersion = info(1)
                        End Select
                    Loop
                    SetVersionLabel()
                End Using

            End If
        Catch ex As IO.FileNotFoundException
            Version = ""
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

    End Sub
#Region "Create Function"
    Overloads Sub CreateBooleanSwitch(row As Integer)
        Dim switch As New CustomComboBox
        switch.Dock = DockStyle.Fill
        switch.OutputMode = OutputMode.String
        switch.DropDownStyle = ComboBoxStyle.DropDownList
        switch.Items.AddRange(New String() {"允許", "不允許"})
        switch.OutputValues = (New String() {"on", "off"})
        ServerOptionBox.Controls.Add(switch, 1, row)
    End Sub
    Overloads Sub CreateBooleanSwitch(row As Integer, trueText As String, falseText As String)
        Dim switch As New CustomComboBox
        switch.Dock = DockStyle.Fill
        switch.OutputMode = OutputMode.String
        switch.DropDownStyle = ComboBoxStyle.DropDownList
        switch.Items.AddRange(New String() {trueText, falseText})
        switch.OutputValues = (New String() {"on", "off"})
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
            Case "announce-player-achievements" : Return 1
            Case "auto-save" : Return 2
            Case "difficulty" : Return 3
            Case "enable-query" : Return 4
            Case "enable-rcon" : Return 5
            Case "force-gamemode" : Return 6
            Case "gamemode" : Return 7
            Case "hardcore" : Return 8
            Case "max-players" : Return 9
            Case "motd" : Return 10
            Case "pvp" : Return 11
            Case "rcon.password" : Return 12
            Case "spawn-animals" : Return 13
            Case "spawn-monsters" : Return 14
            Case "spawn-protection" : Return 15
            Case "sub-motd" : Return 16
            Case "view-distance" : Return 17
            Case "white-list" : Return 18
            Case "xbox-auth" : Return 19
                'Special (1000+)
            Case "server-port" : Return 1000
            Case "server-ip" : Return 1001
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
            Case 1 : Return "announce-player-achievements"
            Case 2 : Return "auto-save"
            Case 3 : Return "difficulty"
            Case 4 : Return "enable-query"
            Case 5 : Return "enable-rcon"
            Case 6 : Return "force-gamemode"
            Case 7 : Return "gamemode"
            Case 8 : Return "hardcore"
            Case 9 : Return "max-players"
            Case 10 : Return "motd"
            Case 11 : Return "pvp"
            Case 12 : Return "rcon.password"
            Case 13 : Return "spawn-animals"
            Case 14 : Return "spawn-monsters"
            Case 15 : Return "spawn-protection"
            Case 16 : Return "sub-motd"
            Case 17 : Return "view-distance"
            Case 18 : Return "white-list"
            Case 19 : Return "xbox-auth"
            Case Else : Return ""
        End Select
    End Function
    Friend Function GetOptionValue(row As Integer) As String
        If row = 1000 Then ' server-port
            Return port
        ElseIf row = 1001 Then ' server-ip
            Return ip
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
        SaveServer()
        dog.Watch()
    End Sub
    Overloads Sub SetVersionLabel(Optional updating As Boolean = False, Optional updatingPercent As Integer = 0)
        BeginInvoke(New Action(Sub()
                                   VersionLabel.Text = "伺服器版本：" & versionType & " " & version & " " & buildVersion
                               End Sub))
    End Sub
    Overloads Sub SetVersionLabel(addtionText As String)
        BeginInvoke(New Action(Sub()
                                   VersionLabel.Text = "伺服器版本：" & versionType & " " & version & " " & buildVersion & " " & addtionText
                               End Sub))
    End Sub
    Sub CloseServer()
        Try
            dog.Close()
            dog = Nothing
        Catch ex As Exception
        End Try
        GenerateServerInfo()
    End Sub
    Sub SaveServer()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(serverDir, "server.properties"), "", False)
        Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(serverDir, "server.properties"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
        Dim optionDict As New Dictionary(Of Integer, Boolean)
        For i = 0 To ServerOptionBox.RowCount - 2
            writer.WriteLine(String.Format("{0}={1}", ToOptionName(i), GetOptionValue(i)))
        Next
        writer.WriteLine("server-ip" & "=" & ip)
        writer.WriteLine("server-port" & "=" & port)
        writer.WriteLine("generator-settings" & "=" & generator_settings)
        writer.WriteLine("level-name" & "=" & level_name)
        writer.WriteLine("level-seed" & "=" & level_seed)
        writer.WriteLine("level-type" & "=" & level_type)

        writer.WriteLine()
        writer.Flush()
        writer.Close()

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveServer()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Process.Start(serverDir)
    End Sub




    Private Sub GenerateServerInfo()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(serverDir, "server.info"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(serverDir, "server.info"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.WriteLine("server-version=" & version)
            writer.WriteLine("server-version-type=" & versionType)
            writer.WriteLine("nukkit-build-version=" & buildVersion)
            writer.Flush()
            writer.Close()
        End Using
    End Sub


End Class
