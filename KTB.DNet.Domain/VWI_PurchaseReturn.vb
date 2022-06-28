#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("VWI_PurchaseReturn")>
    Public Class VWI_PurchaseReturn
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
        Private _dealerCode As String = String.Empty
        Private _claimNo As String = String.Empty
        Private _claimDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _doNumberRef As String = String.Empty
        Private _billingNumber As String = String.Empty
        Private _sONumber As String = String.Empty
        Private _dONumber As String = String.Empty
        Private _sORetur As String = String.Empty
        Private _sOReturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturRetur As String = String.Empty
        Private _partNumber As String = String.Empty
        Private _partName As String = String.Empty
        Private _qty As Integer
        Private _retailPrice As Decimal
        Private _totalPrice As Decimal
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")>
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")>
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("ClaimNo", "'{0}'")>
        Public Property ClaimNo As String
            Get
                Return _claimNo
            End Get
            Set(ByVal value As String)
                _claimNo = value
            End Set
        End Property

        <ColumnInfo("ClaimDate", "'{0:yyyy/MM/dd}'")>
        Public Property ClaimDate As DateTime
            Get
                Return _claimDate
            End Get
            Set(ByVal value As DateTime)
                _claimDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("DONumberRef", "'{0}'")>
        Public Property DONumberRef As String
            Get
                Return _doNumberRef
            End Get
            Set(ByVal value As String)
                _doNumberRef = value
            End Set
        End Property

        <ColumnInfo("BillingNumber", "'{0}'")>
        Public Property BillingNumber As String
            Get
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
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

        <ColumnInfo("DONumber", "'{0}'")>
        Public Property DONumber As String
            Get
                Return _dONumber
            End Get
            Set(ByVal value As String)
                _dONumber = value
            End Set
        End Property

        <ColumnInfo("SORetur", "'{0}'")>
        Public Property SORetur As String
            Get
                Return _sORetur
            End Get
            Set(ByVal value As String)
                _sORetur = value
            End Set
        End Property

        <ColumnInfo("SOReturDate", "'{0:yyyy/MM/dd}'")>
        Public Property SOReturDate As DateTime
            Get
                Return _sOReturDate
            End Get
            Set(ByVal value As DateTime)
                _sOReturDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("FakturRetur", "'{0}'")>
        Public Property FakturRetur As String
            Get
                Return _fakturRetur
            End Get
            Set(ByVal value As String)
                _fakturRetur = value
            End Set
        End Property

        <ColumnInfo("PartNumber", "'{0}'")>
        Public Property PartNumber As String
            Get
                Return _partNumber
            End Get
            Set(ByVal value As String)
                _partNumber = value
            End Set
        End Property

        <ColumnInfo("PartName", "'{0}'")>
        Public Property PartName As String
            Get
                Return _partName
            End Get
            Set(ByVal value As String)
                _partName = value
            End Set
        End Property

        <ColumnInfo("Qty", "{0}")>
        Public Property Qty As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
            End Set
        End Property

        <ColumnInfo("RetailPrice", "{0}")>
        Public Property NetRetailPricePrice As Decimal
            Get
                Return _retailPrice
            End Get
            Set(ByVal value As Decimal)
                _retailPrice = value
            End Set
        End Property

        <ColumnInfo("TotalPrice", "{0}")>
        Public Property TotalPrice As Decimal
            Get
                Return _totalPrice
            End Get
            Set(ByVal value As Decimal)
                _totalPrice = value
            End Set
        End Property

        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd}'")>
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

#End Region

    End Class
End Namespace
