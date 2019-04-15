Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class CreateBedrockServerDialog




    Dim isBedrock As Boolean = False
    Friend _owner As Launcher
    Enum ServerIPType
        UPnP
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
                Dim creator As New BedrockServerCreator(Me, ServerDirBox.Text)
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
            Case 0 ' Nukkit
                VersionBox.Items.Add("build " & _owner.NukkitVersion)
        End Select
    End Sub

        Private Sub CreateServerDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Case "view-distance" : Return 16
            Case "white-list" : Return 17

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
            Case 16 : Return "view-distance"
            Case 17 : Return "white-list"
            Case Else : Return ""
        End Select
    End Function
    Friend Function GetOptionValue(row As Integer) As String

        If row <> -1 Then
                Dim control As Windows.Forms.Control = ServerOptionBox.GetControlFromPosition(1, row)
            MsgBox(row)
            Select Case control.GetType
                    Case GetType(CustomComboBox)
                        Return CType(control, CustomComboBox).GetSelectedValue()
                    Case GetType(NumericUpDown)
                        Return CType(control, NumericUpDown).Value
                    Case GetType(TextBox)
                        Return CType(control, TextBox).Text
                End Select
            End If

    End Function
#End Region

    Private Sub ServerDirBrowseBtn_Click(sender As Object, e As EventArgs) Handles ServerDirBrowseBtn.Click
            If ChooseDirectoryDialog.ShowDialog() = DialogResult.OK Then
                ServerDirBox.Text = ChooseDirectoryDialog.SelectedPath()
            End If
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


End Class
