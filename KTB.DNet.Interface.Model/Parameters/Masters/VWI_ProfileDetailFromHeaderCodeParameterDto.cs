#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Area1ParameterDto  class
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
    public class VWI_ProfileDetailFromHeaderCodeParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        public int ProfileHeaderID { get; set; }

        [AntiXss]
        public string ProfileHeaderCode { get; set; }

        [AntiXss]
        public string ProfileHeaderDesc { get; set; }

        [AntiXss]
        public string ProfileDetailCode { get; set; }

        [AntiXss]
        public string ProfileDetailDesc { get; set; }

        public int Status { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
