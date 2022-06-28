#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 27/01/2020 17:07:57]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateVWI_CRM_PRT_IncomingInventoryTransferWarehouseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,company = "company Value"
				,businessunitcode = "businessunitcode Value"
				,InventoryTransferNo = "InventoryTransferNo Value"
				,TransactionDate = "TransactionDate Value"
				,TransactionType = "TransactionType Value"
				,WorkOrderNo = "WorkOrderNo Value"
				,FromSite = "FromSite Value"
				,FromWarehouse = "FromWarehouse Value"
				,ToWarehouse = "ToWarehouse Value"
				,Product = "Product Value"
				,ProductDescription = "ProductDescription Value"
				,Quantity = "Quantity Value"
				,Unit = "Unit Value"
				,COGSTrx = "COGSTrx Value"
				,msdyn_companycode = "msdyn_companycode Value"
				,xts_inventorytransferdetailid = "xts_inventorytransferdetailid Value"
            };

            return obj;
        }
    }
}