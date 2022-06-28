#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPPenalty Detail  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using System;
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SparePartPenaltyDetailDto : DtoBase
    {
        public string ID { get; set; }
        public decimal AmountPenalty { get; set; }
        public string BillingNumber { get; set; }
        public string DoNumber { get; set; }

    }
}


