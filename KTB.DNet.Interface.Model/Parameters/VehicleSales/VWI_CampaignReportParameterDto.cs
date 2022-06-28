#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_CampaignReportParameterDto  class
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
    public class VWI_CampaignReportParameterDto : IValidatableObject
    {
        public int ID { get; set; }
        [AntiXss]
        public string NomorSurat { get; set; }
        public int Status { get; set; }
        [AntiXss]
        public string BenefitRegNo { get; set; }
        [AntiXss]
        public string Remarks { get; set; }
        public int DetailRowStatus { get; set; }
        public int DealerID { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        public DateTime FakturValidationStart { get; set; }
        public DateTime FakturValidationEnd { get; set; }
        public DateTime FakturOpenStart { get; set; }
        public DateTime FakturOpenEnd { get; set; }
        public int VechileTypeID { get; set; }
        [AntiXss]
        public string VechileTypeCode { get; set; }
        [AntiXss]
        public string VehicleTypeDesc { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
