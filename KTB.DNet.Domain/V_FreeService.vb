
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_FreeService Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 11/15/2016 - 9:11:51 AM
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
    <Serializable(), TableInfo("V_FreeService")> _
    Public Class V_FreeService
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++
        Private _iD As Integer
        Private _status As String = String.Empty
        Private _chassisMasterID As Integer
        Private _fSKindID As Byte
        Private _mileAge As Integer
        Private _serviceDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _serviceDealerID As Short
        Private _soldDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _notificationNumber As String
        Private _notificationType As String = String.Empty
        Private _totalAmount As Decimal
        Private _labourAmount As Decimal
        Private _partAmount As Decimal
        Private _pPNAmount As Decimal
        Private _pPHAmount As Decimal
        Private _reject As String = String.Empty
        Private _reason As Short
        Private _releaseBy As String = String.Empty
        Private _releaseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _visitType As String = String.Empty
        Private _cashBack As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerCode As String = String.Empty
        Private _dealerBranchCode As String = String.Empty
        Private _searchTerm1 As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _kindCode As String = String.Empty
        Private _categoryID As Byte
        Private _categoryCode As String = String.Empty
        Private _reasonCode As String = String.Empty
        Private _reasonDescription As String = String.Empty
        Private _NoRegRequest As String = String.Empty
        Private _WorkOrderNumber As String = String.Empty
        Private _fsType As String = String.Empty
        Private _transferDate As DateTime = DateTime.Now
        Private _assignment As String = String.Empty
        Private _accountingNo As String = String.Empty
        Private _billingNo As String = String.Empty
        Private _totalPembayaran As Decimal = 0
        Private _fileName As String = String.Empty
        Private _filePath As String = String.Empty



#End Region

