#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : SPPenaltyDetailParameter  class
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
	public class SparePartPenaltyDetailParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
		public string IDRow { get; set; }
		[AntiXss]
		public string ID { get; set; }
		[AntiXss]
		public string TOPSPPenaltyID { get; set; }
		[AntiXss]
		public decimal AmountPenalty { get; set; }
		[AntiXss]
		public string BillingNumber { get; set; }
		[AntiXss]
		public string DoNumber { get; set; }

		public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}

