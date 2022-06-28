#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehiclePurchase Controller
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
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
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
    /// Vehicle purchase API
    /// </summary>    
    [RoutePrefix("VehiclePurchase/VehiclePurchase"), ApiGroup("Vehicle Purchase")]
    public class VehiclePurchaseController : BaseController
    {
        private readonly IVehiclePurchaseBL _vehiclePurchaseBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehiclePurchaseBL"></param>
        public VehiclePurchaseController(IVehiclePurchaseBL vehiclePurchaseBL)
        {
            _vehiclePurchaseBL = vehiclePurchaseBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create VehiclePurchaseHeader Data 
        /// </summary>
        /// <param name="data">VehiclePurchaseHeader Data </param>
        /// <remarks>Create VehiclePurchaseHeader Data </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(VehiclePurchaseHeaderCreateParameterDto), typeof(APIVehiclePurchaseCreateExample))]
        [ResponseType(typeof(ResponseBase<VehiclePurchaseHeaderDto>)), HttpPost]
        [JsonValueValidation(typeof(VehiclePurchaseHeaderCreateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehiclePurchase_VehiclePurchase_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehiclePurchase_VehiclePurchase_Create)]
        public IHttpActionResult Create([FromBody] VehiclePurchaseHeaderCreateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vehiclePurchaseBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _vehiclePurchaseBL.Create(data);

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
