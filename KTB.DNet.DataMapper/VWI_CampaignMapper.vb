
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_Campaign Objects Mapper.
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

    Public Class VWI_CampaignMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_Campaign"
        Private m_UpdateStatement As String = "up_UpdateVWI_Campaign"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_Campaign"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_CampaignList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_Campaign"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_Campaign As VWI_Campaign = Nothing
            While dr.Read

                VWI_Campaign = Me.CreateObject(dr)

            End While

            Return VWI_Campaign

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_CampaignList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_Campaign As VWI_Campaign = Me.CreateObject(dr)
                VWI_CampaignList.Add(VWI_Campaign)
            End While

            Return VWI_CampaignList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_Campaign As VWI_Campaign = CType(obj, VWI_Campaign)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_Campaign.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_Campaign As VWI_Campaign = CType(obj, VWI_Campaign)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            'DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@NomorSurat", DbType.AnsiString, VWI_Campaign.NomorSurat)
            'DbCommandWrapper.AddInParameter("@Status", DbType.Int16, VWI_Campaign.Status)
            'DbCommandWrapper.AddInParameter("@BenefitRegNo", DbType.AnsiString, VWI_Campaign.BenefitRegNo)
            'DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, VWI_Campaign.Remarks)
            'DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, VWI_Campaign.RowStatus)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)
            ''DbCommandWrapper.AddInParameter("@DetailRowStatus", DbType.Int16, VWI_Campaign.DetailRowStatus)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, VWI_Campaign.DealerID)
            'DbCommandWrapper.AddInParameter("@DealerCode", DbType.String, VWI_Campaign.DealerCode)
            'DbCommandWrapper.AddInParameter("@FakturValidationStart", DbType.DateTime, VWI_Campaign.FakturValidationStart)
            'DbCommandWrapper.AddInParameter("@FakturValidationEnd", DbType.DateTime, VWI_Campaign.FakturValidationEnd)
            'DbCommandWrapper.AddInParameter("@FakturOpenStart", DbType.DateTime, VWI_Campaign.FakturOpenStart)
            'DbCommandWrapper.AddInParameter("@FakturOpenEnd", DbType.DateTime, VWI_Campaign.FakturOpenEnd)
            'DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, VWI_Campaign.VehicleTypeID)
            'DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, VWI_Campaign.VehicleTypeCode)
            'DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, VWI_Campaign.VehicleTypeDesc)            


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

            Dim VWI_Campaign As VWI_Campaign = CType(obj, VWI_Campaign)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            'DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_Campaign.ID)
            'DbCommandWrapper.AddInParameter("@NomorSurat", DbType.AnsiString, VWI_Campaign.NomorSurat)
            'DbCommandWrapper.AddInParameter("@Status", DbType.Int16, VWI_Campaign.Status)
            'DbCommandWrapper.AddInParameter("@BenefitRegNo", DbType.AnsiString, VWI_Campaign.BenefitRegNo)
            'DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, VWI_Campaign.Remarks)
            'DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, VWI_Campaign.RowStatus)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)
            ''DbCommandWrapper.AddInParameter("@DetailRowStatus", DbType.Int16, VWI_Campaign.DetailRowStatus)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, VWI_Campaign.DealerID)
            'DbCommandWrapper.AddInParameter("@DealerCode", DbType.String, VWI_Campaign.DealerCode)
            'DbCommandWrapper.AddInParameter("@FakturValidationStart", DbType.DateTime, VWI_Campaign.FakturValidationStart)
            'DbCommandWrapper.AddInParameter("@FakturValidationEnd", DbType.DateTime, VWI_Campaign.FakturValidationEnd)
            'DbCommandWrapper.AddInParameter("@FakturOpenStart", DbType.DateTime, VWI_Campaign.FakturOpenStart)
            'DbCommandWrapper.AddInParameter("@FakturOpenEnd", DbType.DateTime, VWI_Campaign.FakturOpenEnd)
            'DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, VWI_Campaign.VehicleTypeID)
            'DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, VWI_Campaign.VehicleTypeCode)
            'DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, VWI_Campaign.VehicleTypeDesc)            

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_Campaign

            Dim VWI_Campaign As VWI_Campaign = New VWI_Campaign

            VWI_Campaign.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_Campaign.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then VWI_Campaign.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignCode")) Then VWI_Campaign.CampaignCode = dr("CampaignCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignType")) Then VWI_Campaign.CampaignType = CType(dr("CampaignType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignTypeCode")) Then VWI_Campaign.CampaignTypeCode = dr("CampaignTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignTypeDesc")) Then VWI_Campaign.CampaignTypeDesc = dr("CampaignTypeDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignName")) Then VWI_Campaign.CampaignName = dr("CampaignName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCampaignName")) Then VWI_Campaign.DealerCampaignName = dr("DealerCampaignName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BabitDealerNumber")) Then VWI_Campaign.BabitDealerNumber = dr("BabitDealerNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStart")) Then VWI_Campaign.PeriodStart = CType(dr("PeriodStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEnd")) Then VWI_Campaign.PeriodEnd = CType(dr("PeriodEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Location")) Then VWI_Campaign.Location = dr("Location").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignDate")) Then VWI_Campaign.CampaignDate = CType(dr("CampaignDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LocationName")) Then VWI_Campaign.LocationName = dr("LocationName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LuasArea")) Then VWI_Campaign.LuasArea = CType(dr("LuasArea"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProspectTarget")) Then VWI_Campaign.ProspectTarget = CType(dr("ProspectTarget"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SPKTarget")) Then VWI_Campaign.SPKTarget = CType(dr("SPKTarget"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InvitationQty")) Then VWI_Campaign.InvitationQty = CType(dr("InvitationQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitCategory")) Then VWI_Campaign.BabitCategory = dr("BabitCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then VWI_Campaign.CityCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeDesc")) Then VWI_Campaign.CityName = dr("VehicleTypeDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then VWI_Campaign.ProvinceCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeDesc")) Then VWI_Campaign.ProvinceName = dr("VehicleTypeDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then VWI_Campaign.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_Campaign.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return VWI_Campaign

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_Campaign) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_Campaign), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_Campaign).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace