
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PerformanceLog Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 2/7/2013 - 10:12:48 AM
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

    Public Class PerformanceLogMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPerformanceLog"
        Private m_UpdateStatement As String = "up_UpdatePerformanceLog"
        Private m_RetrieveStatement As String = "up_RetrievePerformanceLog"
        Private m_RetrieveListStatement As String = "up_RetrievePerformanceLogList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePerformanceLog"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim performanceLog As PerformanceLog = Nothing
            While dr.Read

                performanceLog = Me.CreateObject(dr)

            End While

            Return performanceLog

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim performanceLogList As ArrayList = New ArrayList

            While dr.Read
                Dim performanceLog As PerformanceLog = Me.CreateObject(dr)
                performanceLogList.Add(performanceLog)
            End While

            Return performanceLogList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim performanceLog As PerformanceLog = CType(obj, PerformanceLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, performanceLog.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim performanceLog As PerformanceLog = CType(obj, PerformanceLog)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Modul", DbType.AnsiString, performanceLog.Modul)
            DbCommandWrapper.AddInParameter("@ModulID", DbType.Int32, performanceLog.ModulID)
            DbCommandWrapper.AddInParameter("@Action", DbType.Int16, performanceLog.Action)
            DbCommandWrapper.AddInParameter("@Time1", DbType.Int32, performanceLog.Time1)
            DbCommandWrapper.AddInParameter("@Time2", DbType.Int32, performanceLog.Time2)
            DbCommandWrapper.AddInParameter("@Time3", DbType.Int32, performanceLog.Time3)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, performanceLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, performanceLog.LastUpdateBy)
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

            Dim performanceLog As PerformanceLog = CType(obj, PerformanceLog)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, performanceLog.ID)
            DbCommandWrapper.AddInParameter("@Modul", DbType.AnsiString, performanceLog.Modul)
            DbCommandWrapper.AddInParameter("@ModulID", DbType.Int32, performanceLog.ModulID)
            DbCommandWrapper.AddInParameter("@Action", DbType.Int16, performanceLog.Action)
            DbCommandWrapper.AddInParameter("@Time1", DbType.Int32, performanceLog.Time1)
            DbCommandWrapper.AddInParameter("@Time2", DbType.Int32, performanceLog.Time2)
            DbCommandWrapper.AddInParameter("@Time3", DbType.Int32, performanceLog.Time3)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, performanceLog.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, performanceLog.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PerformanceLog

            Dim performanceLog As PerformanceLog = New PerformanceLog

            performanceLog.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Modul")) Then performanceLog.Modul = dr("Modul").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ModulID")) Then performanceLog.ModulID = CType(dr("ModulID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Action")) Then performanceLog.Action = CType(dr("Action"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Time1")) Then performanceLog.Time1 = CType(dr("Time1"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Time2")) Then performanceLog.Time2 = CType(dr("Time2"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Time3")) Then performanceLog.Time3 = CType(dr("Time3"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then performanceLog.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then performanceLog.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then performanceLog.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then performanceLog.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then performanceLog.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return performanceLog

        End Function

        Private Sub SetTableName()

            If Not (GetType(PerformanceLog) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PerformanceLog), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PerformanceLog).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

