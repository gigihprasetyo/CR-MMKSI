#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_subsidyanddiscountParameterDto  class
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
    public class VWI_CRM_xts_subsidyanddiscountParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public string modifiedonbehalfby { get; set; }
        [AntiXss]
        public string owningteam { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string statecode { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string owninguser { get; set; }
        [AntiXss]
        public string createdonbehalfby { get; set; }
        [AntiXss]
        public string xts_startdate { get; set; }
        [AntiXss]
        public string xts_pkcombinationkey { get; set; }
        [AntiXss]
        public string xts_subsidyanddiscount { get; set; }
        [AntiXss]
        public string xts_subsidyanddiscountid { get; set; }
        [AntiXss]
        public string xts_currencyidname { get; set; }
        [AntiXss]
        public string importsequencenumber { get; set; }
        [AntiXss]
        public string xts_enddate { get; set; }
        [AntiXss]
        public string utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public string owningbusinessunit { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string versionnumber { get; set; }
        [AntiXss]
        public string modifiedby { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string createdby { get; set; }
        [AntiXss]
        public string timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string modifiedon { get; set; }
        [AntiXss]
        public string xts_type { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string statuscode { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string createdon { get; set; }
        [AntiXss]
        public string xts_businessunitid { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_currencyid { get; set; }
        [AntiXss]
        public string xts_typename { get; set; }
        [AntiXss]
        public string xts_description { get; set; }
        [AntiXss]
        public string ownerid { get; set; }
        [AntiXss]
        public string overriddencreatedon { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
