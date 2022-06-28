#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 3/7/2006 - 9:37:19 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("PODetail")> _
    Public Class PODetail
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _lineItem As Integer
        Private _reqQty As Integer
        Private _proposeQty As Integer
        Private _allocQty As Integer
        Private _price As Decimal
        Private _discount As Decimal
        Private _interest As Decimal
        Private _allocationDateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _logisticCost As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _freeDays As Integer
        Private _maxTOPDay As Integer

        Private _contractDetail As ContractDetail
        Private _pOHeader As POHeader

        '' CR Sirkular Rewards
        '' by : ali Akbar
        '' 2014-09-24
        Private _discountReward As Decimal
        Private _amountReward As Decimal
        Private _pPh22 As Decimal
        Private _AmountRewardDepA As Decimal
        '' END CR Sirkular Rewards

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("LineItem", "{0}")> _
        Public Property LineItem() As Integer
            Get
                Return _lineItem
            End Get
            Set(ByVal value As Integer)
                _lineItem = value
            End Set
        End Property


        <ColumnInfo("ReqQty", "{0}")> _
        Public Property ReqQty() As Integer
            Get
                Return _reqQty
            End Get
            Set(ByVal value As Integer)
                _reqQty = value
            End Set
        End Property


        <ColumnInfo("ProposeQty", "{0}")> _
        Public Property ProposeQty() As Integer
            Get
                Return _proposeQty
            End Get
            Set(ByVal value As Integer)
                _proposeQty = value
            End Set
        End Property


        <ColumnInfo("AllocQty", "{0}")> _
        Public Property AllocQty() As Integer
            Get
                Return _allocQty
            End Get
            Set(ByVal value As Integer)
                _allocQty = value
            End Set
        End Property


        <ColumnInfo("Price", "{0}")> _
        Public Property Price() As Decimal
            Get
                Return _price
            End Get
            Set(ByVal value As Decimal)
                _price = value
            End Set
        End Property


        <ColumnInfo("Discount", "{0}")> _
        Public Property Discount() As Decimal
            Get
                Return _discount
            End Get
            Set(ByVal value As Decimal)
                _discount = value
            End Set
        End Property


        <ColumnInfo("Interest", "{0}")> _
        Public Property Interest() As Decimal
            Get
                Return _interest
            End Get
            Set(ByVal value As Decimal)
                _interest = value
            End Set
        End Property

        <ColumnInfo("AllocationDateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property AllocationDateTime() As DateTime
            Get
                Return _allocationDateTime
            End Get
            Set(ByVal value As DateTime)
                _allocationDateTime = value
            End Set
        End Property

        <ColumnInfo("LogisticCost", "{0}")> _
        Public Property LogisticCost() As Decimal
            Get
                Return _logisticCost
            End Get
            Set(ByVal value As Decimal)
                _logisticCost = value
            End Set
        End Property

        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("FreeDays", "{0}")> _
        Public Property FreeDays As Integer
            Get
                Return _freeDays
            End Get
            Set(ByVal value As Integer)
                _freeDays = value
            End Set
        End Property


        <ColumnInfo("MaxTOPDay", "{0}")> _
        Public Property MaxTOPDay As Integer
            Get
                Return _maxTOPDay
            End Get
            Set(ByVal value As Integer)
                _maxTOPDay = value
            End Set
        End Property


        <ColumnInfo("ContractDetailID", "{0}"), _
        RelationInfo("ContractDetail", "ID", "PODetail", "ContractDetailID")> _
        Public Property ContractDetail() As ContractDetail
            Get
                Try
                    If Not IsNothing(Me._contractDetail) AndAlso (Not Me._contractDetail.IsLoaded) Then

                        Me._contractDetail = CType(DoLoad(GetType(ContractDetail).ToString(), _contractDetail.ID), ContractDetail)
                        Me._contractDetail.MarkLoaded()

                    End If

                    Return Me._contractDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ContractDetail)

                Me._contractDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._contractDetail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("POHeaderID", "{0}"), _
        RelationInfo("POHeader", "ID", "PODetail", "POHeaderID")> _
        Public Property POHeader() As POHeader
            Get
                Try
                    If Not IsNothing(Me._pOHeader) AndAlso (Not Me._pOHeader.IsLoaded) Then

                        Me._pOHeader = CType(DoLoad(GetType(POHeader).ToString(), _pOHeader.ID), POHeader)
                        Me._pOHeader.MarkLoaded()

                    End If

                    Return Me._pOHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As POHeader)

                Me._pOHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pOHeader.MarkLoaded()
                End If
            End Set
        End Property


        '' CR Sirkular Rewards
        '' by : ali Akbar
        '' 2014-09-24
        <ColumnInfo("DiscountReward", "{0}")> _
        Public Property DiscountReward() As Decimal
            Get
                Return _discountReward
            End Get
            Set(ByVal value As Decimal)
                _discountReward = value
            End Set
        End Property

        <ColumnInfo("AmountReward", "{0}")> _
        Public Property AmountReward() As Decimal
            Get
                Return _amountReward
            End Get
            Set(ByVal value As Decimal)
                _amountReward = value
            End Set
        End Property

        <ColumnInfo("PPh22", "{0}")> _
        Public Property PPh22() As Decimal
            Get
                Return _pPh22
            End Get
            Set(ByVal value As Decimal)
                _pPh22 = value
            End Set
        End Property

        <ColumnInfo("AmountRewardDepA", "{0}")> _
        Public Property AmountRewardDepA() As Decimal
            Get
                Return _AmountRewardDepA
            End Get
            Set(ByVal value As Decimal)
                _AmountRewardDepA = value
            End Set
        End Property





        '' END CR Sirkular Rewards


