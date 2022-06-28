#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 21/01/2020 14:18:44]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateVWI_CRM_SLS_StockSummaryExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,company = "company Value"
				,businessunitcode = "businessunitcode Value"
				,xts_stocknumber = "xts_stocknumber Value"
				,xts_product = "xts_product Value"
				,xts_description = "xts_description Value"
				,xts_productinteriorcolor = "xts_productinteriorcolor Value"
				,xts_productexteriorcolor = "xts_productexteriorcolor Value"
				,xts_scheduledshipmentdate = "xts_scheduledshipmentdate Value"
				,xts_referencenumber = "xts_referencenumber Value"
				,xts_receivingdate = "xts_receivingdate Value"
				,msdyn_companycode = "msdyn_companycode Value"
            };

            return obj;
        }
    }
}