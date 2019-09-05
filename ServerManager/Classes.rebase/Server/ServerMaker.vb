Public Class ServerMaker
    Shared Function MakeServer(Of T As ServerBase)() As T
        Return Activator.CreateInstance(GetType(T))
    End Function
    Shared Function GetServer(Of T As ServerBase)(path) As T
        Return Activator.CreateInstance(GetType(T), path)
    End Function
End Class
