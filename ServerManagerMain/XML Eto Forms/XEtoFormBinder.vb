Imports Eto.Forms

Module XEtoFormBinder
    ''' <summary>
    ''' 繫結.xeto檔案到 Visual Basic 的 Form 上
    ''' </summary>
    ''' <typeparam name="T">指定要用的表單類別</typeparam>
    ''' <param name="XEtoFormName">.xeto 檔案的名稱</param>
    ''' <param name="Form">要繫結的表單實體</param>
    Sub BindXEtoFormToTargetClass(Of T As Form)(XEtoFormName As String, Form As T)
        Dim assem = Reflection.Assembly.GetExecutingAssembly
        Eto.Serialization.Xaml.XamlReader.Load(Of T)(assem.GetManifestResourceStream(assem.GetName.Name & "." & XEtoFormName & ".xeto"), Form)
    End Sub
End Module
