#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PDIDto  class
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
    public class PDIDto : DtoBase
    {
        [IgnoreDataMember]
        public int ID { get; set; }
        public string ChassisNumber { get; set; }
        public string DealerCode { get; set; }
        public string DealerBranchCode { get; set; }
        public string Kind { get; set; }
        public string PDIStatus { get; set; }
        public DateTime PDIDate { get; set; }
        [IgnoreDataMember]
        public string ReleaseBy { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string WorkOrderNumber { get; set; }
        public string PilotingPDI { get; set; }
    }
}

