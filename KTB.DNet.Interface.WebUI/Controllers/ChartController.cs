#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Chart controller class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebUI.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class ChartController : BaseController
    {
        #region Initialize
        private ITransactionLogRepository<TransactionLog, long> _transactionLogRepo;
        #endregion

        #region Constructor
        public ChartController(
            ITransactionLogRepository<TransactionLog, long> transactionLogRepo)
        {
            _transactionLogRepo = transactionLogRepo;
        }
        #endregion

        #region Method Get Chart Data
        /// <summary>
        /// Get Chart Data
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Chart_Read)]
        [HttpGet]
        public IHttpActionResult ChartData()
        {
            try
            {
                List<object> iData = new List<object>();

                // Creating sample data  
                DataTable dt = new DataTable();
                dt.Columns.Add("DealerCode", System.Type.GetType("System.String"));
                dt.Columns.Add("SucceedCount", System.Type.GetType("System.Int32"));
                dt.Columns.Add("FailedCount", System.Type.GetType("System.Int32"));
                dt.Columns.Add("TotalCount", System.Type.GetType("System.Int32"));

                // get data from DB
                var list = new List<string>();//_transactionLogRepo.GetTopDealerTransactionList();

                foreach (var item in list)
                {
                    string[] values = item.Split('|');
                    DataRow dr = dt.NewRow();
                    dr["DealerCode"] = values[0];
                    dr["SucceedCount"] = int.Parse(values[1]);
                    dr["FailedCount"] = int.Parse(values[2]);
                    dr["TotalCount"] = int.Parse(values[3]);
                    dt.Rows.Add(dr);
                }

                // Looping and extracting each DataColumn to List<Object>  
                foreach (DataColumn dc in dt.Columns)
                {
                    List<object> x = new List<object>();
                    x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                    iData.Add(x);
                }

                // Source data returned as JSON
                return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = iData });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Data = GetInnerException(ex).Message });
            }
        }
        #endregion
    }
}