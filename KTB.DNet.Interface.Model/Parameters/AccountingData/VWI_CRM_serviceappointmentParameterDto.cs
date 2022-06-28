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
    public class VWI_CRM_serviceappointmentParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public string organizer { get; set; }
        [AntiXss]
        public string prioritycodename { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_servicetypename { get; set; }
        [AntiXss]
        public decimal xts_totallaborcost { get; set; }
        [AntiXss]
        public string xts_reasonidname { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string isregularactivityname { get; set; }
        [AntiXss]
        public string traversedpath { get; set; }
        [AntiXss]
        public Guid activityid { get; set; }
        [AntiXss]
        public string from { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string xts_billabletype { get; set; }
        [AntiXss]
        public string customers { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public DateTime deliverylastattemptedon { get; set; }
        [AntiXss]
        public string siteidname { get; set; }
        [AntiXss]
        public string partners { get; set; }
        [AntiXss]
        public Int32 onholdtime { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public Guid xts_reasonid { get; set; }
        [AntiXss]
        public string regardingobjectidyominame { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string prioritycode { get; set; }
        [AntiXss]
        public string xts_isfromwoquotename { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string xts_nonbillabletype { get; set; }
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
        public string subcategory { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public string deliveryprioritycode { get; set; }
        [AntiXss]
        public decimal xts_totallaborcost_base { get; set; }
        [AntiXss]
        public Guid regardingobjectid { get; set; }
        [AntiXss]
        public Int32 statecode { get; set; }
        [AntiXss]
        public Guid seriesid { get; set; }
        [AntiXss]
        public string subject { get; set; }
        [AntiXss]
        public Guid siteid { get; set; }
        [AntiXss]
        public string bcc { get; set; }
        [AntiXss]
        public Guid xts_costcenterid { get; set; }
        [AntiXss]
        public bool xts_isfromwoquote { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public string instancetypecode { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public Guid stageid { get; set; }
        [AntiXss]
        public string community { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_servicetype { get; set; }
        [AntiXss]
        public Guid sendermailboxid { get; set; }
        [AntiXss]
        public string deliveryprioritycodename { get; set; }
        [AntiXss]
        public string activitytypecodename { get; set; }
        [AntiXss]
        public string regardingobjectidname { get; set; }
        [AntiXss]
        public string activityadditionalparams { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_billablename { get; set; }
        [AntiXss]
        public DateTime actualstart { get; set; }
        [AntiXss]
        public string xts_maindealerinvoicegroupname { get; set; }
        [AntiXss]
        public string resources { get; set; }
        [AntiXss]
        public string activitytypecode { get; set; }
        [AntiXss]
        public Int32 timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string instancetypecodename { get; set; }
        [AntiXss]
        public Int32 utcconversiontimezonecode { get; set; }
        [AntiXss]
        public Guid slaid { get; set; }
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
        public Guid processid { get; set; }
        [AntiXss]
        public DateTime sortdate { get; set; }
        [AntiXss]
        public string description { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string xts_billabletypename { get; set; }
        [AntiXss]
        public string location { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string regardingobjecttypecode { get; set; }
        [AntiXss]
        public string ismapiprivatename { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public bool ismapiprivate { get; set; }
        [AntiXss]
        public string isworkflowcreatedname { get; set; }
        [AntiXss]
        public DateTime senton { get; set; }
        [AntiXss]
        public DateTime lastonholdtime { get; set; }
        [AntiXss]
        public string xts_freecouponnumber { get; set; }
        [AntiXss]
        public string exchangeweblink { get; set; }
        [AntiXss]
        public string requiredattendees { get; set; }
        [AntiXss]
        public bool leftvoicemail { get; set; }
        [AntiXss]
        public string xts_nonbillabletypename { get; set; }
        [AntiXss]
        public bool xts_billable { get; set; }
        [AntiXss]
        public string sendermailboxidname { get; set; }
        [AntiXss]
        public Guid transactioncurrencyid { get; set; }
        [AntiXss]
        public Guid slainvokedid { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public bool isalldayevent { get; set; }
        [AntiXss]
        public string exchangeitemid { get; set; }
        [AntiXss]
        public Guid serviceid { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string xts_locationidname { get; set; }
        [AntiXss]
        public DateTime actualend { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public Guid xts_locationid { get; set; }
        [AntiXss]
        public string isalldayeventname { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string cc { get; set; }
        [AntiXss]
        public string to { get; set; }
        [AntiXss]
        public bool isregularactivity { get; set; }
        [AntiXss]
        public string serviceidname { get; set; }
        [AntiXss]
        public string isbilledname { get; set; }
        [AntiXss]
        public string slainvokedidname { get; set; }
        [AntiXss]
        public DateTime scheduledend { get; set; }
        [AntiXss]
        public Int32 statuscode { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string xts_maindealerinvoicegroup { get; set; }
        [AntiXss]
        public string xts_costcenteridname { get; set; }
        [AntiXss]
        public string optionalattendees { get; set; }
        [AntiXss]
        public Int32 actualdurationminutes { get; set; }
        [AntiXss]
        public DateTime postponeactivityprocessinguntil { get; set; }
        [AntiXss]
        public string leftvoicemailname { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string slaname { get; set; }
        [AntiXss]
        public string communityname { get; set; }
        [AntiXss]
        public Guid msdyn_organizationalunitid { get; set; }
        [AntiXss]
        public string msdyn_organizationalunitidname { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }


        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
