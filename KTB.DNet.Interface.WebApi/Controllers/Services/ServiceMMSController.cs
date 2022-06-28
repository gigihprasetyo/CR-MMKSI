#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : ServiceMMS Controller
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021-03-23
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
using KTB.DNet.Interface.WebApi.Models;
using KTB.DNet.Interface.WebApi.Models.Examples;
using KTB.DNet.Interface.WebApi.Parameters;
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
    /// ServiceMMS Web API
    /// </summary>    
    [RoutePrefix("Services/ServiceMMS"), ApiGroup("Services")]
    public class ServiceMMSController : BaseController
    {
        #region variables
        private readonly IServiceMMSBL _ServiceMMSBL;
        private readonly ILoggerService _log;
        private readonly JsonMediaTypeFormatter _json;
        #endregion

        #region constructor
        /// <summary>
        /// ServiceMMS constructor with parameter
        /// </summary>
        /// <param name="ServiceMMSBL"></param>
        /// <param name="loggerService"></param>
        public ServiceMMSController(IServiceMMSBL ServiceMMSBL, ILoggerService loggerService)
        {
            _ServiceMMSBL = ServiceMMSBL;
            _log = loggerService;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Get Service MMS list by Criteria
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
        [SwaggerRequestExample(typeof(ServiceMMSFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<ServiceMMSDto>>)), HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [JsonValueValidation(typeof(ServiceMMSFilterDto))]
        [NonAction]
        public IHttpActionResult Read([FromBody]ServiceMMSFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _ServiceMMSBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _ServiceMMSBL.Read(data, PageSize);

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
        /// Create Service MMS
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Create Service MMS</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(ServiceMMSCreateParameterDto), typeof(APICreateServiceMMSExample))]
        [ResponseType(typeof(ResponseBase<ServiceMMSDto>)), HttpPost]
        [JsonValueValidation(typeof(ServiceMMSCreateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceMMS_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceMMS_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]ServiceMMSCreateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _ServiceMMSBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _ServiceMMSBL.Create(data);

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
        /// Update Service MMS
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Update Service MMS</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(ServiceMMSUpdateParameterDto), typeof(APIUpdateServiceMMSExample))]
        [ResponseType(typeof(ResponseBase<ServiceMMSDto>)), HttpPost]
        [JsonValueValidation(typeof(ServiceMMSUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceMMS_Update_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceMMS_Update)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]ServiceMMSUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _ServiceMMSBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _ServiceMMSBL.Update(data);

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
        /// Delete Service MMS
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Delete Service MMS</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Delete")]
        [SwaggerRequestExample(typeof(ServiceMMSUpdateParameterDto), typeof(APIDeleteServiceMMSExample))]
        [ResponseType(typeof(ResponseBase<ServiceMMSDto>)), HttpPost]
        [JsonValueValidation(typeof(ServiceMMSUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceMMS_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceMMS_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [NonAction]
        public IHttpActionResult Delete([FromBody]ServiceMMSUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _ServiceMMSBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _ServiceMMSBL.Update(data);

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
        #endregion
    }
}

