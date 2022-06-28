#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicemmsParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 09:51:00
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
    public class VWI_CRM_xts_servicemmsParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public bool xts_rearlefttireflaw { get; set; }
        [AntiXss]
        public Guid xts_parentbusinessunitid { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public Guid xts_mmspersoninchargeid { get; set; }
        [AntiXss]
        public bool xts_frontrightoffcentered { get; set; }
        [AntiXss]
        public DateTime xts_scheduledarrivaldateandtime { get; set; }
        [AntiXss]
        public Guid xts_workorderid { get; set; }
        [AntiXss]
        public string xts_frontrighttireflawname { get; set; }
        [AntiXss]
        public string xts_frontlefttireflawname { get; set; }
        [AntiXss]
        public Guid xts_customerid { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string xts_typeoftirename { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string xts_platenumber { get; set; }
        [AntiXss]
        public decimal xts_frontrightmm { get; set; }
        [AntiXss]
        public string xts_vehiclemodelname { get; set; }
        [AntiXss]
        public Guid xts_vehicleidentificationid { get; set; }
        [AntiXss]
        public decimal xts_frontleftkpa { get; set; }
        [AntiXss]
        public Guid xts_mmstemplateid { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string ktb_wodescription { get; set; }
        [AntiXss]
        public string xts_nextservicenote { get; set; }
        [AntiXss]
        public DateTime xts_nextestimatedservicedate { get; set; }
        [AntiXss]
        public bool xts_rearleftoffcentered { get; set; }
        [AntiXss]
        public bool xts_frontrighttireflaw { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string xts_workorderidname { get; set; }
        [AntiXss]
        public bool xts_frontlefttireflaw { get; set; }
        [AntiXss]
        public string xts_customeridyominame { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string xts_mmstemplateidname { get; set; }
        [AntiXss]
        public bool xts_frontleftoffcentered { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public decimal xts_rearbrakepad { get; set; }
        [AntiXss]
        public string xts_customernumber { get; set; }
        [AntiXss]
        public Guid xts_servicemmsid { get; set; }
        [AntiXss]
        public DateTime xts_createdon { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public decimal xts_rearrightmm { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public decimal xts_frontbrakepad { get; set; }
        [AntiXss]
        public string xts_rearrighttireflawname { get; set; }
        [AntiXss]
        public bool xts_rearrightoffcentered { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public decimal xts_rearrightkpa { get; set; }
        [AntiXss]
        public string xts_rearlefttireflawname { get; set; }
        [AntiXss]
        public decimal xts_rearleftkpa { get; set; }
        [AntiXss]
        public Guid xts_servicecategoryid { get; set; }
        [AntiXss]
        public string ktb_wommsidname { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public decimal xts_rearleftmm { get; set; }
        [AntiXss]
        public string xts_vehicleidentificationidname { get; set; }
        [AntiXss]
        public string xts_rearrightoffcenteredname { get; set; }
        [AntiXss]
        public Guid ktb_wommsid { get; set; }
        [AntiXss]
        public string xts_servicemms { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public decimal xts_frontleftmm { get; set; }
        [AntiXss]
        public string xts_customeridname { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string xts_frontleftoffcenteredname { get; set; }
        [AntiXss]
        public string xts_parentbusinessunitidname { get; set; }
        [AntiXss]
        public string xts_frontrightoffcenteredname { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public bool xts_typeoftire { get; set; }
        [AntiXss]
        public string xts_rearleftoffcenteredname { get; set; }
        [AntiXss]
        public decimal xts_frontrightkpa { get; set; }
        [AntiXss]
        public string xts_mmspersoninchargeidname { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public bool xts_rearrighttireflaw { get; set; }
        [AntiXss]
        public string xts_servicecategoryidname { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
