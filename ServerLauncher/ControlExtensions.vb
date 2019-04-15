Imports System.Reflection
Imports System.Runtime.CompilerServices

Module ControlExtensions
    <Extension()>
    Function Clone(Of T As Control)(ByVal controlToClone As T) As T
        Dim controlProperties As PropertyInfo() = GetType(T).GetProperties(BindingFlags.[Public] Or BindingFlags.Instance)
        Dim instance As T = Activator.CreateInstance(Of T)()

        For Each propInfo As PropertyInfo In controlProperties

            If propInfo.CanWrite Then
                Try
                    If propInfo.Name <> "WindowTarget" Then propInfo.SetValue(instance, propInfo.GetValue(controlToClone, Nothing), Nothing)
                Catch ex As Exception
                End Try
            End If
        Next

        Return instance
    End Function
End Module

