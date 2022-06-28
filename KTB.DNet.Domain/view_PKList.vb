#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : view_PKList Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2005 - 4:53:05 PM
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

    <Serializable(), TableInfo("view_PKList")> _
    Public Class view_PKList
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _pKStatus As String = String.Empty
        Private _pKNumber As String = String.Empty
        Private _pKDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _salesOrg As String = String.Empty
        Private _orderType As Short
        Private _productionYear As Short
        Private _projectName As String = String.Empty
        Private _kTBResponse As String = String.Empty
        Private _description As String = String.Empty
        Private _refPK As Integer
        Private _dealerCode As String = String.Empty
        Private _purpose As Short
        Private _rowStatus As Short
        Private _orderPlan As Short

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

        <ColumnInfo("PKStatus", "'{0}'")> _
        Public Property PKStatus() As String
            Get
                Return _pKStatus
            End Get
            Set(ByVal value As String)
                _pKStatus = value
            End Set
        End Property

        <ColumnInfo("PKNumber", "'{0}'")> _
        Public Property PKNumber() As String
            Get
                Return _pKNumber
            End Get
            Set(ByVal value As String)
                _pKNumber = value
            End Set
        End Property

        <ColumnInfo("PKDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PKDate() As DateTime
            Get
                Return _pKDate
            End Get
            Set(ByVal value As DateTime)
                _pKDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("SalesOrg", "'{0}'")> _
        Public Property SalesOrg() As String
            Get
                Return _salesOrg
            End Get
            Set(ByVal value As String)
                _salesOrg = value
            End Set
        End Property

        <ColumnInfo("OrderType", "{0}")> _
        Public Property OrderType() As Short
            Get
                Return _orderType
            End Get
            Set(ByVal value As Short)
                _orderType = value
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

        <ColumnInfo("ProjectName", "'{0}'")> _
        Public Property ProjectName() As String
            Get
                Return _projectName
            End Get
            Set(ByVal value As String)
                _projectName = value
            End Set
        End Property

        <ColumnInfo("KTBResponse", "'{0}'")> _
        Public Property KTBResponse() As String
            Get
                Return _kTBResponse
            End Get
            Set(ByVal value As String)
                _kTBResponse = value
            End Set
        End Property

        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        <ColumnInfo("RefPK", "{0}")> _
        Public Property RefPK() As Integer
            Get
                Return _refPK
            End Get
            Set(ByVal value As Integer)
                _refPK = value
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

        <ColumnInfo("Purpose", "{0}")> _
        Public Property Purpose() As Short
            Get
                Return _purpose
            End Get
            Set(ByVal value As Short)
                _purpose = value
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

        <ColumnInfo("OrderPlan", "{0}")> _
        Public Property OrderPlan() As Short
            Get
                Return _orderPlan
            End Get
            Set(ByVal value As Short)
                _orderPlan = value
            End Set
        End Property

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace