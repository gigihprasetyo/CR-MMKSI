#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleTypeParameterDto  class
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
    public class VehicleTypeParameterDto : ParameterDtoBase, IValidatableObject
    {
        public int ID { get; set; }

        [AntiXss]
        public string VechileTypeCode { get; set; }
        public int ModelID { get; set; }
        public byte CategoryID { get; set; }
        public int ProductCategoryID { get; set; }
        [AntiXss]
        public string Description { get; set; }
        [AntiXss]
        public string Status { get; set; }
        public int VehicleClassID { get; set; }
        public byte IsVehicleKind1 { get; set; }
        public byte IsVehicleKind2 { get; set; }
        public byte IsVehicleKind3 { get; set; }
        public byte IsVehicleKind4 { get; set; }
        public int MaxTOPDays { get; set; }
        [AntiXss]
        public string SAPModel { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
