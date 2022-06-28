
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WsLog Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 28/10/2016 - 08:54:24
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

    Public Class WsLogMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWsLog"
        Private m_UpdateStatement As String = "up_UpdateWsLog"
        Private m_RetrieveStatement As String = "up_RetrieveWsLog"
        Private m_RetrieveListStatement As String = "up_RetrieveWsLogList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWsLog"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim wsLog As WsLog = Nothing
            While dr.Read

                wsLog = Me.CreateObject(dr)

            End While

            Return wsLog

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim wsLogList As ArrayList = New ArrayList

            While dr.Read
                Dim wsLog As WsLog = Me.CreateObject(dr)
                wsLogList.Add(wsLog)
            End While

            Return wsLogList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wsLog As WsLog = CType(obj, WsLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wsLog.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wsLog As WsLog = CType(obj, WsLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Source", DbType.AnsiString, wsLog.Source)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, wsLog.Status)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, wsLog.Message)
            DbCommandWrapper.AddInParameter("@Body", DbType.AnsiString, wsLog.Body)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wsLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, wsLog.CreatedTime)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, wsLog.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, wsLog.LastUpdatedTime)


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

            Dim wsLog As WsLog = CType(obj, WsLog)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wsLog.ID)
            DbCommandWrapper.AddInParameter("@Source", DbType.AnsiString, wsLog.Source)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, wsLog.Status)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, wsLog.Message)
            DbCommandWrapper.AddInParameter("@Body", DbType.AnsiString, wsLog.Body)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wsLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, wsLog.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, wsLog.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WsLog

            Dim wsLog As WsLog = New WsLog

            wsLog.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Source")) Then wsLog.Source = dr("Source").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then wsLog.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Message")) Then wsLog.Message = dr("Message").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Body")) Then wsLog.Body = dr("Body").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then wsLog.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then wsLog.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then wsLog.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then wsLog.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then wsLog.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            Return wsLog

        End Function

        Private Sub SetTableName()

            If Not (GetType(WsLog) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WsLog), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WsLog).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

