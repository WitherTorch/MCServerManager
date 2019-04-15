Imports System.Text.RegularExpressions

Friend Class ServerInfoMaker

    Shared Sub Build(serverPath As String, serverType As Server.EServerVersionType, serverVersion As String, Optional server2ndVersion As String = "", Optional server3rdVersion As String = "", Optional spongeType As SpongeVersionType = SpongeVersionType.None)
        Dim server As Server = Server.GetServer(serverPath)
        Select Case serverType
            Case Server.EServerVersionType.Vanilla
                server.SetVersionType(Server.EServerType.Java, serverType)
            Case Server.EServerVersionType.Forge
                server.SetVersionType(Server.EServerType.Java, serverType)
            Case Server.EServerVersionType.Spigot
                server.SetVersionType(Server.EServerType.Java, serverType)
            Case Server.EServerVersionType.CraftBukkit
                server.SetVersionType(Server.EServerType.Java, serverType)
            Case Server.EServerVersionType.SpongeVanilla
                server.SetVersionType(Server.EServerType.Java, serverType)
            Case Server.EServerVersionType.Nukkit
                server.SetVersionType(Server.EServerType.Bedrock, serverType)
        End Select
        server.SetVersion(serverVersion, server2ndVersion, server3rdVersion, spongeType)
        server.SaveServer(False)
    End Sub
End Class
