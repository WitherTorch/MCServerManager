﻿Public Class Watchdog
    Dim _process As Process
    Dim _delay As Integer = 8000
    'Default Delay 8000 ms
    Sub New(process As Process)
        _process = process
    End Sub
    Sub New(process As Process, delay As ULong)
        _process = process
        _delay = delay
    End Sub
    Sub Run()
        Dim thread As New Threading.Thread(Sub()
                                               Dim nowTime = Date.Now
                                               Do Until Date.Now >= nowTime + New TimeSpan(0, 0, 0, _delay \ 1000, _delay Mod 1000)
                                                   If _process.HasExited = True Then Exit Sub
                                               Loop
                                               'Checking Process has exited
                                               If _process.HasExited = False Then
                                                   _process.Kill()
                                               End If
                                           End Sub)
        thread.Name = "Watchdog Thread"
        thread.IsBackground = False
        thread.Start()
    End Sub
End Class