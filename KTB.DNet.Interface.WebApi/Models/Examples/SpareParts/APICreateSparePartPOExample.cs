#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateSparePartPOExample class
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
    public class APICreateSparePartPOExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var detObj = new
            {
                UpdatedBy = "DealerUser",
                PartNumber = "MY021188",
                Quantity = 1,
                TotalForecast = 1
            };

            object[] detailList = new object[1];
            detailList[0] = detObj;

            var obj = new
            {
                UpdatedBy = "DealerUser",
                DealerCode = "0",
                DeliveryDate = DateTime.Now,
                DMSPRNo = "123",
                OrderType = "E",
                PODate = DateTime.Now,
                PQRNo = "100172/08/CV/1",
                SparePartPODetails = detailList
            };
            return obj;
        }
    }
}