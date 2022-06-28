#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_warehouse class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 05 Feb 2021 13:59:12
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
    public class APIDeleteCRM_xts_warehouseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                xts_warehouseid = "00000000-0000-0000-0000-000000000000"
            };

            return obj;
        }
    }
}