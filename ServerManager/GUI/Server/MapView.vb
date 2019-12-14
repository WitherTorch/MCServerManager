Public Class MapView
    Friend _server As Server
    Friend createMap As CreateMap

    Sub New(server As Server)

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
                    If _server.ServerOptions.ContainsKey("level-name") Then
                        _server.ServerOptions("level-name") = mapChange.newMap.Item1
                    Else
                        _server.ServerOptions.Add("level-name", mapChange.newMap.Item1)
                    End If
                    If _server.ServerOptions.ContainsKey("level-seed") Then
                        _server.ServerOptions("level-seed") = mapChange.newMap.Item2
                    Else
                        _server.ServerOptions.Add("level-seed", mapChange.newMap.Item2)
                    End If
                    If _server.ServerOptions.ContainsKey("generator-settings") Then
                        _server.ServerOptions("generator-settings") = mapChange.newMap.Item4
                    Else
                        _server.ServerOptions.Add("generator-settings", mapChange.newMap.Item4)
                    End If
                    If _server.ServerOptions.ContainsKey("level-type") Then
                        Select Case _server.ServerType
                            Case Server.EServerType.Java
                                _server.ServerOptions("level-type") = CType(mapChange.newMap.Item3, Java_Level_Type).ToString
                            Case Server.EServerType.Bedrock
                                Select Case _server.ServerVersionType
                                    Case Server.EServerVersionType.Nukkit
                                        _server.ServerOptions("level-type") = CType(mapChange.newMap.Item3, Bedrock_Level_Type).ToString
                                    Case Server.EServerVersionType.VanillaBedrock
                                        Select Case CType(mapChange.newMap.Item3, Bedrock_Level_Type)
                                            Case Bedrock_Level_Type.FLAT
                                                _server.ServerOptions("level-type") = "FLAT"
                                            Case Bedrock_Level_Type.OLD
                                                _server.ServerOptions("level-type") = "LEGACY"
                                            Case Bedrock_Level_Type.INFINITE
                                                _server.ServerOptions("level-type") = "DEFAULT"
                                        End Select
                                End Select
                        End Select
                    Else
                        Select Case _server.ServerType
                            Case Server.EServerType.Java
                                _server.ServerOptions.Add("level-type", CType(mapChange.newMap.Item3, Java_Level_Type).ToString)
                            Case Server.EServerType.Bedrock
                                Select Case _server.ServerVersionType
                                    Case Server.EServerVersionType.Nukkit
                                        _server.ServerOptions.Add("level-type", CType(mapChange.newMap.Item3, Bedrock_Level_Type).ToString)
                                    Case Server.EServerVersionType.VanillaBedrock
                                        Select Case CType(mapChange.newMap.Item3, Bedrock_Level_Type)
                                            Case Bedrock_Level_Type.FLAT
                                                _server.ServerOptions.Add("level-type", "FLAT")
                                            Case Bedrock_Level_Type.OLD
                                                _server.ServerOptions.Add("level-type", "LEGACY")
                                            Case Bedrock_Level_Type.INFINITE
                                                _server.ServerOptions.Add("level-type", "DEFAULT")
                                        End Select
                                End Select
                        End Select
                    End If
                Else
                    If _server.ServerOptions.ContainsKey("level-name") Then
                        _server.ServerOptions("level-name") = mapChange.mapList(0)
                    Else
                        _server.ServerOptions.Add("level-name", mapChange.mapList(0))
                    End If
                End If
            Else
                If _server.ServerOptions.ContainsKey("level-name") Then
                    If mapChange.hasNewMap Then
                        _server.ServerOptions("level-name") = mapChange.mapList(mapChange.ListBox1.SelectedIndex - 1)
                    Else
                        _server.ServerOptions("level-name") = mapChange.mapList(mapChange.ListBox1.SelectedIndex)
                    End If
                Else
                    If mapChange.hasNewMap Then
                        _server.ServerOptions.Add("level-name", mapChange.mapList(mapChange.ListBox1.SelectedIndex - 1))
                    Else
                        _server.ServerOptions.Add("level-name", mapChange.mapList(mapChange.ListBox1.SelectedIndex))
                    End If
                End If
            End If
            If mapChange.newMap.Item1.Trim <> "" Then
                Try
                    Select Case _server.ServerType
                        Case Server.EServerType.Java
                            ChangeIcon(IO.Path.Combine(_server.ServerPath, mapChange.newMap.Item1))
                        Case Server.EServerType.Bedrock
                            ChangeIcon(IO.Path.Combine(_server.ServerPath, "\world\", mapChange.newMap.Item1))
                    End Select
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub
    Sub ChooseMap(path As String, Optional create As Boolean = False)
        Dim info As New IO.DirectoryInfo(path)
        If _server.ServerPath <> info.Parent.FullName Then
            Dim newPath As String
            If _server.ServerVersionType = Server.EServerVersionType.Nukkit OrElse
                _server.ServerVersionType = Server.EServerVersionType.VanillaBedrock Then
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
            _server.AddOrSetOption("level-name", GetUnicodedText(newName))
        End If
            Try
            ChangeIcon(IO.Path.Combine(_server.ServerPath, newName))
        Catch ex As Exception
        End Try
        MapNameLabel.Text = newName
        If overwrite = True Then
            If (_server.ServerVersionType = Server.EServerVersionType.Spigot) OrElse
                    (_server.ServerVersionType = Server.EServerVersionType.CraftBukkit) OrElse
                    (_server.ServerVersionType = Server.EServerVersionType.Paper) OrElse
                (_server.ServerVersionType = Server.EServerVersionType.Akarin) Then
                With My.Computer.FileSystem
                    If .DirectoryExists(IO.Path.Combine(_server.ServerVersionType, newName & "_nether")) Then
                        .DeleteDirectory(IO.Path.Combine(_server.ServerVersionType, newName & "_nether"), FileIO.DeleteDirectoryOption.DeleteAllContents)
                    End If
                    If .DirectoryExists(IO.Path.Combine(_server.ServerVersionType, newName & "_the_end")) Then
                        .DeleteDirectory(IO.Path.Combine(_server.ServerVersionType, newName & "_the_end"), FileIO.DeleteDirectoryOption.DeleteAllContents)
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
                CType(FindForm(), ServerCreateDialog).server.AddOrSetOption("level-seed", createMap.LevelSeedBox.Text)
            End If
            If FindForm.GetType = GetType(ServerSetter) Then
                CType(FindForm(), ServerSetter).serverOptions.SetValue("level-type", _type.ToString.ToUpper)
            ElseIf FindForm.GetType = GetType(ServerCreateDialog) AndAlso CType(FindForm(), ServerCreateDialog).serverOptions IsNot Nothing Then
                CType(FindForm(), ServerCreateDialog).serverOptions.SetValue("level-type", _type.ToString.ToUpper)
            Else
                _server.AddOrSetOption("level-type", _type.ToString.ToUpper)
            End If
            If _type = Java_Level_Type.BUFFET OrElse (_type = Java_Level_Type.CUSTOMIZED AndAlso New Version(_server.ServerVersion) < New Version(1, 13)) OrElse (_type = Java_Level_Type.FLAT AndAlso New Version(_server.ServerVersion) >= New Version(1, 13)) Then
                If FindForm.GetType = GetType(ServerSetter) Then
                    CType(FindForm(), ServerSetter).serverOptions.SetValue("generator-settings", createMap.GeneratorSettingBox.Text)
                ElseIf FindForm.GetType = GetType(ServerCreateDialog) AndAlso CType(FindForm(), ServerCreateDialog).serverOptions IsNot Nothing Then
                    CType(FindForm(), ServerCreateDialog).serverOptions.SetValue("generator-settings", createMap.GeneratorSettingBox.Text)
                Else
                    _server.AddOrSetOption("generator-settings", createMap.GeneratorSettingBox.Text)
                End If
            Else
                If FindForm.GetType = GetType(ServerSetter) Then
                    CType(FindForm(), ServerSetter).serverOptions.SetValue("generator-settings", "")
                ElseIf FindForm.GetType = GetType(ServerCreateDialog) AndAlso CType(FindForm(), ServerCreateDialog).serverOptions IsNot Nothing Then
                    CType(FindForm(), ServerCreateDialog).serverOptions.SetValue("generator-settings", "")
                Else
                    _server.AddOrSetOption("generator-settings", "")
                End If
            End If
        Else
        End If
    End Sub

    Private Sub MapView_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _server.ServerType <> Server.EServerType.Java OrElse _server.ServerType <> Server.EServerType.Bedrock Then

        End If
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
