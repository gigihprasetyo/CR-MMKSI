#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_cashandbankParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:26:05
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
    public class VWI_CRM_xts_cashandbankParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
		public string createdonbehalfbyyominame { get; set; }

		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }

		[AntiXss]
		public int statecode { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension2id { get; set; }

		[AntiXss]
		public string statecodename { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension6id { get; set; }

		[AntiXss]
		public string xts_bankaccountnumber { get; set; }

		[AntiXss]
		public Guid createdonbehalfby { get; set; }

		[AntiXss]
		public string xjp_accounttype { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension2idname { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension9idname { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension1idname { get; set; }

		[AntiXss]
		public string xjp_accounttypename { get; set; }

		[AntiXss]
		public string xts_currency { get; set; }

		[AntiXss]
		public Guid xts_cashandbankid { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension7idname { get; set; }

		[AntiXss]
		public int importsequencenumber { get; set; }

		[AntiXss]
		public string organizationidname { get; set; }

		[AntiXss]
		public string modifiedbyyominame { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension7id { get; set; }

		[AntiXss]
		public string xts_bankidname { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension8id { get; set; }

		[AntiXss]
		public int utcconversiontimezonecode { get; set; }

		[AntiXss]
		public string createdbyyominame { get; set; }

		[AntiXss]
		public string xts_cashandbankaccountidname { get; set; }

		[AntiXss]
		public string modifiedbyname { get; set; }

		[AntiXss]
		public Int64 versionnumber { get; set; }

		[AntiXss]
		public Guid modifiedby { get; set; }

		[AntiXss]
		public Guid createdby { get; set; }

		[AntiXss]
		public int timezoneruleversionnumber { get; set; }

		[AntiXss]
		public string statuscodename { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension6idname { get; set; }

		[AntiXss]
		public string xts_company { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension4idname { get; set; }

		[AntiXss]
		public string xts_accountnumber { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension10id { get; set; }

		[AntiXss]
		public DateTime modifiedon { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension10idname { get; set; }

		[AntiXss]
		public Guid xts_cashandbankaccountid { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension3id { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension5id { get; set; }

		[AntiXss]
		public string xts_accountname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }

		[AntiXss]
		public int statuscode { get; set; }

		[AntiXss]
		public string createdbyname { get; set; }

		[AntiXss]
		public DateTime createdon { get; set; }

		[AntiXss]
		public Guid organizationid { get; set; }

		[AntiXss]
		public string createdonbehalfbyname { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension5idname { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension3idname { get; set; }

		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension1id { get; set; }

		[AntiXss]
		public string xts_cashandbankdimension8idname { get; set; }

		[AntiXss]
		public Guid xts_bankid { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension9id { get; set; }

		[AntiXss]
		public DateTime overriddencreatedon { get; set; }

		[AntiXss]
		public Guid xts_cashandbankdimension4id { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
