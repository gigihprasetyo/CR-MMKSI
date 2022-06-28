#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VDH Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 9/3/2006 - 9:59:38 AM
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
    <Serializable(), TableInfo("VDH")> _
    Public Class VDH
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
        Private _chassisNo As String = String.Empty
        Private _itemNo As String = String.Empty
        Private _engineNo As String = String.Empty
        Private _mMCLotNo As String = String.Empty
        Private _invoiceBuy As String = String.Empty
        Private _productionYear As String = String.Empty
        Private _nIKNo As String = String.Empty
        Private _receiptCBUDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _carrosseryTransferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _receiptCarrosseryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _serial As String = String.Empty
        Private _customer As String = String.Empty
        Private _endCustomerName As String = String.Empty
        Private _endCustomerAddress As String = String.Empty
        Private _kelurahan As String = String.Empty
        Private _kecamatan As String = String.Empty
        Private _kabupaten As String = String.Empty
        Private _propinsi As String = String.Empty
        Private _r As String = String.Empty
        Private _type As String = String.Empty
        Private _requestDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dOPrintDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _scheduleShipDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sCVDate1 As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sVCDate2 As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sVCCust1 As String = String.Empty
        Private _sVCCust2 As String = String.Empty
        Private _factureOpenDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _factureDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _factureNo As String = String.Empty
        Private _factureComment As String = String.Empty
        Private _vATNo As String = String.Empty
        Private _vATDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _stockOutDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _orders As String = String.Empty
        Private _pIUDNo As String = String.Empty
        Private _pIUDDate As String = String.Empty
        Private _incoiveSell As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _vDHCustomer As VDHCustomer

        Private _vDHServices As System.Collections.ArrayList = New System.Collections.ArrayList


