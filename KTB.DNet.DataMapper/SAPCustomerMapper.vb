
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SAPCustomer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2014 
'// ---------------------
'// $History      : $
'// Generated on 1/24/2014 - 11:04:58 AM
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

    Public Class SAPCustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSAPCustomer"
        Private m_UpdateStatement As String = "up_UpdateSAPCustomer"
        Private m_RetrieveStatement As String = "up_RetrieveSAPCustomer"
        Private m_RetrieveListStatement As String = "up_RetrieveSAPCustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSAPCustomer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sAPCustomer As SAPCustomer = Nothing
            While dr.Read

                sAPCustomer = Me.CreateObject(dr)

            End While

            Return sAPCustomer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sAPCustomerList As ArrayList = New ArrayList

            While dr.Read
                Dim sAPCustomer As SAPCustomer = Me.CreateObject(dr)
                sAPCustomerList.Add(sAPCustomer)
            End While

            Return sAPCustomerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPCustomer As SAPCustomer = CType(obj, SAPCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPCustomer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPCustomer As SAPCustomer = CType(obj, SAPCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesforceID", DbType.AnsiString, sAPCustomer.SalesforceID)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, sAPCustomer.CustomerCode)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, sAPCustomer.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.Int16, sAPCustomer.CustomerType)
            DbCommandWrapper.AddInParameter("@CustomerAddress", DbType.AnsiString, sAPCustomer.CustomerAddress)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, sAPCustomer.Phone)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, sAPCustomer.Email)
            DbCommandWrapper.AddInParameter("@Sex", DbType.Byte, sAPCustomer.Sex)
            DbCommandWrapper.AddInParameter("@AgeSegment", DbType.Byte, sAPCustomer.AgeSegment)
            DbCommandWrapper.AddInParameter("@CustomerPurpose", DbType.Int16, sAPCustomer.CustomerPurpose)
            DbCommandWrapper.AddInParameter("@InformationType", DbType.Int16, sAPCustomer.InformationType)
            DbCommandWrapper.AddInParameter("@InformationSource", DbType.Int16, sAPCustomer.InformationSource)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, sAPCustomer.Status)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, sAPCustomer.Qty)
            DbCommandWrapper.AddInParameter("@ProspectDate", DbType.DateTime, sAPCustomer.ProspectDate)
            DbCommandWrapper.AddInParameter("@isSPK", DbType.Boolean, sAPCustomer.isSPK)
            DbCommandWrapper.AddInParameter("@CurrVehicleBrand", DbType.AnsiString, sAPCustomer.CurrVehicleBrand)
            DbCommandWrapper.AddInParameter("@CurrVehicleType", DbType.AnsiString, sAPCustomer.CurrVehicleType)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, sAPCustomer.Note)
            DbCommandWrapper.AddInParameter("@WebID", DbType.String, sAPCustomer.WebID)
            DbCommandWrapper.AddInParameter("@VehicleModel", DbType.String, sAPCustomer.VehicleModel)
            DbCommandWrapper.AddInParameter("@Variant", DbType.String, sAPCustomer.Variants)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, sAPCustomer.Sequence)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sAPCustomer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.Date, sAPCustomer.BirthDate)
            DbCommandWrapper.AddInParameter("@PreferedVehicleModel", DbType.AnsiString, sAPCustomer.PreferedVehicleModel)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, sAPCustomer.Description)
            DbCommandWrapper.AddInParameter("@EstimatedCloseDate", DbType.Date, sAPCustomer.EstimatedCloseDate)
            DbCommandWrapper.AddInParameter("@OriginatingLeadId", DbType.Guid, sAPCustomer.OriginatingLeadId)
            DbCommandWrapper.AddInParameter("@StatusCode", DbType.Int16, sAPCustomer.StatusCode)
            DbCommandWrapper.AddInParameter("@LeadStatus", DbType.Byte, sAPCustomer.LeadStatus)
            DbCommandWrapper.AddInParameter("@StateCode", DbType.Byte, sAPCustomer.StateCode)
            DbCommandWrapper.AddInParameter("@CampaignName", DbType.AnsiString, sAPCustomer.CampaignName)
            DbCommandWrapper.AddInParameter("@Name2", DbType.AnsiString, sAPCustomer.Name2)
            DbCommandWrapper.AddInParameter("@PhoneType", DbType.Int32, sAPCustomer.PhoneType)
            DbCommandWrapper.AddInParameter("@Telp", DbType.AnsiString, sAPCustomer.Telp)
            DbCommandWrapper.AddInParameter("@IdentityType", DbType.Int32, sAPCustomer.IdentityType)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, sAPCustomer.IdentityNumber)
            DbCommandWrapper.AddInParameter("@JobKind", DbType.Int32, sAPCustomer.JobKind)
            DbCommandWrapper.AddInParameter("@CusReqPrice", DbType.Currency, sAPCustomer.CusReqPrice)
            DbCommandWrapper.AddInParameter("@CusReqDiscount", DbType.Currency, sAPCustomer.CusReqDiscount)
            DbCommandWrapper.AddInParameter("@BookingFee", DbType.Currency, sAPCustomer.BookingFee)
            DbCommandWrapper.AddInParameter("@BBNType", DbType.Int32, sAPCustomer.BBNType)
            DbCommandWrapper.AddInParameter("@BlankoSPKNo", DbType.AnsiString, sAPCustomer.BlankoSPKNo)
            DbCommandWrapper.AddInParameter("@BlankoSPKDoc", DbType.AnsiString, sAPCustomer.BlankoSPKDoc)
            DbCommandWrapper.AddInParameter("@InterfaceStatus", DbType.Int16, sAPCustomer.InterfaceStatus)
            DbCommandWrapper.AddInParameter("@InterfaceMessage", DbType.AnsiString, sAPCustomer.InterfaceMessage)
            DbCommandWrapper.AddInParameter("@GUIDUpdate", DbType.AnsiString, sAPCustomer.GUIDUpdate)
            DbCommandWrapper.AddInParameter("@Topic", DbType.AnsiString, sAPCustomer.Topic)
            DbCommandWrapper.AddInParameter("@CurrVehicleBrandDesc", DbType.AnsiString, sAPCustomer.CurrVehicleBrandDesc)
            DbCommandWrapper.AddInParameter("@VehicleComparison", DbType.AnsiString, sAPCustomer.VehicleComparison)
            DbCommandWrapper.AddInParameter("@GUID", DbType.AnsiString, sAPCustomer.SAPCustomerGUID)
            DbCommandWrapper.AddInParameter("@CountryCode", DbType.AnsiString, sAPCustomer.CountryCode)
            DbCommandWrapper.AddInParameter("@Rating", DbType.Int32, sAPCustomer.Rating)

            DbCommandWrapper.AddInParameter("@VechileModelID", DbType.Int16, Me.GetRefObject(sAPCustomer.VechileModel))
            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int16, Me.GetRefObject(sAPCustomer.VechileColor))
            DbCommandWrapper.AddInParameter("@DealerVehiclePriceDetailID", DbType.Int32, Me.GetRefObject(sAPCustomer.DealerVehiclePriceDetail))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(sAPCustomer.VechileType))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(sAPCustomer.Dealer))
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailID", DbType.Int32, Me.GetRefObject(sAPCustomer.BusinessSectorDetail))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(sAPCustomer.SalesmanHeader))

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

            Dim sAPCustomer As SAPCustomer = CType(obj, SAPCustomer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPCustomer.ID)
            DbCommandWrapper.AddInParameter("@SalesforceID", DbType.AnsiString, sAPCustomer.SalesforceID)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, sAPCustomer.CustomerCode)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, sAPCustomer.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.Int16, sAPCustomer.CustomerType)
            DbCommandWrapper.AddInParameter("@CustomerAddress", DbType.AnsiString, sAPCustomer.CustomerAddress)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, sAPCustomer.Phone)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, sAPCustomer.Email)
            DbCommandWrapper.AddInParameter("@Sex", DbType.Byte, sAPCustomer.Sex)
            DbCommandWrapper.AddInParameter("@AgeSegment", DbType.Byte, sAPCustomer.AgeSegment)
            DbCommandWrapper.AddInParameter("@CustomerPurpose", DbType.Int16, sAPCustomer.CustomerPurpose)
            DbCommandWrapper.AddInParameter("@InformationType", DbType.Int16, sAPCustomer.InformationType)
            DbCommandWrapper.AddInParameter("@InformationSource", DbType.Int16, sAPCustomer.InformationSource)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, sAPCustomer.Status)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, sAPCustomer.Qty)
            DbCommandWrapper.AddInParameter("@ProspectDate", DbType.DateTime, sAPCustomer.ProspectDate)
            DbCommandWrapper.AddInParameter("@isSPK", DbType.Boolean, sAPCustomer.isSPK)
            DbCommandWrapper.AddInParameter("@CurrVehicleBrand", DbType.AnsiString, sAPCustomer.CurrVehicleBrand)
            DbCommandWrapper.AddInParameter("@CurrVehicleType", DbType.AnsiString, sAPCustomer.CurrVehicleType)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, sAPCustomer.Note)
            DbCommandWrapper.AddInParameter("@WebID", DbType.String, sAPCustomer.WebID)
            DbCommandWrapper.AddInParameter("@VehicleModel", DbType.String, sAPCustomer.VehicleModel)
            DbCommandWrapper.AddInParameter("@Variant", DbType.String, sAPCustomer.Variants)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, sAPCustomer.Sequence)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sAPCustomer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.Date, sAPCustomer.BirthDate)
            DbCommandWrapper.AddInParameter("@PreferedVehicleModel", DbType.AnsiString, sAPCustomer.PreferedVehicleModel)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, sAPCustomer.Description)
            DbCommandWrapper.AddInParameter("@EstimatedCloseDate", DbType.Date, sAPCustomer.EstimatedCloseDate)
            DbCommandWrapper.AddInParameter("@OriginatingLeadId", DbType.Guid, sAPCustomer.OriginatingLeadId)
            DbCommandWrapper.AddInParameter("@StatusCode", DbType.Int16, sAPCustomer.StatusCode)
            DbCommandWrapper.AddInParameter("@LeadStatus", DbType.Byte, sAPCustomer.LeadStatus)
            DbCommandWrapper.AddInParameter("@StateCode", DbType.Byte, sAPCustomer.StateCode)
            DbCommandWrapper.AddInParameter("@CampaignName", DbType.AnsiString, sAPCustomer.CampaignName)
            DbCommandWrapper.AddInParameter("@Name2", DbType.AnsiString, sAPCustomer.Name2)
            DbCommandWrapper.AddInParameter("@PhoneType", DbType.Int32, sAPCustomer.PhoneType)
            DbCommandWrapper.AddInParameter("@Telp", DbType.AnsiString, sAPCustomer.Telp)
            DbCommandWrapper.AddInParameter("@IdentityType", DbType.Int32, sAPCustomer.IdentityType)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, sAPCustomer.IdentityNumber)
            DbCommandWrapper.AddInParameter("@JobKind", DbType.Int32, sAPCustomer.JobKind)
            DbCommandWrapper.AddInParameter("@CusReqPrice", DbType.Currency, sAPCustomer.CusReqPrice)
            DbCommandWrapper.AddInParameter("@CusReqDiscount", DbType.Currency, sAPCustomer.CusReqDiscount)
            DbCommandWrapper.AddInParameter("@BookingFee", DbType.Currency, sAPCustomer.BookingFee)
            DbCommandWrapper.AddInParameter("@BBNType", DbType.Int32, sAPCustomer.BBNType)
            DbCommandWrapper.AddInParameter("@BlankoSPKNo", DbType.AnsiString, sAPCustomer.BlankoSPKNo)
            DbCommandWrapper.AddInParameter("@BlankoSPKDoc", DbType.AnsiString, sAPCustomer.BlankoSPKDoc)
            DbCommandWrapper.AddInParameter("@InterfaceStatus", DbType.Int16, sAPCustomer.InterfaceStatus)
            DbCommandWrapper.AddInParameter("@InterfaceMessage", DbType.AnsiString, sAPCustomer.InterfaceMessage)
            DbCommandWrapper.AddInParameter("@GUIDUpdate", DbType.AnsiString, sAPCustomer.GUIDUpdate)
            DbCommandWrapper.AddInParameter("@Topic", DbType.AnsiString, sAPCustomer.Topic)
            DbCommandWrapper.AddInParameter("@CurrVehicleBrandDesc", DbType.AnsiString, sAPCustomer.CurrVehicleBrandDesc)
            DbCommandWrapper.AddInParameter("@VehicleComparison", DbType.AnsiString, sAPCustomer.VehicleComparison)
            DbCommandWrapper.AddInParameter("@GUID", DbType.AnsiString, sAPCustomer.SAPCustomerGUID)
            DbCommandWrapper.AddInParameter("@CountryCode", DbType.AnsiString, sAPCustomer.CountryCode)
            DbCommandWrapper.AddInParameter("@Rating", DbType.Int32, sAPCustomer.Rating)

            DbCommandWrapper.AddInParameter("@VechileModelID", DbType.Int16, Me.GetRefObject(sAPCustomer.VechileModel))
            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int16, Me.GetRefObject(sAPCustomer.VechileColor))
            DbCommandWrapper.AddInParameter("@DealerVehiclePriceDetailID", DbType.Int32, Me.GetRefObject(sAPCustomer.DealerVehiclePriceDetail))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(sAPCustomer.VechileType))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(sAPCustomer.Dealer))
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailID", DbType.Int32, Me.GetRefObject(sAPCustomer.BusinessSectorDetail))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(sAPCustomer.SalesmanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SAPCustomer

            Dim sAPCustomer As SAPCustomer = New SAPCustomer

            sAPCustomer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesforceID")) Then sAPCustomer.SalesforceID = dr("SalesforceID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerCode")) Then sAPCustomer.CustomerCode = dr("CustomerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then sAPCustomer.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerType")) Then sAPCustomer.CustomerType = CType(dr("CustomerType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerAddress")) Then sAPCustomer.CustomerAddress = dr("CustomerAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then sAPCustomer.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then sAPCustomer.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Sex")) Then sAPCustomer.Sex = CType(dr("Sex"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("AgeSegment")) Then sAPCustomer.AgeSegment = CType(dr("AgeSegment"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerPurpose")) Then sAPCustomer.CustomerPurpose = CType(dr("CustomerPurpose"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("InformationType")) Then sAPCustomer.InformationType = CType(dr("InformationType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("InformationSource")) Then sAPCustomer.InformationSource = CType(dr("InformationSource"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sAPCustomer.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then sAPCustomer.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProspectDate")) Then sAPCustomer.ProspectDate = CType(dr("ProspectDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("isSPK")) Then sAPCustomer.isSPK = CType(dr("isSPK"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("CurrVehicleBrand")) Then sAPCustomer.CurrVehicleBrand = dr("CurrVehicleBrand").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CurrVehicleType")) Then sAPCustomer.CurrVehicleType = dr("CurrVehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleModel")) Then sAPCustomer.VehicleModel = dr("VehicleModel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Variant")) Then sAPCustomer.Variants = dr("Variant").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then sAPCustomer.Note = dr("Note").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WebID")) Then sAPCustomer.WebID = dr("WebID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then sAPCustomer.Sequence = CType(dr("Sequence"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sAPCustomer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sAPCustomer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sAPCustomer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sAPCustomer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sAPCustomer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BirthDate")) Then sAPCustomer.BirthDate = CType(dr("BirthDate"), Date)
            If Not dr.IsDBNull(dr.GetOrdinal("PreferedVehicleModel")) Then sAPCustomer.PreferedVehicleModel = dr("PreferedVehicleModel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then sAPCustomer.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EstimatedCloseDate")) Then sAPCustomer.EstimatedCloseDate = CType(dr("EstimatedCloseDate"), Date)
            If Not dr.IsDBNull(dr.GetOrdinal("OriginatingLeadId")) Then sAPCustomer.OriginatingLeadId = CType(dr("OriginatingLeadId"), Guid)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusCode")) Then sAPCustomer.StatusCode = CType(dr("StatusCode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LeadStatus")) Then sAPCustomer.LeadStatus = CType(dr("LeadStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("StateCode")) Then sAPCustomer.StateCode = CType(dr("StateCode"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignName")) Then sAPCustomer.CampaignName = dr("CampaignName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name2")) Then sAPCustomer.Name2 = dr("Name2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneType")) Then sAPCustomer.PhoneType = CType(dr("PhoneType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Telp")) Then sAPCustomer.Telp = dr("Telp").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityType")) Then sAPCustomer.IdentityType = CType(dr("IdentityType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityNumber")) Then sAPCustomer.IdentityNumber = dr("IdentityNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobKind")) Then sAPCustomer.JobKind = CType(dr("JobKind"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CusReqPrice")) Then sAPCustomer.CusReqPrice = CType(dr("CusReqPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("CusReqDiscount")) Then sAPCustomer.CusReqDiscount = CType(dr("CusReqDiscount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BookingFee")) Then sAPCustomer.BookingFee = CType(dr("BookingFee"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BBNType")) Then sAPCustomer.BBNType = CType(dr("BBNType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BlankoSPKNo")) Then sAPCustomer.BlankoSPKNo = dr("BlankoSPKNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BlankoSPKDoc")) Then sAPCustomer.BlankoSPKDoc = dr("BlankoSPKDoc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InterfaceStatus")) Then sAPCustomer.InterfaceStatus = CType(dr("InterfaceStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("InterfaceMessage")) Then sAPCustomer.InterfaceMessage = dr("InterfaceMessage").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GUIDUpdate")) Then sAPCustomer.GUIDUpdate = dr("GUIDUpdate").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Topic")) Then sAPCustomer.Topic = dr("Topic").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CurrVehicleBrandDesc")) Then sAPCustomer.CurrVehicleBrandDesc = dr("CurrVehicleBrandDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleComparison")) Then sAPCustomer.VehicleComparison = dr("VehicleComparison").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GUID")) Then sAPCustomer.SAPCustomerGUID = dr("GUID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CountryCode")) Then sAPCustomer.CountryCode = dr("CountryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Rating")) Then sAPCustomer.Rating = CType(dr("Rating"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileModelID")) Then
                sAPCustomer.VechileModel = New VechileModel(CType(dr("VechileModelID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then
                sAPCustomer.VechileColor = New VechileColor(CType(dr("VechileColorID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerVehiclePriceDetailID")) Then
                sAPCustomer.DealerVehiclePriceDetail = New DealerVehiclePriceDetail(CType(dr("DealerVehiclePriceDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                sAPCustomer.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sAPCustomer.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessSectorDetailID")) Then
                sAPCustomer.BusinessSectorDetail = New BusinessSectorDetail(CType(dr("BusinessSectorDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                sAPCustomer.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Int32))
            End If

            Return sAPCustomer

        End Function

        Private Sub SetTableName()

            If Not (GetType(SAPCustomer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SAPCustomer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SAPCustomer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

