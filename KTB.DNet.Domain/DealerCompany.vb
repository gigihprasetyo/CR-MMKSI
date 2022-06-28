#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Dealer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 8:51:44 AM
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
    <Serializable(), TableInfo("DealerCompany")> _
    Public Class DealerCompany
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"


        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _id = ID
        End Sub

#End Region

#Region "Private Variables"
        Private _id As Integer
        Private _dealerCompanyCode As String
        Private _dealerCompanyName As String
        Private _dealerGroupId As Integer

        Private _dealerGroupName As String

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
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property

        <ColumnInfo("DealerCompanyCode", "{'0'}")> _
        Public Property DealerCompanyCode() As String
            Get
                Return _dealerCompanyCode
            End Get
            Set(ByVal value As String)
                _dealerCompanyCode = value
            End Set
        End Property

        <ColumnInfo("DealerCompanyName", "{'0'}")> _
        Public Property DealerCompanyName() As String
            Get
                Return _dealerCompanyName

            End Get
            Set(ByVal value As String)
                _dealerCompanyName = value
            End Set
        End Property

        <ColumnInfo("DealerGroupID", "{0}")> _
        Public Property DealerGroupID() As Integer
            Get
                Return _dealerGroupId

            End Get
            Set(ByVal value As Integer)
                _dealerGroupId = value
            End Set
        End Property

        <ColumnInfo("DealerGroupName", "{'0'}")> _
        Public Property DealerGroupName() As String
            Get
                Return _dealerGroupName

            End Get
            Set(ByVal value As String)
                _dealerGroupName = value
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


    End Class
End Namespace

