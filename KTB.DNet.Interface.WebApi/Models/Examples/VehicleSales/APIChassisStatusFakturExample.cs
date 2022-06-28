#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APICreateChassisMasterPKTExample class
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

namespace KTB.DNet.Interface.WebApi.Models
{
    public class APIChassisStatusFakturExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                ChassisNumber = "MK2KRWFNUHJ000740"
            };

            return obj;
        }
    }
}