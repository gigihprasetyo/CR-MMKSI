#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PRHistoryDODto  class
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
    public class VWI_PRHistoryDODto : DtoBase
    {
        public int DealerID { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string PONumber { get; set; }
        public DateTime PODate { get; set; }
        public string DMSPRNo { get; set; }
        public char OrderType { get; set; }
        public string SONumber { get; set; }
        public string NomorDO { get; set; }
        public DateTime TanggalDO { get; set; }
        public int SparePartMasterID { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
        public DateTime EstimasiTanggalPengiriman { get; set; }
        public DateTime PickingDate { get; set; }
        public DateTime PackingDate { get; set; }
        public DateTime GoodIssueDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime ReadyForDeliveryDate { get; set; }
        public string ExpeditionNo { get; set; }
        public string ExpeditionName { get; set; }
        public DateTime ATD { get; set; }
        public DateTime ETA { get; set; }

    }
}

