
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPPenalty Header  class
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
using System.Collections.Generic;
using System.Runtime.Serialization;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class SparePartPenaltyHeaderDto : DtoBase
    {
        public string ID { get; set; }
        public string DealerCode { get; set; }
        public DateTime DebitMemoDate { get; set; }
        public string DebitMemoNumber { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public List<SparePartPenaltyDetailDto> sparepartpenaltydetail { get; set; }

    }
}


