
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODealer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 08/03/2018 - 22:03:46
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
    <Serializable(), TableInfo("VWI_PODealer")> _
    Public Class VWI_PODealer
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal POHeaderId As Integer)
            _pOHeaderId = POHeaderId
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _pOHeaderId As Integer
        Private _dealerId As Short
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _destinationDealerCode As String = String.Empty
        Private _pONumber As String = String.Empty
        Private _pOType As String = String.Empty
        Private _numOfInstallment As Integer
        Private _allocQty As Integer
        Private _price As Decimal
        Private _discount As Decimal
        Private _interest As Decimal
        Private _contractNumber As String = String.Empty
        Private _pKNumber As String = String.Empty
        Private _dealerPKNumber As String = String.Empty
        Private _dealerPONumber As String = String.Empty
        Private _projectName As String = String.Empty
        Private _salesOrderId As Integer
        Private _sONumber As String = String.Empty
        Private _sODate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _paymentRef As String = String.Empty
        Private _sOType As String = String.Empty
        Private _termOfPaymentCode As String = String.Empty
        Private _tOPDescription As String = String.Empty
        Private _dueDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _vehicleColorCode As String = String.Empty
        Private _vehicleTypeCode As String = String.Empty
        Private _materialNumber As String = String.Empty
        Private _materialDescription As String = String.Empty
        Private _basePrice As Decimal
        Private _optionPrice As Decimal
        Private _discountBeforeTax As Decimal
        Private _netPrice As Decimal
        Private _totalHarga As Decimal
        Private _pPN As Decimal
        Private _totalHargaPPN As Decimal
        Private _totalHargaPP As Decimal
        Private _totalHargaLC As Decimal
        Private _totalDeposit As Decimal
        Private _totalInterest As Decimal
        Private _sPLNumber As String = String.Empty
        Private _eTDDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _effectiveDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _releaseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


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


        <ColumnInfo("POHeaderId", "{0}")> _
        Public Property POHeaderId As Integer
            Get
                Return _pOHeaderId
            End Get
            Set(ByVal value As Integer)
                _pOHeaderId = value
            End Set
        End Property


        <ColumnInfo("DealerId", "{0}")> _
        Public Property DealerId As Short
            Get
                Return _dealerId
            End Get
            Set(ByVal value As Short)
                _dealerId = value
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


        <ColumnInfo("DealerName", "'{0}'")>
        Public Property DealerName As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property

        <ColumnInfo("DestinationDealerCode", "'{0}'")>
        Public Property DestinationDealerCode As String
            Get
                Return _destinationDealerCode
            End Get
            Set(ByVal value As String)
                _destinationDealerCode = value
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

        <ColumnInfo("POType", "'{0}'")> _
        Public Property POType As String
            Get
                Return _pOType
            End Get
            Set(ByVal value As String)
                _pOType = value
            End Set
        End Property
        <ColumnInfo("NumOfInstallment", "'{0}'")> _
        Public Property NumOfInstallment As Integer
            Get
                Return _numOfInstallment
            End Get
            Set(ByVal value As Integer)
                _numOfInstallment = value
            End Set
        End Property
        <ColumnInfo("AllocQty", "{0}")> _
        Public Property AllocQty As Integer
            Get
                Return _allocQty
            End Get
            Set(ByVal value As Integer)
                _allocQty = value
            End Set
        End Property


        <ColumnInfo("Price", "{0}")> _
        Public Property Price As Decimal
            Get
                Return _price
            End Get
            Set(ByVal value As Decimal)
                _price = value
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


        <ColumnInfo("Interest", "{0}")> _
        Public Property Interest As Decimal
            Get
                Return _interest
            End Get
            Set(ByVal value As Decimal)
                _interest = value
            End Set
        End Property


        <ColumnInfo("ContractNumber", "'{0}'")> _
        Public Property ContractNumber As String
            Get
                Return _contractNumber
            End Get
            Set(ByVal value As String)
                _contractNumber = value
            End Set
        End Property


        <ColumnInfo("PKNumber", "'{0}'")> _
        Public Property PKNumber As String
            Get
                Return _pKNumber
            End Get
            Set(ByVal value As String)
                _pKNumber = value
            End Set
        End Property


        <ColumnInfo("DealerPKNumber", "'{0}'")> _
        Public Property DealerPKNumber As String
            Get
                Return _dealerPKNumber
            End Get
            Set(ByVal value As String)
                _dealerPKNumber = value
            End Set
        End Property

        <ColumnInfo("DealerPONumber", "'{0}'")> _
        Public Property DealerPONumber As String
            Get
                Return _dealerPONumber
            End Get
            Set(ByVal value As String)
                _dealerPONumber = value
            End Set
        End Property


        <ColumnInfo("ProjectName", "'{0}'")> _
        Public Property ProjectName As String
            Get
                Return _projectName
            End Get
            Set(ByVal value As String)
                _projectName = value
            End Set
        End Property


        <ColumnInfo("SalesOrderId", "{0}")> _
        Public Property SalesOrderId As Integer
            Get
                Return _salesOrderId
            End Get
            Set(ByVal value As Integer)
                _salesOrderId = value
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


        <ColumnInfo("SODate", "'{0:yyyy/MM/dd}'")> _
        Public Property SODate As DateTime
            Get
                Return _sODate
            End Get
            Set(ByVal value As DateTime)
                _sODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PaymentRef", "'{0}'")> _
        Public Property PaymentRef As String
            Get
                Return _paymentRef
            End Get
            Set(ByVal value As String)
                _paymentRef = value
            End Set
        End Property


        <ColumnInfo("SOType", "'{0}'")> _
        Public Property SOType As String
            Get
                Return _sOType
            End Get
            Set(ByVal value As String)
                _sOType = value
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


        <ColumnInfo("TOPDescription", "'{0}'")> _
        Public Property TOPDescription As String
            Get
                Return _tOPDescription
            End Get
            Set(ByVal value As String)
                _tOPDescription = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property

        <ColumnInfo("VehicleColorCode", "'{0}'")> _
        Public Property VehicleColorCode As String
            Get
                Return _vehicleColorCode
            End Get
            Set(ByVal value As String)
                _vehicleColorCode = value
            End Set
        End Property
        <ColumnInfo("VehicleTypeCode", "'{0}'")> _
        Public Property VehicleTypeCode As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property
        <ColumnInfo("MaterialNumber", "'{0}'")> _
        Public Property MaterialNumber As String
            Get
                Return _materialNumber
            End Get
            Set(ByVal value As String)
                _materialNumber = value
            End Set
        End Property

        <ColumnInfo("MaterialDescription", "'{0}'")> _
        Public Property MaterialDescription As String
            Get
                Return _materialDescription
            End Get
            Set(ByVal value As String)
                _materialDescription = value
            End Set
        End Property


        <ColumnInfo("BasePrice", "{0}")> _
        Public Property BasePrice As Decimal
            Get
                Return _basePrice
            End Get
            Set(ByVal value As Decimal)
                _basePrice = value
            End Set
        End Property


        <ColumnInfo("OptionPrice", "{0}")> _
        Public Property OptionPrice As Decimal
            Get
                Return _optionPrice
            End Get
            Set(ByVal value As Decimal)
                _optionPrice = value
            End Set
        End Property


        <ColumnInfo("DiscountBeforeTax", "#,##0")> _
        Public Property DiscountBeforeTax As Decimal
            Get
                Return _discountBeforeTax
            End Get
            Set(ByVal value As Decimal)
                _discountBeforeTax = value
            End Set
        End Property


        <ColumnInfo("NetPrice", "#,##0")> _
        Public Property NetPrice As Decimal
            Get
                Return _netPrice
            End Get
            Set(ByVal value As Decimal)
                _netPrice = value
            End Set
        End Property


        <ColumnInfo("TotalHarga", "#,##0")> _
        Public Property TotalHarga As Decimal
            Get
                Return _totalHarga
            End Get
            Set(ByVal value As Decimal)
                _totalHarga = value
            End Set
        End Property


        <ColumnInfo("PPN", "#,##0")> _
        Public Property PPN As Decimal
            Get
                Return _pPN
            End Get
            Set(ByVal value As Decimal)
                _pPN = value
            End Set
        End Property


        <ColumnInfo("TotalHargaPPN", "#,##0")> _
        Public Property TotalHargaPPN As Decimal
            Get
                Return _totalHargaPPN
            End Get
            Set(ByVal value As Decimal)
                _totalHargaPPN = value
            End Set
        End Property


        <ColumnInfo("TotalHargaPP", "#,##0")> _
        Public Property TotalHargaPP As Decimal
            Get
                Return _totalHargaPP
            End Get
            Set(ByVal value As Decimal)
                _totalHargaPP = value
            End Set
        End Property

        <ColumnInfo("TotalHargaLC", "{0}")> _
        Public Property TotalHargaLC As Decimal
            Get
                Return _totalHargaLC
            End Get
            Set(ByVal value As Decimal)
                _totalHargaLC = value
            End Set
        End Property

        <ColumnInfo("TotalDeposit", "{0}")> _
        Public Property TotalDeposit As Decimal
            Get
                Return _totalDeposit
            End Get
            Set(ByVal value As Decimal)
                _totalDeposit = value
            End Set
        End Property

        <ColumnInfo("TotalInterest", "{0}")> _
        Public Property TotalInterest As Decimal
            Get
                Return _totalInterest
            End Get
            Set(ByVal value As Decimal)
                _totalInterest = value
            End Set
        End Property

        <ColumnInfo("SPLNumber", "{0}")>
        Public Property SPLNumber As String
            Get
                Return _sPLNumber
            End Get
            Set(ByVal value As String)
                _sPLNumber = value
            End Set
        End Property

        <ColumnInfo("ETDDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ETDDate As DateTime
            Get
                Return _eTDDate
            End Get
            Set(ByVal value As DateTime)
                _eTDDate = value
            End Set
        End Property

        <ColumnInfo("EffectiveDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property EffectiveDate As DateTime
            Get
                Return _effectiveDate
            End Get
            Set(ByVal value As DateTime)
                _effectiveDate = value
            End Set
        End Property

        <ColumnInfo("ReleaseDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ReleaseDate As DateTime
            Get
                Return _releaseDate
            End Get
            Set(ByVal value As DateTime)
                _releaseDate = value
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

