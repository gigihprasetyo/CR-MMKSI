
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPPenalty Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 05/13/2019 - 1:55:54 PM
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
    <Serializable(), TableInfo("TOPSPPenalty")> _
    Public Class TOPSPPenalty
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
        Private _amount As Decimal
        Private _lastDownloadby As String = String.Empty
        Private _downloadedDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _debitMemoNumber As String = String.Empty
        Private _debitMemoDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _accountingNumber As String = String.Empty
        Private _clearingNumber As String = String.Empty
        Private _paymentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _amountPayment As Decimal
        Private _debitMemoPath As String = String.Empty
        Private _noRegPengembalian As String = String.Empty
        Private _noBuktiPotong As String = String.Empty
        Private _buktiPotongDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _amountPPh As Decimal
        Private _jVNumber As String = String.Empty
        Private _uploadFilePath As String = String.Empty
        Private _statusPenalty As Short
        Private _statusPengembalian As Short
        Private _attachmentData As System.Web.HttpPostedFile
        Private _message As String = String.Empty

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _tOPSPTransferPayment As TOPSPTransferPayment
        Private _TOPSPPenaltyDetails As System.Collections.ArrayList = New System.Collections.ArrayList()

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


        <ColumnInfo("Amount", "'{0}'")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("LastDownloadby", "'{0}'")> _
        Public Property LastDownloadby As String
            Get
                Return _lastDownloadby
            End Get
            Set(ByVal value As String)
                _lastDownloadby = value
            End Set
        End Property


        <ColumnInfo("DownloadedDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DownloadedDate As DateTime
            Get
                Return _downloadedDate
            End Get
            Set(ByVal value As DateTime)
                _downloadedDate = value
            End Set
        End Property


        <ColumnInfo("DebitMemoNumber", "'{0}'")> _
        Public Property DebitMemoNumber As String
            Get
                Return _debitMemoNumber
            End Get
            Set(ByVal value As String)
                _debitMemoNumber = value
            End Set
        End Property


        <ColumnInfo("DebitMemoDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DebitMemoDate As DateTime
            Get
                Return _debitMemoDate
            End Get
            Set(ByVal value As DateTime)
                _debitMemoDate = value
            End Set
        End Property


        <ColumnInfo("AccountingNumber", "'{0}'")> _
        Public Property AccountingNumber As String
            Get
                Return _accountingNumber
            End Get
            Set(ByVal value As String)
                _accountingNumber = value
            End Set
        End Property


        <ColumnInfo("ClearingNumber", "'{0}'")> _
        Public Property ClearingNumber As String
            Get
                Return _clearingNumber
            End Get
            Set(ByVal value As String)
                _clearingNumber = value
            End Set
        End Property


        <ColumnInfo("PaymentDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PaymentDate As DateTime
            Get
                Return _paymentDate
            End Get
            Set(ByVal value As DateTime)
                _paymentDate = value
            End Set
        End Property


        <ColumnInfo("AmountPayment", "'{0}'")> _
        Public Property AmountPayment As Decimal
            Get
                Return _amountPayment
            End Get
            Set(ByVal value As Decimal)
                _amountPayment = value
            End Set
        End Property


        <ColumnInfo("DebitMemoPath", "'{0}'")> _
        Public Property DebitMemoPath As String
            Get
                Return _debitMemoPath
            End Get
            Set(ByVal value As String)
                _debitMemoPath = value
            End Set
        End Property


        <ColumnInfo("NoRegPengembalian", "'{0}'")> _
        Public Property NoRegPengembalian As String
            Get
                Return _noRegPengembalian
            End Get
            Set(ByVal value As String)
                _noRegPengembalian = value
            End Set
        End Property


        <ColumnInfo("NoBuktiPotong", "'{0}'")> _
        Public Property NoBuktiPotong As String
            Get
                Return _noBuktiPotong
            End Get
            Set(ByVal value As String)
                _noBuktiPotong = value
            End Set
        End Property


        <ColumnInfo("BuktiPotongDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property BuktiPotongDate As DateTime
            Get
                Return _buktiPotongDate
            End Get
            Set(ByVal value As DateTime)
                _buktiPotongDate = value
            End Set
        End Property


        <ColumnInfo("AmountPPh", "'{0}'")> _
        Public Property AmountPPh As Decimal
            Get
                Return _amountPPh
            End Get
            Set(ByVal value As Decimal)
                _amountPPh = value
            End Set
        End Property


        <ColumnInfo("JVNumber", "'{0}'")> _
        Public Property JVNumber As String
            Get
                Return _jVNumber
            End Get
            Set(ByVal value As String)
                _jVNumber = value
            End Set
        End Property


        <ColumnInfo("UploadFilePath", "'{0}'")> _
        Public Property UploadFilePath As String
            Get
                Return _uploadFilePath
            End Get
            Set(ByVal value As String)
                _uploadFilePath = value
            End Set
        End Property


        <ColumnInfo("StatusPenalty", "{0}")> _
        Public Property StatusPenalty As Short
            Get
                Return _statusPenalty
            End Get
            Set(ByVal value As Short)
                _statusPenalty = value
            End Set
        End Property


        <ColumnInfo("StatusPengembalian", "{0}")> _
        Public Property StatusPengembalian As Short
            Get
                Return _statusPengembalian
            End Get
            Set(ByVal value As Short)
                _statusPengembalian = value
            End Set
        End Property


        <ColumnInfo("Message", "'{0}'")> _
        Public Property Message As String
            Get
                Return _message
            End Get
            Set(ByVal value As String)
                _message = value
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


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "TOPSPPenalty", "DealerID")> _
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


        <ColumnInfo("TOPSPTransferPaymentID", "{0}"), _
        RelationInfo("TOPSPTransferPayment", "ID", "TOPSPPenalty", "TOPSPTransferPaymentID")> _
        Public Property TOPSPTransferPayment As TOPSPTransferPayment
            Get
                Try
                    If Not IsNothing(Me._tOPSPTransferPayment) AndAlso (Not Me._tOPSPTransferPayment.IsLoaded) Then

                        Me._tOPSPTransferPayment = CType(DoLoad(GetType(TOPSPTransferPayment).ToString(), _tOPSPTransferPayment.ID), TOPSPTransferPayment)
                        Me._tOPSPTransferPayment.MarkLoaded()
                    End If
                    Return Me._tOPSPTransferPayment
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As TOPSPTransferPayment)
                Me._tOPSPTransferPayment = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._tOPSPTransferPayment.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("TOPSPPenalty", "ID", "TOPSPPenaltyDetail", "TOPSPPenaltyID")> _
        Public ReadOnly Property TOPSPPenaltyDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._TOPSPPenaltyDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TOPSPPenaltyDetail), "TOPSPPenalty", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TOPSPPenaltyDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._TOPSPPenaltyDetails = DoLoadArray(GetType(TOPSPPenaltyDetail).ToString, criterias)
                    End If

                    Return Me._TOPSPPenaltyDetails

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
        Public Property AttachmentData() As System.Web.HttpPostedFile
            Get
                Return _attachmentData
            End Get

            Set(ByVal value As System.Web.HttpPostedFile)
                _attachmentData = value
            End Set
        End Property
#End Region

    End Class
End Namespace

