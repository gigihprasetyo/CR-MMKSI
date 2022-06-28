#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FleetDto  class
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
    public class FleetDto : DtoBase
    {
        public int ID { get; set; }
        public string FleetCode { get; set; }
        public string FleetName { get; set; }
        public string FleetNickName { get; set; }
        public string FleetGroup { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public int BusinessSectorHeaderId { get; set; }
        public int BusinessSectorDetailId { get; set; }
        public string FleetNote { get; set; }
    }
}

