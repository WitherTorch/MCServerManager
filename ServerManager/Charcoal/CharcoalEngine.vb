Imports System.Threading.Tasks
Imports TheArtOfDev

Public Class CharcoalEngine
    Dim client As New Net.WebClient
    Dim parser As New HtmlAgilityPack.HtmlDocument()
    Public Const CHARCOAL_VER As String = "1.5"
    Friend Event NavigationStarted(sender As Object, e As EventArgs)
    Friend Event DownloadProgressChanged(sender As Object, e As Net.DownloadProgressChangedEventArgs)
    Friend Event DownloadCompleted(sender As Object, e As Net.DownloadStringCompletedEventArgs)
    Friend Event NavigationEnded(sender As Object, e As EventArgs)
    Private NavigationTask As Task
    Dim _index As Integer
    Dim mServer As ModPackServer
    Dim pluginName As String = ""
    Enum RenderPageType
        Bukkit_PluginListPage
        Bukkit_PluginMainPage
        Bukkit_PluginDownloadListPage
        CurseForge_PluginListPage
        CurseForge_PluginMainPage
        CurseForge_PluginDownloadListPage
        CurseForge_ModListPage
        CurseForge_ModMainPage
        CurseForge_ModDownloadListPage
        Nukkit_PluginListPage
        Nukkit_PluginMainPage
        FeedTheBeast_ModpackListPage
        FeedTheBeast_ModpackMainPage
        FeedTheBeast_ModpackDownloadListPage
        CurseForge_ModpackListPage
        CurseForge_ModpackMainPage
        CurseForge_ModpackDownloadListPage
    End Enum
    Sub New(index As Integer)
        Dim version = System.Environment.OSVersion.Version
        client.Headers.Add(Net.HttpRequestHeader.UserAgent,
                      "Mozilla/5.0 (Windows NT " & version.Major & "." & version.Minor & ") Charcoal/" & CHARCOAL_VER)
        _index = index
    End Sub
    Sub New(server As ModPackServer)
        Dim version = System.Environment.OSVersion.Version
        client.Headers.Add(Net.HttpRequestHeader.UserAgent,
                      "Mozilla/5.0 (Windows NT " & version.Major & "." & version.Minor & ") Charcoal/" & CHARCOAL_VER)
        mServer = server
    End Sub
    Sub LoadPage(url As String, type As RenderPageType, ByRef targetPanel As Panel)
        Try
            parser = New HtmlAgilityPack.HtmlDocument()
            Dim parent As Panel = targetPanel
            GC.Collect()
            RaiseEvent NavigationStarted(Me, New EventArgs)
            If (type <> RenderPageType.Bukkit_PluginDownloadListPage) And
                (type <> RenderPageType.CurseForge_PluginDownloadListPage) And
                (type <> RenderPageType.CurseForge_ModDownloadListPage) Then
                BeginInvokeIfRequired(parent, Sub() parent.Controls.Clear())
            End If
            Dim uri As New Uri(url)
            Select Case type
