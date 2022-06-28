#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xid_registrationmonitoring class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Khalil
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 17:42:37
 ===========================================================================
*/
#endregion


using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateCRM_xid_registrationmonitoringExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
				xid_registrationmonitoringid = "xid_registrationmonitoringid value",
				ownerid = "ownerid value",
				xid_actualdate = "xid_actualdate value",
				xid_businessunitid = "xid_businessunitid value",
				xid_followup = "xid_followup value",
				xid_gap = "xid_gap value",
				xid_idealdate = "xid_idealdate value",
				xid_leadtime = "xid_leadtime value",
				xid_newvehiclesalesorderid = "xid_newvehiclesalesorderid value",
				xid_parentbusinessunitid = "xid_parentbusinessunitid value",
				xid_progressstageid = "xid_progressstageid value",
				xid_registrationmonitoringnumber = "xid_registrationmonitoringnumber value",
				xid_remarks = "xid_remarks value",
				xid_stageordernumber = "xid_stageordernumber value",
				xid_transactiontype = "xid_transactiontype value",
				UpdatedBy = "UpdatedBy value"
            };

            return obj;
        }
    }
}