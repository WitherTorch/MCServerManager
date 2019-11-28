Imports System.Runtime.InteropServices

Public Class WinAPI
    Public Shared Sub MoveForm(handle As IntPtr)
        ReleaseCapture()
        SendMessage(handle, WM_NCLBUTTONDOWN, 2, 0)
    End Sub
    <DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function
    <DllImport("user32.dll")>
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    Public Const WM_NCLBUTTONDOWN As Integer = &HA1
End Class
