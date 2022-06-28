
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_SparePartPayment Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/10/2018 - 14:13:52
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
    <Serializable(), TableInfo("VWI_SparePartPayment")> _
    Public Class VWI_SparePartPayment
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As String)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As String = String.Empty
        Private _referenceNo As String = String.Empty
        Private _invoiceNo As String = String.Empty
        Private _postingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerID As Short
        Private _dealerCode As String = String.Empty
        Private _sONumber As String = String.Empty
        Private _orderType As String = String.Empty
        Private _dMSPRNo As String = String.Empty
        Private _amount As Decimal
        Private _billingAmount As Decimal
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isTOP As Short
        Private _isPenalty As Short



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "'{0}'")> _
        Public Property ID As String
            Get
                Return _iD
            End Get
            Set(ByVal value As String)
                _iD = value
            End Set
        End Property


        <ColumnInfo("ReferenceNo", "'{0}'")> _
        Public Property ReferenceNo As String
            Get
                Return _referenceNo
            End Get
            Set(ByVal value As String)
                _referenceNo = value
            End Set
        End Property


        <ColumnInfo("InvoiceNo", "'{0}'")> _
        Public Property InvoiceNo As String
            Get
                Return _invoiceNo
            End Get
            Set(ByVal value As String)
                _invoiceNo = value
            End Set
        End Property


        <ColumnInfo("PostingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PostingDate As DateTime
            Get
                Return _postingDate
            End Get
            Set(ByVal value As DateTime)
                _postingDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("SONumber", "'{0}'")>
        Public Property SONumber As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property

        <ColumnInfo("OrderType", "'{0}'")>
        Public Property OrderType As String
            Get
                Return _orderType
            End Get
            Set(ByVal value As String)
                _orderType = value
            End Set
        End Property

        <ColumnInfo("DMSPRNo", "'{0}'")>
        Public Property DMSPRNo As String
            Get
                Return _dMSPRNo
            End Get
            Set(ByVal value As String)
                _dMSPRNo = value
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


        <ColumnInfo("BillingAmount", "{0}")> _
        Public Property BillingAmount As Decimal
            Get
                Return _billingAmount
            End Get
            Set(ByVal value As Decimal)
                _billingAmount = value
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

        <ColumnInfo("IsTOP", "{0}")>
        Public Property IsTOP() As Short
            Get
                Return _isTOP
            End Get
            Set(ByVal value As Short)
                _isTOP = value
            End Set
        End Property

        <ColumnInfo("IsPenalty", "{0}")>
        Public Property IsPenalty() As Short
            Get
                Return _isPenalty
            End Get
            Set(ByVal value As Short)
                _isPenalty = value
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

