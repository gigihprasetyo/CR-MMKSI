﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMAsterATA Controller
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Helper.Throttle;
using KTB.DNet.Interface.WebApi.Models.Examples;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.WebApi.Models;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// ChassisMasterATA Controller
    /// </summary>
    /// <summary>
    /// ChassisMasterATA Web API
    /// </summary>    
    /// <seealso cref="KTB.DNet.Interface.WebApi.Controllers.BaseController" />
    [RoutePrefix("VehiclePurchase/ChassisMasterATA"), ApiGroup("Vehicle Purchase")]
    public class ChassisMasterATAController : BaseController
    {
        private readonly IChassisMasterATABL _chassisMasterATABL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>Initializes a new instance of the <see cref="ChassisMasterATAController"/> class.</summary>
        /// <param name="chassisMasterATABL">The chassis master atabl.</param>
        public ChassisMasterATAController(IChassisMasterATABL chassisMasterATABL)
        {
            _chassisMasterATABL = chassisMasterATABL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>ChassisMaster ATA Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <remarks>Search by Criteria</remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("Update")]
        [SwaggerRequestExample(typeof(ChassisMasterATAParameterDto), typeof(APIChassisMasterATAUpdateExample))]
        [ResponseType(typeof(ResponseBase<List<ChassisMasterATADto>>)), HttpPost]
        [JsonValueValidation(typeof(ChassisMasterATAParameterDto))]
        //[ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehiclePurchase_ChassisMasterATA_Update_URL)]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehiclePurchase_ChassisMasterATA_Update)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]ChassisMasterATAParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _chassisMasterATABL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _chassisMasterATABL.UpdateATA(data);

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