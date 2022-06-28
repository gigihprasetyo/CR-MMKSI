Public Class DetailClass
    Private _ClassCode As String
    Public Property ClassCode() As String
        Get
            Return _ClassCode
        End Get
        Set(ByVal value As String)
            _ClassCode = value
        End Set
    End Property

    Private _CourseCode As String
    Public Property CourseCode() As String
        Get
            Return _CourseCode
        End Get
        Set(ByVal value As String)
            _CourseCode = value
        End Set
    End Property

    Private _TanggalMulai As String
    Public Property TanggalMulai() As String
        Get
            Return _TanggalMulai
        End Get
        Set(ByVal value As String)
            _TanggalMulai = value
        End Set
    End Property

    Private _TanggalSelesai As String
    Public Property TanggalSelesai() As String
        Get
            Return _TanggalSelesai
        End Get
        Set(ByVal value As String)
            _TanggalSelesai = value
        End Set
    End Property

    Private _Lokasi As String
    Public Property Lokasi() As String
        Get
            Return _Lokasi
        End Get
        Set(ByVal value As String)
            _Lokasi = value
        End Set
    End Property

    Private _SiswaTerdaftar As String
    Public Property SiswaTerdaftar() As String
        Get
            Return _SiswaTerdaftar
        End Get
        Set(ByVal value As String)
            _SiswaTerdaftar = value
        End Set
    End Property

    Private _PricePerDay As String
    Public Property PricePerDay() As String
        Get
            Return _PricePerDay
        End Get
        Set(ByVal value As String)
            _PricePerDay = value
        End Set
    End Property

    Private _PaidDay As String
    Public Property PaidDay() As String
        Get
            Return _PaidDay
        End Get
        Set(ByVal value As String)
            _PaidDay = value
        End Set
    End Property

    Private _TotalPrice As String
    Public Property TotalPrice() As String
        Get
            Return _TotalPrice
        End Get
        Set(ByVal value As String)
            _TotalPrice = value
        End Set
    End Property

    Private _Kapasitas As String
    Public Property Kapasitas() As String
        Get
            Return _Kapasitas
        End Get
        Set(ByVal value As String)
            _Kapasitas = value
        End Set
    End Property

    Private _SisaKapasitas As String
    Public Property SisaKapasitas() As String
        Get
            Return _SisaKapasitas
        End Get
        Set(ByVal value As String)
            _SisaKapasitas = value
        End Set
    End Property

End Class
