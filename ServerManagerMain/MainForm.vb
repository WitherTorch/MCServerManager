Public Class MainForm
    Inherits Eto.Forms.Form
    Sub New()
        XEtoFormBinder.BindXEtoFormToTargetClass(Of MainForm)("MainForm", Me)
    End Sub
End Class
