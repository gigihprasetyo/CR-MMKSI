#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_equipmentParameterDto  class
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
	public class VWI_CRM_xts_partsforecastParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
		public string company { get; set; }
		[AntiXss]
		public string businessunitcode { get; set; }
		[AntiXss]
		public string xts_state { get; set; }
		[AntiXss]
		public string createdonbehalfbyyominame { get; set; }
		[AntiXss]
		public Guid modifiedonbehalfby { get; set; }
		[AntiXss]
		public Guid owningteam { get; set; }
		[AntiXss]
		public Int32 statecode { get; set; }
		[AntiXss]
		public string xts_businessunitidname { get; set; }
		[AntiXss]
		public string xts_partsforecast { get; set; }
		[AntiXss]
		public string statecodename { get; set; }
		[AntiXss]
		public Guid owninguser { get; set; }
		[AntiXss]
		public Guid createdonbehalfby { get; set; }
		[AntiXss]
		public string xts_handling { get; set; }
		[AntiXss]
		public Int32 importsequencenumber { get; set; }
		[AntiXss]
		public Guid xts_partsforecastid { get; set; }
		[AntiXss]
		public Int32 utcconversiontimezonecode { get; set; }
		[AntiXss]
		public string createdbyyominame { get; set; }
		[AntiXss]
		public Guid owningbusinessunit { get; set; }
		[AntiXss]
		public string modifiedbyname { get; set; }
		[AntiXss]
		public Int64 versionnumber { get; set; }
		[AntiXss]
		public Guid modifiedby { get; set; }
		[AntiXss]
		public string modifiedbyyominame { get; set; }
		[AntiXss]
		public Guid createdby { get; set; }
		[AntiXss]
		public Int32 timezoneruleversionnumber { get; set; }
		[AntiXss]
		public string owneridtype { get; set; }
		[AntiXss]
		public string xts_handlingname { get; set; }
		[AntiXss]
		public string xts_statename { get; set; }
		[AntiXss]
		public string owneridyominame { get; set; }
		[AntiXss]
		public DateTime modifiedon { get; set; }
		[AntiXss]
		public string xts_forecastyear { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyyominame { get; set; }
		[AntiXss]
		public Int32 statuscode { get; set; }
		[AntiXss]
		public string createdbyname { get; set; }
		[AntiXss]
		public DateTime createdon { get; set; }
		[AntiXss]
		public Guid xts_businessunitid { get; set; }
		[AntiXss]
		public string createdonbehalfbyname { get; set; }
		[AntiXss]
		public string statuscodename { get; set; }
		[AntiXss]
		public string modifiedonbehalfbyname { get; set; }
		[AntiXss]
		public string xts_forecastmonth { get; set; }
		[AntiXss]
		public string owneridname { get; set; }
		[AntiXss]
		public string xts_forecastmonthname { get; set; }
		[AntiXss]
		public Guid ownerid { get; set; }
		[AntiXss]
		public DateTime overriddencreatedon { get; set; }
		[AntiXss]
		public string msdyn_companycode { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}
