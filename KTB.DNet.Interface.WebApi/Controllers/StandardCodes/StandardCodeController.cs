#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCode Controller
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
using KTB.DNet.Interface.WebApi.Models;
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

    [RoutePrefix("StandardCode"), ApiGroup("Standard Code")]
    public class StandardCodeController : BaseController
    {
        private readonly IStandardCodeBL _standardCodeBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="standardCodeBL"></param>
        public StandardCodeController(IStandardCodeBL standardCodeBL)
        {
            this._standardCodeBL = standardCodeBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Update StandardCode entity
        /// </summary>
        /// <param name="data">StandardCode Parameter </param>
        /// <remarks>Update 'StandardCode' entity</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [ResponseType(typeof(ResponseBase<StandardCodeDto>)), HttpPost]
        [JsonValueValidation(typeof(StandardCodeParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_StandardCode_Update_URL)]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_StandardCode_Update)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Update([FromBody]StandardCodeParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _standardCodeBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _standardCodeBL.Update(data);

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
        /// Create StandardCode Data 
        /// </summary>
        /// <param name="data">StandardCode Data </param>
        /// <remarks>Create StandardCode Data </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [ResponseType(typeof(ResponseBase<StandardCodeDto>)), HttpPost]
        [JsonValueValidation(typeof(StandardCodeParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_StandardCode_Create_URL)]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_StandardCode_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Create([FromBody] StandardCodeParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _standardCodeBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _standardCodeBL.Create(data);

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
        /// Delete StandardCode by ID
        /// </summary>
        /// <param name="id">StandardCode ID</param>
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
        [ResponseType(typeof(ResponseBase<StandardCodeDto>)), HttpDelete]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_StandardCode_Delete_URL)]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_StandardCode_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _standardCodeBL.Initialize(UserName, DealerCode);

                var result = _standardCodeBL.Delete(id);

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
        /// Get StandardCode list by Criteria
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
        [SwaggerRequestExample(typeof(StandardCodeFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<StandardCodeDto>>)), HttpPost]
        [JsonValueValidation(typeof(StandardCodeFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_StandardCode_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_StandardCode_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]StandardCodeFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _standardCodeBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _standardCodeBL.Read(data, PageSize);

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
