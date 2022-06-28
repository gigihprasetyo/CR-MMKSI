#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Role Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 12/21/2005 - 10:51:01 AM
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
    <Serializable(), TableInfo("Role")> _
    Public Class Role
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
        Private _roleName As String = String.Empty
        Private _description As String = String.Empty
        Private _roleStatus As Boolean
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

        Private _roleOrganizationPrivileges As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("RoleName", "'{0}'")> _
        Public Property RoleName() As String
            Get
                Return _roleName
            End Get
            Set(ByVal value As String)
                _roleName = value
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


        <ColumnInfo("RoleStatus", "{0}")> _
        Public Property RoleStatus() As Boolean
            Get
                Return _roleStatus
            End Get
            Set(ByVal value As Boolean)
                _roleStatus = value
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




        <ColumnInfo("OrganizationID", "{0}"), _
        RelationInfo("Dealer", "ID", "Role", "OrganizationID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("Role", "ID", "RoleOrganizationPrivilege", "RoleID")> _
        Public ReadOnly Property RoleOrganizationPrivileges() As System.Collections.ArrayList
            Get
                Try
                    If (Me._roleOrganizationPrivileges.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(RoleOrganizationPrivilege), "Role", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(RoleOrganizationPrivilege), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._roleOrganizationPrivileges = DoLoadArray(GetType(RoleOrganizationPrivilege).ToString, criterias)
                    End If

                    Return Me._roleOrganizationPrivileges

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

#Region "Custom Method"

#End Region

    End Class
End Namespace

