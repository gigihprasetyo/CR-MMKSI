#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_bookableresourcebookingParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-08
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
    public class VWI_CRM_bookableresourcebookingParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string resourcename { get; set; }
        public DateTime endtime { get; set; }
        [AntiXss]
        public string msdyn_allowoverlappingname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        public bool msdyn_cascadecrewchanges { get; set; }
        [AntiXss]
        public string msdyn_acceptcascadecrewchangesname { get; set; }
        public int statuscode { get; set; }
        [AntiXss]
        public string msdyn_ursinternalflags { get; set; }
        public Guid ownerid { get; set; }
        [AntiXss]
        public string bookingstatusname { get; set; }
        [AntiXss]
        public string msdyn_worklocation { get; set; }
        public decimal msdyn_milestraveled { get; set; }
        public Guid transactioncurrencyid { get; set; }
        public decimal msdyn_longitude { get; set; }
        public Guid owninguser { get; set; }
        [AntiXss]
        public string name { get; set; }
        public Guid processid { get; set; }
        [AntiXss]
        public string headername { get; set; }
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string msdyn_worklocationname { get; set; }
        public DateTime modifiedon { get; set; }
        public Guid bookableresourcebookingid { get; set; }
        public bool msdyn_acceptcascadecrewchanges { get; set; }
        public Guid msdyn_timegroupdetailselected { get; set; }
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string msdyn_bookingsetupmetadataidname { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string bookingtype { get; set; }
        [AntiXss]
        public string msdyn_resourcegroupname { get; set; }
        [AntiXss]
        public string ktb_urswohandlername { get; set; }
        [AntiXss]
        public string msdyn_serviceappointmentname { get; set; }
        public Guid bookingstatus { get; set; }
        public int statecode { get; set; }
        public Guid resource { get; set; }
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        public Guid owningbusinessunit { get; set; }
        public DateTime createdon { get; set; }
        public Guid header { get; set; }
        public DateTime starttime { get; set; }
        [AntiXss]
        public string msdyn_cascadecrewchangesname { get; set; }
        public int msdyn_actualtravelduration { get; set; }
        public int utcconversiontimezonecode { get; set; }
        public int duration { get; set; }
        public DateTime msdyn_estimatedarrivaltime { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string owningbusinessunitname { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        public DateTime msdyn_actualarrivaltime { get; set; }
        [AntiXss]
        public string msdyn_resourcerequirementname { get; set; }
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        public decimal msdyn_latitude { get; set; }
        public Guid createdonbehalfby { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        public Guid stageid { get; set; }
        [AntiXss]
        public string msdyn_requirementgroupidname { get; set; }
        public Guid msdyn_resourcegroup { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        public decimal exchangerate { get; set; }
        public Guid msdyn_requirementgroupid { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public string msdyn_appointmentbookingidname { get; set; }
        public bool msdyn_allowoverlapping { get; set; }
        public Guid createdby { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string msdyn_bookingmethod { get; set; }
        public Guid ktb_urswohandler { get; set; }
        public Guid msdyn_bookingsetupmetadataid { get; set; }
        public decimal msdyn_effort { get; set; }
        [AntiXss]
        public string msdyn_bookingmethodname { get; set; }
        public Guid msdyn_appointmentbookingid { get; set; }
        [AntiXss]
        public string msdyn_timegroupdetailselectedname { get; set; }
        [AntiXss]
        public string traversedpath { get; set; }
        [AntiXss]
        public string bookingtypename { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        public int msdyn_estimatedtravelduration { get; set; }
        public Guid owningteam { get; set; }
        public Guid msdyn_serviceappointment { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        public Guid msdyn_resourcerequirement { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        public DateTime LastSyncDate { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
