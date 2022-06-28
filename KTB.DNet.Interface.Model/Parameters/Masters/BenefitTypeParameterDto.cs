#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitTypeParameterDto  class
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class BenefitTypeParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [AntiXss]
        public string Name { get; set; }

        public int LeasingBox { get; set; }

        public int AssyYearBox { get; set; }

        public int ReceiptBox { get; set; }

        public int EventValidation { get; set; }

        public int WSDiscount { get; set; }

        public int Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}