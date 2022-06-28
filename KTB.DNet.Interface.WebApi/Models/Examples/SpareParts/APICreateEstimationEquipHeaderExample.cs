#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateEstimationEquipHeaderExample class
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
    public class APICreateEstimationEquipHeaderExample : IExamplesProvider
    {
        public object GetExamples()
        {

            var details = new
            {
                UpdatedBy = "DealerUser",
                //EstimationEquipHeaderID = 0,
                PartNumber = "MB990979",
                //Discount = 0.1,
                TotalForecast = 1,
                EstimationUnit = 1,
                ConfirmedDate = "2018-01-05 17:33:14.290",
                // Remark = "Remark Test"

            };

            object[] detailList = new object[1];
            detailList[0] = details;

            var header = new
            {
                UpdatedBy = "DealerUser",
                //ID = 0,
                DealerCode = "0",
                DMSPRNo = "DMSPRNo Test",
                EstimationEquipDetails = detailList
            };

            return header;
        }
    }
}