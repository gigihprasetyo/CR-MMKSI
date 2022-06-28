#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_assigntosalesperson class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 11:46:13
 ===========================================================================
*/
#endregion


using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateCRM_xts_assigntosalespersonExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				xts_assigntosalespersonid = "xts_assigntosalespersonid value",
				ktb_bypasslimit = "ktb_bypasslimit value",
				ktb_parentbusinessunitid = "ktb_parentbusinessunitid value",
				ownerid = "ownerid value",
				statecode = "statecode value",
				xts_assigntosalesperson = "xts_assigntosalesperson value",
				xts_assigntype = "xts_assigntype value",
				xts_businessunitid = "xts_businessunitid value",
				xts_fromnoid = "xts_fromnoid value",
				xts_fromsalespersonid = "xts_fromsalespersonid value",
				xts_pkcombinationkey = "xts_pkcombinationkey value",
				xts_tonoid = "xts_tonoid value",
				xts_tosalespersonid = "xts_tosalespersonid value",
				xts_transactiondate = "xts_transactiondate value",
				UpdatedBy = "UpdatedBy value"
            };

            return obj;
        }
    }
}