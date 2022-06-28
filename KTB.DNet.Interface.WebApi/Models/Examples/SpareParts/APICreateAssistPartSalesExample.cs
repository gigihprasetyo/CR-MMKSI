#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateAssistPartSalesExample class
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
    public class APICreateAssistPartSalesExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                TglTransaksi = "20/04/18",
                DealerCode = "0",
                KodeCustomer = "30000006",
                SalesChannelCode = "I",
                TrTraineeSalesSparepartID = 0,
                KodeSalesman = "S-100003",
                NoWorkOrder = "108098",
                NoParts = "MB990979",
                Qty = 1,
                HargaBeli = 110,
                HargaJual = 120,
                IsCampaign = true,
                CampaignNo = "2120",
                CampaignDescription = "Sale",
                DealerBranchCode = "300016"
            };

            return obj;
        }
    }
}