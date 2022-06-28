#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_lead class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 14:17:25
 ===========================================================================
*/
#endregion

using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIDeleteCRM_leadExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                leadid = "00000000-0000-0000-0000-000000000000"
            };

            return obj;
        }
    }
}