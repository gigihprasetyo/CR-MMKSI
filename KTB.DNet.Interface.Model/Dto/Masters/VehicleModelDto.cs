#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleModelDto  class
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
    public class VehicleModelDto : DtoBase
    {
        public int ID { get; set; }
        //public string SAPCode { get; set; }
        public string VechileModelCode { get; set; }
        public byte CategoryID { get; set; }
        public string Description { get; set; }
        public string VechileModelIndCode { get; set; }
        public string IndDescription { get; set; }
    }
}
