
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPTransferPayment Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/5/2018 - 4:48:56 PM
'//
'// ===========================================================================	
#end region

#region ".NET Base Class Namespace Imports"
imports System
imports System.Collections
#end region

#region "Custom Namespace Imports"
imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#end region

namespace KTB.DNet.Domain
	<Serializable(), TableInfo("MSPTransferPayment")> _
	public class MSPTransferPayment
		inherits DomainObject
		
		#region "Constructors/Destructors/Finalizers"
		
		Public Sub New()
        End Sub
        
		public sub new(byval ID as integer )
			_iD = ID
		end sub
		
		#end region
		
		#region "Private Variables"
		
		private _iD as integer 			
		private _paymentPurpose as short 		
		private _planTransferDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _regNumber as string = String.Empty 		
		private _isNotOnTime as short 		
		private _totalAmount as decimal 		
		private _totalActualAmount as decimal 		
		private _actualTransferDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _transferAmount as decimal 		
		private _transferDate as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _status as short 		
		private _rowStatus as short 		
		private _createdBy as string = String.Empty 		
		private _createdTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		private _lastUpdateBy as string = String.Empty 		
		private _lastUpdateTime as DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) 		
		
        Private _mspTransferPaymentDetailList As New ArrayList
        Private _objDealer As Dealer
        Private _isValidation As Boolean = False
		
		#end region
		
#Region "Public Properties"

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "MSPTransferPayment", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._objDealer) AndAlso (Not Me._objDealer.IsLoaded) Then
                        Me._objDealer = CType(DoLoad(GetType(Dealer).ToString(), _objDealer.ID), Dealer)
                        Me._objDealer.MarkLoaded()
                    End If

                    Return Me._objDealer
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._objDealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._objDealer.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("MSPTransferPayment", "ID", "MSPTransferPaymentDetail", "MSPTransferPaymentID")> _
        Public ReadOnly Property MSPTransferPaymentDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._mspTransferPaymentDetailList.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(MSPTransferPaymentDetail), "MSPTransferPayment", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(MSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._mspTransferPaymentDetailList = DoLoadArray(GetType(MSPTransferPaymentDetail).ToString, criterias)
                    End If

                    Return Me._mspTransferPaymentDetailList

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("PaymentPurpose", "{0}")> _
        Public Property PaymentPurpose As Short
            Get
                Return _paymentPurpose
            End Get
            Set(ByVal value As Short)
                _paymentPurpose = value
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


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property


        <ColumnInfo("IsNotOnTime", "{0}")> _
        Public Property IsNotOnTime As Short
            Get
                Return _isNotOnTime
            End Get
            Set(ByVal value As Short)
                _isNotOnTime = value
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


        <ColumnInfo("TotalActualAmount", "{0}")> _
        Public Property TotalActualAmount As Decimal
            Get
                Return _totalActualAmount
            End Get
            Set(ByVal value As Decimal)
                _totalActualAmount = value
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


        <ColumnInfo("TransferAmount", "{0}")> _
        Public Property TransferAmount As Decimal
            Get
                Return _transferAmount
            End Get
            Set(ByVal value As Decimal)
                _transferAmount = value
            End Set
        End Property


        <ColumnInfo("TransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransferDate As DateTime
            Get
                Return _transferDate
            End Get
            Set(ByVal value As DateTime)
                _transferDate = New DateTime(value.Year, value.Month, value.Day)
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
		
	end class
end namespace

