#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APIUpdateSparePartPOExample class
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
using KTB.DNet.Interface.Model;
using System;
using System.Collections;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateSparePartPOExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new 
            {
                UpdatedBy = "DealerUser",
                PONumber = "EABA01621800002", 
                DMSPRNo = "Test DMS PR" 
            };
            return obj;
        }
    }
}