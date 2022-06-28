#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_employee class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 13:42:55
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
    public class APIDeleteCRM_xts_employeeExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                xts_employeeid = "00000000-0000-0000-0000-000000000000"
            };

            return obj;
        }
    }
}