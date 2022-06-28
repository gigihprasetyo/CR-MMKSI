#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_CampaignReportDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class VWI_CampaignDto
    {
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string DealerBranchCode { get; set; }
        public string CampaignCode { get; set; }
        public Int32 CampaignType { get; set; }
        public string CampaignTypeCode { get; set; }
        public string CampaignTypeDesc { get; set; }
        public string CampaignName { get; set; }
        public string DealerCampaignName { get; set; }
        public string BabitDealerNumber { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public String Location { get; set; }
        public DateTime CampaignDate { get; set; }
        public string LocationName { get; set; }
        public int LuasArea { get; set; }
        public int ProspectTarget { get; set; }
        public int SPKTarget { get; set; }
        public int InvitationQty { get; set; }
        public string BabitCategory { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public int EventType { get; set; }
    }
}
