#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SMSHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 1/12/2007 - 8:39:28 AM
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

    Public Class SMSHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase()
            SetTableName()
        End Sub

        Public Sub New(ByVal instanceName As String)
            Db = DatabaseFactory.CreateDatabase(instanceName)
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSMSHistory"
        Private m_UpdateStatement As String = "up_UpdateSMSHistory"
        Private m_RetrieveStatement As String = "up_RetrieveSMSHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveSMSHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSMSHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sMSHistory As SMSHistory = Nothing
            While dr.Read

                sMSHistory = Me.CreateObject(dr)

            End While

            Return sMSHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sMSHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim sMSHistory As SMSHistory = Me.CreateObject(dr)
                sMSHistoryList.Add(sMSHistory)
            End While

            Return sMSHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sMSHistory As SMSHistory = CType(obj, SMSHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sMSHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sMSHistory As SMSHistory = CType(obj, SMSHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SMSNumber", DbType.AnsiString, sMSHistory.SMSNumber)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, sMSHistory.Message)
            DbCommandWrapper.AddInParameter("@Keyword", DbType.AnsiString, sMSHistory.Keyword)
            DbCommandWrapper.AddInParameter("@ProcessingTime", DbType.DateTime, sMSHistory.ProcessingTime)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, sMSHistory.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sMSHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sMSHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim sMSHistory As SMSHistory = CType(obj, SMSHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sMSHistory.ID)
            DbCommandWrapper.AddInParameter("@SMSNumber", DbType.AnsiString, sMSHistory.SMSNumber)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, sMSHistory.Message)
            DbCommandWrapper.AddInParameter("@Keyword", DbType.AnsiString, sMSHistory.Keyword)
            DbCommandWrapper.AddInParameter("@ProcessingTime", DbType.DateTime, sMSHistory.ProcessingTime)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, sMSHistory.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sMSHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sMSHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SMSHistory

            Dim sMSHistory As SMSHistory = New SMSHistory

            sMSHistory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SMSNumber")) Then sMSHistory.SMSNumber = dr("SMSNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Message")) Then sMSHistory.Message = dr("Message").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Keyword")) Then sMSHistory.Keyword = dr("Keyword").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessingTime")) Then sMSHistory.ProcessingTime = CType(dr("ProcessingTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sMSHistory.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sMSHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sMSHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sMSHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sMSHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sMSHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sMSHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(SMSHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SMSHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SMSHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

