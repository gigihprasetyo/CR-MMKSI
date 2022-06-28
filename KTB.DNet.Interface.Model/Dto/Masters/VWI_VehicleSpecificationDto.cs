#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_VehicleSpecificationDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_VehicleSpecificationDto : ReadDtoBase
    {
        //public int ID { get; set; }
        public string VehicleCategory_S1 { get; set; }
        public string ClassificationNumber { get; set; }
        public string VehicleDesc { get; set; }
        public string ProductCategory { get; set; }
        public string VehicleCatDesc { get; set; }
        public string VehicleBrand { get; set; }
        public string SpeedType { get; set; }
        public string FuelType { get; set; }
        public string Transmition { get; set; }
        public string Drivesystem { get; set; }
        public string SegmentType { get; set; }
        public int Status { get; set; }
    }
}

