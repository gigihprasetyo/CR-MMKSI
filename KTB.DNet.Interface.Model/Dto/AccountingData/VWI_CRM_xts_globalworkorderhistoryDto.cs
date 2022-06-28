#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_globalworkorderhistoryDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2020 10:42:01
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_globalworkorderhistoryDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public Guid xts_parentbusinessunitid { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public string ktb_servicetype { get; set; }

		public DateTime xts_scheduledarrivaldateandtime { get; set; }

		public string statuscodename { get; set; }

		public string xts_productsegment14 { get; set; }

		public string xts_servicepersoninchargedescription { get; set; }

		public string modifiedbyname { get; set; }

		public string xts_platenumber { get; set; }

		public string xts_vehiclemodelname { get; set; }

		public string xts_vehiclepubliclinkidname { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string ktb_serviceincdnetcode { get; set; }

		public string xts_address2 { get; set; }

		public string xts_lastmileage { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public Guid xts_vehiclepubliclinkid { get; set; }

		public string ktb_servicelayanan { get; set; }

		public string ktb_hasilinterfacename { get; set; }

		public string xts_address4 { get; set; }

		public string xts_ordertype { get; set; }

		public int statecode { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string xts_locking { get; set; }

		public string xts_customernumber { get; set; }

		public DateTime ktb_createdonworkorder { get; set; }

		public string xts_manufacturer { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_serviceadvisor { get; set; }

		public Guid createdonbehalfby { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public DateTime xts_actualfinishdateandtime { get; set; }

		public DateTime xts_transactiondate { get; set; }

		public string xts_servicecategory { get; set; }

		public string xts_businessunit { get; set; }

		public string createdbyname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_address3 { get; set; }

		public string xts_technicaladvisor { get; set; }

		public string ktb_kindcode { get; set; }

		public bool ktb_hasilinterface { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_address1 { get; set; }

		public DateTime ktb_actualservicefinishdateandtime { get; set; }

		public string xts_vehicleidentificationnumber { get; set; }

		public DateTime xts_actualarrivaldateandtime { get; set; }

		public string createdbyyominame { get; set; }

		public string xts_customer { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xts_customerpubliclinkidname { get; set; }

		public string xts_parentbusinessunitidname { get; set; }

		public string organizationidname { get; set; }

		public string xts_workorder { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public Guid xts_globalworkorderhistoryid { get; set; }

		public Guid organizationid { get; set; }

		public Guid xts_customerpubliclinkid { get; set; }

		public string statecodename { get; set; }

		public string xts_globalworkorderhistory { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
