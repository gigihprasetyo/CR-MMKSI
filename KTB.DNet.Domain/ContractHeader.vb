#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ContractHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 11/2/2006 - 9:07:16 AM
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
    <Serializable(), TableInfo("ContractHeader")> _
    Public Class ContractHeader
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
        Private _contractNumber As String = String.Empty
        Private _pKNumber As String = String.Empty
        Private _dealerPKNumber As String = String.Empty
        Private _status As String = String.Empty
        Private _contractPeriodDay As Short
        Private _contractPeriodMonth As Short
        Private _contractPeriodYear As Short
        Private _pricePeriodDay As Short
        Private _pricePeriodMonth As Short
        Private _pricePeriodYear As Short
        Private _contractType As Short
        Private _purpose As Short
        Private _projectName As String = String.Empty
        Private _productionYear As Short
        Private _freePPh22Indicator As Integer
        Private _freePPh22LastUpdateBy As String = String.Empty
        Private _freePPh22LastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sPLNumber As String = String.Empty
        Private _freeIntIndicator As Integer
        Private _refContractNumber As String
        Private _isCarriedOver As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _category As Category
        Private _dealer As Dealer
        Private _pKHeader As PKHeader

        Private _contractDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("ContractNumber", "'{0}'")> _
        Public Property ContractNumber() As String
            Get
                Return _contractNumber
            End Get
            Set(ByVal value As String)
                _contractNumber = value
            End Set
        End Property


        <ColumnInfo("PKNumber", "'{0}'")> _
        Public Property PKNumber() As String
            Get
                Return _pKNumber
            End Get
            Set(ByVal value As String)
                _pKNumber = value
            End Set
        End Property


        <ColumnInfo("DealerPKNumber", "'{0}'")> _
        Public Property DealerPKNumber() As String
            Get
                Return _dealerPKNumber
            End Get
            Set(ByVal value As String)
                _dealerPKNumber = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property

        <ColumnInfo("ContractPeriodDay", "{0}")> _
        Public Property ContractPeriodDay() As Short
            Get
                Return _contractPeriodDay
            End Get
            Set(ByVal value As Short)
                _contractPeriodDay = value
            End Set
        End Property

        <ColumnInfo("ContractPeriodMonth", "{0}")> _
        Public Property ContractPeriodMonth() As Short
            Get
                Return _contractPeriodMonth
            End Get
            Set(ByVal value As Short)
                _contractPeriodMonth = value
            End Set
        End Property


        <ColumnInfo("ContractPeriodYear", "{0}")> _
        Public Property ContractPeriodYear() As Short
            Get
                Return _contractPeriodYear
            End Get
            Set(ByVal value As Short)
                _contractPeriodYear = value
            End Set
        End Property


        <ColumnInfo("PricePeriodDay", "{0}")> _
        Public Property PricePeriodDay() As Short
            Get
                Return _pricePeriodDay
            End Get
            Set(ByVal value As Short)
                _pricePeriodDay = value
            End Set
        End Property

        <ColumnInfo("PricePeriodMonth", "{0}")> _
        Public Property PricePeriodMonth() As Short
            Get
                Return _pricePeriodMonth
            End Get
            Set(ByVal value As Short)
                _pricePeriodMonth = value
            End Set
        End Property


        <ColumnInfo("PricePeriodYear", "{0}")> _
        Public Property PricePeriodYear() As Short
            Get
                Return _pricePeriodYear
            End Get
            Set(ByVal value As Short)
                _pricePeriodYear = value
            End Set
        End Property


        <ColumnInfo("ContractType", "{0}")> _
        Public Property ContractType() As Short
            Get
                Return _contractType
            End Get
            Set(ByVal value As Short)
                _contractType = value
            End Set
        End Property


        <ColumnInfo("Purpose", "{0}")> _
        Public Property Purpose() As Short
            Get
                Return _purpose
            End Get
            Set(ByVal value As Short)
                _purpose = value
            End Set
        End Property


        <ColumnInfo("ProjectName", "'{0}'")> _
        Public Property ProjectName() As String
            Get
                Return _projectName
            End Get
            Set(ByVal value As String)
                _projectName = value
            End Set
        End Property


        <ColumnInfo("ProductionYear", "{0}")> _
        Public Property ProductionYear() As Short
            Get
                Return _productionYear
            End Get
            Set(ByVal value As Short)
                _productionYear = value
            End Set
        End Property


        <ColumnInfo("FreePPh22Indicator", "{0}")> _
        Public Property FreePPh22Indicator() As Integer
            Get
                Return _freePPh22Indicator
            End Get
            Set(ByVal value As Integer)
                _freePPh22Indicator = value
            End Set
        End Property


        <ColumnInfo("FreePPh22LastUpdateBy", "'{0}'")> _
        Public Property FreePPh22LastUpdateBy() As String
            Get
                Return _freePPh22LastUpdateBy
            End Get
            Set(ByVal value As String)
                _freePPh22LastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("FreePPh22LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property FreePPh22LastUpdateTime() As DateTime
            Get
                Return _freePPh22LastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _freePPh22LastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("SPLNumber", "'{0}'")> _
        Public Property SPLNumber() As String
            Get
                Return _sPLNumber
            End Get
            Set(ByVal value As String)
                _sPLNumber = value
            End Set
        End Property


        <ColumnInfo("FreeIntIndicator", "{0}")> _
        Public Property FreeIntIndicator() As Integer
            Get
                Return _freeIntIndicator
            End Get
            Set(ByVal value As Integer)
                _freeIntIndicator = value
            End Set
        End Property

        <ColumnInfo("RefContractNumber", "{0}")> _
        Public Property RefContractNumber() As String
            Get
                Return _refContractNumber
            End Get
            Set(ByVal value As String)
                _refContractNumber = value
            End Set
        End Property
        <ColumnInfo("IsCarriedOver", "{0}")> _
        Public Property IsCarriedOver() As Short
            Get
                Return _isCarriedOver
            End Get
            Set(ByVal value As Short)
                _isCarriedOver = value
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


        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("Category", "ID", "ContractHeader", "CategoryID")> _
        Public Property Category() As Category
            Get
                Try
                    If Not IsNothing(Me._category) AndAlso (Not Me._category.IsLoaded) Then

                        Me._category = CType(DoLoad(GetType(Category).ToString(), _category.ID), Category)
                        Me._category.MarkLoaded()

                    End If

                    Return Me._category

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Category)

                Me._category = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._category.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "ContractHeader", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PKHeaderID", "{0}"), _
        RelationInfo("PKHeader", "ID", "ContractHeader", "PKHeaderID")> _
        Public Property PKHeader(Optional ByVal IsForMapper As Boolean = False) As PKHeader
            Get
                Try
                    'If Not IsNothing(Me._pKHeader) AndAlso (Not Me._pKHeader.IsLoaded) Then
                    '    Me._pKHeader = CType(DoLoad(GetType(PKHeader).ToString(), _pKHeader.ID), PKHeader)
                    '    Me._pKHeader.MarkLoaded()
                    'End If
                    'Return Me._pKHeader

                    Me._pKHeader = New PKHeader
                    'If IsNothing(Me.PKHeader) Then ' Not IsNothing(Me._pKHeader) AndAlso (Not Me._pKHeader.IsLoaded) Then
                    Dim arlPKH As New ArrayList
                    Dim crtPKH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crtPKH.opAnd(New Criteria(GetType(PKHeader), "PKNumber", MatchType.Exact, Me._pKNumber))
                    arlPKH = DoLoadArray(GetType(PKHeader).ToString, crtPKH)
                    If arlPKH.Count > 0 Then
                        Me._pKHeader = CType(arlPKH(0), PKHeader) ' CType(DoLoad(GetType(PKHeader).ToString(), _pKHeader.ID), PKHeader)
                        Me._pKHeader.MarkLoaded()
                    Else
                        If IsForMapper = True Then
                            Dim aPKHs As ArrayList
                            Dim cPKH As New CriteriaComposite(New Criteria(GetType(PKHeader), "PKNumber", MatchType.Exact, Me._pKNumber))
                            aPKHs = DoLoadArray(GetType(PKHeader).ToString, cPKH)
                            If aPKHs.Count > 0 Then
                                Me._pKHeader = CType(aPKHs(0), PKHeader)
                                Me._pKHeader.MarkLoaded()
                            End If
                        End If
                    End If
                    'End If

                    Return Me._pKHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PKHeader)

                Me._pKHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pKHeader.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("ContractHeader", "ID", "ContractDetail", "ContractHeaderID")> _
        Public ReadOnly Property ContractDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._contractDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ContractDetail), "ContractHeader.ID", MatchType.Exact, Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._contractDetails = DoLoadArray(GetType(ContractDetail).ToString, criterias)
                    End If

                    Return Me._contractDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


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

        Public ReadOnly Property TotalContract() As Double
            Get
                'Todo Aggregate
                Dim _total As Double = 0
                For Each item As ContractDetail In Me.ContractDetails
                    'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                    _total += (item.Amount + (item.PPh22 * item.ContractHeader.FreePPh22Indicator)) * item.TargetQty
                    '_total += (item.Amount + (item.PPh22 * IIf(item.ContractHeader.FreePPh22Indicator = 1, 0, 1))) * item.TargetQty
                    'End    :RemainModule-DailyPO:FreePPh By:Doni N
                Next
                Return _total
            End Get

        End Property

        Private _pricingPeriod As Date
        Public Property PricingPeriod() As Date
            Get
                Return _pricingPeriod
            End Get
            Set(ByVal Value As Date)
                _pricingPeriod = Value
            End Set
        End Property

        Public ReadOnly Property TotalSisaJumlahTebus() As Double
            Get
                Dim _total As Double
                Dim arrContractDetail As ArrayList = Me.ContractDetails
                Dim pphIndicator As Integer = 0
                For Each item As ContractDetail In arrContractDetail
                    If item.TargetQty > 0 Then
                        'Start  :RemainModule-DailyPO:FreePPh By:Doni N
                        _total = _total + (CType(item.TargetQty, Double) * (CType(item.Amount, Double) - CType(item.GuaranteeAmount, Double) + (CInt(item.ContractHeader.FreePPh22Indicator) * CType(item.PPh22, Double))))
                        '_total = _total + (CType(item.TargetQty, Double) * (CType(item.Amount, Double) + (IIf(CInt(item.ContractHeader.FreePPh22Indicator) = 1, 0, 1) * CType(item.PPh22, Double))))
                        'End    :RemainModule-DailyPO:FreePPh By:Doni N
                    End If
                    pphIndicator = item.ContractHeader.FreePPh22Indicator
                Next

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.ContractHeader.ID", MatchType.Exact, Me.ID))
                criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Integer) & "," & CType(enumStatusPO.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Konfirmasi, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
                Dim m_PODetailMapper As IMapper
                m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
                Dim PODetailColl As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias)
                If (PODetailColl.Count > 0) Then
                    For Each item As PODetail In PODetailColl
                        If item.POHeader.Status = CInt(enumStatusPO.Status.Baru) OrElse item.POHeader.Status = CInt(enumStatusPO.Status.Konfirmasi) Then
                            If item.ReqQty > 0 Then
                                _total = _total - (CType(item.ReqQty, Double) * (CType(item.ContractDetail.Amount, Double) - CType(item.ContractDetail.GuaranteeAmount, Double) + (CType(item.ContractDetail.PPh22, Double) * pphIndicator)))

                            End If
                        ElseIf item.POHeader.Status = CInt(enumStatusPO.Status.Rilis) OrElse item.POHeader.Status = CInt(enumStatusPO.Status.Setuju) OrElse item.POHeader.Status = CInt(enumStatusPO.Status.Selesai) Then
                            If item.AllocQty > 0 Then
                                _total = _total - (CType(item.AllocQty, Double) * (CType(item.ContractDetail.Amount, Double) - CType(item.ContractDetail.GuaranteeAmount, Double) + (CType(item.ContractDetail.PPh22, Double) * pphIndicator)))

                            End If
                        End If
                    Next
                End If
                Return _total
            End Get
        End Property


        Public ReadOnly Property TotalSisaUnit() As Double
            Get
                'Todo Aggregate
                Dim _total As Double
                Dim arrContractDetail As ArrayList = Me.ContractDetails
                For Each item As ContractDetail In arrContractDetail
                    If item.TargetQty > 0 Then
                        _total = _total + (CType(item.TargetQty, Double))
                    End If

                Next

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(PODetail), "ContractDetail.ContractHeader.ID", MatchType.Exact, Me.ID))
                criterias.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Integer) & "," & CType(enumStatusPO.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Konfirmasi, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
                Dim m_PODetailMapper As IMapper
                m_PODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(PODetail).ToString)
                Dim PODetailColl As ArrayList = m_PODetailMapper.RetrieveByCriteria(criterias)
                If (PODetailColl.Count > 0) Then
                    For Each item As PODetail In PODetailColl
                        If item.POHeader.Status = CInt(enumStatusPO.Status.Baru) OrElse item.POHeader.Status = CInt(enumStatusPO.Status.Konfirmasi) Then
                            If item.ReqQty > 0 Then
                                _total = _total - (CType(item.ReqQty, Double))

                            End If
                        ElseIf item.POHeader.Status = CInt(enumStatusPO.Status.Rilis) OrElse item.POHeader.Status = CInt(enumStatusPO.Status.Setuju) OrElse item.POHeader.Status = CInt(enumStatusPO.Status.Selesai) Then
                            If item.AllocQty > 0 Then
                                _total = _total - (CType(item.AllocQty, Double))
                            End If
                        End If
                    Next
                End If
                Return _total
            End Get
        End Property

        Public ReadOnly Property TotalQuantity() As Double
            Get
                'Todo Aggregate
                Dim Total As Double
                For Each item As ContractDetail In Me.ContractDetails
                    'If (Me.PKStatus = enumStatusPK.Status.Tidak_Setuju OrElse Me.PKStatus = enumStatusPK.Status.DiBlok OrElse Me.PKStatus = enumStatusPK.Status.Rilis OrElse Me.PKStatus = enumStatusPK.Status.Setuju OrElse Me.PKStatus = enumStatusPK.Status.Selesai) Then
                    '    Total = Total + (CType(item.ResponseQty, Long))
                    'Else
                    '    Total = Total + (CType(item.TargetQty, Long))
                    'End If
                    Total = Total + (CType(item.TargetQty, Long))
                Next
                Return Total
            End Get
        End Property
#End Region

    End Class
End Namespace