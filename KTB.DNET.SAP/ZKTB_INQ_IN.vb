Public Class ZKTB_INQ_IN

    Private _customer As String
    Private _material As String

    Public Property CUSTOMER() As String
        Get
            Return _customer
        End Get
        Set(ByVal value As String)
            _customer = value
        End Set
    End Property

    Public Property MATERIAL() As String
        Get
            Return _material
        End Get
        Set(ByVal value As String)
            _material = value
        End Set
    End Property

End Class
