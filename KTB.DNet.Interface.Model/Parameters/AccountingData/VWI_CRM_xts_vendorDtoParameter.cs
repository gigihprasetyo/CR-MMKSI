#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vendorParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 1/2/2020 2:39:06 PM
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
    public class VWI_CRM_xts_vendorParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string xts_shortname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string xts_vendor { get; set; }

		[AntiXss]
		public string xts_mainprovinceidname { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_methodofpaymentidname { get; set; }

		[AntiXss]
		public bool xts_requirepurchaseorderapproval { get; set; }

		[AntiXss]
		public string xts_description { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string xts_insurancecompanytypename { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid xts_classid { get; set; }

		[AntiXss]
		public string xts_requireponumberforaccountpayablename { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_billtoaddress1 { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string xts_taxzoneidname { get; set; }

		[AntiXss]
		public string xts_salutation { get; set; }

		[AntiXss]
		public string xts_billtocountryidname { get; set; }

		[AntiXss]
		public string xts_internalnumber { get; set; }

		[AntiXss]
		public string xts_mainmobilephone { get; set; }

		[AntiXss]
		public bool ktb_registrationagency { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_billtovillageandstreetid { get; set; }

		[AntiXss]
		public string xts_billtovillageandstreetidname { get; set; }

		[AntiXss]
		public string xts_billtoaddress3 { get; set; }

		[AntiXss]
		public string xts_type { get; set; }

		[AntiXss]
		public Guid xts_billtoprovinceid { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_maincityid { get; set; }

		[AntiXss]
		public string xts_mainaddress2 { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public string xts_taxregistrationnumber { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_mainotherphone { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_billtowebsite { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xts_termofpaymentid { get; set; }

		[AntiXss]
		public string xts_classidname { get; set; }

		[AntiXss]
		public string xts_billtomobilephone { get; set; }

		[AntiXss]
		public string xts_termofpaymentidname { get; set; }

		[AntiXss]
		public Guid xts_taxzoneid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public Guid xts_maincountryid { get; set; }

		[AntiXss]
		public Guid xts_billtocountryid { get; set; }

		[AntiXss]
		public string xts_billtofax { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_mainfax { get; set; }

		[AntiXss]
		public bool xts_automaticallycreateapvoucher { get; set; }

		[AntiXss]
		public string xts_billtoaddress4 { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string xts_mainaddress4 { get; set; }

		[AntiXss]
		public string xts_typename { get; set; }

		[AntiXss]
		public string ktb_typename { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xts_vendornumber { get; set; }

		[AntiXss]
		public string xts_mainemail { get; set; }

		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		[AntiXss]
		public string xts_billtoemail { get; set; }

		[AntiXss]
		public string xts_mainpostalcode { get; set; }

		[AntiXss]
		public string xts_billtocityidname { get; set; }

		[AntiXss]
		public string xts_mainwebsite { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string xts_requirepurchaseorderapprovalname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_personincharge { get; set; }

		[AntiXss]
		public string xts_billtootherphone { get; set; }

		[AntiXss]
		public string xts_mainvillageandstreetidname { get; set; }

		[AntiXss]
		public string xts_automaticallycreateapvouchername { get; set; }

		[AntiXss]
		public string xts_aliasname { get; set; }

		[AntiXss]
		public string ktb_type { get; set; }

		[AntiXss]
		public string xts_billtopostalcode { get; set; }

		[AntiXss]
		public string xts_maincountryidname { get; set; }

		[AntiXss]
		public string xts_mainhomephone { get; set; }

		[AntiXss]
		public string ktb_registrationagencyname { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string ktb_leveldataname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_billtoaddress2 { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_mainaddress3 { get; set; }

		[AntiXss]
		public string xts_billtoprovinceidname { get; set; }

		[AntiXss]
		public Guid xts_mainvillageandstreetid { get; set; }

		[AntiXss]
		public Guid xts_mainprovinceid { get; set; }

		[AntiXss]
		public string xts_billtohomephone { get; set; }

		[AntiXss]
		public string xts_maincityidname { get; set; }

		[AntiXss]
		public Guid xts_vendorid { get; set; }

		[AntiXss]
		public string xts_mainbusinessphone { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_leveldata { get; set; }

		[AntiXss]
		public string xts_insurancecompanytype { get; set; }

		[AntiXss]
		public Guid xts_billtocityid { get; set; }

		[AntiXss]
		public string xts_billtobusinessphone { get; set; }

		[AntiXss]
		public bool xts_requireponumberforaccountpayable { get; set; }

		[AntiXss]
		public Guid xts_methodofpaymentid { get; set; }

		[AntiXss]
		public string xts_mainaddress1 { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