#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Method"

        'Private _POHeaderID As Integer

        Public ReadOnly Property POHeaderID() As Integer
            Get
                Return _pOHeader.ID
            End Get
        End Property

        Public ReadOnly Property ContractDetailID() As Integer
            Get
                Return _contractDetail.ID
            End Get
        End Property

        Public ReadOnly Property UsulanATP() As Integer
            Get
                Dim _total As Integer = 0
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, Me.ContractDetail.VechileColor.MaterialNumber))
                criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeMonth", MatchType.Exact, DateTime.Now.Month))
                criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeYear", MatchType.Exact, DateTime.Now.Year))
                criterias.opAnd(New Criteria(GetType(PPQty), "ProductionYear", MatchType.Exact, Me.ContractDetail.ContractHeader.ProductionYear))
                Dim m_PPQtyMapper As IMapper
                m_PPQtyMapper = MapperFactory.GetInstance.GetMapper(GetType(PPQty).ToString)

                If Me.POHeader.POType = LookUp.EnumJenisOrder.Harian Then
                    criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeDate", MatchType.Exact, DateTime.Now.Day))
                Else
                    Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", AggregateType.Max)
                    Dim MaxTgl As Integer = m_PPQtyMapper.RetrieveScalar(agg, criterias)
                    If MaxTgl <> 0 Then
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", MatchType.Exact, MaxTgl))
                    Else
                        Return 0
                    End If
                End If

                Dim PPQtyColl As ArrayList = m_PPQtyMapper.RetrieveByCriteria(criterias)
                If (PPQtyColl.Count > 0) Then
                    For Each item As PPQty In PPQtyColl
                        'If item.POHeader.Status = CInt(enumStatusPO.Status.Baru).ToString Or item.POHeader.Status = CInt(enumStatusPO.Status.Konfirmasi).ToString Then
                        _total = _total + item.AllocationQty
                        'Else
                        '    _total = _total - CType(item.AllocQty, Integer)
                        'End If
                    Next
                End If
                Return _total
            End Get
        End Property

        Public ReadOnly Property StokATP() As Integer
            Get
                'Todo Aggregate
                Dim _total As Integer = 0
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, Me.ContractDetail.VechileColor.MaterialNumber))
                criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeMonth", MatchType.Exact, DateTime.Now.Month))
                criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeYear", MatchType.Exact, DateTime.Now.Year))
                criterias.opAnd(New Criteria(GetType(PPQty), "ProductionYear", MatchType.Exact, Me.ContractDetail.ContractHeader.ProductionYear))
                Dim m_PPQtyMapper As IMapper
                m_PPQtyMapper = MapperFactory.GetInstance.GetMapper(GetType(PPQty).ToString)

                If Me.POHeader.POType = LookUp.EnumJenisOrder.Harian Then
                    criterias.opAnd(New Criteria(GetType(PPQty), "PeriodeDate", MatchType.Exact, DateTime.Now.Day))
                Else
                    Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", AggregateType.Max)
                    Dim MaxTgl As Integer
                    Try
                        MaxTgl = m_PPQtyMapper.RetrieveScalar(agg, criterias)
                    Catch ex As Exception
                        MaxTgl = 0
                    End Try

                    If MaxTgl <> 0 Then
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", MatchType.Exact, MaxTgl))
                    Else
                        Return 0
                    End If
                End If

                Dim PPQtyColl As ArrayList = m_PPQtyMapper.RetrieveByCriteria(criterias)
                If (PPQtyColl.Count > 0) Then
                    For Each item As PPQty In PPQtyColl
                        'If item.POHeader.Status = CInt(enumStatusPO.Status.Baru).ToString Or item.POHeader.Status = CInt(enumStatusPO.Status.Konfirmasi).ToString Then
                        _total = _total + item.AllocationQty
                        'Else
                        '    _total = _total - CType(item.AllocQty, Integer)
                        'End If
                    Next
                End If
                Return _total
            End Get
        End Property

        ''Ali Akbar

        Public ReadOnly Property RealQty() As Integer
            'Refer to logic in view v_RekapPO
            Get
                Select Case Me.POHeader.Status   ' Must be a primitive data type
                    Case enumStatusPO.Status.Baru, enumStatusPO.Status.Batal, enumStatusPO.Status.Konfirmasi, enumStatusPO.Status.Ditolak
                        Return Me.ReqQty
                    Case enumStatusPO.Status.DiBlok
                        Return 0
                    Case Else
                        Return Me.AllocQty
                End Select
            End Get
        End Property

        ''ENd Ali Akbar

        Private _v_DealerOrder As v_DealerOrder
        Private i As Integer

        Public ReadOnly Property v_DealerOrder() As v_DealerOrder
            Get
                If IsNothing(Me._v_DealerOrder) OrElse Me._v_DealerOrder.ID < 1 Then
                    Dim cVDO As New CriteriaComposite(New Criteria(GetType(v_DealerOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aVDOs As ArrayList
                    Dim today As Date = DateSerial(Me.AllocationDateTime.Year, Me.AllocationDateTime.Month, Me.AllocationDateTime.Day)

                    cVDO.opAnd(New Criteria(GetType(v_DealerOrder), "PODetailID", MatchType.Exact, Me.ID))
                    cVDO.opAnd(New Criteria(GetType(v_DealerOrder), "AllocationDateTime", MatchType.GreaterOrEqual, today))
                    cVDO.opAnd(New Criteria(GetType(v_DealerOrder), "AllocationDateTime", MatchType.Lesser, today.AddDays(1)))

                    aVDOs = DoLoadArray(GetType(v_DealerOrder).ToString, cVDO)
                    If aVDOs.Count > 0 Then
                        Me._v_DealerOrder = aVDOs(0)
                    End If
                End If
                Return Me._v_DealerOrder
            End Get
        End Property

        Public Function getRefPrice()
            'Dim oP As Price
            Dim contDate As Date = DateSerial(Me.ContractDetail.ContractHeader.PricePeriodYear, Me.ContractDetail.ContractHeader.PricePeriodMonth, Me.ContractDetail.ContractHeader.PricePeriodDay)
            Dim cP As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cP.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, Me.ContractDetail.VechileColor.ID))
            cP.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, Me.POHeader.Dealer.ID))
            cP.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "ValidFrom", MatchType.LesserOrEqual, contDate))

            Dim sP As SortCollection = New SortCollection
            sP.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
            Dim m_PriceMapper As IMapper
            m_PriceMapper = MapperFactory.GetInstance.GetMapper(GetType(Price).ToString)

            Dim aPs As ArrayList = m_PriceMapper.RetrieveByCriteria(cP, sP)
            If aPs.Count > 0 Then
                Return aPs(0)
            Else
                Return New Price
            End If
        End Function
#End Region

    End Class
End Namespace