
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODraftHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 12/14/2018 - 2:57:42 PM
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
    <Serializable(), TableInfo("PODraftHeader")> _
    Public Class PODraftHeader
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
        'private _dealerID as integer 		
        Private _draftPONumber As String = String.Empty
        Private _status As String = String.Empty
        'private _contractHeaderID as integer 		
        Private _reqAllocationDate As Byte
        Private _reqAllocationMonth As Byte
        Private _reqAllocationYear As Short
        Private _reqAllocationDateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _effectiveDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerPONumber As String = String.Empty
        'private _termOfPaymentID as integer 		
        Private _pOType As String = String.Empty
        Private _freePPh22Indicator As Byte
        Private _passTOP As Byte
        Private _isFactoring As Short
        'private _sPLID as integer 		
        Private _isTransfer As Short
        'private _pODestinationID as integer 		
        'private _pOHeaderID as short 		
        Private _submitPODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _contractHeader As ContractHeader
        Private _termOfPayment As TermOfPayment
        Private _sPL As SPL
        Private _pODestination As PODestination = Nothing
        Private _pOHeader As POHeader

        Private _pODraftDetail As System.Collections.ArrayList = New System.Collections.ArrayList

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        '<ColumnInfo("DealerID","{0}")> _
        'public property DealerID as integer
        '	get
        '		return _dealerID
        '	end get
        '	set(byval value as integer)
        '		_dealerID= value
        '	end set
        'end property


        <ColumnInfo("DraftPONumber", "'{0}'")> _
        Public Property DraftPONumber As String
            Get
                Return _draftPONumber
            End Get
            Set(ByVal value As String)
                _draftPONumber = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        '<ColumnInfo("ContractHeaderID","{0}")> _
        'public property ContractHeaderID as integer
        '	get
        '		return _contractHeaderID
        '	end get
        '	set(byval value as integer)
        '		_contractHeaderID= value
        '	end set
        'end property


        <ColumnInfo("ReqAllocationDate", "{0}")> _
        Public Property ReqAllocationDate As Byte
            Get
                Return _reqAllocationDate
            End Get
            Set(ByVal value As Byte)
                _reqAllocationDate = value
            End Set
        End Property


        <ColumnInfo("ReqAllocationMonth", "{0}")> _
        Public Property ReqAllocationMonth As Byte
            Get
                Return _reqAllocationMonth
            End Get
            Set(ByVal value As Byte)
                _reqAllocationMonth = value
            End Set
        End Property


        <ColumnInfo("ReqAllocationYear", "{0}")> _
        Public Property ReqAllocationYear As Short
            Get
                Return _reqAllocationYear
            End Get
            Set(ByVal value As Short)
                _reqAllocationYear = value
            End Set
        End Property


        <ColumnInfo("ReqAllocationDateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ReqAllocationDateTime As DateTime
            Get
                Return _reqAllocationDateTime
            End Get
            Set(ByVal value As DateTime)
                _reqAllocationDateTime = value
            End Set
        End Property


        <ColumnInfo("EffectiveDate", "'{0:yyyy/MM/dd}'")> _
        Public Property EffectiveDate As DateTime
            Get
                Return _effectiveDate
            End Get
            Set(ByVal value As DateTime)
                _effectiveDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DealerPONumber", "'{0}'")> _
        Public Property DealerPONumber As String
            Get
                Return _dealerPONumber
            End Get
            Set(ByVal value As String)
                _dealerPONumber = value
            End Set
        End Property


        '<ColumnInfo("TermOfPaymentID","{0}")> _
        'public property TermOfPaymentID as integer
        '	get
        '		return _termOfPaymentID
        '	end get
        '	set(byval value as integer)
        '		_termOfPaymentID= value
        '	end set
        'end property


        <ColumnInfo("POType", "'{0}'")> _
        Public Property POType As String
            Get
                Return _pOType
            End Get
            Set(ByVal value As String)
                _pOType = value
            End Set
        End Property


        <ColumnInfo("FreePPh22Indicator", "{0}")> _
        Public Property FreePPh22Indicator As Byte
            Get
                Return _freePPh22Indicator
            End Get
            Set(ByVal value As Byte)
                _freePPh22Indicator = value
            End Set
        End Property


        <ColumnInfo("PassTOP", "{0}")> _
        Public Property PassTOP As Byte
            Get
                Return _passTOP
            End Get
            Set(ByVal value As Byte)
                _passTOP = value
            End Set
        End Property


        <ColumnInfo("IsFactoring", "{0}")> _
        Public Property IsFactoring As Short
            Get
                Return _isFactoring
            End Get
            Set(ByVal value As Short)
                _isFactoring = value
            End Set
        End Property


        '<ColumnInfo("SPLID","{0}")> _
        'public property SPLID as integer
        '	get
        '		return _sPLID
        '	end get
        '	set(byval value as integer)
        '		_sPLID= value
        '	end set
        'end property


        <ColumnInfo("IsTransfer", "{0}")> _
        Public Property IsTransfer As Short
            Get
                Return _isTransfer
            End Get
            Set(ByVal value As Short)
                _isTransfer = value
            End Set
        End Property


        '<ColumnInfo("PODestinationID","{0}")> _
        'public property PODestinationID as integer
        '	get
        '		return _pODestinationID
        '	end get
        '	set(byval value as integer)
        '		_pODestinationID= value
        '	end set
        'end property


        '<ColumnInfo("POHeaderID","{0}")> _
        'public property POHeaderID as short
        '	get
        '		return _pOHeaderID
        '	end get
        '	set(byval value as short)
        '		_pOHeaderID= value
        '	end set
        'end property


        <ColumnInfo("SubmitPODate", "{0}")> _
        Public Property SubmitPODate As DateTime
            Get
                Return _submitPODate
            End Get
            Set(ByVal value As DateTime)
                _submitPODate = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property

        <ColumnInfo("ContractHeaderID", "{0}"), _
        RelationInfo("ContractHeader", "ID", "PODraftHeader", "ContractHeaderID")> _
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
        RelationInfo("TermOfPayment", "ID", "PODraftHeader", "TermOfPaymentID")> _
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
        RelationInfo("Dealer", "ID", "PODraftHeader", "DealerID")> _
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
                    If Not IsNothing(Me._sPL) AndAlso (Not Me._sPL.IsLoaded) Then

                        Me._sPL = CType(DoLoad(GetType(SPL).ToString(), _sPL.ID), SPL)
                        Me._sPL.MarkLoaded()

                    End If

                    Return Me._sPL

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPL)

                Me._sPL = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPL.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("PODestinationID", "{0}"), _
        RelationInfo("PODestination", "ID", "POHeader", "PODestinationID")> _
        Public Property PODestination As PODestination
            Get
                Try
                    If Not IsNothing(Me._pODestination) AndAlso (Not Me._pODestination.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pODestination.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("PODraftHeader", "ID", "PODraftDetail", "PODraftHeaderID")> _
        Public ReadOnly Property PODraftDetail() As System.Collections.ArrayList
            Get
                Try
                    If (Me._pODraftDetail.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(PODraftDetail), "PODraftHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(PODraftDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pODraftDetail = DoLoadArray(GetType(PODraftDetail).ToString, criterias)
                    End If

                    Return Me._pODraftDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("POHeaderID", "{0}"), _
        RelationInfo("POHeader", "ID", "PODraftHeader", "POHeaderID")> _
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

        Public ReadOnly Property TotalGuarantee() As Decimal
            Get
                'Todo Replace with view
                Dim Total As Decimal = 0
                If Me.TermOfPayment.PaymentType <> enumPaymentType.PaymentType.TOP Then
                    Return Total
                End If
                For Each objPOD As PODraftDetail In Me.PODraftDetail
                    If Me.Status = enumStatusPO.Status.Konfirmasi Then
                        'If objPOD.AllocQty > 0 Then
                        '    Total += objPOD.ContractDetail.GuaranteeAmount * objPOD.AllocQty
                        'Else
                        Total += objPOD.ContractDetail.GuaranteeAmount * objPOD.ReqQty
                        'End If
                    ElseIf Me.Status = 0 Then
                        Total += objPOD.ContractDetail.GuaranteeAmount * objPOD.ReqQty
                    Else
                        Total += objPOD.ContractDetail.GuaranteeAmount * objPOD.ReqQty
                        'Total += objPOD.ContractDetail.GuaranteeAmount * objPOD.AllocQty
                    End If
                Next
                Return Total
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
        Private _V_RekapDraftPO As V_RekapDraftPO
        Public ReadOnly Property V_RekapDraftPO() As V_RekapDraftPO
            Get
                Try
                    If IsNothing(Me._V_RekapDraftPO) And Me.ID > 0 Then

                        Me._V_RekapDraftPO = CType(DoLoad(GetType(V_RekapDraftPO).ToString(), Me.ID), V_RekapDraftPO)
                        Me._V_RekapDraftPO.MarkLoaded()

                    End If

                    Return Me._V_RekapDraftPO

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
        End Property


        Private _v_DraftPOTotalDetail As V_DraftPOTotalDetail
        Public ReadOnly Property v_DraftPOTotalDetail() As V_DraftPOTotalDetail
            Get
                Try
                    If IsNothing(Me._v_DraftPOTotalDetail) And Me.ID > 0 Then

                        Me._v_DraftPOTotalDetail = CType(DoLoad(GetType(V_DraftPOTotalDetail).ToString(), Me.ID), V_DraftPOTotalDetail)
                        Me._v_DraftPOTotalDetail.MarkLoaded()

                    End If

                    Return Me._v_DraftPOTotalDetail

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

                If IsNothing(Me.v_DraftPOTotalDetail) Then
                    Return 0
                Else
                    Return Me.v_DraftPOTotalDetail.TotalDetail
                End If
            End Get
        End Property

        Public ReadOnly Property TotalHarga() As Decimal
            Get

                If Not IsNothing(Me.V_RekapDraftPO) Then
                    Return V_RekapDraftPO.TotalHarga
                Else
                    Return 0
                End If

            End Get
        End Property

        Public ReadOnly Property TotalHargaPP() As Double
            Get
                If Not IsNothing(Me.V_RekapDraftPO) Then
                    Return V_RekapDraftPO.TotalHargaPP
                Else
                    Return 0
                End If
            End Get
        End Property

        Public ReadOnly Property TotalHargaIT() As Double
            Get
                If Not IsNothing(Me.V_RekapDraftPO) Then
                    Return V_RekapDraftPO.TotalHargaIT
                Else
                    Return 0
                End If
            End Get
        End Property

        Public ReadOnly Property TotalHargaLC() As Double
            Get
                If Not IsNothing(Me.V_RekapDraftPO) Then
                    Return V_RekapDraftPO.TotalHargaLC
                Else
                    Return 0
                End If
            End Get
        End Property


#End Region

    End Class
End Namespace

