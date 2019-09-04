Imports ServerManager

Public Class CraftBukkitServer
    Inherits VanillaServer
    Dim bukkitOptions As BukkitOptions
    Protected Friend Overrides Sub SetMainServerOptions()
        MyBase.SetMainServerOptions()
        bukkitOptions = New BukkitOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
    End Sub
    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Dim items As New List(Of AbstractSoftwareOptions)
        items.AddRange(MyBase.GetOptionObjects)
        items.Add(bukkitOptions)
        Return items.ToArray()
    End Function
    Public Overrides Function DownloadServer() As ServerDownloadTask
        Dim targetURL As String = CraftBukkitVersionDict(ServerVersion)
        Dim url = (New HtmlAgilityPack.HtmlWeb).Load(targetURL).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[4]/div[1]/div[1]/div[1]/div[1]/h2[1]/a[1]").GetAttributeValue("href", "")
        ' My.Computer.Network.DownloadFile(
        'New Uri(String.Format("https://cdn.getbukkit.org/craftbukkit/craftbukkit-{0}.jar", server.ServerVersion)),
        'IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "craftbukkit-" & server.ServerVersion & ".jar"), "", "", True, 100000, True, FileIO.UICancelOption.DoNothing)
        DownloadFile(url, IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "craftbukkit-" & Server.ServerVersion & ".jar"), Server.EServerVersionType.CraftBukkit, Server.ServerVersion)

    End Function
End Class
