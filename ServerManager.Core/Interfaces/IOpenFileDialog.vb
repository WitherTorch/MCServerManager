Public Interface IOpenFileDialog
    Inherits IDisposable
    Property Filter As String
    Function ShowDialog() As DialogResult
    Property FileName As String
    Property Title As String
End Interface
Public Enum DialogResult
    Abort = 3
    Cancel = 2
    Ignore = 5
    No = 7
    None = 0
    OK = 1
    Retry = 4
    Yes = 6
End Enum
