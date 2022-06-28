#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrPaymentHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2019 - 4:07:32 PM
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
Imports System.Collections.Generic
Imports System.Linq
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("TrBillingHeader")> _
    Public Class TrBillingHeader
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
        Private _requestID As String = String.Empty
        Private _fiscalYear As String = String.Empty
        Private _dealer As Dealer
        Private _paymentType As Short
        Private _pathDebitNote As String
        Private _pathFaktur As String
        Private _pathSuratKuasa As String
        Private _dealerCode As String
        Private _JVNumber As String = String.Empty
        Private _debitNoteNumber As String
        Private _totalPrice As Decimal
        Private _totalVoucher As Decimal
        Private _ppn As Decimal
        Private _total As Decimal
        Private _posteddate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _listTrBillingDetail As New List(Of TrBillingDetail)

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


        <ColumnInfo("RequestID", "'{0}'")> _
        Public Property RequestID As String
            Get
                Return _requestID
            End Get
            Set(ByVal value As String)
                _requestID = value
            End Set
        End Property


        <ColumnInfo("FiscalYear", "'{0}'")> _
        Public Property FiscalYear As String
            Get
                Return _fiscalYear
            End Get
            Set(ByVal value As String)
                _fiscalYear = value
            End Set
        End Property

        <ColumnInfo("PathDebitNote", "'{0}'")> _
        Public Property PathDebitNote As String
            Get
                Return _pathDebitNote
            End Get
            Set(ByVal value As String)
                _pathDebitNote = value
            End Set
        End Property

        <ColumnInfo("DebitNoteNumber", "'{0}'")> _
        Public Property DebitNoteNumber As String
            Get
                Return _debitNoteNumber
            End Get
            Set(ByVal value As String)
                _debitNoteNumber = value
            End Set
        End Property

        <ColumnInfo("PathFaktur", "'{0}'")> _
        Public Property PathFaktur As String
            Get
                Return _pathFaktur
            End Get
            Set(ByVal value As String)
                _pathFaktur = value
            End Set
        End Property

        <ColumnInfo("PathSuratKuasa", "'{0}'")> _
        Public Property PathSuratKuasa As String
            Get
                Return _pathSuratKuasa
            End Get
            Set(ByVal value As String)
                _pathSuratKuasa = value
            End Set
        End Property

        <ColumnInfo("JVNumber", "'{0}'")> _
        Public Property JVNumber As String
            Get
                Return _JVNumber
            End Get
            Set(ByVal value As String)
                _JVNumber = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}")> _
        <RelationInfo("Dealer", "ID", "TrBillingHeader", "DealerID")> _
        Public Property Dealer As Dealer
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

        <ColumnInfo("PaymentType", "{0}")> _
        Public Property PaymentType As Short
            Get
                Return _paymentType
            End Get
            Set(ByVal value As Short)
                _paymentType = value
            End Set
        End Property

        <ColumnInfo("TotalPrice", "{0}")> _
        Public Property TotalPrice As Decimal
            Get
                Return _totalPrice
            End Get
            Set(ByVal value As Decimal)
                _totalPrice = value
            End Set
        End Property


        <ColumnInfo("TotalVoucher", "{0}")> _
        Public Property TotalVoucher As Decimal
            Get
                Return _totalVoucher
            End Get
            Set(ByVal value As Decimal)
                _totalVoucher = value
            End Set
        End Property


        <ColumnInfo("PPN", "{0}")> _
        Public Property PPN As Decimal
            Get
                Return _ppn
            End Get
            Set(ByVal value As Decimal)
                _ppn = value
            End Set
        End Property


        <ColumnInfo("Total", "{0}")> _
        Public Property Total As Decimal
            Get
                Return _total
            End Get
            Set(ByVal value As Decimal)
                _total = value
            End Set
        End Property

        <ColumnInfo("PostedDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PostedDate As DateTime
            Get
                Return _posteddate
            End Get
            Set(ByVal value As DateTime)
                _posteddate = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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

        <RelationInfo("TrBillingHeader", "ID", "TrBillingDetail", "TrBillingHeaderID")> _
        Public ReadOnly Property ListTrBillingDetail() As List(Of TrBillingDetail)
            Get
                Try
                    Dim arrTrBilling As New ArrayList()
                    If (Me._listTrBillingDetail.Count < 1) Then
                        Dim _criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        _criteria.opAnd(New Criteria(GetType(TrBillingDetail), "TrBillingHeader.ID", MatchType.Exact, Me.ID))

                        arrTrBilling = DoLoadArray(GetType(TrBillingDetail).ToString, _criteria)
                        If arrTrBilling.Count > 0 Then
                            Me._listTrBillingDetail = arrTrBilling.Cast(Of TrBillingDetail).ToList()
                        End If

                    End If

                    Return Me._listTrBillingDetail

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

#End Region

    End Class
End Namespace
