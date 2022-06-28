#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateAssistPartStockExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateAssistPartStockExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                DealerCode = "100001",
                DealerBranchCode = "200078",
                HargaBeli = 150000,
                JumlahDatang = 1,
                JumlahStokAwal = 0,
                Month = "1",
                NoParts = "MM900005",
                Year = "2000"
            };

            return obj;
        }
    }
}