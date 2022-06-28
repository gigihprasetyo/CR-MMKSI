#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceReminderAnomaly Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/17/2020 - 6:15:18 PM
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

    Public Class ServiceReminderAnomalyMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceReminderAnomaly"
        Private m_UpdateStatement As String = "up_UpdateServiceReminderAnomaly"
        Private m_RetrieveStatement As String = "up_RetrieveServiceReminderAnomaly"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceReminderAnomalyList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceReminderAnomaly"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim serviceReminderAnomaly As ServiceReminderAnomaly = Nothing
            While dr.Read

                serviceReminderAnomaly = Me.CreateObject(dr)

            End While

            Return serviceReminderAnomaly

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim serviceReminderAnomalyList As ArrayList = New ArrayList

            While dr.Read
                Dim serviceReminderAnomaly As ServiceReminderAnomaly = Me.CreateObject(dr)
                serviceReminderAnomalyList.Add(serviceReminderAnomaly)
            End While

            Return serviceReminderAnomalyList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceReminderAnomaly As ServiceReminderAnomaly = CType(obj, ServiceReminderAnomaly)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceReminderAnomaly.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceReminderAnomaly As ServiceReminderAnomaly = CType(obj, ServiceReminderAnomaly)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, serviceReminderAnomaly.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, serviceReminderAnomaly.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, serviceReminderAnomaly.PMKindID)
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int32, serviceReminderAnomaly.FSKindID)
            DbCommandWrapper.AddInParameter("@LinearRegressionDate", DbType.DateTime, serviceReminderAnomaly.LinearRegressionDate)
            DbCommandWrapper.AddInParameter("@PredictionDate", DbType.DateTime, serviceReminderAnomaly.PredictionDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, serviceReminderAnomaly.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceReminderAnomaly.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, serviceReminderAnomaly.LastUpdateBy)
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

            Dim serviceReminderAnomaly As ServiceReminderAnomaly = CType(obj, ServiceReminderAnomaly)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceReminderAnomaly.ID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, serviceReminderAnomaly.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, serviceReminderAnomaly.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, serviceReminderAnomaly.PMKindID)
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int32, serviceReminderAnomaly.FSKindID)
            DbCommandWrapper.AddInParameter("@LinearRegressionDate", DbType.DateTime, serviceReminderAnomaly.LinearRegressionDate)
            DbCommandWrapper.AddInParameter("@PredictionDate", DbType.DateTime, serviceReminderAnomaly.PredictionDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, serviceReminderAnomaly.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceReminderAnomaly.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, serviceReminderAnomaly.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceReminderAnomaly

            Dim serviceReminderAnomaly As ServiceReminderAnomaly = New ServiceReminderAnomaly

            serviceReminderAnomaly.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then serviceReminderAnomaly.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then serviceReminderAnomaly.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PMKindID")) Then serviceReminderAnomaly.PMKindID = CType(dr("PMKindID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FSKindID")) Then serviceReminderAnomaly.FSKindID = CType(dr("FSKindID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LinearRegressionDate")) Then serviceReminderAnomaly.LinearRegressionDate = CType(dr("LinearRegressionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PredictionDate")) Then serviceReminderAnomaly.PredictionDate = CType(dr("PredictionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then serviceReminderAnomaly.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then serviceReminderAnomaly.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then serviceReminderAnomaly.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then serviceReminderAnomaly.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then serviceReminderAnomaly.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then serviceReminderAnomaly.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return serviceReminderAnomaly

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceReminderAnomaly) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceReminderAnomaly), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceReminderAnomaly).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
