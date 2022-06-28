Namespace KTB.DNet.Parser

    Public Class StockMaterialPromotionUpload

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal kodebarang As String, ByVal Namabarang As String, ByVal unit As String, ByVal stock As String)
            _KodeBarang = kodebarang
            _NamaBarang = namabarang
            _Unit = unit
            _Stock = stock
        End Sub

#End Region

#Region "Private Variables"

        Private _KodeBarang As String
        Private _NamaBarang As String
        Private _Unit As String
        Private _Stock As String
        Private _ErrorMessage As String

#End Region

#Region "Public Properties"

        Property KodeBarang() As String
            Get
                Return _KodeBarang
            End Get
            Set(ByVal Value As String)
                _KodeBarang = Value
            End Set
        End Property

        Property NamaBarang() As String
            Get
                Return _NamaBarang
            End Get
            Set(ByVal Value As String)
                _NamaBarang = Value
            End Set
        End Property

        Property Unit() As String
            Get
                Return _Unit
            End Get
            Set(ByVal Value As String)
                _Unit = Value
            End Set
        End Property

        Property Stock() As String
            Get
                Return _Stock
            End Get
            Set(ByVal Value As String)
                _Stock = Value
            End Set
        End Property
        Property ErrorMessage() As String
            Get
                Return _ErrorMessage
            End Get
            Set(ByVal Value As String)
                _ErrorMessage = Value
            End Set
        End Property

#End Region

    End Class

End Namespace