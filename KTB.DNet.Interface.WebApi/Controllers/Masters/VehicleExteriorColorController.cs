#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleExteriorColor Controller
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
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Models.Examples;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;


#endregion

namespace KTB.DNet.Interface.WebApi.Controllers.Master
{
    /// <summary>
    /// Vehicle Exterior color API controller
    /// </summary>
    [RoutePrefix("Master/VehicleExteriorColor"), ApiGroup("Masters")]
    public class VehicleExteriorColorController : BaseController
    {
        private readonly IVWI_VehicleExteriorColorBL _vehicleExteriorColorBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// VehicleExteriorColorController cnstructor
        /// </summary>
        /// <param name="vehicleExteriorColorBL"></param>
        public VehicleExteriorColorController(IVWI_VehicleExteriorColorBL vehicleExteriorColorBL)
        {
            _vehicleExteriorColorBL = vehicleExteriorColorBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get Vehicle Exterior Color list by Criteria
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Read")]
        [SwaggerRequestExample(typeof(VWI_VehicleExteriorColorFilterDto), typeof(APIReadVehicleExteriorColorExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_VehicleExteriorColorDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_VehicleExteriorColorFilterDto))]
        //[ScheduleAuthorize(ControllerMethodName = Constants.EndPointUrl.ReadMasterVehicleExteriorColor)]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_VehicleExteriorColor_Read)]
        [NonAction]
        public IHttpActionResult Read([FromBody]VWI_VehicleExteriorColorFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vehicleExteriorColorBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _vehicleExteriorColorBL.Read(data, PageSize);

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
