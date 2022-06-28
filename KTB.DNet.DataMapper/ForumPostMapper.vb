#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ForumPost Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/28/2007 - 9:37:51 AM
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

    Public Class ForumPostMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertForumPost"
        Private m_UpdateStatement As String = "up_UpdateForumPost"
        Private m_RetrieveStatement As String = "up_RetrieveForumPost"
        Private m_RetrieveListStatement As String = "up_RetrieveForumPostList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteForumPost"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim forumPost As ForumPost = Nothing
            While dr.Read

                forumPost = Me.CreateObject(dr)

            End While

            Return forumPost

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim forumPostList As ArrayList = New ArrayList

            While dr.Read
                Dim forumPost As ForumPost = Me.CreateObject(dr)
                forumPostList.Add(forumPost)
            End While

            Return forumPostList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim forumPost As ForumPost = CType(obj, ForumPost)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, forumPost.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim forumPost As ForumPost = CType(obj, ForumPost)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, forumPost.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, forumPost.Attachment)
            DbCommandWrapper.AddInParameter("@isHeader", DbType.Byte, forumPost.isHeader)
            DbCommandWrapper.AddInParameter("@UserEntry", DbType.AnsiString, forumPost.UserEntry)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, forumPost.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, forumPost.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@UserID", DbType.Int32, Me.GetRefObject(forumPost.UserInfo))
            DbCommandWrapper.AddInParameter("@ForumTopicID", DbType.Int32, Me.GetRefObject(forumPost.ForumTopic))

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

            Dim forumPost As ForumPost = CType(obj, ForumPost)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, forumPost.ID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, forumPost.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, forumPost.Attachment)
            DbCommandWrapper.AddInParameter("@isHeader", DbType.Byte, forumPost.isHeader)
            DbCommandWrapper.AddInParameter("@UserEntry", DbType.AnsiString, forumPost.UserEntry)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, forumPost.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, forumPost.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@UserID", DbType.Int32, Me.GetRefObject(forumPost.UserInfo))
            DbCommandWrapper.AddInParameter("@ForumTopicID", DbType.Int32, Me.GetRefObject(forumPost.ForumTopic))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ForumPost

            Dim forumPost As ForumPost = New ForumPost

            forumPost.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then forumPost.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then forumPost.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("isHeader")) Then forumPost.isHeader = CType(dr("isHeader"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("UserEntry")) Then forumPost.UserEntry = dr("UserEntry").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then forumPost.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then forumPost.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then forumPost.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then forumPost.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then forumPost.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UserID")) Then
                forumPost.UserInfo = New UserInfo(CType(dr("UserID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ForumTopicID")) Then
                forumPost.ForumTopic = New ForumTopic(CType(dr("ForumTopicID"), Integer))
            End If

            Return forumPost

        End Function

        Private Sub SetTableName()

            If Not (GetType(ForumPost) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ForumPost), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ForumPost).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

