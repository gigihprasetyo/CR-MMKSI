Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework

Namespace KTB.DNet.Parser.Domain

    Public Class PRPExcelPerToko

#Region "Constructor"

        Public Sub New()

        End Sub

        Public Sub New( _
            ByVal nPsCode As String, _
            ByVal nPartShopName As String, _
            ByVal nKota As String, _
            ByVal nJan As Integer, _
            ByVal nFeb As Integer, _
            ByVal nMar As Integer, _
            ByVal nApr As Integer, _
            ByVal nMay As Integer, _
            ByVal nJun As Integer, _
            ByVal nJul As Integer, _
            ByVal nAug As Integer, _
            ByVal nSep As Integer, _
            ByVal nOct As Integer, _
            ByVal nNov As Integer, _
            ByVal nDec As Integer, _
            ByVal nTotal As Integer)

            _psCode = nPsCode
            _partShopName = nPartShopName
            _kota = nKota
            _jan = nJan
            _feb = nFeb
            _mar = nMar
            _apr = nApr
            _may = nMay
            _jun = nJun
            _jul = nJul
            _aug = nAug
            _sep = nSep
            _oct = nOct
            _nov = nNov
            _dec = nDec
            _total = nTotal
        End Sub
#End Region

#Region "Private Property"
        Private _psCode As String
        Private _partShopName As String
        Private _kota As String
        Private _jan As Integer
        Private _feb As Integer
        Private _mar As Integer
        Private _apr As Integer
        Private _may As Integer
        Private _jun As Integer
        Private _jul As Integer
        Private _aug As Integer
        Private _sep As Integer
        Private _oct As Integer
        Private _nov As Integer
        Private _dec As Integer
        Private _total As Integer
#End Region

#Region "Public Property"

        Public Property PsCode() As String
            Get
                Return _psCode
            End Get
            Set(ByVal Value As String)
                _psCode = Value
            End Set
        End Property

        Public Property PartShopName() As String
            Get
                Return _partShopName
            End Get
            Set(ByVal Value As String)
                _partShopName = Value
            End Set
        End Property

        Public Property Kota() As String
            Get
                Return _kota
            End Get
            Set(ByVal Value As String)
                _kota = Value
            End Set
        End Property

        Public Property Jan() As Integer
            Get
                Return _jan
            End Get
            Set(ByVal Value As Integer)
                _jan = Value
            End Set
        End Property

        Public Property Feb() As Integer
            Get
                Return _feb
            End Get
            Set(ByVal Value As Integer)
                _feb = Value
            End Set
        End Property

        Public Property Mar() As Integer
            Get
                Return _mar
            End Get
            Set(ByVal Value As Integer)
                _mar = Value
            End Set
        End Property

        Public Property Apr() As Integer
            Get
                Return _apr
            End Get
            Set(ByVal Value As Integer)
                _apr = Value
            End Set
        End Property

        Public Property May() As Integer
            Get
                Return _may
            End Get
            Set(ByVal Value As Integer)
                _may = Value
            End Set
        End Property

        Public Property Jun() As Integer
            Get
                Return _jun
            End Get
            Set(ByVal Value As Integer)
                _jun = Value
            End Set
        End Property

        Public Property Jul() As Integer
            Get
                Return _jul
            End Get
            Set(ByVal Value As Integer)
                _jul = Value
            End Set
        End Property

        Public Property Aug() As Integer
            Get
                Return _aug
            End Get
            Set(ByVal Value As Integer)
                _aug = Value
            End Set
        End Property

        Public Property Sep() As Integer
            Get
                Return _sep
            End Get
            Set(ByVal Value As Integer)
                _sep = Value
            End Set
        End Property

        Public Property Oct() As Integer
            Get
                Return _oct
            End Get
            Set(ByVal Value As Integer)
                _oct = Value
            End Set
        End Property

        Public Property Nov() As Integer
            Get
                Return _nov
            End Get
            Set(ByVal Value As Integer)
                _nov = Value
            End Set
        End Property

        Public Property Dec() As Integer
            Get
                Return _dec
            End Get
            Set(ByVal Value As Integer)
                _dec = Value
            End Set
        End Property

        Public Property Total() As Integer
            Get
                Return _total
            End Get
            Set(ByVal Value As Integer)
                _total = Value
            End Set
        End Property

#End Region

    End Class

    Public Class PRPExcelPerDealer
        Inherits PRPExcelPerToko

#Region "Constructor"
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal nDealerCode As String, _
            ByVal nPsCode As String, _
            ByVal nPartShopName As String, _
            ByVal nKota As String, _
            ByVal nJan As Integer, _
            ByVal nFeb As Integer, _
            ByVal nMar As Integer, _
            ByVal nApr As Integer, _
            ByVal nMay As Integer, _
            ByVal nJun As Integer, _
            ByVal nJul As Integer, _
            ByVal nAug As Integer, _
            ByVal nSep As Integer, _
            ByVal nOct As Integer, _
            ByVal nNov As Integer, _
            ByVal nDec As Integer, _
            ByVal nTotal As Integer)

            MyBase.New( _
                nPsCode, _
                nPartShopName, _
                nKota, _
                nJan, _
                nFeb, _
                nMar, _
                nApr, _
                nMay, _
                nJun, _
                nJul, _
                nAug, _
                nSep, _
                nOct, _
                nNov, _
                nDec, _
                nTotal)
            DealerCode = nDealerCode

        End Sub
#End Region

        Private _dealerCode As String
        Private _dealer As Dealer

        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal Value As String)
                _dealerCode = Value
            End Set
        End Property

        Public Property Dealer() As Dealer
            Get
                If IsNothing(_dealer) Then
                    FillDealer()
                End If
                Return _dealer
            End Get
            Set(ByVal Value As Dealer)
                _dealer = Dealer
            End Set
        End Property

        Private Sub FillDealer()
            If _dealerCode = String.Empty Then
                _dealer = Nothing
                Return
            End If

            Dim mapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(Dealer).ToString)
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(Dealer), "SearchTerm2", MatchType.Exact, _dealerCode))
            Dim result As ArrayList = mapper.RetrieveByCriteria(crit)
            If IsNothing(result) Then
                _dealer = Nothing
            End If
            _dealer = CType(result(0), Dealer)
        End Sub
    End Class

End Namespace
