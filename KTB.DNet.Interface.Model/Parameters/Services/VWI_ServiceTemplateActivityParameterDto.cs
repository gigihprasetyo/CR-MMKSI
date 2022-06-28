
#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_ServiceTemplateActivityParameterDto class
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
	public class VWI_ServiceTemplateActivityParameterDto : ParameterDtoBase, IValidatableObject
	{
		[AntiXss]
        public Int32 ID { get; set; }

        [AntiXss]
        public string ServiceType { get; set; }

        [AntiXss]
        public Int32 ServiceTemplateHeaderID { get; set; }

        [AntiXss]
        public Int32 KindID { get; set; }

        [AntiXss]
        public Int32 VechileTypeID { get; set; }

        [AntiXss]
        public DateTime ValidFrom { get; set; }

        [AntiXss]
        public string KindCode { get; set; }

        [AntiXss]
        public string KindDescription { get; set; }

        [AntiXss]
        public string VehicleTypeCode { get; set; }

        [AntiXss]
        public string ActName { get; set; }

        [AntiXss]
        public string ActSequence { get; set; }

        [AntiXss]
        public Decimal Duration { get; set; }

        [AntiXss]
        public string SVCTemplateActivity { get; set; }

        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			return results;
		}
	}
}

