Public Interface ICMDWindow
    Sub Run(program As String, arguments As String, workingDirectory As String)
    Function ShowDialog() As DialogResult
    Property Text As String
End Interface