#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("ChassisNo", "'{0}'")> _
        Public Property ChassisNo() As String
            Get
                Return _chassisNo
            End Get
            Set(ByVal value As String)
                _chassisNo = value
            End Set
        End Property


        <ColumnInfo("ItemNo", "'{0}'")> _
        Public Property ItemNo() As String
            Get
                Return _itemNo
            End Get
            Set(ByVal value As String)
                _itemNo = value
            End Set
        End Property


        <ColumnInfo("EngineNo", "'{0}'")> _
        Public Property EngineNo() As String
            Get
                Return _engineNo
            End Get
            Set(ByVal value As String)
                _engineNo = value
            End Set
        End Property


        <ColumnInfo("MMCLotNo", "'{0}'")> _
        Public Property MMCLotNo() As String
            Get
                Return _mMCLotNo
            End Get
            Set(ByVal value As String)
                _mMCLotNo = value
            End Set
        End Property


        <ColumnInfo("InvoiceBuy", "'{0}'")> _
        Public Property InvoiceBuy() As String
            Get
                Return _invoiceBuy
            End Get
            Set(ByVal value As String)
                _invoiceBuy = value
            End Set
        End Property


        <ColumnInfo("ProductionYear", "'{0}'")> _
        Public Property ProductionYear() As String
            Get
                Return _productionYear
            End Get
            Set(ByVal value As String)
                _productionYear = value
            End Set
        End Property


        <ColumnInfo("NIKNo", "'{0}'")> _
        Public Property NIKNo() As String
            Get
                Return _nIKNo
            End Get
            Set(ByVal value As String)
                _nIKNo = value
            End Set
        End Property


        <ColumnInfo("ReceiptCBUDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReceiptCBUDate() As DateTime
            Get
                Return _receiptCBUDate
            End Get
            Set(ByVal value As DateTime)
                _receiptCBUDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CarrosseryTransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property CarrosseryTransferDate() As DateTime
            Get
                Return _carrosseryTransferDate
            End Get
            Set(ByVal value As DateTime)
                _carrosseryTransferDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ReceiptCarrosseryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReceiptCarrosseryDate() As DateTime
            Get
                Return _receiptCarrosseryDate
            End Get
            Set(ByVal value As DateTime)
                _receiptCarrosseryDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Serial", "'{0}'")> _
        Public Property Serial() As String
            Get
                Return _serial
            End Get
            Set(ByVal value As String)
                _serial = value
            End Set
        End Property


        <ColumnInfo("Customer", "'{0}'")> _
        Public Property Customer() As String
            Get
                Return _customer
            End Get
            Set(ByVal value As String)
                _customer = value
            End Set
        End Property


        <ColumnInfo("EndCustomerName", "'{0}'")> _
        Public Property EndCustomerName() As String
            Get
                Return _endCustomerName
            End Get
            Set(ByVal value As String)
                _endCustomerName = value
            End Set
        End Property


        <ColumnInfo("EndCustomerAddress", "'{0}'")> _
        Public Property EndCustomerAddress() As String
            Get
                Return _endCustomerAddress
            End Get
            Set(ByVal value As String)
                _endCustomerAddress = value
            End Set
        End Property


        <ColumnInfo("Kelurahan", "'{0}'")> _
        Public Property Kelurahan() As String
            Get
                Return _kelurahan
            End Get
            Set(ByVal value As String)
                _kelurahan = value
            End Set
        End Property


        <ColumnInfo("Kecamatan", "'{0}'")> _
        Public Property Kecamatan() As String
            Get
                Return _kecamatan
            End Get
            Set(ByVal value As String)
                _kecamatan = value
            End Set
        End Property


        <ColumnInfo("Kabupaten", "'{0}'")> _
        Public Property Kabupaten() As String
            Get
                Return _kabupaten
            End Get
            Set(ByVal value As String)
                _kabupaten = value
            End Set
        End Property


        <ColumnInfo("Propinsi", "'{0}'")> _
        Public Property Propinsi() As String
            Get
                Return _propinsi
            End Get
            Set(ByVal value As String)
                _propinsi = value
            End Set
        End Property


        <ColumnInfo("R", "'{0}'")> _
        Public Property R() As String
            Get
                Return _r
            End Get
            Set(ByVal value As String)
                _r = value
            End Set
        End Property


        <ColumnInfo("Type", "'{0}'")> _
        Public Property Type() As String
            Get
                Return _type
            End Get
            Set(ByVal value As String)
                _type = value
            End Set
        End Property


        <ColumnInfo("RequestDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RequestDate() As DateTime
            Get
                Return _requestDate
            End Get
            Set(ByVal value As DateTime)
                _requestDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DOPrintDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DOPrintDate() As DateTime
            Get
                Return _dOPrintDate
            End Get
            Set(ByVal value As DateTime)
                _dOPrintDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ScheduleShipDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ScheduleShipDate() As DateTime
            Get
                Return _scheduleShipDate
            End Get
            Set(ByVal value As DateTime)
                _scheduleShipDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("SCVDate1", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SCVDate1() As DateTime
            Get
                Return _sCVDate1
            End Get
            Set(ByVal value As DateTime)
                _sCVDate1 = value
            End Set
        End Property


        <ColumnInfo("SVCDate2", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SVCDate2() As DateTime
            Get
                Return _sVCDate2
            End Get
            Set(ByVal value As DateTime)
                _sVCDate2 = value
            End Set
        End Property


        <ColumnInfo("SVCCust1", "'{0}'")> _
        Public Property SVCCust1() As String
            Get
                Return _sVCCust1
            End Get
            Set(ByVal value As String)
                _sVCCust1 = value
            End Set
        End Property


        <ColumnInfo("SVCCust2", "'{0}'")> _
        Public Property SVCCust2() As String
            Get
                Return _sVCCust2
            End Get
            Set(ByVal value As String)
                _sVCCust2 = value
            End Set
        End Property


        <ColumnInfo("FactureOpenDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FactureOpenDate() As DateTime
            Get
                Return _factureOpenDate
            End Get
            Set(ByVal value As DateTime)
                _factureOpenDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("FactureDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FactureDate() As DateTime
            Get
                Return _factureDate
            End Get
            Set(ByVal value As DateTime)
                _factureDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("FactureNo", "'{0}'")> _
        Public Property FactureNo() As String
            Get
                Return _factureNo
            End Get
            Set(ByVal value As String)
                _factureNo = value
            End Set
        End Property


        <ColumnInfo("FactureComment", "'{0}'")> _
        Public Property FactureComment() As String
            Get
                Return _factureComment
            End Get
            Set(ByVal value As String)
                _factureComment = value
            End Set
        End Property


        <ColumnInfo("VATNo", "'{0}'")> _
        Public Property VATNo() As String
            Get
                Return _vATNo
            End Get
            Set(ByVal value As String)
                _vATNo = value
            End Set
        End Property


        <ColumnInfo("VATDate", "'{0:yyyy/MM/dd}'")> _
        Public Property VATDate() As DateTime
            Get
                Return _vATDate
            End Get
            Set(ByVal value As DateTime)
                _vATDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("StockOutDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StockOutDate() As DateTime
            Get
                Return _stockOutDate
            End Get
            Set(ByVal value As DateTime)
                _stockOutDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Orders", "'{0}'")> _
        Public Property Orders() As String
            Get
                Return _orders
            End Get
            Set(ByVal value As String)
                _orders = value
            End Set
        End Property


        <ColumnInfo("PIUDNo", "'{0}'")> _
        Public Property PIUDNo() As String
            Get
                Return _pIUDNo
            End Get
            Set(ByVal value As String)
                _pIUDNo = value
            End Set
        End Property


        <ColumnInfo("PIUDDate", "'{0}'")> _
        Public Property PIUDDate() As String
            Get
                Return _pIUDDate
            End Get
            Set(ByVal value As String)
                _pIUDDate = value
            End Set
        End Property


        <ColumnInfo("IncoiveSell", "'{0}'")> _
        Public Property IncoiveSell() As String
            Get
                Return _incoiveSell
            End Get
            Set(ByVal value As String)
                _incoiveSell = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("VDHCustomerID", "{0}"), _
        RelationInfo("VDHCustomer", "ID", "VDH", "VDHCustomerID")> _
        Public Property VDHCustomer() As VDHCustomer
            Get
                Try
                    If Not isnothing(Me._vDHCustomer) AndAlso (Not Me._vDHCustomer.IsLoaded) Then

                        Me._vDHCustomer = CType(DoLoad(GetType(VDHCustomer).ToString(), _vDHCustomer.ID), VDHCustomer)
                        Me._vDHCustomer.MarkLoaded()

                    End If

                    Return Me._vDHCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VDHCustomer)

                Me._vDHCustomer = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vDHCustomer.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("VDH", "ID", "VDHService", "VDHID")> _
        Public ReadOnly Property VDHServices() As System.Collections.ArrayList
            Get
                Try
                    If (Me._vDHServices.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(VDHService), "VDH", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(VDHService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._vDHServices = DoLoadArray(GetType(VDHService).ToString, criterias)
                    End If

                    Return Me._vDHServices

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
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

