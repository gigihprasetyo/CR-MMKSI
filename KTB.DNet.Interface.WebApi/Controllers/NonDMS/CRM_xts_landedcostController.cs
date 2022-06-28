#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_landedcost class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 09:02:59
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

	[RoutePrefix("NonDMS/Landedcost"), ApiGroup("Non DMS")]
    public class CRM_xts_landedcostController : BaseController
    {
        private readonly ICRM_xts_landedcostBL _CRM_xts_landedcostBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CRM_xts_landedcostBL"></param>
        public CRM_xts_landedcostController(ICRM_xts_landedcostBL CRM_xts_landedcostBL)
        {
            this._CRM_xts_landedcostBL = CRM_xts_landedcostBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

		
        /// <summary>
        /// Get Landedcost list by Criteria
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
        [SwaggerRequestExample(typeof(CRM_xts_landedcostFilterDto), typeof(APIReadCRM_xts_landedcostExample))]
        [ResponseType(typeof(ResponseBase<List<CRM_xts_landedcostDto>>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_landedcostFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_landedcost_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_landedcost_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]CRM_xts_landedcostFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_landedcostBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

				var result = _CRM_xts_landedcostBL.ReadList(data, PageSize);

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
        /// Create Landedcost 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Create Landedcost Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(CRM_xts_landedcostCreateParameterDto), typeof(APICreateCRM_xts_landedcostExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_landedcostDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_landedcostCreateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_landedcost_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_landedcost_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]CRM_xts_landedcostCreateParameterDto data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_landedcostBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_landedcostBL.Create(data);

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
        /// Create List Landedcost
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Create List Landedcost</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("CreateList")]
        [SwaggerRequestExample(typeof(CRM_xts_landedcostCreateParameterDto), typeof(APICreateCRM_xts_landedcostExample))]
        [ResponseType(typeof(ResponseBase<List<CRM_xts_landedcostDto>>)), HttpPost]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_landedcost_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_landedcost_Create)]
        [JsonValueValidation(typeof(List<CRM_xts_landedcostCreateParameterDto>))]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult CreateList([FromBody]List<CRM_xts_landedcostCreateParameterDto> data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_landedcostBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_landedcostBL.BulkCreate(data);

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
        /// Update Landedcost 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Update Landedcost Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(CRM_xts_landedcostUpdateParameterDto), typeof(APIUpdateCRM_xts_landedcostExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_landedcostDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_landedcostUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_landedcost_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_landedcost_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]CRM_xts_landedcostUpdateParameterDto data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_landedcostBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_landedcostBL.Update(data);

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
        /// Delete Landedcost 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Delete Landedcost Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Delete")]
        [SwaggerRequestExample(typeof(CRM_xts_landedcostDeleteParameterDto), typeof(APIDeleteCRM_xts_landedcostExample))]
        [ResponseType(typeof(ResponseBase<CRM_xts_landedcostDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_xts_landedcostDeleteParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_xts_landedcost_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_xts_landedcost_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Delete([FromBody]CRM_xts_landedcostDeleteParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_xts_landedcostBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_xts_landedcostBL.Delete(data);

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
