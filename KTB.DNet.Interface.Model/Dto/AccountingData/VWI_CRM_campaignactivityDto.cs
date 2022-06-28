#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_campaignactivityDto  class
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
    public class VWI_CRM_campaignactivityDto : DtoBase
    {
        public string company { get; set; }

		public string businessunitcode { get; set; }

		public string subcategory { get; set; }

		public string prioritycodename { get; set; }

		public Int64 versionnumber { get; set; }

		public DateTime createdon { get; set; }

		public string modifiedonbehalfbyyominame { get; set; }

		public string activitytypecodename { get; set; }

		public string from { get; set; }

		public string resources { get; set; }

		public string statuscodename { get; set; }

		public string isregularactivityname { get; set; }

		public string traversedpath { get; set; }

		public Guid activityid { get; set; }

		public string xts_salesobjective { get; set; }

		public string owneridtype { get; set; }

		public Guid xts_manufacturerid { get; set; }

		public string ismapiprivatename { get; set; }

		public string customers { get; set; }

		public Guid createdonbehalfby { get; set; }

		public DateTime deliverylastattemptedon { get; set; }

		public DateTime postponeactivityprocessinguntil { get; set; }

		public int onholdtime { get; set; }

		public string createdbyyominame { get; set; }

		public string xts_vehiclemodelidname { get; set; }

		public string regardingobjectidyominame { get; set; }

		public string owneridname { get; set; }

		public string prioritycode { get; set; }

		public DateTime overriddencreatedon { get; set; }

		public Guid xts_vehiclemodelid { get; set; }

		public string exchangeitemid { get; set; }

		public DateTime scheduledstart { get; set; }

		public string createdonbehalfbyyominame { get; set; }

		public bool isbilled { get; set; }

		public decimal exchangerate { get; set; }

		public string deliveryprioritycodename { get; set; }

		public int excludeifcontactedinxdays { get; set; }

		public decimal budgetedcost_base { get; set; }

		public Guid owningteam { get; set; }

		public Guid xts_vehiclebrandid { get; set; }

		public string deliveryprioritycode { get; set; }

		public Guid sendermailboxid { get; set; }

		public string xts_productidname { get; set; }

		public Guid regardingobjectid { get; set; }

		public int statecode { get; set; }

		public string exchangeweblink { get; set; }

		public string activityadditionalparams { get; set; }

		public Guid seriesid { get; set; }

		public int timezoneruleversionnumber { get; set; }

		public string bcc { get; set; }

		public Guid xts_businessunitid { get; set; }

		public string xts_productexteriorcoloridname { get; set; }

		public Guid modifiedonbehalfby { get; set; }

		public string xts_locking { get; set; }

		public string community { get; set; }

		public string xts_manufactureridname { get; set; }

		public decimal actualcost_base { get; set; }

		public string regardingobjectidname { get; set; }

		public string slainvokedidname { get; set; }

		public bool ignoreinactivelistmembers { get; set; }

		public string createdonbehalfbyname { get; set; }

		public string ignoreinactivelistmembersname { get; set; }

		public DateTime actualstart { get; set; }

		public Guid xts_productexteriorcolorid { get; set; }

		public int utcconversiontimezonecode { get; set; }

		public string activitytypecode { get; set; }

		public string instancetypecodename { get; set; }

		public DateTime lastonholdtime { get; set; }

		public Guid slaid { get; set; }

		public Guid slainvokedid { get; set; }

		public DateTime sortdate { get; set; }

		public Guid ownerid { get; set; }

		public DateTime modifiedon { get; set; }

		public int importsequencenumber { get; set; }

		public string owneridyominame { get; set; }

		public int scheduleddurationminutes { get; set; }

		public string category { get; set; }

		public Guid processid { get; set; }

		public decimal actualcost { get; set; }

		public string description { get; set; }

		public string slaname { get; set; }

		public string to { get; set; }

		public string createdbyname { get; set; }

		public string regardingobjecttypecode { get; set; }

		public string xts_salesobjectivename { get; set; }

		public string modifiedonbehalfbyname { get; set; }

		public bool ismapiprivate { get; set; }

		public string isworkflowcreatedname { get; set; }

		public string requiredattendees { get; set; }

		public bool donotsendonoptout { get; set; }

		public Guid stageid { get; set; }

		public string partners { get; set; }

		public string checkfordonotsendmmonlistmembersname { get; set; }

		public bool leftvoicemail { get; set; }

		public string optionalattendees { get; set; }

		public Guid xts_productid { get; set; }

		public string channeltypecode { get; set; }

		public string instancetypecode { get; set; }

		public Guid createdby { get; set; }

		public Guid modifiedby { get; set; }

		public string organizer { get; set; }

		public Guid serviceid { get; set; }

		public string channeltypecodename { get; set; }

		public bool isworkflowcreated { get; set; }

		public Guid owninguser { get; set; }

		public string transactioncurrencyidname { get; set; }

		public DateTime actualend { get; set; }

		public Guid owningbusinessunit { get; set; }

		public string xts_vehiclebrandidname { get; set; }

		public Guid transactioncurrencyid { get; set; }

		public string modifiedbyyominame { get; set; }

		public string typecodename { get; set; }

		public string sendermailboxidname { get; set; }

		public bool isregularactivity { get; set; }

		public string cc { get; set; }

		public string isbilledname { get; set; }

		public DateTime senton { get; set; }

		public DateTime scheduledend { get; set; }

		public int statuscode { get; set; }

		public string modifiedbyname { get; set; }

		public decimal budgetedcost { get; set; }

		public int actualdurationminutes { get; set; }

		public string typecode { get; set; }

		public string subject { get; set; }

		public string leftvoicemailname { get; set; }

		public string statecodename { get; set; }

		public string xts_businessunitidname { get; set; }

		public string communityname { get; set; }

		public string msdyn_companycode { get; set; }
    }
}
