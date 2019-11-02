Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports YamlDotNet.Serialization

''' <summary>
''' akarin.yml 的對應.NET 類別
''' </summary>
Public Class AkarinOptions
    Inherits AbstractSoftwareOptions
    Dim path As String
    Dim Config_Version As Integer = 1
#Region "Alternative"
    <DisplayName("伺服器軟體名稱")> <DefaultValue("")> <Category("附加設定")> <Description("欺騙一些會主動偵測伺服器軟體的插件。")>
    Public Property Modified_Server_Brand_Name As String = ""
    <DisplayName("重啟前回收記憶體")> <DefaultValue(True)> <Category("附加設定")> <Description("是否在伺服器重啟前回收所有垃圾記憶體。" &
                                                     vbNewLine & "注意：Spigot 的重啟機制無法在搭配伺服器管理員的情況下正常運作。")>
    Public Property Gc_Before_Stuck_Restart As Boolean = True
    <DisplayName("禁止創建終界傳送門")> <DefaultValue(False)> <Category("附加設定")> <Description("是否禁止創建終界傳送門。（仍可用部分插件來到達終界）")>
    Public Property Disable_End_Portal_Create As Boolean = False
    <DisplayName("Paper 版本快取時間")> <DefaultValue("3600s")> <Category("附加設定")> <Description("底層Paper 架構的版本快取時間。" &
                                                            vbNewLine & "單位：" &
                                                            vbNewLine & "秒=s" &
                                                            vbNewLine & "分=m" &
                                                            vbNewLine & "時=h" &
                                                            vbNewLine & "日=d")>
    Public Property Version_Update_Interval As String = "3600s"
    <DisplayName("極限模式強制困難難度")> <DefaultValue(True)> <Category("附加設定")> <Description("是否在極限模式下鎖定伺服器的難度為困難。")>
    Public Property Force_Difficulty_On_Hardcore As Boolean = True
    <DisplayName("允許修改生怪磚")> <DefaultValue(True)> <Category("附加設定")> <Description("是否能用生怪蛋更改生怪磚要生成的生物。")>
    Public Property Allow_Spawner_Modify As Boolean = True
    <DisplayName("使用已棄用的更新檢查程式")> <DefaultValue(False)> <Category("附加設定")> <Description("是否啟用棄用的Akarin 檢查版本程式。（這會造成一些插件異常）")>
    Public Property Legacy_Versioning_Compat As Boolean = False
#End Region
#Region "Core"
    <DisplayName("區塊儲存執行緒數量")> <DefaultValue(2)> <Category("核心設定")> <Description("伺服器儲存區塊所用的執行緒數量。")>
    Public Property Chunk_Save_Threads As Integer = 2
    <DisplayName("用戶端最大回應時間")> <DefaultValue("30s")> <Category("核心設定")> <Description("用戶端的最大回應時間。 " &
                                                          vbNewLine & " 超過此時間伺服器將會自動把超時的玩家踢出。" &
                                                            vbNewLine & "單位：" &
                                                            vbNewLine & "秒=s" &
                                                            vbNewLine & "分=m" &
                                                            vbNewLine & "時=h" &
                                                            vbNewLine & "日=d")>
    Public Property Keep_Alive_Response_Timeout As String = "30s"
    <DisplayName("額外IP位址")> <DefaultValue(New String() {})> <Category("核心設定")> <Description("額外的綁定IP。")> '<Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    Public Property Extra_Local_Address As String() = {}

#End Region
    Friend Sub CreateOptionsWithDefaultSetting(path As String)
        Me.path = path
    End Sub
    Public Overrides Function GetOptionsTitle() As String
        Return "Akarin 設定"
    End Function
    Friend Sub New(filepath As String)
        MyBase.New(filepath)
        If IO.File.Exists(filepath) Then
            With Me
                Dim jsonObject As JObject
                Try
                    Dim reader As New IO.StreamReader(New IO.FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                    Dim deserializer = New DeserializerBuilder().Build()
                    Dim yamlObject = deserializer.Deserialize(reader)
                    deserializer = Nothing
                    Dim serializer = New SerializerBuilder().JsonCompatible().Build()
                    Dim jsonString = serializer.Serialize(yamlObject)
                    serializer = Nothing
                    jsonObject = GetDeserialisedObject(jsonString)
                    reader.Close()
                Catch ex As Exception
                    jsonObject = New JObject
                End Try
                Dim bootstrapRegion As JObject = GetJsonObject(jsonObject, "bootstrap")
                Dim alternativeRegion As JObject = GetJsonObject(jsonObject, "alternative")
                Dim coreRegion As JObject = GetJsonObject(jsonObject, "core")
                InputPropertyValue(jsonObject, "config-version", .Config_Version)
                InputPropertyValue(bootstrapRegion, "extra-local-address", .Extra_Local_Address)
                InputPropertyValue(alternativeRegion, "modified-server-brand-name", .Modified_Server_Brand_Name)
                InputPropertyValue(alternativeRegion, "gc-before-stuck-restart", .Gc_Before_Stuck_Restart)
                InputPropertyValue(alternativeRegion, "disable-end-portal-create", .Disable_End_Portal_Create)
                InputPropertyValue(alternativeRegion, "version-update-interval", .Version_Update_Interval)
                InputPropertyValue(alternativeRegion, "force-difficulty-on-hardcore", .Force_Difficulty_On_Hardcore)
                InputPropertyValue(alternativeRegion, "allow-spawner-modify", .Allow_Spawner_Modify)
                InputPropertyValue(alternativeRegion, "legacy-versioning-compat", .Legacy_Versioning_Compat)
                InputPropertyValue(coreRegion, "keep-alive-response-timeout", .Keep_Alive_Response_Timeout)
                InputPropertyValue(coreRegion, "chunk-save-threads", .Chunk_Save_Threads)
                .path = filepath
                GC.Collect()
            End With
        Else
            CreateOptionsWithDefaultSetting(filepath)
        End If
    End Sub
    Public Overrides Sub SaveOption()
        Dim jsonObject As JObject
        If IO.File.Exists(path) Then
            Try
                Dim reader As New IO.StreamReader(New IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                Dim deserializer = New DeserializerBuilder().Build()
                Dim yamlObject = deserializer.Deserialize(reader)
                deserializer = Nothing
                Dim jsonSerializer = New SerializerBuilder().JsonCompatible().Build()
                Dim jsonString = jsonSerializer.Serialize(yamlObject)
                jsonSerializer = Nothing
                jsonObject = GetDeserialisedObject(jsonString)
                reader.Close()
            Catch ex As Exception
                jsonObject = New JObject
            End Try
        Else
            jsonObject = New JObject
        End If
        Dim bootstrapRegion As JObject = GetJsonObject(jsonObject, "bootstrap")
        Dim alternativeRegion As JObject = GetJsonObject(jsonObject, "alternative")
        Dim coreRegion As JObject = GetJsonObject(jsonObject, "core")
        SetPropertyValue(jsonObject, "config-version", Config_Version)
        SetPropertyValue(bootstrapRegion, "extra-local-address", New JArray(Extra_Local_Address))
        SetPropertyValue(alternativeRegion, "modified-server-brand-name", Modified_Server_Brand_Name)
        SetPropertyValue(alternativeRegion, "gc-before-stuck-restart", Gc_Before_Stuck_Restart)
        SetPropertyValue(alternativeRegion, "disable-end-portal-create", Disable_End_Portal_Create)
        SetPropertyValue(alternativeRegion, "version-update-interval", Version_Update_Interval)
        SetPropertyValue(alternativeRegion, "force-difficulty-on-hardcore", Force_Difficulty_On_Hardcore)
        SetPropertyValue(alternativeRegion, "allow-spawner-modify", Allow_Spawner_Modify)
        SetPropertyValue(alternativeRegion, "legacy-versioning-compat", Legacy_Versioning_Compat)
        SetPropertyValue(coreRegion, "keep-alive-response-timeout", Keep_Alive_Response_Timeout)
        SetPropertyValue(coreRegion, "chunk-save-threads", Chunk_Save_Threads)
        SetPropertyValue(jsonObject, "bootstrap", bootstrapRegion)
        SetPropertyValue(jsonObject, "alternative", alternativeRegion)
        SetPropertyValue(jsonObject, "core", coreRegion)
        Dim expConverter = New Newtonsoft.Json.Converters.ExpandoObjectConverter()
        Dim deserializedObject = JsonConvert.DeserializeObject(Of Dynamic.ExpandoObject)(JsonConvert.SerializeObject(jsonObject), expConverter)
        Dim serializer As New YamlDotNet.Serialization.Serializer()
        Dim writer As IO.StreamWriter
        If IO.File.Exists(path) Then
            writer = New IO.StreamWriter(New IO.FileStream(path, IO.FileMode.Truncate, IO.FileAccess.Write))
        Else
            writer = New IO.StreamWriter(New IO.FileStream(path, IO.FileMode.CreateNew, IO.FileAccess.Write))
        End If
        serializer.Serialize(writer, deserializedObject)
        writer.Flush()
        writer.Close()
    End Sub
End Class
