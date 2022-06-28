#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_QuickProductParameterDto  class
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
    public class VWI_QuickProductParameterDto : DtoBase, IValidatableObject
    {
        public Int64 ID { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string VehicleType { get; set; }
        [AntiXss]
        public string VehicleDesc { get; set; }
        [AntiXss]
        public string ProductCategory { get; set; }
        [AntiXss]
        public string VehicleCatDesc { get; set; }
        [AntiXss]
        public string ColorCode { get; set; }
        [AntiXss]
        public string ColorDescription { get; set; }
        [AntiXss]
        public string VehicleBrand { get; set; }
        [AntiXss]
        public string VehicleModel_S1 { get; set; }
        [AntiXss]
        public string VehicleCategory_S2 { get; set; }
        [AntiXss]
        public string ProductSegment_S3 { get; set; }
        [AntiXss]
        public string DriveSystem_S4 { get; set; }
        public int Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
