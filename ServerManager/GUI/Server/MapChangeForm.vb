Imports System.Threading.Tasks

Public Class MapChangeForm
    Friend mapList As New List(Of String)
    Friend newMap As (String, Long, Integer, String)
    Friend hasNewMap As Boolean
    Dim netherList As New Dictionary(Of String, String)
    Dim theEndList As New Dictionary(Of String, String)
    Dim isLoaded As Boolean = False
    Dim server As Server
    Friend view As MapView
    Sub New(mapView As MapView)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = mapView._server
        view = mapView
    End Sub
    Overloads Function SafeGetOption(optionName As String, defaultResult As Long) As Long
        Try
            Return server.ServerOptions(optionName)
        Catch ex As Exception
            Return defaultResult
        End Try
    End Function
    Overloads Function SafeGetOption(optionName As String, defaultResult As String) As String
        Try
            Return server.ServerOptions(optionName)
        Catch ex As Exception
            Return defaultResult
        End Try
    End Function
    Overloads Function SafeGetOption(optionName As String, defaultResult As Integer) As Integer
        Try
            Return server.ServerOptions(optionName)
        Catch ex As Exception
            Return defaultResult
        End Try
    End Function
    Private Sub MapChangeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SearchMaps()
        Task.Run(Sub()
                     Do Until isLoaded
                     Loop
                     If mapList.Contains(view.MapNameLabel.Text) Then
                         BeginInvokeIfRequired(Me, Sub() ListBox1.SelectedIndex = mapList.IndexOf(view.MapNameLabel.Text))
                     Else
                         BeginInvokeIfRequired(Me, Sub() ListBox1.Items.Insert(0, String.Format("{0} ({1} *)", view.MapNameLabel.Text, view.MapNameLabel.Text)))
                         Select Case server.ServerType
                             Case Server.EServerType.Java
                                 newMap = (view.MapNameLabel.Text, SafeGetOption("level-seed", New Random().Next(Integer.MaxValue)), CInt([Enum].Parse(GetType(Java_Level_Type), SafeGetOption("level-type", "DEFAULT").ToUpper)), SafeGetOption("generator-settings", ""))
                                 hasNewMap = True
                             Case Server.EServerType.Bedrock
                                 Select Case server.ServerVersionType
                                     Case Server.EServerVersionType.Nukkit
                                         newMap = (view.MapNameLabel.Text, SafeGetOption("level-seed", New Random().Next(Integer.MaxValue)), CInt([Enum].Parse(GetType(Bedrock_Level_Type), SafeGetOption("level-type", "INFINITE").ToUpper)), SafeGetOption("generator-settings", ""))
                                         hasNewMap = True
                                     Case Server.EServerVersionType.VanillaBedrock
                                         Dim _Level_Type As Bedrock_Level_Type
                                         Select Case SafeGetOption("level-type", "DEFAULT").ToUpper
                                             Case "FLAT"
                                                 _Level_Type = Bedrock_Level_Type.FLAT
                                             Case "LEGACY"
                                                 _Level_Type = Bedrock_Level_Type.OLD
                                             Case "DEFAULT"
                                                 _Level_Type = Bedrock_Level_Type.INFINITE
                                         End Select
                                         newMap = (view.MapNameLabel.Text, SafeGetOption("level-seed", New Random().Next(Integer.MaxValue)), _Level_Type, SafeGetOption("generator-settings", ""))
                                         hasNewMap = True
                                 End Select
                         End Select
                     End If
                 End Sub)
    End Sub
    Sub SearchMaps()
        Dim pathSeperator As String = ""
        If IsUnixLikeSystem Then
            pathSeperator = "/"
        Else
            pathSeperator = "\"
        End If
        Dim mapPathBase As String
        If server IsNot Nothing Then
            If server.ServerType = Server.EServerType.Bedrock Then
                mapPathBase = server.ServerPath.TrimEnd(pathSeperator) & pathSeperator & "worlds" & pathSeperator
            Else
                mapPathBase = server.ServerPath.TrimEnd(pathSeperator)
            End If
            Dim dirInfo As New IO.DirectoryInfo(mapPathBase)
            If dirInfo.Exists Then
                Task.Run(Sub()
                             For Each subDirInfo In dirInfo.GetDirectories
                                 For Each fileInfo In subDirInfo.GetFiles
                                     If fileInfo.Name = "level.dat" Then
                                         Try
                                             Dim nbt As New NBT.IO.NBTFile()
                                             nbt.Load(New IO.FileStream(fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read))
                                             Dim levelName As String = nbt.RootTag.Item("Data").Item("LevelName").Value
                                             BeginInvokeIfRequired(Me, Sub()
                                                                           Dim lName As String = levelName
                                                                           If IO.Directory.Exists(subDirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & "DIM1") _
                                                                           AndAlso subDirInfo.Name.EndsWith("_nether") _
                                                                           AndAlso IO.Directory.Exists(dirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & subDirInfo.Name.Substring(0, subDirInfo.Name.Length - 7)) Then
                                                                               netherList.Add(dirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & subDirInfo.Name.Substring(0, subDirInfo.Name.Length - 7), subDirInfo.FullName)
                                                                           ElseIf IO.Directory.Exists(subDirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & "DIM-1") _
                                                                           AndAlso subDirInfo.Name.EndsWith("_the_end") _
                                                                           AndAlso IO.Directory.Exists(dirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & subDirInfo.Name.Substring(0, subDirInfo.Name.Length - 8)) Then
                                                                               theEndList.Add(dirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & subDirInfo.Name.Substring(0, subDirInfo.Name.Length - 7), subDirInfo.FullName)
                                                                           Else
                                                                               ListBox1.Items.Add(String.Format("{0} ({1})", lName, subDirInfo.Name))
                                                                           End If
                                                                       End Sub)
                                             mapList.Add(subDirInfo.Name)
                                             Exit For
                                         Catch ex As Exception
                                             Exit For
                                         End Try
                                     End If
                                 Next
                             Next
                             isLoaded = True
                         End Sub)
            End If
        End If
    End Sub
    Sub ConvertBukkitMapToVanillaMap(mapDirInfo As IO.DirectoryInfo)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        BeginInvokeIfRequired(Me, Sub()
                                      Dim index = ListBox1.SelectedIndex
                                      If hasNewMap Then
                                          index -= 1
                                      End If
                                      If index >= 0 Then
                                          Try
                                              IO.Directory.Delete(mapList(index))
                                              If netherList.ContainsKey(mapList(index)) Then
                                                  IO.Directory.Delete(netherList.ContainsKey(mapList(index)))
                                                  netherList.Remove(mapList(index))
                                              End If
                                              If theEndList.ContainsKey(mapList(index)) Then
                                                  IO.Directory.Delete(theEndList.ContainsKey(mapList(index)))
                                                  theEndList.Remove(mapList(index))
                                              End If
                                              BeginInvokeIfRequired(Me, Sub() ListBox1.Items.RemoveAt(ListBox1.SelectedIndex))
                                              mapList.RemoveAt(index)
                                          Catch ex As Exception

                                          End Try
                                      End If
                                  End Sub)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim pathSeperator As String = ""
        If IsUnixLikeSystem Then
            pathSeperator = "/"
        Else
            pathSeperator = "\"
        End If
        Static open As New FolderBrowserDialog
        If open Is Nothing Then open = New FolderBrowserDialog
        If open.ShowDialog = DialogResult.OK Then
            If server IsNot Nothing Then
                Dim mapPathBase As String = ""
                If server.ServerType = Server.EServerType.Bedrock Then
                    mapPathBase = server.ServerPath.TrimEnd(pathSeperator) & pathSeperator & "worlds" & pathSeperator
                Else
                    mapPathBase = server.ServerPath.TrimEnd(pathSeperator)
                End If
                My.Computer.FileSystem.CopyDirectory(open.SelectedPath, mapPathBase & pathSeperator & New IO.DirectoryInfo(open.SelectedPath).Name, True)
                Dim dirInfo As New IO.DirectoryInfo(mapPathBase)
                Dim subDirInfo As New IO.DirectoryInfo(mapPathBase & pathSeperator & New IO.DirectoryInfo(open.SelectedPath).Name)
                Task.Run(Sub()
                             For Each fileInfo In subDirInfo.GetFiles
                                 If fileInfo.Name = "level.dat" Then
                                     Try
                                         Dim nbt As New NBT.IO.NBTFile()
                                         nbt.Load(New IO.FileStream(fileInfo.FullName, IO.FileMode.Open, IO.FileAccess.Read))
                                         Dim levelName As String = nbt.RootTag.Item("Data").Item("LevelName").Value
                                         BeginInvokeIfRequired(Me, Sub()
                                                                       Dim lName As String = levelName
                                                                       If IO.Directory.Exists(subDirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & "DIM1") _
                                                                           AndAlso subDirInfo.Name.EndsWith("_nether") _
                                                                           AndAlso IO.Directory.Exists(dirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & subDirInfo.Name.Substring(0, subDirInfo.Name.Length - 7)) Then
                                                                           netherList.Add(dirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & subDirInfo.Name.Substring(0, subDirInfo.Name.Length - 7), subDirInfo.FullName)
                                                                       ElseIf IO.Directory.Exists(subDirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & "DIM-1") _
                                                                           AndAlso subDirInfo.Name.EndsWith("_the_end") _
                                                                           AndAlso IO.Directory.Exists(dirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & subDirInfo.Name.Substring(0, subDirInfo.Name.Length - 8)) Then
                                                                           theEndList.Add(dirInfo.FullName.TrimEnd(pathSeperator) & pathSeperator & subDirInfo.Name.Substring(0, subDirInfo.Name.Length - 7), subDirInfo.FullName)
                                                                       Else
                                                                           ListBox1.Items.Add(String.Format("{0} ({1})", lName, subDirInfo.Name))
                                                                           mapList.Add(subDirInfo.Name)
                                                                       End If
                                                                   End Sub)
                                         Exit For
                                     Catch ex As Exception
                                         Exit For
                                     End Try
                                 End If
                             Next
                         End Sub)
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim mapCreateForm As CreateMap
        If hasNewMap Then
            mapCreateForm = New CreateMap(server, True, newMap.Item1, newMap.Item3, newMap.Item2, newMap.Item4)
        Else
            mapCreateForm = New CreateMap(server)
        End If
        If mapCreateForm.ShowDialog = DialogResult.OK Then
            Try
                If hasNewMap Then
                    ListBox1.Items(0) = String.Format("{0} ({1} *)", mapCreateForm.LevelNameBox.Text, mapCreateForm.LevelNameBox.Text)
                Else
                    ListBox1.Items.Insert(0, String.Format("{0} ({1} *)", mapCreateForm.LevelNameBox.Text, mapCreateForm.LevelNameBox.Text))
                End If
                newMap = (mapCreateForm.LevelNameBox.Text, mapCreateForm.LevelSeedBox.Text, mapCreateForm.LevelTypeBox.SelectedIndex, mapCreateForm.GeneratorSettingBox.Text)
            Catch ex As Exception
            End Try
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex = -1 Then
            MsgBox("必須選擇一張地圖!", , Text)
        Else
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class