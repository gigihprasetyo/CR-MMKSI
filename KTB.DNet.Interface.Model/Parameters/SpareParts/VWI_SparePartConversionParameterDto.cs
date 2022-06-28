#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SparePartConversionParameterDto  class
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
    public class VWI_SparePartConversionParameterDto : DtoBase, IValidatableObject
    {
        public int ID { get; set; }
        [AntiXss]
        public string PartNumber { get; set; }
        [AntiXss]
        public string PartName { get; set; }
        [AntiXss]
        public string UOMFrom { get; set; }
        [AntiXss]
        public string UOMTo { get; set; }
        public int Qty { get; set; }
        [AntiXss]
        public string ProductType { get; set; }
        [AntiXss]
        public string PartNumberReff { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}

