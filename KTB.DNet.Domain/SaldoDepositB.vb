Public Class SaldoDepositB
    Private _SDepositB As Double
    Public Property SDepositB() As Double
        Get
            Return _SDepositB
        End Get
        Set(ByVal value As Double)
            _SDepositB = value
        End Set
    End Property

    Private _SPlafon As Double
    Public Property Plafon() As Double
        Get
            Return _SPlafon
        End Get
        Set(ByVal value As Double)
            _SPlafon = value
        End Set
    End Property

    Private _TotPengajuan As Double
    Public Property TotalPengajuan() As Double
        Get
            Return _TotPengajuan
        End Get
        Set(ByVal value As Double)
            _TotPengajuan = value
        End Set
    End Property

    Private _SisaSaldo As Double
    Public Property SisaSaldo() As Double
        Get
            Return _SisaSaldo
        End Get
        Set(ByVal value As Double)
            _SisaSaldo = value
        End Set
    End Property


End Class
