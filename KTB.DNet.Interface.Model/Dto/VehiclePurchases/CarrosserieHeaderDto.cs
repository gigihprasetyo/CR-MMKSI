#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CarrosserieHeaderDto  class
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
    public class CarrosserieHeaderDto : DtoBase
    {
        public int ID { get; set; }
        public short PDIStateCode { get; set; }
        public short PDIStatusCode { get; set; }
        public string BUCode { get; set; }
        public string BUName { get; set; }
        public string PDIName { get; set; }
        public string PDIReceiptNo { get; set; }
        public string PDIReceiptRefName { get; set; }
        public short PDIReceiptStatus { get; set; }
        public DateTime TransactionDate { get; set; }
        public short TransactionType { get; set; }
        public string VendorName { get; set; }
        public string ChassisNumber { get; set; }


        public List<CarrosserieDetailDto> CarrosserieDetails { get; set; }
    }
}

