Imports System.ComponentModel
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Launcher
    Friend VanillaManifestURLs As New Dictionary(Of String, String)
    Friend VanillaVersionLists As New List(Of String)
    Friend ForgeVersionLists As New List(Of String)
    Friend SpigotVersionLists As New List(Of String)
    Friend CraftBukkitVersionLists As New List(Of String)
    Friend NukkitVersion As String
    Friend NukkitVersionUrl As String
    Dim VanillaGetVersionTask As Task
    Dim ForgeGetVersionTask As Task
    Dim SpigotGetVersionTask As Task
    Dim CraftBukkitGetVersionTask As Task
    Dim NukkitGetVersionTask As Task
    Friend servers As New List(Of String)
    Friend upnpProvider As New UPnPMapper
    Dim sharer As String = ""
    Friend CanUPnP As Boolean
    Dim CanEnabledUPnP As Boolean
    Friend ip As String = ""

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Select Case ServerManagerTabs.SelectedIndex
            Case 0
                Dim dialog = New CreateJavaServerDialog(Me)
                dialog.Show()
            Case 1
                Dim dialog = New CreateBedrockServerDialog(Me)
                dialog.Show()
        End Select
    End Sub

    Private Sub Launcher_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        UpdateVersionLists()
        Try
            For Each i In Net.Dns.GetHostAddresses(Net.Dns.GetHostName)
                If i.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                    Dim b = i.GetAddressBytes
                    IPLabel.Text = "IP 位址：" & b(0) & "." & b(1) & "." & b(2) & "." & b(3)
                    ip = b(0) & "." & b(1) & "." & b(2) & "." & b(3)
                    Exit Try
                End If
            Next
        Catch ex As Exception
            IPLabel.Text = "IP 位址：(錯誤)"
            Button1.Enabled = False
        End Try
        Try
            For Each i In Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(0) _
