#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_saleschannelParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 08:15:00
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
    public class VWI_CRM_ktb_saleschannelParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public int ktb_dnetid { get; set; }
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        [AntiXss]
        public Guid owninguser { get; set; }
        [AntiXss]
        public int statecode { get; set; }
        [AntiXss]
        public string owneridname { get; set; }
        [AntiXss]
        public string statecodename { get; set; }
        [AntiXss]
        public Guid createdonbehalfby { get; set; }
        [AntiXss]
        public string ktb_saleschannelcode { get; set; }
        [AntiXss]
        public int importsequencenumber { get; set; }
        [AntiXss]
        public Guid ktb_saleschannelid { get; set; }
        [AntiXss]
        public string modifiedbyyominame { get; set; }
        [AntiXss]
        public string ktb_name { get; set; }
        [AntiXss]
        public int utcconversiontimezonecode { get; set; }
        [AntiXss]
        public string createdbyyominame { get; set; }
        [AntiXss]
        public bool ktb_isdefaultoncreatewo { get; set; }
        [AntiXss]
        public Guid owningbusinessunit { get; set; }
        [AntiXss]
        public string modifiedbyname { get; set; }
        [AntiXss]
        public Guid owningteam { get; set; }
        [AntiXss]
        public Guid modifiedby { get; set; }
        [AntiXss]
        public Guid createdby { get; set; }
        [AntiXss]
        public int timezoneruleversionnumber { get; set; }
        [AntiXss]
        public string ktb_description { get; set; }
        [AntiXss]
        public string owneridtype { get; set; }
        [AntiXss]
        public string statuscodename { get; set; }
        [AntiXss]
        public bool ktb_isinterfaced { get; set; }
        [AntiXss]
        public string ktb_saleschanneltype { get; set; }
        [AntiXss]
        public string owneridyominame { get; set; }
        [AntiXss]
        public string ktb_isinterfacedname { get; set; }
        [AntiXss]
        public DateTime modifiedon { get; set; }
        [AntiXss]
        public string ktb_isdefaultoncreatewoname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }
        [AntiXss]
        public int statuscode { get; set; }
        [AntiXss]
        public string createdbyname { get; set; }
        [AntiXss]
        public DateTime createdon { get; set; }
        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }
        [AntiXss]
        public Int64 versionnumber { get; set; }
        [AntiXss]
        public Guid modifiedonbehalfby { get; set; }
        [AntiXss]
        public Guid ownerid { get; set; }
        [AntiXss]
        public DateTime overriddencreatedon { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
