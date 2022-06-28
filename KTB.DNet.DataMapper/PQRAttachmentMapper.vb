#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRAttachment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2007 - 2:35:03 PM
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

    Public Class PQRAttachmentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPQRAttachment"
        Private m_UpdateStatement As String = "up_UpdatePQRAttachment"
        Private m_RetrieveStatement As String = "up_RetrievePQRAttachment"
        Private m_RetrieveListStatement As String = "up_RetrievePQRAttachmentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePQRAttachment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pQRAttachment As PQRAttachment = Nothing
            While dr.Read

                pQRAttachment = Me.CreateObject(dr)

            End While

            Return pQRAttachment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pQRAttachmentList As ArrayList = New ArrayList

            While dr.Read
                Dim pQRAttachment As PQRAttachment = Me.CreateObject(dr)
                pQRAttachmentList.Add(pQRAttachment)
            End While

            Return pQRAttachmentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pQRAttachment As PQRAttachment = CType(obj, PQRAttachment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pQRAttachment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pQRAttachment As PQRAttachment = CType(obj, PQRAttachment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int32, pQRAttachment.Type)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, pQRAttachment.Message)
            DbCommandWrapper.AddInParameter("@AttachmentType", DbType.AnsiString, pQRAttachment.AttachmentType)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, pQRAttachment.Attachment)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pQRAttachment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pQRAttachment.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PQRHeaderID", DbType.Int32, Me.GetRefObject(pQRAttachment.PQRHeader))

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

            Dim pQRAttachment As PQRAttachment = CType(obj, PQRAttachment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pQRAttachment.ID)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int32, pQRAttachment.Type)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, pQRAttachment.Message)
            DbCommandWrapper.AddInParameter("@AttachmentType", DbType.AnsiString, pQRAttachment.AttachmentType)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, pQRAttachment.Attachment)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pQRAttachment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pQRAttachment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@PQRHeaderID", DbType.Int32, Me.GetRefObject(pQRAttachment.PQRHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PQRAttachment

            Dim pQRAttachment As PQRAttachment = New PQRAttachment

            pQRAttachment.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then pQRAttachment.Type = CType(dr("Type"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Message")) Then pQRAttachment.Message = dr("Message").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AttachmentType")) Then pQRAttachment.AttachmentType = dr("AttachmentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then pQRAttachment.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pQRAttachment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pQRAttachment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pQRAttachment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pQRAttachment.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pQRAttachment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRHeaderID")) Then
                pQRAttachment.PQRHeader = New PQRHeader(CType(dr("PQRHeaderID"), Integer))
            End If

            Return pQRAttachment

        End Function

        Private Sub SetTableName()

            If Not (GetType(PQRAttachment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PQRAttachment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PQRAttachment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

