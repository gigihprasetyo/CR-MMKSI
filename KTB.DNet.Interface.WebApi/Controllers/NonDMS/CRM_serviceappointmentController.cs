#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_serviceappointment class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 09:35:16
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

	[RoutePrefix("NonDMS/Serviceappointment"), ApiGroup("Non DMS")]
    public class CRM_serviceappointmentController : BaseController
    {
        private readonly ICRM_serviceappointmentBL _CRM_serviceappointmentBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CRM_serviceappointmentBL"></param>
        public CRM_serviceappointmentController(ICRM_serviceappointmentBL CRM_serviceappointmentBL)
        {
            this._CRM_serviceappointmentBL = CRM_serviceappointmentBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

		
        /// <summary>
        /// Get Serviceappointment list by Criteria
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
        [SwaggerRequestExample(typeof(CRM_serviceappointmentFilterDto), typeof(APIReadCRM_serviceappointmentExample))]
        [ResponseType(typeof(ResponseBase<List<CRM_serviceappointmentDto>>)), HttpPost]
        [JsonValueValidation(typeof(CRM_serviceappointmentFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_serviceappointment_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_serviceappointment_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]CRM_serviceappointmentFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_serviceappointmentBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

				var result = _CRM_serviceappointmentBL.ReadList(data, PageSize);

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
        /// Create Serviceappointment 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Create Serviceappointment Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(CRM_serviceappointmentCreateParameterDto), typeof(APICreateCRM_serviceappointmentExample))]
        [ResponseType(typeof(ResponseBase<CRM_serviceappointmentDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_serviceappointmentCreateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_serviceappointment_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_serviceappointment_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]CRM_serviceappointmentCreateParameterDto data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_serviceappointmentBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_serviceappointmentBL.Create(data);

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
        /// Create List Serviceappointment
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Create List Serviceappointment</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("CreateList")]
        [SwaggerRequestExample(typeof(CRM_serviceappointmentCreateParameterDto), typeof(APICreateCRM_serviceappointmentExample))]
        [ResponseType(typeof(ResponseBase<List<CRM_serviceappointmentDto>>)), HttpPost]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_serviceappointment_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_serviceappointment_Create)]
        [JsonValueValidation(typeof(List<CRM_serviceappointmentCreateParameterDto>))]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult CreateList([FromBody]List<CRM_serviceappointmentCreateParameterDto> data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_serviceappointmentBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_serviceappointmentBL.BulkCreate(data);

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
        /// Update Serviceappointment 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Update Serviceappointment Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(CRM_serviceappointmentUpdateParameterDto), typeof(APIUpdateCRM_serviceappointmentExample))]
        [ResponseType(typeof(ResponseBase<CRM_serviceappointmentDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_serviceappointmentUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_serviceappointment_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_serviceappointment_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]CRM_serviceappointmentUpdateParameterDto data)
        {
            try
            {
				if (data == null)
                { throw new System.Exception(MessageResource.ErrorMsgTypeDataInObject); }

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_serviceappointmentBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_serviceappointmentBL.Update(data);

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
        /// Delete Serviceappointment 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// /// <remarks>Delete Serviceappointment Service</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Delete")]
        [SwaggerRequestExample(typeof(CRM_serviceappointmentDeleteParameterDto), typeof(APIDeleteCRM_serviceappointmentExample))]
        [ResponseType(typeof(ResponseBase<CRM_serviceappointmentDto>)), HttpPost]
        [JsonValueValidation(typeof(CRM_serviceappointmentDeleteParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_CRM_serviceappointment_Delete_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_CRM_serviceappointment_Delete)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Delete([FromBody]CRM_serviceappointmentDeleteParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _CRM_serviceappointmentBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _CRM_serviceappointmentBL.Delete(data);

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
