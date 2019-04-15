Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class CreateJavaServerDialog
    Dim isBedrock As Boolean = False
    Friend _owner As Launcher
    Enum ServerIPType
        Float
        [Default]
        Custom
    End Enum
    Friend ipType As ServerIPType = ServerIPType.Default


    Public Sub New(owner As Launcher)
        _owner = owner

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        If ServerDirBox.Text = "" Then
            MsgBox("伺服器路徑不得為空!")
        Else
            If VersionBox.Text = "" Then
                MsgBox("伺服器版本不得為空!")
            Else
                Dim creator As New JavaServerCreator(Me, ServerDirBox.Text)
                creator.ShowDialog()
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ServerTypeBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ServerTypeBox.SelectedIndexChanged
        VersionBox.Items.Clear()

        Select Case ServerTypeBox.SelectedIndex
            Case 0 ' Vanilla
                VersionBox.Items.AddRange(_owner.VanillaVersionLists.ToArray)
            Case 1 ' Forge
                VersionBox.Items.AddRange(_owner.ForgeVersionLists.ToArray)
            Case 2 ' Spigot
                VersionBox.Items.AddRange(_owner.SpigotVersionLists.ToArray)
            Case 3 ' CraftBukkit
                VersionBox.Items.AddRange(_owner.CraftBukkitVersionLists.ToArray)
        End Select
    End Sub

    Private Sub CreateServerDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i = 0 To ServerOptionBox.RowCount - 1
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
        LevelNameBox.Text = "world"
        LevelTypeBox.SelectedIndex = 0
        ServerTypeBox.SelectedIndex = 0
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

            Case Else : Return -1
        End Select

    End Function
    Private Sub SetOptionValue(row As Integer, value As String)

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
    Function GetOptionValue(row As Integer) As String
        Try
            If row <> -1 Then
                Dim control As Windows.Forms.Control = ServerOptionBox.GetControlFromPosition("1", row)
                Select Case control.GetType
                    Case GetType(CustomComboBox)
                        Return CType(control, CustomComboBox).GetSelectedValue()
                    Case GetType(NumericUpDown)
                        Return CType(control, NumericUpDown).Value
                    Case GetType(TextBox)
                        Return CType(control, TextBox).Text
                End Select
            End If
        Catch ex As Exception
        End Try
    End Function
#End Region
    Private Sub ServerDirBrowseBtn_Click(sender As Object, e As EventArgs) Handles ServerDirBrowseBtn.Click
        If ChooseDirectoryDialog.ShowDialog() = DialogResult.OK Then
            ServerDirBox.Text = ChooseDirectoryDialog.SelectedPath()
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        IPBox.Text = ""
        IPBox.ReadOnly = True
        ipType = ServerIPType.Float
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        IPBox.Text = _owner.ip
        IPBox.ReadOnly = True
        ipType = ServerIPType.Default
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        IPBox.ReadOnly = False
        ipType = ServerIPType.Custom
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
