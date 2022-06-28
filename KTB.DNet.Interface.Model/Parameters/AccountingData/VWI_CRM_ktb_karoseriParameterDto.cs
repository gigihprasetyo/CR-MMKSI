#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_karoseriParameterDto  class
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
    public class VWI_CRM_ktb_karoseriParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public string owninguser { get; set; }
        [AntiXss]
        public string ktb_karoseriid { get; set; }
        [AntiXss]
        public string statecode { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string ktb_provinceidname { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string createdonbehalfby { get; set; }
        [AntiXss]
        public string ktb_karoseri { get; set; }
        [AntiXss]
        public string ktb_cityid { get; set; }
        [AntiXss]
        public string importsequencenumber { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string ktb_code { get; set; }
        [AntiXss]
        public string ktb_provinceid { get; set; }
        [AntiXss]
        public string utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public string owningbusinessunit { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public string owningteam { get; set; }
        [AntiXss]
        public string modifiedby { get; set; }
        [AntiXss]
        public string createdby { get; set; }
        [AntiXss]
        public string timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string ktb_cityidname { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string ktb_isinterfaced { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string ktb_isinterfacedname { get; set; }
        [AntiXss]
        public string modifiedon { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string statuscode { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string createdon { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string versionnumber { get; set; }
        [AntiXss]
        public string modifiedonbehalfby { get; set; }
        [AntiXss]
        public string ownerid { get; set; }
        [AntiXss]
        public string overriddencreatedon { get; set; }
        [AntiXss]
        public string RowStatus { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }


        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
