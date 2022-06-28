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
    public class WarrantyActivationDto : DtoBase
    {
        [IgnoreDataMember]
        public int ID { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime PDIDate { get; set; }
        public string PDIDealerCode { get; set; }
        public DateTime PKTDate { get; set; }
        public string PKTDealerCode { get; set; }
        public short Status { get; set; }
        public DateTime WADate { get; set; }
        public string FileName { get; set; }
    }
}
