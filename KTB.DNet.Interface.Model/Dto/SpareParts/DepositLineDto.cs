#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DepositLineDto  class
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

namespace KTB.DNet.Interface.Model
{
    public class DepositLineDto : ReadDtoBase
    {
        public string DealerCode { get; set; }

        public string OrderNo { get; set; }

        public string ReferenceNo { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime PostingDate { get; set; }

        public Decimal Debit { get; set; }
    }
}

