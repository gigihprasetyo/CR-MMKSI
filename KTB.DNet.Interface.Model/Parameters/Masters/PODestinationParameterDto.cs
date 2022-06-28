#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PODestination  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 10/09/2020 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Embarr.WebAPI.AntiXss;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model.CustomAttribute;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace KTB.DNet.Interface.Model
{
    public class PODestinationParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [AntiXss]
        public string Code { get; set; }

        [AntiXss]
        public string Nama { get; set; }

        [AntiXss]
        public string Alamat { get; set; }

        [AntiXss]
        public string CityCode { get; set; }

        [AntiXss]
        public string DealerCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
