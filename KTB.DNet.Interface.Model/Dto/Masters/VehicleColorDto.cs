#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleColorDto  class
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
    public class VehicleColorDto : DtoBase
    {
        public int ID { get; set; }

        public string ColorCode { get; set; }

        public string ColorIndName { get; set; }

        public string ColorEngName { get; set; }

        public string MaterialNumber { get; set; }

        public string MaterialDescription { get; set; }

        public string HeaderBOM { get; set; }

        public string MarketCode { get; set; }

        public string SpecialFlag { get; set; }

        public string Status { get; set; }

        public VehicleTypeDto VehicleType { get; set; }
    }
}
