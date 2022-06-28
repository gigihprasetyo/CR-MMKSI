
#region Namespace Imports
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Helper.Throttle;
using KTB.DNet.Interface.WebApi.Models;
using KTB.DNet.Interface.WebApi.Models.Examples;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// StallWorkingTime Controller
    /// </summary>
    /// <summary>
    /// StallWorkingTime Web API
    /// </summary>    
    [RoutePrefix("Services/StallWorkingTime"), ApiGroup("Services")]
    public class StallWorkingTimeController : BaseController
    {
        private readonly IStallWorkingTimeBL _stallWorkingTimeBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// StallWorkingTime constructor with parameter
        /// </summary>
        /// <param name="stallMasterBL"></param>
        public StallWorkingTimeController(IStallWorkingTimeBL stallWorkingTimeBL)
        {
            _stallWorkingTimeBL = stallWorkingTimeBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create StallWorkingTime
        /// </summary>
        /// <param name="data">StallWorkingTime Data</param>
        /// <remarks>Create Stall Master</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(StallWorkingTimeParameterDto), typeof(APICreateStallWorkingTimeExample))]
        [ResponseType(typeof(ResponseBase<StallWorkingTimeDto>)), HttpPost]
        [JsonValueValidation(typeof(StallWorkingTimeParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_StallWorkingTime_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_StallWorkingTime_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody] StallWorkingTimeParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }
                _stallWorkingTimeBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _stallWorkingTimeBL.Create(data);

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
        /// Update StallWorkingTime
        /// </summary>
        /// <param name="data">StallWorkingTime Update Info</param>
        /// <remarks>Update StallWorkingTime</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(StallWorkingTimeUpdateParameterDto), typeof(APIUpdateStallWorkingTimeExample))]
        [ResponseType(typeof(ResponseBase<StallWorkingTimeDto>)), HttpPost]
        [JsonValueValidation(typeof(StallWorkingTimeUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_StallWorkingTime_Update_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_StallWorkingTime_Update)]
        public IHttpActionResult Update([FromBody] StallWorkingTimeUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _stallWorkingTimeBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _stallWorkingTimeBL.Update(data);

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
        /// Create List StallWorkingTime
        /// </summary>
        /// <param name="data">StallWorkingTime Data</param>
        /// <remarks>Create Stall Master</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("CreateList")]
        [SwaggerRequestExample(typeof(StallWorkingTimeCreateListParameterDto), typeof(APICreateStallWorkingTimeExample))]
        [ResponseType(typeof(ResponseBase<List<StallWorkingTimeDto>>)), HttpPost]
        [JsonValueValidation(typeof(List<StallWorkingTimeCreateListParameterDto>))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_StallWorkingTime_CreateList_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_StallWorkingTime_CreateList)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult CreateList([FromBody] List<StallWorkingTimeCreateListParameterDto> data)
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

                _stallWorkingTimeBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _stallWorkingTimeBL.BulkCreate(data);

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
        /// Update List StallWorkingTime
        /// </summary>
        /// <param name="data">StallWorkingTime Update Info</param>
        /// <remarks>Update StallWorkingTime</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("UpdateList")]
        [SwaggerRequestExample(typeof(StallWorkingTimeUpdateListParameterDto), typeof(APIUpdateStallWorkingTimeExample))]
        [ResponseType(typeof(ResponseBase<List<StallWorkingTimeDto>>)), HttpPost]
        [JsonValueValidation(typeof(List<StallWorkingTimeUpdateListParameterDto>))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_StallWorkingTime_UpdateList_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_StallWorkingTime_UpdateList)]
        public IHttpActionResult UpdateList([FromBody] List<StallWorkingTimeUpdateListParameterDto> data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _stallWorkingTimeBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _stallWorkingTimeBL.BulkUpdate(data);

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