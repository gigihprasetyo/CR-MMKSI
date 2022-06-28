#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Bank Controller
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namescape Imports
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Models;
using KTB.DNet.Interface.WebApi.Models.Examples;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// Bank Controller Api
    /// </summary>
    [RoutePrefix("Bank"), ApiGroup("Bank")]
    public class BankController : BaseController
    {
        private readonly IBankBL _bankBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankBL"></param>
        public BankController(IBankBL bankBL)
        {
            _bankBL = bankBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Update Bank entity
        /// </summary>
        /// <param name="data">Bank Parameter </param>
        /// <remarks>Update 'Bank' entity</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [ResponseType(typeof(ResponseBase<BankDto>)), HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [JsonValueValidation(typeof(BankParameterDto))]
        [NonAction]
        public IHttpActionResult Update([FromBody]BankParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _bankBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var log = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _bankBL.Update(data);

                log.FinishedTime = DateTime.Now;

                LogTransactionRuntime(log, result, result.success);

                if (result.success)
                    return Json(result);
                else
                    return Content(GetHttpCodeMsg(result.messages), result, _json);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
            }
        }

        /// <summary>
        /// Create Bank Data 
        /// </summary>
        /// <param name="data">Bank Data </param>
        /// <remarks>Create Bank Data </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [ResponseType(typeof(ResponseBase<BankDto>)), HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [JsonValueValidation(typeof(BankParameterDto))]
        [NonAction]
        public IHttpActionResult Create([FromBody] BankParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _bankBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var log = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _bankBL.Create(data);

                log.FinishedTime = DateTime.Now;

                LogTransactionRuntime(log, result, result.success);

                if (result.success)
                    return Json(result);
                else
                    return Content(GetHttpCodeMsg(result.messages), result, _json);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
            }
        }

        /// <summary>
        /// Delete Bank by ID
        /// </summary>
        /// <param name="id">bank ID</param>
        /// <remarks>Delete by id</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [ActionWebApiFilterAttribute]
        [Route("Delete")]
        [ResponseType(typeof(ResponseBase<BankDto>)), HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _bankBL.Initialize(UserName, DealerCode);

                var result = _bankBL.Delete(id);

                if (result.success)
                    return Json(result);
                else
                    return Content(GetHttpCodeMsg(result.messages), result, _json);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
            }
        }

        /// <summary>
        /// Get Bank list by Criteria
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Search by Criteria</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Read")]
        [SwaggerRequestExample(typeof(BankFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<BankDto>>)), HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [JsonValueValidation(typeof(BankFilterDto))]
        [NonAction]
        public IHttpActionResult Read([FromBody]BankFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _bankBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _bankBL.Read(data, PageSize);

                runtimeLog.FinishedTime = DateTime.Now;
                LogTransactionRuntime(runtimeLog, result, result.success);

                if (result.success)
                    return Json(result);
                else
                    return Content(GetHttpCodeMsg(result.messages), result, _json);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
            }
        }
    }
}
