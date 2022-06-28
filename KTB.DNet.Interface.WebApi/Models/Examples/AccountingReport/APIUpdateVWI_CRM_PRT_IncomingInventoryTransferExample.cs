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
    public class APIUpdateVWI_CRM_PRT_IncomingInventoryTransferExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,businessunitcode = "businessunitcode Value"
				,xts_inventorytransactionnumber = "xts_inventorytransactionnumber Value"
				,xts_product = "xts_product Value"
				,xts_productdescription = "xts_productdescription Value"
				,xts_quantity = "xts_quantity Value"
				,transactionunitname = "transactionunitname Value"
				,xts_transactiondate = "xts_transactiondate Value"
				,warehousename = "warehousename Value"
				,sitename = "sitename Value"
				,frombusinessunitcode = "frombusinessunitcode Value"
            };

            return obj;
        }
    }
}