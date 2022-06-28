Public Class EnumItem
    Private _ID As Integer
    Private _Name As String

    Public Sub New(ByVal ID As Integer, ByVal Name As String)
        _ID = ID
        _Name = Name
    End Sub

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal Value As Integer)
            _ID = Value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property
End Class
