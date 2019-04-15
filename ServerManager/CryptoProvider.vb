Imports System.IO
Imports System.Net.Sockets
Imports System.Security.Cryptography
Imports System.Text
' https://www.youtube.com/watch?v=vGc31AoqdoE by MIGUEL GALANG
Public Class CryptoProvider
    Friend Shared Function GetSHA1(filename As String) As String
        Dim SHA1 As New SHA1CryptoServiceProvider
        Dim F As New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
        SHA1.ComputeHash(F)

        Dim Hash As Byte() = SHA1.Hash
        Dim Buff As New StringBuilder
        Dim HashByte As Byte

        For Each HashByte In Hash
            Buff.Append(String.Format("{0:X2}", HashByte))
        Next

        Return Buff.ToString.ToLower


    End Function

End Class
