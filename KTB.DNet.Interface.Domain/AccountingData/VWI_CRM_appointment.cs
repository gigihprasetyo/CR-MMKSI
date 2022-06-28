#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_equipment  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
	public class VWI_CRM_appointment
	{
        public string company { get; set; }
        public string businessunitcode { get; set; }
        public string xts_appointmenttypename { get; set; }
        public string prioritycodename { get; set; }
        public Int64 versionnumber { get; set; }
        public DateTime createdon { get; set; }
        public string modifiedonbehalfbyyominame { get; set; }
        public string xts_regardingpersoninchargeidname { get; set; }
        public DateTime originalstartdate { get; set; }
        public Guid xts_referenceid { get; set; }
        public Int32 xts_referencenumberlookuptype { get; set; }
        public Int32 attachmentcount { get; set; }
        public string statuscodename { get; set; }
        public string isregularactivityname { get; set; }
        public Guid activityid { get; set; }
        public string xts_referenceglobalserviceidname { get; set; }
        public string xto_activityrating { get; set; }
        public string owneridtype { get; set; }
        public string isdraftname { get; set; }
        public Guid xts_personinchargeid { get; set; }
        public Guid createdonbehalfby { get; set; }
        public string xts_mobilephone { get; set; }
        public Int32 onholdtime { get; set; }
        public string xts_businessunitidname { get; set; }
        public string xts_vehiclemodelidname { get; set; }
        public string regardingobjectidyominame { get; set; }
        public string owneridname { get; set; }
        public string prioritycode { get; set; }
        public DateTime overriddencreatedon { get; set; }
        public Guid xts_vehiclemodelid { get; set; }
        public string xts_referenceusedvehicleidname { get; set; }
        public Guid xts_referenceglobalserviceid { get; set; }
        public DateTime scheduledstart { get; set; }
        public string createdonbehalfbyyominame { get; set; }
        public Guid subscriptionid { get; set; }
        public bool isbilled { get; set; }
        public decimal exchangerate { get; set; }
        public bool isregularactivity { get; set; }
        public string subcategory { get; set; }
        public Guid owningteam { get; set; }
        public string activityadditionalparams { get; set; }
        public Guid xts_vehiclebrandid { get; set; }
        public string xts_productidname { get; set; }
        public Guid regardingobjectid { get; set; }
        public Int32 statecode { get; set; }
        public Guid seriesid { get; set; }
        public Int32 timezoneruleversionnumber { get; set; }
        public string attachmenterrors { get; set; }
        public Guid xts_businessunitid { get; set; }
        public string xts_productexteriorcoloridname { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        public string xts_referencenumberlookupname { get; set; }
        public string xts_locking { get; set; }
        public string xts_email { get; set; }
        public string attachmenterrorsname { get; set; }
        public string xts_manufactureridname { get; set; }
        public string activitytypecodename { get; set; }
        public string regardingobjectidname { get; set; }
        public Guid xto_salespersonid { get; set; }
        public string traversedpath { get; set; }
        public string createdonbehalfbyname { get; set; }
        public DateTime actualstart { get; set; }
        public Guid xts_productexteriorcolorid { get; set; }
        public string safedescription { get; set; }
        public string isbilledname { get; set; }
        public string activitytypecode { get; set; }
        public Int32 utcconversiontimezonecode { get; set; }
        public string instancetypecodename { get; set; }
        public string xts_vehiclebrandidname { get; set; }
        public string organizer { get; set; }
        public Guid slainvokedid { get; set; }
        public Guid ownerid { get; set; }
        public bool isworkflowcreated { get; set; }
        public Int32 importsequencenumber { get; set; }
        public string owneridyominame { get; set; }
        public Int32 scheduleddurationminutes { get; set; }
        public string category { get; set; }
        public Int32 isunsafe { get; set; }
        public string xts_personinchargeidname { get; set; }
        public Guid processid { get; set; }
        public DateTime sortdate { get; set; }
        public string description { get; set; }
        public DateTime modifiedon { get; set; }
        public bool isdraft { get; set; }
        public string location { get; set; }
        public string createdbyname { get; set; }
        public string globalobjectid { get; set; }
        public string instancetypecode { get; set; }
        public string ismapiprivatename { get; set; }
        public string modifiedonbehalfbyname { get; set; }
        public bool ismapiprivate { get; set; }
        public string isworkflowcreatedname { get; set; }
        public string requiredattendees { get; set; }
        public string xts_vehicleidentificationidname { get; set; }
        public DateTime lastonholdtime { get; set; }
        public Guid stageid { get; set; }
        public string optionalattendees { get; set; }
        public string xts_appointmenttype { get; set; }
        public string xto_activityresult { get; set; }
        public Guid xts_productid { get; set; }
        public Guid transactioncurrencyid { get; set; }
        public Guid slaid { get; set; }
        public Guid createdby { get; set; }
        public Guid modifiedby { get; set; }
        public bool isalldayevent { get; set; }
        public Guid serviceid { get; set; }
        public string createdbyyominame { get; set; }
        public Guid owninguser { get; set; }
        public string transactioncurrencyidname { get; set; }
        public string xto_activityresultname { get; set; }
        public DateTime actualend { get; set; }
        public Guid owningbusinessunit { get; set; }
        public Guid xts_vehicleidentificationid { get; set; }
        public string isalldayeventname { get; set; }
        public string modifiedbyyominame { get; set; }
        public Guid xts_regardingpersoninchargeid { get; set; }
        public string regardingobjecttypecode { get; set; }
        public string xto_salespersonidname { get; set; }
        public string slainvokedidname { get; set; }
        public DateTime scheduledend { get; set; }
        public Int32 statuscode { get; set; }
        public string modifiedbyname { get; set; }
        public Guid xts_referenceusedvehicleid { get; set; }
        public string modifiedfieldsmask { get; set; }
        public Int32 outlookownerapptid { get; set; }
        public Int32 actualdurationminutes { get; set; }
        public Guid xts_manufacturerid { get; set; }
        public string xts_referenceidname { get; set; }
        public string subject { get; set; }
        public string xto_activityratingname { get; set; }
        public string statecodename { get; set; }
        public string slaname { get; set; }
        public string msdyn_companycode { get; set; }
    }
}
