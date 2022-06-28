#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_bookingstatusParameterDto  class
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
    public class VWI_CRM_bookingstatusParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string msdyn_imageurl { get; set; }
        [AntiXss]
        public string msdyn_serviceappointmentstatusname { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        public Guid transactioncurrencyid { get; set; }
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string msdyn_serviceappointmentstatus { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string owningbusinessunitname { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string name { get; set; }
        public int importsequencenumber { get; set; }
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string msdyn_statuscolor { get; set; }
        public Guid bookingstatusid { get; set; }
        public Guid owninguser { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        public int statecode { get; set; }
        public Int64 versionnumber { get; set; }
        public DateTime createdon { get; set; }
        public Guid createdby { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        public Guid modifiedby { get; set; }
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        public decimal exchangerate { get; set; }
        [AntiXss]
        public string description { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        public Guid owningbusinessunit { get; set; }
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        public Guid owningteam { get; set; }
        [AntiXss]
        public string transactioncurrencyidname { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        public int statuscode { get; set; }
        [AntiXss]
        public string status { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string statusname { get; set; }
        public Guid ownerid { get; set; }
        public int utcconversiontimezonecode { get; set; }
        public DateTime LastSyncDate { get; set; }
        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
