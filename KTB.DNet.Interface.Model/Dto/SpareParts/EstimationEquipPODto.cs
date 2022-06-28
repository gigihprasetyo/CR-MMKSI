#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipPODto  class
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
    public class EstimationEquipPODto : DtoBase
    {
        public int ID { get; set; }
        public int EstimationEquipDetailID { get; set; }
        public int IndentPartDetailID { get; set; }
        public string Note { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}

