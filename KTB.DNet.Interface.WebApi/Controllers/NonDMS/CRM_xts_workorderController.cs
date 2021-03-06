#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_workorder class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 19:46:39
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

	[RoutePrefix("NonDMS/Workorder"), ApiGroup("Non DMS")]
    public class CRM_xts_workorderController : BaseController
    {
        private readonly ICRM_xts_workorderBL _CRM_xts_workorderBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CRM_xts_workorderBL"></param>
        public CRM_xts_workorderController(ICRM_xts_workorderBL CRM_xts_workorderBL)
        {
            this._CRM_xts_workorderBL = CRM_xts_workorderBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

		
        /// <summary>
        /// Get Workorder list by Criteria
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
        [SwaggerRequestExample(typeof(CRM_xts_workorderFilterDto), typeof(APIReadCRM_xts_workorderExample))]
        [ResponseType(typeof(ResponseBase<List<CRM_xts_workorderDto>>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_workorderFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_workorder_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_workorder_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]CRM_xts_workorderFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_workorderBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

				var result = _CRM_xts_workorderBL.ReadList(data, PageSize);

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
        /// Create Workorder 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Create Workorder Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(CRM_xts_workorderCreateParameterDto), typeof(APICreateCRM_xts_workorderExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_workorderDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_workorderCreateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_workorder_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_workorder_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]CRM_xts_workorderCreateParameterDto data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_workorderBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_workorderBL.Create(data);

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
        /// Create List Workorder
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Create List Workorder</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("CreateList")]
        [SwaggerRequestExample(typeof(CRM_xts_workorderCreateParameterDto), typeof(APICreateCRM_xts_workorderExample))]
        [ResponseType(typeof(ResponseBase<List<CRM_xts_workorderDto>>)), HttpPost]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_workorder_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_workorder_Create)]
        [JsonValueValidation(typeof(List<CRM_xts_workorderCreateParameterDto>))]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult CreateList([FromBody]List<CRM_xts_workorderCreateParameterDto> data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_workorderBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_workorderBL.BulkCreate(data);

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
        /// Update Workorder 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Update Workorder Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(CRM_xts_workorderUpdateParameterDto), typeof(APIUpdateCRM_xts_workorderExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_workorderDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_workorderUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_workorder_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_workorder_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]CRM_xts_workorderUpdateParameterDto data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_workorderBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_workorderBL.Update(data);

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
        /// Delete Workorder 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Delete Workorder Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Delete")]
        [SwaggerRequestExample(typeof(CRM_xts_workorderDeleteParameterDto), typeof(APIDeleteCRM_xts_workorderExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_workorderDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_workorderDeleteParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_workorder_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_workorder_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Delete([FromBody]CRM_xts_workorderDeleteParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_workorderBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_workorderBL.Delete(data);

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
