﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleType Controller
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
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// VehicleType Controller
    /// </summary>
    /// <summary>
    /// VehicleType Web API
    /// </summary>    
    [RoutePrefix("Master/VehicleType"), ApiGroup("Masters")]
    public class VehicleTypeController : BaseController
    {
        private readonly IVehicleTypeBL _vehicletypeBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// VehicleType constructor with parameter
        /// </summary>
        /// <param name="vehicletypeBL"></param>
        public VehicleTypeController(IVehicleTypeBL vehicletypeBL)
        {
            _vehicletypeBL = vehicletypeBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get VehicleType list by Criteria
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
        [SwaggerRequestExample(typeof(VehicleTypeFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VehicleTypeDto>>)), HttpPost]
        [JsonValueValidation(typeof(VehicleTypeFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_VehicleType_Read_URL)]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_VehicleType_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [NonAction]
        public IHttpActionResult Read([FromBody]VehicleTypeFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vehicletypeBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _vehicletypeBL.Read(data, PageSize);

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