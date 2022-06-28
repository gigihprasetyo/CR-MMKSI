﻿#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_serviceappointment  class
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
    public class VWI_CRM_serviceappointment
    {
        public string company { get; set; }
        public string businessunitcode { get; set; }
        public string organizer { get; set; }
        public string prioritycodename { get; set; }
        public Int64 versionnumber { get; set; }
        public DateTime createdon { get; set; }
        public string modifiedonbehalfbyyominame { get; set; }
        public string xts_servicetypename { get; set; }
        public decimal xts_totallaborcost { get; set; }
        public string xts_reasonidname { get; set; }
        public string statuscodename { get; set; }
        public string isregularactivityname { get; set; }
        public string traversedpath { get; set; }
        public Guid activityid { get; set; }
        public string from { get; set; }
        public string owneridtype { get; set; }
        public string xts_billabletype { get; set; }
        public string customers { get; set; }
        public Guid createdonbehalfby { get; set; }
        public DateTime deliverylastattemptedon { get; set; }
        public string siteidname { get; set; }
        public string partners { get; set; }
        public Int32 onholdtime { get; set; }
        public string xts_businessunitidname { get; set; }
        public Guid xts_reasonid { get; set; }
        public string regardingobjectidyominame { get; set; }
        public string owneridname { get; set; }
        public string prioritycode { get; set; }
        public string xts_isfromwoquotename { get; set; }
        public DateTime overriddencreatedon { get; set; }
        public string xts_nonbillabletype { get; set; }
        public DateTime scheduledstart { get; set; }
        public string createdonbehalfbyyominame { get; set; }
        public Guid subscriptionid { get; set; }
        public bool isbilled { get; set; }
        public decimal exchangerate { get; set; }
        public string subcategory { get; set; }
        public Guid owningteam { get; set; }
        public string deliveryprioritycode { get; set; }
        public decimal xts_totallaborcost_base { get; set; }
        public Guid regardingobjectid { get; set; }
        public Int32 statecode { get; set; }
        public Guid seriesid { get; set; }
        public string subject { get; set; }
        public Guid siteid { get; set; }
        public string bcc { get; set; }
        public Guid xts_costcenterid { get; set; }
        public bool xts_isfromwoquote { get; set; }
        public Guid xts_businessunitid { get; set; }
        public string instancetypecode { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        public Guid stageid { get; set; }
        public string community { get; set; }
        public string xts_locking { get; set; }
        public string xts_servicetype { get; set; }
        public Guid sendermailboxid { get; set; }
        public string deliveryprioritycodename { get; set; }
        public string activitytypecodename { get; set; }
        public string regardingobjectidname { get; set; }
        public string activityadditionalparams { get; set; }
        public string createdonbehalfbyname { get; set; }
        public string xts_billablename { get; set; }
        public DateTime actualstart { get; set; }
        public string xts_maindealerinvoicegroupname { get; set; }
        public string resources { get; set; }
        public string activitytypecode { get; set; }
        public Int32 timezoneruleversionnumber { get; set; }
        public string instancetypecodename { get; set; }
        public Int32 utcconversiontimezonecode { get; set; }
        public Guid slaid { get; set; }
        public Guid ownerid { get; set; }
        public bool isworkflowcreated { get; set; }
        public Int32 importsequencenumber { get; set; }
        public string owneridyominame { get; set; }
        public Int32 scheduleddurationminutes { get; set; }
        public string category { get; set; }
        public Guid processid { get; set; }
        public DateTime sortdate { get; set; }
        public string description { get; set; }
        public DateTime modifiedon { get; set; }
        public string xts_billabletypename { get; set; }
        public string location { get; set; }
        public string createdbyname { get; set; }
        public string regardingobjecttypecode { get; set; }
        public string ismapiprivatename { get; set; }
        public string modifiedonbehalfbyname { get; set; }
        public bool ismapiprivate { get; set; }
        public string isworkflowcreatedname { get; set; }
        public DateTime senton { get; set; }
        public DateTime lastonholdtime { get; set; }
        public string xts_freecouponnumber { get; set; }
        public string exchangeweblink { get; set; }
        public string requiredattendees { get; set; }
        public bool leftvoicemail { get; set; }
        public string xts_nonbillabletypename { get; set; }
        public bool xts_billable { get; set; }
        public string sendermailboxidname { get; set; }
        public Guid transactioncurrencyid { get; set; }
        public Guid slainvokedid { get; set; }
        public Guid createdby { get; set; }
        public Guid modifiedby { get; set; }
        public bool isalldayevent { get; set; }
        public string exchangeitemid { get; set; }
        public Guid serviceid { get; set; }
        public string createdbyyominame { get; set; }
        public Guid owninguser { get; set; }
        public string transactioncurrencyidname { get; set; }
        public string xts_locationidname { get; set; }
        public DateTime actualend { get; set; }
        public Guid owningbusinessunit { get; set; }
        public Guid xts_locationid { get; set; }
        public string isalldayeventname { get; set; }
        public string modifiedbyyominame { get; set; }
        public string cc { get; set; }
        public string to { get; set; }
        public bool isregularactivity { get; set; }
        public string serviceidname { get; set; }
        public string isbilledname { get; set; }
        public string slainvokedidname { get; set; }
        public DateTime scheduledend { get; set; }
        public Int32 statuscode { get; set; }
        public string modifiedbyname { get; set; }
        public string xts_maindealerinvoicegroup { get; set; }
        public string xts_costcenteridname { get; set; }
        public string optionalattendees { get; set; }
        public Int32 actualdurationminutes { get; set; }
        public DateTime postponeactivityprocessinguntil { get; set; }
        public string leftvoicemailname { get; set; }
        public string statecodename { get; set; }
        public string slaname { get; set; }
        public string communityname { get; set; }
        public string RowStatus { get; set; }
        public Guid msdyn_organizationalunitid { get; set; }
        public string msdyn_organizationalunitidname { get; set; }
        public string msdyn_companycode { get; set; }
    }
}