#Region "dev.bukkit.org/bukkit-plugins"
                Case RenderPageType.Bukkit_PluginListPage
                    pluginName = ""
                    Dim layout As New TableLayoutPanel
                    layout.Dock = DockStyle.Fill
                    layout.AutoScroll = True
                    layout.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim strip As New ToolStrip
                                                                              strip.Height = 27
                                                                              layout.RowStyles.Add(New RowStyle(SizeType.Absolute, 27))
                                                                              layout.Controls.Add(strip, 0, 0)
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/section[2]/div[2]/div[1]/div[2]/ul[1]/*")
                                                                                  Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                  Dim label As ToolStripItem = Nothing
                                                                                  Select Case innerElement.Name
                                                                                      Case "span"
                                                                                          label = New ToolStripLabel
                                                                                          label.Text = innerElement.InnerText
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.AutoSize = True
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                      Case "a"
                                                                                          label = New ToolStripButton
                                                                                          label.AutoToolTip = False
                                                                                          label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                          label.Text = innerElement.InnerText
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.AutoSize = True
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          AddHandler label.Click, Sub()
                                                                                                                      LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), innerElement.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.Bukkit_PluginListPage, parent)
                                                                                                                  End Sub
                                                                                          label.ForeColor = SystemColors.HotTrack
                                                                                  End Select
                                                                                  If IsNothing(label) = False Then
                                                                                      strip.Items.Add(label)
                                                                                  End If
                                                                              Next
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/section[2]/div[2]/div[2]/ul[1]/*")
                                                                                  Dim pluginItem As New PluginListItem()
                                                                                  pluginItem.Dock = DockStyle.Fill

                                                                                  Dim iconNode As HtmlAgilityPack.HtmlNode = Nothing
                                                                                  For Each node In element.SelectSingleNode("div[1]/a[1]").ChildNodes
                                                                                      If node.Name.ToLower = "img" Then
                                                                                          iconNode = node
                                                                                          Exit For
                                                                                      End If
                                                                                  Next
                                                                                  Dim nameNode = element.SelectSingleNode("div[2]/div[1]/div[1]/a[1]")
                                                                                  pluginItem.pluginIcon.Cursor = Cursors.Hand
                                                                                  If IsNothing(iconNode) = False Then
                                                                                      pluginItem.pluginIcon.ImageLocation = iconNode.GetAttributeValue("src", "https://media.forgecdn.net/avatars/thumbnails/65/443/48/48/636162895990633284.png")
                                                                                  Else
                                                                                      pluginItem.pluginIcon.Image = My.Resources.bukkit
                                                                                  End If
                                                                                  AddHandler pluginItem.pluginIcon.Click, Sub()
                                                                                                                              LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.SelectSingleNode("div[2]/div[1]/div[1]/a[1]").GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.Bukkit_PluginMainPage, parent)
                                                                                                                              pluginName = nameNode.InnerText.Trim.Replace("&amp;", "&")
                                                                                                                          End Sub
                                                                                  pluginItem.pluginName.Text = nameNode.InnerText
                                                                                  pluginItem.pluginName.Cursor = Cursors.Hand
                                                                                  pluginItem.pluginName.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 18, GraphicsUnit.Pixel)
                                                                                  AddHandler pluginItem.pluginName.Click, Sub()
                                                                                                                              LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.SelectSingleNode("div[2]/div[1]/div[1]/a[1]").GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.Bukkit_PluginMainPage, parent)
                                                                                                                              pluginName = nameNode.InnerText.Trim.Replace("&amp;", "&")
                                                                                                                          End Sub
                                                                                  pluginItem.DescriptionLabel.Text = element.SelectSingleNode("div[2]/div[4]/p[1]").InnerText
                                                                                  pluginItem.DescriptionLabel.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 13, GraphicsUnit.Pixel)
                                                                                  layout.RowStyles.Add(New RowStyle(SizeType.Absolute, pluginItem.Height))
                                                                                  layout.Controls.Add(pluginItem, 0, layout.RowCount - 1)
                                                                              Next
                                                                              parent.Controls.Add(layout)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2

                    client.DownloadStringAsync(New Uri(url))
                Case RenderPageType.Bukkit_PluginMainPage
                    ' parent.Controls.Clear()
                    Dim layout As New TabControl
                    layout.Dock = DockStyle.Fill
                    Dim IntroPage As New TabPage("插件內容")
                    layout.TabPages.Add(IntroPage)
                    Dim DownloadPage As New TabPage("插件下載")
                    layout.TabPages.Add(DownloadPage)
                    parent.Controls.Add(layout)
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(_index.ToString, pluginName, DownloadVersionList.BrowsingWebsite.Bukkit)
                                                                              table.Dock = DockStyle.Fill
                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/div[1]/div[2]/div[1]/div[2]/ul[1]/*")
                                                                                  If node.Name = "li" Then
                                                                                      Dim innerElement As HtmlAgilityPack.HtmlNode = node.FirstChild
                                                                                      Dim label As ToolStripItem = Nothing
                                                                                      Select Case innerElement.Name
                                                                                          Case "span"
                                                                                              label = New ToolStripLabel
                                                                                              label.Text = innerElement.InnerText
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.AutoSize = True
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          Case "a"
                                                                                              label = New ToolStripButton
                                                                                              label.AutoToolTip = False
                                                                                              label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                              label.Text = innerElement.InnerText
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.AutoSize = True
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                              AddHandler label.Click, Sub()
                                                                                                                          LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), innerElement.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.Bukkit_PluginDownloadListPage, parent)
                                                                                                                      End Sub
                                                                                              label.ForeColor = SystemColors.HotTrack
                                                                                      End Select
                                                                                      If IsNothing(label) = False Then
                                                                                          table.NaviBar.Items.Add(label)
                                                                                      End If
                                                                                  End If
                                                                              Next

                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/div[1]/div[2]/div[2]/table[1]/tbody[1]/*")
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node1Class As String = node.SelectSingleNode("td[1]/div[1]").GetAttributeValue("class", "")
                                                                                  Select Case node1Class
                                                                                      Case "release-phase tip"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "正式版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(140, 175, 98)})
                                                                                      Case "beta-phase tip"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "預覽版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(127, 165, 196)})
                                                                                  End Select
                                                                                  Dim node2 = node.SelectSingleNode("td[2]/div[1]/div[2]/a[1]")
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim())
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node2.GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  Dim node3_value As String = node.SelectSingleNode("td[3]").InnerText.Trim()
                                                                                  item.SubItems.Add(node3_value)
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[4]/abbr[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node4_value)
                                                                                  Dim node5_value As String = node.SelectSingleNode("td[5]/span[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node5_value)
                                                                                  Dim node6_value As String = node.SelectSingleNode("td[6]").InnerText.Trim()
                                                                                  item.SubItems.Add(node6_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim node = parser.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/div[1]/div[1]")
                                                                              Dim headNode = parser.DocumentNode.SelectSingleNode("/html[1]/head[1]")
                                                                              Dim p As New HtmlRenderer.WinForms.HtmlPanel
                                                                              p.Dock = DockStyle.Fill
                                                                              p.AutoScroll = True
                                                                              p.Text = "<html>" & headNode.OuterHtml & "<body>" & node.OuterHtml & "</body></html>"
                                                                              IntroPage.Controls.Add(p)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                              AddHandler client.DownloadProgressChanged, e3
                                                                              AddHandler client.DownloadStringCompleted, e4
                                                                              client.DownloadStringAsync(New Uri(url & "/files"))
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2
                    client.DownloadStringAsync(uri)
                Case RenderPageType.Bukkit_PluginDownloadListPage
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e3
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              Dim DownloadPage = CType(parent.Controls(0), TabControl).TabPages.Item(1)
                                                                              DownloadPage.Controls.Clear()
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(_index.ToString, pluginName, DownloadVersionList.BrowsingWebsite.Bukkit)
                                                                              table.Dock = DockStyle.Fill
                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/div[1]/div[2]/div[1]/div[2]/ul[1]/*")
                                                                                  If node.Name = "li" Then
                                                                                      Dim innerElement As HtmlAgilityPack.HtmlNode = node.FirstChild
                                                                                      Dim label As ToolStripItem = Nothing
                                                                                      Select Case innerElement.Name
                                                                                          Case "span"
                                                                                              label = New ToolStripLabel
                                                                                              label.Text = innerElement.InnerText
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.AutoSize = True
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          Case "a"
                                                                                              label = New ToolStripButton
                                                                                              label.AutoToolTip = False
                                                                                              label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                              label.Text = innerElement.InnerText
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.AutoSize = True
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                              AddHandler label.Click, Sub()
                                                                                                                          LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), innerElement.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.Bukkit_PluginDownloadListPage, parent)
                                                                                                                      End Sub
                                                                                              label.ForeColor = SystemColors.HotTrack
                                                                                      End Select
                                                                                      If IsNothing(label) = False Then
                                                                                          table.NaviBar.Items.Add(label)
                                                                                      End If
                                                                                  End If
                                                                              Next
                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/div[1]/div[2]/div[2]/table[1]/tbody[1]/*")
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node1Class As String = node.SelectSingleNode("td[1]/div[1]").GetAttributeValue("class", "")
                                                                                  Select Case node1Class
                                                                                      Case "release-phase tip"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "正式版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(140, 175, 98)})
                                                                                      Case "beta-phase tip"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "預覽版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(127, 165, 196)})
                                                                                  End Select
                                                                                  Dim node2 = node.SelectSingleNode("td[2]/div[1]/div[2]/a[1]")
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim)
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node2.GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  Dim node3_value As String = node.SelectSingleNode("td[3]").InnerText.Trim
                                                                                  item.SubItems.Add(node3_value)
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[4]/abbr[1]").InnerText.Trim
                                                                                  item.SubItems.Add(node4_value)
                                                                                  Dim node5_value As String = node.SelectSingleNode("td[5]/span[1]").InnerText.Trim
                                                                                  item.SubItems.Add(node5_value)
                                                                                  Dim node6_value As String = node.SelectSingleNode("td[6]").InnerText.Trim
                                                                                  item.SubItems.Add(node6_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e4
                    client.DownloadStringAsync(New Uri(url))
#End Region
#Region "www.curseforge.com/minecraft/bukkit-plugins"
                Case RenderPageType.CurseForge_PluginListPage
                    pluginName = ""
                    Dim layout As New TableLayoutPanel
                    layout.Dock = DockStyle.Fill
                    layout.AutoScroll = True
                    layout.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim strip As New ToolStrip
                                                                              strip.Height = 27
                                                                              layout.RowStyles.Add(New RowStyle(SizeType.Absolute, 27))
                                                                              layout.Controls.Add(strip, 0, 0)
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]/*")
                                                                                  Dim label As ToolStripItem = Nothing
                                                                                  Select Case element.Name
                                                                                      Case "span"
                                                                                          label = New ToolStripLabel
                                                                                          label.Text = element.InnerText.Replace("&hellip;", "...")
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.AutoSize = True
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                      Case "a"
                                                                                          Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                          label = New ToolStripButton
                                                                                          label.AutoToolTip = False
                                                                                          label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                          label.Text = innerElement.InnerText
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.AutoSize = True
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          AddHandler label.Click, Sub()
                                                                                                                      LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_PluginListPage, parent)
                                                                                                                  End Sub
                                                                                          label.ForeColor = SystemColors.HotTrack
                                                                                  End Select
                                                                                  If IsNothing(label) = False Then
                                                                                      strip.Items.Add(label)
                                                                                  End If
                                                                              Next
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[3]/div[1]/*")
                                                                                  Dim pluginItem As New PluginListItem()
                                                                                  pluginItem.Dock = DockStyle.Fill

                                                                                  Dim iconNode As HtmlAgilityPack.HtmlNode = element.SelectSingleNode("div[1]/div[1]/div[1]/div[1]/a[1]/img[1]")

                                                                                  Dim nameNode = element.SelectSingleNode("div[1]/div[1]/div[2]/a[1]/h3[1]")
                                                                                  pluginItem.pluginIcon.Cursor = Cursors.Hand
                                                                                  If IsNothing(iconNode) = False Then
                                                                                      pluginItem.pluginIcon.ImageLocation = iconNode.GetAttributeValue("src", "https://media.forgecdn.net/avatars/thumbnails/65/443/48/48/636162895990633284.png")
                                                                                  Else
                                                                                      pluginItem.pluginIcon.Image = My.Resources.bukkit
                                                                                  End If
                                                                                  AddHandler pluginItem.pluginIcon.Click, Sub()
                                                                                                                              LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.SelectSingleNode("div[1]/div[1]/div[2]/a[1]").GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_PluginMainPage, parent)
                                                                                                                              pluginName = nameNode.InnerText.Trim.Replace("&amp;", "&")
                                                                                                                          End Sub
                                                                                  pluginItem.pluginName.Text = nameNode.InnerText.Trim.Replace("&amp;", "&")
                                                                                  pluginItem.pluginName.Cursor = Cursors.Hand
                                                                                  pluginItem.pluginName.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 18, GraphicsUnit.Pixel)
                                                                                  AddHandler pluginItem.pluginName.Click, Sub()
                                                                                                                              LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.SelectSingleNode("div[1]/div[1]/div[2]/a[1]").GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_PluginMainPage, parent)
                                                                                                                              pluginName = nameNode.InnerText.Trim.Replace("&amp;", "&")
                                                                                                                          End Sub
                                                                                  pluginItem.DescriptionLabel.Text = element.SelectSingleNode("div[1]/div[2]/p[1]").InnerText.Trim
                                                                                  pluginItem.DescriptionLabel.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 13, GraphicsUnit.Pixel)
                                                                                  layout.RowStyles.Add(New RowStyle(SizeType.Absolute, pluginItem.Height))
                                                                                  layout.Controls.Add(pluginItem, 0, layout.RowCount - 1)
                                                                              Next
                                                                              parent.Controls.Add(layout)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2

                    client.DownloadStringAsync(New Uri(url))
                Case RenderPageType.CurseForge_PluginMainPage

                    Dim layout As New TabControl
                    layout.Dock = DockStyle.Fill
                    Dim IntroPage As New TabPage("插件內容")
                    layout.TabPages.Add(IntroPage)
                    Dim DownloadPage As New TabPage("插件下載")
                    layout.TabPages.Add(DownloadPage)
                    parent.Controls.Add(layout)
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(_index.ToString, pluginName, DownloadVersionList.BrowsingWebsite.CurseForge_Plugin)
                                                                              table.Dock = DockStyle.Fill
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[1]/*")
                                                                                  Dim label As ToolStripItem = Nothing
                                                                                  Select Case element.Name
                                                                                      Case "span"
                                                                                          label = New ToolStripLabel
                                                                                          label.Text = element.InnerText.Replace("&hellip;", "...")
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.AutoSize = True
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                      Case "a"
                                                                                          Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                          label = New ToolStripButton
                                                                                          label.AutoToolTip = False
                                                                                          label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                          label.Text = innerElement.InnerText
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.AutoSize = True
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          AddHandler label.Click, Sub()
                                                                                                                      LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_PluginDownloadListPage, parent)
                                                                                                                  End Sub
                                                                                          label.ForeColor = SystemColors.HotTrack
                                                                                  End Select
                                                                                  If IsNothing(label) = False Then
                                                                                      table.NaviBar.Items.Add(label)
                                                                                  End If
                                                                              Next

                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/table[1]/tbody[1]/*")
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node1Class As String = node.SelectSingleNode("td[1]/div[1]").GetAttributeValue("class", "")
                                                                                  Select Case node1Class
                                                                                      Case "w-5 h-5 bg-green-500 flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "正式版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(140, 175, 98)})
                                                                                      Case "w-5 h-5 bg-prime-blue flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "預覽版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(127, 165, 196)})
                                                                                  End Select
                                                                                  Dim node2 = node.SelectSingleNode("td[2]/a[1]")
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim())
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node2.GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  Dim node3_value As String = node.SelectSingleNode("td[3]").InnerText.Trim()
                                                                                  item.SubItems.Add(node3_value)
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[4]/abbr[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node4_value)
                                                                                  Dim node5_value As String = node.SelectSingleNode("td[5]/div[1]/div[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node5_value)
                                                                                  Dim node6_value As String = node.SelectSingleNode("td[6]").InnerText.Trim()
                                                                                  item.SubItems.Add(node6_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim node = parser.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[1]")
                                                                              Dim headNode = parser.DocumentNode.SelectSingleNode("/html[1]/head[1]")
                                                                              Dim p As New HtmlRenderer.WinForms.HtmlPanel
                                                                              p.Dock = DockStyle.Fill
                                                                              p.AutoScroll = True
                                                                              p.Text = "<html>" & headNode.OuterHtml & "<body>" & node.OuterHtml & "</body></html>"
                                                                              IntroPage.Controls.Add(p)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                              AddHandler client.DownloadProgressChanged, e3
                                                                              AddHandler client.DownloadStringCompleted, e4
                                                                              client.DownloadStringAsync(New Uri(url & "/files/all"))
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2
                    client.DownloadStringAsync(uri)
                Case RenderPageType.CurseForge_PluginDownloadListPage
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e3
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              Dim DownloadPage = CType(parent.Controls(0), TabControl).TabPages.Item(1)
                                                                              DownloadPage.Controls.Clear()
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(_index.ToString, pluginName, DownloadVersionList.BrowsingWebsite.CurseForge_Plugin)
                                                                              table.Dock = DockStyle.Fill
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[1]/*")
                                                                                  Dim label As ToolStripItem = Nothing
                                                                                  Select Case element.Name
                                                                                      Case "span"
                                                                                          label = New ToolStripLabel
                                                                                          label.Text = element.InnerText.Replace("&hellip;", "...")
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.AutoSize = True
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                      Case "a"
                                                                                          Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                          label = New ToolStripButton
                                                                                          label.AutoToolTip = False
                                                                                          label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                          label.Text = innerElement.InnerText
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.AutoSize = True
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          AddHandler label.Click, Sub()
                                                                                                                      LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_PluginDownloadListPage, parent)
                                                                                                                  End Sub
                                                                                          label.ForeColor = SystemColors.HotTrack
                                                                                  End Select
                                                                                  If IsNothing(label) = False Then
                                                                                      table.NaviBar.Items.Add(label)
                                                                                  End If
                                                                              Next

                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/table[1]/tbody[1]/*")
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node1Class As String = node.SelectSingleNode("td[1]/div[1]").GetAttributeValue("class", "")
                                                                                  Select Case node1Class
                                                                                      Case "w-5 h-5 bg-green-500 flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "正式版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(140, 175, 98)})
                                                                                      Case "w-5 h-5 bg-prime-blue flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "預覽版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(127, 165, 196)})
                                                                                  End Select
                                                                                  Dim node2 = node.SelectSingleNode("td[2]/a[1]")
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim())
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node2.GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  Dim node3_value As String = node.SelectSingleNode("td[3]").InnerText.Trim()
                                                                                  item.SubItems.Add(node3_value)
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[4]/abbr[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node4_value)
                                                                                  Dim node5_value As String = node.SelectSingleNode("td[5]/div[1]/div[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node5_value)
                                                                                  Dim node6_value As String = node.SelectSingleNode("td[6]").InnerText.Trim()
                                                                                  item.SubItems.Add(node6_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)

                    AddHandler client.DownloadStringCompleted, e4
                    client.DownloadStringAsync(New Uri(url))
#End Region
#Region "www.curseforge.com/minecraft/mc-mods"
                Case RenderPageType.CurseForge_ModListPage
                    pluginName = ""
                    Dim layout As New TableLayoutPanel
                    layout.Dock = DockStyle.Fill
                    layout.AutoScroll = True
                    layout.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim strip As New ToolStrip
                                                                              strip.Height = 27
                                                                              layout.RowStyles.Add(New RowStyle(SizeType.Absolute, 27))
                                                                              layout.Controls.Add(strip, 0, 0)
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]/*")
                                                                                  Dim label As ToolStripItem = Nothing
                                                                                  Select Case element.Name
                                                                                      Case "span"
                                                                                          label = New ToolStripLabel
                                                                                          label.Text = element.InnerText.Replace("&hellip;", "...")
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.AutoSize = True
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                      Case "a"
                                                                                          Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                          label = New ToolStripButton
                                                                                          label.AutoToolTip = False
                                                                                          label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                          label.Text = innerElement.InnerText
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.AutoSize = True
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          AddHandler label.Click, Sub()
                                                                                                                      LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_ModListPage, parent)
                                                                                                                  End Sub
                                                                                          label.ForeColor = SystemColors.HotTrack
                                                                                  End Select
                                                                                  If IsNothing(label) = False Then
                                                                                      strip.Items.Add(label)
                                                                                  End If
                                                                              Next
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[3]/div[1]/*")
                                                                                  Dim pluginItem As New PluginListItem()
                                                                                  pluginItem.Dock = DockStyle.Fill

                                                                                  Dim iconNode As HtmlAgilityPack.HtmlNode = element.SelectSingleNode("div[1]/div[1]/div[1]/div[1]/a[1]/img[1]")

                                                                                  Dim nameNode = element.SelectSingleNode("div[1]/div[1]/div[2]/a[1]/h3[1]")
                                                                                  pluginItem.pluginIcon.Cursor = Cursors.Hand
                                                                                  If IsNothing(iconNode) = False Then
                                                                                      pluginItem.pluginIcon.ImageLocation = iconNode.GetAttributeValue("src", "https://media.forgecdn.net/avatars/thumbnails/65/443/48/48/636162895990633284.png")
                                                                                  Else
                                                                                      pluginItem.pluginIcon.Image = My.Resources.bukkit
                                                                                  End If
                                                                                  AddHandler pluginItem.pluginIcon.Click, Sub()
                                                                                                                              LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.SelectSingleNode("div[1]/div[1]/div[2]/a[1]").GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_ModMainPage, parent)
                                                                                                                              pluginName = nameNode.InnerText.Trim.Replace("&amp;", "&")
                                                                                                                          End Sub
                                                                                  pluginItem.pluginName.Text = nameNode.InnerText.Trim.Replace("&amp;", "&").Replace("&amp;", "&")
                                                                                  pluginItem.pluginName.Cursor = Cursors.Hand
                                                                                  pluginItem.pluginName.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 18, GraphicsUnit.Pixel)
                                                                                  AddHandler pluginItem.pluginName.Click, Sub()
                                                                                                                              LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.SelectSingleNode("div[1]/div[1]/div[2]/a[1]").GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_ModMainPage, parent)
                                                                                                                              pluginName = nameNode.InnerText.Trim.Replace("&amp;", "&")
                                                                                                                          End Sub
                                                                                  pluginItem.DescriptionLabel.Text = element.SelectSingleNode("div[1]/div[2]/p[1]").InnerText.Trim
                                                                                  pluginItem.DescriptionLabel.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 13, GraphicsUnit.Pixel)
                                                                                  layout.RowStyles.Add(New RowStyle(SizeType.Absolute, pluginItem.Height))
                                                                                  layout.Controls.Add(pluginItem, 0, layout.RowCount - 1)
                                                                              Next
                                                                              parent.Controls.Add(layout)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2

                    client.DownloadStringAsync(New Uri(url))
                Case RenderPageType.CurseForge_ModMainPage

                    Dim layout As New TabControl
                    layout.Dock = DockStyle.Fill
                    Dim IntroPage As New TabPage("模組內容")
                    layout.TabPages.Add(IntroPage)
                    Dim DownloadPage As New TabPage("模組下載")
                    layout.TabPages.Add(DownloadPage)
                    parent.Controls.Add(layout)
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(_index.ToString, pluginName, DownloadVersionList.BrowsingWebsite.CurseForge_Mod)
                                                                              table.Dock = DockStyle.Fill
                                                                              Dim stripElements = parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[3]/div[1]/*")
                                                                              If stripElements IsNot Nothing Then
                                                                                  For Each element In stripElements
                                                                                      Dim label As ToolStripItem = Nothing
                                                                                      Select Case element.Name
                                                                                          Case "span"
                                                                                              label = New ToolStripLabel
                                                                                              label.Text = element.InnerText.Replace("&hellip;", "...")
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.AutoSize = True
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          Case "a"
                                                                                              Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                              label = New ToolStripButton
                                                                                              label.AutoToolTip = False
                                                                                              label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                              label.Text = innerElement.InnerText
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.AutoSize = True
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                              AddHandler label.Click, Sub()
                                                                                                                          LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_ModDownloadListPage, parent)
                                                                                                                      End Sub
                                                                                              label.ForeColor = SystemColors.HotTrack
                                                                                      End Select
                                                                                      If IsNothing(label) = False Then
                                                                                          table.NaviBar.Items.Add(label)
                                                                                      End If
                                                                                  Next
                                                                              End If
                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/table[1]/tbody[1]/*")
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node1Class As String = node.SelectSingleNode("td[1]/div[1]").GetAttributeValue("class", "")
                                                                                  Select Case node1Class
                                                                                      Case "w-5 h-5 bg-green-500 flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "正式版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(140, 175, 98)})
                                                                                      Case "w-5 h-5 bg-prime-blue flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "預覽版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(127, 165, 196)})
                                                                                      Case "w-5 h-5 bg-twitch-offset flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "開發版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(211, 202, 232)})
                                                                                  End Select
                                                                                  Dim node2 = node.SelectSingleNode("td[2]/a[1]")
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim())
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node2.GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  Dim node3_value As String = node.SelectSingleNode("td[3]").InnerText.Trim()
                                                                                  item.SubItems.Add(node3_value)
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[4]/abbr[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node4_value)
                                                                                  Dim node5_value As String = node.SelectSingleNode("td[5]/div[1]/div[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node5_value)
                                                                                  Dim node6_value As String = node.SelectSingleNode("td[6]").InnerText.Trim()
                                                                                  item.SubItems.Add(node6_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim node = parser.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[1]")
                                                                              Dim headNode = parser.DocumentNode.SelectSingleNode("/html[1]/head[1]")
                                                                              Dim p As New HtmlRenderer.WinForms.HtmlPanel
                                                                              p.Dock = DockStyle.Fill
                                                                              p.AutoScroll = True
                                                                              p.Text = "<html>" & headNode.OuterHtml & "<body>" & node.OuterHtml & "</body></html>"
                                                                              IntroPage.Controls.Add(p)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                              AddHandler client.DownloadProgressChanged, e3
                                                                              AddHandler client.DownloadStringCompleted, e4
                                                                              client.DownloadStringAsync(New Uri(url & "/files/all"))
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2
                    client.DownloadStringAsync(uri)
                Case RenderPageType.CurseForge_ModDownloadListPage
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e3
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              Dim DownloadPage = CType(parent.Controls(0), TabControl).TabPages.Item(1)
                                                                              DownloadPage.Controls.Clear()
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(_index.ToString, pluginName, DownloadVersionList.BrowsingWebsite.CurseForge_Mod)
                                                                              table.Dock = DockStyle.Fill
                                                                              Dim stripElements = parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[3]/div[1]/*")
                                                                              If stripElements IsNot Nothing Then
                                                                                  For Each element In stripElements
                                                                                      Dim label As ToolStripItem = Nothing
                                                                                      Select Case element.Name
                                                                                          Case "span"
                                                                                              label = New ToolStripLabel
                                                                                              label.Text = element.InnerText.Replace("&hellip;", "...")
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.AutoSize = True
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          Case "a"
                                                                                              Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                              label = New ToolStripButton
                                                                                              label.AutoToolTip = False
                                                                                              label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                              label.Text = innerElement.InnerText
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.AutoSize = True
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                              AddHandler label.Click, Sub()
                                                                                                                          LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_ModDownloadListPage, parent)
                                                                                                                      End Sub
                                                                                              label.ForeColor = SystemColors.HotTrack
                                                                                      End Select
                                                                                      If IsNothing(label) = False Then
                                                                                          table.NaviBar.Items.Add(label)
                                                                                      End If
                                                                                  Next
                                                                              End If
                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/table[1]/tbody[1]/*")
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node1Class As String = node.SelectSingleNode("td[1]/div[1]").GetAttributeValue("class", "")
                                                                                  Select Case node1Class
                                                                                      Case "w-5 h-5 bg-green-500 flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "正式版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(140, 175, 98)})
                                                                                      Case "w-5 h-5 bg-prime-blue flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "預覽版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(127, 165, 196)})
                                                                                      Case "w-5 h-5 bg-twitch-offset flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "開發版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(211, 202, 232)})
                                                                                  End Select
                                                                                  Dim node2 = node.SelectSingleNode("td[2]/a[1]")
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim())
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node2.GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  Dim node3_value As String = node.SelectSingleNode("td[3]").InnerText.Trim()
                                                                                  item.SubItems.Add(node3_value)
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[4]/abbr[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node4_value)
                                                                                  Dim node5_value As String = node.SelectSingleNode("td[5]/div[1]/div[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node5_value)
                                                                                  Dim node6_value As String = node.SelectSingleNode("td[6]").InnerText.Trim()
                                                                                  item.SubItems.Add(node6_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)

                    AddHandler client.DownloadStringCompleted, e4
                    client.DownloadStringAsync(New Uri(url))
#End Region
#Region "nukkitx.com/resources/categories/nukkit-plugins"
                Case RenderPageType.Nukkit_PluginListPage
                    pluginName = ""
                    Dim layout As New TableLayoutPanel
                    layout.Dock = DockStyle.Fill
                    layout.AutoScroll = True
                    layout.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim strip As New ToolStrip
                                                                              strip.Height = 27
                                                                              layout.RowStyles.Add(New RowStyle(SizeType.Absolute, 27))
                                                                              layout.Controls.Add(strip, 0, 0)
                                                                              Dim lastNumber As Integer
                                                                              Dim numberNode = parser.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[4]/div[2]/div[2]/form[1]/div[1]/div[2]/nav[1]/a[1]")
                                                                              If numberNode.InnerText = "1" Then
                                                                                  lastNumber = parser.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[4]/div[2]/div[2]/form[1]/div[1]/div[2]/nav[1]/a[4]").InnerText
                                                                              Else
                                                                                  lastNumber = parser.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[4]/div[2]/div[2]/form[1]/div[1]/div[2]/nav[1]/a[5]").InnerText
                                                                              End If
                                                                              Dim targetNumber = 1
                                                                              If New System.Text.RegularExpressions.Regex("https:\/\/nukkitx.com\/resources\/categories\/nukkit-plugins.1\/\?page=[0-9]{1,}").IsMatch(url) Then
                                                                                  targetNumber = url.Substring(url.LastIndexOf("=") + 1)
                                                                              ElseIf url = "https://nukkitx.com/resources/categories/nukkit-plugins.1/" Then
                                                                                  targetNumber = 1
                                                                              Else
                                                                                  targetNumber = 0
                                                                              End If
                                                                              For i As Integer = 1 To lastNumber
                                                                                  If i = targetNumber Then
                                                                                      Dim Label = New ToolStripLabel
                                                                                      Label.Text = i
                                                                                      ' label.Location = New Point(1, 1)
                                                                                      Label.AutoSize = True
                                                                                      Label.TextAlign = ContentAlignment.MiddleCenter
                                                                                      Label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                      strip.Items.Add(Label)
                                                                                  ElseIf Math.Sqrt((i - targetNumber) ^ 2) < 6 Then
                                                                                      Dim nowNum = i
                                                                                      Dim Label = New ToolStripButton
                                                                                      Label.AutoToolTip = False
                                                                                      Label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                      Label.Text = nowNum
                                                                                      ' label.Location = New Point(1, 1)
                                                                                      Label.TextAlign = ContentAlignment.MiddleCenter
                                                                                      Label.AutoSize = True
                                                                                      Label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                      If i = 1 Then
                                                                                          AddHandler Label.Click, Sub()
                                                                                                                      LoadPage("https://nukkitx.com/resources/categories/nukkit-plugins.1/", RenderPageType.Nukkit_PluginListPage, parent)
                                                                                                                  End Sub
                                                                                      Else
                                                                                          AddHandler Label.Click, Sub()
                                                                                                                      LoadPage("https://nukkitx.com/resources/categories/nukkit-plugins.1/?page=" & nowNum, RenderPageType.Nukkit_PluginListPage, parent)
                                                                                                                  End Sub
                                                                                      End If
                                                                                      Label.ForeColor = SystemColors.HotTrack
                                                                                      strip.Items.Add(Label)
                                                                                  End If
                                                                              Next
                                                                              Dim node = parser.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[4]/div[2]/div[2]/form[1]/ol[1]/li[2]/div[2]/div[1]")
                                                                              Dim ci As Integer = 0
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[4]/div[2]/div[2]/form[1]/ol[1]/*")
                                                                                  ci += 1
                                                                                  If ci = 1 Then Continue For
                                                                                  Dim pluginItem As New PluginListItem()
                                                                                  pluginItem.Dock = DockStyle.Fill

                                                                                  Dim iconNode As HtmlAgilityPack.HtmlNode = element.SelectSingleNode("div[1]/div[1]/a[1]/img[1]")

                                                                                  Dim nameNode = element.SelectSingleNode("div[2]/div[1]/h3[1]/a[1]")
                                                                                  pluginItem.pluginIcon.Cursor = Cursors.Hand
                                                                                  If IsNothing(iconNode) = False Then
                                                                                      Dim imageURL = iconNode.GetAttributeValue("src", "")
                                                                                      imageURL.Trim("""")
                                                                                      imageURL = New Uri(New Uri("https://nukkitx.com/"), imageURL).AbsoluteUri
                                                                                      If imageURL = "" Then
                                                                                          pluginItem.pluginIcon.Image = My.Resources.nukkitPlguinDefault
                                                                                      Else
                                                                                          pluginItem.pluginIcon.ImageLocation = imageURL
                                                                                      End If
                                                                                  Else
                                                                                      pluginItem.pluginIcon.Image = My.Resources.nukkitPlguinDefault
                                                                                  End If
                                                                                  AddHandler pluginItem.pluginIcon.Click, Sub()
                                                                                                                              LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), nameNode.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.Nukkit_PluginMainPage, parent)
                                                                                                                              pluginName = nameNode.InnerText.Trim.Replace("&amp;", "&")
                                                                                                                          End Sub
                                                                                  pluginItem.pluginName.Text = nameNode.InnerText.Trim.Replace("&amp;", "&")
                                                                                  pluginItem.pluginName.Cursor = Cursors.Hand
                                                                                  pluginItem.pluginName.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 18, GraphicsUnit.Pixel)
                                                                                  AddHandler pluginItem.pluginName.Click, Sub()
                                                                                                                              LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), nameNode.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.Nukkit_PluginMainPage, parent)
                                                                                                                              pluginName = nameNode.InnerText.Trim.Replace("&amp;", "&")
                                                                                                                          End Sub
                                                                                  pluginItem.DescriptionLabel.Text = element.SelectSingleNode("div[2]/div[1]/div[2]").InnerText.Trim
                                                                                  pluginItem.DescriptionLabel.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 13, GraphicsUnit.Pixel)
                                                                                  layout.RowStyles.Add(New RowStyle(SizeType.Absolute, pluginItem.Height))
                                                                                  layout.Controls.Add(pluginItem, 0, layout.RowCount - 1)

                                                                              Next
                                                                              parent.Controls.Add(layout)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2
                    client.DownloadStringAsync(New Uri(url))
                Case RenderPageType.Nukkit_PluginMainPage
                    Dim layout As New TabControl
                    layout.Dock = DockStyle.Fill
                    Dim IntroPage As New TabPage("插件內容")
                    layout.TabPages.Add(IntroPage)
                    Dim DownloadPage As New TabPage("插件下載")
                    layout.TabPages.Add(DownloadPage)
                    parent.Controls.Add(layout)
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(_index.ToString, pluginName, DownloadVersionList.BrowsingWebsite.Nukkit_PluginDownloadList)
                                                                              table.Dock = DockStyle.Fill
                                                                              Dim i As Integer = 0
                                                                              Dim n = parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[5]/table[1]/*")
                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[5]/table[1]/*")
                                                                                  i += 1
                                                                                  If i <= 2 Then Continue For
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node2 = node.SelectSingleNode("td[1]")
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim())
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node.SelectSingleNode("td[5]/a[1]").GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  item.SubItems.Add("")
                                                                                  Dim node3 = node.SelectSingleNode("td[2]/span[1]")
                                                                                  If node3 Is Nothing Then
                                                                                      node3 = node.SelectSingleNode("td[2]/abbr[1]")
                                                                                  End If
                                                                                  Dim node3_value As String = node3.InnerText.Trim()
                                                                                  item.SubItems.Add(node3_value)
                                                                                  item.SubItems.Add("")
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[3]").InnerText.Trim()
                                                                                  item.SubItems.Add(node4_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim node = parser.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/div[5]/div[1]/ol[1]/li[1]")
                                                                              Dim headNode = parser.DocumentNode.SelectSingleNode("/html[1]/head[1]")
                                                                              Dim p As New HtmlRenderer.WinForms.HtmlPanel
                                                                              p.Dock = DockStyle.Fill
                                                                              p.AutoScroll = True
                                                                              p.Text = "<html>" & headNode.OuterHtml & "<body>" & node.OuterHtml & "</body></html>"
                                                                              IntroPage.Controls.Add(p)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                              AddHandler client.DownloadProgressChanged, e3
                                                                              AddHandler client.DownloadStringCompleted, e4
                                                                              client.DownloadStringAsync(New Uri(url & "/history"))
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2
                    client.DownloadStringAsync(uri)
#End Region
#Region "www.feed-the-beast.com/modpacks"
                Case RenderPageType.FeedTheBeast_ModpackListPage
                    pluginName = ""
                    Dim layout As New TableLayoutPanel
                    layout.Dock = DockStyle.Fill
                    layout.AutoScroll = True
                    layout.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim strip As New ToolStrip
                                                                              strip.Height = 27
                                                                              layout.RowStyles.Add(New RowStyle(SizeType.Absolute, 27))
                                                                              layout.Controls.Add(strip, 0, 0)
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/section[2]/div[2]/div[1]/div[2]/ul[1]/*")
                                                                                  Dim label As ToolStripItem = Nothing
                                                                                  Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                  Select Case innerElement.Name
                                                                                      Case "span"
                                                                                          label = New ToolStripLabel
                                                                                          label.Text = innerElement.InnerText.Replace("&hellip;", "...")
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.AutoSize = True
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                      Case "a"
                                                                                          label = New ToolStripButton
                                                                                          label.AutoToolTip = False
                                                                                          label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                          label.Text = innerElement.InnerText
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.AutoSize = True
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          AddHandler label.Click, Sub()
                                                                                                                      LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), innerElement.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.FeedTheBeast_ModpackListPage, parent)
                                                                                                                  End Sub
                                                                                          label.ForeColor = SystemColors.HotTrack
                                                                                  End Select
                                                                                  If IsNothing(label) = False Then
                                                                                      strip.Items.Add(label)
                                                                                  End If
                                                                              Next
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/section[2]/div[2]/div[2]/ul[1]/*")
                                                                                  Dim pluginItem As New PluginListItem()
                                                                                  pluginItem.Dock = DockStyle.Fill

                                                                                  Dim iconNode As HtmlAgilityPack.HtmlNode = element.SelectSingleNode("div[1]/a[1]/img[1]")

                                                                                  Dim nameNode = element.SelectSingleNode("div[2]/div[1]/div[1]/a[1]")
                                                                                  pluginItem.pluginIcon.Cursor = Cursors.Hand
                                                                                  If IsNothing(iconNode) = False Then
                                                                                      pluginItem.pluginIcon.ImageLocation = iconNode.GetAttributeValue("src", "https://media.forgecdn.net/avatars/thumbnails/65/443/48/48/636162895990633284.png")
                                                                                  Else
                                                                                      pluginItem.pluginIcon.Image = My.Resources.bukkit
                                                                                  End If
                                                                                  AddHandler pluginItem.pluginIcon.Click, Sub()
                                                                                                                              LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.SelectSingleNode("div[2]/div[1]/div[1]/a[1]").GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.FeedTheBeast_ModpackMainPage, parent)
                                                                                                                              pluginName = Net.WebUtility.HtmlDecode(nameNode.InnerText)
                                                                                                                          End Sub
                                                                                  pluginItem.pluginName.Text = Net.WebUtility.HtmlDecode(nameNode.InnerText)
                                                                                  pluginItem.pluginName.Cursor = Cursors.Hand
                                                                                  pluginItem.pluginName.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 18, GraphicsUnit.Pixel)
                                                                                  AddHandler pluginItem.pluginName.Click, Sub()
                                                                                                                              LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.SelectSingleNode("div[2]/div[1]/div[1]/a[1]").GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.FeedTheBeast_ModpackMainPage, parent)
                                                                                                                              pluginName = Net.WebUtility.HtmlDecode(nameNode.InnerText)
                                                                                                                          End Sub
                                                                                  pluginItem.DescriptionLabel.Text = Net.WebUtility.HtmlDecode(element.SelectSingleNode("div[2]/div[4]/p[1]").InnerText.Trim)
                                                                                  pluginItem.DescriptionLabel.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 13, GraphicsUnit.Pixel)
                                                                                  layout.RowStyles.Add(New RowStyle(SizeType.Absolute, pluginItem.Height))
                                                                                  layout.Controls.Add(pluginItem, 0, layout.RowCount - 1)
                                                                              Next
                                                                              parent.Controls.Add(layout)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2

                    client.DownloadStringAsync(New Uri(url))
                Case RenderPageType.FeedTheBeast_ModpackMainPage

                    Dim layout As New TabControl
                    layout.Dock = DockStyle.Fill
                    Dim IntroPage As New TabPage("模組包內容")
                    layout.TabPages.Add(IntroPage)
                    Dim DownloadPage As New TabPage("模組包下載")
                    layout.TabPages.Add(DownloadPage)
                    parent.Controls.Add(layout)
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(mServer, pluginName, DownloadVersionList.BrowsingWebsite.FeedTheBeast_Modpack)
                                                                              table.Dock = DockStyle.Fill
                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/div[1]/div[1]/div[2]/table[1]/tbody[1]/*")
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node1Class As String = node.SelectSingleNode("td[1]/div[1]").GetAttributeValue("class", "")
                                                                                  Select Case node1Class
                                                                                      Case "release-phase tip"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "正式版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(140, 175, 98)})
                                                                                      Case "beta-phase tip"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "預覽版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(127, 165, 196)})
                                                                                  End Select
                                                                                  Dim node2 = node.SelectSingleNode("td[2]/div[1]/div[1]/a[1]")
                                                                                  Dim node2check = node.SelectSingleNode("td[2]/div[1]/div[1]/a[2]")
                                                                                  If node2check Is Nothing Then Continue For
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim())
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node2.GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  Dim node3_value As String = node.SelectSingleNode("td[3]").InnerText.Trim()
                                                                                  item.SubItems.Add(node3_value)
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[4]/abbr[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node4_value)
                                                                                  Dim node5_value As String = node.SelectSingleNode("td[5]/span[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node5_value)
                                                                                  Dim node6_value As String = node.SelectSingleNode("td[6]").InnerText.Trim()
                                                                                  item.SubItems.Add(node6_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim node = parser.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/div[1]/div[1]/div[2]")
                                                                              Dim headNode = parser.DocumentNode.SelectSingleNode("/html[1]/head[1]")
                                                                              Dim p As New HtmlRenderer.WinForms.HtmlPanel
                                                                              p.Dock = DockStyle.Fill
                                                                              p.AutoScroll = True
                                                                              p.Text = "<html>" & headNode.OuterHtml & "<body>" & node.OuterHtml & "</body></html>"
                                                                              IntroPage.Controls.Add(p)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                              AddHandler client.DownloadProgressChanged, e3
                                                                              AddHandler client.DownloadStringCompleted, e4
                                                                              client.DownloadStringAsync(New Uri(url & "/files"))
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2
                    client.DownloadStringAsync(uri)
                Case RenderPageType.FeedTheBeast_ModpackDownloadListPage
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e3
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              Dim DownloadPage = CType(parent.Controls(0), TabControl).TabPages.Item(1)
                                                                              DownloadPage.Controls.Clear()
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(mServer, pluginName, DownloadVersionList.BrowsingWebsite.FeedTheBeast_Modpack)
                                                                              table.Dock = DockStyle.Fill
                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/table[1]/tbody[1]/*")
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node1Class As String = node.SelectSingleNode("td[1]/div[1]").GetAttributeValue("class", "")
                                                                                  Select Case node1Class
                                                                                      Case "release-phase tip"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "正式版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(140, 175, 98)})
                                                                                      Case "beta-phase tip"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "預覽版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(127, 165, 196)})
                                                                                  End Select
                                                                                  Dim node2 = node.SelectSingleNode("td[2]/div[1]/div[1]/a[1]")
                                                                                  Dim node2check = node.SelectSingleNode("td[2]/div[1]/div[1]/a[2]")
                                                                                  If node2check Is Nothing Then Continue For
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim())
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node2.GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  Dim node3_value As String = node.SelectSingleNode("td[3]").InnerText.Trim()
                                                                                  item.SubItems.Add(node3_value)
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[4]/abbr[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node4_value)
                                                                                  Dim node5_value As String = node.SelectSingleNode("td[5]/span[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node5_value)
                                                                                  Dim node6_value As String = node.SelectSingleNode("td[6]").InnerText.Trim()
                                                                                  item.SubItems.Add(node6_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)

                    AddHandler client.DownloadStringCompleted, e4
                    client.DownloadStringAsync(New Uri(url))

#End Region
#Region "www.curseforge.com/minecraft/modpacks"
                Case RenderPageType.CurseForge_ModpackListPage
                    pluginName = ""
                    Dim layout As New TableLayoutPanel
                    layout.Dock = DockStyle.Fill
                    layout.AutoScroll = True
                    layout.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim strip As New ToolStrip
                                                                              strip.Height = 27
                                                                              layout.RowStyles.Add(New RowStyle(SizeType.Absolute, 27))
                                                                              layout.Controls.Add(strip, 0, 0)
                                                                              Dim node = parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]/*")
                                                                              If node IsNot Nothing Then
                                                                                  For Each element In node
                                                                                      Dim label As ToolStripItem = Nothing
                                                                                      Select Case element.Name
                                                                                          Case "span"
                                                                                              label = New ToolStripLabel
                                                                                              label.Text = element.InnerText.Replace("&hellip;", "...")
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.AutoSize = True
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          Case "a"
                                                                                              Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                              label = New ToolStripButton
                                                                                              label.AutoToolTip = False
                                                                                              label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                              label.Text = innerElement.InnerText
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.AutoSize = True
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                              AddHandler label.Click, Sub()
                                                                                                                          LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_ModpackListPage, parent)
                                                                                                                      End Sub
                                                                                              label.ForeColor = SystemColors.HotTrack
                                                                                      End Select
                                                                                      If IsNothing(label) = False Then
                                                                                          strip.Items.Add(label)
                                                                                      End If
                                                                                  Next
                                                                              End If
                                                                              Dim nodeBody = parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[3]/div[1]/*")
                                                                              If nodeBody IsNot Nothing Then
                                                                                  For Each element In nodeBody
                                                                                      Dim pluginItem As New PluginListItem()
                                                                                      pluginItem.Dock = DockStyle.Fill

                                                                                      Dim iconNode As HtmlAgilityPack.HtmlNode = element.SelectSingleNode("div[1]/div[1]/div[1]/div[1]/a[1]/img[1]")

                                                                                      Dim nameNode = element.SelectSingleNode("div[1]/div[1]/div[2]/a[1]/h3[1]")
                                                                                      pluginItem.pluginIcon.Cursor = Cursors.Hand
                                                                                      If IsNothing(iconNode) = False Then
                                                                                          pluginItem.pluginIcon.ImageLocation = iconNode.GetAttributeValue("src", "https://media.forgecdn.net/avatars/thumbnails/65/443/48/48/636162895990633284.png")
                                                                                      Else
                                                                                          pluginItem.pluginIcon.Image = My.Resources.bukkit
                                                                                      End If
                                                                                      AddHandler pluginItem.pluginIcon.Click, Sub()
                                                                                                                                  LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.SelectSingleNode("div[1]/div[1]/div[2]/a[1]").GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_ModpackMainPage, parent)
                                                                                                                                  pluginName = Net.WebUtility.HtmlDecode(nameNode.InnerText.Trim)
                                                                                                                              End Sub
                                                                                      pluginItem.pluginName.Text = Net.WebUtility.HtmlDecode(nameNode.InnerText.Trim)
                                                                                      pluginItem.pluginName.Cursor = Cursors.Hand
                                                                                      pluginItem.pluginName.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 18, GraphicsUnit.Pixel)
                                                                                      AddHandler pluginItem.pluginName.Click, Sub()
                                                                                                                                  LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.SelectSingleNode("div[1]/div[1]/div[2]/a[1]").GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_ModpackMainPage, parent)
                                                                                                                                  pluginName = Net.WebUtility.HtmlDecode(nameNode.InnerText.Trim)
                                                                                                                              End Sub
                                                                                      pluginItem.DescriptionLabel.Text = Net.WebUtility.HtmlDecode(element.SelectSingleNode("div[1]/div[2]/p[1]").InnerText.Trim)
                                                                                      pluginItem.DescriptionLabel.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), 13, GraphicsUnit.Pixel)
                                                                                      layout.RowStyles.Add(New RowStyle(SizeType.Absolute, pluginItem.Height))
                                                                                      layout.Controls.Add(pluginItem, 0, layout.RowCount - 1)
                                                                                  Next
                                                                              End If
                                                                              parent.Controls.Add(layout)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2

                    client.DownloadStringAsync(New Uri(url))
                Case RenderPageType.CurseForge_ModpackMainPage

                    Dim layout As New TabControl
                    layout.Dock = DockStyle.Fill
                    Dim IntroPage As New TabPage("模組包內容")
                    layout.TabPages.Add(IntroPage)
                    Dim DownloadPage As New TabPage("模組包下載")
                    layout.TabPages.Add(DownloadPage)
                    parent.Controls.Add(layout)
                    Dim e1 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e1
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(mServer, pluginName, DownloadVersionList.BrowsingWebsite.Curse_Modpack)
                                                                              table.Dock = DockStyle.Fill
                                                                              Dim stripElements = parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[3]/div[1]/*")
                                                                              If stripElements IsNot Nothing Then
                                                                                  For Each element In stripElements
                                                                                      Dim label As ToolStripItem = Nothing
                                                                                      Select Case element.Name
                                                                                          Case "span"
                                                                                              label = New ToolStripLabel
                                                                                              label.Text = element.InnerText.Replace("&hellip;", "...")
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.AutoSize = True
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          Case "a"
                                                                                              Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                              label = New ToolStripButton
                                                                                              label.AutoToolTip = False
                                                                                              label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                              label.Text = innerElement.InnerText
                                                                                              ' label.Location = New Point(1, 1)
                                                                                              label.TextAlign = ContentAlignment.MiddleCenter
                                                                                              label.AutoSize = True
                                                                                              label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                              AddHandler label.Click, Sub()
                                                                                                                          LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_ModpackDownloadListPage, parent)
                                                                                                                      End Sub
                                                                                              label.ForeColor = SystemColors.HotTrack
                                                                                      End Select
                                                                                      If IsNothing(label) = False Then
                                                                                          table.NaviBar.Items.Add(label)
                                                                                      End If
                                                                                  Next
                                                                              End If
                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/table[1]/tbody[1]/*")
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node1Class As String = node.SelectSingleNode("td[1]/div[1]").GetAttributeValue("class", "")
                                                                                  Select Case node1Class
                                                                                      Case "w-5 h-5 bg-green-500 flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "正式版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(140, 175, 98)})
                                                                                      Case "w-5 h-5 bg-prime-blue flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "預覽版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(127, 165, 196)})
                                                                                      Case "w-5 h-5 bg-twitch-offset flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "開發版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(211, 202, 232)})
                                                                                  End Select
                                                                                  Dim node2 = node.SelectSingleNode("td[2]/a[1]")
                                                                                  Dim node2checking = node.SelectSingleNode("td[2]/a[2]")
                                                                                  If node2checking Is Nothing Then Continue For
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim())
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node2.GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  Dim node3_value As String = node.SelectSingleNode("td[3]").InnerText.Trim()
                                                                                  item.SubItems.Add(node3_value)
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[4]/abbr[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node4_value)
                                                                                  Dim node5_value As String = node.SelectSingleNode("td[5]/div[1]/div[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node5_value)
                                                                                  Dim node6_value As String = node.SelectSingleNode("td[6]").InnerText.Trim()
                                                                                  item.SubItems.Add(node6_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)
                    Dim e2 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim node = parser.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[1]")
                                                                              Dim headNode = parser.DocumentNode.SelectSingleNode("/html[1]/head[1]")
                                                                              Dim p As New HtmlRenderer.WinForms.HtmlPanel
                                                                              p.Dock = DockStyle.Fill
                                                                              p.AutoScroll = True
                                                                              p.Text = "<html>" & headNode.OuterHtml & "<body>" & node.OuterHtml & "</body></html>"
                                                                              IntroPage.Controls.Add(p)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e1
                                                                              RemoveHandler client.DownloadStringCompleted, e2
                                                                              AddHandler client.DownloadProgressChanged, e3
                                                                              AddHandler client.DownloadStringCompleted, e4
                                                                              client.DownloadStringAsync(New Uri(url & "/files/all"))
                                                                          End Sub)
                    AddHandler client.DownloadStringCompleted, e2
                    client.DownloadStringAsync(uri)
                Case RenderPageType.CurseForge_ModpackDownloadListPage
                    Dim e3 As New Net.DownloadProgressChangedEventHandler(Sub(sender, e)
                                                                              RaiseEvent DownloadProgressChanged(sender, e)
                                                                          End Sub)
                    AddHandler client.DownloadProgressChanged, e3
                    Dim e4 As New Net.DownloadStringCompletedEventHandler(Sub(sender, e)
                                                                              Dim DownloadPage = CType(parent.Controls(0), TabControl).TabPages.Item(1)
                                                                              DownloadPage.Controls.Clear()
                                                                              RaiseEvent DownloadCompleted(sender, e)
                                                                              parser.LoadHtml(e.Result)
                                                                              Dim table As New DownloadVersionList(mServer, pluginName, DownloadVersionList.BrowsingWebsite.Curse_Modpack)
                                                                              table.Dock = DockStyle.Fill
                                                                              For Each element In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[1]/*")
                                                                                  Dim label As ToolStripItem = Nothing
                                                                                  Select Case element.Name
                                                                                      Case "span"
                                                                                          label = New ToolStripLabel
                                                                                          label.Text = element.InnerText.Replace("&hellip;", "...")
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.AutoSize = True
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                      Case "a"
                                                                                          Dim innerElement As HtmlAgilityPack.HtmlNode = element.FirstChild
                                                                                          label = New ToolStripButton
                                                                                          label.AutoToolTip = False
                                                                                          label.DisplayStyle = ToolStripItemDisplayStyle.Text
                                                                                          label.Text = innerElement.InnerText
                                                                                          ' label.Location = New Point(1, 1)
                                                                                          label.TextAlign = ContentAlignment.MiddleCenter
                                                                                          label.AutoSize = True
                                                                                          label.Font = New Font(New FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), 12, System.Drawing.GraphicsUnit.Pixel)
                                                                                          AddHandler label.Click, Sub()
                                                                                                                      LoadPage(New Uri(New Uri("https://" & uri.DnsSafeHost), element.GetAttributeValue("href", "")).AbsoluteUri, RenderPageType.CurseForge_ModDownloadListPage, parent)
                                                                                                                  End Sub
                                                                                          label.ForeColor = SystemColors.HotTrack
                                                                                  End Select
                                                                                  If IsNothing(label) = False Then
                                                                                      table.NaviBar.Items.Add(label)
                                                                                  End If
                                                                              Next

                                                                              For Each node In parser.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/table[1]/tbody[1]/*")
                                                                                  Dim item As New ListViewItem("")
                                                                                  Dim node1Class As String = node.SelectSingleNode("td[1]/div[1]").GetAttributeValue("class", "")
                                                                                  Select Case node1Class
                                                                                      Case "w-5 h-5 bg-green-500 flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "正式版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(140, 175, 98)})
                                                                                      Case "w-5 h-5 bg-prime-blue flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "預覽版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(127, 165, 196)})
                                                                                      Case "w-5 h-5 bg-twitch-offset flex items-center justify-center text-white mx-auto rounded-sm"
                                                                                          item.SubItems.Item(0) = (New ListViewItem.ListViewSubItem(item, "開發版") With {.ForeColor = Color.White, .BackColor = Color.FromArgb(211, 202, 232)})
                                                                                  End Select
                                                                                  Dim node2 = node.SelectSingleNode("td[2]/a[1]")
                                                                                  Dim item2 = New ListViewItem.ListViewSubItem(item, node2.InnerText.Trim())
                                                                                  table.DownloadList.Add(New Uri(New Uri("https://" & uri.DnsSafeHost), node2.GetAttributeValue("href", "")).AbsoluteUri)
                                                                                  item2.ForeColor = SystemColors.HotTrack
                                                                                  item.SubItems.Add(item2)
                                                                                  Dim node3_value As String = node.SelectSingleNode("td[3]").InnerText.Trim()
                                                                                  item.SubItems.Add(node3_value)
                                                                                  Dim node4_value As String = node.SelectSingleNode("td[4]/abbr[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node4_value)
                                                                                  Dim node5_value As String = node.SelectSingleNode("td[5]/div[1]/div[1]").InnerText.Trim()
                                                                                  item.SubItems.Add(node5_value)
                                                                                  Dim node6_value As String = node.SelectSingleNode("td[6]").InnerText.Trim()
                                                                                  item.SubItems.Add(node6_value)
                                                                                  table.VersionList.Items.Add(item)
                                                                              Next
                                                                              DownloadPage.Controls.Add(table)
                                                                              RaiseEvent NavigationEnded(Me, New EventArgs)
                                                                              RemoveHandler client.DownloadProgressChanged, e3
                                                                              RemoveHandler client.DownloadStringCompleted, e4
                                                                          End Sub)

                    AddHandler client.DownloadStringCompleted, e4
                    client.DownloadStringAsync(New Uri(url))

#End Region
            End Select
        Catch ex As Exception
        End Try
    End Sub
End Class
