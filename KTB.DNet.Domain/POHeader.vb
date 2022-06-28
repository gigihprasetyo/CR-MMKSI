
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : POHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/20/2009 - 9:28:34 AM
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
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("POHeader")> _
    Public Class POHeader
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
        Private _pONumber As String = String.Empty
        Private _status As String = String.Empty
        Private _reqAllocationDate As Byte
        Private _reqAllocationMonth As Byte
        Private _reqAllocationYear As Short
        Private _reqAllocationDateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _effectiveDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerPONumber As String = String.Empty
        Private _pOType As String = String.Empty
        Private _releaseDate As Byte
        Private _releaseMonth As Byte
        Private _releaseYear As Short
        Private _sONumber As String = String.Empty
        Private _freePPh22Indicator As Byte
        Private _passTOP As Byte
        Private _lastReqAllocationDateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _remarkStatus As Short
        Private _dOBlockHistory As Short
        Private _remark As String = String.Empty
        Private _changedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _changedBy As String = String.Empty
        Private _blockedStatus As Short
        Private _isFactoring As Short
        'Private _sPLID As Integer
        Private _isTransfer As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _contractHeader As ContractHeader
        Private _termOfPayment As TermOfPayment
        Private _dealer As Dealer
        Private _sPL As SPL
        Private _pODestination As PODestination = Nothing

        Private _dailyPayments As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _salesOrders As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _pODetails As System.Collections.ArrayList = New System.Collections.ArrayList


        Private _isHavingGyro As Boolean

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


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
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


        <ColumnInfo("ReqAllocationDate", "{0}")> _
        Public Property ReqAllocationDate() As Byte
            Get
                Return _reqAllocationDate
            End Get
            Set(ByVal value As Byte)
                _reqAllocationDate = value
            End Set
        End Property


        <ColumnInfo("ReqAllocationMonth", "{0}")> _
        Public Property ReqAllocationMonth() As Byte
            Get
                Return _reqAllocationMonth
            End Get
            Set(ByVal value As Byte)
                _reqAllocationMonth = value
            End Set
        End Property


        <ColumnInfo("ReqAllocationYear", "{0}")> _
        Public Property ReqAllocationYear() As Short
            Get
                Return _reqAllocationYear
            End Get
            Set(ByVal value As Short)
                _reqAllocationYear = value
            End Set
        End Property


        <ColumnInfo("ReqAllocationDateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ReqAllocationDateTime() As DateTime
            Get
                Return _reqAllocationDateTime
            End Get
            Set(ByVal value As DateTime)
                _reqAllocationDateTime = value
            End Set
        End Property

        <ColumnInfo("EffectiveDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EffectiveDate() As DateTime
            Get
                Return _effectiveDate
            End Get
            Set(ByVal value As DateTime)
                _effectiveDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("DealerPONumber", "'{0}'")> _
        Public Property DealerPONumber() As String
            Get
                Return _dealerPONumber
            End Get
            Set(ByVal value As String)
                _dealerPONumber = value
            End Set
        End Property


        <ColumnInfo("POType", "'{0}'")> _
        Public Property POType() As String
            Get
                Return _pOType
            End Get
            Set(ByVal value As String)
                _pOType = value
            End Set
        End Property


        <ColumnInfo("ReleaseDate", "{0}")> _
        Public Property ReleaseDate() As Byte
            Get
                Return _releaseDate
            End Get
            Set(ByVal value As Byte)
                _releaseDate = value
            End Set
        End Property


        <ColumnInfo("ReleaseMonth", "{0}")> _
        Public Property ReleaseMonth() As Byte
            Get
                Return _releaseMonth
            End Get
            Set(ByVal value As Byte)
                _releaseMonth = value
            End Set
        End Property


        <ColumnInfo("ReleaseYear", "{0}")> _
        Public Property ReleaseYear() As Short
            Get
                Return _releaseYear
            End Get
            Set(ByVal value As Short)
                _releaseYear = value
            End Set
        End Property


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property


        <ColumnInfo("FreePPh22Indicator", "{0}")> _
        Public Property FreePPh22Indicator() As Byte
            Get
                Return _freePPh22Indicator
            End Get
            Set(ByVal value As Byte)
                _freePPh22Indicator = value
            End Set
        End Property


        <ColumnInfo("PassTOP", "{0}")> _
        Public Property PassTOP() As Byte
            Get
                Return _passTOP
            End Get
            Set(ByVal value As Byte)
                _passTOP = value
            End Set
        End Property


        <ColumnInfo("LastReqAllocationDateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastReqAllocationDateTime() As DateTime
            Get
                Return _lastReqAllocationDateTime
            End Get
            Set(ByVal value As DateTime)
                _lastReqAllocationDateTime = value
            End Set
        End Property


        <ColumnInfo("RemarkStatus", "{0}")> _
        Public Property RemarkStatus() As Short
            Get
                Return _remarkStatus
            End Get
            Set(ByVal value As Short)
                _remarkStatus = value
            End Set
        End Property


        <ColumnInfo("DOBlockHistory", "{0}")> _
        Public Property DOBlockHistory() As Short
            Get
                Return _dOBlockHistory
            End Get
            Set(ByVal value As Short)
                _dOBlockHistory = value
            End Set
        End Property


        <ColumnInfo("Remark", "'{0}'")> _
        Public Property Remark() As String
            Get
                Return _remark
            End Get
            Set(ByVal value As String)
                _remark = value
            End Set
        End Property


        <ColumnInfo("ChangedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ChangedTime() As DateTime
            Get
                Return _changedTime
            End Get
            Set(ByVal value As DateTime)
                _changedTime = value
            End Set
        End Property


        <ColumnInfo("ChangedBy", "'{0}'")> _
        Public Property ChangedBy() As String
            Get
                Return _changedBy
            End Get
            Set(ByVal value As String)
                _changedBy = value
            End Set
        End Property


        <ColumnInfo("BlockedStatus", "{0}")> _
        Public Property BlockedStatus() As Short
            Get
                Return _blockedStatus
            End Get
            Set(ByVal value As Short)
                _blockedStatus = value
            End Set
        End Property

        <ColumnInfo("IsFactoring", "{0}")> _
          Public Property IsFactoring() As Short
            Get
                Return _isFactoring
            End Get
            Set(ByVal value As Short)
                _isFactoring = value
            End Set
        End Property

        '<ColumnInfo("SPLID", "{0}")> _
        'Public Property SPLID() As Integer
        '    Get
        '        Return _sPLID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _sPLID = value
        '    End Set
        'End Property


        <ColumnInfo("IsTransfer", "{0}")> _
        Public Property IsTransfer() As Short
            Get
                Return _isTransfer
            End Get
            Set(ByVal value As Short)
                _isTransfer = value
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


        <ColumnInfo("ContractHeaderID", "{0}"), _
        RelationInfo("ContractHeader", "ID", "POHeader", "ContractHeaderID")> _
        Public Property ContractHeader() As ContractHeader
            Get
                Try
                    If Not IsNothing(Me._contractHeader) AndAlso (Not Me._contractHeader.IsLoaded) Then

                        Me._contractHeader = CType(DoLoad(GetType(ContractHeader).ToString(), _contractHeader.ID), ContractHeader)
                        Me._contractHeader.MarkLoaded()

                    End If

                    Return Me._contractHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ContractHeader)

                Me._contractHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._contractHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("TermOfPaymentID", "{0}"), _
        RelationInfo("TermOfPayment", "ID", "POHeader", "TermOfPaymentID")> _
        Public Property TermOfPayment() As TermOfPayment
            Get
                Try
                    If Not IsNothing(Me._termOfPayment) AndAlso (Not Me._termOfPayment.IsLoaded) Then

                        Me._termOfPayment = CType(DoLoad(GetType(TermOfPayment).ToString(), _termOfPayment.ID), TermOfPayment)
                        Me._termOfPayment.MarkLoaded()

                    End If

                    Return Me._termOfPayment

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TermOfPayment)

                Me._termOfPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._termOfPayment.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "POHeader", "DealerID")> _
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


        <ColumnInfo("SPLID", "{0}"), _
        RelationInfo("SPL", "ID", "POHeader", "SPLID")> _
        Public Property SPL() As SPL
            Get
                Try
                    If Not IsNothing(Me._SPL) AndAlso (Not Me._SPL.IsLoaded) Then

                        Me._SPL = CType(DoLoad(GetType(SPL).ToString(), _SPL.ID), SPL)
                        Me._SPL.MarkLoaded()

                    End If

                    Return Me._SPL

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPL)

                Me._SPL = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._SPL.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("POHeader", "ID", "DailyPayment", "POID")> _
        Public ReadOnly Property DailyPayments() As System.Collections.ArrayList
            Get
                Try
                    If (Me._dailyPayments.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DailyPayment), "POHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._dailyPayments = DoLoadArray(GetType(DailyPayment).ToString, criterias)
                    End If

                    Return Me._dailyPayments

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("POHeader", "ID", "SalesOrder", "POHeaderID")> _
        Public ReadOnly Property SalesOrders() As System.Collections.ArrayList
            Get
                Try
                    If (Me._salesOrders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SalesOrder), "POHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._salesOrders = DoLoadArray(GetType(SalesOrder).ToString, criterias)
                    End If

                    Return Me._salesOrders

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("POHeader", "ID", "PODetail", "POHeaderID")> _
        Public ReadOnly Property PODetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pODetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PODetail), "POHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pODetails = DoLoadArray(GetType(PODetail).ToString, criterias)
                    End If

                    Return Me._pODetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("PODestinationID", "{0}"), _
        RelationInfo("PODestination", "ID", "POHeader", "PODestinationID")> _
        Public Property PODestination As PODestination
            Get
                Try
                    If Not isnothing(Me._pODestination) AndAlso (Not Me._pODestination.IsLoaded) Then

                        Me._pODestination = CType(DoLoad(GetType(PODestination).ToString(), _pODestination.ID), PODestination)
                        Me._pODestination.MarkLoaded()

                    End If

                    Return Me._pODestination

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PODestination)

                Me._pODestination = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pODestination.MarkLoaded()
                End If
            End Set
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
        Public ReadOnly Property TotalHarga() As Decimal
            Get

                If Not IsNothing(Me.V_RekapPO) Then
                    Return V_RekapPO.TotalHarga
                Else
                    Return 0
                End If

            End Get
        End Property

        Public ReadOnly Property TotalHargaPP() As Double
            Get
                If Not IsNothing(Me.V_RekapPO) Then
                    Return V_RekapPO.TotalHargaPP
                Else
                    Return 0
                End If
            End Get
        End Property

        Public ReadOnly Property TotalHargaIT() As Double
            Get
                If Not IsNothing(Me.V_RekapPO) Then
                    Return V_RekapPO.TotalHargaIT
                Else
                    Return 0
                End If
            End Get
        End Property

        Public ReadOnly Property TotalHargaLC() As Double
            Get
                If Not IsNothing(Me.V_RekapPO) Then
                    Return V_RekapPO.TotalHargaLC
                Else
                    Return 0
                End If
            End Get
        End Property

        ' End -------

        Public ReadOnly Property TotalHargaExposure() As Double
            Get
                Dim total As Double = 0
                If (Me.Status = enumStatusPO.Status.Baru OrElse Me.Status = enumStatusPO.Status.Konfirmasi) Then
                    For Each item As PODetail In Me.PODetails
                        total += CType(item.ReqQty, Double) * CType(item.ContractDetail.Amount, Double)
                    Next
                Else
                    If (Me.Status = enumStatusPO.Status.Rilis OrElse Me.Status = enumStatusPO.Status.Setuju OrElse Me.Status = enumStatusPO.Status.Selesai) Then
                        For Each item As PODetail In Me.PODetails
                            total += CType(item.AllocQty, Double) * CType(item.ContractDetail.Amount, Double)
                        Next
                    End If
                End If
                Return total
            End Get
        End Property

        Public ReadOnly Property TotalQuantity() As Integer
            Get
                If Not IsNothing(Me.V_RekapPO) Then
                    Return V_RekapPO.TotalQuantity
                Else
                    Return 0
                End If
            End Get
        End Property


        Private _V_RekapPO As V_RekapPO

        Public ReadOnly Property V_RekapPO() As V_RekapPO
            Get
                Try
                    If IsNothing(Me._V_RekapPO) And Me.ID > 0 Then

                        Me._V_RekapPO = CType(DoLoad(GetType(V_RekapPO).ToString(), Me.ID), V_RekapPO)
                        Me._V_RekapPO.MarkLoaded()

                    End If

                    Return Me._V_RekapPO

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get


        End Property

        Private _V_POTotalDetail As V_POTotalDetail
        Public ReadOnly Property V_POTotalDetail() As V_POTotalDetail
            Get
                Try
                    If IsNothing(Me._V_POTotalDetail) And Me.ID > 0 Then

                        Me._V_POTotalDetail = CType(DoLoad(GetType(V_POTotalDetail).ToString(), Me.ID), V_POTotalDetail)
                        Me._V_POTotalDetail.MarkLoaded()

                    End If

                    Return Me._V_POTotalDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get


        End Property


        Public ReadOnly Property TotalPODetail() As Decimal
            Get

                If IsNothing(Me.V_POTotalDetail) Then
                    Return 0
                Else
                    Return Me.V_POTotalDetail.TotalDetail
                End If
            End Get
        End Property

        Private _IsHavingGyroLoaded As Boolean
        Public Property IsHavingGyro() As Boolean
            Get
                If Me.V_POTotalDetail.Gyro > 0 Then
                    Return True
                Else
                    Return False
                End If
            End Get
            Set(ByVal Value As Boolean)
                Me._isHavingGyro = Value
            End Set
        End Property

        Public ReadOnly Property TotalGuarantee() As Decimal
            Get
                'Todo Replace with view
                Dim Total As Decimal = 0
                If Me.TermOfPayment.PaymentType <> enumPaymentType.PaymentType.TOP Then
                    Return Total
                End If
                For Each objPOD As PODetail In Me.PODetails
                    If Me.Status = enumStatusPO.Status.Konfirmasi Then
                        If objPOD.AllocQty > 0 Then
                            Total += objPOD.ContractDetail.GuaranteeAmount * objPOD.AllocQty
                        Else
                            Total += objPOD.ContractDetail.GuaranteeAmount * objPOD.ReqQty
                        End If
                    ElseIf Me.Status = 0 Then
                        Total += objPOD.ContractDetail.GuaranteeAmount * objPOD.ReqQty
                    Else
                        Total += objPOD.ContractDetail.GuaranteeAmount * objPOD.AllocQty
                    End If
                Next
                Return Total
            End Get
        End Property


#End Region

    End Class
End Namespace

