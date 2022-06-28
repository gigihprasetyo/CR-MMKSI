#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehiclebrandParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2020 09:12:00
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
    public class VWI_CRM_xts_vehiclebrandParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public Guid xts_manufacturer { get; set; }
        [AntiXss]
        public string xts_pkcombinationkey { get; set; }
        [AntiXss]
        public string xts_manufacturername { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string xts_vehiclecategory { get; set; }
        [AntiXss]
        public string xts_branddescription { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public Guid xts_vehiclebrandid { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string xts_vehiclebrand { get; set; }
        [AntiXss]
        public string xts_vehiclecategoryname { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string xts_vehiclecategoryidname { get; set; }
        [AntiXss]
        public DateTime xts_releasedate { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public Guid xts_vehiclecategoryid { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string xts_brand { get; set; }
        [AntiXss]
        public DateTime xts_announcementdate { get; set; }
        [AntiXss]
        public string xts_brandgeneration { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string organizationidname { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public Guid organizationid { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public DateTime xts_createddate { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
