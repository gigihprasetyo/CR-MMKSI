
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceCalculationHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 29/03/2020 - 19:46:52
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

    Public Class CcCSPerformanceCalculationHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcCSPerformanceCalculationHistory"
        Private m_UpdateStatement As String = "up_UpdateCcCSPerformanceCalculationHistory"
        Private m_RetrieveStatement As String = "up_RetrieveCcCSPerformanceCalculationHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveCcCSPerformanceCalculationHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcCSPerformanceCalculationHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccCSPerformanceCalculationHistory As CcCSPerformanceCalculationHistory = Nothing
            While dr.Read

                ccCSPerformanceCalculationHistory = Me.CreateObject(dr)

            End While

            Return ccCSPerformanceCalculationHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccCSPerformanceCalculationHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim ccCSPerformanceCalculationHistory As CcCSPerformanceCalculationHistory = Me.CreateObject(dr)
                ccCSPerformanceCalculationHistoryList.Add(ccCSPerformanceCalculationHistory)
            End While

            Return ccCSPerformanceCalculationHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceCalculationHistory As CcCSPerformanceCalculationHistory = CType(obj, CcCSPerformanceCalculationHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceCalculationHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceCalculationHistory As CcCSPerformanceCalculationHistory = CType(obj, CcCSPerformanceCalculationHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceMasterID", DbType.Int32, ccCSPerformanceCalculationHistory.CcCSPerformanceMaster.ID)
            DbCommandWrapper.AddInParameter("@CcPeriodID", DbType.Int32, ccCSPerformanceCalculationHistory.CcPeriod.ID)
            DbCommandWrapper.AddInParameter("@ClusterID", DbType.Int32, ccCSPerformanceCalculationHistory.CcCSPerformanceCluster.ID)
            DbCommandWrapper.AddInParameter("@RequestedDate", DbType.DateTime, ccCSPerformanceCalculationHistory.RequestedDate)
            DbCommandWrapper.AddInParameter("@ProcessedDate", DbType.DateTime, ccCSPerformanceCalculationHistory.ProcessedDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceCalculationHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccCSPerformanceCalculationHistory.LastUpdateBy)
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

            Dim ccCSPerformanceCalculationHistory As CcCSPerformanceCalculationHistory = CType(obj, CcCSPerformanceCalculationHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceCalculationHistory.ID)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceMasterID", DbType.Int32, ccCSPerformanceCalculationHistory.CcCSPerformanceMaster.ID)
            DbCommandWrapper.AddInParameter("@CcPeriodID", DbType.Int32, ccCSPerformanceCalculationHistory.CcPeriod.ID)
            DbCommandWrapper.AddInParameter("@ClusterID", DbType.Int32, ccCSPerformanceCalculationHistory.CcCSPerformanceCluster.ID)
            DbCommandWrapper.AddInParameter("@RequestedDate", DbType.DateTime, ccCSPerformanceCalculationHistory.RequestedDate)
            DbCommandWrapper.AddInParameter("@ProcessedDate", DbType.DateTime, ccCSPerformanceCalculationHistory.ProcessedDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceCalculationHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccCSPerformanceCalculationHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcCSPerformanceCalculationHistory

            Dim ccCSPerformanceCalculationHistory As CcCSPerformanceCalculationHistory = New CcCSPerformanceCalculationHistory

            ccCSPerformanceCalculationHistory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceMasterID")) Then ccCSPerformanceCalculationHistory.CcCSPerformanceMaster = New CcCSPerformanceMaster(CType(dr("CcCSPerformanceMasterID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("CcPeriodID")) Then ccCSPerformanceCalculationHistory.CcPeriod = New CcPeriod(CType(dr("CcPeriodID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("ClusterID")) Then ccCSPerformanceCalculationHistory.CcCSPerformanceCluster = New CcCSPerformanceCluster(CType(dr("ClusterID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("RequestedDate")) Then ccCSPerformanceCalculationHistory.RequestedDate = CType(dr("RequestedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessedDate")) Then ccCSPerformanceCalculationHistory.ProcessedDate = CType(dr("ProcessedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccCSPerformanceCalculationHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccCSPerformanceCalculationHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccCSPerformanceCalculationHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccCSPerformanceCalculationHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccCSPerformanceCalculationHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return ccCSPerformanceCalculationHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcCSPerformanceCalculationHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcCSPerformanceCalculationHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcCSPerformanceCalculationHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

