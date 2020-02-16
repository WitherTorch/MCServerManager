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
    <DllImport("imm32.dll", EntryPoint:="ImmGetContext")>
    Private Shared Function ImmGetContext(ByVal hwnd As IntPtr) As IntPtr
    End Function
    <DllImport("imm32.dll", EntryPoint:="ImmReleaseContext")>
    Private Shared Function ImmReleaseContext(ByVal hwnd As IntPtr, ByVal himc As IntPtr) As IntPtr
    End Function
    <DllImport("imm32.dll", EntryPoint:="ImmSetOpenStatus", CharSet:=CharSet.Unicode)>
    Private Shared Function ImmGetCompositionStringW(ByVal himc As IntPtr, ByVal dwIndex As Integer, ByVal lpBuf As Byte(), ByVal dwBufLen As Integer) As Boolean
    End Function
    Public Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const GCS_COMPSTR As Integer = 8

    Public Shared Function CurrentCompStr(ByVal handle As IntPtr) As String
        Dim readType As Integer = GCS_COMPSTR
        Dim hIMC As IntPtr = ImmGetContext(handle)

        Try
            Dim strLen As Integer = ImmGetCompositionStringW(hIMC, readType, Nothing, 0)

            If strLen > 0 Then
                Dim buffer As Byte() = New Byte(strLen - 1) {}
                ImmGetCompositionStringW(hIMC, readType, buffer, strLen)
                Return System.Text.Encoding.Unicode.GetString(buffer)
            Else
                Return String.Empty
            End If

        Finally
            ImmReleaseContext(handle, hIMC)
        End Try
    End Function
End Class
