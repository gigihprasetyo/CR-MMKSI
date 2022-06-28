
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_MonthlyReport Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 02/05/2018 - 10:56:02 AM
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
    <Serializable(), TableInfo("V_MonthlyReport")> _
    Public Class V_MonthlyReport
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
        Private _kind As Integer
        Private _periodeMonth As Short
        Private _periodeYear As Short
        Private _productCategoryID As Short
        Private _dealerID As Short
        Private _fileName As String = String.Empty
        Private _fileSize As Integer
        Private _lastDownloadBy As String = String.Empty
        Private _lastDownloadDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _billingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _billingNo As String = String.Empty
        Private _accountingNo As String = String.Empty
        Private _taxNo As String = String.Empty
        Private _transferDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerCode As String = String.Empty
        Private _period As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isNullLastDownloadDate As Integer
        Private _isNullTransferDate As Integer




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


        <ColumnInfo("Kind", "{0}")> _
        Public Property Kind As Integer
            Get
                Return _kind
            End Get
            Set(ByVal value As Integer)
                _kind = value
            End Set
        End Property


        <ColumnInfo("PeriodeMonth", "{0}")> _
        Public Property PeriodeMonth As Short
            Get
                Return _periodeMonth
            End Get
            Set(ByVal value As Short)
                _periodeMonth = value
            End Set
        End Property


        <ColumnInfo("PeriodeYear", "{0}")> _
        Public Property PeriodeYear As Short
            Get
                Return _periodeYear
            End Get
            Set(ByVal value As Short)
                _periodeYear = value
            End Set
        End Property


        <ColumnInfo("ProductCategoryID", "{0}")> _
        Public Property ProductCategoryID As Short
            Get
                Return _productCategoryID
            End Get
            Set(ByVal value As Short)
                _productCategoryID = value
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


        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property


        <ColumnInfo("FileSize", "{0}")> _
        Public Property FileSize As Integer
            Get
                Return _fileSize
            End Get
            Set(ByVal value As Integer)
                _fileSize = value
            End Set
        End Property


        <ColumnInfo("LastDownloadBy", "'{0}'")> _
        Public Property LastDownloadBy As String
            Get
                Return _lastDownloadBy
            End Get
            Set(ByVal value As String)
                _lastDownloadBy = value
            End Set
        End Property


        <ColumnInfo("LastDownloadDate", "'{0:yyyy/MM/dd}'")> _
        Public Property LastDownloadDate As DateTime
            Get
                Return _lastDownloadDate
            End Get
            Set(ByVal value As DateTime)
                _lastDownloadDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("BillingNo", "'{0}'")> _
        Public Property BillingNo As String
            Get
                Return _billingNo
            End Get
            Set(ByVal value As String)
                _billingNo = value
            End Set
        End Property


        <ColumnInfo("AccountingNo", "'{0}'")> _
        Public Property AccountingNo As String
            Get
                Return _accountingNo
            End Get
            Set(ByVal value As String)
                _accountingNo = value
            End Set
        End Property


        <ColumnInfo("TaxNo", "'{0}'")> _
        Public Property TaxNo As String
            Get
                Return _taxNo
            End Get
            Set(ByVal value As String)
                _taxNo = value
            End Set
        End Property


        <ColumnInfo("TransferDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransferDate As DateTime
            Get
                Return _transferDate
            End Get
            Set(ByVal value As DateTime)
                _transferDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("Period", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property Period As DateTime
            Get
                Return _period
            End Get
            Set(ByVal value As DateTime)
                _period = value
            End Set
        End Property


        <ColumnInfo("IsNullLastDownloadDate", "{0}")> _
        Public Property IsNullLastDownloadDate As Integer
            Get
                Return _isNullLastDownloadDate
            End Get
            Set(ByVal value As Integer)
                _isNullLastDownloadDate = value
            End Set
        End Property


        <ColumnInfo("IsNullTransferDate", "{0}")> _
        Public Property IsNullTransferDate As Integer
            Get
                Return _isNullTransferDate
            End Get
            Set(ByVal value As Integer)
                _isNullTransferDate = value
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

