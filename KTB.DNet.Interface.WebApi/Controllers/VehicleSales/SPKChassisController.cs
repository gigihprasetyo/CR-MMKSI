#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKChassis Controller
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
    /// <summary>
    /// SPK Chassis API Controller
    /// </summary>
    [RoutePrefix("VehicleSales/SPKChassis"), ApiGroup("Vehicle Sales")]
    public class SPKChassisController : BaseController
    {
        private readonly ISPKChassisBL _spkChassisBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SPK Chassis Constructor with parameter
        /// </summary>
        /// <param name="spkChassisBL"></param>
        public SPKChassisController(ISPKChassisBL spkChassisBL)
        {
            _spkChassisBL = spkChassisBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create SPK Chassis
        /// </summary>
        /// <param name="data">SPK Chassis Data</param>
        /// <remarks>Create SPK Chassis</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(SPKChassisParameterDto), typeof(APICreateSPKChassisExample))]
        [ResponseType(typeof(ResponseBase<SPKChassisDto>)), HttpPost]
        [JsonValueValidation(typeof(SPKChassisParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_SPKChassis_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_SPKChassis_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]SPKChassisParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _spkChassisBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _spkChassisBL.Create(data);

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
        /// Validate Chassis
        /// </summary>
        /// <param name="data">Validate Chassis</param>
        /// <remarks>Validate Chassis</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("ValidateChassis")]
        [SwaggerRequestExample(typeof(ValidateChassisParameterDto), typeof(APICreateValidateChassisExample))]
        [ResponseType(typeof(ResponseBase<List<ValidateChassisDto>>)), HttpPost]
        [JsonValueValidation(typeof(List<ValidateChassisParameterDto>))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_SPKChassis_ValidateChassis_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_SPKChassis_ValidateChassis)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult ValidateChassis([FromBody]List<ValidateChassisParameterDto> data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _spkChassisBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _spkChassisBL.ValidateChassis(data);
                foreach(var i in result.messages)
                {
                    if(i.ErrorCode == ErrorCode.Er_OK)
                    {
                        result.total += 1;
                    }
                }
                if(result.total==result.messages.Count)
                {
                    result.success = true;
                }

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
        /// Delete SPK Chassis by ID
        /// </summary>
        /// <param name="id">SPK Chassis ID</param>
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
        [ResponseType(typeof(ResponseBase<SPKChassisDto>)), HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _spkChassisBL.Initialize(UserName, DealerCode);

                var result = _spkChassisBL.Delete(id);

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
        /// Get SPK Chassis list by Criteria
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
        [SwaggerRequestExample(typeof(SPKChassisFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SPKChassisDto>>)), HttpPost]
        [JsonValueValidation(typeof(SPKChassisFilterDto))]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Read([FromBody]SPKChassisFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _spkChassisBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _spkChassisBL.Read(data, PageSize);

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
