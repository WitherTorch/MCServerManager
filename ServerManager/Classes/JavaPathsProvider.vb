Public Class JavaPathsProvider
    Structure JavaInfo
        Public Property Path As String
        Public Property Name As String
    End Structure
    Shared Function GetJavaList() As List(Of JavaInfo)
        Dim result As New List(Of JavaInfo)
        Try
            Dim jrekey As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\JavaSoft\Java Runtime Environment")
            For Each subKeyName In jrekey.GetSubKeyNames
                Dim subKey = jrekey.OpenSubKey(subKeyName)
                If subKey.GetSubKeyNames.Contains("MSI") Then
                    result.Add(New JavaInfo() With {.Path = IO.Path.Combine(subKey.GetValue("JavaHome"), "bin"), .Name = "JRE " & subKeyName})
                End If
            Next
        Catch ex As Exception
        End Try
        Try
            Dim jdkkey As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\JavaSoft\Java Development Kit")
            For Each subKeyName In jdkkey.GetSubKeyNames
                Dim subKey = jdkkey.OpenSubKey(subKeyName)
                If subKey.GetSubKeyNames.Contains("MSI") Then
                    result.Add(New JavaInfo() With {.Path = IO.Path.Combine(subKey.GetValue("JavaHome"), "jre\bin"), .Name = "JDK " & subKeyName})
                End If
            Next
        Catch ex As Exception
        End Try
        If Environment.Is64BitOperatingSystem Then
            Try
                Dim jrekey32 As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\JavaSoft\Java Runtime Environment")
                For Each subKeyName In jrekey32.GetSubKeyNames
                    Dim subKey = jrekey32.OpenSubKey(subKeyName)
                    If subKey.GetSubKeyNames.Contains("MSI") Then
                        result.Add(New JavaInfo() With {.Path = IO.Path.Combine(subKey.GetValue("JavaHome"), "bin"), .Name = "JRE " & subKeyName & " (32 位元)"})
                    End If
                Next
            Catch ex As Exception
            End Try
            Try
                Dim jdkkey32 As Microsoft.Win32.RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\JavaSoft\Java Development Kit")
                For Each subKeyName In jdkkey32.GetSubKeyNames
                    Dim subKey = jdkkey32.OpenSubKey(subKeyName)
                    If subKey.GetSubKeyNames.Contains("MSI") Then
                        result.Add(New JavaInfo() With {.Path = IO.Path.Combine(subKey.GetValue("JavaHome"), "jre\bin"), .Name = "JDK " & subKeyName & " (32 位元)"})
                    End If
                Next
            Catch ex As Exception
            End Try
        End If
        Return result
    End Function
End Class
