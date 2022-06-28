#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_gljournalParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-09-28
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
    public class VWI_CRM_xts_gljournalParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        public string createdonbehalfbyyominame { get; set; }
        public Guid modifiedonbehalfby { get; set; }
        public int statecode { get; set; }

        [AntiXss]
        public string xts_postinglayer { get; set; }

        [AntiXss]
        public string xts_gljournalnumber { get; set; }
        public int statuscode { get; set; }

        [AntiXss]
        public string statecodename { get; set; }

        [AntiXss]
        public string xts_documentnumber { get; set; }

        [AntiXss]
        public string xts_currency { get; set; }
        public Guid createdonbehalfby { get; set; }
        public int importsequencenumber { get; set; }
        public Guid organizationid { get; set; }

        [AntiXss]
        public string modifiedonbehalfbyname { get; set; }

        [AntiXss]
        public string organizationidname { get; set; }
        public Guid xts_gljournalid { get; set; }
        public DateTime xts_posteddateandtime { get; set; }
        public int utcconversiontimezonecode { get; set; }

        [AntiXss]
        public string createdbyyominame { get; set; }

        [AntiXss]
        public string modifiedbyname { get; set; }
        public Int64 versionnumber { get; set; }
        public Guid modifiedby { get; set; }

        [AntiXss]
        public string modifiedbyyominame { get; set; }

        [AntiXss]
        public string xts_company { get; set; }
        public int timezoneruleversionnumber { get; set; }

        [AntiXss]
        public string xts_transactiongroupname { get; set; }

        [AntiXss]
        public string statuscodename { get; set; }
        public bool xts_posted { get; set; }

        [AntiXss]
        public string xts_journaltype { get; set; }

        [AntiXss]
        public string xts_journaltypename { get; set; }
        public DateTime modifiedon { get; set; }

        [AntiXss]
        public string xts_journalnameidname { get; set; }
        public Guid xts_journalnameid { get; set; }

        [AntiXss]
        public string modifiedonbehalfbyyominame { get; set; }

        [AntiXss]
        public string xts_postinglayername { get; set; }

        [AntiXss]
        public string createdbyname { get; set; }
        public DateTime createdon { get; set; }

        [AntiXss]
        public string xts_transactiongroup { get; set; }

        [AntiXss]
        public string xts_log { get; set; }

        [AntiXss]
        public string createdonbehalfbyname { get; set; }
        public Guid createdby { get; set; }

        [AntiXss]
        public string xts_description { get; set; }

        [AntiXss]
        public string xts_postedname { get; set; }
        public DateTime overriddencreatedon { get; set; }

        [AntiXss]
        public string DealerCode { get; set; }

        [AntiXss]
        public string SourceType { get; set; }
        public int RowStatus { get; set; }
        public DateTime LastSyncDate { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
