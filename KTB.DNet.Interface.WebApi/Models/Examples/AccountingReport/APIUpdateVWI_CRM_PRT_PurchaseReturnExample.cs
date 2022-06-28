#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 27/01/2020 1:23:06]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateVWI_CRM_PRT_PurchaseReturnExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,company = "company Value"
				,businessunitcode = "businessunitcode Value"
				,PurchaseOrderNo = "PurchaseOrderNo Value"
				,PurchaseReceiptNo = "PurchaseReceiptNo Value"
				,TransactionDate = "TransactionDate Value"
				,PurchaseReceiptType = "PurchaseReceiptType Value"
				,PRPOType = "PRPOType Value"
				,PurchaseReceiptStatus = "PurchaseReceiptStatus Value"
				,Vendor = "Vendor Value"
				,Product = "Product Value"
				,ProductDescription = "ProductDescription Value"
				,ReceivedQuantity = "ReceivedQuantity Value"
				,PurchaseUnit = "PurchaseUnit Value"
				,UnitCost = "UnitCost Value"
				,DiscountPercentage = "DiscountPercentage Value"
				,DiscountAmount = "DiscountAmount Value"
				,ConsumptionTax1 = "ConsumptionTax1 Value"
				,ConsumptionTax1Amount = "ConsumptionTax1Amount Value"
				,ConsumptionTax2 = "ConsumptionTax2 Value"
				,ConsumptionTax2Amount = "ConsumptionTax2Amount Value"
				,TotalConsumptionTaxAmount = "TotalConsumptionTaxAmount Value"
				,TotalBaseAmount = "TotalBaseAmount Value"
				,TransactionAmount = "TransactionAmount Value"
				,Site = "Site Value"
				,Warehouse = "Warehouse Value"
				,msdyn_companycode = "msdyn_companycode Value"
            };

            return obj;
        }
    }
}