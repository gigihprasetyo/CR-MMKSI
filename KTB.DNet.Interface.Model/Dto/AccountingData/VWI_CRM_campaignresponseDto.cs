#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_campaignresponseDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:45
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_campaignresponseDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string xts_subject { get; set; }

		public DateTime scheduledend { get; set; }

		public Guid slaid { get; set; }

		public DateTime modifiedon { get; set; }

		public DateTime scheduledstart { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public string statuscodename { get; set; }

		public DateTime senton { get; set; }

		public string activityadditionalparams { get; set; }

		public string xts_mobilephone { get; set; }

		public string subcategory { get; set; }

		public string responsecodename { get; set; }

		public Guid seriesid { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string requiredattendees { get; set; }

		public string exchangeweblink { get; set; }

		public string xts_postalcode { get; set; }

		public Guid slainvokedid { get; set; }

		public string customers { get; set; }

		public bool isworkflowcreated { get; set; }

		public DateTime postponeactivityprocessinguntil { get; set; }

		public int scheduleddurationminutes { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string prioritycodename { get; set; }

		public string to { get; set; }

		public string activitytypecodename { get; set; }

		public string community { get; set; }

		public string channeltypecodename { get; set; }

		public string instancetypecode { get; set; }

		public int statuscode { get; set; }

		public int onholdtime { get; set; }

		public int statecode { get; set; }

		public string ismapiprivatename { get; set; }

		public string bcc { get; set; }

		public Int64 versionnumber { get; set; }

		public string statecodename { get; set; }

		public DateTime lastonholdtime { get; set; }

		public decimal exchangerate { get; set; }

		public Guid ownerid { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public Guid activityid { get; set; }

		public string companyname { get; set; }

		public string communityname { get; set; }

		public string category { get; set; }

		public bool isbilled { get; set; }

		public DateTime createdon { get; set; }

		public string isworkflowcreatedname { get; set; }

		public string slaname { get; set; }

		public string isregularactivityname { get; set; }

		public Guid originatingactivityid { get; set; }

		public string leftvoicemailname { get; set; }

		public string firstname { get; set; }

		public string customer { get; set; }

		public string exchangeitemid { get; set; }

		public DateTime actualstart { get; set; }

		public string xts_salesobjective { get; set; }

		public Guid xts_productid { get; set; }

		public string owneridyominame { get; set; }

		public Guid xts_vehiclemodelid { get; set; }

		public Guid xts_countryid { get; set; }

		public string description { get; set; }

		public Guid modifiedby { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public bool isregularactivity { get; set; }

		public string xts_productexteriorcoloridname { get; set; }

		public string xts_address1 { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string promotioncodename { get; set; }

		public string xts_customernumber { get; set; }

		public string yomilastname { get; set; }

		public bool ismapiprivate { get; set; }

		public Guid owningteam { get; set; }

		public string partners { get; set; }

		public string traversedpath { get; set; }

		public string isbilledname { get; set; }

		public string xts_aliasname { get; set; }

		public string createdonbehalfbyname { get; set; }

		public Guid owninguser { get; set; }

		public string optionalattendees { get; set; }

		public DateTime sortdate { get; set; }

		public string owneridtype { get; set; }

		public string instancetypecodename { get; set; }

		public Guid sendermailboxid { get; set; }

		public string xts_website { get; set; }

		public Guid regardingobjectid { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public bool leftvoicemail { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public DateTime actualend { get; set; }

		public string xts_firstname { get; set; }

		public string regardingobjectidname { get; set; }

		public string xts_vehiclemodelidname { get; set; }

		public string deliveryprioritycodename { get; set; }

		public Guid processid { get; set; }

		public Guid createdonbehalfby { get; set; }

		public int actualdurationminutes { get; set; }

		public string emailaddress { get; set; }

		public string deliveryprioritycode { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string activitytypecode { get; set; }

		public Guid xts_vehiclebrandid { get; set; }

		public Guid xts_cityid { get; set; }

		public string resources { get; set; }

		public string responsecode { get; set; }

		public string xts_productidname { get; set; }

		public string slainvokedidname { get; set; }

		public string fax { get; set; }

		public string sendermailboxidname { get; set; }

		public string xts_vehiclebrandidname { get; set; }

		public DateTime deliverylastattemptedon { get; set; }

		public string channeltypecode { get; set; }

		public string xts_provinceidname { get; set; }

		public string subject { get; set; }

		public string xts_manufactureridname { get; set; }

		public string createdbyname { get; set; }

		public string xts_businessunitidname { get; set; }

		public string modifiedbyyominame { get; set; }

		public string from { get; set; }

		public string yomifirstname { get; set; }

		public Guid createdby { get; set; }

		public string xts_salesobjectivename { get; set; }

		public string originatingactivityname { get; set; }

		public string regardingobjectidyominame { get; set; }

		public string regardingobjecttypecode { get; set; }

		public string xts_homephone { get; set; }

		public string prioritycode { get; set; }

		public string xts_countryidname { get; set; }

		public Guid stageid { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string partner { get; set; }

		public int importsequencenumber { get; set; }

		public string xts_locking { get; set; }

		public string originatingactivityidtypecode { get; set; }

		public string xts_address2 { get; set; }

		public Guid xts_businessunitid { get; set; }

		public DateTime receivedon { get; set; }

		public string cc { get; set; }

		public string yomicompanyname { get; set; }

		public Guid xts_provinceid { get; set; }

		public string modifiedbyname { get; set; }

		public string createdbyyominame { get; set; }

		public string owneridname { get; set; }

		public string telephone { get; set; }

		public Guid serviceid { get; set; }

		public string xts_villageandstreetidname { get; set; }

		public string lastname { get; set; }

		public string xts_address4 { get; set; }

		public string xts_cityidname { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string xts_address3 { get; set; }

		public string organizer { get; set; }

		public Guid xts_villageandstreetid { get; set; }

		public string xts_otherphone { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
