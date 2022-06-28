#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APPaymentDetailDto  class
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
    public class APPaymentDetailDto : DtoBase
    {
        public int ID { get; set; }
        public int APPaymentID { get; set; }
        public string Owner { get; set; }
        public string APPaymentDetailNo { get; set; }
        public string APPaymentNo { get; set; }
        public string BU { get; set; }
        public Decimal ChangeAmount { get; set; }
        public string Description { get; set; }
        public Double DifferenceValue { get; set; }
        public string ExternalDocumentNo { get; set; }
        public int ExternalDocumentType { get; set; }
        public string APVoucherNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNoNVSOReferral { get; set; }
        public string OrderNoOutsourceWorkOrder { get; set; }
        public string OrderNo { get; set; }
        public string OrderNoUVSOReferral { get; set; }
        public Decimal OutstandingBalance { get; set; }
        public Decimal PaymentAmount { get; set; }
        public string PaymentSlipNo { get; set; }
        public Boolean ReceiptFromVendor { get; set; }
        public Decimal RemainingBalance { get; set; }
        public int SourceType { get; set; }
        public string TransactionDocument { get; set; }
        public string Vendor { get; set; }
    }
}

