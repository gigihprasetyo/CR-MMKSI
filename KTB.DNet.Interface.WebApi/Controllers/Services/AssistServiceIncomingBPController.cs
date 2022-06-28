#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : AssistServiceIncomingBP Controller
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
    /// AssistServiceIncomingBP Web API
    /// </summary>    
    [RoutePrefix("Services/ServiceIncomingBP"), ApiGroup("Services")]
    public class AssistServiceIncomingBPController : BaseController
    {
        #region variables
        private readonly IAssistServiceIncomingBPBL _AssistServiceIncomingBPBL;
        private readonly ILoggerService _log;
        private readonly JsonMediaTypeFormatter _json;
        #endregion

        #region constructor
        /// <summary>
        /// AssistServiceIncomingBP constructor with parameter
        /// </summary>
        /// <param name="AssistServiceIncomingBPBL"></param>
        /// <param name="loggerService"></param>
        public AssistServiceIncomingBPController(IAssistServiceIncomingBPBL AssistServiceIncomingBPBL, ILoggerService loggerService)
        {
            _AssistServiceIncomingBPBL = AssistServiceIncomingBPBL;
            _log = loggerService;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Get Assist Service Incoming Body Paint list by Criteria
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
        [SwaggerRequestExample(typeof(AssistServiceIncomingBPFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<AssistServiceIncomingBPDto>>)), HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [JsonValueValidation(typeof(AssistServiceIncomingBPFilterDto))]
        [NonAction]
        public IHttpActionResult Read([FromBody]AssistServiceIncomingBPFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _AssistServiceIncomingBPBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _AssistServiceIncomingBPBL.Read(data, PageSize);

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
        /// Create Assist Service Incoming Body Paint
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Create Assist Service Incoming Body Paint</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(AssistServiceIncomingBPCreateParameterDto), typeof(APICreateAssistServiceIncomingBPExample))]
        [ResponseType(typeof(ResponseBase<AssistServiceIncomingBPDto>)), HttpPost]
        [JsonValueValidation(typeof(AssistServiceIncomingBPCreateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceIncomingBP_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceIncomingBP_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]AssistServiceIncomingBPCreateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _AssistServiceIncomingBPBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _AssistServiceIncomingBPBL.Create(data);

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
        /// Update Assist Service Incoming Body Paint
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Update Assist Service Incoming Body Paint</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(AssistServiceIncomingBPUpdateParameterDto), typeof(APIUpdateAssistServiceIncomingBPExample))]
        [ResponseType(typeof(ResponseBase<AssistServiceIncomingBPDto>)), HttpPost]
        [JsonValueValidation(typeof(AssistServiceIncomingBPUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceIncomingBP_Update_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceIncomingBP_Update)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]AssistServiceIncomingBPUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _AssistServiceIncomingBPBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _AssistServiceIncomingBPBL.Update(data);

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
        /// Delete Assist Service Incoming Body Paint
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Delete Assist Service Incoming Body Paint</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Delete")]
        [SwaggerRequestExample(typeof(AssistServiceIncomingBPDeleteParameterDto), typeof(APIDeleteAssistServiceIncomingBPExample))]
        [ResponseType(typeof(ResponseBase<AssistServiceIncomingBPDto>)), HttpPost]
        [JsonValueValidation(typeof(AssistServiceIncomingBPDeleteParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceIncomingBP_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceIncomingBP_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Delete([FromBody]AssistServiceIncomingBPDeleteParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _AssistServiceIncomingBPBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _AssistServiceIncomingBPBL.Delete(data);

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

