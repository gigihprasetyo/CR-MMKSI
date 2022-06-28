#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitMasterDetailParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class BenefitMasterDetailParameterDto : ParameterDtoBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public int ID { get; set; }

        [AntiXss]
        public string FormulaID { get; set; }

        [AntiXss]
        public int BenefitMasterHeaderID { get; set; }

        public int BenefitTypeID { get; set; }

        [AntiXss]
        public string Description { get; set; }

        public Decimal Amount { get; set; }

        public DateTime FakturValidationStart { get; set; }

        public DateTime FakturValidationEnd { get; set; }

        public DateTime FakturOpenStart { get; set; }

        public DateTime FakturOpenEnd { get; set; }

        public int AssyYear { get; set; }

        public int MaxClaim { get; set; }

        public int WSDiscount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}