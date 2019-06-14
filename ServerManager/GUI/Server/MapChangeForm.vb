Imports System.Threading.Tasks

Public Class MapChangeForm
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
                                                                       ListBox1.Items.Add(String.Format("{0} ({1})", lName, subDirInfo.Name))
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
    Sub ConvertVanillaMapToBukkitMap(mapName As IO.DirectoryInfo)

    End Sub
End Class