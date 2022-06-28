Public Class ZSPST0028_01

    Private _matnr As String
    Private _rqqty As Integer
    Private _maktx As String
    Private _pcode As String
    Private _matkl As String
    Private _rtlpr As Decimal
    Private _stock As String
    Private _normt As String
    Private _subnr As String
    Private _messg As String

    Public Property MATNR() As String
        Get
            Return _matnr
        End Get
        Set(ByVal value As String)
            _matnr = value
        End Set
    End Property

    Public Property RQQTY() As Integer
        Get
            Return _rqqty
        End Get
        Set(ByVal value As Integer)
            _rqqty = value
        End Set
    End Property

    Public Property MAKTX() As String
        Get
            Return _maktx
        End Get
        Set(ByVal value As String)
            _maktx = value
        End Set
    End Property

    Public Property PCODE() As String
        Get
            Return _pcode
        End Get
        Set(ByVal value As String)
            _pcode = value
        End Set
    End Property

    Public Property MATKL() As String
        Get
            Return _matkl
        End Get
        Set(ByVal value As String)
            _matkl = value
        End Set
    End Property

    Public Property RTLPR() As Decimal
        Get
            Return _rtlpr
        End Get
        Set(ByVal value As Decimal)
            _rtlpr = value
        End Set
    End Property

    Public Property STOCK() As String
        Get
            Return _stock
        End Get
        Set(ByVal value As String)
            _stock = value
        End Set
    End Property

    Public Property NORMT() As String
        Get
            Return _normt
        End Get
        Set(ByVal value As String)
            _normt = value
        End Set
    End Property

    Public Property SUBNR() As String
        Get
            Return _subnr
        End Get
        Set(ByVal value As String)
            _subnr = value
        End Set
    End Property

    Public Property MESSG() As String
        Get
            Return _messg
        End Get
        Set(ByVal value As String)
            _messg = value
        End Set
    End Property

End Class
