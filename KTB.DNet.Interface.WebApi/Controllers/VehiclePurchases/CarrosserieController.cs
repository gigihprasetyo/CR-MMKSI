#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Carrosserie Controller
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
using Swashbuckle.Examples;
using System;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// Carroserie Controller
    /// </summary>
    [RoutePrefix("VehiclePurchase/Carrosserie"), ApiGroup("Vehicle Purchase")]
    public class CarrosserieController : BaseController
    {
        private readonly ICarrosserieBL _carrosserieBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carrosserieBL"></param>
        public CarrosserieController(ICarrosserieBL carrosserieBL)
        {
            _carrosserieBL = carrosserieBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create CarrosserieHeader Data 
        /// </summary>
        /// <param name="data">CarrosserieHeader Data </param>
        /// <remarks>Create CarrosserieHeader Data </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(CarrosserieHeaderCreateParameterDto), typeof(APICreateCarrosserieExample))]
        [ResponseType(typeof(ResponseBase<CarrosserieHeaderDto>)), HttpPost]
        [JsonValueValidation(typeof(CarrosserieHeaderCreateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehiclePurchase_Carrosserie_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehiclePurchase_Carrosserie_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody] CarrosserieHeaderCreateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _carrosserieBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _carrosserieBL.Create(data);

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
