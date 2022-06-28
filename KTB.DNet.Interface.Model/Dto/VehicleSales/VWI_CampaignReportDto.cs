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
    public class VWI_CampaignReportDto
    {
        public int ID { get; set; }
        public int HeaderID { get; set; }
        public string NomorSurat { get; set; }
        public int Status { get; set; }
        public string BenefitRegNo { get; set; }
        public string Remarks { get; set; }
        public int RowStatus { get; set; }
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public DateTime FakturValidationStart { get; set; }
        public DateTime FakturValidationEnd { get; set; }
        public DateTime FakturOpenStart { get; set; }
        public DateTime FakturOpenEnd { get; set; }
        public int VehicleTypeID { get; set; }
        public string VehicleTypeCode { get; set; }
        public string VehicleTypeDesc { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}