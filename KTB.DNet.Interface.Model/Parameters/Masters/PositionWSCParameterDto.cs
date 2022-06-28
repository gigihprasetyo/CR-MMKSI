#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PositionWSCParameterDto  class
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
    public class PositionWSCParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        [AntiXss]
        public string PositionCategory { get; set; }
        [AntiXss]
        public string PositionCode { get; set; }
        [AntiXss]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

