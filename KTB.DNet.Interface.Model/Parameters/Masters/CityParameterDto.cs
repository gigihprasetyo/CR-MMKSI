#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CityParameterDto  class
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
    public class CityParameterDto : ParameterDtoBase, IValidatableObject
    {
        [AntiXss]
        [Display(ResourceType = typeof(FieldResource), Name = "CityCode")]
        public string CityCode { get; set; }

        [AntiXss]
        [Display(ResourceType = typeof(FieldResource), Name = "CityName")]
        public string CityName { get; set; }

        [Display(ResourceType = typeof(FieldResource), Name = "StatusKota")]
        public int Status { get; set; }

        [AntiXss]
        [Display(ResourceType = typeof(FieldResource), Name = "ProvinceCode")]
        public string ProvinceCode { get; set; }

        [AntiXss]
        [Display(ResourceType = typeof(FieldResource), Name = "ProvinceName")]
        public string ProvinceName { get; set; }

        public new DateTime LastUpdateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
