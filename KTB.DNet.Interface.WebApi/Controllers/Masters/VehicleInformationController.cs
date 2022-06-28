#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleInformation Controller
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
    /// VehicleInformation Controller
    /// </summary>
    /// <summary>
    /// VehicleInformation Web API
    /// </summary>    
    [RoutePrefix("Master/VehicleInformation"), ApiGroup("Masters")]
    public class VehicleInformationController : BaseController
    {
        private readonly IVWI_VehicleInformationBL _vehicleInformationBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// VehicleInformation constructor with parameter
        /// </summary>
        /// <param name="vehicletypeBL"></param>
        public VehicleInformationController(IVWI_VehicleInformationBL vehicletypeBL)
        {
            _vehicleInformationBL = vehicletypeBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get VehicleInformation list by Criteria
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
        [SwaggerRequestExample(typeof(VWI_VehicleInformationFilterDto), typeof(APIReadEmptyFilterExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_VehicleInformationDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_VehicleInformationFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_VehicleInformation_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_VehicleInformation_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]VWI_VehicleInformationFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vehicleInformationBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _vehicleInformationBL.Read(data, PageSize);

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
        /// Get VehicleInformation list by Criteria
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
        [SwaggerRequestExample(typeof(VWI_VehicleInformationFilterDto), typeof(APIReadEmptyFilterExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_VehicleInformationDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_VehicleInformationFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_VehicleInformation_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_VehicleInformation_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [NonAction]
        public IHttpActionResult ReadWithView([FromBody]VWI_VehicleInformationFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vehicleInformationBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { MethodName = Constants.EndpointUrl.WebAPI_Master_VehicleInformation_Read_URL + "WithView", StartedTime = DateTime.Now };

                var result = _vehicleInformationBL.ReadWithView(data, PageSize);

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