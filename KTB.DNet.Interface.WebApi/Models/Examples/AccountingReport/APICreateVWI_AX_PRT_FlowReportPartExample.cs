#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 12/02/2020 9:04:16]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateVWI_AX_PRT_FlowReportPartExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				UpdatedBy = "DealerUser"
				,Company = "Company Value"
				,Product = "Product Value"
				,ProductDescription = "ProductDescription Value"
				,Site = "Site Value"
				,Warehouse = "Warehouse Value"
				,Period = "Period Value"
				,StockUOM = "StockUOM Value"
				,TransactionDateFrom = "TransactionDateFrom Value"
				,TransactionDateTo = "TransactionDateTo Value"
				,BeginningQty = "BeginningQty Value"
				,BeginningAmount = "BeginningAmount Value"
				,QtyIn = "QtyIn Value"
				,AmountIn = "AmountIn Value"
				,QtyOut = "QtyOut Value"
				,AmountOut = "AmountOut Value"
				,QtyEnding = "QtyEnding Value"
				,AmountEnding = "AmountEnding Value"
            };

            return obj;
        }
    }
}