#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PRHistorySODto  class
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
    public class VWI_PRHistorySODto : ReadDtoBase
    {
        public string PONumber { get; set; }
        public short DealerID { get; set; }
        public string DealerCode { get; set; }
        public DateTime PODate { get; set; }
        public string DMSPRNo { get; set; }
        public char OrderType { get; set; }
        public DateTime TanggalPO { get; set; }
        public string NomorPenjualan { get; set; }
        public string DocumentType { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public int JumlahPesanan { get; set; }
        public int JumlahPemenuhan { get; set; }
        public decimal HargaEceran { get; set; }
        public int TotalBaseAmountDetail { get; set; }
        public string NomorPengganti { get; set; }
        public decimal Diskon { get; set; }
        public int TotalAmountDetail { get; set; }
        public int TotalBaseAmountHeader { get; set; }
        public int TotalConsumptionTaxAmount { get; set; }
        public int TotalAmountHeader { get; set; }
        public string Status { get; set; }
        public string StatusDesc { get; set; }
    }
}

