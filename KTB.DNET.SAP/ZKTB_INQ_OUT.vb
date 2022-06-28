Public Class ZKTB_INQ_OUT

    ' Fields
    Private _Currency As String
    Private _Customer As String
    Private _Discount As Decimal
    Private _Material As String
    Private _Retail_Price As Decimal

    ' Properties
    Public Property Currency As String
        Get
            Return Me._Currency
        End Get
        Set(ByVal value As String)
            Me._Currency = value
        End Set
    End Property

    Public Property Customer As String
        Get
            Return Me._Customer
        End Get
        Set(ByVal value As String)
            Me._Customer = value
        End Set
    End Property

    Public Property Discount As Decimal
        Get
            Return Me._Discount
        End Get
        Set(ByVal value As Decimal)
            Me._Discount = value
        End Set
    End Property

    Public Property Material As String
        Get
            Return Me._Material
        End Get
        Set(ByVal value As String)
            Me._Material = value
        End Set
    End Property

    Public Property Retail_Price As Decimal
        Get
            Return Me._Retail_Price
        End Get
        Set(ByVal value As Decimal)
            Me._Retail_Price = value
        End Set
    End Property



End Class
