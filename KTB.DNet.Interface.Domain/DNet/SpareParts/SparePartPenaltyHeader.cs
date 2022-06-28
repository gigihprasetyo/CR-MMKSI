
#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : SPPenaltyHeaderDomain class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:16:28
//
// ===========================================================================	
#endregion

using System;
using System.Collections.Generic;
namespace KTB.DNet.Interface.Domain
{
    public class SparePartPenaltyHeader
    {
        public string IDRow { get; set; }
        public string ID { get; set; }
        public string DealerCode { get; set; }
        public DateTime DebitMemoDate { get; set; }
        public string DebitMemoNumber { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public List<SparePartPenaltyDetail> sparepartpenaltydetail { get; set; }
    }
}
