#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateAssistPartStockExample class
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
    public class APIUpdateAssistPartStockExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 4,
                DealerCode = "0",
                DealerBranchCode = "300016",
                HargaBeli = 150000,
                JumlahDatang = 1,
                JumlahStokAwal = 0,
                Month = "1",
                NoParts = "MM900004",
                Year = "2000"
                //RemarksSystem = "",
                //StatusAktif = 0,
                //ValidateSystemStatus = 0,                
            };
            return obj;
        }
    }
}