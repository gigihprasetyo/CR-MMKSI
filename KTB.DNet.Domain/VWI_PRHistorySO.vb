
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
    <Serializable(), TableInfo("VWI_PRHistorySO")> _
    Public Class VWI_PRHistorySO
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal PODate As DateTime, ByVal OrderType As Char)

        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _pONumber As String
        Private _dealerID As Short
        Private _dealerCode As String
        Private _pODate As DateTime
        Private _dMSPRNo As String
        Private _orderType As Char
        Private _sODate As DateTime
        Private _nomorPenjualan As String
        Private _documentType As String
        Private _kodeBarang As String
        Private _namaBarang As String
        Private _jumlahPesanan As Integer
        Private _jumlahPemenuhan As Integer
        Private _hargaEceran As Decimal
        Private _totalBaseAmountDetail As Integer
        Private _nomorPengganti As String
        Private _diskon As Decimal
        Private _totalAmountDetail As Integer
        Private _totalBaseAmountHeader As Integer
        Private _totalConsumptionTaxAmount As Integer
        Private _totalAmountHeader As Integer
        Private _status As String
        Private _statusDesc As String
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

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
        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property
        <ColumnInfo("DealerId", "{0}")> _
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
        <ColumnInfo("PODate", "'{0:yyyy/MM/dd}'")> _
        Public Property PODate As DateTime
            Get
                Return _pODate
            End Get
            Set(ByVal value As DateTime)
                _pODate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property
        <ColumnInfo("DMSPRNo", "'{0}'")> _
        Public Property DMSPRNo As String
            Get
                Return _dMSPRNo
            End Get
            Set(ByVal value As String)
                _dMSPRNo = value
            End Set
        End Property
        <ColumnInfo("OrderType", "'{0}'")> _
        Public Property OrderType As Char
            Get
                Return _orderType
            End Get
            Set(ByVal value As Char)
                _orderType = value
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
        <ColumnInfo("NomorPenjualan", "'{0}'")> _
        Public Property NomorPenjualan As String
            Get
                Return _nomorPenjualan
            End Get
            Set(ByVal value As String)
                _nomorPenjualan = value
            End Set
        End Property
        <ColumnInfo("DocumentType", "'{0}'")> _
        Public Property DocumentType As String
            Get
                Return _documentType
            End Get
            Set(ByVal value As String)
                _documentType = value
            End Set
        End Property
        <ColumnInfo("KodeBarang", "'{0}'")> _
        Public Property KodeBarang As String
            Get
                Return _kodeBarang
            End Get
            Set(ByVal value As String)
                _kodeBarang = value
            End Set
        End Property
        <ColumnInfo("NamaBarang", "'{0}'")> _
        Public Property NamaBarang As String
            Get
                Return _namaBarang
            End Get
            Set(ByVal value As String)
                _namaBarang = value
            End Set
        End Property
        <ColumnInfo("JumlahPesanan", "{0}")> _
        Public Property JumlahPesanan As Integer
            Get
                Return _jumlahPesanan
            End Get
            Set(ByVal value As Integer)
                _jumlahPesanan = value
            End Set
        End Property
        <ColumnInfo("JumlahPemenuhan", "{0}")> _
        Public Property JumlahPemenuhan As Integer
            Get
                Return _jumlahPemenuhan
            End Get
            Set(ByVal value As Integer)
                _jumlahPemenuhan = value
            End Set
        End Property
        <ColumnInfo("HargaEceran", "{0}")> _
        Public Property HargaEceran As Decimal
            Get
                Return _hargaEceran
            End Get
            Set(ByVal value As Decimal)
                _hargaEceran = value
            End Set
        End Property
        <ColumnInfo("TotalBaseAmountDetail", "{0}")> _
        Public Property TotalBaseAmountDetail As Integer
            Get
                Return _totalBaseAmountDetail
            End Get
            Set(ByVal value As Integer)
                _totalBaseAmountDetail = value
            End Set
        End Property
        <ColumnInfo("NomorPengganti", "'{0}'")> _
        Public Property NomorPengganti As String
            Get
                Return _nomorPengganti
            End Get
            Set(ByVal value As String)
                _nomorPengganti = value
            End Set
        End Property
        <ColumnInfo("Diskon", "{0}")> _
        Public Property Diskon As Decimal
            Get
                Return _diskon
            End Get
            Set(ByVal value As Decimal)
                _diskon = value
            End Set
        End Property
        <ColumnInfo("TotalAmountDetail", "{0}")> _
        Public Property TotalAmountDetail As Integer
            Get
                Return _totalAmountDetail
            End Get
            Set(ByVal value As Integer)
                _totalAmountDetail = value
            End Set
        End Property
        <ColumnInfo("TotalBaseAmountHeader", "{0}")> _
        Public Property TotalBaseAmountHeader As Integer
            Get
                Return _totalBaseAmountHeader
            End Get
            Set(ByVal value As Integer)
                _totalBaseAmountHeader = value
            End Set
        End Property
        <ColumnInfo("TotalConsumptionTaxAmount", "{0}")> _
        Public Property TotalConsumptionTaxAmount As Integer
            Get
                Return _totalConsumptionTaxAmount
            End Get
            Set(ByVal value As Integer)
                _totalConsumptionTaxAmount = value
            End Set
        End Property
        <ColumnInfo("TotalAmountHeader", "{0}")> _
        Public Property TotalAmountHeader As Integer
            Get
                Return _totalAmountHeader
            End Get
            Set(ByVal value As Integer)
                _totalAmountHeader = value
            End Set
        End Property
        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property
        <ColumnInfo("StatusDesc", "'{0}'")> _
        Public Property StatusDesc As String
            Get
                Return _statusDesc
            End Get
            Set(ByVal value As String)
                _statusDesc = value
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

#End Region

    End Class
End Namespace

