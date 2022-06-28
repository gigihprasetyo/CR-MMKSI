
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DRReportRanking Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 10/15/2012 - 10:33:32 AM
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

    Public Class DRReportRankingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDRReportRanking"
        Private m_UpdateStatement As String = "up_UpdateDRReportRanking"
        Private m_RetrieveStatement As String = "up_RetrieveDRReportRanking"
        Private m_RetrieveListStatement As String = "up_RetrieveDRReportRankingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDRReportRanking"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dRReportRanking As DRReportRanking = Nothing
            While dr.Read

                dRReportRanking = Me.CreateObject(dr)

            End While

            Return dRReportRanking

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dRReportRankingList As ArrayList = New ArrayList

            While dr.Read
                Dim dRReportRanking As DRReportRanking = Me.CreateObject(dr)
                dRReportRankingList.Add(dRReportRanking)
            End While

            Return dRReportRankingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dRReportRanking As DRReportRanking = CType(obj, DRReportRanking)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dRReportRanking.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dRReportRanking As DRReportRanking = CType(obj, DRReportRanking)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerAreaID", DbType.Int32, dRReportRanking.DealerAreaID)
            DbCommandWrapper.AddInParameter("@PdfFileName", DbType.AnsiString, dRReportRanking.PdfFileName)
            DbCommandWrapper.AddInParameter("@XlsFileName", DbType.AnsiString, dRReportRanking.XlsFileName)
            DbCommandWrapper.AddInParameter("@ReportStatus", DbType.Int16, dRReportRanking.ReportStatus)
            DbCommandWrapper.AddInParameter("@DownloadStatus", DbType.Int16, dRReportRanking.DownloadStatus)
            DbCommandWrapper.AddInParameter("@PeriodType", DbType.Int16, dRReportRanking.PeriodType)
            DbCommandWrapper.AddInParameter("@Period", DbType.DateTime, dRReportRanking.Period)
            DBCommandWrapper.AddInParameter("@DealerType", DbType.Int16, dRReportRanking.DealerType)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dRReportRanking.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dRReportRanking.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CcReportMasterID", DbType.Int32, Me.GetRefObject(dRReportRanking.CcReportMaster))
            DbCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int16, Me.GetRefObject(dRReportRanking.CcVehicleCategory))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dRReportRanking.Dealer))
            DbCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, Me.GetRefObject(dRReportRanking.DealerGroup))
            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, Me.GetRefObject(dRReportRanking.CcCustomerCategory))

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

            Dim dRReportRanking As DRReportRanking = CType(obj, DRReportRanking)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dRReportRanking.ID)
            DbCommandWrapper.AddInParameter("@DealerAreaID", DbType.Int32, dRReportRanking.DealerAreaID)
            DbCommandWrapper.AddInParameter("@PdfFileName", DbType.AnsiString, dRReportRanking.PdfFileName)
            DbCommandWrapper.AddInParameter("@XlsFileName", DbType.AnsiString, dRReportRanking.XlsFileName)
            DbCommandWrapper.AddInParameter("@ReportStatus", DbType.Int16, dRReportRanking.ReportStatus)
            DbCommandWrapper.AddInParameter("@DownloadStatus", DbType.Int16, dRReportRanking.DownloadStatus)
            DbCommandWrapper.AddInParameter("@PeriodType", DbType.Int16, dRReportRanking.PeriodType)
            DBCommandWrapper.AddInParameter("@Period", DbType.DateTime, dRReportRanking.Period)
            DBCommandWrapper.AddInParameter("@DealerType", DbType.Int16, dRReportRanking.DealerType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dRReportRanking.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dRReportRanking.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CcReportMasterID", DbType.Int32, Me.GetRefObject(dRReportRanking.CcReportMaster))
            DbCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int16, Me.GetRefObject(dRReportRanking.CcVehicleCategory))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dRReportRanking.Dealer))
            DbCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, Me.GetRefObject(dRReportRanking.DealerGroup))
            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, Me.GetRefObject(dRReportRanking.CcCustomerCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DRReportRanking

            Dim dRReportRanking As DRReportRanking = New DRReportRanking

            dRReportRanking.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerAreaID")) Then dRReportRanking.DealerAreaID = CType(dr("DealerAreaID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PdfFileName")) Then dRReportRanking.PdfFileName = dr("PdfFileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReportStatus")) Then dRReportRanking.ReportStatus = CType(dr("ReportStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadStatus")) Then dRReportRanking.DownloadStatus = CType(dr("DownloadStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodType")) Then dRReportRanking.PeriodType = CType(dr("PeriodType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Period")) Then dRReportRanking.Period = CType(dr("Period"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerType")) Then dRReportRanking.DealerType = CType(dr("DealerType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dRReportRanking.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dRReportRanking.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dRReportRanking.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dRReportRanking.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dRReportRanking.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CcReportMasterID")) Then
                dRReportRanking.CcReportMaster = New CcReportMaster(CType(dr("CcReportMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CcVehicleCategoryID")) Then
                dRReportRanking.CcVehicleCategory = New CcVehicleCategory(CType(dr("CcVehicleCategoryID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dRReportRanking.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerGroupID")) Then
                dRReportRanking.DealerGroup = New DealerGroup(CType(dr("DealerGroupID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CcCustomerCategoryID")) Then
                dRReportRanking.CcCustomerCategory = New CcCustomerCategory(CType(dr("CcCustomerCategoryID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("XlsFileName")) Then dRReportRanking.XlsFileName = dr("XlsFileName").ToString
            Return dRReportRanking

        End Function

        Private Sub SetTableName()

            If Not (GetType(DRReportRanking) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DRReportRanking), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DRReportRanking).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

