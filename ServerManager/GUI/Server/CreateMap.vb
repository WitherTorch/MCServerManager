Imports System.ComponentModel

Public Class CreateMap
    Dim server As Server
    Friend mapping As GeneratorMapping
    Dim editMode As Boolean
    Dim editLvlName As String
    Dim editLvlType As Integer
    Dim editLvlSeed As Long
    Dim editLvlGen As String
    Sub New(server As Server, Optional editMode As Boolean = False, Optional editLevelName As String = "", Optional editLevelType As Integer = 0, Optional editLevelSeed As Long = 0, Optional editLevelGensettings As String = "")

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
        Me.editMode = editMode
        editLvlName = editLevelName
        editLvlType = editLevelType
        editLvlSeed = editLevelSeed
        editLvlGen = editLevelGensettings
    End Sub
    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        If LevelNameBox.Text.Trim(" ") <> "" Then
            If LevelTypeBox.SelectedIndex <> -1 Then
                DialogResult = DialogResult.OK
                Close()
            End If
        End If
    End Sub

    Private Sub LevelTypeBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LevelTypeBox.SelectedIndexChanged
        Dim version = server.ServerVersion
        Dim snapshotRegex As New System.Text.RegularExpressions.Regex("[0-9]{2}")
        If server.ServerVersionType = Server.EServerVersionType.Kettle Then version = "1.12.2"
        If version = "master" Then
            version = "100.100"
        ElseIf server.Server2ndVersion <> "" AndAlso
            snapshotRegex.IsMatch(server.Server2ndVersion) AndAlso
             snapshotRegex.Matches(server.Server2ndVersion).Count = 2 Then
            If (snapshotRegex.Matches(server.Server2ndVersion)(0).Value = 18 AndAlso
                snapshotRegex.Matches(server.Server2ndVersion)(1).Value >= 16) OrElse
                snapshotRegex.Matches(server.Server2ndVersion)(0).Value >= 19 Then '18w16a - 1.13 Starting Support Buffet World Type
                version = "1.13.9999" 'Fake Version,it means the server's version is 1.13 up
            End If
        End If
        If server.ServerType = Server.EServerType.Java Then
            If server.ServerVersionType = Server.EServerVersionType.Forge Then
                If New Version(version) >= New Version(1, 13) Then
                    If LevelTypeBox.SelectedIndex = Java_Level_Type.CUSTOMIZED Then
                        LevelTypeBox.SelectedIndex = Java_Level_Type.FLAT
                    End If
                Else
                    If LevelTypeBox.SelectedIndex = Java_Level_Type.BUFFET Then
                        LevelTypeBox.SelectedIndex = Java_Level_Type.DEFAULT
                    End If
                End If
            Else
                If New Version(version) >= New Version(1, 13) Then
                    If LevelTypeBox.SelectedIndex = Java_Level_Type.CUSTOMIZED Then
                        LevelTypeBox.SelectedIndex = Java_Level_Type.FLAT
                    End If
                    If (LevelTypeBox.SelectedIndex = Java_Level_Type.FLAT) OrElse (LevelTypeBox.SelectedIndex = Java_Level_Type.BUFFET) Then
                        GeneratorSettingBox.Enabled = True
                        Button1.Enabled = True
                    Else
                        GeneratorSettingBox.Enabled = False
                        Button1.Enabled = False
                    End If
                Else
                    If LevelTypeBox.SelectedIndex = Java_Level_Type.BUFFET Then
                        LevelTypeBox.SelectedIndex = Java_Level_Type.DEFAULT
                    End If
                    If LevelTypeBox.SelectedIndex = Java_Level_Type.CUSTOMIZED Then
                        GeneratorSettingBox.Enabled = True
                        Button1.Enabled = True
                    Else
                        GeneratorSettingBox.Enabled = False
                        Button1.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        mapping = New GeneratorMapping
        If mapping.ShowDialog() = DialogResult.OK Then
            GeneratorSettingBox.Text = GeneratorMapping.GetSolutionCode(mapping.ChooseBox.SelectedIndex)
        End If
    End Sub

    Private Sub CreateMap_Load(sender As Object, e As EventArgs) Handles Me.Load
        CreatedForm.Add(Me)
        ShowInTaskbar = MiniState = 0
        Select Case server.ServerType
            Case Server.EServerType.Java
                LevelTypeBox.Items.AddRange(New String() {"預設", "超平坦(1.13 後可配合生成器設置)", "巨大生態系", "巨大化世界", "自定義(配合生成器設置,1.13 後不可用)", "自選世界類型(1.13 後可用,需配合生成器設置,否則與預設相同)"})
            Case Server.EServerType.Bedrock
                LevelTypeBox.Items.AddRange(New String() {"無限", "超平坦(可配合生成器設置)", "舊世界"})
        End Select
        LevelTypeBox.SelectedIndex = 0
        Dim worldName As String = "world"
        If editMode Then
            worldName = editLvlName
            LevelSeedBox.Text = editLvlSeed
            LevelTypeBox.SelectedIndex = editLvlType
            GeneratorSettingBox.Text = editLvlGen
        Else
            If IO.Directory.Exists(IO.Path.Combine(server.ServerPath, worldName)) Then
                Dim i As UInteger = 1
                Do
                    i += 1
                    worldName = "world-" & i
                Loop Until (IO.Directory.Exists(IO.Path.Combine(server.ServerPath, worldName)))
            End If
            Randomize()
            LevelSeedBox.Text = New Random().Next(Integer.MaxValue)
        End If
        LevelNameBox.Text = worldName
        If server.ServerVersionType = Server.EServerVersionType.Forge Then
            GeneratorSettingBox.Enabled = True
            Button1.Enabled = True
        End If
    End Sub

    Private Sub CreateMap_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CreatedForm.Remove(Me)

    End Sub
End Class