.GetIPProperties.DhcpServerAddresses
                If i.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                    Dim b = i.GetAddressBytes
                    RouterLabel.Text = "分享器/路由器位址：" & b(0) & "." & b(1) & "." & b(2) & "." & b(3)
                    ChangeRouterBtn.Enabled = True
                    sharer = b(0) & "." & b(1) & "." & b(2) & "." & b(3)
                    Exit Try
                End If
            Next
        Catch ex As Exception
            RouterLabel.Text = "分享器/路由器位址：(錯誤)"
            ChangeRouterBtn.Enabled = False
        End Try
        If upnpProvider.Init() Then
            CanEnabledUPnP = True
            UPnPLabel.Text = "UPnP 狀態：可用"
            CheckBox1.Enabled = True
            CheckBox1.Checked = True
            UPnPIPLabel.Text = "UPnP-IP位址：" & upnpProvider.ExternalIP
        Else
            CanEnabledUPnP = False
            UPnPLabel.Text = "UPnP 狀態：不可用"
            CheckBox1.Enabled = False
            UPnPIPLabel.Text = "UPnP-IP位址：" & "(無)"
        End If
        If JavaServerDirs <> "" Then
            For Each s In CType(Newtonsoft.Json.JsonConvert.DeserializeObject(JavaServerDirs), Newtonsoft.Json.Linq.JArray)
                AddJavaServer(s.ToString)
                servers.Add(s.ToString)
            Next
        End If
        If BedrockServerDirs <> "" Then
            For Each s In CType(Newtonsoft.Json.JsonConvert.DeserializeObject(BedrockServerDirs), Newtonsoft.Json.Linq.JArray)
                AddBedrockServer(s.ToString)
                servers.Add(s.ToString)
            Next
        End If
        If My.Computer.FileSystem.FileExists(IO.Path.Combine(My.Application.Info.DirectoryPath, "manager-setting.txt")) Then
            Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(My.Application.Info.DirectoryPath, "manager-setting.txt"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)

                Do Until reader.EndOfStream
                    Dim infoText As String = reader.ReadLine
                    Dim info = infoText.Split("=", 2, StringSplitOptions.None)
                    Select Case info(0)
                        Case "memory-min"
                            If IsNumeric(info(1)) Then MemoryMinBox.Value = info(1)
                        Case "memory-max"
                            If IsNumeric(info(1)) Then MemoryMaxBox.Value = info(1)
                    End Select
                Loop
            End Using

        End If
    End Sub

    Private Sub ChangeRouterBtn_Click(sender As Object, e As EventArgs) Handles ChangeRouterBtn.Click
        Process.Start(New Uri("http://" & sharer).ToString)
    End Sub

    Friend Sub AddJavaServer(serverDirectory As String)
        Dim t As New TabPage(New IO.DirectoryInfo(serverDirectory).Name)
        Dim server As New JavaServer(serverDirectory)
        t.Controls.Add(server)
        server.Dock = DockStyle.Fill
        AddHandler server.GotServerPort, Sub(obj, args)
                                             t.Text = New IO.DirectoryInfo(serverDirectory).Name & " [" & args & "]"
                                         End Sub
        AddHandler server.ServerOptionNotFound, Sub(obj)
                                                    JavaServerManager.TabPages.Remove(t)
                                                End Sub
        JavaServerManager.TabPages.Add(t)
        servers.Add(serverDirectory)
    End Sub
    Friend Sub AddBedrockServer(serverDirectory As String)
        Dim t As New TabPage(New IO.DirectoryInfo(serverDirectory).Name)
        Dim server As New BedrockServer(serverDirectory)
        t.Controls.Add(server)
        server.Dock = DockStyle.Fill
        AddHandler server.GotServerPort, Sub(obj, args)
                                             t.Text = New IO.DirectoryInfo(serverDirectory).Name & " [" & args & "]"
                                         End Sub
        AddHandler server.ServerOptionNotFound, Sub(obj)
                                                    BedrockServerManager.TabPages.Remove(t)
                                                End Sub
        BedrockServerManager.TabPages.Add(t)
        servers.Add(serverDirectory)
    End Sub
    Sub RegisterJavaServer(serverDirectory As String)
        If JavaServerDirs <> "" Then
            Dim array = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(JavaServerDirs)
            array.Add(New Newtonsoft.Json.Linq.JValue(serverDirectory))
            JavaServerDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
        Else
            Dim array = New Newtonsoft.Json.Linq.JArray()
            array.Add(New Newtonsoft.Json.Linq.JValue(serverDirectory))
            JavaServerDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
        End If
    End Sub
    Sub UnRegisterJavaServer(serverPage As TabPage)
        Try
            If JavaServerDirs <> "" Then
                Dim array = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(JavaServerDirs)
                array.RemoveAt(JavaServerManager.TabPages.IndexOf(serverPage))
                JavaServerDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Sub RegisterBedrockServer(serverDirectory As String)
        If BedrockServerDirs <> "" Then
            Dim array = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(BedrockServerDirs)
            array.Add(New Newtonsoft.Json.Linq.JValue(serverDirectory))
            BedrockServerDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
        Else
            Dim array = New Newtonsoft.Json.Linq.JArray()
            array.Add(New Newtonsoft.Json.Linq.JValue(serverDirectory))
            BedrockServerDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
        End If
    End Sub
    Sub UnRegisterBedrockServer(serverPage As TabPage)
        Try
            If BedrockServerDirs <> "" Then
                Dim array = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(BedrockServerDirs)
                array.RemoveAt(JavaServerManager.TabPages.IndexOf(serverPage))
                BedrockServerDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Static dialog As New FolderBrowserDialog
        dialog.ShowNewFolderButton = False
        If dialog.ShowDialog = DialogResult.OK Then
            If servers.Contains(dialog.SelectedPath) Then Exit Sub
            Select Case ServerManagerTabs.SelectedIndex
                Case 0
                    AddJavaServer(dialog.SelectedPath)
                    RegisterJavaServer(dialog.SelectedPath)
                Case 1
                    AddBedrockServer(dialog.SelectedPath)
                    RegisterBedrockServer(dialog.SelectedPath)
            End Select
        End If
    End Sub

    Private Sub Launcher_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            For Each page As TabPage In JavaServerManager.TabPages
                Dim server As JavaServer = CType(page.Controls(0), JavaServer)
                server.CloseServer()
            Next
            For Each page As TabPage In BedrockServerManager.TabPages
                Dim server As BedrockServer = CType(page.Controls(0), BedrockServer)
                server.CloseServer()
            Next
        Catch ex As Exception
        End Try
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "manager-setting.txt"), "memory-min=" & MemoryMinBox.Value & vbNewLine & "memory-max=" & MemoryMaxBox.Value, False)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Select Case ServerManagerTabs.SelectedIndex
                Case 0
                    Select Case MsgBox("要刪除伺服器的資料夾嗎？", MsgBoxStyle.YesNoCancel, "移除伺服器")
                        Case MsgBoxResult.Yes
                            Dim page As TabPage = JavaServerManager.SelectedTab
                            If IsNothing(page) = False Then
                                UnRegisterJavaServer(page)
                                Dim server As JavaServer = CType(page.Controls(0), JavaServer)
                                If My.Computer.FileSystem.DirectoryExists(server.serverDir) Then
                                    My.Computer.FileSystem.DeleteDirectory(server.serverDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
                                End If
                                server.CloseServer()
                                Invoke(New Action(Sub() JavaServerManager.TabPages.Remove(page)))
                                servers.Remove(server.serverDir)
                            End If
                        Case MsgBoxResult.No
                            Dim page As TabPage = JavaServerManager.SelectedTab
                            If IsNothing(page) = False Then
                                UnRegisterJavaServer(page)
                                Dim server As JavaServer = CType(page.Controls(0), JavaServer)
                                server.CloseServer()
                                Invoke(New Action(Sub() JavaServerManager.TabPages.Remove(page)))
                                servers.Remove(server.serverDir)
                            End If
                        Case MsgBoxResult.Cancel
                            Exit Sub
                    End Select
                Case 1
                    Select Case MsgBox("要刪除伺服器的資料夾嗎？", MsgBoxStyle.YesNoCancel, "移除伺服器")
                        Case MsgBoxResult.Yes
                            Dim page As TabPage = BedrockServerManager.SelectedTab
                            If IsNothing(page) = False Then
                                UnRegisterBedrockServer(page)
                                Dim server As BedrockServer = CType(page.Controls(0), BedrockServer)
                                If My.Computer.FileSystem.DirectoryExists(server.serverDir) Then
                                    My.Computer.FileSystem.DeleteDirectory(server.serverDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
                                End If
                                server.CloseServer()
                                Invoke(New Action(Sub() BedrockServerManager.TabPages.Remove(page)))
                                servers.Remove(server.serverDir)
                            End If
                        Case MsgBoxResult.No
                            Dim page As TabPage = BedrockServerManager.SelectedTab
                            If IsNothing(page) = False Then
                                UnRegisterBedrockServer(page)
                                Dim server As BedrockServer = CType(page.Controls(0), BedrockServer)
                                server.CloseServer()
                                Invoke(New Action(Sub() BedrockServerManager.TabPages.Remove(page)))
                                servers.Remove(server.serverDir)
                            End If
                        Case MsgBoxResult.Cancel
                            Exit Sub
                    End Select
            End Select
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetVanillaServerVersionList()
        VanillaGetVersionTask = Task.Factory.StartNew(New Action(Sub()
                                                                     VanillaVersionLists.Clear()
                                                                     Dim manifestListURL As String = "https://launchermeta.mojang.com/mc/game/version_manifest.json"
                                                                     Dim client As New Net.WebClient()
                                                                     BeginInvoke(New Action(Sub() VanillaLoadingLabel.Text = "原版：" & "下載列表中..."))
                                                                     Dim docHtml = client.DownloadString(manifestListURL)
                                                                     BeginInvoke(New Action(Sub() VanillaLoadingLabel.Text = "原版：" & "載入列表中..."))
                                                                     Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(docHtml)
                                                                     For Each jsonValue In jsonObject.GetValue("versions").ToObject(Of JArray)
                                                                         VanillaVersionLists.Add(jsonValue.Item("id").ToString())
                                                                         VanillaManifestURLs.Add(jsonValue.Item("id").ToString(), jsonValue.Item("url").ToString())
                                                                         If (jsonValue.Item("id").ToString() = "1.2.2") Then
                                                                             Exit For
                                                                         End If
                                                                     Next
                                                                     BeginInvoke(New Action(Sub() VanillaLoadingLabel.Text = "原版：" & "載入完成"))
                                                                 End Sub))
    End Sub
    Private Sub GetCraftBukkitServerVersionList()
        CraftBukkitGetVersionTask = Task.Factory.StartNew(New Action(Sub()
                                                                         CraftBukkitVersionLists.Clear()
                                                                         Dim listURL As String = "https://getbukkit.org/download/craftbukkit"
                                                                         Dim client As New Net.WebClient()
                                                                         Dim versionList = (New HtmlAgilityPack.HtmlWeb).Load(listURL).DocumentNode.SelectNodes("//div[4]/div[1]/div[1]/div[1]/*")
                                                                         For Each version In versionList
                                                                             CraftBukkitVersionLists.Add(version.SelectNodes("div[1]/div[1]/h2[1]")(0).InnerText)
                                                                         Next
                                                                         BeginInvoke(New Action(Sub() CraftBukkitLoadingLabel.Text = "CraftBukkit：" & "載入完成"))
                                                                     End Sub))

    End Sub
    Private Sub GetSpigotServerVersionList()
        SpigotGetVersionTask = Task.Factory.StartNew(New Action(Sub()
                                                                    SpigotVersionLists.Clear()
                                                                    Dim listURL As String = "https://getbukkit.org/download/spigot"
                                                                    Dim client As New Net.WebClient()
                                                                    Dim versionList = (New HtmlAgilityPack.HtmlWeb).Load(listURL).DocumentNode.SelectNodes("//div[4]/div[1]/div[1]/div[1]/*")
                                                                    For Each version In versionList
                                                                        SpigotVersionLists.Add(version.SelectNodes("div[1]/div[1]/h2[1]")(0).InnerText)
                                                                    Next
                                                                    BeginInvoke(New Action(Sub() SpigotLoadingLabel.Text = "Spigot：" & "載入完成"))
                                                                End Sub))
    End Sub
    Private Sub GetForgeServerVersionList()
        ForgeGetVersionTask = Task.Factory.StartNew(New Action(Sub()
                                                                   ForgeNewestBranchVersionList.Clear()
                                                                   ForgeVersionLists.Clear()
                                                                   Dim manifestListURL As String = "http://files.minecraftforge.net/maven/net/minecraftforge/forge/json"
                                                                   Dim client As New Net.WebClient()
                                                                   BeginInvoke(New Action(Sub() ForgeLoadingLabel.Text = "Forge：" & "下載列表中..."))
                                                                   Dim docHtml = client.DownloadString(manifestListURL)
                                                                   BeginInvoke(New Action(Sub() ForgeLoadingLabel.Text = "Forge：" & "載入列表中..."))
                                                                   Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(docHtml)
                                                                   For Each _version In jsonObject.GetValue("mcversion").ToObject(Of JObject).Children
                                                                       Dim Version = _version.ToObject(Of JProperty).Name
                                                                       If IsNumeric(Version.ToString.Replace(".", "")) Then
                                                                           ForgeVersionLists.Add(Version)
                                                                       End If
                                                                       Dim branchNumber As Integer = CInt(jsonObject.GetValue("mcversion").Item(Version).ToObject(Of JArray).Last.ToString)
                                                                       ForgeNewestBranchVersionList.Add(Version, jsonObject.GetValue("number").Item(branchNumber.ToString).Item("version").ToString)
                                                                   Next
                                                                   ForgeVersionLists = ForgeVersionLists.OrderBy(Function(p)
                                                                                                                     Return New Version(p)
                                                                                                                 End Function).ToList
                                                                   ForgeVersionLists.Reverse()
                                                                   docHtml = Nothing
                                                                   client.Dispose()
                                                                   BeginInvoke(New Action(Sub() ForgeLoadingLabel.Text = "Forge：" & "載入完成"))
                                                               End Sub))
    End Sub
    Private Sub GetNukkitServerVersionList()
        ForgeGetVersionTask = Task.Factory.StartNew(New Action(Sub()
                                                                   NukkitVersion = ""
                                                                   Dim manifestListURL As String = "https://ci.nukkitx.com/job/NukkitX/job/master/api/json"
                                                                   Dim client As New Net.WebClient()
                                                                   BeginInvoke(New Action(Sub() NukkitLoadingLabel.Text = "Nukkit：" & "下載列表中..."))
                                                                   Dim docHtml = client.DownloadString(manifestListURL)
                                                                   BeginInvoke(New Action(Sub() NukkitLoadingLabel.Text = "Nukkit：" & "載入列表中..."))
                                                                   Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(docHtml)
                                                                   Dim lastSuccessfulBuild = jsonObject.GetValue("lastSuccessfulBuild").ToObject(Of JObject)
                                                                   NukkitVersion = lastSuccessfulBuild.Item("number")
                                                                   NukkitVersionUrl = lastSuccessfulBuild.Item("url")
                                                                   docHtml = Nothing
                                                                   client.Dispose()
                                                                   BeginInvoke(New Action(Sub() NukkitLoadingLabel.Text = "Nukkit：" & "載入完成"))
                                                               End Sub))
    End Sub
    Private Sub VersionListReloadButton_Click(sender As Object, e As EventArgs) Handles VersionListReloadButton.Click
        UpdateVersionLists()
    End Sub
    Private Sub UpdateVersionLists()
        If IsNothing(VanillaGetVersionTask) = False Then
            VanillaGetVersionTask.Dispose()
            VanillaGetVersionTask = Nothing
        End If
        If IsNothing(ForgeGetVersionTask) = False Then
            ForgeGetVersionTask.Dispose()
            ForgeGetVersionTask = Nothing
        End If
        If IsNothing(SpigotGetVersionTask) = False Then
            SpigotGetVersionTask.Dispose()
            SpigotGetVersionTask = Nothing
        End If
        If IsNothing(CraftBukkitGetVersionTask) = False Then
            CraftBukkitGetVersionTask.Dispose()
            CraftBukkitGetVersionTask = Nothing
        End If
        If IsNothing(NukkitGetVersionTask) = False Then
            NukkitGetVersionTask.Dispose()
            NukkitGetVersionTask = Nothing
        End If
        GetVanillaServerVersionList()
        GetForgeServerVersionList()
        SpigotLoadingLabel.Text = "Spigot：" & "載入中..."
        GetSpigotServerVersionList()
        CraftBukkitLoadingLabel.Text = "CraftBukkit：" & "載入中..."
        GetCraftBukkitServerVersionList()
        GetNukkitServerVersionList()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Dim dialog = New CreateBedrockServerDialog(Me)
        dialog.Show()
    End Sub



    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CanEnabledUPnP Then
            Select Case CheckBox1.Checked
                Case True
                    CanUPnP = True
                Case False
                    CanUPnP = False
            End Select
        End If
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        Dim form As New MinecraftGUITestForm
        form.Show()
    End Sub
End Class
