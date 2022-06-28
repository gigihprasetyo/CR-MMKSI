#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 13/01/2020 9:21:47]
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
    public class APIDeleteVWI_CRM_xts_accountpayablepaymentdetailExample : IExamplesProvider
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