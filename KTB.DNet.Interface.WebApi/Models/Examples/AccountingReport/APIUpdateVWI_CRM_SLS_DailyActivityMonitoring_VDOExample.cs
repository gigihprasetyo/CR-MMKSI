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
    public class APIUpdateVWI_CRM_SLS_DailyActivityMonitoring_VDOExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,businessunitcode = "businessunitcode Value"
				,xts_newvehicledeliveryordernumber = "xts_newvehicledeliveryordernumber Value"
				,xts_newvehiclesalesordernumber = "xts_newvehiclesalesordernumber Value"
				,personincharge = "personincharge Value"
				,salesperson = "salesperson Value"
				,xts_productsegment2id = "xts_productsegment2id Value"
            };

            return obj;
        }
    }
}