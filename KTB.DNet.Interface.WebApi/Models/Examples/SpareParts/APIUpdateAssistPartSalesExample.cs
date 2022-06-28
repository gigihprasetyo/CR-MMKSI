#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateAssistPartSalesExample class
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
using System;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateAssistPartSalesExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 1,
                //AssistUploadLogID = 1,
                TglTransaksi = "20/04/18",
                DealerCode = "0",
                KodeCustomer = "30000006",
                SalesChannelCode = "I",
                TrTraineeSalesSparepartID = 0,
                KodeSalesman = "S-100003",
                NoWorkOrder = "string",
                NoParts = "MB990979",
                Qty = 1,
                HargaBeli = 110,
                HargaJual = 120,
                IsCampaign = true,
                CampaignNo = "string",
                CampaignDescription = "test Update :" + DateTime.Now,
                DealerBranchCode = "300016"
            };

            return obj;
        }
    }
}