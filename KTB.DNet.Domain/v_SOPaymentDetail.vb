
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_SOPaymentDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 09/08/2016 - 16:42:09
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
    <Serializable(), TableInfo("v_SOPaymentDetail")> _
    Public Class v_SOPaymentDetail
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
        Private _sONumber As String = String.Empty
        Private _dealerID As Short
        Private _pONumber As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _totalVH As Decimal
        Private _totalPP As Decimal
        Private _totalIT As Decimal
        Private _regNumber As String = String.Empty
        Private _paymentPurposeID As Byte
        Private _paymentPurposeCode As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _planTransferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Short
        Private _amount As Decimal




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


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
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


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
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


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property


        <ColumnInfo("PaymentPurposeID", "{0}")> _
        Public Property PaymentPurposeID As Byte
            Get
                Return _paymentPurposeID
            End Get
            Set(ByVal value As Byte)
                _paymentPurposeID = value
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("PlanTransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PlanTransferDate As DateTime
            Get
                Return _planTransferDate
            End Get
            Set(ByVal value As DateTime)
                _planTransferDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
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

