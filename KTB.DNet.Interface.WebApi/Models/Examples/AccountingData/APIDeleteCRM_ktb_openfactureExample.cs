#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 17/02/2021 11:49:03]
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
    public class APIDeleteCRM_ktb_openfactureExample : IExamplesProvider
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