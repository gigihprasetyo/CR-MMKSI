#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipPOParameterDto  class
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
    public class EstimationEquipPOParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public int EstimationEquipDetailID { get; set; }
        public int IndentPartDetailID { get; set; }
        [AntiXss]
        public string Note { get; set; }
        [AntiXss]
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

