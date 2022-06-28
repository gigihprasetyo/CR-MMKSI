#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MessageServiceDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/21/2020 - 10:24:33 AM
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

    Public Class MessageServiceDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMessageServiceDetail"
        Private m_UpdateStatement As String = "up_UpdateMessageServiceDetail"
        Private m_RetrieveStatement As String = "up_RetrieveMessageServiceDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveMessageServiceDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMessageServiceDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim messageServiceDetail As MessageServiceDetail = Nothing
            While dr.Read

                messageServiceDetail = Me.CreateObject(dr)

            End While

            Return messageServiceDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim messageServiceDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim messageServiceDetail As MessageServiceDetail = Me.CreateObject(dr)
                messageServiceDetailList.Add(messageServiceDetail)
            End While

            Return messageServiceDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim messageServiceDetail As MessageServiceDetail = CType(obj, MessageServiceDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, messageServiceDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim messageServiceDetail As MessageServiceDetail = CType(obj, MessageServiceDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@MessageServiceID", DbType.Int32, messageServiceDetail.MessageServiceID)
            DbCommandWrapper.AddInParameter("@ChannelID", DbType.Int16, messageServiceDetail.ChannelID)
            DbCommandWrapper.AddInParameter("@Destination", DbType.AnsiString, messageServiceDetail.Destination)
            DbCommandWrapper.AddInParameter("@MessageServiceDataID", DbType.Int32, messageServiceDetail.MessageServiceDataID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, messageServiceDetail.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.StringFixedLength, messageServiceDetail.LastUpdateBy)


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

            Dim messageServiceDetail As MessageServiceDetail = CType(obj, MessageServiceDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, messageServiceDetail.ID)
            DbCommandWrapper.AddInParameter("@MessageServiceID", DbType.Int32, messageServiceDetail.MessageServiceID)
            DbCommandWrapper.AddInParameter("@ChannelID", DbType.Int16, messageServiceDetail.ChannelID)
            DbCommandWrapper.AddInParameter("@Destination", DbType.AnsiString, messageServiceDetail.Destination)
            DbCommandWrapper.AddInParameter("@MessageServiceDataID", DbType.Int32, messageServiceDetail.MessageServiceDataID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, messageServiceDetail.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, messageServiceDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.StringFixedLength, User)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MessageServiceDetail

            Dim messageServiceDetail As MessageServiceDetail = New MessageServiceDetail

            messageServiceDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MessageServiceID")) Then messageServiceDetail.MessageServiceID = CType(dr("MessageServiceID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChannelID")) Then messageServiceDetail.ChannelID = CType(dr("ChannelID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Destination")) Then messageServiceDetail.Destination = dr("Destination").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MessageServiceDataID")) Then messageServiceDetail.MessageServiceDataID = CType(dr("MessageServiceDataID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then messageServiceDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then messageServiceDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then messageServiceDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then messageServiceDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then messageServiceDetail.LastUpdateBy = dr("LastUpdateBy").ToString

            Return messageServiceDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(MessageServiceDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MessageServiceDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MessageServiceDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
