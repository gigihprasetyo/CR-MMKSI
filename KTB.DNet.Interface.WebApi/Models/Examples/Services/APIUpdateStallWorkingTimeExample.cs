
#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateStallWorkingTimeExample class
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
using System.Collections.Generic;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateStallWorkingTimeExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser",
                ID = 12,
                DealerCode = "100714",
                Tanggal = "2021-02-16",
                TimeStart = "08:00:00",
                TimeEnd = "08:00:00",
                RestTimeStart = "08:00:00",
                RestTimeEnd = "08:00:00",
                StallCode = "100001-S06",
                IsHoliday = "1",
                VisitType = "0",
                Notes = "testing",
            };

            return obj;
        }
    }
}