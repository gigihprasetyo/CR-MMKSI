Public Class ZSPST0028_02


    ' Fields
    Private _Maktx As String
    Private _Matkl As String
    Private _Matnr1 As String
    Private _Matnr2 As String
    Private _Normt As String
    Private _Rtlpr As Decimal
    Private _Stock As String

    Public Property Maktx As String
        Get
            Return Me._Maktx
        End Get
        Set(ByVal value As String)
            Me._Maktx = value
        End Set
    End Property

    Public Property Matkl As String
        Get
            Return Me._Matkl
        End Get
        Set(ByVal value As String)
            Me._Matkl = value
        End Set
    End Property

    Public Property Matnr1 As String
        Get
            Return Me._Matnr1
        End Get
        Set(ByVal value As String)
            Me._Matnr1 = value
        End Set
    End Property

    Public Property Matnr2 As String
        Get
            Return Me._Matnr2
        End Get
        Set(ByVal value As String)
            Me._Matnr2 = value
        End Set
    End Property

    Public Property Normt As String
        Get
            Return Me._Normt
        End Get
        Set(ByVal value As String)
            Me._Normt = value
        End Set
    End Property

    Public Property Rtlpr As Decimal
        Get
            Return Me._Rtlpr
        End Get
        Set(ByVal value As Decimal)
            Me._Rtlpr = value
        End Set
    End Property

    Public Property Stock As String
        Get
            Return Me._Stock
        End Get
        Set(ByVal value As String)
            Me._Stock = value
        End Set
    End Property

End Class
