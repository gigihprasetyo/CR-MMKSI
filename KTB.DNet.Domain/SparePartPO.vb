
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPO Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 11/21/2011 - 11:03:12 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
Imports System.Web.UI.WebControls

#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("SparePartPO")> _
    Public Class SparePartPO
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
        Private _orderType As String = String.Empty
        Private _pODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _deliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _processCode As String = String.Empty
        Private _cancelRequestBy As String = String.Empty
        Private _indentTransfer As Short
        Private _pickingTicket As String = String.Empty
        Private _sentPODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isTransfer As Boolean
        Private _dMSPRNo As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _sparePartPODetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sparePartPOEstimate As SparePartPOEstimate = New SparePartPOEstimate(0)
        Private _sparePartPOStatus As SparePartPOStatus = New SparePartPOStatus(0)
        Private _termOfPayment As TermOfPayment
        Private _topBlockStatus As TOPBlockStatus
        Private _pQRHeader As PQRHeader

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


        <ColumnInfo("OrderType", "'{0}'")> _
        Public Property OrderType() As String
            Get
                Return _orderType
            End Get
            Set(ByVal value As String)
                _orderType = value
            End Set
        End Property


        <ColumnInfo("PODate", "'{0:yyyy/MM/dd}'")> _
        Public Property PODate() As DateTime
            Get
                Return _pODate
            End Get
            Set(ByVal value As DateTime)
                _pODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DeliveryDate() As DateTime
            Get
                Return _deliveryDate
            End Get
            Set(ByVal value As DateTime)
                _deliveryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ProcessCode", "'{0}'")> _
        Public Property ProcessCode() As String
            Get
                Return _processCode
            End Get
            Set(ByVal value As String)
                _processCode = value
            End Set
        End Property


        <ColumnInfo("CancelRequestBy", "'{0}'")> _
        Public Property CancelRequestBy() As String
            Get
                Return _cancelRequestBy
            End Get
            Set(ByVal value As String)
                _cancelRequestBy = value
            End Set
        End Property


        <ColumnInfo("IndentTransfer", "{0}")> _
        Public Property IndentTransfer() As Short
            Get
                Return _indentTransfer
            End Get
            Set(ByVal value As Short)
                _indentTransfer = value
            End Set
        End Property


        <ColumnInfo("PickingTicket", "'{0}'")> _
        Public Property PickingTicket() As String
            Get
                Return _pickingTicket
            End Get
            Set(ByVal value As String)
                _pickingTicket = value
            End Set
        End Property

        <ColumnInfo("SentPODate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SentPODate() As DateTime
            Get
                Return _sentPODate
            End Get
            Set(ByVal value As DateTime)
                _sentPODate = value
            End Set
        End Property

        <ColumnInfo("IsTransfer", "{0}")> _
        Public Property IsTransfer() As Integer
            Get
                Return _isTransfer
            End Get
            Set(ByVal value As Integer)
                _isTransfer = value
            End Set
        End Property

        <ColumnInfo("DMSPRNo", "'{0}'")> _
        Public Property DMSPRNo() As String
            Get
                Return _dMSPRNo
            End Get
            Set(ByVal value As String)
                _dMSPRNo = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "SparePartPO", "DealerID")> _
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


        <ColumnInfo("TermOfPaymentID", "{0}"), _
        RelationInfo("TermOfPayment", "ID", "SparePartPO", "TermOfPaymentID")> _
        Public Property TermOfPayment As TermOfPayment
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


        <ColumnInfo("TOPBlockStatusID", "{0}"), _
        RelationInfo("TOPBlockStatus", "ID", "SparePartPO", "TOPBlockStatusID")> _
        Public Property TOPBlockStatus As TOPBlockStatus
            Get
                Try
                    If Not IsNothing(Me._topBlockStatus) AndAlso (Not Me._topBlockStatus.IsLoaded) Then

                        Me._topBlockStatus = CType(DoLoad(GetType(TOPBlockStatus).ToString(), _topBlockStatus.ID), TOPBlockStatus)
                        Me._topBlockStatus.MarkLoaded()

                    End If

                    Return Me._topBlockStatus

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TOPBlockStatus)

                Me._topBlockStatus = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._topBlockStatus.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("SparePartPO", "ID", "SparePartPODetail", "SparePartPOID")> _
        Public ReadOnly Property SparePartPODetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartPODetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartPODetail), "SparePartPO", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartPODetails = DoLoadArray(GetType(SparePartPODetail).ToString, criterias)
                    End If

                    Return Me._sparePartPODetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        <ColumnInfo("ID", "{0}"), _
        RelationInfo("SparePartPO", "ID", "SparePartPOEstimate", "SparePartPOID")> _
        Public ReadOnly Property SparePartPOEstimate() As SparePartPOEstimate
            Get
                Try
                    If Not IsNothing(Me._sparePartPOEstimate) AndAlso (Not Me._sparePartPOEstimate.IsLoaded) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartPOEstimate), "SparePartPO", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(SparePartPOEstimate).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._sparePartPOEstimate = CType(tempColl(0), SparePartPOEstimate)
                        Else
                            Me._sparePartPOEstimate = Nothing
                        End If
                    End If

                    Return Me._sparePartPOEstimate

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
        End Property

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("SparePartPO", "ID", "SparePartPOStatus", "SparePartPOID")> _
        Public ReadOnly Property SparePartPOStatus() As SparePartPOStatus
            Get
                Try
                    If Not IsNothing(Me._sparePartPOStatus) AndAlso (Not Me._sparePartPOStatus.IsLoaded) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartPOStatus), "SparePartPO", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartPOStatus), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(SparePartPOStatus).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._sparePartPOStatus = CType(tempColl(0), SparePartPOStatus)
                        Else
                            Me._sparePartPOStatus = Nothing
                        End If
                    End If

                    Return Me._sparePartPOStatus

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
        End Property


        <ColumnInfo("PQRHeaderID", "{0}"), _
        RelationInfo("PQRHeader", "ID", "SparePartPO", "PQRHeaderID")> _
        Public Property PQRHeader As PQRHeader
            Get
                Try
                    If Not IsNothing(Me._pQRHeader) AndAlso (Not Me._pQRHeader.IsLoaded) Then

                        Me._pQRHeader = CType(DoLoad(GetType(PQRHeader).ToString(), _pQRHeader.ID), PQRHeader)
                        Me._pQRHeader.MarkLoaded()

                    End If

                    Return Me._pQRHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PQRHeader)

                Me._pQRHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pQRHeader.MarkLoaded()
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




