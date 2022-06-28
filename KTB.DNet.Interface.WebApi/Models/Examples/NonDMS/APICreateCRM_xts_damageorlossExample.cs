#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_damageorloss class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 11:01:20
 ===========================================================================
*/
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateCRM_xts_damageorlossExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				ktb_parentbusinessunitid = "ktb_parentbusinessunitid value",
				ownerid = "ownerid value",
				xts_businessunitid = "xts_businessunitid value",
				xts_damageorloss = "xts_damageorloss value",
				xts_pkcombinationkey = "xts_pkcombinationkey value",
				xts_remarks = "xts_remarks value",
				xts_salespersonid = "xts_salespersonid value",
				xts_transactiondate = "xts_transactiondate value",
				xts_type = "xts_type value",
				xts_vehicleorderformnumberid = "xts_vehicleorderformnumberid value",
				UpdatedBy = "UpdatedBy value"
            };

            return obj;
        }
    }
}