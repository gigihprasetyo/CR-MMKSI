#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleColor Controller
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
    /// VehicleColor Controller
    /// </summary>
    /// <summary>
    /// VehicleColor Web API
    /// </summary>    
    [RoutePrefix("Master/VehicleColor"), ApiGroup("Masters")]
    public class VehicleColorController : BaseController
    {
        private readonly IVehicleColorBL _vehiclecolorBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// VehicleColor constructor with parameter
        /// </summary>
        /// <param name="vehiclecolorBL"></param>
        public VehicleColorController(IVehicleColorBL vehiclecolorBL)
        {
            _vehiclecolorBL = vehiclecolorBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get VehicleColor list by Criteria
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
        [SwaggerRequestExample(typeof(VehicleColorFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VehicleColorDto>>)), HttpPost]
        [JsonValueValidation(typeof(VehicleColorFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_VehicleColor_Read_URL)]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_VehicleColor_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [NonAction]
        public IHttpActionResult Read([FromBody]VehicleColorFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vehiclecolorBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _vehiclecolorBL.Read(data, PageSize);

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