#Region "Custom Properties"

        Private _tempIDIndentPartHeader As Integer

        Public ReadOnly Property IndentTransferDesc() As String
            Get
                If Me.IndentTransfer = 1 Then
                    Return "Sudah"
                Else
                    Return "Belum"
                End If
            End Get
        End Property

        Public Property TempIDIndentPartHeader() As Integer
            Get
                Return _tempIDIndentPartHeader
            End Get
            Set(ByVal value As Integer)
                _tempIDIndentPartHeader = value
            End Set
        End Property

        Private Shared _arrayListOrderType As ArrayList

        Public ReadOnly Property OrderTypeDesc() As String
            Get
                If _orderType.Trim <> "" Then
                    _arrayListOrderType = CType(LookUp.ArraySPOrderTypeKTBDealer, ArrayList)
                    If _arrayListOrderType.Count > 0 Then
                        Dim objListItemOrderType As ListItem = (From i As ListItem In _arrayListOrderType Where CType(i.Value, String) = _orderType).FirstOrDefault()
                        If Not IsNothing(objListItemOrderType) Then
                            Return objListItemOrderType.Text
                        Else
                            Return String.Empty
                        End If
                    Else
                        Return String.Empty
                    End If
                Else
                    Return String.Empty
                End If

                'If _orderType = "E" Then
                '    Return "Emergency"
                'ElseIf _orderType = "R" Then
                '    Return "Regular"
                'ElseIf _orderType.ToUpper = "K" Then
                '    Return "P.Khusus"
                'ElseIf _orderType.ToUpper = "I" Then
                '    Return "Indent"
                'ElseIf _orderType = "Z" Then
                '    Return "Others Reguler"
                'ElseIf _orderType = "Y" Then
                '    Return "Others Emergency"
                'Else
                '    Return String.Empty
                'End If
            End Get
        End Property

        Public ReadOnly Property ProcessCodeDesc() As String
            Get
                If _processCode = "" Then
                    Return "Baru"
                ElseIf _processCode = "S" Then
                    Return "Telah dikirim"
                ElseIf _processCode = "P" Then
                    Return "Telah diproses"
                ElseIf _processCode = "C" Then
                    Return "Batal"
                ElseIf _processCode = "X" Then
                    Return "Batal MMKSI"
                ElseIf _processCode = "T" Then
                    Return "Tidak Dipenuhi"
                ElseIf _processCode = "F" Then
                    Return "Selesai"
                ElseIf _processCode = "A" Then
                    Return "Alokasi Sebagian"
                ElseIf _processCode = "R" Then
                    Return "Rilis"
                ElseIf _processCode = "O" Then
                    Return "Batal Order"
                ElseIf _processCode = "N" Then
                    Return "Baru"
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        'Public ReadOnly Property LookUpStatus() As Integer
        '    Get
        '        If _processCode = "" Then
        '            Return SPPOProcessCode.LOOKUP_N_NEW

        '        ElseIf _processCode = "S" Then
        '            Return SPPOProcessCode.LOOKUP_S_SENT

        '        ElseIf _processCode = "P" Then
        '            Return SPPOProcessCode.LOOKUP_P_PROCEED

        '        ElseIf _processCode = "C" Then
        '            Return SPPOProcessCode.LOOKUP_C_CANCEL

        '        ElseIf _processCode = "X" Then
        '            Return SPPOProcessCode.LOOKUP_X_CANCEL_KTB

        '        ElseIf _processCode = "T" Then
        '            Return SPPOProcessCode.LOOKUP_T_REJECT

        '        ElseIf _processCode = "F" Then
        '            Return SPPOProcessCode.LOOKUP_F_FINISH

        '        ElseIf _processCode = "A" Then
        '            Return SPPOProcessCode.LOOKUP_A_ALOCATION

        '        ElseIf _processCode = "R" Then
        '            Return SPPOProcessCode.LOOKUP_R_RELEASE

        '        ElseIf _processCode = "O" Then
        '            Return SPPOProcessCode.LOOKUP_V_ORDER_TO_VENDOR

        '        ElseIf _processCode = "N" Then
        '            Return SPPOProcessCode.LOOKUP_N_NEW

        '        Else
        '            Return 0

        '        End If

        '    End Get
        'End Property


        'Public ReadOnly Property POAmount() As Decimal
        '    Get
        '        Dim dTotal As Decimal = 0
        '        If Not IsNothing(_sparePartPODetails) Then
        '            For Each poDetail As SparePartPODetail In _sparePartPODetails
        '                dTotal = dTotal + (poDetail.Quantity * poDetail.RetailPrice)
        '            Next
        '        End If
        '        Return dTotal

        '    End Get
        'End Property

