#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_inventorytransfer class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 27 Aug 2020 12:00:07
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

	[RoutePrefix("NonDMS/Inventorytransfer"), ApiGroup("Non DMS")]
    public class CRM_xts_inventorytransferController : BaseController
    {
        private readonly ICRM_xts_inventorytransferBL _CRM_xts_inventorytransferBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CRM_xts_inventorytransferBL"></param>
        public CRM_xts_inventorytransferController(ICRM_xts_inventorytransferBL CRM_xts_inventorytransferBL)
        {
            this._CRM_xts_inventorytransferBL = CRM_xts_inventorytransferBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

		
        /// <summary>
        /// Get Inventorytransfer list by Criteria
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
        [SwaggerRequestExample(typeof(CRM_xts_inventorytransferFilterDto), typeof(APIReadCRM_xts_inventorytransferExample))]
        [ResponseType(typeof(ResponseBase<List<CRM_xts_inventorytransferDto>>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_inventorytransferFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_inventorytransfer_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_inventorytransfer_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]CRM_xts_inventorytransferFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_inventorytransferBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

				var result = _CRM_xts_inventorytransferBL.ReadList(data, PageSize);

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
        /// Create Inventorytransfer 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Create Inventorytransfer Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(CRM_xts_inventorytransferCreateParameterDto), typeof(APICreateCRM_xts_inventorytransferExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_inventorytransferDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_inventorytransferCreateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_inventorytransfer_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_inventorytransfer_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]CRM_xts_inventorytransferCreateParameterDto data)
        {
            try
            {
                if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_inventorytransferBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_inventorytransferBL.Create(data);

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
        /// Create List Inventorytransfer
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Create List Inventorytransfer</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("CreateList")]
        [SwaggerRequestExample(typeof(CRM_xts_inventorytransferCreateParameterDto), typeof(APICreateCRM_xts_inventorytransferExample))]
        [ResponseType(typeof(ResponseBase<List<CRM_xts_inventorytransferDto>>)), HttpPost]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_inventorytransfer_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_inventorytransfer_Create)]
        [JsonValueValidation(typeof(List<CRM_xts_inventorytransferCreateParameterDto>))]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult CreateList([FromBody]List<CRM_xts_inventorytransferCreateParameterDto> data)
        {
            try
            {
                if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_inventorytransferBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_inventorytransferBL.BulkCreate(data);

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
        /// Update Inventorytransfer 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Update Inventorytransfer Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(CRM_xts_inventorytransferUpdateParameterDto), typeof(APIUpdateCRM_xts_inventorytransferExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_inventorytransferDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_inventorytransferUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_inventorytransfer_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_inventorytransfer_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]CRM_xts_inventorytransferUpdateParameterDto data)
        {
            try
            {
                if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_inventorytransferBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_inventorytransferBL.Update(data);

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
        /// Delete Inventorytransfer 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Delete Inventorytransfer Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Delete")]
        [SwaggerRequestExample(typeof(CRM_xts_inventorytransferDeleteParameterDto), typeof(APIDeleteCRM_xts_inventorytransferExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_inventorytransferDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_inventorytransferDeleteParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_inventorytransfer_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_inventorytransfer_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Delete([FromBody]CRM_xts_inventorytransferDeleteParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_inventorytransferBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_inventorytransferBL.Delete(data);

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
