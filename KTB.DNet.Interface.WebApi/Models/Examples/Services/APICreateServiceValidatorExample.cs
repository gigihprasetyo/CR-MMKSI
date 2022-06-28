#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateServiceValidatorExample class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 30/10/2018 7:31
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateServiceValidatorExample : IExamplesProvider
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
                PMKindCode = "03",
                StandKM = 3000,
                BookingNo = "1234",
                PMStatus = "0",
                Kind = "A",
                PDIStatus = "3",
                PDIDate = "13032018",
                ReleaseBy = "000539ads_mila",
                ReleaseDate = "13032018",
                ServiceType = "FS"
            };

            return obj;
        }
    }
}