
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BulletinCategoryUserGroupDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/5/2008 - 10:43:27 AM
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

    Public Class BulletinCategoryUserGroupDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBulletinCategoryUserGroupDetail"
        Private m_UpdateStatement As String = "up_UpdateBulletinCategoryUserGroupDetail"
        Private m_RetrieveStatement As String = "up_RetrieveBulletinCategoryUserGroupDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveBulletinCategoryUserGroupDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBulletinCategoryUserGroupDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim bulletinCategoryUserGroupDetail As BulletinCategoryUserGroupDetail = Nothing
            While dr.Read

                bulletinCategoryUserGroupDetail = Me.CreateObject(dr)

            End While

            Return bulletinCategoryUserGroupDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim bulletinCategoryUserGroupDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim bulletinCategoryUserGroupDetail As BulletinCategoryUserGroupDetail = Me.CreateObject(dr)
                bulletinCategoryUserGroupDetailList.Add(bulletinCategoryUserGroupDetail)
            End While

            Return bulletinCategoryUserGroupDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bulletinCategoryUserGroupDetail As BulletinCategoryUserGroupDetail = CType(obj, BulletinCategoryUserGroupDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bulletinCategoryUserGroupDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bulletinCategoryUserGroupDetail As BulletinCategoryUserGroupDetail = CType(obj, BulletinCategoryUserGroupDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bulletinCategoryUserGroupDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, bulletinCategoryUserGroupDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BuletinCategoryID", DbType.Int32, Me.GetRefObject(bulletinCategoryUserGroupDetail.BuletinCategory))
            DbCommandWrapper.AddInParameter("@UserGroupID", DbType.Int32, Me.GetRefObject(bulletinCategoryUserGroupDetail.UserGroup))

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

            Dim bulletinCategoryUserGroupDetail As BulletinCategoryUserGroupDetail = CType(obj, BulletinCategoryUserGroupDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bulletinCategoryUserGroupDetail.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bulletinCategoryUserGroupDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, bulletinCategoryUserGroupDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BuletinCategoryID", DbType.Int32, Me.GetRefObject(bulletinCategoryUserGroupDetail.BuletinCategory))
            DbCommandWrapper.AddInParameter("@UserGroupID", DbType.Int32, Me.GetRefObject(bulletinCategoryUserGroupDetail.UserGroup))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BulletinCategoryUserGroupDetail

            Dim bulletinCategoryUserGroupDetail As BulletinCategoryUserGroupDetail = New BulletinCategoryUserGroupDetail

            bulletinCategoryUserGroupDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then bulletinCategoryUserGroupDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then bulletinCategoryUserGroupDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then bulletinCategoryUserGroupDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then bulletinCategoryUserGroupDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then bulletinCategoryUserGroupDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BuletinCategoryID")) Then
                bulletinCategoryUserGroupDetail.BuletinCategory = New BuletinCategory(CType(dr("BuletinCategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("UserGroupID")) Then
                bulletinCategoryUserGroupDetail.UserGroup = New UserGroup(CType(dr("UserGroupID"), Integer))
            End If

            Return bulletinCategoryUserGroupDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(BulletinCategoryUserGroupDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BulletinCategoryUserGroupDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BulletinCategoryUserGroupDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

