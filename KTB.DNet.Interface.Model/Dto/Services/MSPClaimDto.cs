#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MSPClaimDto  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 9/11/2018 9:53
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.Model
{
    public class MSPClaimDto : ReadDtoBase
    {
        public int ID { get; set; }
        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string PMKindCode { get; set; }
        public int StandKM { get; set; }
        public DateTime ServiceDate { get; set; }
        public string VisitType { get; set; }
        public string Remarks { get; set; }
    }
}

