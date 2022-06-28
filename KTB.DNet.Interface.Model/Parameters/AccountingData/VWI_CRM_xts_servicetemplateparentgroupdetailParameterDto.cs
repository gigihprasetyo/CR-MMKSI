﻿#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicetemplateparentgroupdetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 16:23:00
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
    public class VWI_CRM_xts_servicetemplateparentgroupdetailParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string company { get; set; }
        [AntiXss]
        public string businessunitcode { get; set; }
        [AntiXss]
        public Guid xts_productsegment2id { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public Guid xts_servicetemplatesubgroupid { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string xts_productsegment1idname { get; set; }
        [AntiXss]
        public string xts_servicetemplatesubgroupidname { get; set; }
        [AntiXss]
        public Guid xts_businessunitid { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public Guid xts_productsegment3id { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public string xts_productsegment3idname { get; set; }
        [AntiXss]
        public string xts_businessunitidname { get; set; }
        [AntiXss]
        public Guid xts_servicetemplateparentgroupdetailid { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public string xts_servicetemplategroupidname { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string xts_locking { get; set; }
        [AntiXss]
        public string xts_servicetemplateparentgroupidname { get; set; }
        [AntiXss]
        public string xts_pkcombinationkey { get; set; }
        [AntiXss]
        public Guid xts_productsegment1id { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public string xts_productsegment4idname { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public Guid xts_servicetemplategroupid { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public Guid xts_servicetemplateparentgroupid { get; set; }
        [AntiXss]
        public Guid xts_productsegment4id { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public string xts_productsegment2idname { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string xts_servicetemplategroupdetail { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
