#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_MSPMembershipParameterDto  class
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
    public class VWI_MSPMembershipParameterDto : IValidatableObject
    {
        public Int64 ID { get; set; }
        public int MSPCustomerID { get; set; }
        public int DealerId { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string DealerName { get; set; }
        public int ChassisMasterID { get; set; }
        [AntiXss]
        public string MSPCode { get; set; }
        [AntiXss]
        public string ChassisNumber { get; set; }
        [AntiXss]
        public string ColorCode { get; set; }
        [AntiXss]
        public string VehicleTypeCode { get; set; }
        [AntiXss]
        public string VehicleTypeDesc { get; set; }
        public int MSPKm { get; set; }
        public int Duration { get; set; }
        [AntiXss]
        public string Description { get; set; }
        public DateTime ValidUntil { get; set; }
        public DateTime RegistrationDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

