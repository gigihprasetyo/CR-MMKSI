#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_writeoffbalance class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Khalil (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 13:18:19
 ===========================================================================
*/
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APICreateCRM_xts_writeoffbalanceExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				modifiedon = "modifiedon value",
				ownerid = "ownerid value",
				transactioncurrencyid = "transactioncurrencyid value",
				xts_businessunitid = "xts_businessunitid value",
				xts_codetrigger = "xts_codetrigger value",
				xts_customerid = "xts_customerid value",
				xts_customernumber = "xts_customernumber value",
				xts_parentbusinessunitid = "xts_parentbusinessunitid value",
				xts_status = "xts_status value",
				xts_transactiondate = "xts_transactiondate value",
				xts_writeoffbalancenumber = "xts_writeoffbalancenumber value",
				xts_writeofflimit = "xts_writeofflimit value",
				xts_writeofftype = "xts_writeofftype value",
				UpdatedBy = "UpdatedBy value"
            };

            return obj;
        }
    }
}