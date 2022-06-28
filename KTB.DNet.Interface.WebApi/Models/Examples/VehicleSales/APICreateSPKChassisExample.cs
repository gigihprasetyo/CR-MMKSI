#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateSPKChassisExample class
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

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APICreateSPKChassisExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                SPKDetailID = 1,
                MatchingType = 1,
                MatchingDate = "2018-03-15T11:43:55.402Z",
                MatchingNumber = "AXX",
                ReferenceNumber = "TEST",
                KeyNumber = "KEYNUMBER",
                ChassisNumber = "MHMFE74P5EK122192",
                SPKNumber = "1104000003"
            };

            return obj;
        }
    }
}