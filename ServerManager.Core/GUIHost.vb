Public Class GUIHost
    Public Shared Property GUITypeCheckDict As New Dictionary(Of Type, Type)
    Public Shared Property GUIHandler As IGUIHandler
    Shared Sub SetGUIClass(Of TGUIInterface)(clazz As Type)
        GUITypeCheckDict.Add(GetType(TGUIInterface), clazz)
    End Sub
    Overloads Shared Function GenerateObjectByInterface(Of TGUIInterface)() As TGUIInterface
        Return Activator.CreateInstance(GUITypeCheckDict(GetType(TGUIInterface)))
    End Function
    Overloads Shared Function GenerateObjectByInterface(Of TGUIInterface)(ParamArray args As Object()) As TGUIInterface
        Return Activator.CreateInstance(GUITypeCheckDict(GetType(TGUIInterface)), args)
    End Function
    Shared Sub SetGUIHandler(handler As IGUIHandler)
        GUIHandler = handler
    End Sub
End Class
