
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ATPHistory Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 5/29/2012 - 11:13:31 AM
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
    <Serializable(), TableInfo("ATPHistory")> _
    Public Class ATPHistory
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
        Private _allocationDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _materialNumber As String = String.Empty
        Private _pODetailID As Integer
        Private _stokATP As Integer
        Private _stokSebelum As Integer
        Private _stokSesudah As Integer
        Private _downloadedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _productionYear As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("AllocationDate", "'{0:yyyy/MM/dd}'")> _
        Public Property AllocationDate() As DateTime
            Get
                Return _allocationDate
            End Get
            Set(ByVal value As DateTime)
                _allocationDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("MaterialNumber", "'{0}'")> _
        Public Property MaterialNumber() As String
            Get
                Return _materialNumber
            End Get
            Set(ByVal value As String)
                _materialNumber = value
            End Set
        End Property


        <ColumnInfo("PODetailID", "{0}")> _
        Public Property PODetailID() As Integer
            Get
                Return _pODetailID
            End Get
            Set(ByVal value As Integer)
                _pODetailID = value
            End Set
        End Property


        <ColumnInfo("StokATP", "{0}")> _
        Public Property StokATP() As Integer
            Get
                Return _stokATP
            End Get
            Set(ByVal value As Integer)
                _stokATP = value
            End Set
        End Property


        <ColumnInfo("StokSebelum", "{0}")> _
        Public Property StokSebelum() As Integer
            Get
                Return _stokSebelum
            End Get
            Set(ByVal value As Integer)
                _stokSebelum = value
            End Set
        End Property


        <ColumnInfo("StokSesudah", "{0}")> _
        Public Property StokSesudah() As Integer
            Get
                Return _stokSesudah
            End Get
            Set(ByVal value As Integer)
                _stokSesudah = value
            End Set
        End Property

        <ColumnInfo("DownloadedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DownloadedTime() As DateTime
            Get
                Return _downloadedTime
            End Get
            Set(ByVal value As DateTime)
                _downloadedTime = value
            End Set
        End Property


        <ColumnInfo("ProductionYear", "{0}")> _
        Public Property ProductionYear() As Short
            Get
                Return _productionYear
            End Get
            Set(ByVal value As Short)
                _productionYear = value
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

