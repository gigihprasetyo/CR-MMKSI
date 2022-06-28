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
    public class APIUpdateVWI_CRM_PRT_IncomingTransactionExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,company = "company Value"
				,businessunitcode = "businessunitcode Value"
				,InventoryTransactionNo = "InventoryTransactionNo Value"
				,TransactionDate = "TransactionDate Value"
				,TransactionType = "TransactionType Value"
				,ReasonCode = "ReasonCode Value"
				,Product = "Product Value"
				,ProductDescription = "ProductDescription Value"
				,TransactionUnit = "TransactionUnit Value"
				,Quantity = "Quantity Value"
				,UnitCost = "UnitCost Value"
				,TotalCost = "TotalCost Value"
				,msdyn_companycode = "msdyn_companycode Value"
            };

            return obj;
        }
    }
}