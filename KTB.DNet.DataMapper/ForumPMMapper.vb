#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ForumPM Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/4/2007 - 10:32:24 AM
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

    Public Class ForumPMMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertForumPM"
        Private m_UpdateStatement As String = "up_UpdateForumPM"
        Private m_RetrieveStatement As String = "up_RetrieveForumPM"
        Private m_RetrieveListStatement As String = "up_RetrieveForumPMList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteForumPM"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim forumPM As ForumPM = Nothing
            While dr.Read

                forumPM = Me.CreateObject(dr)

            End While

            Return forumPM

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim forumPMList As ArrayList = New ArrayList

            While dr.Read
                Dim forumPM As ForumPM = Me.CreateObject(dr)
                forumPMList.Add(forumPM)
            End While

            Return forumPMList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim forumPM As ForumPM = CType(obj, ForumPM)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, forumPM.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim forumPM As ForumPM = CType(obj, ForumPM)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, forumPM.Subject)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, forumPM.Message)
            DbCommandWrapper.AddInParameter("@UserFrom", DbType.Int32, forumPM.UserFrom)
            DbCommandWrapper.AddInParameter("@isRead", DbType.Byte, forumPM.isRead)
            DbCommandWrapper.AddInParameter("@isDeletedInbox", DbType.Byte, forumPM.isDeletedInbox)
            DbCommandWrapper.AddInParameter("@isDeletedOutBox", DbType.Byte, forumPM.isDeletedOutBox)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, forumPM.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, forumPM.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@UserInfoID", DbType.Int32, Me.GetRefObject(forumPM.UserInfo))

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

            Dim forumPM As ForumPM = CType(obj, ForumPM)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, forumPM.ID)
            DbCommandWrapper.AddInParameter("@Subject", DbType.AnsiString, forumPM.Subject)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, forumPM.Message)
            DbCommandWrapper.AddInParameter("@UserFrom", DbType.Int32, forumPM.UserFrom)
            DbCommandWrapper.AddInParameter("@isRead", DbType.Byte, forumPM.isRead)
            DbCommandWrapper.AddInParameter("@isDeletedInbox", DbType.Byte, forumPM.isDeletedInbox)
            DbCommandWrapper.AddInParameter("@isDeletedOutBox", DbType.Byte, forumPM.isDeletedOutBox)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, forumPM.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, forumPM.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@UserInfoID", DbType.Int32, Me.GetRefObject(forumPM.UserInfo))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ForumPM

            Dim forumPM As ForumPM = New ForumPM

            forumPM.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Subject")) Then forumPM.Subject = dr("Subject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Message")) Then forumPM.Message = dr("Message").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UserFrom")) Then forumPM.UserFrom = CType(dr("UserFrom"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("isRead")) Then forumPM.isRead = CType(dr("isRead"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("isDeletedInbox")) Then forumPM.isDeletedInbox = CType(dr("isDeletedInbox"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("isDeletedOutBox")) Then forumPM.isDeletedOutBox = CType(dr("isDeletedOutBox"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then forumPM.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then forumPM.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then forumPM.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then forumPM.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then forumPM.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UserInfoID")) Then
                forumPM.UserInfo = New UserInfo(CType(dr("UserInfoID"), Integer))
            End If

            Return forumPM

        End Function

        Private Sub SetTableName()

            If Not (GetType(ForumPM) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ForumPM), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ForumPM).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

