#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartStock Controller
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Helper.Throttle;
using KTB.DNet.Interface.WebApi.Models.Examples;
using KTB.DNet.Interface.WebApi.Parameters;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// AssistPartStock Web API
    /// </summary>    
    [RoutePrefix("SparePart/AssistPartStock"), ApiGroup("Spare Part")]
    public class AssistPartStockController : BaseController
    {
        #region variables
        private readonly IAssistPartStockBL _assistpartstockBL;
        private readonly JsonMediaTypeFormatter _json;
        #endregion

        #region constructor
        /// <summary>
        /// AssistPartStock constructor with parameter
        /// </summary>
        /// <param name="assistpartstockBL"></param>
        public AssistPartStockController(IAssistPartStockBL assistpartstockBL)
        {
            _assistpartstockBL = assistpartstockBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Get Assist Part Stock list by Criteria
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
        //[Route("Read")]
        //[SwaggerRequestExample(typeof(AssistPartStockFilterDto), typeof(APIReadExample))]
        //[ResponseType(typeof(ResponseBase<List<AssistPartStockDto>>)), HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [JsonValueValidation(typeof(AssistPartStockFilterDto))]
        [NonAction]
        public IHttpActionResult Read([FromBody]AssistPartStockFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _assistpartstockBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _assistpartstockBL.Read(data, PageSize);

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

        /// <summary>
        /// Create Assist Part Stock
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Create Assist Part Stock</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(AssistPartStockParameterDto), typeof(APICreateAssistPartStockExample))]
        [ResponseType(typeof(ResponseBase<AssistPartStockDto>)), HttpPost]
        [JsonValueValidation(typeof(AssistPartStockParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_AssistPartStock_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_AssistPartStock_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]AssistPartStockParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                _assistpartstockBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _assistpartstockBL.Create(data);

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

        /// <summary>
        /// Create List Assist Part Stock
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Create List Assist Part Stock</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("CreateList")]
        [SwaggerRequestExample(typeof(AssistPartStockParameterDto), typeof(APICreateAssistPartStockExample))]
        [ResponseType(typeof(ResponseBase<List<AssistPartStockDto>>)), HttpPost]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_AssistPartStock_CreateList_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_AssistPartStock_CreateList)]
        [JsonValueValidation(typeof(List<AssistPartStockParameterDto>))]
        public IHttpActionResult CreateList([FromBody]List<AssistPartStockParameterDto> data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                foreach (var item in data)
                {
                    if (item.DealerCode != DealerCode)
                    {
                        return InvalidDealerCode(_json, item.DealerCode);
                    }
                }

                _assistpartstockBL.Initialize(GetUsername(data[0].ResendBy, data[0].LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _assistpartstockBL.Create(data);

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

        /// <summary>
        /// Create List Assist Part Stock
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Create List Assist Part Stock</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("CreateListAsync")]
        [SwaggerRequestExample(typeof(AssistPartStockParameterDto), typeof(APICreateAssistPartStockExample))]
        [ResponseType(typeof(ResponseBase<List<AssistPartStockDto>>)), HttpPost]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_AssistPartStock_CreateListAsync_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_AssistPartStock_CreateList)]
        [JsonValueValidation(typeof(List<AssistPartStockParameterDto>))]
        public async Task<IHttpActionResult> CreateListAsync([FromBody]List<AssistPartStockParameterDto> data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                foreach (var item in data)
                {
                    if (item.DealerCode != DealerCode)
                    {
                        return InvalidDealerCode(_json, item.DealerCode);
                    }
                }

                _assistpartstockBL.Initialize(GetUsername(data[0].ResendBy, data[0].LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = await _assistpartstockBL.BulkCreateAsync(data);

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

        /// <summary>
        /// Update Assist Part Stock List
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Update Assist Part Stock List</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("UpdateList")]
        [SwaggerRequestExample(typeof(AssistPartStockUpdateParameterDto), typeof(APIUpdateAssistPartStockExample))]
        [ResponseType(typeof(ResponseBase<List<AssistPartStockDto>>)), HttpPost]
        [JsonValueValidation(typeof(List<AssistPartStockUpdateParameterDto>))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_AssistPartStock_UpdateList_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_AssistPartStock_UpdateList)]
        public IHttpActionResult UpdateList([FromBody]List<AssistPartStockUpdateParameterDto> data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                foreach (var item in data)
                {
                    if (item.DealerCode != DealerCode)
                    {
                        return InvalidDealerCode(_json, item.DealerCode);
                    }
                }

                _assistpartstockBL.Initialize(GetUsername(data[0].ResendBy, data[0].LogId), DealerCode);

                List<AssistPartStockParameterDto> lst = data.ConvertAll(x => (AssistPartStockParameterDto)x);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _assistpartstockBL.Update(lst);

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

        /// <summary>
        /// Update Assist Part Stock
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Update Assist Part Stock</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(AssistPartStockUpdateParameterDto), typeof(APIUpdateAssistPartStockExample))]
        [ResponseType(typeof(ResponseBase<AssistPartStockDto>)), HttpPost]
        [JsonValueValidation(typeof(AssistPartStockUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_AssistPartStock_Update_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_AssistPartStock_Update)]
        public IHttpActionResult Update([FromBody]AssistPartStockUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                _assistpartstockBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _assistpartstockBL.Update(data);

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

        /// <summary>
        /// Delete AssistPartStock by ID
        /// </summary>
        /// <param name="id">assistpartstock ID</param>
        /// <remarks>Delete by id</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        //[ActionWebApiFilterAttribute]
        //[Route("Delete")]
        //[ResponseType(typeof(ResponseBase<AssistPartStockDto>)), HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _assistpartstockBL.Delete(id);

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
        #endregion
    }
}

