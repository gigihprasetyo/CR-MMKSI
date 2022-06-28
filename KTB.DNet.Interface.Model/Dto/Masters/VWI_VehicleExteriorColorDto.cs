#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_VehicleExteriorColorDto  class
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
using System.Runtime.Serialization;

namespace KTB.DNet.Interface.Model
{
    public class VWI_VehicleExteriorColorDto
    {
        [IgnoreDataMember]
        public int ID { get; set; }

        public string ColorCode { get; set; }

        public string Description { get; set; }

        [IgnoreDataMember]
        public string LastUpdateBy { get; set; }

        public DateTime LastUpdateTime { get; set; }

        [IgnoreDataMember]
        public int RowStatus { get; set; }
    }
}
