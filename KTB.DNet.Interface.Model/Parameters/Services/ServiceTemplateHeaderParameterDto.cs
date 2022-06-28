#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : ServiceTemplateHeader Parameter  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:16:28
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
	public class ServiceTemplateHeaderParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
		public string IDRow { get; set; }
		[AntiXss]
		public string ServiceTemplate { get; set; }
		[AntiXss]
		public string ServiceTemplateGroup { get; set; }
		[AntiXss]
		public string ServiceTemplateSubGroup { get; set; }
		[AntiXss]
		public string Description { get; set; }
		[AntiXss]
		public string ServiceCategory { get; set; }
		[AntiXss]
		public int IntervalTime { get; set; }
		[AntiXss]
		public string ServiceTemplateVehiclePricePattern { get; set; }
		[AntiXss]
		public string CalculationMethod { get; set; }
		[AntiXss]
		public string KindCode { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}
