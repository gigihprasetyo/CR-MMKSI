#region Summary
// ===========================================================================
// AUTHOR        : PT BSI 
// PURPOSE       : SparePartForecast Controller
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 10/01/2022 
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
using KTB.DNet.Interface.WebApi.Parameters;
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
    /// SparePartForecast Controller
    /// </summary>
    /// <summary>
    /// SparePartForecast Web API
    /// </summary>    
    [RoutePrefix("SparePart/SparePartForecast"), ApiGroup("Spare Part")]
    public class SparePartForecastController : BaseController
    {
        private readonly ISparePartForecastBL _SparePartForecastBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SparePartForecast constructor with parameter
        /// </summary>
        /// <param name="SparePartForecastBL"></param>
        public SparePartForecastController(ISparePartForecastBL SparePartForecastBL)
        {
            _SparePartForecastBL = SparePartForecastBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create Spare Part Forecast
        /// </summary>
        /// <param name="data">Spare Part Forecast Data</param>
        /// <remarks>Create Spare Part Forecast</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [ResponseType(typeof(ResponseBase<SparePartForecastDto>)), HttpPost]
        [SwaggerRequestExample(typeof(SparePartForecastParameterDto), typeof(APICreateSparePartForecastExample))]
        [JsonValueValidation(typeof(SparePartForecastParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartForecast_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartForecast_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]SparePartForecastParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                _SparePartForecastBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _SparePartForecastBL.Create(data);

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
        /// Create Spare Part Forecast
        /// </summary>
        /// <param name="data">Spare Part Forecast Data</param>
        /// <remarks>Create Spare Part Forecast</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Validator")]
        [ResponseType(typeof(ResponseBase<SparePartForecastValidatorDto>)), HttpPost]
        [SwaggerRequestExample(typeof(SparePartForecastParameterDto), typeof(APICreateSparePartForecastExample))]
        [JsonValueValidation(typeof(SparePartForecastParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartForecast_Validator_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartForecast_Validator)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Validator([FromBody]SparePartForecastParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                _SparePartForecastBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _SparePartForecastBL.Validator(data);

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
        /// Get SparePartForecast StockManagement list by Criteria
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
        [Route("StockManagement/Read")]
        [SwaggerRequestExample(typeof(SparePartForecastStockManagementFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SparePartForecastStockManagementDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartForecastStockManagementFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartForecast_StockManagement_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartForecast_StockManagement_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]SparePartForecastStockManagementFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _SparePartForecastBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _SparePartForecastBL.ReadStockManagement(data, PageSize);

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
        /// Get SparePartForecast Reject list by Criteria
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
        [Route("Reject/Read")]
        [SwaggerRequestExample(typeof(SparePartForecastRejectFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SparePartForecastRejectDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartForecastRejectFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartForecast_Reject_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartForecast_Reject_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]SparePartForecastRejectFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _SparePartForecastBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _SparePartForecastBL.ReadReject(data, PageSize);

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
        /// Get SparePartForecast POEstimate list by Criteria
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
        [Route("POEstimate/Read")]
        [SwaggerRequestExample(typeof(SparePartForecastPOEstimateFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SparePartForecastPOEstimateDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartForecastPOEstimateFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartForecast_POEstimate_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartForecast_POEstimate_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]SparePartForecastPOEstimateFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _SparePartForecastBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _SparePartForecastBL.ReadPOEstimate(data, PageSize);

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
        /// Get SparePartForecast GoodReceipt list by Criteria
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
        [Route("GoodReceipt/Read")]
        [SwaggerRequestExample(typeof(SparePartForecastGoodReceiptFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SparePartForecastGoodReceiptDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartForecastGoodReceiptFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartForecast_GoodReceipt_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartForecast_GoodReceipt_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        //[NonAction]
        public IHttpActionResult Read([FromBody]SparePartForecastGoodReceiptFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _SparePartForecastBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _SparePartForecastBL.ReadGoodReceipt(data, PageSize);

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

