#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_contactParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:46
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
    public class VWI_CRM_contactParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string ktb_tipepelanggan { get; set; }

		[AntiXss]
		public string customertypecodename { get; set; }

		[AntiXss]
		public string xts_originatingcampaignresponseidname { get; set; }

		[AntiXss]
		public string address1_county { get; set; }

		[AntiXss]
		public string xts_customertypename { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_sourcecampaignidname { get; set; }

		[AntiXss]
		public string department { get; set; }

		[AntiXss]
		public string haschildrencode { get; set; }

		[AntiXss]
		public Guid address1_addressid { get; set; }

		[AntiXss]
		public bool isbackofficecustomer { get; set; }

		[AntiXss]
		public Guid address3_addressid { get; set; }

		[AntiXss]
		public decimal address1_longitude { get; set; }

		[AntiXss]
		public string managername { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public Guid entityimageid { get; set; }

		[AntiXss]
		public string telephone2 { get; set; }

		[AntiXss]
		public string address1_shippingmethodcodename { get; set; }

		[AntiXss]
		public string parentcustomeridtype { get; set; }

		[AntiXss]
		public int address2_utcoffset { get; set; }

		[AntiXss]
		public string customertypecode { get; set; }

		[AntiXss]
		public string address1_upszone { get; set; }

		[AntiXss]
		public bool xts_homevisit { get; set; }

		[AntiXss]
		public string address1_freighttermscode { get; set; }

		[AntiXss]
		public bool donotbulkpostalmail { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string gendercode { get; set; }

		[AntiXss]
		public bool donotpostalmail { get; set; }

		[AntiXss]
		public string xts_countryidname { get; set; }

		[AntiXss]
		public DateTime lastonholdtime { get; set; }

		[AntiXss]
		public string xts_homevisitname { get; set; }

		[AntiXss]
		public string address1_addresstypecode { get; set; }

		[AntiXss]
		public string telephone3 { get; set; }

		[AntiXss]
		public string xts_villageandstreetidname { get; set; }

		[AntiXss]
		public string address1_telephone1 { get; set; }

		[AntiXss]
		public string address1_telephone2 { get; set; }

		[AntiXss]
		public string address1_telephone3 { get; set; }

		[AntiXss]
		public string address1_fax { get; set; }

		[AntiXss]
		public string address2_upszone { get; set; }

		[AntiXss]
		public string address3_telephone2 { get; set; }

		[AntiXss]
		public string parentcustomeridname { get; set; }

		[AntiXss]
		public string fax { get; set; }

		[AntiXss]
		public string preferredappointmentdaycodename { get; set; }

		[AntiXss]
		public string assistantname { get; set; }

		[AntiXss]
		public string address2_line3 { get; set; }

		[AntiXss]
		public string xts_customeridyominame { get; set; }

		[AntiXss]
		public string accountidyominame { get; set; }

		[AntiXss]
		public string parentcontactidyominame { get; set; }

		[AntiXss]
		public string slainvokedidname { get; set; }

		[AntiXss]
		public string address2_addresstypecode { get; set; }

		[AntiXss]
		public string defaultpricelevelidname { get; set; }

		[AntiXss]
		public string fullname { get; set; }

		[AntiXss]
		public Guid xts_categoryid { get; set; }

		[AntiXss]
		public decimal address2_longitude { get; set; }

		[AntiXss]
		public Guid subscriptionid { get; set; }

		[AntiXss]
		public string originatingleadidname { get; set; }

		[AntiXss]
		public string slaname { get; set; }

		[AntiXss]
		public Guid xts_industryid { get; set; }

		[AntiXss]
		public string familystatuscodename { get; set; }

		[AntiXss]
		public string createdbyexternalpartyname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public decimal address1_latitude { get; set; }

		[AntiXss]
		public bool xts_isdialog { get; set; }

		[AntiXss]
		public decimal aging30 { get; set; }

		[AntiXss]
		public string xts_companysizeidname { get; set; }

		[AntiXss]
		public string paymenttermscodename { get; set; }

		[AntiXss]
		public DateTime lastusedincampaign { get; set; }

		[AntiXss]
		public Guid xts_sourcecampaignid { get; set; }

		[AntiXss]
		public string address3_fax { get; set; }

		[AntiXss]
		public string shippingmethodcode { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string address2_telephone1 { get; set; }

		[AntiXss]
		public string preferredappointmenttimecode { get; set; }

		[AntiXss]
		public decimal creditlimit_base { get; set; }

		[AntiXss]
		public Guid preferredserviceid { get; set; }

		[AntiXss]
		public string pager { get; set; }

		[AntiXss]
		public string salutation { get; set; }

		[AntiXss]
		public string accountidname { get; set; }

		[AntiXss]
		public Guid xts_residentialtypeid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string preferredserviceidname { get; set; }

		[AntiXss]
		public string address2_city { get; set; }

		[AntiXss]
		public Guid xts_ownershipid { get; set; }

		[AntiXss]
		public string address3_postalcode { get; set; }

		[AntiXss]
		public string address2_line2 { get; set; }

		[AntiXss]
		public decimal aging60 { get; set; }

		[AntiXss]
		public decimal aging90 { get; set; }

		[AntiXss]
		public string address3_upszone { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string address3_county { get; set; }

		[AntiXss]
		public string entityimage_url { get; set; }

		[AntiXss]
		public string governmentid { get; set; }

		[AntiXss]
		public string xts_categoryidname { get; set; }

		[AntiXss]
		public string callback { get; set; }

		[AntiXss]
		public bool creditonhold { get; set; }

		[AntiXss]
		public string xts_isdialogname { get; set; }

		[AntiXss]
		public string mergedname { get; set; }

		[AntiXss]
		public bool msdyn_gdproptout { get; set; }

		[AntiXss]
		public Guid parentcontactid { get; set; }

		[AntiXss]
		public Guid xts_customerclassid { get; set; }

		[AntiXss]
		public string address2_freighttermscode { get; set; }

		[AntiXss]
		public string xts_converttocustomeridname { get; set; }

		[AntiXss]
		public string business2 { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_preareaname { get; set; }

		[AntiXss]
		public string msdyn_gdproptoutname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string msdyn_orgchangestatus { get; set; }

		[AntiXss]
		public string marketingonlyname { get; set; }

		[AntiXss]
		public string yomifullname { get; set; }

		[AntiXss]
		public string address2_telephone2 { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string description { get; set; }

		[AntiXss]
		public string originatingleadidyominame { get; set; }

		[AntiXss]
		public string ktb_pobox { get; set; }

		[AntiXss]
		public bool xts_deactivatecontact { get; set; }

		[AntiXss]
		public string address2_postalcode { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public DateTime birthdate { get; set; }

		[AntiXss]
		public string parentcontactidname { get; set; }

		[AntiXss]
		public string spousesname { get; set; }

		[AntiXss]
		public Guid parentcustomerid { get; set; }

		[AntiXss]
		public int address1_utcoffset { get; set; }

		[AntiXss]
		public string yomimiddlename { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public Guid modifiedbyexternalparty { get; set; }

		[AntiXss]
		public string donotemailname { get; set; }

		[AntiXss]
		public string address1_line3 { get; set; }

		[AntiXss]
		public string address3_country { get; set; }

		[AntiXss]
		public string territorycodename { get; set; }

		[AntiXss]
		public string paymenttermscode { get; set; }

		[AntiXss]
		public string address3_telephone1 { get; set; }

		[AntiXss]
		public string haschildrencodename { get; set; }

		[AntiXss]
		public string address2_line1 { get; set; }

		[AntiXss]
		public string accountrolecode { get; set; }

		[AntiXss]
		public string middlename { get; set; }

		[AntiXss]
		public string customersizecodename { get; set; }

		[AntiXss]
		public string managerphone { get; set; }

		[AntiXss]
		public string address2_stateorprovince { get; set; }

		[AntiXss]
		public string xts_converttocustomeridyominame { get; set; }

		[AntiXss]
		public string xts_button { get; set; }

		[AntiXss]
		public string xts_hobbyidname { get; set; }

		[AntiXss]
		public string childrensnames { get; set; }

		[AntiXss]
		public string address2_name { get; set; }

		[AntiXss]
		public string address3_stateorprovince { get; set; }

		[AntiXss]
		public string entityimage { get; set; }

		[AntiXss]
		public string address3_line1 { get; set; }

		[AntiXss]
		public Guid slainvokedid { get; set; }

		[AntiXss]
		public string businesscardattributes { get; set; }

		[AntiXss]
		public DateTime anniversary { get; set; }

		[AntiXss]
		public string donotbulkpostalmailname { get; set; }

		[AntiXss]
		public string xts_religionidname { get; set; }

		[AntiXss]
		public string educationcodename { get; set; }

		[AntiXss]
		public bool donotsendmm { get; set; }

		[AntiXss]
		public bool donotbulkemail { get; set; }

		[AntiXss]
		public string donotsendmarketingmaterialname { get; set; }

		[AntiXss]
		public string address1_primarycontactname { get; set; }

		[AntiXss]
		public Guid preferredsystemuserid { get; set; }

		[AntiXss]
		public string donotfaxname { get; set; }

		[AntiXss]
		public string xts_deactivatecontactname { get; set; }

		[AntiXss]
		public string home2 { get; set; }

		[AntiXss]
		public string address1_name { get; set; }

		[AntiXss]
		public string address2_telephone3 { get; set; }

		[AntiXss]
		public string address3_primarycontactname { get; set; }

		[AntiXss]
		public string ftpsiteurl { get; set; }

		[AntiXss]
		public string xts_companyname { get; set; }

		[AntiXss]
		public string address1_postalcode { get; set; }

		[AntiXss]
		public string address3_freighttermscode { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid slaid { get; set; }

		[AntiXss]
		public Guid originatingleadid { get; set; }

		[AntiXss]
		public string address1_stateorprovince { get; set; }

		[AntiXss]
		public string xts_aliasname { get; set; }

		[AntiXss]
		public bool followemail { get; set; }

		[AntiXss]
		public Guid xts_jobtitleid { get; set; }

		[AntiXss]
		public decimal aging90_base { get; set; }

		[AntiXss]
		public string jobtitle { get; set; }

		[AntiXss]
		public Guid masterid { get; set; }

		[AntiXss]
		public string leadsourcecodename { get; set; }

		[AntiXss]
		public decimal annualincome_base { get; set; }

		[AntiXss]
		public string isbackofficecustomername { get; set; }

		[AntiXss]
		public Guid defaultpricelevelid { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string xts_identificationtype { get; set; }

		[AntiXss]
		public string xts_identificationtypename { get; set; }

		[AntiXss]
		public string address2_shippingmethodcodename { get; set; }

		[AntiXss]
		public string xts_ownershipidname { get; set; }

		[AntiXss]
		public string address1_shippingmethodcode { get; set; }

		[AntiXss]
		public string preferredappointmentdaycode { get; set; }

		[AntiXss]
		public string address2_addresstypecodename { get; set; }

		[AntiXss]
		public string address2_fax { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string customersizecode { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public decimal annualincome { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string xts_taxregistrationnumber { get; set; }

		[AntiXss]
		public string employeeid { get; set; }

		[AntiXss]
		public string address1_postofficebox { get; set; }

		[AntiXss]
		public string address1_city { get; set; }

		[AntiXss]
		public string participatesinworkflowname { get; set; }

		[AntiXss]
		public int teamsfollowed { get; set; }

		[AntiXss]
		public decimal creditlimit { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string address3_telephone3 { get; set; }

		[AntiXss]
		public string preferredcontactmethodcode { get; set; }

		[AntiXss]
		public string leadsourcecode { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public string creditonholdname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string address3_line2 { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid createdbyexternalparty { get; set; }

		[AntiXss]
		public decimal aging60_base { get; set; }

		[AntiXss]
		public Guid preferredequipmentid { get; set; }

		[AntiXss]
		public Int64 entityimage_timestamp { get; set; }

		[AntiXss]
		public bool participatesinworkflow { get; set; }

		[AntiXss]
		public string address1_freighttermscodename { get; set; }

		[AntiXss]
		public string address2_postofficebox { get; set; }

		[AntiXss]
		public string emailaddress3 { get; set; }

		[AntiXss]
		public string emailaddress2 { get; set; }

		[AntiXss]
		public string emailaddress1 { get; set; }

		[AntiXss]
		public Guid xts_provinceid { get; set; }

		[AntiXss]
		public string websiteurl { get; set; }

		[AntiXss]
		public string gendercodename { get; set; }

		[AntiXss]
		public string nickname { get; set; }

		[AntiXss]
		public string address3_name { get; set; }

		[AntiXss]
		public string lastname { get; set; }

		[AntiXss]
		public decimal address3_longitude { get; set; }

		[AntiXss]
		public Guid xts_cityid { get; set; }

		[AntiXss]
		public int address3_utcoffset { get; set; }

		[AntiXss]
		public Guid xts_hobbyid { get; set; }

		[AntiXss]
		public string ktb_prearea { get; set; }

		[AntiXss]
		public string xts_jobtitleidname { get; set; }

		[AntiXss]
		public bool merged { get; set; }

		[AntiXss]
		public int onholdtime { get; set; }

		[AntiXss]
		public Guid xts_originatingcampaignresponseid { get; set; }

		[AntiXss]
		public string preferredappointmenttimecodename { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_provinceidname { get; set; }

		[AntiXss]
		public bool marketingonly { get; set; }

		[AntiXss]
		public string preferredsystemuseridyominame { get; set; }

		[AntiXss]
		public string address3_shippingmethodcode { get; set; }

		[AntiXss]
		public string isprivatename { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string telephone1 { get; set; }

		[AntiXss]
		public string modifiedbyexternalpartyname { get; set; }

		[AntiXss]
		public string address3_postofficebox { get; set; }

		[AntiXss]
		public string ktb_tipepelangganname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string assistantphone { get; set; }

		[AntiXss]
		public Guid address2_addressid { get; set; }

		[AntiXss]
		public string address1_composite { get; set; }

		[AntiXss]
		public string xts_address4 { get; set; }

		[AntiXss]
		public bool xts_customertype { get; set; }

		[AntiXss]
		public Guid xts_countryid { get; set; }

		[AntiXss]
		public bool donotfax { get; set; }

		[AntiXss]
		public string xts_customerclassidname { get; set; }

		[AntiXss]
		public string xts_parentcustomernumber { get; set; }

		[AntiXss]
		public bool isprivate { get; set; }

		[AntiXss]
		public Guid xts_converttocustomerid { get; set; }

		[AntiXss]
		public string accountrolecodename { get; set; }

		[AntiXss]
		public string address3_line3 { get; set; }

		[AntiXss]
		public string address2_shippingmethodcode { get; set; }

		[AntiXss]
		public string ktb_leveldataname { get; set; }

		[AntiXss]
		public string address2_country { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public string mobilephone { get; set; }

		[AntiXss]
		public string xts_cityidname { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string address1_line1 { get; set; }

		[AntiXss]
		public string address2_freighttermscodename { get; set; }

		[AntiXss]
		public string createdbyexternalpartyyominame { get; set; }

		[AntiXss]
		public string xts_identificationnumber { get; set; }

		[AntiXss]
		public string mastercontactidyominame { get; set; }

		[AntiXss]
		public string yomifirstname { get; set; }

		[AntiXss]
		public string mastercontactidname { get; set; }

		[AntiXss]
		public string address3_city { get; set; }

		[AntiXss]
		public string address1_country { get; set; }

		[AntiXss]
		public string externaluseridentifier { get; set; }

		[AntiXss]
		public string modifiedbyexternalpartyyominame { get; set; }

		[AntiXss]
		public string firstname { get; set; }

		[AntiXss]
		public string xts_residentialtypeidname { get; set; }

		[AntiXss]
		public Guid xts_villageandstreetid { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string address3_addresstypecodename { get; set; }

		[AntiXss]
		public bool donotphone { get; set; }

		[AntiXss]
		public Guid xts_religionid { get; set; }

		[AntiXss]
		public string suffix { get; set; }

		[AntiXss]
		public string territorycode { get; set; }

		[AntiXss]
		public string parentcustomeridyominame { get; set; }

		[AntiXss]
		public string timespentbymeonemailandmeetings { get; set; }

		[AntiXss]
		public decimal address2_latitude { get; set; }

		[AntiXss]
		public string address3_composite { get; set; }

		[AntiXss]
		public string address3_addresstypecode { get; set; }

		[AntiXss]
		public decimal aging30_base { get; set; }

		[AntiXss]
		public Guid xts_companysizeid { get; set; }

		[AntiXss]
		public string preferredcontactmethodcodename { get; set; }

		[AntiXss]
		public string familystatuscode { get; set; }

		[AntiXss]
		public string preferredsystemuseridname { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string address2_county { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public Guid contactid { get; set; }

		[AntiXss]
		public string address2_primarycontactname { get; set; }

		[AntiXss]
		public string donotpostalmailname { get; set; }

		[AntiXss]
		public string address3_freighttermscodename { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string address1_line2 { get; set; }

		[AntiXss]
		public string donotphonename { get; set; }

		[AntiXss]
		public bool isautocreate { get; set; }

		[AntiXss]
		public bool donotemail { get; set; }

		[AntiXss]
		public string yomilastname { get; set; }

		[AntiXss]
		public string followemailname { get; set; }

		[AntiXss]
		public string educationcode { get; set; }

		[AntiXss]
		public string address3_shippingmethodcodename { get; set; }

		[AntiXss]
		public string ktb_leveldata { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public Guid accountid { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string businesscard { get; set; }

		[AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string preferredequipmentidname { get; set; }

		[AntiXss]
		public string shippingmethodcodename { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string msdyn_orgchangestatusname { get; set; }

		[AntiXss]
		public string address2_composite { get; set; }

		[AntiXss]
		public string donotbulkemailname { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public int numberofchildren { get; set; }

		[AntiXss]
		public string xts_industryidname { get; set; }

		[AntiXss]
		public string address1_addresstypecodename { get; set; }

		[AntiXss]
		public decimal address3_latitude { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
