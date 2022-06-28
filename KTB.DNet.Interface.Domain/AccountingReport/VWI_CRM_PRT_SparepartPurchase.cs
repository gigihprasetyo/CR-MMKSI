#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_PRT_SparepartPurchase  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:04
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.Domain
{
    public class VWI_CRM_PRT_SparepartPurchase
    {
        public Int64 IDRow { get; set; }

		public string businessunitcode { get; set; }

		public string xts_purchaseordernumber { get; set; }

		public string xts_accountpayablevouchernumber { get; set; }

		public DateTime TransactionDate { get; set; }

		public DateTime PurchaseReceiptDate { get; set; }

		public string xts_packingslipnumber { get; set; }

		public string xts_vendor { get; set; }

		public string xts_product { get; set; }

		public string xts_description { get; set; }

		public string ktb_modelcode { get; set; }

		public string xts_uom { get; set; }

		public decimal xts_receivedquantity { get; set; }

		public decimal xts_unitcost { get; set; }

		public decimal xts_discountamount { get; set; }

		public decimal xts_totalconsumptiontaxamount { get; set; }

		public decimal xts_totalamount { get; set; }
    }
}
