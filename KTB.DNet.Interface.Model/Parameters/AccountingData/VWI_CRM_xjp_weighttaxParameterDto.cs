#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_weighttaxParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 15:33:00
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
	public class VWI_CRM_xjp_weighttaxParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
		public Guid xjp_weighttaxdimension4id { get; set; }
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }
		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }
		[AntiXss]
		public Guid xjp_weighttaxdimension1id { get; set; }
		[AntiXss]
		public string xjp_weighttax { get; set; }
		[AntiXss]
		public string statecodename { get; set; }
		[AntiXss]
		public Guid createdonbehalfby { get; set; }
		[AntiXss]
		public Guid xjp_weighttaxdimension2id { get; set; }
		[AntiXss]
		public Guid xjp_weighttaxdimension5id { get; set; }
		[AntiXss]
		public int importsequencenumber { get; set; }
		[AntiXss]
		public string organizationidname { get; set; }
		[AntiXss]
		public string modifiedbyyominame { get; set; }
		[AntiXss]
		public int statecode { get; set; }
		[AntiXss]
		public string xjp_weighttaxdimension3idname { get; set; }
		[AntiXss]
		public int utcconversiontimezonecode { get; set; }
		[AntiXss]
		public string createdbyyominame { get; set; }
		[AntiXss]
		public string modifiedbyname { get; set; }
		[AntiXss]
		public Int64 versionnumber { get; set; }
		[AntiXss]
		public Guid modifiedby { get; set; }
		[AntiXss]
		public string xjp_locking { get; set; }
		[AntiXss]
		public Guid createdby { get; set; }
		[AntiXss]
		public int timezoneruleversionnumber { get; set; }
		[AntiXss]
		public Guid xjp_weighttaxid { get; set; }
		[AntiXss]
		public string xjp_weighttaxdimension6idname { get; set; }
		[AntiXss]
		public string xjp_weighttaxdimension1idname { get; set; }
		[AntiXss]
		public string xjp_description { get; set; }
		[AntiXss]
		public Guid xjp_weighttaxdimension6id { get; set; }
		[AntiXss]
		public DateTime modifiedon { get; set; }
		[AntiXss]
		public string xjp_weighttaxdimension2idname { get; set; }
		[AntiXss]
		public string xjp_weighttaxdimension4idname { get; set; }
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
		public string statuscodename { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }
		[AntiXss]
		public Guid xjp_weighttaxdimension3id { get; set; }
		[AntiXss]
		public Guid xjp_accountid { get; set; }
		[AntiXss]
		public string xjp_weighttaxdimension5idname { get; set; }
		[AntiXss]
		public DateTime overriddencreatedon { get; set; }
		[AntiXss]
		public string xjp_accountidname { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}
