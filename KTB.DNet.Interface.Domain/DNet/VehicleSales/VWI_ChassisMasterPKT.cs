#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_ChassisMasterPKT  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-11 15:38:00
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_ChassisMasterPKT
    {
        public Int64 IDRow { get; set; }
        public Int64 ID { get; set; }

        public int ChassisMasterID { get; set; }

        public string ChassisNumber { get; set; }

        public string DealerCode { get; set; }

        public DateTime PKTDate { get; set; }

        public DateTime LastUpdateTime { get; set; }
        public int RowStatus { get; set; }
    }
}
