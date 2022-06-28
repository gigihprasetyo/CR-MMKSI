#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_assigntosalespersonParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 1/9/2020 2:44:54 PM
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
    public class VWI_CRM_xts_assigntosalespersonParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string company { get; set; }

		[AntiXss]
		public string businessunitcode { get; set; }

		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid owninguser { get; set; }

		[AntiXss]
		public Guid xts_tono { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public string xts_businessunitidname { get; set; }

		[AntiXss]
		public string xts_pkcombinationkey { get; set; }

		[AntiXss]
		public string owneridname { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xts_fromsalespersonname { get; set; }

		[AntiXss]
		public string xts_tonoidname { get; set; }

		[AntiXss]
		public string xts_assigntypename { get; set; }

		[AntiXss]
		public string xts_fromnoidname { get; set; }

		[AntiXss]
		public string xts_fromsalespersonidname { get; set; }

		[AntiXss]
		public string xts_tosalespersonidname { get; set; }

		[AntiXss]
		public string ktb_bypasslimitname { get; set; }

		[AntiXss]
		public string xts_tononame { get; set; }

		[AntiXss]
		public string xts_assigntype { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid xts_fromno { get; set; }

		[AntiXss]
		public string ktb_parentbusinessunitidname { get; set; }

		[AntiXss]
		public DateTime xts_transactiondate { get; set; }

		[AntiXss]
		public Guid xts_fromsalespersonid { get; set; }

		[AntiXss]
		public Guid xts_fromnoid { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public Guid xts_fromsalesperson { get; set; }

		[AntiXss]
		public Guid owningbusinessunit { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Guid owningteam { get; set; }

		[AntiXss]
		public bool ktb_bypasslimit { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public Guid xts_tonoid { get; set; }

		[AntiXss]
		public string owneridtype { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_tosalespersonname { get; set; }

		[AntiXss]
		public Guid xts_tosalespersonid { get; set; }

		[AntiXss]
		public string owneridyominame { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public Guid xts_tosalesperson { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid xts_businessunitid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_fromnoname { get; set; }

		[AntiXss]
		public Guid xts_assigntosalespersonid { get; set; }

		[AntiXss]
		public Guid ktb_parentbusinessunitid { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public string xts_assigntosalesperson { get; set; }

		[AntiXss]
		public Guid ownerid { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_parentbusinessunitid { get; set; }

		[AntiXss]
		public string xts_parentbusinessunitidname { get; set; }

		[AntiXss]
		public string msdyn_companycode { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
