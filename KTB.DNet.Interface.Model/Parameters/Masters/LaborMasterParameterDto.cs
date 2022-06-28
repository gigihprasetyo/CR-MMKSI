#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : LaborMasterParameterDto  class
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
    public class LaborMasterParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        public int VechileTypeID { get; set; }
        [AntiXss]
        public string LaborCode { get; set; }
        [AntiXss]
        public string WorkCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

