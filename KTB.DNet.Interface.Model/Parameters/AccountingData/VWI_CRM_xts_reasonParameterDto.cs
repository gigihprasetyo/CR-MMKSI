#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_reasonParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 09:58:00
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
    public class VWI_CRM_xts_reasonParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public Guid xts_dimension1id { get; set; }
        [AntiXss]
        public string xts_reasoncodedim1fromname { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string xts_dimension5idname { get; set; }
        [AntiXss]
        public string xts_reasoncodedim6from { get; set; }
        [AntiXss]
        public string xts_dimension3idname { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public Guid xts_dimension6id { get; set; }
        [AntiXss]
        public string xts_reasoncodedim3from { get; set; }
        [AntiXss]
        public Guid xts_dimension7id { get; set; }
        [AntiXss]
        public string xts_dimension2idname { get; set; }
        [AntiXss]
        public string xts_reasoncodedim5fromname { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_categoryname { get; set; }
        [AntiXss]
        public string xts_dimension8idname { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public Guid xts_dimension10id { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public string xts_dimension4idname { get; set; }
        [AntiXss]
        public Guid xts_dimension9id { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string xts_reason { get; set; }
        [AntiXss]
        public string xts_reasoncodedim6fromname { get; set; }
        [AntiXss]
        public string xts_dimension1idname { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid xts_accountid { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_reasoncodedim4fromname { get; set; }
        [AntiXss]
        public string xts_dimension10idname { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string xts_company { get; set; }
        [AntiXss]
        public string xts_dimension9idname { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public Guid xts_dimension4id { get; set; }
        [AntiXss]
        public string xts_category { get; set; }
        [AntiXss]
        public string xts_reasoncodedim5from { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_dimension6idname { get; set; }
        [AntiXss]
        public string xts_reasoncodedim4from { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string xts_dimension7idname { get; set; }
        [AntiXss]
        public Guid xts_dimension2id { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string xts_reasoncodedim2from { get; set; }
        [AntiXss]
        public Guid xts_dimension5id { get; set; }
        [AntiXss]
        public string xts_reasoncodedim3fromname { get; set; }
        [AntiXss]
        public string xts_description { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string xts_reasoncodedim1from { get; set; }
        [AntiXss]
        public Guid xts_dimension8id { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public Guid xts_reasonid { get; set; }
        [AntiXss]
        public string xts_accountidname { get; set; }
        [AntiXss]
        public string xts_reasoncodedim2fromname { get; set; }
        [AntiXss]
        public Guid organizationid { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string organizationidname { get; set; }
        [AntiXss]
        public Guid xts_dimension3id { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
