Imports System.Threading

Public Class ForgeModExplorer
    Dim engine As CharcoalEngine
    Dim spongeThread As Thread
    Friend _server As Server
    Friend index As Integer


    Sub New(index As Integer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.index = index
        Me._server = GlobalModule.Manager.ServerEntityList(index)
        engine = New CharcoalEngine(index)
    End Sub

    Private Sub BukkitForm_Load(sender As Object, e As EventArgs) Handles Me.Load

        AddHandler engine.DownloadProgressChanged, Sub(obj, args)
                                                       ToolStripProgressBar1.Value = args.ProgressPercentage
                                                   End Sub
        GoHome()
    End Sub
    Sub GoHome()

    End Sub


    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        engine.LoadPage("https://www.curseforge.com/minecraft/mc-mods/server-utility", CharcoalEngine.PluginPageType.CurseForge_ModListPage, CharcoalEnginePanel)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If spongeThread IsNot Nothing AndAlso spongeThread.IsAlive Then
            spongeThread.Abort()
        End If
        spongeThread = New Thread(Sub()
                                      Dim sponge As New SpongeForgeProvider
                                      sponge.Initallise()
                                      Dim versions = sponge.GetSpongeForgeVersionsOnBranch(_server.ServerVersion, _server.Server2ndVersion.Split(".").Last)
                                      Dim versionListBox As New ListView With {.View = View.List, .Dock = DockStyle.Fill}
                                      BeginInvoke(Sub() CharcoalEnginePanel.Controls.Add(versionListBox))
                                      For Each version In versions
                                          Dim spongeVer As Version = version.SpongeVersion
                                          BeginInvoke(Sub() versionListBox.Items.Add(spongeVer.Major & "." & spongeVer.Minor & "." & spongeVer.Build & " " & version.SpongeVersionType.ToString.ToUpper & " " & version.Build))
                                      Next
                                      AddHandler versionListBox.ItemActivate, Sub()
                                                                                  My.Computer.Network.DownloadFile(versions(versionListBox.SelectedIndices(0)).GetDownloadUrl, IO.Path.Combine(_server.ServerPath, "mods\spongeforge-" & versions(versionListBox.SelectedIndices(0)).OriginalString & ".jar"), "", "", True, 100, True)
                                                                                  For Each forgeMod In _server.ServerMods
                                                                                      If forgeMod.Name = "SpongeForge" Then
                                                                                          IO.File.Delete(forgeMod.Path)
                                                                                          _server.ServerMods.Remove(forgeMod)
                                                                                      End If
                                                                                  Next
                                                                                  _server.ServerMods.Add(New Server.ForgeMod("SpongeForge", IO.Path.Combine(_server.ServerPath, "mods\spongeforge-" & versions(versionListBox.SelectedIndices(0)).OriginalString & ".jar"), Now))
                                                                              End Sub
                                  End Sub)
        spongeThread.Start()
    End Sub
End Class