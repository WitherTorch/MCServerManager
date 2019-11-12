Imports System.Reflection
Imports System.Reflection.Emit

Public Class SkriptLoader
    Public Shared TaskActions As Dictionary(Of String, SkriptFile)
    Sub LoadSkript(filepath As String)
        If IO.File.Exists(filepath) Then
            Dim text As String = IO.File.ReadAllText(filepath)
            Dim task As New SkriptFile(filepath, text)
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
                If node.Parent Is Nothing Then
                    task.Tasks.Add(CreateSkriptTaskFromTopNode(node))
                Else
                    Continue For
                End If
            Next
            nodes.Clear()
            GC.Collect()
        End If
    End Sub
    Private Function CreateSkriptTaskFromTopNode(node As ScriptNode) As SkriptTask
        Dim triggerName, triggerValue As String
        Select Case node.Value
            Case "on startup:"
                triggerName = "startup_server"
            Case "on shutdown:"
                triggerName = "shutdown_server"
            Case "on connect:"
                triggerName = "connect_player"
            Case "on disconnect:"
                triggerName = "disconnect_player"
            Case "on join:"
                triggerName = "join_player"
            Case "on quit:"
                triggerName = "quit_player"
            Case "on lag:"
                triggerName = "server_lag"
            Case Else
                If node.Value.StartsWith("on command ") Then
                    triggerName = "player_command"
                    triggerValue = node.Value.Substring(11).TrimStart
                ElseIf node.Value.StartsWith("at time ") Then
                    triggerName = "at_time"
                    triggerValue = node.Value.Substring(8).TrimStart
                ElseIf node.Value.StartsWith("every ") Then
                    triggerName = "period"
                    triggerValue = node.Value.Substring(8).TrimStart
                End If
                triggerValue = triggerValue.Substring(0, triggerValue.Length - 1)
        End Select
        Dim methodAndArgs As (String(), DynamicMethod) = CreateILMethodFromTopNode(node)
        Return New SkriptTask(methodAndArgs.Item2, methodAndArgs.Item1, New KeyValuePair(Of String, String)(triggerName, triggerValue))
    End Function

    Private Function CreateILMethodFromTopNode(node As ScriptNode) As (String(), DynamicMethod)
        Dim result As New DynamicMethod("", Nothing, {GetType(Dictionary(Of String, String))})
        Dim generator As ILGenerator = result.GetILGenerator(256)
        Dim returnLabel As Label = generator.DefineLabel()
        Dim requirement As New List(Of String)
        requirement.Add("current_time")
        requirement.Add("server_name")
        Select Case node.Value
            Case "on connect:"
                requirement.Add("player")
            Case "on disconnect:"
                requirement.Add("player")
            Case "on join:"
                requirement.Add("player")
            Case "on quit:"
                requirement.Add("player")
            Case "on lag:"
                requirement.Add("skip_ticks")
            Case Else
                If node.Value.StartsWith("on command ") Then
                    requirement.Add("command")
                    requirement.Add("arguments")
                End If
        End Select
        ' generator.Emit(OpCodes.Ldarg_1)
        'generator.Emit(OpCodes.Ldstr, "command")
        ' generator.EmitCall(OpCodes.Callvirt, GetType(Dictionary(Of String, String)).GetMethod("get_Item", {GetType(String)}), {GetType(String)})
        'generator.Emit(OpCodes.Ldstr, node.Value.Substring(11).TrimStart)
        ' generator.Emit(OpCodes.Ldc_I4_1)
        For Each child In node.Childs
            GenerateCodeFromNode(generator, child)
        Next
        generator.MarkLabel(returnLabel)
        generator.Emit(OpCodes.Ret)
    End Function
    Private Sub GenerateCodeFromNode(ByRef generator As ILGenerator, node As ScriptNode)
        Dim nodeValueLower As String = node.Value.ToLower()
        If nodeValueLower.StartsWith("if") And nodeValueLower.EndsWith(":") Then
            nodeValueLower = nodeValueLower.Substring(2, nodeValueLower.Length - 3)
        End If
    End Sub
#Region "MSIL Code Block Generator"
    Private Sub GenerateStringCompare(ByRef generator As ILGenerator, stringA As String, stringB As String, ByRef returnLabel As Label)
        generator.Emit(OpCodes.Ldstr, stringA)
        generator.Emit(OpCodes.Ldstr, stringB)
        generator.Emit(OpCodes.Ldc_I4_1)
        generator.EmitCall(OpCodes.Call, GetType(String).GetMethod("Compare", {GetType(String), GetType(String), GetType(Boolean)}), {GetType(String), GetType(String), GetType(Boolean)})
        generator.Emit(OpCodes.Ldc_I4_0)
        generator.Emit(OpCodes.Ceq)
        generator.Emit(OpCodes.Stloc_0)
        generator.Emit(OpCodes.Ldloc_0)
        generator.Emit(OpCodes.Brfalse_S, returnLabel)
    End Sub
#End Region
    Public Sub Test(d As Dictionary(Of String, String))
        If Date.Now = Date.Parse("2016/10/28") Then
            GUIHost.GUIHandler.MsgBox("Do something")
        End If
    End Sub
End Class
Public Class SkriptFile
    Public Property Path As String
    Public Property Source As String
    Public Property Tasks As New List(Of SkriptTask)
    Sub New(path As String, source As String)
        Me.Path = path
        Me.Source = source
    End Sub
End Class
Public Class SkriptTask
    Public Property Method As DynamicMethod
    Public Property MethodRequirement As String()
    Public Property Trigger As KeyValuePair(Of String, String)
    Sub New(method As DynamicMethod, methodRequirement As String(), trigger As KeyValuePair(Of String, String))
        Me.Method = method
        Me.MethodRequirement = methodRequirement
        Me.Trigger = trigger
    End Sub
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
