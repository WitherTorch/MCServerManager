Namespace JsonServer.Exceptions
    Public Class JsonServerException
        Inherits Exception
        Public ReadOnly Property JsonErrorCode As Enums.ErrorCodes
        Sub New(Optional errorCode As Enums.ErrorCodes = Enums.ErrorCodes.Unknown)
            _JsonErrorCode = errorCode
        End Sub
    End Class
End Namespace