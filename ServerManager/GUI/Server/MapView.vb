Public Class MapView
    Friend _server As ServerBase
    Friend createMap As CreateMap

    Sub New(server As ServerBase)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _server = server
    End Sub
    Private Sub BrowseButton_Click(sender As Object, e As EventArgs) Handles BrowseButton.Click
        Dim mapChange As New MapChangeForm(Me)
        If mapChange Is Nothing Or mapChange.IsDisposed Then
            mapChange = New MapChangeForm(Me)
        End If
        If mapChange.ShowDialog() = DialogResult.OK Then
            If mapChange.ListBox1.SelectedIndex = 0 Then
                If mapChange.hasNewMap Then
                    MapNameLabel.Text = mapChange.newMap.Item1
                    _server.GetServerProperties.SetValue("level-name", mapChange.newMap.Item1)
                    _server.GetServerProperties.SetValue("level-seed", mapChange.newMap.Item2)
                    _server.GetServerProperties.SetValue("generator-settings", mapChange.newMap.Item4)
                    If TypeOf _server Is NukkitServer Then
                        _server.GetServerProperties.SetValue("level-type", CType(mapChange.newMap.Item3, Bedrock_Level_Type).ToString)
                    ElseIf TypeOf _server Is VanillaServer Then
                        _server.GetServerProperties.SetValue("level-type", CType(mapChange.newMap.Item3, Java_Level_Type).ToString)
                    ElseIf TypeOf _server Is BDSServer Then
                        Select Case CType(mapChange.newMap.Item3, Bedrock_Level_Type)
                            Case Bedrock_Level_Type.FLAT
                                _server.GetServerProperties.SetValue("level-type", "FLAT")
                            Case Bedrock_Level_Type.OLD
                                _server.GetServerProperties.SetValue("level-type", "LEGACY")
                            Case Bedrock_Level_Type.INFINITE
                                _server.GetServerProperties.SetValue("level-type", "DEFAULT")
                        End Select
                    End If
                Else
                    _server.GetServerProperties.SetValue("level-name", mapChange.mapList(0))
                End If
            Else
                If mapChange.hasNewMap Then
                    _server.GetServerProperties.SetValue("level-name", mapChange.mapList(mapChange.ListBox1.SelectedIndex - 1))
                Else
                    _server.GetServerProperties.SetValue("level-name", mapChange.mapList(mapChange.ListBox1.SelectedIndex))
                End If
            End If
            If mapChange.newMap.Item1.Trim <> "" Then
                Try
                    If TypeOf _server Is VanillaServer AndAlso TypeOf _server IsNot NukkitServer Then
                        ChangeIcon(IO.Path.Combine(_server.ServerPath, mapChange.newMap.Item1))
                    Else
                        ChangeIcon(IO.Path.Combine(_server.ServerPath, "\world\", mapChange.newMap.Item1))
                    End If
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub
    Sub ChooseMap(path As String, Optional create As Boolean = False)
        Dim info As New IO.DirectoryInfo(path)
        If _server.ServerPath <> info.Parent.FullName Then
            Dim newPath As String
            If TypeOf _server Is NukkitServer OrElse
               TypeOf _server Is BDSServer Then
                newPath = IO.Path.Combine(_server.ServerPath, "\worlds\" & info.Name)
            Else
                newPath = IO.Path.Combine(_server.ServerPath, info.Name)
            End If
            If My.Computer.FileSystem.DirectoryExists(newPath) Then
                Select Case MsgBox("已經存在使用此名稱的地圖，要覆寫嗎？", vbYesNoCancel, Application.ProductName)
                    Case MsgBoxResult.Yes
                        If create = False Then My.Computer.FileSystem.CopyDirectory(info.FullName, newPath, True)
                        ChangeMap(info.Name, True)
                    Case MsgBoxResult.No
                        Dim isNewDirectory As Boolean = False
                        Dim newName As String = ""
                        Do
                            newName = InputBox("此地圖的新名稱？", Application.ProductName, info.Name & " (1)")
                            If newName = "" Then
                                Exit Sub
                            Else
                                isNewDirectory = Not (My.Computer.FileSystem.DirectoryExists(IO.Path.Combine(_server.ServerPath, newName)))
                            End If
                        Loop Until isNewDirectory
                        If create Then My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(_server.ServerPath, newName))
                        Try
                            My.Computer.FileSystem.CopyDirectory(info.FullName, IO.Path.Combine(_server.ServerPath, newName))
                        Catch ex As Exception
                        End Try
                        ChangeMap(newName)
                    Case MsgBoxResult.Cancel
                End Select
            Else
                If create Then My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(_server.ServerPath, newPath))
                Try
                    My.Computer.FileSystem.CopyDirectory(info.FullName, newPath)
                Catch ex As Exception
                End Try
                ChangeMap(info.Name)
            End If
        Else
            ChangeMap(info.Name)
        End If
    End Sub
    Private Sub ChangeMap(newName As String, Optional overwrite As Boolean = False)
        If FindForm.GetType = GetType(ServerSetter) Then
            CType(FindForm(), ServerSetter).serverOptions.SetValue("level-name", GetUnicodedText(newName))
        ElseIf FindForm.GetType = GetType(ServerCreateDialog) AndAlso CType(FindForm(), ServerCreateDialog).serverOptions IsNot Nothing Then
            CType(FindForm(), ServerCreateDialog).serverOptions.SetValue("level-name", GetUnicodedText(newName))
        Else
            _server.GetServerProperties.SetValue("level-name", GetUnicodedText(newName))
        End If
        Try
            ChangeIcon(IO.Path.Combine(_server.ServerPath, newName))
        Catch ex As Exception
        End Try
        MapNameLabel.Text = newName
        If overwrite = True Then
            If TypeOf _server Is IBukkit Then
                With My.Computer.FileSystem
                    If .DirectoryExists(IO.Path.Combine(_server.ServerPath, newName & "_nether")) Then
                        .DeleteDirectory(IO.Path.Combine(_server.ServerPath, newName & "_nether"), FileIO.DeleteDirectoryOption.DeleteAllContents)
                    End If
                    If .DirectoryExists(IO.Path.Combine(_server.ServerPath, newName & "_the_end")) Then
                        .DeleteDirectory(IO.Path.Combine(_server.ServerPath, newName & "_the_end"), FileIO.DeleteDirectoryOption.DeleteAllContents)
                    End If
                End With
            End If
        End If
    End Sub

    Private Sub CreateButton_Click(sender As Object, e As EventArgs)
        createMap = New CreateMap(_server)
        createMap.LevelSeedBox.Text = TryGetKey(_server, ("level-seed"))
        If createMap.ShowDialog() = DialogResult.OK Then
            Dim mapName = createMap.LevelNameBox.Text
            Dim _type As Java_Level_Type = [Enum].Parse(GetType(Java_Level_Type), createMap.LevelTypeBox.SelectedIndex)
            Try
                My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(_server.ServerPath, mapName))
                ChooseMap(IO.Path.Combine(_server.ServerPath, mapName))
            Catch ex As Exception
                ChangeMap(mapName, True)
            End Try
            If FindForm.GetType = GetType(ServerSetter) Then
                CType(FindForm(), ServerSetter).serverOptions.SetValue("level-seed", createMap.LevelSeedBox.Text)
            ElseIf FindForm.GetType = GetType(ServerCreateDialog) AndAlso CType(FindForm(), ServerCreateDialog).serverOptions IsNot Nothing Then
                CType(FindForm(), ServerCreateDialog).serverOptions.SetValue("level-seed", createMap.LevelSeedBox.Text)
            Else
                CType(FindForm(), ServerCreateDialog).server.GetServerProperties.SetValue("level-seed", createMap.LevelSeedBox.Text)
            End If
            If FindForm.GetType = GetType(ServerSetter) Then
                CType(FindForm(), ServerSetter).serverOptions.SetValue("level-type", _type.ToString.ToUpper)
            ElseIf FindForm.GetType = GetType(ServerCreateDialog) AndAlso CType(FindForm(), ServerCreateDialog).serverOptions IsNot Nothing Then
                CType(FindForm(), ServerCreateDialog).serverOptions.SetValue("level-type", _type.ToString.ToUpper)
            Else
                _server.GetServerProperties.SetValue("level-type", _type.ToString.ToUpper)
            End If
            If _type = Java_Level_Type.BUFFET OrElse (_type = Java_Level_Type.CUSTOMIZED AndAlso New Version(_server.ServerVersion) < New Version(1, 13)) OrElse (_type = Java_Level_Type.FLAT AndAlso New Version(_server.ServerVersion) >= New Version(1, 13)) Then
                If FindForm.GetType = GetType(ServerSetter) Then
                    CType(FindForm(), ServerSetter).serverOptions.SetValue("generator-settings", createMap.GeneratorSettingBox.Text)
                ElseIf FindForm.GetType = GetType(ServerCreateDialog) AndAlso CType(FindForm(), ServerCreateDialog).serverOptions IsNot Nothing Then
                    CType(FindForm(), ServerCreateDialog).serverOptions.SetValue("generator-settings", createMap.GeneratorSettingBox.Text)
                Else
                    _server.GetServerProperties.SetValue("generator-settings", createMap.GeneratorSettingBox.Text)
                End If
            Else
                If FindForm.GetType = GetType(ServerSetter) Then
                    CType(FindForm(), ServerSetter).serverOptions.SetValue("generator-settings", "")
                ElseIf FindForm.GetType = GetType(ServerCreateDialog) AndAlso CType(FindForm(), ServerCreateDialog).serverOptions IsNot Nothing Then
                    CType(FindForm(), ServerCreateDialog).serverOptions.SetValue("generator-settings", "")
                Else
                    _server.GetServerProperties.SetValue("generator-settings", "")
                End If
            End If
        Else
        End If
    End Sub

    Private Sub MapView_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim mapName As String
        Try
            mapName = GetDeUnicodedText(TryGetKey(_server, "level-name", "world"))
        Catch ex As Exception
            mapName = "world"
        End Try
        MapNameLabel.Text = mapName
        If mapName.Trim <> "" Then
            Try
                ChangeIcon(IO.Path.Combine(_server.ServerPath, mapName))
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub ChangeIcon(path As String)
        Try
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(path, "icon.png")) Then
                Try
                    IconBox.Image = Image.FromFile(IO.Path.Combine(path, "icon.png"))
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub


End Class
