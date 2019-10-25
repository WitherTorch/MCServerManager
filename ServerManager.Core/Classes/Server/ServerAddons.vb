Public Class ServerAddons
    Friend Property Name As String
    Friend Property Version As String
    Friend ReadOnly Property Path As String
    Friend ReadOnly Property VersionDate As DateTime
    Friend ReadOnly Property LastDate As DateTime
    Sub New(Name As String, Path As String, Version As String, VersionDate As DateTime, LastDate As DateTime)
        _Name = Name
        _Version = Version
        _Path = Path
        _VersionDate = VersionDate
        _LastDate = LastDate
    End Sub
    Public Shared Operator =(addon1 As ServerAddons, addon2 As ServerAddons) As Boolean
        Return addon1.Path = addon2.Path
    End Operator
    Public Shared Operator <>(addon1 As ServerAddons, addon2 As ServerAddons) As Boolean
        Return addon1.Path <> addon2.Path
    End Operator
End Class
