#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 28/01/2020 16:30:06]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateVWI_CRM_PRT_IncomingInventoryTransferOutletExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,company = "company Value"
				,businessunitcode = "businessunitcode Value"
				,InventoryTransactionNo = "InventoryTransactionNo Value"
				,InventoryTransferNo = "InventoryTransferNo Value"
				,FromBU = "FromBU Value"
				,TransactionDate = "TransactionDate Value"
				,Product = "Product Value"
				,ProductDescription = "ProductDescription Value"
				,Site = "Site Value"
				,Warehouse = "Warehouse Value"
				,Location = "Location Value"
				,Quantity = "Quantity Value"
				,TransactionUnit = "TransactionUnit Value"
				,UnitCost = "UnitCost Value"
				,TotalCost = "TotalCost Value"
				,msdyn_companycode = "msdyn_companycode Value"
            };

            return obj;
        }
    }
}