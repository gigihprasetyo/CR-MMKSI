#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleColorParameterDto  class
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
    public class VehicleColorParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [AntiXss]
        public string ColorCode { get; set; }
        [AntiXss]
        public string ColorIndName { get; set; }
        [AntiXss]
        public string ColorEngName { get; set; }
        [AntiXss]
        public string MaterialNumber { get; set; }
        [AntiXss]
        public string MaterialDescription { get; set; }
        [AntiXss]
        public string HeaderBOM { get; set; }
        [AntiXss]
        public string MarketCode { get; set; }
        [AntiXss]
        public string SpecialFlag { get; set; }
        [AntiXss]
        public string Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
