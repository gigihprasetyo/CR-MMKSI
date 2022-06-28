#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_serviceParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-02-11 10:29
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
	public class VWI_CRM_serviceParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }
		[AntiXss]
		public string granularity { get; set; }
		[AntiXss]
		public string description { get; set; }
		[AntiXss]
		public Guid createdonbehalfby { get; set; }
		[AntiXss]
		public string name { get; set; }
		[AntiXss]
		public Guid serviceid { get; set; }
		[AntiXss]
		public Guid resourcespecid { get; set; }
		[AntiXss]
		public bool isvisible { get; set; }
		[AntiXss]
		public int importsequencenumber { get; set; }
		[AntiXss]
		public Guid organizationid { get; set; }
		[AntiXss]
		public string modifiedbyyominame { get; set; }
		[AntiXss]
		public string isschedulablename { get; set; }
		[AntiXss]
		public bool isschedulable { get; set; }
		[AntiXss]
		public string showresourcesname { get; set; }
		[AntiXss]
		public int duration { get; set; }
		[AntiXss]
		public int utcconversiontimezonecode { get; set; }
		[AntiXss]
		public string createdbyyominame { get; set; }
		[AntiXss]
		public string modifiedbyname { get; set; }
		[AntiXss]
		public Int64 versionnumber { get; set; }
		[AntiXss]
		public string initialstatuscodename { get; set; }
		[AntiXss]
		public Guid modifiedby { get; set; }
		[AntiXss]
		public Guid createdby { get; set; }
		[AntiXss]
		public int timezoneruleversionnumber { get; set; }
		[AntiXss]
		public string organizationidname { get; set; }
		[AntiXss]
		public Guid strategyid { get; set; }
		[AntiXss]
		public string resourcespecidname { get; set; }
		[AntiXss]
		public string isvisiblename { get; set; }
		[AntiXss]
		public Guid calendarid { get; set; }
		[AntiXss]
		public DateTime modifiedon { get; set; }
		[AntiXss]
		public int anchoroffset { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }
		[AntiXss]
		public string createdbyname { get; set; }
		[AntiXss]
		public DateTime createdon { get; set; }
		[AntiXss]
		public int initialstatuscode { get; set; }
		[AntiXss]
		public string createdonbehalfbyname { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }
		[AntiXss]
		public bool showresources { get; set; }
		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }
		[AntiXss]
		public DateTime overriddencreatedon { get; set; }
		[AntiXss]
		public bool ktb_isurs { get; set; }
		[AntiXss]
		public Guid msdyn_requirementgroupid { get; set; }
		[AntiXss]
		public string msdyn_requirementgroupidname { get; set; }
		[AntiXss]
		public string msdyn_SchedulingEngine { get; set; }
		[AntiXss]
		public string ktb_isursname { get; set; }
		[AntiXss]
		public string msdyn_schedulingenginename { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}
