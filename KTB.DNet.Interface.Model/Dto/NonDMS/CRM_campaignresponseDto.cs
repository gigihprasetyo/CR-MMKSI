#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_campaignresponse class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 08:58:34
 ===========================================================================
*/
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class CRM_campaignresponseDto : DtoBase
    {
        
		public string activityadditionalparams { get; set; }

		public Guid activityid { get; set; }

		public string activitytypecode { get; set; }

		public string activitytypecodename { get; set; }

		public int? actualdurationminutes { get; set; }

		public DateTime? actualend { get; set; }

		public DateTime? actualstart { get; set; }

		public string bcc { get; set; }

		public string category { get; set; }

		public string cc { get; set; }

		public string channeltypecode { get; set; }

		public string channeltypecodename { get; set; }

		public string community { get; set; }

		public string communityname { get; set; }

		public string companyname { get; set; }

		public Guid createdby { get; set; }

		public string createdbyname { get; set; }

		public string createdbyyominame { get; set; }

		public DateTime? createdon { get; set; }

		public Guid createdonbehalfby { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public string customer { get; set; }

		public string customers { get; set; }

		public string DealerCode { get; set; }

		public DateTime? deliverylastattemptedon { get; set; }

		public string deliveryprioritycode { get; set; }

		public string deliveryprioritycodename { get; set; }

		public string description { get; set; }

		public string emailaddress { get; set; }

		public string exchangeitemid { get; set; }

		public decimal? exchangerate { get; set; }

		public string exchangeweblink { get; set; }

		public string fax { get; set; }

		public string firstname { get; set; }

		public string from { get; set; }

		public int? importsequencenumber { get; set; }

		public string instancetypecode { get; set; }

		public string instancetypecodename { get; set; }

		public bool? isbilled { get; set; }

		public string isbilledname { get; set; }

		public bool? ismapiprivate { get; set; }

		public string ismapiprivatename { get; set; }

		public bool? isregularactivity { get; set; }

		public string isregularactivityname { get; set; }

		public bool? isworkflowcreated { get; set; }

		public string isworkflowcreatedname { get; set; }

		public string lastname { get; set; }

		public DateTime? lastonholdtime { get; set; }

		public bool? leftvoicemail { get; set; }

		public string leftvoicemailname { get; set; }

		public Guid modifiedby { get; set; }

		public string modifiedbyname { get; set; }

		public string modifiedbyyominame { get; set; }

		public DateTime? modifiedon { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public int? onholdtime { get; set; }

		public string optionalattendees { get; set; }

		public string organizer { get; set; }

		public Guid originatingactivityid { get; set; }

		public string originatingactivityidtypecode { get; set; }

		public string originatingactivityname { get; set; }

		public DateTime? overriddencreatedon { get; set; }

		public Guid ownerid { get; set; }

		public string owneridname { get; set; }

		public string owneridtype { get; set; }

		public string owneridyominame { get; set; }

		public Guid owningbusinessunit { get; set; }

		public Guid owningteam { get; set; }

		public Guid owninguser { get; set; }

		public string partner { get; set; }

		public string partners { get; set; }

		public DateTime? postponeactivityprocessinguntil { get; set; }

		public string prioritycode { get; set; }

		public string prioritycodename { get; set; }

		public Guid processid { get; set; }

		public string promotioncodename { get; set; }

		public DateTime? receivedon { get; set; }

		public Guid regardingobjectid { get; set; }

		public string regardingobjectidname { get; set; }

		public string regardingobjectidyominame { get; set; }

		public string regardingobjecttypecode { get; set; }

		public string requiredattendees { get; set; }

		public string resources { get; set; }

		public string responsecode { get; set; }

		public string responsecodename { get; set; }

		//public string RowStatus { get; set; }

		public int? scheduleddurationminutes { get; set; }

		public DateTime? scheduledend { get; set; }

		public DateTime? scheduledstart { get; set; }

		public Guid sendermailboxid { get; set; }

		public string sendermailboxidname { get; set; }

		public DateTime? senton { get; set; }

		public Guid seriesid { get; set; }

		public Guid serviceid { get; set; }

		public Guid slaid { get; set; }

		public Guid slainvokedid { get; set; }

		public string slainvokedidname { get; set; }

		public string slaname { get; set; }

		public DateTime? sortdate { get; set; }

		public string SourceType { get; set; }

		public Guid stageid { get; set; }

		public int? statecode { get; set; }

		public string statecodename { get; set; }

		public int? statuscode { get; set; }

		public string statuscodename { get; set; }

		public string subcategory { get; set; }

		public string subject { get; set; }

		public string telephone { get; set; }

		public int? timezoneruleversionnumber { get; set; }

		public string to { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string transactioncurrencyidname { get; set; }

		public string traversedpath { get; set; }

		public int? utcconversiontimezonecode { get; set; }

		public long? versionnumber { get; set; }

		public string xts_address1 { get; set; }

		public string xts_address2 { get; set; }

		public string xts_address3 { get; set; }

		public string xts_address4 { get; set; }

		public string xts_aliasname { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_businessunitidname { get; set; }

		public Guid xts_cityid { get; set; }

		public string xts_cityidname { get; set; }

		public Guid xts_countryid { get; set; }

		public string xts_countryidname { get; set; }

		public string xts_customernumber { get; set; }

		public string xts_firstname { get; set; }

		public string xts_homephone { get; set; }

		public string xts_locking { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public string xts_manufactureridname { get; set; }

		public string xts_mobilephone { get; set; }

		public string xts_otherphone { get; set; }

		public string xts_postalcode { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public string xts_productexteriorcoloridname { get; set; }

		public Guid xts_productid { get; set; }

		public string xts_productidname { get; set; }

		public Guid xts_provinceid { get; set; }

		public string xts_provinceidname { get; set; }

		public string xts_salesobjective { get; set; }

		public string xts_salesobjectivename { get; set; }

		public string xts_subject { get; set; }

		public Guid xts_vehiclebrandid { get; set; }

		public string xts_vehiclebrandidname { get; set; }

		public Guid xts_vehiclemodelid { get; set; }

		public string xts_vehiclemodelidname { get; set; }

		public Guid xts_villageandstreetid { get; set; }

		public string xts_villageandstreetidname { get; set; }

		public string xts_website { get; set; }

		public string yomicompanyname { get; set; }

		public string yomifirstname { get; set; }

		public string yomilastname { get; set; }

    }
}
