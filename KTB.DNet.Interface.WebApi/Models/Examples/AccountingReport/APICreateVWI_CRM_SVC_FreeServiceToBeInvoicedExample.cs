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
    public class APICreateVWI_CRM_SVC_FreeServiceToBeInvoicedExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,businessunitcode = "businessunitcode Value"
				,xts_workorder = "xts_workorder Value"
				,xts_workorderstatus = "xts_workorderstatus Value"
				,xts_servicecategory = "xts_servicecategory Value"
				,xts_product = "xts_product Value"
				,xts_platenumber = "xts_platenumber Value"
				,xts_ordertype = "xts_ordertype Value"
				,customername = "customername Value"
				,billtocustomername = "billtocustomername Value"
				,ktb_wodate = "ktb_wodate Value"
            };

            return obj;
        }
    }
}