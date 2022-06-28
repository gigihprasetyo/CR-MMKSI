#region Summary
// ===========================================================================
// AUTHOR        : PT BSI 
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
    public class APICreateSparePartForecastExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var detObj = new
            {
                UpdatedBy = "DealerUser",
                PartNumber = "1592370100",
                RequestQty = 1
            };

            object[] detailList = new object[1];
            detailList[0] = detObj;

            var obj = new
            {
                UpdatedBy = "DealerUser",
                DealerCode = "000000",
                DMSPRNo = "DMSPRNo",
                PoDate = DateTime.Now,
                SparePartForecastDetails = detailList
            };
            return obj;
        }
    }
}