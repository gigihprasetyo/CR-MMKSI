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
    public class APIUpdateVWI_CRM_PRT_OutgoingInventoryTransferOutletExample : IExamplesProvider
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
				,WorkOrderNo = "WorkOrderNo Value"
				,Product = "Product Value"
				,ProductDescription = "ProductDescription Value"
				,Quantity = "Quantity Value"
				,FromWarehouse = "FromWarehouse Value"
				,ToWarehouse = "ToWarehouse Value"
				,ToBU = "ToBU Value"
				,msdyn_companycode = "msdyn_companycode Value"
            };

            return obj;
        }
    }
}