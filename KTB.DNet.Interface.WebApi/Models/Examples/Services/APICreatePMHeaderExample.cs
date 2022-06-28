#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreatePMHeaderExample class
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
    public class APICreatePMHeaderExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                DealerCode = "100109",
                DealerBranchCode = "300016",
                EngineNumber = "6G75TQ5310",
                ChassisNumber = "JMFLYV97W8J000873",
                StandKM = 3000,
                PMKindCode = "01",
                ServiceDate = DateTime.Now,
                WorkOrderNumber = "testWONum",
                BookingNo = "1234",
                VisitType = "WI",
                PMStatus = "0"
            };

            return obj;
        }
    }
}