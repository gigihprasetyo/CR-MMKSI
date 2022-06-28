#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : SPPenaltyDetailDomain class
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
    public class SparePartPenaltyDetail
    {
        public string IDRow { get; set; }
        public string ID { get; set; }
        public string TOPSPPenaltyID { get; set; }
        public decimal AmountPenalty { get; set; }
        public string BillingNumber { get; set; }
        public string DoNumber { get; set; }
    }
}

