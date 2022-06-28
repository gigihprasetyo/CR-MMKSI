#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPODto  class
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
using System.Collections.Generic;

namespace KTB.DNet.Interface.Model
{
    public class SparePartPODto : DtoBase
    {
        public int ID { get; set; }
        public string PONumber { get; set; }
        public string OrderType { get; set; }
        public int DealerID { get; set; }
        public DateTime PODate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ProcessCode { get; set; }
        public string CancelRequestBy { get; set; }
        public byte IndentTransfer { get; set; }
        public string PickingTicket { get; set; }
        public DateTime SentPODate { get; set; }
        public Boolean IsTransfer { get; set; }
        public string DMSPRNo { get; set; }
        public DealerDto Dealer { get; set; }
    }

    /// <summary>
    /// SparePartPOResponse Class
    /// </summary>
    public class SparePartPOResponse
    {
        public string PONumber { get; set; }
        public DateTime PODate { get; set; }
    }

    public class SparePartPOOtherDto
    {
        public string DealerCode { get; set; }
        public string DMSPRNo { get; set; }
        public DateTime PODate { get; set; }
        public string OrderType { get; set; }
        public string PickingTicket { get; set; }
        public string PONumber { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public List<SparePartPOOtherDetailDto> Details { get; set; }
    }

}

