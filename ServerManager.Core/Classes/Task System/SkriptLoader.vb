Public Class SkriptLoader
    Public Shared TaskActions As Dictionary(Of String, SkriptTask)

End Class
Public Structure SkriptTask
    Public NeedArguments As String()
    Public TaskProgram As Action(Of String())
End Structure
