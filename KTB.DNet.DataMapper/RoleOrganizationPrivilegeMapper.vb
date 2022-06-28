#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RoleOrganizationPrivilege Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 2/2/2006 - 9:05:02 AM
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class RoleOrganizationPrivilegeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRoleOrganizationPrivilege"
        Private m_UpdateStatement As String = "up_UpdateRoleOrganizationPrivilege"
        Private m_RetrieveStatement As String = "up_RetrieveRoleOrganizationPrivilege"
        Private m_RetrieveListStatement As String = "up_RetrieveRoleOrganizationPrivilegeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRoleOrganizationPrivilege"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim roleOrganizationPrivilege As RoleOrganizationPrivilege = Nothing
            While dr.Read

                roleOrganizationPrivilege = Me.CreateObject(dr)

            End While

            Return roleOrganizationPrivilege

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim roleOrganizationPrivilegeList As ArrayList = New ArrayList

            While dr.Read
                Dim roleOrganizationPrivilege As RoleOrganizationPrivilege = Me.CreateObject(dr)
                roleOrganizationPrivilegeList.Add(roleOrganizationPrivilege)
            End While

            Return roleOrganizationPrivilegeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim roleOrganizationPrivilege As RoleOrganizationPrivilege = CType(obj, RoleOrganizationPrivilege)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, roleOrganizationPrivilege.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim roleOrganizationPrivilege As RoleOrganizationPrivilege = CType(obj, RoleOrganizationPrivilege)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, roleOrganizationPrivilege.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, roleOrganizationPrivilege.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@OrganizationPrivilegeID", DbType.Int32, Me.GetRefObject(roleOrganizationPrivilege.OrganizationPrivilege))
            DbCommandWrapper.AddInParameter("@RoleID", DbType.Int32, Me.GetRefObject(roleOrganizationPrivilege.Role))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim roleOrganizationPrivilege As RoleOrganizationPrivilege = CType(obj, RoleOrganizationPrivilege)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, roleOrganizationPrivilege.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, roleOrganizationPrivilege.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, roleOrganizationPrivilege.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@OrganizationPrivilegeID", DbType.Int32, Me.GetRefObject(roleOrganizationPrivilege.OrganizationPrivilege))
            DbCommandWrapper.AddInParameter("@RoleID", DbType.Int32, Me.GetRefObject(roleOrganizationPrivilege.Role))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RoleOrganizationPrivilege

            Dim roleOrganizationPrivilege As RoleOrganizationPrivilege = New RoleOrganizationPrivilege

            roleOrganizationPrivilege.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then roleOrganizationPrivilege.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then roleOrganizationPrivilege.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then roleOrganizationPrivilege.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then roleOrganizationPrivilege.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then roleOrganizationPrivilege.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OrganizationPrivilegeID")) Then
                roleOrganizationPrivilege.OrganizationPrivilege = New OrganizationPrivilege(CType(dr("OrganizationPrivilegeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("RoleID")) Then
                roleOrganizationPrivilege.Role = New Role(CType(dr("RoleID"), Integer))
            End If

            Return roleOrganizationPrivilege

        End Function

        Private Sub SetTableName()

            If Not (GetType(RoleOrganizationPrivilege) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RoleOrganizationPrivilege), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RoleOrganizationPrivilege).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

