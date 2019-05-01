Imports Newtonsoft.Json.Linq
Imports YamlDotNet.Serialization

Class BukkitPluginUnpatcher
    Implements IDisposable
    Dim pack As IO.Compression.ZipArchive
    Structure PluginInfo
        Friend Name As String
        Friend Version As String
        Friend IsNull As Boolean
        Sub New(Optional null As Boolean = True)
            IsNull = null
        End Sub
        Sub New(pluginName As String, pluginVersion As String)
            Name = pluginName
            Version = pluginVersion
            IsNull = False
        End Sub
    End Structure
    Sub New(path As String)
        pack = IO.Compression.ZipFile.OpenRead(path)
    End Sub
    Function GetPluginInfo() As PluginInfo
        Try
            Using reader As New IO.StreamReader(pack.GetEntry("plugin.yml").Open())
                Dim deserializer = New DeserializerBuilder().Build()
                Dim yamlObject = deserializer.Deserialize(reader)
                deserializer = Nothing
                Dim serializer = New SerializerBuilder().JsonCompatible().Build()
                Dim jsonString = serializer.Serialize(yamlObject)
                serializer = Nothing
                Dim jsonObject As JObject = GetDeserialisedObject(jsonString)
                Dim name As JValue = ""
                Dim version As JValue = ""
                If jsonObject.TryGetValue("name", name) AndAlso jsonObject.TryGetValue("version", version) Then
                    Return New PluginInfo(name, version)
                Else
                    Return New PluginInfo(True)
                End If
            End Using
        Catch ex As Exception
            Return New PluginInfo(True)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 處置受控狀態 (受控物件)。
                If pack IsNot Nothing Then
                    pack.Dispose()
                End If
            End If

            ' TODO: 釋放非受控資源 (非受控物件) 並覆寫下方的 Finalize()。
            ' TODO: 將大型欄位設為 null。
        End If
        disposedValue = True
    End Sub

    ' TODO: 只有當上方的 Dispose(disposing As Boolean) 具有要釋放非受控資源的程式碼時，才覆寫 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 請勿變更這個程式碼。請將清除程式碼放在上方的 Dispose(disposing As Boolean) 中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 加入這個程式碼的目的，在於能正確地實作可處置的模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更這個程式碼。請將清除程式碼放在上方的 Dispose(disposing As Boolean) 中。
        Dispose(True)
        ' TODO: 覆寫上列 Finalize() 時，取消下行的註解狀態。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
