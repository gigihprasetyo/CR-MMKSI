#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PDI Controller
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
using KTB.DNet.Interface.WebApi.Models.Examples;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers.Services
{
    /// <summary>
    /// WarrantyActivation Controller
    /// </summary>
    /// <summary>
    /// WarrantyActivation Web API
    /// </summary>
    [RoutePrefix("Services/WarrantyActivation"), ApiGroup("Services")]
    public class WarrantyActivationController : BaseController
    {
        private readonly IWarrantyActivationBL _warrantyActivationBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// WarrantyActivation constructor with parameter
        /// </summary>
        ///<param name="warrantyActivationBL"></param>
        public WarrantyActivationController(IWarrantyActivationBL warrantyActivationBL)
        {
            _warrantyActivationBL = warrantyActivationBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Read V_WarrantyActivation by Criteria
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
        [SwaggerRequestExample(typeof(V_WarrantyActivationFilterDto), typeof(APIReadEmptyFilterExample))]
        [ResponseType(typeof(ResponseBase<List<V_WarrantyActivationDto>>)), HttpPost]
        [JsonValueValidation(typeof(V_WarrantyActivationFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_WarrantyActivation_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_WarrantyActivation_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]V_WarrantyActivationFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _warrantyActivationBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _warrantyActivationBL.Read(data, 50);

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
        /// Create WarrantyActivation
        /// </summary>
        /// <param name="data">WarrantyActivation Data</param>
        /// <remarks>Create WarrantyActivation</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(WarrantyActivationParameterDto), typeof(APICreateWarrantyActivationExample))]
        [ResponseType(typeof(ResponseBase<WarrantyActivationDto>)), HttpPost]
        [JsonValueValidation(typeof(WarrantyActivationParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_WarrantyActivation_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_WarrantyActivation_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]WarrantyActivationParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _warrantyActivationBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _warrantyActivationBL.Create(data);

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
        /// Get PKT Certificate
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>Get PKT Certificate</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("GetPDF")]
        [HttpPost]
        [SwaggerRequestExample(typeof(WarrantyActivationGetFileParameter), typeof(APIGetPDFPDIExample))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_WarrantyActivation_GetFile)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_WarrantyActivation_GetFile_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public HttpResponseMessage GetPDF([FromBody] WarrantyActivationGetFileParameter parameter)
        {
            if (String.IsNullOrEmpty(parameter.ChassisNumber))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            _warrantyActivationBL.Initialize(UserName, DealerCode);

            string fileName = string.Empty;
            FileStream file = _warrantyActivationBL.GetFile(parameter, out fileName);
            if (file != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(file);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = fileName;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                return response;
            }
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
