#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 23/01/2020 9:18:41]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateVWI_CRM_PRT_SparepartSalesToPartshopExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,company = "company Value"
				,businessunitcode = "businessunitcode Value"
				,Site = "Site Value"
				,SalesOrderNo = "SalesOrderNo Value"
				,DeliveryOrderNo = "DeliveryOrderNo Value"
				,TransactionDate = "TransactionDate Value"
				,CustomerNo = "CustomerNo Value"
				,Customer = "Customer Value"
				,Product = "Product Value"
				,ProductDescription = "ProductDescription Value"
				,SalesUnit = "SalesUnit Value"
				,TermOfPayment = "TermOfPayment Value"
				,QuantityDelivered = "QuantityDelivered Value"
				,QuantityReturned = "QuantityReturned Value"
				,UnitPrice = "UnitPrice Value"
				,DiscountPercentage = "DiscountPercentage Value"
				,DiscountAmount = "DiscountAmount Value"
				,TotalConsumptionTaxAmount = "TotalConsumptionTaxAmount Value"
				,ConsumptionTax1 = "ConsumptionTax1 Value"
				,ConsumptionTax1Amount = "ConsumptionTax1Amount Value"
				,TotalAmount = "TotalAmount Value"
				,COGSTrx = "COGSTrx Value"
				,msdyn_companycode = "msdyn_companycode Value"
            };

            return obj;
        }
    }
}