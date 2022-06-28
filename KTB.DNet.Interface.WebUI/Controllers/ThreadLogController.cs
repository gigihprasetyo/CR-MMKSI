#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ThreadLog controller class
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
using KTB.DNet.Interface.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class ThreadLogController : BaseController
    {
        #region Initialize
        private ITransactionRuntimeRepository<TransactionRuntime, long> _threadLogRepo;
        private ITransactionLogRepository<TransactionLog, long> _transactionLogRepo;
        #endregion

        #region Constructor
        public ThreadLogController(
            ITransactionRuntimeRepository<TransactionRuntime, long> threadLogRepo,
            ITransactionLogRepository<TransactionLog, long> transactionLogRepo)
        {
            _threadLogRepo = threadLogRepo;
            _transactionLogRepo = transactionLogRepo;

            _threadLogRepo.SetUserLogin(this.UserName);
            _transactionLogRepo.SetUserLogin(this.UserName);
        }
        #endregion

        #region Get Transaction Log
        ///// <summary>
        ///// Get transaction log detail
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_TransactionRuntime_Read)]
        public IHttpActionResult GetDetailTransactionLog(int id)
        {
            TransactionLog transaction = _transactionLogRepo.Get(id);
            List<TransactionLog> resendTrans = _transactionLogRepo.GetResendTransaction(id);
            if (transaction == null)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Transaction Log not Found." });
            }

            TransactionLogViewModel result = transaction.ConvertObject<TransactionLogViewModel>();

            if (resendTrans != null && resendTrans.Count > 0)
            {
                result.ResendLog = new List<TransactionLogViewModel>();
                foreach (var item in resendTrans)
                {
                    TransactionLogViewModel resendLog = item.ConvertObject<TransactionLogViewModel>();

                    if (item.Status)
                    {
                        result.IsResolved = item.Status;
                    }

                    result.ResendLog.Add(resendLog);
                }
            }

            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = result });
        }
        #endregion

        #region Method Search
        ///// <summary>
        ///// Load thread log
        ///// </summary>
        ///// <param name="postModel"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_TransactionRuntime_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<TransactionRuntime> listOfLogs = _threadLogRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                var data = listOfLogs.ConvertList<TransactionRuntime, ThreadLogViewModel>();

                data = data.Select(c => { c.ExecutionTime = (int)(c.FinishedTime - c.StartedTime).TotalMilliseconds; return c; }).ToList();

                return Json(new
                {
                    Records = data,
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    Records = new List<TransactionRuntime>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Method SetUserModifier
        /// <summary>
        /// Set modifier for Created By and Updated By
        /// </summary>
        /// <param name="username"></param>
        public override void SetUserModifier(string username)
        {
            _threadLogRepo.SetUserLogin(username);
            _transactionLogRepo.SetUserLogin(username);
        }
        #endregion
    }
}