Imports System.ComponentModel

Friend Class JavaChangeMapForm
    Private _server As JavaServer

    Sub New(ByRef server As JavaServer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _server = server
    End Sub
    Private Sub ChangeMapForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ChangeMapForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ChooseMapButton_Click(sender As Object, e As EventArgs) Handles ChooseMapButton.Click
        Using openDirDialog As New FolderBrowserDialog
            openDirDialog.Description = "選擇地圖的資料夾"
            openDirDialog.ShowNewFolderButton = False
            If openDirDialog.ShowDialog = DialogResult.OK Then
                ChooseMap(openDirDialog.SelectedPath)
            End If
        End Using
    End Sub
    Sub ChooseMap(path As String, Optional create As Boolean = False)
        Dim info As New IO.DirectoryInfo(path)
        If _server.serverDir <> info.Parent.FullName Then
            Dim newPath As String = IO.Path.Combine(_server.serverDir, info.Name)
            If My.Computer.FileSystem.DirectoryExists(newPath) Then
                Select Case MsgBox("已經存在使用此名稱的地圖，要覆寫嗎？", vbYesNoCancel)
                    Case MsgBoxResult.Yes
                        If create = False Then My.Computer.FileSystem.CopyDirectory(info.FullName, newPath, True)
                        ChangeMap(info.Name, True)
                    Case MsgBoxResult.No
                        Dim isNewDirectory As Boolean = False
                        Dim newName As String = ""
                        Do
                            newName = InputBox("此地圖的新名稱？",, info.Name & " (1)")
                            If newName = "" Then
                                DialogResult = DialogResult.Cancel
                                Exit Sub
                            Else
                                isNewDirectory = Not (My.Computer.FileSystem.DirectoryExists(IO.Path.Combine(_server.serverDir, newName)))
                            End If
                        Loop Until isNewDirectory
                        If create Then My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(_server.serverDir, newName))
                        My.Computer.FileSystem.CopyDirectory(info.FullName, IO.Path.Combine(_server.serverDir, newName))
                        ChangeMap(newName)
                    Case MsgBoxResult.Cancel
                        DialogResult = DialogResult.Cancel
                End Select
            Else
                If create Then My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(_server.serverDir, newPath))
                My.Computer.FileSystem.CopyDirectory(info.FullName, newPath)
                ChangeMap(info.Name)
            End If
        Else
            ChangeMap(info.Name)
        End If
    End Sub
    Private Sub ChangeMap(newName As String, Optional overwrite As Boolean = False)
        _server.level_name = GetUnicodedText(newName)
        If overwrite = True Then
            If (_server.versionType = "Spigot") OrElse (_server.versionType = "CraftBukkit") Then
                With My.Computer.FileSystem
                    If .DirectoryExists(IO.Path.Combine(_server.serverDir, newName & "_nether")) Then
                        .DeleteDirectory(IO.Path.Combine(_server.serverDir, newName & "_nether"), FileIO.DeleteDirectoryOption.DeleteAllContents)
                    End If
                    If .DirectoryExists(IO.Path.Combine(_server.serverDir, newName & "_the_end")) Then
                        .DeleteDirectory(IO.Path.Combine(_server.serverDir, newName & "_the_end"), FileIO.DeleteDirectoryOption.DeleteAllContents)
                    End If
                End With
            End If
        End If
        DialogResult = DialogResult.OK
        _server.SaveServer()
        Close()
    End Sub

    Private Sub CreateNewMap_Click(sender As Object, e As EventArgs) Handles CreateNewMap.Click
        Dim mapName As String = InputBox("地圖名稱？")
        If mapName <> "" Then
            Dim seed As String = ""
            Select Case MsgBox("要指定種子嗎？", vbYesNo)
                Case MsgBoxResult.Yes
                    If _server.level_seed <> "" Then
                        If MsgBox("發現舊有種子，要建立新的嗎？", vbYesNo) = MsgBoxResult.No Then
                            seed = _server.level_seed
                        End If
                    End If
                    seed = InputBox("指定新的種子",, New Random().Next(Int32.MinValue, Int32.MaxValue))
                Case MsgBoxResult.No
                    seed = ""
            End Select
            My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(_server.serverDir, mapName))
            ChooseMap(IO.Path.Combine(_server.serverDir, mapName))
            _server.level_seed = seed
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub
End Class