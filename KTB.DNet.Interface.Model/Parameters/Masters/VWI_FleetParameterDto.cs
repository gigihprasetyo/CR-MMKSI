﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_FleetParameterDto  class
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
    public class VWI_FleetParameterDto : DtoBase, IValidatableObject
    {
        public int ID { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string DealerName { get; set; }
        [AntiXss]
        public string CustomerCode { get; set; }
        [AntiXss]
        public string FleetCode { get; set; }
        [AntiXss]
        public string FleetCustomerName { get; set; }
        [AntiXss]
        public string FleetCustomerAddress { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            return results;
        }
    }
}

