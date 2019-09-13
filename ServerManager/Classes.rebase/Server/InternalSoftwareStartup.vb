Public Class InternalSoftwareStartup
    Shared Sub Startup()
        ' 登錄版本載入程式
        VersionListLoader.RegisterVersionListFunction(Of VanillaServer)(AddressOf VanillaServer.GetVersionList)
        VersionListLoader.RegisterVersionListFunction(Of CraftBukkitServer)(AddressOf CraftBukkitServer.GetVersionList)
        VersionListLoader.RegisterVersionListFunction(Of SpigotServer)(AddressOf SpigotServer.GetVersionList)
        VersionListLoader.RegisterVersionListFunction(Of SpigotWithGitServer)(AddressOf SpigotWithGitServer.GetVersionList)
        VersionListLoader.RegisterVersionListFunction(Of PaperServer)(AddressOf PaperServer.GetVersionList)
        VersionListLoader.RegisterVersionListFunction(Of AkarinServer)(AddressOf AkarinServer.GetVersionList)
        ' 登錄伺服器軟體
        ServerMaker.RegisterServerSoftware(Of VanillaServer)("Vanilla", "原版 (Java)")
        ServerMaker.RegisterServerSoftware(Of CraftBukkitServer)("CraftBukkit", "CraftBukkit")
        ServerMaker.RegisterServerSoftware(Of SpigotServer)("Spigot", "Spigot")
        ServerMaker.RegisterServerSoftware(Of SpigotWithGitServer)("Spigot_Git", "Spigot")
        ServerMaker.RegisterServerSoftware(Of PaperServer)("Paper", "Paper")
        ServerMaker.RegisterServerSoftware(Of AkarinServer)("Akarin", "Akarin")
    End Sub
End Class
