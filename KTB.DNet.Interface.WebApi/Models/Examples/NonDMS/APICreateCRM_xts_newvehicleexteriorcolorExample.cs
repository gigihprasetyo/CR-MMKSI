#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_newvehicleexteriorcolor class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Khalil (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 04 Sep 2020 14:23:04
 ===========================================================================
*/
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateCRM_xts_newvehicleexteriorcolorExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				modifiedon = "modifiedon value",
				statecode = "statecode value",
				xts_description = "xts_description value",
				xts_entitytag = "xts_entitytag value",
				xts_newvehicleexteriorcolor = "xts_newvehicleexteriorcolor value",
				UpdatedBy = "UpdatedBy value"
            };

            return obj;
        }
    }
}