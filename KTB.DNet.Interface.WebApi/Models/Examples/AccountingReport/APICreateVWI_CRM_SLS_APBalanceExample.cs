#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 23/01/2020 14:05:28]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateVWI_CRM_SLS_APBalanceExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,company = "company Value"
				,businessunitcode = "businessunitcode Value"
				,IdentificationType = "IdentificationType Value"
				,APVoucherNo = "APVoucherNo Value"
				,TransactionDate = "TransactionDate Value"
				,Vendor = "Vendor Value"
				,TransactionType = "TransactionType Value"
				,DueDate = "DueDate Value"
				,VendorInvoiceNumber = "VendorInvoiceNumber Value"
				,APVoucherDetailNo = "APVoucherDetailNo Value"
				,PaymentTerms = "PaymentTerms Value"
				,GrandTotal = "GrandTotal Value"
				,Balance = "Balance Value"
				,PaymentAmount = "PaymentAmount Value"
				,ConsumptionTax1Amount = "ConsumptionTax1Amount Value"
				,ConsumptionTax2Amount = "ConsumptionTax2Amount Value"
				,Description = "Description Value"
				,DocState = "DocState Value"
				,msdyn_companycode = "msdyn_companycode Value"
            };

            return obj;
        }
    }
}