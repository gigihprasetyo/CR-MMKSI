#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : ServiceTemplateDetail Parameter  class
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
	public class ServiceTemplateDetailParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
		public string IDRow { get; set; }
		[AntiXss]
		public string ServiceTemplateHeaderID { get; set; }
		[AntiXss]
		public string ServiceTemplate { get; set; }
		[AntiXss]
		public string ServiceTemplateDetail { get; set; }
		[AntiXss]
		public string ProductType { get; set; }
		[AntiXss]
		public string Product { get; set; }
		[AntiXss]
		public string ProductDescription { get; set; }
		[AntiXss]
		public string PartCode { get; set; }
		[AntiXss]
		public string PartCodeDescription { get; set; }
		[AntiXss]
		public string Quantity { get; set; }
		[AntiXss]
		public string UnitPrice { get; set; }
		[AntiXss]
		public string TotalPrice { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}
