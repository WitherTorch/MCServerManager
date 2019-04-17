Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Module JSONSafeModule
    Enum TripleStatus
        [Default]
        [True]
        [False]
    End Enum
    Friend Function GetTripleStatus(boolString As String) As TripleStatus
        Select Case boolString.ToLower
            Case "true"
                Return TripleStatus.True
            Case "false"
                Return TripleStatus.False
            Case "default"
                Return TripleStatus.Default
            Case Else
                Throw New InvalidCastException
        End Select
    End Function
    Friend Sub SetPropertyValue(jsonObject As JObject, key As String, value As JToken)
        If jsonObject.ContainsKey(key) Then
            jsonObject.Item(key) = value
        Else
            jsonObject.Add(key, value)
        End If
    End Sub
    Friend Sub InputPropertyValue(jsonObject As JObject, key As String, ByRef target As Boolean)
        Try
            target = GetBoolean(jsonObject.GetValue(key))
        Catch ex As Exception

        End Try
    End Sub
    Friend Sub InputPropertyValue(jsonObject As JObject, key As String, ByRef target As String)
        Try
            target = jsonObject.GetValue(key)
        Catch ex As Exception

        End Try
    End Sub
    Friend Sub InputPropertyValue(jsonObject As JObject, key As String, ByRef target As Integer)
        Try
            target = jsonObject.GetValue(key)
        Catch ex As Exception

        End Try
    End Sub
    Friend Sub InputPropertyValue(jsonObject As JObject, key As String, ByRef target As Single)
        Try
            target = jsonObject.GetValue(key)
        Catch ex As Exception

        End Try
    End Sub
    Friend Sub InputPropertyValue(jsonObject As JObject, key As String, ByRef target As Double)
        Try
            target = jsonObject.GetValue(key)
        Catch ex As Exception

        End Try
    End Sub
    Friend Sub InputPropertyValue(jsonObject As JObject, key As String, ByRef target As Decimal)
        Try
            target = jsonObject.GetValue(key)
        Catch ex As Exception

        End Try
    End Sub
    Friend Sub InputPropertyValue(jsonObject As JObject, key As String, ByRef target As String())
        Try
            Dim token = jsonObject.GetValue(key)
            If token IsNot Nothing Then
                Dim arr = TryCast(token, JArray)
                If arr IsNot Nothing Then
                    Dim str_arrayList As New List(Of String)
                    For Each item In arr
                        str_arrayList.Add(item)
                    Next
                    target = str_arrayList.ToArray
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Friend Sub InputPropertyValue(jsonObject As JObject, key As String, ByRef target As List(Of String))
        Try
            Dim token = jsonObject.GetValue(key)
            If token IsNot Nothing Then
                Dim arr = TryCast(token, JArray)
                If arr IsNot Nothing Then
                    Dim str_arrayList As New List(Of String)
                    For Each item In arr
                        str_arrayList.Add(item)
                    Next
                    target = str_arrayList
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Friend Function GetJsonObject(jsonObject As JObject, key As String) As JObject
        If jsonObject.ContainsKey(key) Then
            Try
                Return CType(jsonObject.Item(key), JObject)
            Catch ex As Exception
                Return New JObject
            End Try
        Else
            Return New JObject
        End If
    End Function
    Friend Function GetDeserialisedObject(jsonString As String) As JObject
        Try
            Return JsonConvert.DeserializeObject(Of JObject)(jsonString)
        Catch ex As Exception
            Return New JObject
        End Try
    End Function
    Friend Function GetBoolean(boolString As String) As Boolean
        Select Case boolString.ToLower
            Case "true"
                Return True
            Case "false"
                Return False
            Case Else
                Throw New InvalidCastException
        End Select
    End Function
End Module
