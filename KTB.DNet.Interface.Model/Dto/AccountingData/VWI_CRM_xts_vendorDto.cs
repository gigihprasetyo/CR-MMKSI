#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vendorDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 1/2/2020 2:39:05 PM
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_xts_vendorDto : DtoBase
    {
        public string company { get; set; }

		public string xts_shortname { get; set; }

		public Int64 versionnumber { get; set; }

		public string xts_consumptiontax2idname { get; set; }

		public DateTime createdon { get; set; }

		public string xts_vendor { get; set; }

		public string xts_mainprovinceidname { get; set; }

		public Guid xts_consumptiontax1id { get; set; }

		public string statuscodename { get; set; }

		public string xts_methodofpaymentidname { get; set; }

		public bool xts_requirepurchaseorderapproval { get; set; }

		public string xts_description { get; set; }

		public string modifiedbyyominame { get; set; }

		public string xts_insurancecompanytypename { get; set; }

		public string owneridtype { get; set; }

		public Guid xts_classid { get; set; }

		public string xts_requireponumberforaccountpayablename { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string xts_billtoaddress1 { get; set; }

		public string modifiedbyname { get; set; }

		public string xts_taxzoneidname { get; set; }

		public string xts_salutation { get; set; }

		public string xts_billtocountryidname { get; set; }

		public string xts_internalnumber { get; set; }

		public string xts_mainmobilephone { get; set; }

		public bool ktb_registrationagency { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public Guid xts_billtovillageandstreetid { get; set; }

		public string xts_billtovillageandstreetidname { get; set; }

		public string xts_billtoaddress3 { get; set; }

		public string xts_type { get; set; }

		public Guid xts_billtoprovinceid { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public Guid xts_maincityid { get; set; }

		public string xts_mainaddress2 { get; set; }

		public Guid xts_consumptiontax2id { get; set; }

		public string xts_taxregistrationnumber { get; set; }

		public Guid owningteam { get; set; }

		public string xts_mainotherphone { get; set; }

		public int statecode { get; set; }

		public string xts_billtowebsite { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public Guid xts_termofpaymentid { get; set; }

		public string xts_classidname { get; set; }

		public string xts_billtomobilephone { get; set; }

		public string xts_termofpaymentidname { get; set; }

		public Guid xts_taxzoneid { get; set; }

		public string xts_locking { get; set; }

		public Guid xts_maincountryid { get; set; }

		public Guid xts_billtocountryid { get; set; }

		public string xts_billtofax { get; set; }

		public string xts_consumptiontax1idname { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string xts_mainfax { get; set; }

		public bool xts_automaticallycreateapvoucher { get; set; }

		public string xts_billtoaddress4 { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string owneridname { get; set; }

		public string xts_mainaddress4 { get; set; }

		public string xts_typename { get; set; }

		public string ktb_typename { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public string xts_vendornumber { get; set; }

		public string xts_mainemail { get; set; }

		public string xts_pkcombinationkey { get; set; }

		public string xts_billtoemail { get; set; }

		public string xts_mainpostalcode { get; set; }

		public string xts_billtocityidname { get; set; }

		public string xts_mainwebsite { get; set; }

		public string createdbyname { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_requirepurchaseorderapprovalname { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string xts_personincharge { get; set; }

		public string xts_billtootherphone { get; set; }

		public string xts_mainvillageandstreetidname { get; set; }

		public string xts_automaticallycreateapvouchername { get; set; }

		public string xts_aliasname { get; set; }

		public string ktb_type { get; set; }

		public string xts_billtopostalcode { get; set; }

		public string xts_maincountryidname { get; set; }

		public string xts_mainhomephone { get; set; }

		public string ktb_registrationagencyname { get; set; }

		public Guid createdby { get; set; }

		public string ktb_leveldataname { get; set; }

		public Guid modifiedby { get; set; }

		public string xts_billtoaddress2 { get; set; }

		public string createdbyyominame { get; set; }

		public Guid owninguser { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string xts_mainaddress3 { get; set; }

		public string xts_billtoprovinceidname { get; set; }

		public Guid xts_mainvillageandstreetid { get; set; }

		public Guid xts_mainprovinceid { get; set; }

		public string xts_billtohomephone { get; set; }

		public string xts_maincityidname { get; set; }

		public Guid xts_vendorid { get; set; }

		public string xts_mainbusinessphone { get; set; }

		public int statuscode { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string ktb_leveldata { get; set; }

		public string xts_insurancecompanytype { get; set; }

		public Guid xts_billtocityid { get; set; }

		public string xts_billtobusinessphone { get; set; }

		public bool xts_requireponumberforaccountpayable { get; set; }

		public Guid xts_methodofpaymentid { get; set; }

		public string xts_mainaddress1 { get; set; }

		public string statecodename { get; set; }

		public string xts_businessunitidname { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
