
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrInhouse Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 6/25/2009 - 8:57:15 AM
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

    Public Class TrInhouseMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrInhouse"
        Private m_UpdateStatement As String = "up_UpdateTrInhouse"
        Private m_RetrieveStatement As String = "up_RetrieveTrInhouse"
        Private m_RetrieveListStatement As String = "up_RetrieveTrInhouseList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrInhouse"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trInhouse As TrInhouse = Nothing
            While dr.Read

                trInhouse = Me.CreateObject(dr)

            End While

            Return trInhouse

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trInhouseList As ArrayList = New ArrayList

            While dr.Read
                Dim trInhouse As TrInhouse = Me.CreateObject(dr)
                trInhouseList.Add(trInhouse)
            End While

            Return trInhouseList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trInhouse As TrInhouse = CType(obj, TrInhouse)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trInhouse.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trInhouse As TrInhouse = CType(obj, TrInhouse)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiStringFixedLength, trInhouse.Code)
            DbCommandWrapper.AddInParameter("@OrganizationID", DbType.Int32, trInhouse.OrganizationID)
            DbCommandWrapper.AddInParameter("@ReportDate", DbType.DateTime, trInhouse.ReportDate)
            DbCommandWrapper.AddInParameter("@ApprovedBy1", DbType.AnsiString, trInhouse.ApprovedBy1)
            DbCommandWrapper.AddInParameter("@JobPosition1", DbType.AnsiString, trInhouse.JobPosition1)
            DBCommandWrapper.AddInParameter("@ApprovedBy2", DbType.AnsiString, trInhouse.ApprovedBy2)
            DbCommandWrapper.AddInParameter("@JobPosition2", DbType.AnsiString, trInhouse.JobPosition2)
            DbCommandWrapper.AddInParameter("@ApprovedBy3", DbType.AnsiString, trInhouse.ApprovedBy3)
            DBCommandWrapper.AddInParameter("@JobPosition3", DbType.AnsiString, trInhouse.JobPosition3)
            DBCommandWrapper.AddInParameter("@UploadedReportFile", DbType.AnsiString, trInhouse.UploadedReportFile)
            DBCommandWrapper.AddInParameter("@UploadedAttendanceFile", DbType.AnsiString, trInhouse.UploadedAttendanceFile)
            DBCommandWrapper.AddInParameter("@UploadedEvaluationFile", DbType.AnsiString, trInhouse.UploadedEvaluationFile)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trInhouse.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trInhouse.LastUpdateBy)
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

            Dim trInhouse As TrInhouse = CType(obj, TrInhouse)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trInhouse.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiStringFixedLength, trInhouse.Code)
            DbCommandWrapper.AddInParameter("@OrganizationID", DbType.Int32, trInhouse.OrganizationID)
            DbCommandWrapper.AddInParameter("@ReportDate", DbType.DateTime, trInhouse.ReportDate)
            DbCommandWrapper.AddInParameter("@ApprovedBy1", DbType.AnsiString, trInhouse.ApprovedBy1)
            DbCommandWrapper.AddInParameter("@JobPosition1", DbType.AnsiString, trInhouse.JobPosition1)
            DBCommandWrapper.AddInParameter("@ApprovedBy2", DbType.AnsiString, trInhouse.ApprovedBy2)
            DbCommandWrapper.AddInParameter("@JobPosition2", DbType.AnsiString, trInhouse.JobPosition2)
            DbCommandWrapper.AddInParameter("@ApprovedBy3", DbType.AnsiString, trInhouse.ApprovedBy3)
            DBCommandWrapper.AddInParameter("@JobPosition3", DbType.AnsiString, trInhouse.JobPosition3)
            DBCommandWrapper.AddInParameter("@UploadedReportFile", DbType.AnsiString, trInhouse.UploadedReportFile)
            DBCommandWrapper.AddInParameter("@UploadedAttendanceFile", DbType.AnsiString, trInhouse.UploadedAttendanceFile)
            DBCommandWrapper.AddInParameter("@UploadedEvaluationFile", DbType.AnsiString, trInhouse.UploadedEvaluationFile)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trInhouse.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trInhouse.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrInhouse

            Dim trInhouse As TrInhouse = New TrInhouse

            trInhouse.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then trInhouse.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrganizationID")) Then trInhouse.OrganizationID = CType(dr("OrganizationID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReportDate")) Then trInhouse.ReportDate = CType(dr("ReportDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovedBy1")) Then trInhouse.ApprovedBy1 = dr("ApprovedBy1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPosition1")) Then trInhouse.JobPosition1 = dr("JobPosition1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovedBy2")) Then trInhouse.ApprovedBy2 = dr("ApprovedBy2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPosition2")) Then trInhouse.JobPosition2 = dr("JobPosition2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovedBy3")) Then trInhouse.ApprovedBy3 = dr("ApprovedBy3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPosition3")) Then trInhouse.JobPosition3 = dr("JobPosition3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadedReportFile")) Then trInhouse.UploadedReportFile = dr("UploadedReportFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadedAttendanceFile")) Then trInhouse.UploadedAttendanceFile = dr("UploadedAttendanceFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadedEvaluationFile")) Then trInhouse.UploadedEvaluationFile = dr("UploadedEvaluationFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trInhouse.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trInhouse.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trInhouse.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trInhouse.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trInhouse.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trInhouse

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrInhouse) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrInhouse), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrInhouse).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

