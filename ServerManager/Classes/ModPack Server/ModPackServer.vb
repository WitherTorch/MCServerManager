Imports System.Threading

Public Class ModPackServer
    Enum ModPackType
        [Error]
        FeedTheBeast
        CurseForge
    End Enum
    Public Event Initallised()
    Public Event ServerInfoUpdated()
    Public Event ServerIconUpdated()
    Dim _isRunning As Boolean = False
    Public Property IsRunning As Boolean
        Get
            Return _isRunning
        End Get
        Set(value As Boolean)
            _isRunning = value
            RaiseEvent ServerInfoUpdated()
        End Set
    End Property
    Public ReadOnly Property IsInitallised As Boolean = False
    Public ReadOnly Property PackName As String
    Public ReadOnly Property PackType As ModPackType
    Public ReadOnly Property ServerPath As String
    Public ReadOnly Property ServerPathName As String
    Public Property ServerOptions As New Dictionary(Of String, String)
    Public ReadOnly Property ServerIcon As Image = New Bitmap(64, 64)
    Public Property ProcessID As Integer = 0
    Public _InternalJavaArguments As String = ""
    Property InternalJavaArguments As String
        Get
            Return _InternalJavaArguments
        End Get
        Friend Set(value As String)
            _InternalJavaArguments = value
        End Set
    End Property
    Public _ServerRunJAR As String = ""
    Public Property ServerRunJAR As String
        Get
            Return _ServerRunJAR
        End Get
        Friend Set(value As String)
            _ServerRunJAR = value
        End Set
    End Property

    Sub SetPackInfo(name As String, Type As ModPackType)
        _PackName = name
        _PackType = Type
    End Sub
    Sub SetPath(dir As String)
        _ServerPath = dir
    End Sub
    Private Sub New()
    End Sub
    Private Sub New(serverPath As String)
        _ServerPath = serverPath
    End Sub
    Shared Function CreateServer() As ModPackServer
        Return New ModPackServer
    End Function
    Shared Function GetServer(path As String) As ModPackServer
        If path <> "" Then
            Dim server As New ModPackServer(path)
            server._ServerPath = path
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(path, "server.info")) Then
                Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(path, "server.info"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                    Do Until reader.EndOfStream
                        Dim infoText As String = reader.ReadLine
                        Dim info = infoText.Split("=", 2, StringSplitOptions.None)
                        If info.Length >= 2 Then
                            Select Case info(0)
                                Case "pack-type"
                                    Select Case info(1).ToLower
                                        Case "feedthebeast"
                                            server._PackType = ModPackType.FeedTheBeast
                                        Case "curseforge"
                                            server._PackType = ModPackType.CurseForge
                                        Case Else
                                            server._PackType = ModPackType.Error
                                    End Select
                                Case "pack-name"
                                    server._PackName = info(1)
                                Case "internal-java-args"
                                    server._InternalJavaArguments = info(1)
                                Case "server-file"
                                    server._ServerRunJAR = info(1)
                            End Select
                        End If
                    Loop
                End Using
            End If
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(path, "server-icon.png")) Then
                server._ServerIcon = Image.FromFile(IO.Path.Combine(path, "server-icon.png"))
            Else
                server._ServerIcon = My.Resources.ServerDefaultIcon
            End If
            server._ServerPathName = New IO.DirectoryInfo(path).Name
            Return server
        Else
            Return Nothing
        End If
    End Function
    Friend Sub Initallise()
        Dim mainThread As New Thread(Sub()
                                         Try
                                             Dim serverOptions As New Dictionary(Of String, String)
                                             If My.Computer.FileSystem.FileExists(IO.Path.Combine(ServerPath, "server.properties")) Then
                                                 Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(ServerPath, "server.properties"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                                                     Dim optionDict As New Dictionary(Of Integer, Boolean)
                                                     Do Until reader.EndOfStream
                                                         Try
                                                             Dim optionText As String = reader.ReadLine
                                                             If optionText.StartsWith("#") = False Then
                                                                 Dim options = optionText.Split("=", 2, StringSplitOptions.None)
                                                                 If options.Count = 2 Then
                                                                     serverOptions.Add(options(0), options(1))
                                                                 ElseIf options.Count = 1 Then
                                                                     If options(0).Trim(" ") <> "" Then serverOptions.Add(options(0), "")
                                                                 ElseIf options.Count = 0 Then
                                                                 Else
                                                                 End If
                                                             End If
                                                         Catch ex As Exception
                                                             Continue Do
                                                         End Try
                                                     Loop
                                                 End Using
                                             End If
                                             _ServerOptions = serverOptions
                                         Catch fileException As IO.FileNotFoundException
                                         End Try
                                         _IsInitallised = True
                                         RaiseEvent Initallised()
                                     End Sub)
        mainThread.IsBackground = True
        mainThread.Start()
    End Sub
    Private Overloads Sub GenerateServerInfo()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(ServerPath, "server.info"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(ServerPath, "server.info"), IO.FileMode.Truncate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.AutoFlush = True
            writer.WriteLine("pack-name=" & PackName)
            writer.WriteLine("pack-type=" & PackType.ToString)
            writer.WriteLine("internal-java-args=" & InternalJavaArguments)
            writer.WriteLine("server-file=" & ServerRunJAR)
            writer.Flush()
            writer.Close()
        End Using
    End Sub
    Friend Sub SaveServer()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(ServerPath, "server.properties"), "", False)
        Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(ServerPath, "server.properties"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
        writer.WriteLine("# Minecraft server properties")
        writer.WriteLine("#" & Now.ToString("ddd MMM dd HH:mm:ss K yyyy", System.Globalization.CultureInfo.CurrentUICulture))
        For Each [option] In ServerOptions
            writer.WriteLine(String.Format("{0}={1}", [option].Key, [option].Value))
        Next
        writer.WriteLine()
        writer.Flush()
        writer.Close()
        GenerateServerInfo()
    End Sub
End Class
