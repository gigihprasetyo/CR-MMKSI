﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeePartsParameterDto  class
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_EmployeeResignParameterDto : ParameterDtoBase, IValidatableObject
    {

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            return results;
        }
    }
}
