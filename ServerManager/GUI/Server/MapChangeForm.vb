Imports System.Threading.Tasks

Public Class MapChangeForm
    Dim mapList As New List(Of String)
    Dim netherList As New Dictionary(Of String, String)
    Dim theEndList As New Dictionary(Of String, String)
    Dim server As Server
    Sub New(server As Server)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
    End Sub
    Private Sub MapChangeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SearchMaps()
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
            For Each subDirInfo In dirInfo.GetDirectories
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
            Next
        End If
    End Sub
    Sub ConvertBukkitMapToVanillaMap(mapDirInfo As IO.DirectoryInfo)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        BeginInvokeIfRequired(Me, Sub()
                                      IO.Directory.Delete(mapList(ListBox1.SelectedIndex))
                                      If netherList.ContainsKey(mapList(ListBox1.SelectedIndex)) Then
                                          IO.Directory.Delete(netherList.ContainsKey(mapList(ListBox1.SelectedIndex)))
                                          netherList.Remove(mapList(ListBox1.SelectedIndex))
                                      End If
                                      If theEndList.ContainsKey(mapList(ListBox1.SelectedIndex)) Then
                                          IO.Directory.Delete(theEndList.ContainsKey(mapList(ListBox1.SelectedIndex)))
                                          theEndList.Remove(mapList(ListBox1.SelectedIndex))
                                      End If
                                      ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
                                      mapList.RemoveAt(ListBox1.SelectedIndex)
                                  End Sub)
    End Sub
End Class