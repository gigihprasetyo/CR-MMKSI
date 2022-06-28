#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_CampaignParameterDto  class
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
    public class VWI_CampaignParameterDto : IValidatableObject
    {
        public int ID { get; set; }
        [AntiXss]
        public string DealerCode { get; set; }
        [AntiXss]
        public string DealerBranchCode { get; set; }
        [AntiXss]
        public string CampaignCode { get; set; }
        public Int32 CampaignType { get; set; }
        [AntiXss]
        public string CampaignTypeCode { get; set; }
        [AntiXss]
        public string CampaignTypeDesc { get; set; }
        [AntiXss]
        public string CampaignName { get; set; }
        [AntiXss]
        public string DealerCampaignName { get; set; }
        [AntiXss]
        public string BabitDealerNumber { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        [AntiXss]
        public String Location { get; set; }
        public DateTime CampaignDate { get; set; }
        [AntiXss]
        public string LocationName { get; set; }
        public int LuasArea { get; set; }
        public int ProspectTarget { get; set; }
        public int SPKTarget { get; set; }
        public int InvitationQty { get; set; }
        [AntiXss]
        public string BabitCategory { get; set; }
        [AntiXss]
        public string CityCode { get; set; }
        [AntiXss]
        public string CityName { get; set; }
        [AntiXss]
        public string ProvinceCode { get; set; }
        [AntiXss]
        public string ProvinceName { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
