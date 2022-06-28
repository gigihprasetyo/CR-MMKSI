#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ErrorLog controller class
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class ErrorLogController : BaseController
    {
        #region Initialize
        private IElmahErrorRepository<ELMAH_Error, Guid> _elmahErrorRepo;
        #endregion

        #region Constructor
        public ErrorLogController(
            IElmahErrorRepository<ELMAH_Error, Guid> elmahErrorRepo)
        {
            _elmahErrorRepo = elmahErrorRepo;

            _elmahErrorRepo.SetUserLogin(this.UserName);
        }
        #endregion

        #region Method Search
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_ErrorLog_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel, string appName)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                if (postModel.Order != null && postModel.Order.Column.Equals("TimeLocal", StringComparison.OrdinalIgnoreCase))
                {
                    postModel.Order.Column = "TimeUtc";
                }

                if (postModel.Order != null && (postModel.Order.Column.Equals("Verb", StringComparison.OrdinalIgnoreCase) || postModel.Order.Column.Equals("URL", StringComparison.OrdinalIgnoreCase)))
                {
                    postModel.Order.Column = string.Empty;
                }

                List<ELMAH_Error> listOfLogs = _elmahErrorRepo.Search(postModel, out filteredResultsCount, out totalResultsCount, appName);

                List<ElmahErrorViewModel> data;

                data = listOfLogs.Select(c =>
                {
                    ElmahErrorViewModel error = c.ConvertObject<ElmahErrorViewModel>();
                    error.TimeLocal = c.TimeUtc.ToLocalTime();
                    error.Message = c.Message.Length > 50 ? c.Message.Substring(0, 50) + "..." : c.Message;
                    error.URL = c.URL.Length > 50 ? c.URL.Substring(0, 50) + "..." : c.URL;
                    return error;
                }).ToList();

                return Json(new
                {
                    Records = data,
                    TotalRecord = filteredResultsCount
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Records = new List<ELMAH_Error>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Method Get
        /// <summary>
        /// Get error detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>elmah_error object on httpactionresult</returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_ErrorLog_Read)]
        public async Task<IHttpActionResult> GetAsync(string id)
        {
            ELMAH_Error result = null;

            if (!string.IsNullOrEmpty(id))
            {
                result = await _elmahErrorRepo.GetAsync(new Guid(id));
            }

            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = result });
        }

        /// <summary>
        /// Get error detail by id and application
        /// </summary>
        /// <param name="id"></param>
        /// <param name="app"></param>
        /// <returns>elmah_error object on http response</returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_ErrorLog_Read)]
        [Route("api/ErrorLog/GetErrorDetailAsync")]
        public async Task<HttpResponseMessage> GetErrorDetailAsync(string id, string app)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(id))
            {
                result = await _elmahErrorRepo.GetErrorDetailAsync(new Guid(id), app);
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);

                response.Content = new StringContent(result, Encoding.UTF8, "application/json");

                return response;
            }
            else
            {
                HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.BadRequest);
                return response;
            }



            //return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = new StringContent(result, Encoding.UTF8, "application/json") });
        }

        /// <summary>
        /// get error summary for chart
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_ErrorLog_Read)]
        [HttpGet]
        public async Task<IHttpActionResult> GetErrorLogSummaryPerApplication()
        {
            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = await _elmahErrorRepo.GetErrorLogSummaryPerApplication()
            });
        }

        /// <summary>
        /// get latest error log
        /// </summary>
        /// <param name="totalTake"></param>
        /// <param name="appName"></param>
        /// <param name="severity"></param>
        /// <returns>list of elmah_error object</returns>
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_ErrorLog_Read)]
        [HttpGet]
        public async Task<IHttpActionResult> GetLatestErrorLog(int totalTake, string appName, int severity)
        {
            var result = await _elmahErrorRepo.GetLatestErrorLog(totalTake, appName, severity);
            List<ELMAH_Error> dataResult = result.ToList();

            List<ElmahErrorViewModel> data;

            data = dataResult.Select(c =>
            {
                ElmahErrorViewModel error = c.ConvertObject<ElmahErrorViewModel>();
                error.TimeLocal = c.TimeUtc.ToLocalTime();
                error.Message = c.Message.Length > 200 ? c.Message.Substring(0, 200) + "..." : c.Message;
                return error;
            }).ToList();

            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = data
            });
        }

        /// <summary>
        /// get data for piechart
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_ErrorLog_Read)]
        [HttpGet]
        public async Task<IHttpActionResult> GetErrorLogMainInfo()
        {
            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = await _elmahErrorRepo.GetErrorLogMainInfo()
            });
        }

        /// <summary>
        /// get applications that available on db
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_ErrorLog_Read)]
        [HttpGet]
        public async Task<IHttpActionResult> GetApplicationLogList()
        {
            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = await _elmahErrorRepo.GetListOfApplication()
            });
        }
        #endregion

        #region Method Delete Transction Log Within Interval Date
        /// <summary>
        /// Delete transaction log
        /// </summary>
        /// <param name="intervalParam"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Log_Delete)]
        public IHttpActionResult Delete(DeleteLogViewModel intervalParam)
        {
            try
            {
                DateTime retainedLogFromDate = DateTime.UtcNow.AddDays(-AppConfigs.GetInt("RetainTransactionLogDays")).Date;
                DateTime from = intervalParam.From.Date;
                DateTime to = intervalParam.To.Date;

                if (from > to)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Invalid interval date." });
                }

                if (to >= retainedLogFromDate)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("Retained transaction log. Unable to delete transaction log with date >= {0}", retainedLogFromDate.ToString("yyyy-MM-dd")) });
                }

                ResponseMessage result = _elmahErrorRepo.Delete(from, to);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Failed to delete transaction log. " + GetInnerException(ex).Message });
            }
        }
        #endregion
    }
}
