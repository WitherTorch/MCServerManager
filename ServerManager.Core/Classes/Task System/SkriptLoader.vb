Public Class SkriptLoader
    Public Shared TaskActions As Dictionary(Of String, SkriptTask)
    Sub LoadSkript(text As String)
        Dim task As New SkriptTask
        Dim nodes As New List(Of ScriptNode)
        Dim nodeStack As New Stack(Of ScriptNode)
        For Each line In text.Split(vbNewLine) '建構指令樹
            Dim lineNodeLevel As Integer = 0
            For Each charator In line
                If charator.Equals(" ") Then
                    lineNodeLevel += 1
                    line = line.Remove(0, 1)
                Else
                    Exit For
                End If
            Next
            If nodeStack.Count < lineNodeLevel Then '子節點
                If nodes.Count > 0 Then
                    Dim parentNode As ScriptNode = nodes.Last
                    Dim node As New ScriptNode(line, parentNode)
                    parentNode.Childs.Add(node)
                    nodeStack.Push(parentNode)
                    nodes.Add(node)
                Else
                    Dim node As New ScriptNode(line)
                    nodes.Add(node)
                End If
            ElseIf nodeStack.Count = lineNodeLevel Then '同級節點
                If nodeStack.Count > 0 Then
                    Dim parentNode As ScriptNode = nodeStack.Peek
                    Dim node As New ScriptNode(line, parentNode)
                    parentNode.Childs.Add(node)
                    nodes.Add(node)
                Else
                    Dim node As New ScriptNode(line)
                    nodes.Add(node)
                End If
            Else '父節點
                If nodeStack.Count > 1 Then
                    nodeStack.Pop()
                    Dim parentNode As ScriptNode = nodeStack.Peek
                    Dim node As New ScriptNode(line, parentNode)
                    parentNode.Childs.Add(node)
                    nodes.Add(node)
                Else
                    Dim node As New ScriptNode(line)
                    nodes.Add(node)
                End If
            End If
        Next
        nodeStack.Clear() '釋放暫存節點堆疊資源
        For Each node In nodes '從指令樹中建構伺服器排程腳本

        Next
    End Sub
End Class
Public Class SkriptTask
    Public NeedArguments As String()
End Class
Public Class ScriptNode
    Public Property Parent As ScriptNode
    Public Property Childs As New List(Of ScriptNode)
    Public Property Value As String
    Sub New(value As String, Optional parent As ScriptNode = Nothing)
        Me.Value = value
        Me.Parent = parent
    End Sub
End Class
