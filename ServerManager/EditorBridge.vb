Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports ServerManager.CauldronOptions
Imports ServerManager.PaperOptions

Public Class EditorBridge
    Shared Sub Hook()
        TypeDescriptor.AddAttributes(GetType(List(Of SpigotWorldSettings)), {New EditorAttribute(GetType(SpigotWorldSettingsCollectionEditor), GetType(UITypeEditor))})
        TypeDescriptor.AddAttributes(GetType(List(Of PaperWorldSettings)), {New EditorAttribute(GetType(PaperWorldSettingsCollectionEditor), GetType(UITypeEditor))})
        TypeDescriptor.AddAttributes(GetType(List(Of CauldronWorldSettings)), {New EditorAttribute(GetType(CauldronWorldSettingsCollectionEditor), GetType(UITypeEditor))})
        TypeDescriptor.AddAttributes(GetType(List(Of CauldronPluginSettings)), {New EditorAttribute(GetType(CauldronPluginSettingsCollectionEditor), GetType(UITypeEditor))})
        TypeDescriptor.AddAttributes(GetType(String()), {New EditorAttribute("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))})
    End Sub
End Class
Public Class SpigotWorldSettingsCollectionEditor
    Inherits CollectionEditor
    Dim itemCount As Integer = 1
    Public Sub New()
        MyBase.New(type:=GetType(List(Of SpigotWorldSettings)))
    End Sub
    Protected Overrides Function CreateCollectionForm() As CollectionForm
        Dim form = MyBase.CreateCollectionForm()
        form.Text = "Spigot 世界設定編輯器"
        AddHandler form.Shown, Sub()
                                   ShowDescription(form)
                               End Sub
        Return form
    End Function
    Protected Overrides Function CreateInstance(ByVal itemType As Type) As Object
        Dim settings = New SpigotWorldSettings
        settings.Name = "world-" & itemCount
        itemCount += 1
        Return settings
    End Function
    Shared Sub ShowDescription(control As Control)
        Dim grid As PropertyGrid = TryCast(control, PropertyGrid)
        If grid IsNot Nothing Then grid.HelpVisible = True
        For Each child As Control In control.Controls
            ShowDescription(child)
        Next
    End Sub
    Protected Overrides Function CanRemoveInstance(value As Object) As Boolean
        Dim settings As SpigotWorldSettings = value
        Return settings.Name.ToLower <> "default"
    End Function
    Protected Overrides Function GetDisplayText(value As Object) As String
        Dim settings As SpigotWorldSettings = value
        Return settings.Name
    End Function
End Class

Public Class PaperWorldSettingsCollectionEditor
    Inherits CollectionEditor
    Dim itemCount As Integer = 1
    Public Sub New()
        MyBase.New(type:=GetType(List(Of PaperWorldSettings)))
    End Sub
    Protected Overrides Function CreateCollectionForm() As CollectionForm
        Dim form = MyBase.CreateCollectionForm()
        form.Text = "Paper 世界設定編輯器"
        AddHandler form.Shown, Sub()
                                   ShowDescription(form)
                               End Sub
        Return form
    End Function
    Protected Overrides Function CreateInstance(ByVal itemType As Type) As Object
        Dim settings = New PaperWorldSettings
        settings.Name = "world-" & itemCount
        itemCount += 1
        Return settings
    End Function
    Shared Sub ShowDescription(control As Control)
        Dim grid As PropertyGrid = TryCast(control, PropertyGrid)
        If grid IsNot Nothing Then grid.HelpVisible = True
        For Each child As Control In control.Controls
            ShowDescription(child)
        Next
    End Sub
    Protected Overrides Function CanRemoveInstance(value As Object) As Boolean
        Dim settings As PaperWorldSettings = value
        Return settings.Name.ToLower <> "default"
    End Function
    Protected Overrides Function GetDisplayText(value As Object) As String
        Dim settings As PaperWorldSettings = value
        Return settings.Name
    End Function
End Class
Public Class CauldronWorldSettingsCollectionEditor
    Inherits CollectionEditor
    Dim itemCount As Integer = 1
    Public Sub New()
        MyBase.New(type:=GetType(List(Of CauldronWorldSettings)))
    End Sub
    Protected Overrides Function CreateCollectionForm() As CollectionForm
        Dim form = MyBase.CreateCollectionForm()
        form.Text = "Cauldron 世界設定編輯器"
        AddHandler form.Shown, Sub()
                                   ShowDescription(form)
                               End Sub
        Return form
    End Function
    Protected Overrides Function CreateInstance(ByVal itemType As Type) As Object
        Dim settings = New CauldronWorldSettings
        settings.Name = "world-" & itemCount
        itemCount += 1
        Return settings
    End Function
    Shared Sub ShowDescription(control As Control)
        Dim grid As PropertyGrid = TryCast(control, PropertyGrid)
        If grid IsNot Nothing Then grid.HelpVisible = True
        For Each child As Control In control.Controls
            ShowDescription(child)
        Next
    End Sub
    Protected Overrides Function CanRemoveInstance(value As Object) As Boolean
        Dim settings As CauldronWorldSettings = value
        Return settings.Name.ToLower <> "default"
    End Function
    Protected Overrides Function GetDisplayText(value As Object) As String
        Dim settings As CauldronWorldSettings = value
        Return settings.Name
    End Function
End Class
Public Class CauldronPluginSettingsCollectionEditor
    Inherits CollectionEditor
    Dim itemCount As Integer = 1
    Public Sub New()
        MyBase.New(type:=GetType(List(Of CauldronPluginSettings)))
    End Sub
    Protected Overrides Function CreateCollectionForm() As CollectionForm
        Dim form = MyBase.CreateCollectionForm()
        form.Text = "Cauldron 插件設定編輯器"
        AddHandler form.Shown, Sub()
                                   ShowDescription(form)
                               End Sub
        Return form
    End Function
    Protected Overrides Function CreateInstance(ByVal itemType As Type) As Object
        Dim settings = New CauldronPluginSettings
        settings.Name = "world-" & itemCount
        itemCount += 1
        Return settings
    End Function
    Shared Sub ShowDescription(control As Control)
        Dim grid As PropertyGrid = TryCast(control, PropertyGrid)
        If grid IsNot Nothing Then grid.HelpVisible = True
        For Each child As Control In control.Controls
            ShowDescription(child)
        Next
    End Sub
    Protected Overrides Function CanRemoveInstance(value As Object) As Boolean
        Dim settings As CauldronPluginSettings = value
        Return settings.Name.ToLower <> "default"
    End Function
    Protected Overrides Function GetDisplayText(value As Object) As String
        Dim settings As CauldronPluginSettings = value
        Return settings.Name
    End Function
End Class
