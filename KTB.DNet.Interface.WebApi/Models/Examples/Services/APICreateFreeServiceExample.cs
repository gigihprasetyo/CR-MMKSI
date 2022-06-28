#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateFreeServiceExample class
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
    public class APICreateFreeServiceExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ChassisNumber = "MK2L0PU39HK013747",
                DealerCode = "100714",
                DealerBranchCode = "300033",
                FSKindCode = "1",
                ServiceDate = "10032018",
                SoldDate = "10122000",
                MileAge = 1001,
                WorkOrderNumber = "TEST123456",
                VisitType = "WI",
                EngineNumber = "4D34-432469",
                isBB = false,
                FileName = "fs.pdf",
                Base64ofStream = "xxxxx"
            };

            return obj;
        }
    }
}