#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_Zombie_WOTimeRegister  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-13
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_Zombie_WOTimeRegister
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
