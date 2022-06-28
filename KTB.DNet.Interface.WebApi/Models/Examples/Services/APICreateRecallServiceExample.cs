#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateRecallServiceExample class
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
    public class APICreateRecallServiceExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ChassisNumber = "MMBGUKS10GH012477",
                RecallRegNo = "RC170000010",
                DealerCode = "100170",
                DealerBranchCode = "200005",
                ServiceDate = "10032018",
                MileAge = 1001,
                WorkOrderNumber = "TEST123456"
            };

            return obj;
        }
    }
}