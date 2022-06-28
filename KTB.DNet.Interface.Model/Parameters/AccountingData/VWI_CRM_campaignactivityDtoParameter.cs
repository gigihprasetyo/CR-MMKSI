#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_campaignactivityParameterDto  class
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
using Embarr.WebAPI.AntiXss;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_CRM_campaignactivityParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string subcategory { get; set; }

		[AntiXss]
		public string prioritycodename { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public string activitytypecodename { get; set; }

		[AntiXss]
		public string from { get; set; }

		[AntiXss]
		public string resources { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string isregularactivityname { get; set; }

		[AntiXss]
		public string traversedpath { get; set; }

		[AntiXss]
		public Guid activityid { get; set; }

		[AntiXss]
		public string xts_salesobjective { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public Guid xts_manufacturerid { get; set; }

		[AntiXss]
		public string ismapiprivatename { get; set; }

		[AntiXss]
		public string customers { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public DateTime deliverylastattemptedon { get; set; }

		[AntiXss]
		public DateTime postponeactivityprocessinguntil { get; set; }

		[AntiXss]
		public int onholdtime { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_vehiclemodelidname { get; set; }

		[AntiXss]
		public string regardingobjectidyominame { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string prioritycode { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_vehiclemodelid { get; set; }

		[AntiXss]
		public string exchangeitemid { get; set; }

		[AntiXss]
		public DateTime scheduledstart { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public bool isbilled { get; set; }

		[AntiXss]
		public decimal exchangerate { get; set; }

		[AntiXss]
		public string deliveryprioritycodename { get; set; }

		[AntiXss]
		public int excludeifcontactedinxdays { get; set; }

		[AntiXss]
		public decimal budgetedcost_base { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public Guid xts_vehiclebrandid { get; set; }

		[AntiXss]
		public string deliveryprioritycode { get; set; }

		[AntiXss]
		public Guid sendermailboxid { get; set; }

		[AntiXss]
		public string xts_productidname { get; set; }

		[AntiXss]
		public Guid regardingobjectid { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string exchangeweblink { get; set; }

		[AntiXss]
		public string activityadditionalparams { get; set; }

		[AntiXss]
		public Guid seriesid { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string bcc { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string xts_productexteriorcoloridname { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_locking { get; set; }

		[AntiXss]
		public string community { get; set; }

		[AntiXss]
		public string xts_manufactureridname { get; set; }

		[AntiXss]
		public decimal actualcost_base { get; set; }

		[AntiXss]
		public string regardingobjectidname { get; set; }

		[AntiXss]
		public string slainvokedidname { get; set; }

		[AntiXss]
		public bool ignoreinactivelistmembers { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string ignoreinactivelistmembersname { get; set; }

		[AntiXss]
		public DateTime actualstart { get; set; }

		[AntiXss]
		public Guid xts_productexteriorcolorid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string activitytypecode { get; set; }

		[AntiXss]
		public string instancetypecodename { get; set; }

		[AntiXss]
		public DateTime lastonholdtime { get; set; }

		[AntiXss]
		public Guid slaid { get; set; }

		[AntiXss]
		public Guid slainvokedid { get; set; }

		[AntiXss]
		public DateTime sortdate { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public int scheduleddurationminutes { get; set; }

		[AntiXss]
		public string category { get; set; }

		[AntiXss]
		public Guid processid { get; set; }

		[AntiXss]
		public decimal actualcost { get; set; }

		[AntiXss]
		public string description { get; set; }

		[AntiXss]
		public string slaname { get; set; }

		[AntiXss]
		public string to { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public string regardingobjecttypecode { get; set; }

		[AntiXss]
		public string xts_salesobjectivename { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public bool ismapiprivate { get; set; }

		[AntiXss]
		public string isworkflowcreatedname { get; set; }

		[AntiXss]
		public string requiredattendees { get; set; }

		[AntiXss]
		public bool donotsendonoptout { get; set; }

		[AntiXss]
		public Guid stageid { get; set; }

		[AntiXss]
		public string partners { get; set; }

		[AntiXss]
		public string checkfordonotsendmmonlistmembersname { get; set; }

		[AntiXss]
		public bool leftvoicemail { get; set; }

		[AntiXss]
		public string optionalattendees { get; set; }

		[AntiXss]
		public Guid xts_productid { get; set; }

		[AntiXss]
		public string channeltypecode { get; set; }

		[AntiXss]
		public string instancetypecode { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public string organizer { get; set; }

		[AntiXss]
		public Guid serviceid { get; set; }

		[AntiXss]
		public string channeltypecodename { get; set; }

		[AntiXss]
		public bool isworkflowcreated { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public string transactioncurrencyidname { get; set; }

		[AntiXss]
		public DateTime actualend { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string xts_vehiclebrandidname { get; set; }

		[AntiXss]
		public Guid transactioncurrencyid { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public string typecodename { get; set; }

		[AntiXss]
		public string sendermailboxidname { get; set; }

		[AntiXss]
		public bool isregularactivity { get; set; }

		[AntiXss]
		public string cc { get; set; }

		[AntiXss]
		public string isbilledname { get; set; }

		[AntiXss]
		public DateTime senton { get; set; }

		[AntiXss]
		public DateTime scheduledend { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public decimal budgetedcost { get; set; }

		[AntiXss]
		public int actualdurationminutes { get; set; }

		[AntiXss]
		public string typecode { get; set; }

		[AntiXss]
		public string subject { get; set; }

		[AntiXss]
		public string leftvoicemailname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string communityname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
