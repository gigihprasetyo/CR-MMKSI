#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_globalworkorderhistoryParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_globalworkorderhistoryParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string ktb_servicetype { get; set; }

		[AntiXss]
		public DateTime xts_scheduledarrivaldateandtime { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_productsegment14 { get; set; }

		[AntiXss]
		public string xts_servicepersoninchargedescription { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_platenumber { get; set; }

		[AntiXss]
		public string xts_vehiclemodelname { get; set; }

		[AntiXss]
		public string xts_vehiclepubliclinkidname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string ktb_serviceincdnetcode { get; set; }

		[AntiXss]
		public string xts_address2 { get; set; }

		[AntiXss]
		public string xts_lastmileage { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_vehiclepubliclinkid { get; set; }

		[AntiXss]
		public string ktb_servicelayanan { get; set; }

		[AntiXss]
		public string ktb_hasilinterfacename { get; set; }

		[AntiXss]
		public string xts_address4 { get; set; }

		[AntiXss]
		public string xts_ordertype { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public DateTime ktb_createdonworkorder { get; set; }

		[AntiXss]
		public string xts_manufacturer { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_serviceadvisor { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public DateTime xts_actualfinishdateandtime { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public string xts_servicecategory { get; set; }

		[AntiXss]
		public string xts_businessunit { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_address3 { get; set; }

		[AntiXss]
		public string xts_technicaladvisor { get; set; }

		[AntiXss]
		public string ktb_kindcode { get; set; }

		[AntiXss]
		public bool ktb_hasilinterface { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_address1 { get; set; }

		[AntiXss]
		public DateTime ktb_actualservicefinishdateandtime { get; set; }

		[AntiXss]
		public string xts_vehicleidentificationnumber { get; set; }

		[AntiXss]
		public DateTime xts_actualarrivaldateandtime { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_customer { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_customerpubliclinkidname { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public string xts_workorder { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xts_globalworkorderhistoryid { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public Guid xts_customerpubliclinkid { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_globalworkorderhistory { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
