
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
    <Serializable(), TableInfo("VWI_PRHistoryDO")> _
    Public Class VWI_PRHistoryDO
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal PODate As DateTime, ByVal OrderType As Char)

        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _dealerID As Short
        Private _dealerCode As String
        Private _dealerName As String
        Private _pONumber As String
        Private _pODate As DateTime
        Private _dMSPRNo As String
        Private _orderType As String
        Private _sONumber As String
        Private _nomorDO As String
        Private _tanggalDO As DateTime
        Private _sparePartMasterID As Integer
        Private _partNumber As String
        Private _partName As String
        Private _qty As Integer
        Private _estimasiTanggalPengiriman As DateTime
        Private _pickingDate As DateTime
        Private _packingDate As DateTime
        Private _goodIssueDate As DateTime
        Private _paymentDate As DateTime
        Private _readyForDeliveryDate As DateTime
        Private _expeditionNo As String
        Private _expeditionName As String
        Private _aTD As DateTime
        Private _eTA As DateTime

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
        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
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
        <ColumnInfo("SONumber", "'{0}'")> _
        Public Property SONumber As String
            Get
                Return _sONumber
            End Get
            Set(ByVal value As String)
                _sONumber = value
            End Set
        End Property
        <ColumnInfo("NomorDO", "'{0}'")> _
        Public Property NomorDO As String
            Get
                Return _nomorDO
            End Get
            Set(ByVal value As String)
                _nomorDO = value
            End Set
        End Property
        <ColumnInfo("TanggalDO", "'{0:yyyy/MM/dd}'")> _
        Public Property TanggalDO As DateTime
            Get
                Return _tanggalDO
            End Get
            Set(ByVal value As DateTime)
                _tanggalDO = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property
        <ColumnInfo("SparePartMasterID", "{0}")> _
        Public Property SparePartMasterID As Integer
            Get
                Return _sparePartMasterID
            End Get
            Set(ByVal value As Integer)
                _sparePartMasterID = value
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
        <ColumnInfo("EstimasiTanggalPengiriman", "'{0:yyyy/MM/dd}'")> _
        Public Property EstimasiTanggalPengiriman As DateTime
            Get
                Return _estimasiTanggalPengiriman
            End Get
            Set(ByVal value As DateTime)
                _estimasiTanggalPengiriman = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property
        <ColumnInfo("PickingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PickingDate As DateTime
            Get
                Return _pickingDate
            End Get
            Set(ByVal value As DateTime)
                _pickingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property
        <ColumnInfo("PackingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PackingDate As DateTime
            Get
                Return _packingDate
            End Get
            Set(ByVal value As DateTime)
                _packingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property
        <ColumnInfo("GoodIssueDate", "'{0:yyyy/MM/dd}'")> _
        Public Property GoodIssueDate As DateTime
            Get
                Return _goodIssueDate
            End Get
            Set(ByVal value As DateTime)
                _goodIssueDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property
        <ColumnInfo("PaymentDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PaymentDate As DateTime
            Get
                Return _paymentDate
            End Get
            Set(ByVal value As DateTime)
                _paymentDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property
        <ColumnInfo("ReadyForDeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReadyForDeliveryDate As DateTime
            Get
                Return _readyForDeliveryDate
            End Get
            Set(ByVal value As DateTime)
                _readyForDeliveryDate = New DateTime(value.Year, value.Month, value.Day)
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
        <ColumnInfo("ExpeditionName", "'{0}'")> _
        Public Property ExpeditionName As String
            Get
                Return _expeditionName
            End Get
            Set(ByVal value As String)
                _expeditionName = value
            End Set
        End Property
        <ColumnInfo("ATD", "'{0:yyyy/MM/dd}'")> _
        Public Property ATD As DateTime
            Get
                Return _aTD
            End Get
            Set(ByVal value As DateTime)
                _aTD = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property
        <ColumnInfo("ETA", "'{0:yyyy/MM/dd}'")> _
        Public Property ETA As DateTime
            Get
                Return _eTA
            End Get
            Set(ByVal value As DateTime)
                _eTA = New DateTime(value.Year, value.Month, value.Day)
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

