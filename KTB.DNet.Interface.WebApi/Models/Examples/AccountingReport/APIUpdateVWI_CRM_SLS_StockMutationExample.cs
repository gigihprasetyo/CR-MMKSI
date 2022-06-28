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
    public class APIUpdateVWI_CRM_SLS_StockMutationExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,businessunitcode = "businessunitcode Value"
				,xts_transferhistorynumber = "xts_transferhistorynumber Value"
				,transfersourcesitename = "transfersourcesitename Value"
				,transferdestinationsitename = "transferdestinationsitename Value"
				,xts_transferdate = "xts_transferdate Value"
            };

            return obj;
        }
    }
}