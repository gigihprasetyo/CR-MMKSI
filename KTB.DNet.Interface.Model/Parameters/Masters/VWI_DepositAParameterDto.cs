#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_DepositAParameterDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 13 Sep 2021
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
    public class VWI_DepositAParameterDto : ParameterDtoBase, IValidatableObject
    {
        public System.Collections.Generic.IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
