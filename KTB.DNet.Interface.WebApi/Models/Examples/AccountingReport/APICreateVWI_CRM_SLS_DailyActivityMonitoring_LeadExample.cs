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
    public class APICreateVWI_CRM_SLS_DailyActivityMonitoring_LeadExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,businessunitcode = "businessunitcode Value"
				,subject = "subject Value"
				,fullname = "fullname Value"
				,statuscode = "statuscode Value"
				,xts_leaddate = "xts_leaddate Value"
				,xts_productsegment2 = "xts_productsegment2 Value"
				,xts_employee = "xts_employee Value"
				,ktb_superiors = "ktb_superiors Value"
            };

            return obj;
        }
    }
}