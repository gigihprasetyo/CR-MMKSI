
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Stall Working Time class
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
    public class StallWorkingTimeDto : DtoBase
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public DateTime Tanggal { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime RestTimeStart { get; set; }
        public DateTime RestTimeEnd { get; set; }
        public int StallMasterID { get; set; }
        public int IsHoliday { get; set; }
        public int VisitType { get; set; }
        public string Notes { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }
}

