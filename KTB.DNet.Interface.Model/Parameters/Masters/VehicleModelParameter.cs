#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleModelParameter  class
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
    public class VehicleModelParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public string SAPCode { get; set; }
        [AntiXss]
        public string VechileModelCode { get; set; }
        public byte CategoryID { get; set; }
        [AntiXss]
        public string Description { get; set; }
        [AntiXss]
        public string VechileModelIndCode { get; set; }
        [AntiXss]
        public string IndDescription { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
