#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceValidatorDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/10/2018 9:47
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class ServiceValidatorDto : DtoBase
    {
        public int ID { get; set; }
        public string ChassisNumber { get; set; }
        public string DealerCode { get; set; }
        public string DealerBranchCode { get; set; }
        public string Kind { get; set; }
        public string PDIStatus { get; set; }
        public DateTime PDIDate { get; set; }
        public string ReleaseBy { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string WorkOrderNumber { get; set; }
    }
}

