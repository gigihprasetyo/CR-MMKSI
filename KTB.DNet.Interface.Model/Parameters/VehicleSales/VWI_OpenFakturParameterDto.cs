#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_OpenFakturParameterDto  class
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
    public class VWI_OpenFakturParameterDto : IValidatableObject
    {
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string ChassisNumber { get; set; }
        public DateTime OpenFactureDate { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
