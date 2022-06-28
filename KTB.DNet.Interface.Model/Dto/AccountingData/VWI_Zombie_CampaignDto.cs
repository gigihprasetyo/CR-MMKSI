#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_Zombie_CampaignDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class VWI_Zombie_CampaignDto : DtoBase
    {
        public Guid Id { get; set; }
        public string DealerCode { get; set; }
        public string DealerCompany { get; set; }
        public bool InvalidZombieData { get; set; }
        public string Category_ZombieData { get; set; }
        public int? Category_ZombieDataCode { get; set; }
        public DateTime LastCheckedTime { get; set; }
    }
}
