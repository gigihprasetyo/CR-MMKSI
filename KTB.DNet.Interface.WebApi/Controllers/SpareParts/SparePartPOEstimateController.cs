#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPOEstimate Controller
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
    /// <summary>
    /// SparePartPOEstimate Controller
    /// </summary>
    /// <summary>
    /// SparePartPOEstimate Web API
    /// </summary>    
    [RoutePrefix("SparePart/SparePartPOEstimate"), ApiGroup("Spare Part")]
    public class SparePartPOEstimateController : BaseController
    {
        private readonly IVWI_SparePartPOEstimateBL _sparePartPOEstimateBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SparePartPOEstimate constructor with parameter
        /// </summary>
        /// <param name="sparepartpoestimateBL"></param>
        public SparePartPOEstimateController(IVWI_SparePartPOEstimateBL sparepartpoestimateBL)
        {
            _sparePartPOEstimateBL = sparepartpoestimateBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get SparePartPOEstimate list by Criteria
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
        [Route("~/SparePart/SparePartPOEstimateOri/Read")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [SwaggerRequestExample(typeof(SparePartPOEstimateFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SparePartPOEstimateDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartPOEstimateFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartPOEstimate_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartPOEstimate_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]SparePartPOEstimateFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparePartPOEstimateBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _sparePartPOEstimateBL.Read(data, PageSize);

                runtimeLog.FinishedTime = DateTime.Now;
                LogTransactionRuntime(runtimeLog, result, result.success);

                if (result.success)
                    return Json(result);
                else
                    return Content(GetHttpCodeMsg(result.messages), result, _json);
            }
            catch (HttpResponseException ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.RequestTimeout, GetUnhandledExceptionMsg(ex.Message), _json);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
            }
        }

        /// <summary>
        /// Get SparePartPOEstimate list by Criteria
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
        //[Route("Read")]
        [SwaggerRequestExample(typeof(SparePartPOEstimateFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SparePartPOEstimateDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartPOEstimateFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartPOEstimate_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartPOEstimate_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [NonAction]
        public IHttpActionResult Read1([FromBody]SparePartPOEstimateFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparePartPOEstimateBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _sparePartPOEstimateBL.Read1(data, PageSize);

                runtimeLog.FinishedTime = DateTime.Now;
                LogTransactionRuntime(runtimeLog, result, result.success);

                if (result.success)
                    return Json(result);
                else
                    return Content(GetHttpCodeMsg(result.messages), result, _json);
            }
            catch (HttpResponseException ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.RequestTimeout, GetUnhandledExceptionMsg(ex.Message), _json);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
            }
        }

        /// <summary>
        /// Get SparePartPOEstimate list by Criteria
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
        [SwaggerRequestExample(typeof(SparePartPOEstimateFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SparePartPOEstimateDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartPOEstimateFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartPOEstimate_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartPOEstimate_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult ReadWithCriteria([FromBody]SparePartPOEstimateFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparePartPOEstimateBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _sparePartPOEstimateBL.ReadWithCriteria(data, PageSize);

                runtimeLog.FinishedTime = DateTime.Now;
                LogTransactionRuntime(runtimeLog, result, result.success);

                if (result.success)
                    return Json(result);
                else
                    return Content(GetHttpCodeMsg(result.messages), result, _json);
            }
            catch (HttpResponseException ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.RequestTimeout, GetUnhandledExceptionMsg(ex.Message), _json);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
            }
        }
    }
}

