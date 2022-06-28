#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ProfileHeaderToGroup Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 28/08/2007 - 16:05:32
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

    Public Class ProfileHeaderToGroupMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertProfileHeaderToGroup"
        Private m_UpdateStatement As String = "up_UpdateProfileHeaderToGroup"
        Private m_RetrieveStatement As String = "up_RetrieveProfileHeaderToGroup"
        Private m_RetrieveListStatement As String = "up_RetrieveProfileHeaderToGroupList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteProfileHeaderToGroup"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim profileHeaderToGroup As ProfileHeaderToGroup = Nothing
            While dr.Read

                profileHeaderToGroup = Me.CreateObject(dr)

            End While

            Return profileHeaderToGroup

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim profileHeaderToGroupList As ArrayList = New ArrayList

            While dr.Read
                Dim profileHeaderToGroup As ProfileHeaderToGroup = Me.CreateObject(dr)
                profileHeaderToGroupList.Add(profileHeaderToGroup)
            End While

            Return profileHeaderToGroupList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim profileHeaderToGroup As ProfileHeaderToGroup = CType(obj, ProfileHeaderToGroup)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, profileHeaderToGroup.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim profileHeaderToGroup As ProfileHeaderToGroup = CType(obj, ProfileHeaderToGroup)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Priority", DbType.Int32, profileHeaderToGroup.Priority)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, profileHeaderToGroup.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, profileHeaderToGroup.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ProfileGroupID", DbType.Int32, Me.GetRefObject(profileHeaderToGroup.ProfileGroup))
            DbCommandWrapper.AddInParameter("@ProfileHeaderID", DbType.Int32, Me.GetRefObject(profileHeaderToGroup.ProfileHeader))

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

            Dim profileHeaderToGroup As ProfileHeaderToGroup = CType(obj, ProfileHeaderToGroup)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, profileHeaderToGroup.ID)
            DbCommandWrapper.AddInParameter("@Priority", DbType.Int32, profileHeaderToGroup.Priority)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, profileHeaderToGroup.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, profileHeaderToGroup.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ProfileGroupID", DbType.Int32, Me.GetRefObject(profileHeaderToGroup.ProfileGroup))
            DbCommandWrapper.AddInParameter("@ProfileHeaderID", DbType.Int32, Me.GetRefObject(profileHeaderToGroup.ProfileHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ProfileHeaderToGroup

            Dim profileHeaderToGroup As ProfileHeaderToGroup = New ProfileHeaderToGroup

            profileHeaderToGroup.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Priority")) Then profileHeaderToGroup.Priority = CType(dr("Priority"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then profileHeaderToGroup.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then profileHeaderToGroup.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then profileHeaderToGroup.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then profileHeaderToGroup.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then profileHeaderToGroup.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileGroupID")) Then
                profileHeaderToGroup.ProfileGroup = New ProfileGroup(CType(dr("ProfileGroupID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileHeaderID")) Then
                profileHeaderToGroup.ProfileHeader = New ProfileHeader(CType(dr("ProfileHeaderID"), Integer))
            End If

            Return profileHeaderToGroup

        End Function

        Private Sub SetTableName()

            If Not (GetType(ProfileHeaderToGroup) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ProfileHeaderToGroup), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ProfileHeaderToGroup).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

