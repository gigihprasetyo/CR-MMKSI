#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 1/6/2020 11:16:45 AM]
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
    public class APIDeleteVWI_CRM_ktb_perlengkapanstandardExample : IExamplesProvider
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