Public Interface IGUIHandler
    Sub MsgBox(text As String)
    Sub MsgBox(text As String, caption As String)
    Function Shell(prompt As String, style As AppWinStyle, Optional timeout As Integer = 5000) As Integer
    Enum AppWinStyle
        Hide = 0
        MaximizedFocus = 3
        MinimizedFocus = 2
        MinimizedNoFocus = 6
        NormalFocus = 1
        NormalNoFocus = 4
    End Enum
End Interface

