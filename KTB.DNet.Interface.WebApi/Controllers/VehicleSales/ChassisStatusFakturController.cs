#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : OpenFakturForPDI Controller
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
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// Open Faktur For PDI Web Api
    /// </summary>
    [RoutePrefix("VehicleSales"), ApiGroup("Vehicle Sales")]
    public class ChassisStatusFakturController : BaseController
    {
        private readonly IVWI_ChassisStatusFakturBL _chassisStatusFakturBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Vehicle sales open faktur controller
        /// </summary>
        /// <param name="chassisStatusFakturBL"></param>
        public ChassisStatusFakturController(IVWI_ChassisStatusFakturBL chassisStatusFakturBL)
        {
            _chassisStatusFakturBL = chassisStatusFakturBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get Vehicle Sales Chassis Status Faktur
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
        [Route("ChassisStatusFaktur/Read")]
        [SwaggerRequestExample(typeof(VWI_ChassisStatusFakturFilterDto), typeof(APIChassisStatusFakturExample))]
        [ResponseType(typeof(ResponseBase<VWI_ChassisStatusFakturDto>)), HttpPost]
        [JsonValueValidation(typeof(VWI_ChassisStatusFakturFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_ChassisStatusFaktur_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_ChassisStatusFaktur_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]VWI_ChassisStatusFakturFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _chassisStatusFakturBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _chassisStatusFakturBL.Read(data.ChassisNumber);

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
        /// Get Vehicle Sales Chassis Status Faktur List
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
        [Route("ChassisStatusFakturList/Read")]
        [SwaggerRequestExample(typeof(List<VWI_ChassisStatusFakturFilterDto>), typeof(APIChassisStatusFakturExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_ChassisStatusFakturDto>>)), HttpPost]
        [JsonValueValidation(typeof(List<VWI_ChassisStatusFakturFilterDto>))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_ChassisStatusFakturList_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_ChassisStatusFakturList_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult ReadList([FromBody]List<VWI_ChassisStatusFakturFilterDto> data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _chassisStatusFakturBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _chassisStatusFakturBL.ReadList(data);

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
        /// Get Vehicle Sales Chassis Status Faktur List
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
        [Route("ChassisStatusFakturListAsync/Read")]
        [SwaggerRequestExample(typeof(List<VWI_ChassisStatusFakturFilterDto>), typeof(APIChassisStatusFakturExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_ChassisStatusFakturDto>>)), HttpPost]
        [JsonValueValidation(typeof(List<VWI_ChassisStatusFakturFilterDto>))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_ChassisStatusFakturList_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_ChassisStatusFakturList_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public async Task<IHttpActionResult> ReadListAsync([FromBody]List<VWI_ChassisStatusFakturFilterDto> data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _chassisStatusFakturBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = await _chassisStatusFakturBL.ReadListAsync(data);

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
        /// Get Vehicle Sales Chassis Status Faktur
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
        [Route("ChassisStatusFaktur/InvoiceRevision/Read")]
        [SwaggerRequestExample(typeof(VWI_ChassisStatusFakturLastUpdateTimeFilterDto), typeof(APIChassisStatusFakturLastUpdateExample))]
        [ResponseType(typeof(ResponseBase<VWI_ChassisStatusFakturDto>)), HttpPost]
        [JsonValueValidation(typeof(VWI_ChassisStatusFakturFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_ChassisStatusFakturIRList_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_ChassisStatusFaktur_InvoiceRevision_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult ReadIR([FromBody]VWI_ChassisStatusFakturLastUpdateTimeFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _chassisStatusFakturBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _chassisStatusFakturBL.ReadListByLastUpdateTime(data.LastUpdateTime);

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
