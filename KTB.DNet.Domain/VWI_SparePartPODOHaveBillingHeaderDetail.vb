#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("VWI_PODOHaveBillingHeaderDetail")> _
    Public Class VWI_SparePartPODOHaveBillingHeaderDetail
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
        Private _sparePartDOID As Integer
        Private _dONumber As String = String.Empty
        Private _dealerID As Short
        Private _dealerCode As String = String.Empty
        Private _doDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _billingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _billingNumber As String = String.Empty
        Private _expeditionNo As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _termOfPaymentValue As Integer
        Private _termOfPaymentCode As String = String.Empty
        Private _termOfPaymentDesc As String = String.Empty


        Private _sONumber As String = String.Empty
        Private _partNumber As String = String.Empty
        Private _partName As String = String.Empty
        Private _qty As Integer
        Private _tax As Decimal
        Private _discount As Decimal
        Private _retailPrice As Decimal

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

        <ColumnInfo("SparePartDOID", "{0}")> _
        Public Property SparePartDOID As Integer
            Get
                Return _sparePartDOID
            End Get
            Set(ByVal value As Integer)
                _sparePartDOID = value
            End Set
        End Property

        <ColumnInfo("DONumber", "'{0}'")> _
        Public Property DONumber As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
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

        <ColumnInfo("DoDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DoDate As DateTime
            Get
                Return _doDate
            End Get
            Set(ByVal value As DateTime)
                _doDate = New DateTime(value.Year, value.Month, value.Day)
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

        <ColumnInfo("BillingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BillingDate As DateTime
            Get
                Return _billingDate
            End Get
            Set(ByVal value As DateTime)
                _billingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("BillingNumber", "'{0}'")> _
        Public Property BillingNumber As String
            Get
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
            End Set
        End Property

        <ColumnInfo("ExpeditionNo", "'{0}'")> _
        Public Property ExpeditionNo As String
            Get
                Return _expeditionNo
            End Get
            Set(ByVal value As String)
                _expeditionNo = value
            End Set
        End Property

        <ColumnInfo("TermOfPaymentValue", "{0}")> _
        Public Property TermOfPaymentValue As Integer
            Get
                Return _termOfPaymentValue
            End Get
            Set(ByVal value As Integer)
                _termOfPaymentValue = value
            End Set
        End Property

        <ColumnInfo("TermOfPaymentCode", "'{0}'")> _
        Public Property TermOfPaymentCode As String
            Get
                Return _termOfPaymentCode
            End Get
            Set(ByVal value As String)
                _termOfPaymentCode = value
            End Set
        End Property

        <ColumnInfo("TermOfPaymentDesc", "'{0}'")> _
        Public Property TermOfPaymentDesc As String
            Get
                Return _termOfPaymentDesc
            End Get
            Set(ByVal value As String)
                _termOfPaymentDesc = value
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


        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property

        <ColumnInfo("PartNumber", "'{0}'")> _
        Public Property PartNumber As String
            Get
                Return _partNumber
            End Get
            Set(ByVal value As String)
                _partNumber = value
            End Set
        End Property

        <ColumnInfo("PartName", "'{0}'")> _
        Public Property PartName As String
            Get
                Return _partName
            End Get
            Set(ByVal value As String)
                _partName = value
            End Set
        End Property

        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
            End Set
        End Property

        <ColumnInfo("Tax", "{0}")> _
        Public Property Tax As Decimal
            Get
                Return _tax
            End Get
            Set(ByVal value As Decimal)
                _tax = value
            End Set
        End Property

        <ColumnInfo("Discount", "{0}")> _
        Public Property Discount As Decimal
            Get
                Return _discount
            End Get
            Set(ByVal value As Decimal)
                _discount = value
            End Set
        End Property

        <ColumnInfo("RetailPrice", "{0}")> _
        Public Property RetailPrice As Decimal
            Get
                Return _retailPrice
            End Get
            Set(ByVal value As Decimal)
                _retailPrice = value
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

    End Class
End Namespace

