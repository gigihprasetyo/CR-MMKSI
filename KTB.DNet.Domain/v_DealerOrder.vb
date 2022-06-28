
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_DealerOrder Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 5/31/2012 - 2:29:18 PM
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
    <Serializable(), TableInfo("v_DealerOrder")> _
    Public Class v_DealerOrder
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
        Private _dealerName As String = String.Empty
        Private _reqAllocationDateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _materialNumber As String = String.Empty
        Private _pODetailID As Integer
        Private _allocationDateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pONumber As String = String.Empty
        Private _dealerPONumber As String = String.Empty
        Private _stokATP As Integer
        Private _stokSebelum As Integer
        Private _reqQty As Integer
        Private _allocQty As Integer
        Private _unAllocated As Integer
        Private _stokSesudah As Integer
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


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("ReqAllocationDateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ReqAllocationDateTime() As DateTime
            Get
                Return _reqAllocationDateTime
            End Get
            Set(ByVal value As DateTime)
                _reqAllocationDateTime = value
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


        <ColumnInfo("AllocationDateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property AllocationDateTime() As DateTime
            Get
                Return _allocationDateTime
            End Get
            Set(ByVal value As DateTime)
                _allocationDateTime = value
            End Set
        End Property


        <ColumnInfo("PONumber", "'{0}'")> _
        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal value As String)
                _pONumber = value
            End Set
        End Property


        <ColumnInfo("DealerPONumber", "'{0}'")> _
        Public Property DealerPONumber() As String
            Get
                Return _dealerPONumber
            End Get
            Set(ByVal value As String)
                _dealerPONumber = value
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


        <ColumnInfo("ReqQty", "{0}")> _
        Public Property ReqQty() As Integer
            Get
                Return _reqQty
            End Get
            Set(ByVal value As Integer)
                _reqQty = value
            End Set
        End Property


        <ColumnInfo("AllocQty", "{0}")> _
        Public Property AllocQty() As Integer
            Get
                Return _allocQty
            End Get
            Set(ByVal value As Integer)
                _allocQty = value
            End Set
        End Property


        <ColumnInfo("UnAllocated", "{0}")> _
        Public Property UnAllocated() As Integer
            Get
                Return _unAllocated
            End Get
            Set(ByVal value As Integer)
                _unAllocated = value
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

