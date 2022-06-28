
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceCluster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/03/2020 - 8:46:17
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

    Public Class CcCSPerformanceClusterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcCSPerformanceCluster"
        Private m_UpdateStatement As String = "up_UpdateCcCSPerformanceCluster"
        Private m_RetrieveStatement As String = "up_RetrieveCcCSPerformanceCluster"
        Private m_RetrieveListStatement As String = "up_RetrieveCcCSPerformanceClusterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcCSPerformanceCluster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccCSPerformanceCluster As CcCSPerformanceCluster = Nothing
            While dr.Read

                ccCSPerformanceCluster = Me.CreateObject(dr)

            End While

            Return ccCSPerformanceCluster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccCSPerformanceClusterList As ArrayList = New ArrayList

            While dr.Read
                Dim ccCSPerformanceCluster As CcCSPerformanceCluster = Me.CreateObject(dr)
                ccCSPerformanceClusterList.Add(ccCSPerformanceCluster)
            End While

            Return ccCSPerformanceClusterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceCluster As CcCSPerformanceCluster = CType(obj, CcCSPerformanceCluster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceCluster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceCluster As CcCSPerformanceCluster = CType(obj, CcCSPerformanceCluster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceMasterID", DbType.Int32, ccCSPerformanceCluster.CcCSPerformanceMaster.ID)
            DbCommandWrapper.AddInParameter("@ClusterName", DbType.AnsiString, ccCSPerformanceCluster.ClusterName)
            DbCommandWrapper.AddInParameter("@StartPeriodCal", DbType.Int32, ccCSPerformanceCluster.StartPeriodCal)
            DbCommandWrapper.AddInParameter("@EndPeriodCal", DbType.Int32, ccCSPerformanceCluster.EndPeriodCal)
            DbCommandWrapper.AddInParameter("@MinPoint", DbType.Int32, ccCSPerformanceCluster.MinPoint)
            DbCommandWrapper.AddInParameter("@MaxPoint", DbType.Int32, ccCSPerformanceCluster.MaxPoint)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, ccCSPerformanceCluster.VehicleType)
            DbCommandWrapper.AddInParameter("@DealerType", DbType.AnsiString, ccCSPerformanceCluster.DealerType)
            DbCommandWrapper.AddInParameter("@TypeCal", DbType.Int16, ccCSPerformanceCluster.TypeCal)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceCluster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccCSPerformanceCluster.LastUpdateBy)
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

            Dim ccCSPerformanceCluster As CcCSPerformanceCluster = CType(obj, CcCSPerformanceCluster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceCluster.ID)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceMasterID", DbType.Int32, ccCSPerformanceCluster.CcCSPerformanceMaster.ID)
            DbCommandWrapper.AddInParameter("@ClusterName", DbType.AnsiString, ccCSPerformanceCluster.ClusterName)
            DbCommandWrapper.AddInParameter("@StartPeriodCal", DbType.Int32, ccCSPerformanceCluster.StartPeriodCal)
            DbCommandWrapper.AddInParameter("@EndPeriodCal", DbType.Int32, ccCSPerformanceCluster.EndPeriodCal)
            DbCommandWrapper.AddInParameter("@MinPoint", DbType.Int32, ccCSPerformanceCluster.MinPoint)
            DbCommandWrapper.AddInParameter("@MaxPoint", DbType.Int32, ccCSPerformanceCluster.MaxPoint)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, ccCSPerformanceCluster.VehicleType)
            DbCommandWrapper.AddInParameter("@DealerType", DbType.AnsiString, ccCSPerformanceCluster.DealerType)
            DbCommandWrapper.AddInParameter("@TypeCal", DbType.Int16, ccCSPerformanceCluster.TypeCal)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceCluster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccCSPerformanceCluster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcCSPerformanceCluster

            Dim ccCSPerformanceCluster As CcCSPerformanceCluster = New CcCSPerformanceCluster

            ccCSPerformanceCluster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceMasterID")) Then ccCSPerformanceCluster.CcCSPerformanceMaster = New CcCSPerformanceMaster(CType(dr("CcCSPerformanceMasterID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("ClusterName")) Then ccCSPerformanceCluster.ClusterName = dr("ClusterName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleType")) Then ccCSPerformanceCluster.VehicleType = dr("VehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerType")) Then ccCSPerformanceCluster.DealerType = dr("DealerType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartPeriodCal")) Then ccCSPerformanceCluster.StartPeriodCal = CType(dr("StartPeriodCal"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EndPeriodCal")) Then ccCSPerformanceCluster.EndPeriodCal = CType(dr("EndPeriodCal"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MinPoint")) Then ccCSPerformanceCluster.MinPoint = CType(dr("MinPoint"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxPoint")) Then ccCSPerformanceCluster.MaxPoint = CType(dr("MaxPoint"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TypeCal")) Then ccCSPerformanceCluster.TypeCal = CType(dr("TypeCal"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccCSPerformanceCluster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccCSPerformanceCluster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccCSPerformanceCluster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccCSPerformanceCluster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccCSPerformanceCluster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return ccCSPerformanceCluster

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcCSPerformanceCluster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcCSPerformanceCluster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcCSPerformanceCluster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

