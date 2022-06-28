
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistUploadLog Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/2/2018 - 1:18:57 PM
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

    Public Class AssistUploadLogMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAssistUploadLog"
        Private m_UpdateStatement As String = "up_UpdateAssistUploadLog"
        Private m_RetrieveStatement As String = "up_RetrieveAssistUploadLog"
        Private m_RetrieveListStatement As String = "up_RetrieveAssistUploadLogList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAssistUploadLog"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim assistUploadLog As AssistUploadLog = Nothing
            While dr.Read

                assistUploadLog = Me.CreateObject(dr)

            End While

            Return assistUploadLog

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim assistUploadLogList As ArrayList = New ArrayList

            While dr.Read
                Dim assistUploadLog As AssistUploadLog = Me.CreateObject(dr)
                assistUploadLogList.Add(assistUploadLog)
            End While

            Return assistUploadLogList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistUploadLog As AssistUploadLog = CType(obj, AssistUploadLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistUploadLog.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistUploadLog As AssistUploadLog = CType(obj, AssistUploadLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistUploadLog.Dealer))
            DbCommandWrapper.AddInParameter("@ModuleID", DbType.Int32, Me.GetRefObject(assistUploadLog.AssistModule))
            DbCommandWrapper.AddInParameter("@Month", DbType.Int32, assistUploadLog.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int32, assistUploadLog.Year)
            DbCommandWrapper.AddInParameter("@ErrorRatio", DbType.Decimal, assistUploadLog.ErrorRatio)
            DbCommandWrapper.AddInParameter("@Performance", DbType.Decimal, assistUploadLog.Performance)
            DbCommandWrapper.AddInParameter("@UploadTime", DbType.DateTime, assistUploadLog.UploadTime)
            DbCommandWrapper.AddInParameter("@OriginalFileName", DbType.AnsiString, assistUploadLog.OriginalFileName)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, assistUploadLog.FileName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, assistUploadLog.Status)
            DbCommandWrapper.AddInParameter("@ValidateStatus", DbType.Int16, assistUploadLog.ValidateStatus)
            DbCommandWrapper.AddInParameter("@ErrorMessage", DbType.AnsiString, assistUploadLog.ErrorMessage)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistUploadLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistUploadLog.LastUpdateBy)
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

            Dim assistUploadLog As AssistUploadLog = CType(obj, AssistUploadLog)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistUploadLog.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistUploadLog.Dealer))
            DbCommandWrapper.AddInParameter("@ModuleID", DbType.Int32, Me.GetRefObject(assistUploadLog.AssistModule))
            DbCommandWrapper.AddInParameter("@Month", DbType.Int32, assistUploadLog.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int32, assistUploadLog.Year)
            DbCommandWrapper.AddInParameter("@ErrorRatio", DbType.Decimal, assistUploadLog.ErrorRatio)
            DbCommandWrapper.AddInParameter("@Performance", DbType.Decimal, assistUploadLog.Performance)
            DbCommandWrapper.AddInParameter("@UploadTime", DbType.DateTime, assistUploadLog.UploadTime)
            DbCommandWrapper.AddInParameter("@OriginalFileName", DbType.AnsiString, assistUploadLog.OriginalFileName)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, assistUploadLog.FileName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, assistUploadLog.Status)
            DbCommandWrapper.AddInParameter("@ValidateStatus", DbType.Int16, assistUploadLog.ValidateStatus)
            DbCommandWrapper.AddInParameter("@ErrorMessage", DbType.AnsiString, assistUploadLog.ErrorMessage)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistUploadLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, assistUploadLog.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AssistUploadLog

            Dim assistUploadLog As AssistUploadLog = New AssistUploadLog

            assistUploadLog.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Month")) Then assistUploadLog.Month = CType(dr("Month"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ModuleID")) Then assistUploadLog.ModuleID = CType(dr("ModuleID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then assistUploadLog.Year = CType(dr("Year"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ErrorRatio")) Then assistUploadLog.ErrorRatio = CType(dr("ErrorRatio"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Performance")) Then assistUploadLog.Performance = CType(dr("Performance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadTime")) Then assistUploadLog.UploadTime = CType(dr("UploadTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OriginalFileName")) Then assistUploadLog.OriginalFileName = dr("OriginalFileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then assistUploadLog.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then assistUploadLog.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateStatus")) Then assistUploadLog.ValidateStatus = CType(dr("ValidateStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ErrorMessage")) Then assistUploadLog.ErrorMessage = dr("ErrorMessage").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then assistUploadLog.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then assistUploadLog.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then assistUploadLog.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then assistUploadLog.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then assistUploadLog.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                assistUploadLog.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ModuleID")) Then
                assistUploadLog.AssistModule = New AssistModule(CType(dr("ModuleID"), Integer))
            End If

            Return assistUploadLog

        End Function

        Private Sub SetTableName()

            If Not (GetType(AssistUploadLog) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AssistUploadLog), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AssistUploadLog).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

