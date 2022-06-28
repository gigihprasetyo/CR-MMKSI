#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartForecastDto  class
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
using System.Runtime.Serialization;

namespace KTB.DNet.Interface.Model
{
    public class SparePartForecastDto : DtoBase
    {
        public int ID { get; set; }
        public DateTime PoDate { get; set; }
        public string PoNumber { get; set; }
    }

    /// <summary>
    /// SparePartForecastStockManagementDto Class
    /// </summary>
    public class SparePartForecastStockManagementDto
    {
        public string PartNumber { get; set; }
        public int Stock { get; set; }
        public string NoBulletinService { get; set; }
        public string NoRecallCategory { get; set; }
        public int MaxOrder { get; set; }
        public int Status { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class SparePartForecastRejectDto
    {
        public string DealerCode { get; set; }
        public string PartNumber { get; set; }
        public int isReject { get; set; }
        public string PoNumber { get; set; }
        public string DMSPRNo { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }

    public class SparePartForecastPOEstimateDto
    {
        [IgnoreDataMember]
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public string DMSPRNo { get; set; }
        public string PONumber { get; set; }
        public string SONumber { get; set; }
        public DateTime? SODate { get; set; }
        public string DocumentType { get; set; }
        public int TermOfPaymentValue { get; set; }
        public string TermOfPaymentCode { get; set; }
        public string TermOfPaymentDesc { get; set; }
        public int AmountC2 { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public List<SparePartForecastPOEstimateDetailDto> Details { get; set; }
    }

    public class SparePartForecastPOEstimateDetailDto
    {
        public string PartNumber { get; set; }
        public int AllocQty { get; set; }
        public int OrderQty { get; set; }
        public int RetailPrice { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
    }

    public class SparePartForecastGoodReceiptDto
    {
        [IgnoreDataMember]
        public int ID { get; set; }
        public string DealerCode { get; set; }
        public DateTime DODate { get; set; }
        public DateTime DueDate { get; set; }
        public string DONumber { get; set; }
        public DateTime BillingDate { get; set; }
        public string BillingNumber { get; set; }
        public string ExpeditionNumber { get; set; }
        public int TermOfPaymentValue { get; set; }
        public string TermOfPaymentCode { get; set; }
        public string TermOfPaymentDesc { get; set; }
        public int AmountC2 { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public List<SparePartForecastGoodReceiptDetailDto> Details { get; set; }
    }

    public class SparePartForecastGoodReceiptDetailDto
    {
        public string SONumber { get; set; }
        public int Discount { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
        public int Tax { get; set; }
        public int RetailPrice { get; set; }
    }
    public class SparePartForecastValidatorDto
    {
        public int ID { get; set; }
    }

}



