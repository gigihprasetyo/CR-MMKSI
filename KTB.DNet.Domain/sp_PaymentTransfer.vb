
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_PaymentTransfer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/26/2016 - 10:15:35 AM
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
    <Serializable(), TableInfo("sp_PaymentTransfer")> _
    Public Class sp_PaymentTransfer
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Integer)
            _id = id
        End Sub

#End Region

#Region "Private Variables"

        Private _id As Integer
        Private _sONumber As String = String.Empty
        Private _dueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _totalAmount As Decimal
        Private _totalVH As Decimal
        Private _totalPP As Decimal
        Private _totalIT As Decimal
        Private _dealerID As Short
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _cityName As String = String.Empty
        Private _paymentPurposeCode As String = String.Empty
        Private _CreditAccount As String = String.Empty


#End Region

#Region "Public Properties"

        <ColumnInfo("id", "{0}")> _
        Public Property id As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property

        <ColumnInfo("PaymentPurposeCode", "'{0}'")> _
        Public Property PaymentPurposeCode As String
            Get
                Return _paymentPurposeCode
            End Get
            Set(ByVal value As String)
                _paymentPurposeCode = value
            End Set
        End Property

        <ColumnInfo("CreditAccount", "'{0}'")> _
        Public Property CreditAccount As String
            Get
                Return _CreditAccount
            End Get
            Set(ByVal value As String)
                _CreditAccount = value
            End Set
        End Property



        <ColumnInfo("DueDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DueDate As DateTime
            Get
                Return _dueDate
            End Get
            Set(ByVal value As DateTime)
                _dueDate = New DateTime(value.Year, value.Month, value.Day)
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

        <ColumnInfo("TotalVH", "{0}")> _
        Public Property TotalVH As Decimal
            Get
                Return _totalVH
            End Get
            Set(ByVal value As Decimal)
                _totalVH = value
            End Set
        End Property


        <ColumnInfo("TotalPP", "{0}")> _
        Public Property TotalPP As Decimal
            Get
                Return _totalPP
            End Get
            Set(ByVal value As Decimal)
                _totalPP = value
            End Set
        End Property


        <ColumnInfo("TotalIT", "{0}")> _
        Public Property TotalIT As Decimal
            Get
                Return _totalIT
            End Get
            Set(ByVal value As Decimal)
                _totalIT = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID As Short
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
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


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("CityName", "'{0}'")> _
        Public Property CityName As String
            Get
                Return _cityName
            End Get
            Set(ByVal value As String)
                _cityName = value
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

