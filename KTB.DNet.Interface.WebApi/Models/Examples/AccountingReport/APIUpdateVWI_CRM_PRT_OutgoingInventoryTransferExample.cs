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
    public class APIUpdateVWI_CRM_PRT_OutgoingInventoryTransferExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,businessunitcode = "businessunitcode Value"
				,xts_inventorytransfernumber = "xts_inventorytransfernumber Value"
				,xts_product = "xts_product Value"
				,xts_productdescription = "xts_productdescription Value"
				,xts_quantity = "xts_quantity Value"
				,ktb_cogstrx = "ktb_cogstrx Value"
				,transactiontypename = "transactiontypename Value"
				,xts_transactiondate = "xts_transactiondate Value"
				,fromwarehousename = "fromwarehousename Value"
				,towarehousename = "towarehousename Value"
				,fromsitename = "fromsitename Value"
				,tositename = "tositename Value"
				,tobusinessunitcode = "tobusinessunitcode Value"
            };

            return obj;
        }
    }
}