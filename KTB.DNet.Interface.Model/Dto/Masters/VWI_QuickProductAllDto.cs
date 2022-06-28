#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_QuickProductDto  class
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
    public class VWI_QuickProductAllDto
    {
        public Int64 ID { get; set; }
        //public string DealerCode { get; set; } // CR Delaer category
        public string VehicleType { get; set; }
        public string VehicleDesc { get; set; }
        public string ProductCategory { get; set; }
        public string VehicleCatDesc { get; set; }
        public string ColorCode { get; set; }
        public string ColorDescription { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel_S1 { get; set; }
        public string VehicleCategory_S2 { get; set; }
        public string ProductSegment_S3 { get; set; }
        public string DriveSystem_S4 { get; set; }
        public string VariantType { get; set; }
        public string TransmitType { get; set; }
        public string DriveSystemType { get; set; }
        public string SpeedType { get; set; }
        public string FuelType { get; set; }
        public string SpecialFlag { get; set; }
        public int ModelYear { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
