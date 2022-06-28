
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_MonthlyReport Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 02/05/2018 - 10:56:41 AM
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

    Public Class V_MonthlyReportMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_MonthlyReport"
        Private m_UpdateStatement As String = "up_UpdateV_MonthlyReport"
        Private m_RetrieveStatement As String = "up_RetrieveV_MonthlyReport"
        Private m_RetrieveListStatement As String = "up_RetrieveV_MonthlyReportList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_MonthlyReport"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_MonthlyReport As V_MonthlyReport = Nothing
            While dr.Read

                v_MonthlyReport = Me.CreateObject(dr)

            End While

            Return v_MonthlyReport

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_MonthlyReportList As ArrayList = New ArrayList

            While dr.Read
                Dim v_MonthlyReport As V_MonthlyReport = Me.CreateObject(dr)
                v_MonthlyReportList.Add(v_MonthlyReport)
            End While

            Return v_MonthlyReportList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_MonthlyReport As V_MonthlyReport = CType(obj, V_MonthlyReport)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_MonthlyReport.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_MonthlyReport As V_MonthlyReport = CType(obj, V_MonthlyReport)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Kind", DbType.Int32, v_MonthlyReport.Kind)
            DbCommandWrapper.AddInParameter("@PeriodeMonth", DbType.Int16, v_MonthlyReport.PeriodeMonth)
            DbCommandWrapper.AddInParameter("@PeriodeYear", DbType.Int16, v_MonthlyReport.PeriodeYear)
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, v_MonthlyReport.ProductCategoryID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_MonthlyReport.DealerID)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, v_MonthlyReport.FileName)
            DbCommandWrapper.AddInParameter("@FileSize", DbType.Int32, v_MonthlyReport.FileSize)
            DbCommandWrapper.AddInParameter("@LastDownloadBy", DbType.AnsiString, v_MonthlyReport.LastDownloadBy)
            DbCommandWrapper.AddInParameter("@LastDownloadDate", DbType.DateTime, v_MonthlyReport.LastDownloadDate)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, v_MonthlyReport.BillingDate)
            DbCommandWrapper.AddInParameter("@BillingNo", DbType.AnsiString, v_MonthlyReport.BillingNo)
            DbCommandWrapper.AddInParameter("@AccountingNo", DbType.AnsiString, v_MonthlyReport.AccountingNo)
            DbCommandWrapper.AddInParameter("@TaxNo", DbType.AnsiString, v_MonthlyReport.TaxNo)
            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, v_MonthlyReport.TransferDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_MonthlyReport.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_MonthlyReport.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_MonthlyReport.DealerCode)
            DbCommandWrapper.AddInParameter("@Period", DbType.DateTime, v_MonthlyReport.Period)
            DbCommandWrapper.AddInParameter("@IsNullLastDownloadDate", DbType.Int32, v_MonthlyReport.IsNullLastDownloadDate)
            DbCommandWrapper.AddInParameter("@IsNullTransferDate", DbType.Int32, v_MonthlyReport.IsNullTransferDate)


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

            Dim v_MonthlyReport As V_MonthlyReport = CType(obj, V_MonthlyReport)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_MonthlyReport.ID)
            DbCommandWrapper.AddInParameter("@Kind", DbType.Int32, v_MonthlyReport.Kind)
            DbCommandWrapper.AddInParameter("@PeriodeMonth", DbType.Int16, v_MonthlyReport.PeriodeMonth)
            DbCommandWrapper.AddInParameter("@PeriodeYear", DbType.Int16, v_MonthlyReport.PeriodeYear)
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, v_MonthlyReport.ProductCategoryID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_MonthlyReport.DealerID)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, v_MonthlyReport.FileName)
            DbCommandWrapper.AddInParameter("@FileSize", DbType.Int32, v_MonthlyReport.FileSize)
            DbCommandWrapper.AddInParameter("@LastDownloadBy", DbType.AnsiString, v_MonthlyReport.LastDownloadBy)
            DbCommandWrapper.AddInParameter("@LastDownloadDate", DbType.DateTime, v_MonthlyReport.LastDownloadDate)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, v_MonthlyReport.BillingDate)
            DbCommandWrapper.AddInParameter("@BillingNo", DbType.AnsiString, v_MonthlyReport.BillingNo)
            DbCommandWrapper.AddInParameter("@AccountingNo", DbType.AnsiString, v_MonthlyReport.AccountingNo)
            DbCommandWrapper.AddInParameter("@TaxNo", DbType.AnsiString, v_MonthlyReport.TaxNo)
            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, v_MonthlyReport.TransferDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_MonthlyReport.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_MonthlyReport.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_MonthlyReport.DealerCode)
            DbCommandWrapper.AddInParameter("@Period", DbType.DateTime, v_MonthlyReport.Period)
            DbCommandWrapper.AddInParameter("@IsNullLastDownloadDate", DbType.Int32, v_MonthlyReport.IsNullLastDownloadDate)
            DbCommandWrapper.AddInParameter("@IsNullTransferDate", DbType.Int32, v_MonthlyReport.IsNullTransferDate)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_MonthlyReport

            Dim v_MonthlyReport As V_MonthlyReport = New V_MonthlyReport

            v_MonthlyReport.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Kind")) Then v_MonthlyReport.Kind = CType(dr("Kind"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeMonth")) Then v_MonthlyReport.PeriodeMonth = CType(dr("PeriodeMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeYear")) Then v_MonthlyReport.PeriodeYear = CType(dr("PeriodeYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then v_MonthlyReport.ProductCategoryID = CType(dr("ProductCategoryID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_MonthlyReport.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then v_MonthlyReport.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileSize")) Then v_MonthlyReport.FileSize = CType(dr("FileSize"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LastDownloadBy")) Then v_MonthlyReport.LastDownloadBy = dr("LastDownloadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastDownloadDate")) Then v_MonthlyReport.LastDownloadDate = CType(dr("LastDownloadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingDate")) Then v_MonthlyReport.BillingDate = CType(dr("BillingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNo")) Then v_MonthlyReport.BillingNo = dr("BillingNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AccountingNo")) Then v_MonthlyReport.AccountingNo = dr("AccountingNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TaxNo")) Then v_MonthlyReport.TaxNo = dr("TaxNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransferDate")) Then v_MonthlyReport.TransferDate = CType(dr("TransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_MonthlyReport.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_MonthlyReport.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_MonthlyReport.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_MonthlyReport.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_MonthlyReport.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_MonthlyReport.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Period")) Then v_MonthlyReport.Period = CType(dr("Period"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsNullLastDownloadDate")) Then v_MonthlyReport.IsNullLastDownloadDate = CType(dr("IsNullLastDownloadDate"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsNullTransferDate")) Then v_MonthlyReport.IsNullTransferDate = CType(dr("IsNullTransferDate"), Integer)

            Return v_MonthlyReport

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_MonthlyReport) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_MonthlyReport), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_MonthlyReport).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

