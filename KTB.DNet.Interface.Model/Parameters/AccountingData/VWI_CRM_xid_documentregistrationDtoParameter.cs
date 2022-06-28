#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xid_documentregistrationParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:50
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
    public class VWI_CRM_xid_documentregistrationParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public Guid xid_registrationcolorid { get; set; }

		[AntiXss]
		public string xid_handling { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xid_financingcompanyidname { get; set; }

		[AntiXss]
		public string xid_documentregistrationnumber { get; set; }

		[AntiXss]
		public string xid_chasissno { get; set; }

		[AntiXss]
		public string xid_firststagename { get; set; }

		[AntiXss]
		public Guid xid_stocknumberid { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xid_nvexteriorcoloridname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string ktb_notes { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string xid_platenumber { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public Guid ktb_validnextdocumentregistrationid { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xid_financingcompanyid { get; set; }

		[AntiXss]
		public Guid xid_productid { get; set; }

		[AntiXss]
		public Guid ktb_spknameid { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string xid_customeridyominame { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public DateTime xid_transactiondate { get; set; }

		[AntiXss]
		public Guid xid_territoryid { get; set; }

		[AntiXss]
		public string xid_productidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public DateTime xid_vehicleregistrationinvoiceduedate { get; set; }

		[AntiXss]
		public Guid xid_nvexteriorcolorid { get; set; }

		[AntiXss]
		public string xid_financingponumber { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xid_handlingname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public Guid ktb_bpkbnameid { get; set; }

		[AntiXss]
		public string xid_businessunitidname { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string xid_customeridname { get; set; }

		[AntiXss]
		public string xid_cancellationnotes { get; set; }

		[AntiXss]
		public string xid_contactperson { get; set; }

		[AntiXss]
		public string ktb_spknameidyominame { get; set; }

		[AntiXss]
		public string xid_stocknumberidname { get; set; }

		[AntiXss]
		public string xid_contactpersonphone { get; set; }

		[AntiXss]
		public string ktb_validnextdocumentregistrationidname { get; set; }

		[AntiXss]
		public string xid_transactiontypename { get; set; }

		[AntiXss]
		public Guid xid_documentregistrationid { get; set; }

		[AntiXss]
		public string ktb_bpkbnameidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xid_progressstageidname { get; set; }

		[AntiXss]
		public Guid xid_registrationagencyid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public DateTime xid_vehicleregistrationvaliddate { get; set; }

		[AntiXss]
		public string xid_personinchargeidname { get; set; }

		[AntiXss]
		public string ktb_spkdetaildummyname { get; set; }

		[AntiXss]
		public Guid xid_businessunitid { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string xid_chassismodel { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xid_engineno { get; set; }

		[AntiXss]
		public string ktb_spknameidname { get; set; }

		[AntiXss]
		public int xid_stageordernumber { get; set; }

		[AntiXss]
		public Guid xid_customerid { get; set; }

		[AntiXss]
		public Guid xid_previousdocregistrationid { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xid_vehicleregistrationnumber { get; set; }

		[AntiXss]
		public string xid_invoicenumber { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xid_progressstageid { get; set; }

		[AntiXss]
		public string xid_previousdocregistrationidname { get; set; }

		[AntiXss]
		public string ktb_bpkbnameidyominame { get; set; }

		[AntiXss]
		public string xid_transactiontype { get; set; }

		[AntiXss]
		public string xid_statusname { get; set; }

		[AntiXss]
		public string xid_financingcompanyidyominame { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid xid_parentbusinessunitid { get; set; }

		[AntiXss]
		public string xid_vehicleregistrationaddress { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string xid_vehicleregistrationname { get; set; }

		[AntiXss]
		public string xid_registrationcoloridname { get; set; }

		[AntiXss]
		public Guid ktb_spkdetaildummy { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xid_businessunit { get; set; }

		[AntiXss]
		public string xid_status { get; set; }

		[AntiXss]
		public string xid_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string xid_territoryidname { get; set; }

		[AntiXss]
		public string xid_vehicleownershipcerificatenumber { get; set; }

		[AntiXss]
		public bool xid_firststage { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid xid_personinchargeid { get; set; }

		[AntiXss]
		public Guid xid_newvehiclesalesorderid { get; set; }

		[AntiXss]
		public string xid_registrationagencyidname { get; set; }

		[AntiXss]
		public string xid_productdescription { get; set; }

		[AntiXss]
		public string xid_newvehiclesalesorderidname { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
