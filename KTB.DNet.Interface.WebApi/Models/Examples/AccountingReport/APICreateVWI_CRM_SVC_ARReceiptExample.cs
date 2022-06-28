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
    public class APICreateVWI_CRM_SVC_ARReceiptExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,businessunitcode = "businessunitcode Value"
				,xts_accountreceivableinvoice = "xts_accountreceivableinvoice Value"
				,xts_type = "xts_type Value"
				,xts_transactiondate = "xts_transactiondate Value"
				,xts_status = "xts_status Value"
				,xts_reversing = "xts_reversing Value"
				,xts_ordernumber = "xts_ordernumber Value"
				,xts_invoiceamount = "xts_invoiceamount Value"
				,xts_customernumber = "xts_customernumber Value"
				,xts_balance = "xts_balance Value"
				,address1_line1 = "address1_line1 Value"
				,xts_totalworkamount = "xts_totalworkamount Value"
				,xts_totalpartsamount = "xts_totalpartsamount Value"
				,xts_totalmiscchargeamount = "xts_totalmiscchargeamount Value"
				,xts_platenumber = "xts_platenumber Value"
            };

            return obj;
        }
    }
}