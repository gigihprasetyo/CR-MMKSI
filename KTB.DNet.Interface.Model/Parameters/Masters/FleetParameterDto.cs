#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FleetParameterDto  class
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
    public class FleetParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }
        [AntiXss]
        public string FleetCode { get; set; }
        [AntiXss]
        public string FleetName { get; set; }
        [AntiXss]
        public string FleetNickName { get; set; }
        [AntiXss]
        public string FleetGroup { get; set; }
        [AntiXss]
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int IdentityType { get; set; }
        [AntiXss]
        public string IdentityNumber { get; set; }
        public int BusinessSectorHeaderId { get; set; }
        public int BusinessSectorDetailId { get; set; }
        [AntiXss]
        public string FleetNote { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

