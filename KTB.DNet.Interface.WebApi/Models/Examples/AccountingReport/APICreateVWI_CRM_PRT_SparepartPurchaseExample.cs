#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:03]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateVWI_CRM_PRT_SparepartPurchaseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,businessunitcode = "businessunitcode Value"
				,xts_purchaseordernumber = "xts_purchaseordernumber Value"
				,xts_accountpayablevouchernumber = "xts_accountpayablevouchernumber Value"
				,TransactionDate = "TransactionDate Value"
				,PurchaseReceiptDate = "PurchaseReceiptDate Value"
				,xts_packingslipnumber = "xts_packingslipnumber Value"
				,xts_vendor = "xts_vendor Value"
				,xts_product = "xts_product Value"
				,xts_description = "xts_description Value"
				,ktb_modelcode = "ktb_modelcode Value"
				,xts_uom = "xts_uom Value"
				,xts_receivedquantity = "xts_receivedquantity Value"
				,xts_unitcost = "xts_unitcost Value"
				,xts_discountamount = "xts_discountamount Value"
				,xts_totalconsumptiontaxamount = "xts_totalconsumptiontaxamount Value"
				,xts_totalamount = "xts_totalamount Value"
            };

            return obj;
        }
    }
}