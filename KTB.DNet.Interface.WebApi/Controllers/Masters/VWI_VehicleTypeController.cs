#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Area1 Controller
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
    [RoutePrefix("Master/WebsiteVehicleType"), ApiGroup("Masters")]
    public class VWI_VehicleTypeController : BaseController
    {
        private readonly IVWI_VehicleTypeBL _vwi_vehicletypeBL;
        private readonly JsonMediaTypeFormatter _json;

        public VWI_VehicleTypeController(IVWI_VehicleTypeBL vwi_vehicletype)
        {
            _vwi_vehicletypeBL = vwi_vehicletype;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        [Route("Read")]
        [SwaggerRequestExample(typeof(VWI_VehicleTypeFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_VehicleTypeDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_VehicleTypeFilterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_VWI_VehicleType_Read)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_VWI_VehicleType_Read_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]VWI_VehicleTypeFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vwi_vehicletypeBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _vwi_vehicletypeBL.Read(data, PageSize);

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
