
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DRReportRankingStatus Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 10/15/2012 - 10:34:12 AM
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

    Public Class DRReportRankingStatusMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDRReportRankingStatus"
        Private m_UpdateStatement As String = "up_UpdateDRReportRankingStatus"
        Private m_RetrieveStatement As String = "up_RetrieveDRReportRankingStatus"
        Private m_RetrieveListStatement As String = "up_RetrieveDRReportRankingStatusList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDRReportRankingStatus"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dRReportRankingStatus As DRReportRankingStatus = Nothing
            While dr.Read

                dRReportRankingStatus = Me.CreateObject(dr)

            End While

            Return dRReportRankingStatus

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dRReportRankingStatusList As ArrayList = New ArrayList

            While dr.Read
                Dim dRReportRankingStatus As DRReportRankingStatus = Me.CreateObject(dr)
                dRReportRankingStatusList.Add(dRReportRankingStatus)
            End While

            Return dRReportRankingStatusList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dRReportRankingStatus As DRReportRankingStatus = CType(obj, DRReportRankingStatus)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dRReportRankingStatus.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dRReportRankingStatus As DRReportRankingStatus = CType(obj, DRReportRankingStatus)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PeriodFrom", DbType.DateTime, dRReportRankingStatus.PeriodFrom)
            DbCommandWrapper.AddInParameter("@PeriodTo", DbType.DateTime, dRReportRankingStatus.PeriodTo)
            DbCommandWrapper.AddInParameter("@ReportStatus", DbType.Int16, dRReportRankingStatus.ReportStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dRReportRankingStatus.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dRReportRankingStatus.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CcReportMasterID", DbType.Int32, Me.GetRefObject(dRReportRankingStatus.CcReportMaster))

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

            Dim dRReportRankingStatus As DRReportRankingStatus = CType(obj, DRReportRankingStatus)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dRReportRankingStatus.ID)
            DbCommandWrapper.AddInParameter("@PeriodFrom", DbType.DateTime, dRReportRankingStatus.PeriodFrom)
            DbCommandWrapper.AddInParameter("@PeriodTo", DbType.DateTime, dRReportRankingStatus.PeriodTo)
            DbCommandWrapper.AddInParameter("@ReportStatus", DbType.Int16, dRReportRankingStatus.ReportStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dRReportRankingStatus.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dRReportRankingStatus.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CcReportMasterID", DbType.Int32, Me.GetRefObject(dRReportRankingStatus.CcReportMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DRReportRankingStatus

            Dim dRReportRankingStatus As DRReportRankingStatus = New DRReportRankingStatus

            dRReportRankingStatus.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodFrom")) Then dRReportRankingStatus.PeriodFrom = CType(dr("PeriodFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodTo")) Then dRReportRankingStatus.PeriodTo = CType(dr("PeriodTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReportStatus")) Then dRReportRankingStatus.ReportStatus = CType(dr("ReportStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dRReportRankingStatus.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dRReportRankingStatus.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dRReportRankingStatus.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dRReportRankingStatus.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dRReportRankingStatus.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CcReportMasterID")) Then
                dRReportRankingStatus.CcReportMaster = New CcReportMaster(CType(dr("CcReportMasterID"), Integer))
            End If

            Return dRReportRankingStatus

        End Function

        Private Sub SetTableName()

            If Not (GetType(DRReportRankingStatus) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DRReportRankingStatus), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DRReportRankingStatus).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

