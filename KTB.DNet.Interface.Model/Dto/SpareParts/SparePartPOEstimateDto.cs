#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPOEstimateDto  class
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
    public class SparePartPOEstimateDto : ReadDtoBase
    {
        //public int ID { get; set; }
        //public int SparePartPOID { get; set; }
        public int DealerCode { get; set; }
        public string DMSPRNo { get; set; }
        public string PONumber { get; set; }
        public string SONumber { get; set; }
        public DateTime SODate { get; set; }
        //public DateTime DeliveryDate { get; set; }
        public string DocumentType { get; set; }
        public int TermOfPaymentValue { get; set; }
        public string TermOfPaymentCode { get; set; }
        public string TermOfPaymentDesc { get; set; }

        public Decimal AmountC2 { get; set; }
        public List<SparePartPOEstimateDetailDto> SparePartPOEstimateDetails { get; set; }
    }
}

