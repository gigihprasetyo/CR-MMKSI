#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_leadParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:48
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
    public class VWI_CRM_leadParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string ktb_bucompany { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string ktb_statusreason { get; set; }

		[AntiXss]
		public Guid xts_leadsourceid { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public string address1_county { get; set; }

		[AntiXss]
		public int ktb_dnetid { get; set; }

		[AntiXss]
		public string yomilastname { get; set; }

		[AntiXss]
		public string contactidname { get; set; }

		[AntiXss]
		public string xts_residentialtypeidname { get; set; }

		[AntiXss]
		public Guid leadid { get; set; }

		[AntiXss]
		public string salesstagename { get; set; }

		[AntiXss]
		public bool evaluatefit { get; set; }

		[AntiXss]
		public bool confirminterest { get; set; }

		[AntiXss]
		public Guid address1_addressid { get; set; }

		[AntiXss]
		public string ktb_statusreasonname { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public decimal address1_longitude { get; set; }

		[AntiXss]
		public string xts_leadpurposename { get; set; }

		[AntiXss]
		public Guid entityimageid { get; set; }

		[AntiXss]
		public string telephone2 { get; set; }

		[AntiXss]
		public string address1_shippingmethodcodename { get; set; }

		[AntiXss]
		public int address2_utcoffset { get; set; }

		[AntiXss]
		public string address1_upszone { get; set; }

		[AntiXss]
		public bool xts_homevisit { get; set; }

		[AntiXss]
		public DateTime xts_birthdate { get; set; }

		[AntiXss]
		public Guid originatingcaseid { get; set; }

		[AntiXss]
		public string masterleadidname { get; set; }

		[AntiXss]
		public string address1_addresstypecode { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public bool donotpostalmail { get; set; }

		[AntiXss]
		public string xts_countryidname { get; set; }

		[AntiXss]
		public DateTime lastonholdtime { get; set; }

		[AntiXss]
		public string xts_homevisitname { get; set; }

		[AntiXss]
		public string customeridname { get; set; }

		[AntiXss]
		public DateTime xts_estimatedpurchasedate { get; set; }

		[AntiXss]
		public string qualifyingopportunityidname { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public string purchasetimeframe { get; set; }

		[AntiXss]
		public string address1_telephone1 { get; set; }

		[AntiXss]
		public string address1_telephone2 { get; set; }

		[AntiXss]
		public string address1_telephone3 { get; set; }

		[AntiXss]
		public string address2_upszone { get; set; }

		[AntiXss]
		public string fax { get; set; }

		[AntiXss]
		public Guid xts_vehiclebrandid { get; set; }

		[AntiXss]
		public string xts_villageandstreetidname { get; set; }

		[AntiXss]
		public string accountidyominame { get; set; }

		[AntiXss]
		public Guid parentaccountid { get; set; }

		[AntiXss]
		public DateTime xts_disorqualifieddate { get; set; }

		[AntiXss]
		public string slainvokedidname { get; set; }

		[AntiXss]
		public Guid ktb_vehiclemodelid { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string address1_city { get; set; }

		[AntiXss]
		public string xts_leadsourceidname { get; set; }

		[AntiXss]
		public string fullname { get; set; }

		[AntiXss]
		public string preferredcontactmethodcodename { get; set; }

		[AntiXss]
		public Guid xts_categoryid { get; set; }

		[AntiXss]
		public decimal address2_longitude { get; set; }

		[AntiXss]
		public string xts_agesegmentname { get; set; }

		[AntiXss]
		public string slaname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_provinceid { get; set; }

		[AntiXss]
		public string initialcommunication { get; set; }

		[AntiXss]
		public string xts_vehiclebrandidname { get; set; }

		[AntiXss]
		public decimal address1_latitude { get; set; }

		[AntiXss]
		public string ktb_customerpurpose { get; set; }

		[AntiXss]
		public string customeridtype { get; set; }

		[AntiXss]
		public string xts_companysizeidname { get; set; }

		[AntiXss]
		public string confirminterestname { get; set; }

		[AntiXss]
		public string leadqualitycode { get; set; }

		[AntiXss]
		public string salesstagecode { get; set; }

		[AntiXss]
		public decimal estimatedamount { get; set; }

		[AntiXss]
		public string address2_telephone1 { get; set; }

		[AntiXss]
		public Guid xts_jobtitleid { get; set; }

		[AntiXss]
		public string ktb_interfaceexceptionmessage { get; set; }

		[AntiXss]
		public string pager { get; set; }

		[AntiXss]
		public string xto_reasonforvisitname { get; set; }

		[AntiXss]
		public string salutation { get; set; }

		[AntiXss]
		public string initialcommunicationname { get; set; }

		[AntiXss]
		public string originatingcaseidname { get; set; }

		[AntiXss]
		public string accountidname { get; set; }

		[AntiXss]
		public string needname { get; set; }

		[AntiXss]
		public string xts_manufactureridname { get; set; }

		[AntiXss]
		public string address2_city { get; set; }

		[AntiXss]
		public string xts_identificationtype { get; set; }

		[AntiXss]
		public Guid xts_cityid { get; set; }

		[AntiXss]
		public string subject { get; set; }

		[AntiXss]
		public string address2_line2 { get; set; }

		[AntiXss]
		public string prioritycodename { get; set; }

		[AntiXss]
		public string xts_customernumber { get; set; }

		[AntiXss]
		public string ktb_eventdata { get; set; }

		[AntiXss]
		public string leadqualitycodename { get; set; }

		[AntiXss]
		public string xts_gender { get; set; }

		[AntiXss]
		public string isautocreatename { get; set; }

		[AntiXss]
		public Guid xts_originatingcampaignresponseid { get; set; }

		[AntiXss]
		public string xto_registrationcode { get; set; }

		[AntiXss]
		public string entityimage_url { get; set; }

		[AntiXss]
		public string xts_categoryidname { get; set; }

		[AntiXss]
		public Guid qualifyingopportunityid { get; set; }

		[AntiXss]
		public string mergedname { get; set; }

		[AntiXss]
		public bool msdyn_gdproptout { get; set; }

		[AntiXss]
		public Guid parentcontactid { get; set; }

		[AntiXss]
		public Guid xts_customerclassid { get; set; }

		[AntiXss]
		public Guid relatedobjectid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string industrycode { get; set; }

		[AntiXss]
		public string industrycodename { get; set; }

		[AntiXss]
		public Guid ktb_vehiclecategoryid { get; set; }

		[AntiXss]
		public string msdyn_gdproptoutname { get; set; }

		[AntiXss]
		public string ktb_vehiclecategoryidname { get; set; }

		[AntiXss]
		public string address1_postofficebox { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string xts_salesobjectivename { get; set; }

		[AntiXss]
		public string xts_jobtitleidname { get; set; }

		[AntiXss]
		public string yomifullname { get; set; }

		[AntiXss]
		public Guid xts_manufacturerid { get; set; }

		[AntiXss]
		public string xts_originatingcampaignresponseidname { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string xts_preferredvehiclemodel { get; set; }

		[AntiXss]
		public string ktb_vehiclemodelidname { get; set; }

		[AntiXss]
		public string description { get; set; }

		[AntiXss]
		public string ktb_informationsourcename { get; set; }

		[AntiXss]
		public string campaignidname { get; set; }

		[AntiXss]
		public string address2_postalcode { get; set; }

		[AntiXss]
		public string decisionmakername { get; set; }

		[AntiXss]
		public string websiteurl { get; set; }

		[AntiXss]
		public string parentcontactidname { get; set; }

		[AntiXss]
		public int address1_utcoffset { get; set; }

		[AntiXss]
		public string yomimiddlename { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_maritalstatus { get; set; }

		[AntiXss]
		public string donotemailname { get; set; }

		[AntiXss]
		public string address1_line3 { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string ktb_statusinterfacednet { get; set; }

		[AntiXss]
		public string ktb_vehicletypecode { get; set; }

		[AntiXss]
		public string address2_line1 { get; set; }

		[AntiXss]
		public string evaluatefitname { get; set; }

		[AntiXss]
		public string telephone3 { get; set; }

		[AntiXss]
		public string middlename { get; set; }

		[AntiXss]
		public string address2_stateorprovince { get; set; }

		[AntiXss]
		public string xts_hobbyidname { get; set; }

		[AntiXss]
		public string address2_name { get; set; }

		[AntiXss]
		public string entityimage { get; set; }

		[AntiXss]
		public string businesscardattributes { get; set; }

		[AntiXss]
		public string purchasetimeframename { get; set; }

		[AntiXss]
		public string xts_vendoridname { get; set; }

		[AntiXss]
		public string xts_religionidname { get; set; }

		[AntiXss]
		public bool donotsendmm { get; set; }

		[AntiXss]
		public bool donotbulkemail { get; set; }

		[AntiXss]
		public string donotsendmarketingmaterialname { get; set; }

		[AntiXss]
		public string ktb_vehicledescription { get; set; }

		[AntiXss]
		public string donotfaxname { get; set; }

		[AntiXss]
		public string address1_name { get; set; }

		[AntiXss]
		public string address2_telephone2 { get; set; }

		[AntiXss]
		public string address2_telephone3 { get; set; }

		[AntiXss]
		public string address1_postalcode { get; set; }

		[AntiXss]
		public int xto_quantity { get; set; }

		[AntiXss]
		public decimal budgetamount { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid slaid { get; set; }

		[AntiXss]
		public string address1_stateorprovince { get; set; }

		[AntiXss]
		public string xts_aliasname { get; set; }

		[AntiXss]
		public bool followemail { get; set; }

		[AntiXss]
		public DateTime schedulefollowup_qualify { get; set; }

		[AntiXss]
		public string jobtitle { get; set; }

		[AntiXss]
		public Guid xts_industryid { get; set; }

		[AntiXss]
		public string parentaccountidname { get; set; }

		[AntiXss]
		public Guid xts_vehiclemodelid { get; set; }

		[AntiXss]
		public string yomicompanyname { get; set; }

		[AntiXss]
		public string xts_vendorclassidname { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public DateTime schedulefollowup_prospect { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public DateTime lastusedincampaign { get; set; }

		[AntiXss]
		public string xts_identificationtypename { get; set; }

		[AntiXss]
		public string address2_shippingmethodcodename { get; set; }

		[AntiXss]
		public string xts_ownershipidname { get; set; }

		[AntiXss]
		public string address1_shippingmethodcode { get; set; }

		[AntiXss]
		public string budgetstatus { get; set; }

		[AntiXss]
		public DateTime xts_anniversarydate { get; set; }

		[AntiXss]
		public Guid xts_ownershipid { get; set; }

		[AntiXss]
		public string address2_addresstypecodename { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public string address2_fax { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string preferredcontactmethodcode { get; set; }

		[AntiXss]
		public string xts_existingmanufacturer { get; set; }

		[AntiXss]
		public string xts_taxregistrationnumber { get; set; }

		[AntiXss]
		public string companyname { get; set; }

		[AntiXss]
		public string ktb_customerpurposename { get; set; }

		[AntiXss]
		public string purchaseprocessname { get; set; }

		[AntiXss]
		public string participatesinworkflowname { get; set; }

		[AntiXss]
		public int teamsfollowed { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_informationsource { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public bool decisionmaker { get; set; }

		[AntiXss]
		public string leadsourcecode { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public string masterleadidyominame { get; set; }

		[AntiXss]
		public decimal revenue { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid slainvokedid { get; set; }

		[AntiXss]
		public string sic { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public Int64 entityimage_timestamp { get; set; }

		[AntiXss]
		public bool participatesinworkflow { get; set; }

		[AntiXss]
		public Guid xts_vendorid { get; set; }

		[AntiXss]
		public string address2_postofficebox { get; set; }

		[AntiXss]
		public string prioritycode { get; set; }

		[AntiXss]
		public string xts_salesobjective { get; set; }

		[AntiXss]
		public string emailaddress2 { get; set; }

		[AntiXss]
		public string emailaddress1 { get; set; }

		[AntiXss]
		public string leadsourcecodename { get; set; }

		[AntiXss]
		public string lastname { get; set; }

		[AntiXss]
		public string purchaseprocess { get; set; }

		[AntiXss]
		public string budgetstatusname { get; set; }

		[AntiXss]
		public Guid xts_hobbyid { get; set; }

		[AntiXss]
		public decimal estimatedvalue { get; set; }

		[AntiXss]
		public string xts_leadpurpose { get; set; }

		[AntiXss]
		public bool merged { get; set; }

		[AntiXss]
		public int onholdtime { get; set; }

		[AntiXss]
		public string xto_typeofvisitname { get; set; }

		[AntiXss]
		public decimal estimatedamount_base { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public string xts_provinceidname { get; set; }

		[AntiXss]
		public string need { get; set; }

		[AntiXss]
		public string isprivatename { get; set; }

		[AntiXss]
		public string customeridyominame { get; set; }

		[AntiXss]
		public string contactidyominame { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string salesstagecodename { get; set; }

		[AntiXss]
		public Guid campaignid { get; set; }

		[AntiXss]
		public string address2_addresstypecode { get; set; }

		[AntiXss]
		public Guid xts_residentialtypeid { get; set; }

		[AntiXss]
		public int ktb_qty { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid xts_religionid { get; set; }

		[AntiXss]
		public Guid address2_addressid { get; set; }

		[AntiXss]
		public string address1_composite { get; set; }

		[AntiXss]
		public string xts_address4 { get; set; }

		[AntiXss]
		public Guid xts_countryid { get; set; }

		[AntiXss]
		public bool donotfax { get; set; }

		[AntiXss]
		public string xts_customerclassidname { get; set; }

		[AntiXss]
		public bool isprivate { get; set; }

		[AntiXss]
		public string address2_shippingmethodcode { get; set; }

		[AntiXss]
		public decimal budgetamount_base { get; set; }

		[AntiXss]
		public decimal revenue_base { get; set; }

		[AntiXss]
		public string address2_country { get; set; }

		[AntiXss]
		public string xts_gendername { get; set; }

		[AntiXss]
		public string mobilephone { get; set; }

		[AntiXss]
		public string xts_cityidname { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public string xto_typeofvisit { get; set; }

		[AntiXss]
		public string xts_agesegment { get; set; }

		[AntiXss]
		public string xts_identificationnumber { get; set; }

		[AntiXss]
		public string yomifirstname { get; set; }

		[AntiXss]
		public string parentcontactidyominame { get; set; }

		[AntiXss]
		public string address1_country { get; set; }

		[AntiXss]
		public string ktb_vehiclecolorname { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public string firstname { get; set; }

		[AntiXss]
		public string xts_maritalstatusname { get; set; }

		[AntiXss]
		public Guid customerid { get; set; }

		[AntiXss]
		public Guid xts_villageandstreetid { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public string ktb_statusinterfacednetname { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public DateTime ktb_prospectdate { get; set; }

		[AntiXss]
		public bool donotphone { get; set; }

		[AntiXss]
		public string address1_line1 { get; set; }

		[AntiXss]
		public Guid masterid { get; set; }

		[AntiXss]
		public decimal xts_baseprice_base { get; set; }

		[AntiXss]
		public string ktb_webid { get; set; }

		[AntiXss]
		public int ktb_dnetspkcustomerid { get; set; }

		[AntiXss]
		public string timespentbymeonemailandmeetings { get; set; }

		[AntiXss]
		public decimal address2_latitude { get; set; }

		[AntiXss]
		public string salesstage { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public decimal xts_baseprice { get; set; }

		[AntiXss]
		public string parentaccountidyominame { get; set; }

		[AntiXss]
		public Guid xts_companysizeid { get; set; }

		[AntiXss]
		public string ktb_informationtype { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string address2_county { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public Guid xts_vendorclassid { get; set; }

		[AntiXss]
		public Guid contactid { get; set; }

		[AntiXss]
		public string telephone1 { get; set; }

		[AntiXss]
		public string donotpostalmailname { get; set; }

		[AntiXss]
		public string relatedobjectidname { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string address1_line2 { get; set; }

		[AntiXss]
		public string address2_line3 { get; set; }

		[AntiXss]
		public string donotphonename { get; set; }

		[AntiXss]
		public DateTime xts_leaddate { get; set; }

		[AntiXss]
		public string xts_existingvehiclemodel { get; set; }

		[AntiXss]
		public string xts_vehiclemodelidname { get; set; }

		[AntiXss]
		public bool isautocreate { get; set; }

		[AntiXss]
		public bool donotemail { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public string followemailname { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string ktb_informationtypename { get; set; }

		[AntiXss]
		public Guid accountid { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string businesscard { get; set; }

		[AntiXss]
		public string qualificationcomments { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string emailaddress3 { get; set; }

		[AntiXss]
		public string address2_composite { get; set; }

		[AntiXss]
		public string donotbulkemailname { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public DateTime estimatedclosedate { get; set; }

		[AntiXss]
		public int numberofemployees { get; set; }

		[AntiXss]
		public string address1_fax { get; set; }

		[AntiXss]
		public string xto_reasonforvisit { get; set; }

		[AntiXss]
		public string xts_industryidname { get; set; }

		[AntiXss]
		public string address1_addresstypecodename { get; set; }

		[AntiXss]
		public string xts_leadcategoryname { get; set; }

		[AntiXss]
		public string xts_leadcategory { get; set; }

		[AntiXss]
		public string xts_contactpersonidname { get; set; }

		[AntiXss]
		public Guid xts_contactpersonid { get; set; }

		[AntiXss]
		public string xts_contactpersonidyominame { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
