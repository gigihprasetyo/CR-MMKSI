#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_accountParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/01/2020 8:22:47
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
    public class VWI_CRM_accountParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string address2_city { get; set; }

		[AntiXss]
		public string ktb_tipeperusahaanname { get; set; }

		[AntiXss]
		public string ktb_tipepelanggan { get; set; }

		[AntiXss]
		public string customertypecodename { get; set; }

		[AntiXss]
		public string ktb_isinterfacedname { get; set; }

		[AntiXss]
		public string xts_termofpaymentidname { get; set; }

		[AntiXss]
		public string address1_county { get; set; }

		[AntiXss]
		public DateTime openrevenue_date { get; set; }

		[AntiXss]
		public string xts_customertypename { get; set; }

		[AntiXss]
		public string xjp_driverlicensename { get; set; }

		[AntiXss]
		public string xts_residentialtypeidname { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public string xts_originatingcontactidyominame { get; set; }

		[AntiXss]
		public string xts_saturdayname { get; set; }

		[AntiXss]
		public string parentaccountidname { get; set; }

		[AntiXss]
		public Guid address1_addressid { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public decimal address1_longitude { get; set; }

		[AntiXss]
		public string xts_ignoredownpaymentname { get; set; }

		[AntiXss]
		public string telephone2 { get; set; }

		[AntiXss]
		public string address1_shippingmethodcodename { get; set; }

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
		public string ktb_groupkategoriname { get; set; }

		[AntiXss]
		public DateTime xts_birthdate { get; set; }

		[AntiXss]
		public string accountnumber { get; set; }

		[AntiXss]
		public string xts_originatingcontactidname { get; set; }

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
		public string address1_addresstypecode { get; set; }

		[AntiXss]
		public string xts_customerclasstype { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public string xts_villageandstreetidname { get; set; }

		[AntiXss]
		public string address1_telephone1 { get; set; }

		[AntiXss]
		public string address1_telephone2 { get; set; }

		[AntiXss]
		public string address1_telephone3 { get; set; }

		[AntiXss]
		public string paymenttermscodename { get; set; }

		[AntiXss]
		public string address2_upszone { get; set; }

		[AntiXss]
		public Guid xts_sourcecampaignid { get; set; }

		[AntiXss]
		public string fax { get; set; }

		[AntiXss]
		public string preferredappointmentdaycodename { get; set; }

		[AntiXss]
		public string address2_line3 { get; set; }

		[AntiXss]
		public string ktb_pendidikan { get; set; }

		[AntiXss]
		public Guid parentaccountid { get; set; }

		[AntiXss]
		public string accountclassificationcode { get; set; }

		[AntiXss]
		public string slainvokedidname { get; set; }

		[AntiXss]
		public string address2_addresstypecode { get; set; }

		[AntiXss]
		public Guid ktb_ocrid { get; set; }

		[AntiXss]
		public string xts_shortname { get; set; }

		[AntiXss]
		public string defaultpricelevelidname { get; set; }

		[AntiXss]
		public string stockexchange { get; set; }

		[AntiXss]
		public Guid xts_categoryid { get; set; }

		[AntiXss]
		public decimal address2_longitude { get; set; }

		[AntiXss]
		public string originatingleadidname { get; set; }

		[AntiXss]
		public string xts_shipmenttypename { get; set; }

		[AntiXss]
		public bool ktb_interfacetodnet { get; set; }

		[AntiXss]
		public string slaname { get; set; }

		[AntiXss]
		public string xts_customerrankidname { get; set; }

		[AntiXss]
		public string xts_consumptiontax2idname { get; set; }

		[AntiXss]
		public string ktb_groupkategori { get; set; }

		[AntiXss]
		public string ktb_birthdaymonthname { get; set; }

		[AntiXss]
		public string address2_freighttermscodename { get; set; }

		[AntiXss]
		public string ktb_interfacestatus { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid xts_salespersonid { get; set; }

		[AntiXss]
		public decimal address1_latitude { get; set; }

		[AntiXss]
		public string accountcategorycodename { get; set; }

		[AntiXss]
		public Guid entityimageid { get; set; }

		[AntiXss]
		public string xjp_ownershipposition { get; set; }

		[AntiXss]
		public string ktb_interfacestatusname { get; set; }

		[AntiXss]
		public bool xts_saturday { get; set; }

		[AntiXss]
		public string yominame { get; set; }

		[AntiXss]
		public bool xts_tuesday { get; set; }

		[AntiXss]
		public string shippingmethodcode { get; set; }

		[AntiXss]
		public Guid xts_taxzoneid { get; set; }

		[AntiXss]
		public bool xts_wednesday { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string preferredappointmenttimecode { get; set; }

		[AntiXss]
		public decimal creditlimit_base { get; set; }

		[AntiXss]
		public Guid preferredserviceid { get; set; }

		[AntiXss]
		public string ktb_interfaceexceptionmessage { get; set; }

		[AntiXss]
		public string xts_thursdayname { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string preferredserviceidname { get; set; }

		[AntiXss]
		public decimal aging30 { get; set; }

		[AntiXss]
		public string xjp_companycode { get; set; }

		[AntiXss]
		public string xts_identificationtype { get; set; }

		[AntiXss]
		public Guid xts_ownershipid { get; set; }

		[AntiXss]
		public bool xts_checkcreditlimit { get; set; }

		[AntiXss]
		public string address2_line2 { get; set; }

		[AntiXss]
		public decimal xts_creditlimitbalance { get; set; }

		[AntiXss]
		public decimal aging60 { get; set; }

		[AntiXss]
		public string xts_mondayname { get; set; }

		[AntiXss]
		public string xts_sourcecampaignidname { get; set; }

		[AntiXss]
		public string xts_religionidname { get; set; }

		[AntiXss]
		public string xts_gender { get; set; }

		[AntiXss]
		public decimal aging90 { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public string entityimage_url { get; set; }

		[AntiXss]
		public string xts_categoryidname { get; set; }

		[AntiXss]
		public bool creditonhold { get; set; }

		[AntiXss]
		public string mergedname { get; set; }

		[AntiXss]
		public Guid xts_customerclassid { get; set; }

		[AntiXss]
		public string address2_freighttermscode { get; set; }

		[AntiXss]
		public string xts_taxregistrationname { get; set; }

		[AntiXss]
		public string xts_companysizeidname { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string industrycodename { get; set; }

		[AntiXss]
		public string ktb_birthdayyear { get; set; }

		[AntiXss]
		public string ktb_preareaname { get; set; }

		[AntiXss]
		public string preferredsystemuseridname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string address1_name { get; set; }

		[AntiXss]
		public string marketingonlyname { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public string address2_telephone2 { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public decimal marketcap_base { get; set; }

		[AntiXss]
		public string xts_customerclasstypename { get; set; }

		[AntiXss]
		public string description { get; set; }

		[AntiXss]
		public string ktb_interfacehandling { get; set; }

		[AntiXss]
		public string originatingleadidyominame { get; set; }

		[AntiXss]
		public int xts_statementdate { get; set; }

		[AntiXss]
		public int opendeals { get; set; }

		[AntiXss]
		public int xts_numberofvehicle { get; set; }

		[AntiXss]
		public string address2_postalcode { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public string xts_salutation { get; set; }

		[AntiXss]
		public int address1_utcoffset { get; set; }

		[AntiXss]
		public string accountcategorycode { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string xts_maritalstatus { get; set; }

		[AntiXss]
		public string ktb_isinsurancename { get; set; }

		[AntiXss]
		public Guid modifiedbyexternalparty { get; set; }

		[AntiXss]
		public string donotemailname { get; set; }

		[AntiXss]
		public string address1_line3 { get; set; }

		[AntiXss]
		public DateTime xjp_prospectdateforincreasevehicle { get; set; }

		[AntiXss]
		public string territorycodename { get; set; }

		[AntiXss]
		public string paymenttermscode { get; set; }

		[AntiXss]
		public string address2_line1 { get; set; }

		[AntiXss]
		public Guid xts_customerrankid { get; set; }

		[AntiXss]
		public string businesstypecode { get; set; }

		[AntiXss]
		public string customersizecodename { get; set; }

		[AntiXss]
		public string address2_stateorprovince { get; set; }

		[AntiXss]
		public string industrycode { get; set; }

		[AntiXss]
		public string ktb_ocrsimidname { get; set; }

		[AntiXss]
		public string address2_name { get; set; }

		[AntiXss]
		public string entityimage { get; set; }

		[AntiXss]
		public string xts_fridayname { get; set; }

		[AntiXss]
		public string ktb_pobox { get; set; }

		[AntiXss]
		public string primarytwitterid { get; set; }

		[AntiXss]
		public string donotbulkpostalmailname { get; set; }

		[AntiXss]
		public string xts_sundayname { get; set; }

		[AntiXss]
		public string accountclassificationcodename { get; set; }

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
		public string accountratingcodename { get; set; }

		[AntiXss]
		public string donotfaxname { get; set; }

		[AntiXss]
		public decimal xts_creditlimitamount_base { get; set; }

		[AntiXss]
		public string ktb_customerfleetcode { get; set; }

		[AntiXss]
		public string address2_telephone1 { get; set; }

		[AntiXss]
		public string address2_telephone3 { get; set; }

		[AntiXss]
		public string ftpsiteurl { get; set; }

		[AntiXss]
		public string xts_hobbyidname { get; set; }

		[AntiXss]
		public string address1_postalcode { get; set; }

		[AntiXss]
		public bool ktb_isinsurance { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid slaid { get; set; }

		[AntiXss]
		public Guid originatingleadid { get; set; }

		[AntiXss]
		public string ktb_interfacetodnetname { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public string address1_stateorprovince { get; set; }

		[AntiXss]
		public string xts_aliasname { get; set; }

		[AntiXss]
		public string xts_integrationnumber { get; set; }

		[AntiXss]
		public bool followemail { get; set; }

		[AntiXss]
		public Guid xts_jobtitleid { get; set; }

		[AntiXss]
		public decimal aging90_base { get; set; }

		[AntiXss]
		public Guid masterid { get; set; }

		[AntiXss]
		public string primarysatoriid { get; set; }

		[AntiXss]
		public Guid xts_industryid { get; set; }

		[AntiXss]
		public string xts_checkcreditlimitname { get; set; }

		[AntiXss]
		public string xts_originatingcustomerpublic { get; set; }

		[AntiXss]
		public string ktb_pendidikanname { get; set; }

		[AntiXss]
		public Guid defaultpricelevelid { get; set; }

		[AntiXss]
		public int openrevenue_state { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public bool xts_thursday { get; set; }

		[AntiXss]
		public string ownershipcodename { get; set; }

		[AntiXss]
		public DateTime lastusedincampaign { get; set; }

		[AntiXss]
		public string primarycontactidname { get; set; }

		[AntiXss]
		public string xts_identificationtypename { get; set; }

		[AntiXss]
		public string xts_taxcode { get; set; }

		[AntiXss]
		public string xts_ownershipidname { get; set; }

		[AntiXss]
		public string address1_shippingmethodcode { get; set; }

		[AntiXss]
		public string xts_wednesdayname { get; set; }

		[AntiXss]
		public string preferredappointmentdaycode { get; set; }

		[AntiXss]
		public DateTime xts_anniversarydate { get; set; }

		[AntiXss]
		public string address2_addresstypecodename { get; set; }

		[AntiXss]
		public bool ktb_isinterfaced { get; set; }

		[AntiXss]
		public string address2_fax { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public bool xjp_roadservice { get; set; }

		[AntiXss]
		public string ktb_customerrequestid { get; set; }

		[AntiXss]
		public string customersizecode { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string xts_lastname { get; set; }

		[AntiXss]
		public string xts_email4 { get; set; }

		[AntiXss]
		public string xts_taxregistrationnumber { get; set; }

		[AntiXss]
		public bool xjp_driverlicense { get; set; }

		[AntiXss]
		public string address1_postofficebox { get; set; }

		[AntiXss]
		public string address1_city { get; set; }

		[AntiXss]
		public string xts_consumptiontax1idname { get; set; }

		[AntiXss]
		public string ktb_birthdaymonth { get; set; }

		[AntiXss]
		public string participatesinworkflowname { get; set; }

		[AntiXss]
		public decimal xts_creditlimitbalance_base { get; set; }

		[AntiXss]
		public decimal creditlimit { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string ktb_birthdaydate { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public bool xts_ignoredownpayment { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string preferredcontactmethodcode { get; set; }

		[AntiXss]
		public string ktb_dealercode { get; set; }

		[AntiXss]
		public string tickersymbol { get; set; }

		[AntiXss]
		public string ktb_interfacehandlingname { get; set; }

		[AntiXss]
		public string xts_customeridname { get; set; }

		[AntiXss]
		public string accountratingcode { get; set; }

		[AntiXss]
		public bool xts_sunday { get; set; }

		[AntiXss]
		public string creditonholdname { get; set; }

		[AntiXss]
		public decimal revenue { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax1id { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid createdbyexternalparty { get; set; }

		[AntiXss]
		public decimal aging60_base { get; set; }

		[AntiXss]
		public Guid preferredequipmentid { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public int sharesoutstanding { get; set; }

		[AntiXss]
		public Int64 entityimage_timestamp { get; set; }

		[AntiXss]
		public string territoryidname { get; set; }

		[AntiXss]
		public bool participatesinworkflow { get; set; }

		[AntiXss]
		public string address1_freighttermscodename { get; set; }

		[AntiXss]
		public DateTime opendeals_date { get; set; }

		[AntiXss]
		public string address2_postofficebox { get; set; }

		[AntiXss]
		public string ktb_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string emailaddress3 { get; set; }

		[AntiXss]
		public string emailaddress2 { get; set; }

		[AntiXss]
		public string emailaddress1 { get; set; }

		[AntiXss]
		public Guid xts_consumptiontax2id { get; set; }

		[AntiXss]
		public string websiteurl { get; set; }

		[AntiXss]
		public string xjp_ownershippositionname { get; set; }

		[AntiXss]
		public int xts_graceperiod { get; set; }

		[AntiXss]
		public string xts_otherphone { get; set; }

		[AntiXss]
		public Guid xts_cityid { get; set; }

		[AntiXss]
		public string xts_phonepriorityname { get; set; }

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
		public string preferredappointmenttimecodename { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public bool ktb_overdueonhold { get; set; }

		[AntiXss]
		public string ktb_autonumber { get; set; }

		[AntiXss]
		public string xts_provinceidname { get; set; }

		[AntiXss]
		public bool marketingonly { get; set; }

		[AntiXss]
		public string xts_firstname { get; set; }

		[AntiXss]
		public string preferredsystemuseridyominame { get; set; }

		[AntiXss]
		public string createdbyexternalpartyname { get; set; }

		[AntiXss]
		public decimal xts_creditlimitamount { get; set; }

		[AntiXss]
		public string telephone1 { get; set; }

		[AntiXss]
		public string modifiedbyexternalpartyname { get; set; }

		[AntiXss]
		public bool xts_sharepersonalinformation { get; set; }

		[AntiXss]
		public string ktb_tipepelangganname { get; set; }

		[AntiXss]
		public Guid xts_residentialtypeid { get; set; }

		[AntiXss]
		public Guid xts_religionid { get; set; }

		[AntiXss]
		public string xjp_roadservicename { get; set; }

		[AntiXss]
		public Guid address2_addressid { get; set; }

		[AntiXss]
		public string address1_composite { get; set; }

		[AntiXss]
		public string xts_address4 { get; set; }

		[AntiXss]
		public string xts_customertype { get; set; }

		[AntiXss]
		public Guid xts_countryid { get; set; }

		[AntiXss]
		public bool donotfax { get; set; }

		[AntiXss]
		public string xts_customerclassidname { get; set; }

		[AntiXss]
		public bool xts_friday { get; set; }

		[AntiXss]
		public decimal xts_overduebalance { get; set; }

		[AntiXss]
		public string xts_parentcustomernumber { get; set; }

		[AntiXss]
		public bool isprivate { get; set; }

		[AntiXss]
		public string xts_companyname { get; set; }

		[AntiXss]
		public Guid xts_originatingcontactid { get; set; }

		[AntiXss]
		public string address2_shippingmethodcode { get; set; }

		[AntiXss]
		public string ktb_leveldataname { get; set; }

		[AntiXss]
		public string primarycontactidyominame { get; set; }

		[AntiXss]
		public decimal revenue_base { get; set; }

		[AntiXss]
		public string address2_country { get; set; }

		[AntiXss]
		public string xts_gendername { get; set; }

		[AntiXss]
		public Guid xts_methodofpaymentid { get; set; }

		[AntiXss]
		public Guid primarycontactid { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public decimal openrevenue_base { get; set; }

		[AntiXss]
		public bool xts_monday { get; set; }

		[AntiXss]
		public string createdbyexternalpartyyominame { get; set; }

		[AntiXss]
		public string ktb_overdueonholdname { get; set; }

		[AntiXss]
		public string xts_identificationnumber { get; set; }

		[AntiXss]
		public string xts_phonepriority { get; set; }

		[AntiXss]
		public string telephone3 { get; set; }

		[AntiXss]
		public string address1_country { get; set; }

		[AntiXss]
		public string address2_shippingmethodcodename { get; set; }

		[AntiXss]
		public string xts_shipmenttype { get; set; }

		[AntiXss]
		public string xts_sharepersonalinformationname { get; set; }

		[AntiXss]
		public string modifiedbyexternalpartyyominame { get; set; }

		[AntiXss]
		public string ktb_tipeperusahaan { get; set; }

		[AntiXss]
		public string xts_maritalstatusname { get; set; }

		[AntiXss]
		public string sic { get; set; }

		[AntiXss]
		public Guid slainvokedid { get; set; }

		[AntiXss]
		public Guid xts_villageandstreetid { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xts_preferredserviceid { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string xts_preferredserviceidname { get; set; }

		[AntiXss]
		public string xts_otherhobby { get; set; }

		[AntiXss]
		public decimal xts_overduebalance_base { get; set; }

		[AntiXss]
		public string xts_tuesdayname { get; set; }

		[AntiXss]
		public int teamsfollowed { get; set; }

		[AntiXss]
		public bool donotphone { get; set; }

		[AntiXss]
		public string ownershipcode { get; set; }

		[AntiXss]
		public string masteraccountidname { get; set; }

		[AntiXss]
		public string territorycode { get; set; }

		[AntiXss]
		public string businesstypecodename { get; set; }

		[AntiXss]
		public string ktb_ocridname { get; set; }

		[AntiXss]
		public Guid xts_customerid { get; set; }

		[AntiXss]
		public string timespentbymeonemailandmeetings { get; set; }

		[AntiXss]
		public decimal address2_latitude { get; set; }

		[AntiXss]
		public Guid xts_termofpaymentid { get; set; }

		[AntiXss]
		public decimal aging30_base { get; set; }

		[AntiXss]
		public string parentaccountidyominame { get; set; }

		[AntiXss]
		public string xts_taxzoneidname { get; set; }

		[AntiXss]
		public Guid xts_companysizeid { get; set; }

		[AntiXss]
		public string preferredcontactmethodcodename { get; set; }

		[AntiXss]
		public string name { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string address2_county { get; set; }

		[AntiXss]
		public string xts_salespersonidname { get; set; }

		[AntiXss]
		public decimal openrevenue { get; set; }

		[AntiXss]
		public string address1_line1 { get; set; }

		[AntiXss]
		public string address2_primarycontactname { get; set; }

		[AntiXss]
		public string donotpostalmailname { get; set; }

		[AntiXss]
		public string xts_methodofpaymentidname { get; set; }

		[AntiXss]
		public Guid ktb_parentbusinessunitid { get; set; }

		[AntiXss]
		public Guid xts_provinceid { get; set; }

		[AntiXss]
		public string address1_line2 { get; set; }

		[AntiXss]
		public int opendeals_state { get; set; }

		[AntiXss]
		public string isprivatename { get; set; }

		[AntiXss]
		public string donotphonename { get; set; }

		[AntiXss]
		public bool donotemail { get; set; }

		[AntiXss]
		public string xts_internalnumber { get; set; }

		[AntiXss]
		public string followemailname { get; set; }

		[AntiXss]
		public string ktb_customercodesap { get; set; }

		[AntiXss]
		public decimal marketcap { get; set; }

		[AntiXss]
		public string ktb_leveldata { get; set; }

		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		[AntiXss]
		public Guid accountid { get; set; }

		[AntiXss]
		public string xts_cityidname { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string preferredequipmentidname { get; set; }

		[AntiXss]
		public string shippingmethodcodename { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string ktb_ribbonocr { get; set; }

		[AntiXss]
		public Guid ktb_ocrsimid { get; set; }

		[AntiXss]
		public string address2_composite { get; set; }

		[AntiXss]
		public string donotbulkemailname { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public string masteraccountidyominame { get; set; }

		[AntiXss]
		public int numberofemployees { get; set; }

		[AntiXss]
		public string address1_fax { get; set; }

		[AntiXss]
		public string xts_industryidname { get; set; }

		[AntiXss]
		public string address1_addresstypecodename { get; set; }

		[AntiXss]
		public Guid territoryid { get; set; }

		[AntiXss]
		public string xts_middlename { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
