Imports System

Module Program
    Friend Arguments As New Dictionary(Of String, String)
    Friend Server As MiNET.MiNetServer
    Sub Main(args As String())
        Console.WriteLine("Server Initalizing...")
        For Each arg In args
            If arg.Contains("=") Then
                Dim s As String() = arg.Split(New Char() {"="}, 2)
                Arguments.Add(s(0), s(1))
            End If
            Console.WriteLine("Loaded Argument : " & arg)
        Next
        Server = New MiNET.MiNetServer(New Net.IPEndPoint(Arguments.Item("server-ip"), Arguments.Item("server-port")))
        Console.WriteLine("Server Initalized")
        Server.StartServer()
        Console.WriteLine("Server Started")
        MiNET.Client.MiNetClient
        Dim endProgram As Boolean = False
        Do Until endProgram
            Select Case Console.ReadLine
                Case "stop"
                    Server.StopServer()

                Case "help"
                    Console.WriteLine("help - 顯示此說明")
                    Console.WriteLine("stop - 停止伺服器")
            End Select
        Loop
    End Sub

End Module
