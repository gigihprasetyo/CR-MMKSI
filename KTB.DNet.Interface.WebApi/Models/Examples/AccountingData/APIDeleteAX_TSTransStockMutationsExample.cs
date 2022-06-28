#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 29/03/2022 9:17:19]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIDeleteAX_TSTransStockMutationsExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                ID = "123",
                UpdatedBy = "DealerUser"
            };

            return obj;
        }
    }
}