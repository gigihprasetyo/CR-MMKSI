#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_vehicleprice class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 05 Feb 2021 13:44:53
 ===========================================================================
*/
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
using KTB.DNet.Interface.Resources;
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

	[RoutePrefix("NonDMS/Vehicleprice"), ApiGroup("Non DMS")]
    public class CRM_xts_vehiclepriceController : BaseController
    {
        private readonly ICRM_xts_vehiclepriceBL _CRM_xts_vehiclepriceBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CRM_xts_vehiclepriceBL"></param>
        public CRM_xts_vehiclepriceController(ICRM_xts_vehiclepriceBL CRM_xts_vehiclepriceBL)
        {
            this._CRM_xts_vehiclepriceBL = CRM_xts_vehiclepriceBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

		
        /// <summary>
        /// Get Vehicleprice list by Criteria
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
        [SwaggerRequestExample(typeof(CRM_xts_vehiclepriceFilterDto), typeof(APIReadCRM_xts_vehiclepriceExample))]
        [ResponseType(typeof(ResponseBase<List<CRM_xts_vehiclepriceDto>>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_vehiclepriceFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_vehicleprice_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_vehicleprice_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]CRM_xts_vehiclepriceFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_vehiclepriceBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

				var result = _CRM_xts_vehiclepriceBL.ReadList(data, PageSize);

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
        /// Create Vehicleprice 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Create Vehicleprice Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(CRM_xts_vehiclepriceCreateParameterDto), typeof(APICreateCRM_xts_vehiclepriceExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_vehiclepriceDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_vehiclepriceCreateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_vehicleprice_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_vehicleprice_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]CRM_xts_vehiclepriceCreateParameterDto data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_vehiclepriceBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_vehiclepriceBL.Create(data);

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
        /// Create List Vehicleprice
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Create List Vehicleprice</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("CreateList")]
        [SwaggerRequestExample(typeof(CRM_xts_vehiclepriceCreateParameterDto), typeof(APICreateCRM_xts_vehiclepriceExample))]
        [ResponseType(typeof(ResponseBase<List<CRM_xts_vehiclepriceDto>>)), HttpPost]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_vehicleprice_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_vehicleprice_Create)]
        [JsonValueValidation(typeof(List<CRM_xts_vehiclepriceCreateParameterDto>))]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult CreateList([FromBody]List<CRM_xts_vehiclepriceCreateParameterDto> data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_vehiclepriceBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_vehiclepriceBL.BulkCreate(data);

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
        /// Update Vehicleprice 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Update Vehicleprice Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(CRM_xts_vehiclepriceUpdateParameterDto), typeof(APIUpdateCRM_xts_vehiclepriceExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_vehiclepriceDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_vehiclepriceUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_vehicleprice_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_vehicleprice_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]CRM_xts_vehiclepriceUpdateParameterDto data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_vehiclepriceBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_vehiclepriceBL.Update(data);

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
        /// Delete Vehicleprice 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Delete Vehicleprice Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Delete")]
        [SwaggerRequestExample(typeof(CRM_xts_vehiclepriceDeleteParameterDto), typeof(APIDeleteCRM_xts_vehiclepriceExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_vehiclepriceDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_vehiclepriceDeleteParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_vehicleprice_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_vehicleprice_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Delete([FromBody]CRM_xts_vehiclepriceDeleteParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_vehiclepriceBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_vehiclepriceBL.Delete(data);

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
