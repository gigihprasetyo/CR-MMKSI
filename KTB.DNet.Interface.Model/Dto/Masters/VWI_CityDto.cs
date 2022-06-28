#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_CityDto  class
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
    public class VWI_CityDto
    {
        public int ID { get; set; }

        public string ProvinceCode { get; set; }

        public string ProvinceName { get; set; }

        public string CityCode { get; set; }

        public string CityName { get; set; }

        public int Status { get; set; }

        public DateTime LastUpdateTime { get; set; }
    }
}
