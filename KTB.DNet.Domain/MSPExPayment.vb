
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExPayment Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2020 - 4:03:29 PM
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
    <Serializable(), TableInfo("MSPExPayment")> _
    Public Class MSPExPayment
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
        Private _regNumber As String = String.Empty
        Private _planTransferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _totalAmount As Decimal
        Private _actualTransferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _actualTotalAmount As Decimal
        Private _status As Short
        Private _isTransfertoSAP As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer
        Private _mSPExPaymentDetailList As ArrayList = New ArrayList()
        Private _isValidation As Boolean = False
        Private _clearingTotalAmount As Decimal
        Private _clearingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _tRNumber As String = String.Empty
        Private _bankReffNumber As String = String.Empty


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


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property


        <ColumnInfo("PlanTransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanTransferDate As DateTime
            Get
                Return _planTransferDate
            End Get
            Set(ByVal value As DateTime)
                _planTransferDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("TotalAmount", "{0}")> _
        Public Property TotalAmount As Decimal
            Get
                Return _totalAmount
            End Get
            Set(ByVal value As Decimal)
                _totalAmount = value
            End Set
        End Property


        <ColumnInfo("ActualTransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ActualTransferDate As DateTime
            Get
                Return _actualTransferDate
            End Get
            Set(ByVal value As DateTime)
                _actualTransferDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ActualTotalAmount", "{0}")> _
        Public Property ActualTotalAmount As Decimal
            Get
                Return _actualTotalAmount
            End Get
            Set(ByVal value As Decimal)
                _actualTotalAmount = value
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


        <ColumnInfo("IsTransfertoSAP", "{0}")> _
        Public Property IsTransfertoSAP As Short
            Get
                Return _isTransfertoSAP
            End Get
            Set(ByVal value As Short)
                _isTransfertoSAP = value
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

        Private _dealerCode As String
        <ColumnInfo("LastUpdateBy", "'{0}'")>
        Public Property DealerCode As String

            Set(value As String)
                _dealerCode = value
            End Set
            Get
                Return _dealerCode
            End Get
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "MSPExPayment", "DealerID")> _
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


        <RelationInfo("MSPExPayment", "ID", "MSPExPaymentDetail", "MSPExPaymentID")> _
        Public ReadOnly Property MSPExPaymentDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._mSPExPaymentDetailList.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MSPExPaymentDetail), "MSPExPayment.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MSPExPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._mSPExPaymentDetailList = DoLoadArray(GetType(MSPExPaymentDetail).ToString, criterias)
                    End If

                    Return Me._mSPExPaymentDetailList

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
        End Property


        <ColumnInfo("ClearingTotalAmount", "{0}")> _
        Public Property ClearingTotalAmount As Decimal
            Get
                Return _clearingTotalAmount
            End Get
            Set(ByVal value As Decimal)
                _clearingTotalAmount = value
            End Set
        End Property


        <ColumnInfo("ClearingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ClearingDate As DateTime
            Get
                Return _clearingDate
            End Get
            Set(ByVal value As DateTime)
                _clearingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("TRNumber", "'{0}'")> _
        Public Property TRNumber As String
            Get
                Return _tRNumber
            End Get
            Set(ByVal value As String)
                _tRNumber = value
            End Set
        End Property


        <ColumnInfo("BankReffNumber", "'{0}'")> _
        Public Property BankReffNumber As String
            Get
                Return _bankReffNumber
            End Get
            Set(ByVal value As String)
                _bankReffNumber = value
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
        Public Property IsValidation As Boolean
            Get
                Return _isValidation
            End Get
            Set(ByVal value As Boolean)
                _isValidation = value
            End Set
        End Property
#End Region

    End Class
End Namespace

