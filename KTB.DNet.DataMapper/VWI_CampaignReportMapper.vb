
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_CampaignReport Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 07/03/2018 - 13:18:56
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

    Public Class VWI_CampaignReportMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_CampaignReport"
        Private m_UpdateStatement As String = "up_UpdateVWI_CampaignReport"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_CampaignReport"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_CampaignReportList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_CampaignReport"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_CampaignReport As VWI_CampaignReport = Nothing
            While dr.Read

                vWI_CampaignReport = Me.CreateObject(dr)

            End While

            Return vWI_CampaignReport

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_CampaignReportList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_CampaignReport As VWI_CampaignReport = Me.CreateObject(dr)
                vWI_CampaignReportList.Add(vWI_CampaignReport)
            End While

            Return vWI_CampaignReportList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_CampaignReport As VWI_CampaignReport = CType(obj, VWI_CampaignReport)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_CampaignReport.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_CampaignReport As VWI_CampaignReport = CType(obj, VWI_CampaignReport)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NomorSurat", DbType.AnsiString, vWI_CampaignReport.NomorSurat)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, vWI_CampaignReport.Status)
            DbCommandWrapper.AddInParameter("@BenefitRegNo", DbType.AnsiString, vWI_CampaignReport.BenefitRegNo)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, vWI_CampaignReport.Remarks)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vWI_CampaignReport.RowStatus)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)
            'DbCommandWrapper.AddInParameter("@DetailRowStatus", DbType.Int16, vWI_CampaignReport.DetailRowStatus)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, vWI_CampaignReport.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.String, vWI_CampaignReport.DealerCode)
            DbCommandWrapper.AddInParameter("@FakturValidationStart", DbType.DateTime, vWI_CampaignReport.FakturValidationStart)
            DbCommandWrapper.AddInParameter("@FakturValidationEnd", DbType.DateTime, vWI_CampaignReport.FakturValidationEnd)
            DbCommandWrapper.AddInParameter("@FakturOpenStart", DbType.DateTime, vWI_CampaignReport.FakturOpenStart)
            DbCommandWrapper.AddInParameter("@FakturOpenEnd", DbType.DateTime, vWI_CampaignReport.FakturOpenEnd)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, vWI_CampaignReport.VehicleTypeID)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, vWI_CampaignReport.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, vWI_CampaignReport.VehicleTypeDesc)            


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

            Dim vWI_CampaignReport As VWI_CampaignReport = CType(obj, VWI_CampaignReport)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_CampaignReport.ID)
            DbCommandWrapper.AddInParameter("@NomorSurat", DbType.AnsiString, vWI_CampaignReport.NomorSurat)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, vWI_CampaignReport.Status)
            DbCommandWrapper.AddInParameter("@BenefitRegNo", DbType.AnsiString, vWI_CampaignReport.BenefitRegNo)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, vWI_CampaignReport.Remarks)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vWI_CampaignReport.RowStatus)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)
            'DbCommandWrapper.AddInParameter("@DetailRowStatus", DbType.Int16, vWI_CampaignReport.DetailRowStatus)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, vWI_CampaignReport.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.String, vWI_CampaignReport.DealerCode)
            DbCommandWrapper.AddInParameter("@FakturValidationStart", DbType.DateTime, vWI_CampaignReport.FakturValidationStart)
            DbCommandWrapper.AddInParameter("@FakturValidationEnd", DbType.DateTime, vWI_CampaignReport.FakturValidationEnd)
            DbCommandWrapper.AddInParameter("@FakturOpenStart", DbType.DateTime, vWI_CampaignReport.FakturOpenStart)
            DbCommandWrapper.AddInParameter("@FakturOpenEnd", DbType.DateTime, vWI_CampaignReport.FakturOpenEnd)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, vWI_CampaignReport.VehicleTypeID)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, vWI_CampaignReport.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, vWI_CampaignReport.VehicleTypeDesc)            



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_CampaignReport

            Dim vWI_CampaignReport As VWI_CampaignReport = New VWI_CampaignReport

            vWI_CampaignReport.ID = CType(dr("ID"), Integer)
            vWI_CampaignReport.HeaderID = CType(dr("HeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NomorSurat")) Then vWI_CampaignReport.NomorSurat = dr("NomorSurat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vWI_CampaignReport.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitRegNo")) Then vWI_CampaignReport.BenefitRegNo = CType(dr("BenefitRegNo"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("Remarks")) Then vWI_CampaignReport.Remarks = dr("Remarks").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vWI_CampaignReport.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_CampaignReport.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("DetailRowStatus")) Then vWI_CampaignReport.DetailRowStatus = CType(dr("DetailRowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then vWI_CampaignReport.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_CampaignReport.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturValidationStart")) Then vWI_CampaignReport.FakturValidationStart = CType(dr("FakturValidationStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturValidationEnd")) Then vWI_CampaignReport.FakturValidationEnd = CType(dr("FakturValidationEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturOpenStart")) Then vWI_CampaignReport.FakturOpenStart = CType(dr("FakturOpenStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturOpenEnd")) Then vWI_CampaignReport.FakturOpenEnd = CType(dr("FakturOpenEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeID")) Then vWI_CampaignReport.VehicleTypeID = CType(dr("VehicleTypeID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then vWI_CampaignReport.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeDesc")) Then vWI_CampaignReport.VehicleTypeDesc = dr("VehicleTypeDesc").ToString            

            Return vWI_CampaignReport

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_CampaignReport) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_CampaignReport), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_CampaignReport).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace