#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xjp_vehicletransfer class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 02 Sep 2020 12:03:21
 ===========================================================================
*/
#endregion


using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateCRM_xjp_vehicletransferExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				xjp_vehicletransferid = "xjp_vehicletransferid value",
				modifiedon = "modifiedon value",
				ownerid = "ownerid value",
				statecode = "statecode value",
				xjp_businessunitid = "xjp_businessunitid value",
				xjp_chassisnumber = "xjp_chassisnumber value",
				xjp_destinationaddress = "xjp_destinationaddress value",
				xjp_destinationlocationid = "xjp_destinationlocationid value",
				xjp_destinationsiteid = "xjp_destinationsiteid value",
				xjp_destinationwarehouseid = "xjp_destinationwarehouseid value",
				xjp_fromlocationid = "xjp_fromlocationid value",
				xjp_fromsiteid = "xjp_fromsiteid value",
				xjp_fromwarehouseid = "xjp_fromwarehouseid value",
				xjp_idempotentmessage = "xjp_idempotentmessage value",
				xjp_parentbusinessunitid = "xjp_parentbusinessunitid value",
				xjp_productexteriorcolorid = "xjp_productexteriorcolorid value",
				xjp_productid = "xjp_productid value",
				xjp_productinteriorcolorid = "xjp_productinteriorcolorid value",
				xjp_productstyleid = "xjp_productstyleid value",
				xjp_receiptdateandtime = "xjp_receiptdateandtime value",
				xjp_status = "xjp_status value",
				xjp_stockid = "xjp_stockid value",
				xjp_transactiontype = "xjp_transactiontype value",
				xjp_vehicletransfernumber = "xjp_vehicletransfernumber value",
				UpdatedBy = "UpdatedBy value"
            };

            return obj;
        }
    }
}