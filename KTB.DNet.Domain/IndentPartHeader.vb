
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : IndentPartHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/12/2008 - 1:48:56 PM
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
    <Serializable(), TableInfo("IndentPartHeader")> _
    Public Class IndentPartHeader
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
        Private _requestNo As String = String.Empty
        Private _requestDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _materialType As Integer
        Private _status As Byte
        Private _statusKTB As Byte
        Private _submitFile As String = String.Empty
        Private _paymentType As Byte
        Private _price As Decimal
        Private _kTBConfirmedDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _descID As Byte
        Private _chassisNumber As String = String.Empty
        Private _dMSPRNo As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _termOfPayment As TermOfPayment
        Private _topBlockStatus As TOPBlockStatus

        Private _indentPartDetails As System.Collections.ArrayList = New System.Collections.ArrayList

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


        <ColumnInfo("RequestNo", "'{0}'")> _
        Public Property RequestNo() As String
            Get
                Return _requestNo
            End Get
            Set(ByVal value As String)
                _requestNo = value
            End Set
        End Property


        <ColumnInfo("RequestDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RequestDate() As DateTime
            Get
                Return _requestDate
            End Get
            Set(ByVal value As DateTime)
                _requestDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("MaterialType", "{0}")> _
        Public Property MaterialType() As Integer
            Get
                Return _materialType
            End Get
            Set(ByVal value As Integer)
                _materialType = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Byte
            Get
                Return _status
            End Get
            Set(ByVal value As Byte)
                _status = value
            End Set
        End Property


        <ColumnInfo("StatusKTB", "{0}")> _
        Public Property StatusKTB() As Byte
            Get
                Return _statusKTB
            End Get
            Set(ByVal value As Byte)
                _statusKTB = value
            End Set
        End Property


        <ColumnInfo("SubmitFile", "'{0}'")> _
        Public Property SubmitFile() As String
            Get
                Return _submitFile
            End Get
            Set(ByVal value As String)
                _submitFile = value
            End Set
        End Property


        <ColumnInfo("PaymentType", "{0}")> _
        Public Property PaymentType() As Byte
            Get
                Return _paymentType
            End Get
            Set(ByVal value As Byte)
                _paymentType = value
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


        <ColumnInfo("KTBConfirmedDate", "'{0:yyyy/MM/dd}'")> _
        Public Property KTBConfirmedDate() As DateTime
            Get
                Return _kTBConfirmedDate
            End Get
            Set(ByVal value As DateTime)
                _kTBConfirmedDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DescID", "{0}")> _
        Public Property DescID() As Byte
            Get
                Return _descID
            End Get
            Set(ByVal value As Byte)
                _descID = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber() As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
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
        RelationInfo("Dealer", "ID", "IndentPartHeader", "DealerID")> _
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
        RelationInfo("TermOfPayment", "ID", "IndentPartHeader", "TermOfPaymentID")> _
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
        RelationInfo("TOPBlockStatus", "ID", "IndentPartHeader", "TOPBlockStatusID")> _
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

        <RelationInfo("IndentPartHeader", "ID", "IndentPartDetail", "IndentPartHeaderID")> _
        Public ReadOnly Property IndentPartDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._indentPartDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(IndentPartDetail), "IndentPartHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._indentPartDetails = DoLoadArray(GetType(IndentPartDetail).ToString, criterias)
                    End If

                    Return Me._indentPartDetails

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

        Public ReadOnly Property MaterialTypeDesc() As String
            Get
                Select Case _materialType
                    Case 1
                        Return "Part"
                    Case 2
                        Return "Tools"
                    Case 3
                        Return "Accessories"
                End Select
            End Get
        End Property

        Public ReadOnly Property StatusDealerDesc() As String
            Get
                Select Case _status
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Batal_Order
                        Return EnumIndentPartStatus.IndentPartStatusDealer.Batal_Order.ToString
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Proses
                        Return EnumIndentPartStatus.IndentPartStatusDealer.Proses.ToString
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Baru
                        Return EnumIndentPartStatus.IndentPartStatusDealer.Baru.ToString
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Batal
                        Return EnumIndentPartStatus.IndentPartStatusDealer.Batal.ToString
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi
                        Return EnumIndentPartStatus.IndentPartStatusDealer.Dealer_Konfirmasi.ToString
                        'Case EnumIndentPartStatus.IndentPartStatusDealer.Rilis
                        '    Return EnumIndentPartStatus.IndentPartStatusDealer.Rilis.ToString
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Kirim
                        Return EnumIndentPartStatus.IndentPartStatusDealer.Kirim.ToString
                    Case EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi
                        Return EnumIndentPartStatus.IndentPartStatusDealer.KTB_Konfirmasi.ToString
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Selesai
                        Return EnumIndentPartStatus.IndentPartStatusDealer.Selesai.ToString
                    Case EnumIndentPartStatus.IndentPartStatusDealer.Tolak
                        Return EnumIndentPartStatus.IndentPartStatusDealer.Tolak.ToString
                End Select
            End Get
        End Property

        Public ReadOnly Property StatusKTBDesc() As String
            Get
                Select Case _statusKTB
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Batal_Order
                        Return EnumIndentPartStatus.IndentPartStatusKTB.Batal_Order.ToString
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Proses
                        Return EnumIndentPartStatus.IndentPartStatusKTB.Proses.ToString
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Baru
                        Return EnumIndentPartStatus.IndentPartStatusKTB.Baru.ToString
                    Case EnumIndentPartStatus.IndentPartStatusKTB.BelumValidasi
                        Return EnumIndentPartStatus.IndentPartStatusKTB.BelumValidasi.ToString
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi
                        Return EnumIndentPartStatus.IndentPartStatusKTB.Dealer_Konfirmasi.ToString
                    Case EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi
                        Return EnumIndentPartStatus.IndentPartStatusKTB.KTB_Konfirmasi.ToString
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Rilis
                        Return EnumIndentPartStatus.IndentPartStatusKTB.Rilis.ToString
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Selesai
                        Return EnumIndentPartStatus.IndentPartStatusKTB.Selesai.ToString
                    Case EnumIndentPartStatus.IndentPartStatusKTB.Tolak
                        Return EnumIndentPartStatus.IndentPartStatusKTB.Tolak.ToString

                End Select
            End Get
        End Property

        Public ReadOnly Property TotalQuantity() As Integer
            Get
                'Todo Aggregate
                Dim total As Integer
                For Each item As IndentPartDetail In Me.IndentPartDetails
                    total = total + CInt(item.Qty)
                Next

                'If (Me.Status = enumStatusPO.Status.Baru OrElse Me.Status = enumStatusPO.Status.Batal OrElse Me.Status = enumStatusPO.Status.Ditolak OrElse Me.Status = enumStatusPO.Status.Konfirmasi) Then
                '    For Each item As PODetail In Me.PODetails
                '        total = total + (CType(item.ReqQty, Integer))
                '    Next
                'Else
                '    For Each item As PODetail In Me.PODetails
                '        total = total + (CType(item.AllocQty, Integer))
                '    Next
                'End If

                Return total
            End Get
        End Property

        Public ReadOnly Property StatusInProgres() As String
            Get
                Select Case _statusKTB
                    Case 0
                        Return "Belum Diproses"
                    Case 1, 2, 3, 4
                        Return "Dalam Proses"
                    Case 5
                        Return "Tolak"
                    Case 6
                        Return "Selesai"
                End Select
            End Get
        End Property
#End Region

#Region "Custom Properties"

        Public ReadOnly Property AmountBeforeTax() As Decimal
            Get
                'Todo Aggregate

                If Me.ID = 0 Then
                    Return 0
                End If

                Dim Total As Decimal = 0
                Dim TotalAfterDiscount As Decimal = 0

                For Each item As IndentPartDetail In Me.IndentPartDetails
                    TotalAfterDiscount = (item.Qty * item.Price) - ((item.EstimationEquipDetail.Discount / 100) * (item.Qty * item.Price))
                    Total += TotalAfterDiscount ' item.Qty * item.Price
                    'Total += item.Qty * item.Price
                Next

                Return Total

            End Get
        End Property

        Public ReadOnly Property AmountAfterTax() As Decimal
            Get
                'Todo Aggregate

                If Me.ID = 0 Then
                    Return 0
                End If

                Dim Total As Decimal = 0
                Dim TotalAfterDiscount As Decimal = 0
                For Each item As IndentPartDetail In Me.IndentPartDetails
                    TotalAfterDiscount = (item.Qty * item.Price) - ((item.EstimationEquipDetail.Discount / 100) * (item.Qty * item.Price))
                    Total += TotalAfterDiscount ' item.Qty * item.Price
                Next

                Return Total * 1.1

            End Get
        End Property

        'Public ReadOnly Property TotalAmount() As Decimal
        '    Get

        '        If Me.ID = 0 Then
        '            Return 0
        '        End If

        '        Dim Total As Integer = 0

        '        For Each item As IndentPartDetail In Me.IndentPartDetails
        '            Total += item.Qty * item.Price
        '        Next

        '        Return Total * 0.9

        '    End Get
        'End Property

        Public ReadOnly Property TotalQty() As Integer
            Get
                'Todo Aggregate
                If Me.ID = 0 Then
                    Return 0
                End If

                Dim Total As Integer = 0

                For Each item As IndentPartDetail In Me.IndentPartDetails
                    Total += item.Qty
                Next

                Return Total

            End Get
        End Property


        Public ReadOnly Property SisaQty() As Integer
            Get
                'Todo Aggregate
                If Me.ID = 0 Then
                    Return 0
                End If

                Dim Total As Integer = 0

                For Each item As IndentPartDetail In Me.IndentPartDetails
                    Total += item.SisaQty
                Next

                Return Total

            End Get
        End Property


        Public ReadOnly Property POQty() As Integer
            Get
                'Todo Aggregate
                If Me.ID = 0 Then
                    Return 0
                End If

                Dim Total As Integer = 0

                For Each item As IndentPartDetail In Me.IndentPartDetails
                    Total += item.POQty
                Next

                Return Total

            End Get
        End Property


#End Region

    End Class
End Namespace

