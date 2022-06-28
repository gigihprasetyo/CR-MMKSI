#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PDIFilterDto  class
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
    public class PDIFilterDto : FilterDtoBase
    {
        public string ChassisNumber { get; set; }
        public string DealerCode { get; set; }
        public string PDIStatus { get; set; }
        public DateTime PDIDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
