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
    public class APICreateVWI_CRM_SLS_DailyActivityMonitoring_SPKExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,businessunitcode = "businessunitcode Value"
				,xts_newvehiclesalesordernumber = "xts_newvehiclesalesordernumber Value"
				,xts_status = "xts_status Value"
				,xts_productsegment1 = "xts_productsegment1 Value"
				,xts_productsegment2 = "xts_productsegment2 Value"
				,statecode = "statecode Value"
            };

            return obj;
        }
    }
}