#Region "Public Properties"

        <ColumnInfo("FSType", "{0}")> _
        Public Property FSType As String
            Get
                Return _fsType
            End Get
            Set(ByVal value As String)
                _fsType = value
            End Set
        End Property

        <ColumnInfo("TransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransferDate As DateTime
            Get
                Return _transferDate
            End Get
            Set(ByVal value As DateTime)
                _transferDate = value
            End Set
        End Property

        <ColumnInfo("Assignment", "{0}")> _
        Public Property Assignment As String
            Get
                Return _assignment
            End Get
            Set(ByVal value As String)
                _assignment = value
            End Set
        End Property

        <ColumnInfo("AccountingNo", "{0}")> _
        Public Property AccountingNo As String
            Get
                Return _accountingNo
            End Get
            Set(ByVal value As String)
                _accountingNo = value
            End Set
        End Property

        <ColumnInfo("BillingNo", "{0}")> _
        Public Property BillingNo As String
            Get
                Return _billingNo
            End Get
            Set(ByVal value As String)
                _billingNo = value
            End Set
        End Property

        <ColumnInfo("TotalPembayaran", "{0}")> _
        Public Property TotalPembayaran As Decimal
            Get
                Return _totalPembayaran
            End Get
            Set(ByVal value As Decimal)
                _totalPembayaran = value
            End Set
        End Property

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
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


        <ColumnInfo("ChassisMasterID", "{0}")> _
        Public Property ChassisMasterID As Integer
            Get
                Return _chassisMasterID
            End Get
            Set(ByVal value As Integer)
                _chassisMasterID = value
            End Set
        End Property


        <ColumnInfo("FSKindID", "{0}")> _
        Public Property FSKindID As Byte
            Get
                Return _fSKindID
            End Get
            Set(ByVal value As Byte)
                _fSKindID = value
            End Set
        End Property


        <ColumnInfo("MileAge", "{0}")> _
        Public Property MileAge As Integer
            Get
                Return _mileAge
            End Get
            Set(ByVal value As Integer)
                _mileAge = value
            End Set
        End Property


        <ColumnInfo("ServiceDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ServiceDate As DateTime
            Get
                Return _serviceDate
            End Get
            Set(ByVal value As DateTime)
                _serviceDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ServiceDealerID", "{0}")> _
        Public Property ServiceDealerID As Short
            Get
                Return _serviceDealerID
            End Get
            Set(ByVal value As Short)
                _serviceDealerID = value
            End Set
        End Property


        <ColumnInfo("SoldDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SoldDate As DateTime
            Get
                Return _soldDate
            End Get
            Set(ByVal value As DateTime)
                _soldDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("NotificationNumber", "{0}")> _
        Public Property NotificationNumber As String
            Get
                Return _notificationNumber
            End Get
            Set(ByVal value As String)
                _notificationNumber = value
            End Set
        End Property


        <ColumnInfo("NotificationType", "'{0}'")> _
        Public Property NotificationType As String
            Get
                Return _notificationType
            End Get
            Set(ByVal value As String)
                _notificationType = value
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


        <ColumnInfo("LabourAmount", "{0}")> _
        Public Property LabourAmount As Decimal
            Get
                Return _labourAmount
            End Get
            Set(ByVal value As Decimal)
                _labourAmount = value
            End Set
        End Property


        <ColumnInfo("PartAmount", "{0}")> _
        Public Property PartAmount As Decimal
            Get
                Return _partAmount
            End Get
            Set(ByVal value As Decimal)
                _partAmount = value
            End Set
        End Property


        <ColumnInfo("PPNAmount", "{0}")> _
        Public Property PPNAmount As Decimal
            Get
                Return _pPNAmount
            End Get
            Set(ByVal value As Decimal)
                _pPNAmount = value
            End Set
        End Property


        <ColumnInfo("PPHAmount", "{0}")> _
        Public Property PPHAmount As Decimal
            Get
                Return _pPHAmount
            End Get
            Set(ByVal value As Decimal)
                _pPHAmount = value
            End Set
        End Property


        <ColumnInfo("Reject", "'{0}'")> _
        Public Property Reject As String
            Get
                Return _reject
            End Get
            Set(ByVal value As String)
                _reject = value
            End Set
        End Property


        <ColumnInfo("Reason", "{0}")> _
        Public Property Reason As Short
            Get
                Return _reason
            End Get
            Set(ByVal value As Short)
                _reason = value
            End Set
        End Property


        <ColumnInfo("ReleaseBy", "'{0}'")> _
        Public Property ReleaseBy As String
            Get
                Return _releaseBy
            End Get
            Set(ByVal value As String)
                _releaseBy = value
            End Set
        End Property


        <ColumnInfo("ReleaseDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReleaseDate As DateTime
            Get
                Return _releaseDate
            End Get
            Set(ByVal value As DateTime)
                _releaseDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("VisitType", "'{0}'")> _
        Public Property VisitType As String
            Get
                Return _visitType
            End Get
            Set(ByVal value As String)
                _visitType = value
            End Set
        End Property

        <ColumnInfo("CashBack", "{0}")> _
        Public Property CashBack As Decimal
            Get
                Return _cashBack
            End Get
            Set(ByVal value As Decimal)
                _cashBack = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
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


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerBranchCode", "'{0}'")> _
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
            End Set
        End Property


        <ColumnInfo("SearchTerm1", "'{0}'")> _
        Public Property SearchTerm1 As String
            Get
                Return _searchTerm1
            End Get
            Set(ByVal value As String)
                _searchTerm1 = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property


        <ColumnInfo("KindCode", "'{0}'")> _
        Public Property KindCode As String
            Get
                Return _kindCode
            End Get
            Set(ByVal value As String)
                _kindCode = value
            End Set
        End Property


        <ColumnInfo("CategoryID", "{0}")> _
        Public Property CategoryID As Byte
            Get
                Return _categoryID
            End Get
            Set(ByVal value As Byte)
                _categoryID = value
            End Set
        End Property


        <ColumnInfo("CategoryCode", "'{0}'")> _
        Public Property CategoryCode As String
            Get
                Return _categoryCode
            End Get
            Set(ByVal value As String)
                _categoryCode = value
            End Set
        End Property


        <ColumnInfo("ReasonCode", "'{0}'")> _
        Public Property ReasonCode As String
            Get
                Return _reasonCode
            End Get
            Set(ByVal value As String)
                _reasonCode = value
            End Set
        End Property


        <ColumnInfo("ReasonDescription", "'{0}'")> _
        Public Property ReasonDescription As String
            Get
                Return _reasonDescription
            End Get
            Set(ByVal value As String)
                _reasonDescription = value
            End Set
        End Property


        <ColumnInfo("NoRegRequest", "'{0}'")> _
        Public Property NoRegRequest As String
            Get
                Return _NoRegRequest
            End Get
            Set(ByVal value As String)
                _NoRegRequest = value
            End Set
        End Property

        <ColumnInfo("WorkOrderNumber", "'{0}'")> _
        Public Property WorkOrderNumber As String
            Get
                Return _WorkOrderNumber
            End Get
            Set(ByVal value As String)
                _WorkOrderNumber = value
            End Set
        End Property

        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property

        <ColumnInfo("FilePath", "'{0}'")> _
        Public Property FilePath As String
            Get
                Return _filePath
            End Get
            Set(ByVal value As String)
                _filePath = value
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