#End Region

#Region "Custom Method"
        Public ReadOnly Property ItemCount() As Integer
            Get
                'Todo Aggregate
                Return Me.SparePartPODetails.Count
            End Get
        End Property



        Public ReadOnly Property ItemAmount() As Int64
            Get
                'Todo Aggregate
                Dim amount As Int64 = 0
                For Each item As SparePartPODetail In Me.SparePartPODetails
                    amount += (item.Quantity * item.RetailPrice)
                Next
                Return amount
            End Get
        End Property

        Public ReadOnly Property ItemQuantity() As Int64
            Get
                'Todo Aggregate
                Dim qty As Integer = 0
                For Each item As SparePartPODetail In Me.SparePartPODetails
                    qty += item.Quantity
                Next
                Return qty
            End Get
        End Property


        Private _IsChangedWSM As Boolean

        Public Property IsChangedWSM() As Boolean
            Get
                Return _IsChangedWSM
            End Get
            Set(ByVal value As Boolean)
                _IsChangedWSM = value
            End Set
        End Property

        Public Sub SyncProcessCode()
            Dim oSPPO As SparePartPO

            If Me.ID > 0 Then
                oSPPO = CType(DoLoad(GetType(SparePartPO).ToString(), Me.ID), SparePartPO)

                If Not IsNothing(oSPPO) AndAlso oSPPO.ID > 0 Then
                    Me.ProcessCode = oSPPO.ProcessCode
                End If
            ElseIf Me.PONumber.Trim <> String.Empty Then
                Dim cSPPO As New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim aSPPOs As ArrayList

                cSPPO.opAnd(New Criteria(GetType(SparePartPO), "PONumber", MatchType.Exact, Me.PONumber))
                aSPPOs = DoLoadArray(GetType(SparePartPO).ToString, cSPPO)
                If aSPPOs.Count > 0 Then
                    oSPPO = aSPPOs(0)
                    Me.ProcessCode = oSPPO.ProcessCode
                End If
            End If
        End Sub

#End Region

    End Class
End Namespace

