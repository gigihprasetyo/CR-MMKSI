#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdatePDIExample class
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
    public class APIUpdatePDIExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 1288987,
                ChassisNumber = "MK2L0PU39HK013747",
                DealerCode = "100714",
                Kind = "A",
                PDIStatus = "3",
                PDIDate = "13032018",
                ReleaseBy = "000539ads_mila",
                ReleaseDate = "13032018",
                DealerBranchCode = "300033",
                WorkOrderNumber = "TEST123456"
            };

            return obj;
        }
    }
}