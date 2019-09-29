Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class ServerCreateHelper
    Dim server As ServerBase
    Dim client As New Net.WebClient()
    Dim path As String
    Dim forgeVer As String
    Dim downloader As ForgeUpdater
    Dim vanilla_isSnap As Boolean = False
    Dim vanilla_isPre As Boolean = False
    Dim justDownload As Boolean
    Public Sub New(server As ServerBase, serverPath As String, Optional justDownload As Boolean = False)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
        path = serverPath
        Me.justDownload = justDownload
    End Sub
    Public Sub New(server As ServerBase, serverPath As String, targetForgeVersion As String, Optional justDownload As Boolean = False)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
        path = serverPath
        forgeVer = targetForgeVersion
        Me.justDownload = justDownload
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If downloader IsNot Nothing Then downloader.ForceClose()
        client.CancelAsync()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ServerCreateHelper_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Computer.FileSystem.DirectoryExists(path) = False Then
            BeginInvoke(New Action(Sub()
                                       StatusLabel.Text = "狀態：建立目錄中……"
                                       ProgressBar.Value = 0
                                   End Sub))
            My.Computer.FileSystem.CreateDirectory(path)



        End If
        Task.Run(New Action(Sub()
                                DownloadServer()
                            End Sub))





    End Sub
    Sub DownloadServer()
        Dim seperator As String = IIf(IsUnixLikeSystem, "/", "\")
        AddHandler server.ServerDownloadEnd, Sub()
                                                 GlobalModule.Manager.AddServer()
                                             End Sub
    End Sub

    Sub GenerateServerEULA()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(path, "eula.txt"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(path, "eula.txt"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.WriteLine("#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).")
            writer.WriteLine("#" & Now.ToString("ddd MMM dd HH:mm:ss K yyyy"), System.Globalization.CultureInfo.CurrentUICulture)
            writer.WriteLine("eula=true")
            writer.Flush()
            writer.Close()
        End Using
    End Sub
End Class
