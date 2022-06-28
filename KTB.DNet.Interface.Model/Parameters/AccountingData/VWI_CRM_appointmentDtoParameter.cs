#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_equipmentParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2020 18:15:44
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
	public class VWI_CRM_appointmentParameterDto : ParameterDtoBase, IValidatableObject
	{
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public string xts_appointmenttypename { get; set; }
        [AntiXss]
        public string prioritycodename { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_regardingpersoninchargeidname { get; set; }
        [AntiXss]
        public DateTime originalstartdate { get; set; }
        [AntiXss]
        public Guid xts_referenceid { get; set; }
        [AntiXss]
        public Int32 xts_referencenumberlookuptype { get; set; }
        [AntiXss]
        public Int32 attachmentcount { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string isregularactivityname { get; set; }
        [AntiXss]
        public Guid activityid { get; set; }
        [AntiXss]
        public string xts_referenceglobalserviceidname { get; set; }
        [AntiXss]
        public string xto_activityrating { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string isdraftname { get; set; }
        [AntiXss]
        public Guid xts_personinchargeid { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string xts_mobilephone { get; set; }
        [AntiXss]
        public Int32 onholdtime { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
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
        public string xts_referenceusedvehicleidname { get; set; }
        [AntiXss]
        public Guid xts_referenceglobalserviceid { get; set; }
        [AntiXss]
        public DateTime scheduledstart { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid subscriptionid { get; set; }
        [AntiXss]
        public bool isbilled { get; set; }
        [AntiXss]
        public decimal exchangerate { get; set; }
        [AntiXss]
        public bool isregularactivity { get; set; }
        [AntiXss]
        public string subcategory { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public string activityadditionalparams { get; set; }
        [AntiXss]
        public Guid xts_vehiclebrandid { get; set; }
        [AntiXss]
        public string xts_productidname { get; set; }
        [AntiXss]
        public Guid regardingobjectid { get; set; }
        [AntiXss]
        public Int32 statecode { get; set; }
        [AntiXss]
        public Guid seriesid { get; set; }
        [AntiXss]
        public Int32 timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string attachmenterrors { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public string xts_productexteriorcoloridname { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string xts_referencenumberlookupname { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_email { get; set; }
        [AntiXss]
        public string attachmenterrorsname { get; set; }
        [AntiXss]
        public string xts_manufactureridname { get; set; }
        [AntiXss]
        public string activitytypecodename { get; set; }
        [AntiXss]
        public string regardingobjectidname { get; set; }
        [AntiXss]
        public Guid xto_salespersonid { get; set; }
        [AntiXss]
        public string traversedpath { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public DateTime actualstart { get; set; }
        [AntiXss]
        public Guid xts_productexteriorcolorid { get; set; }
        [AntiXss]
        public string safedescription { get; set; }
        [AntiXss]
        public string isbilledname { get; set; }
        [AntiXss]
        public string activitytypecode { get; set; }
        [AntiXss]
        public Int32 utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string instancetypecodename { get; set; }
        [AntiXss]
        public string xts_vehiclebrandidname { get; set; }
        [AntiXss]
        public string organizer { get; set; }
        [AntiXss]
        public Guid slainvokedid { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public bool isworkflowcreated { get; set; }
        [AntiXss]
        public Int32 importsequencenumber { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public Int32 scheduleddurationminutes { get; set; }
        [AntiXss]
        public string category { get; set; }
        [AntiXss]
        public Int32 isunsafe { get; set; }
        [AntiXss]
        public string xts_personinchargeidname { get; set; }
        [AntiXss]
        public Guid processid { get; set; }
        [AntiXss]
        public DateTime sortdate { get; set; }
        [AntiXss]
        public string description { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public bool isdraft { get; set; }
        [AntiXss]
        public string location { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string globalobjectid { get; set; }
        [AntiXss]
        public string instancetypecode { get; set; }
        [AntiXss]
        public string ismapiprivatename { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public bool ismapiprivate { get; set; }
        [AntiXss]
        public string isworkflowcreatedname { get; set; }
        [AntiXss]
        public string requiredattendees { get; set; }
        [AntiXss]
        public string xts_vehicleidentificationidname { get; set; }
        [AntiXss]
        public DateTime lastonholdtime { get; set; }
        [AntiXss]
        public Guid stageid { get; set; }
        [AntiXss]
        public string optionalattendees { get; set; }
        [AntiXss]
        public string xts_appointmenttype { get; set; }
        [AntiXss]
        public string xto_activityresult { get; set; }
        [AntiXss]
        public Guid xts_productid { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public Guid slaid { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public bool isalldayevent { get; set; }
        [AntiXss]
        public Guid serviceid { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string xto_activityresultname { get; set; }
        [AntiXss]
        public DateTime actualend { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public Guid xts_vehicleidentificationid { get; set; }
        [AntiXss]
        public string isalldayeventname { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public Guid xts_regardingpersoninchargeid { get; set; }
        [AntiXss]
        public string regardingobjecttypecode { get; set; }
        [AntiXss]
        public string xto_salespersonidname { get; set; }
        [AntiXss]
        public string slainvokedidname { get; set; }
        [AntiXss]
        public DateTime scheduledend { get; set; }
        [AntiXss]
        public Int32 statuscode { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public Guid xts_referenceusedvehicleid { get; set; }
        [AntiXss]
        public string modifiedfieldsmask { get; set; }
        [AntiXss]
        public Int32 outlookownerapptid { get; set; }
        [AntiXss]
        public Int32 actualdurationminutes { get; set; }
        [AntiXss]
        public Guid xts_manufacturerid { get; set; }
        [AntiXss]
        public string xts_referenceidname { get; set; }
        [AntiXss]
        public string subject { get; set; }
        [AntiXss]
        public string xto_activityratingname { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string slaname { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }


        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}
