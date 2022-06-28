
#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    '<Serializable(), TableInfo("TOPSPTransferPaymentDetail")> _
    Public Class V_TOPSPTransferPaymentDetail

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
        Private _dueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _actualTransferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _billingNumber As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _actualTransferAmount As Double
        Private _penaltyDays As Integer
        Private _amountPenalty As Decimal
        Private _paymentType As Integer
#End Region

#Region "Public Properties"

        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        Public Property BillingNumber As String
            Get
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
            End Set
        End Property


        Public Property DueDate As DateTime
            Get
                Return _dueDate
            End Get
            Set(ByVal value As DateTime)
                _dueDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        Public Property ActualTransferDate As DateTime
            Get
                Return _actualTransferDate
            End Get
            Set(ByVal value As DateTime)
                _actualTransferDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        Public Property ActualTransferAmount As Decimal
            Get
                Return _actualTransferAmount
            End Get
            Set(ByVal value As Decimal)
                _actualTransferAmount = value
            End Set
        End Property


        Public Property PenaltyDays As Integer
            Get
                Return _penaltyDays
            End Get
            Set(ByVal value As Integer)
                _penaltyDays = value
            End Set
        End Property


        Public Property AmountPenalty As Decimal
            Get
                Return _amountPenalty
            End Get
            Set(ByVal value As Decimal)
                _amountPenalty = value
            End Set
        End Property

        Public Property PaymentType As Integer
            Get
                Return _paymentType
            End Get
            Set(ByVal value As Integer)
                _paymentType = value
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

#End Region

    End Class
End Namespace

