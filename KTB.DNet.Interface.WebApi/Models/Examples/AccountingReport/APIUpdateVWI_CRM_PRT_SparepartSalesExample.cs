#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 29/01/2020 10:52:17]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateVWI_CRM_PRT_SparepartSalesExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,company = "company Value"
				,businessunitcode = "businessunitcode Value"
				,TransactionDate = "TransactionDate Value"
				,Customer = "Customer Value"
				,CustomerDescription = "CustomerDescription Value"
				,CustomerType = "CustomerType Value"
				,SalesPerson = "SalesPerson Value"
				,SOorWoNo = "SOorWoNo Value"
				,InvoiceNo = "InvoiceNo Value"
				,ProductCode = "ProductCode Value"
				,ProductDescription = "ProductDescription Value"
				,Model = "Model Value"
				,Quantity = "Quantity Value"
				,CapitalPrice = "CapitalPrice Value"
				,RetailPrice = "RetailPrice Value"
				,DiscountAmount = "DiscountAmount Value"
				,Total = "Total Value"
				,Tax = "Tax Value"
				,TotalAmount = "TotalAmount Value"
				,TotalCOGS = "TotalCOGS Value"
				,Laba = "Laba Value"
				,msdyn_companycode = "msdyn_companycode Value"
            };

            return obj;
        }
    }
}