
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BulletinUserGroupDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/5/2008 - 11:46:42 AM
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

    Public Class BulletinUserGroupDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBulletinUserGroupDetail"
        Private m_UpdateStatement As String = "up_UpdateBulletinUserGroupDetail"
        Private m_RetrieveStatement As String = "up_RetrieveBulletinUserGroupDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveBulletinUserGroupDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBulletinUserGroupDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim bulletinUserGroupDetail As BulletinUserGroupDetail = Nothing
            While dr.Read

                bulletinUserGroupDetail = Me.CreateObject(dr)

            End While

            Return bulletinUserGroupDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim bulletinUserGroupDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim bulletinUserGroupDetail As BulletinUserGroupDetail = Me.CreateObject(dr)
                bulletinUserGroupDetailList.Add(bulletinUserGroupDetail)
            End While

            Return bulletinUserGroupDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bulletinUserGroupDetail As BulletinUserGroupDetail = CType(obj, BulletinUserGroupDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bulletinUserGroupDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bulletinUserGroupDetail As BulletinUserGroupDetail = CType(obj, BulletinUserGroupDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bulletinUserGroupDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, bulletinUserGroupDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BuletinCategoryID", DbType.Int32, Me.GetRefObject(bulletinUserGroupDetail.BuletinCategory))
            DbCommandWrapper.AddInParameter("@UserGroupID", DbType.Int32, Me.GetRefObject(bulletinUserGroupDetail.UserGroup))

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

            Dim bulletinUserGroupDetail As BulletinUserGroupDetail = CType(obj, BulletinUserGroupDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bulletinUserGroupDetail.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bulletinUserGroupDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, bulletinUserGroupDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BuletinCategoryID", DbType.Int32, Me.GetRefObject(bulletinUserGroupDetail.BuletinCategory))
            DbCommandWrapper.AddInParameter("@UserGroupID", DbType.Int32, Me.GetRefObject(bulletinUserGroupDetail.UserGroup))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BulletinUserGroupDetail

            Dim bulletinUserGroupDetail As BulletinUserGroupDetail = New BulletinUserGroupDetail

            bulletinUserGroupDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then bulletinUserGroupDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then bulletinUserGroupDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then bulletinUserGroupDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then bulletinUserGroupDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then bulletinUserGroupDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BuletinCategoryID")) Then
                bulletinUserGroupDetail.BuletinCategory = New BuletinCategory(CType(dr("BuletinCategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("UserGroupID")) Then
                bulletinUserGroupDetail.UserGroup = New UserGroup(CType(dr("UserGroupID"), Integer))
            End If

            Return bulletinUserGroupDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(BulletinUserGroupDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BulletinUserGroupDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BulletinUserGroupDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

