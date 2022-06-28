
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
    /// ServiceBooking Controller
    /// </summary>
    /// <summary>
    /// ServiceBooking Web API
    /// </summary>    
    [RoutePrefix("Services/ServiceBooking"), ApiGroup("Services")]
    public class ServiceBookingController : BaseController
    {
        private readonly IServiceBookingBL _serviceBookingBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// ServiceBooking constructor with parameter
        /// </summary>
        /// <param name="serviceBookingBL"></param>
        public ServiceBookingController(IServiceBookingBL serviceBookingBL)
        {
            _serviceBookingBL = serviceBookingBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Read Service Booking by Criteria
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
        [Route("Realtime/Read")]
        [SwaggerRequestExample(typeof(ServiceBookingFilterDto), typeof(APIReadEmptyFilterExample))]
        [ResponseType(typeof(ResponseBase<List<ServiceBookingRealtimeReadDto>>)), HttpPost]
        [JsonValueValidation(typeof(ServiceBookingFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceBooking_Realtime_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceBooking_Realtime_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult RealtimeRead([FromBody]ServiceBookingFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceBookingBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _serviceBookingBL.RealtimeRead(data, PageSize);

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
        /// Create ServiceBooking
        /// </summary>
        /// <param name="data">ServiceBooking Data</param>
        /// <remarks>Create ServiceBooking</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(ServiceBookingParameterDto), typeof(APICreateServiceBookingExample))]
        [ResponseType(typeof(ResponseBase<ServiceBookingDto>)), HttpPost]
        [JsonValueValidation(typeof(ServiceBookingParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceBooking_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceBooking_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]ServiceBookingParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }
                _serviceBookingBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _serviceBookingBL.Create(data);

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
        /// Create Realtime Service Booking
        /// </summary>
        /// <param name="data">Realtime Service Booking Data</param>
        /// <remarks>Create Realtime Service Booking</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Realtime/Create")]
        [SwaggerRequestExample(typeof(ServiceBookingRealtimeParameterDto), typeof(APICreateRealtimeServiceBookingExample))]
        [ResponseType(typeof(ResponseBase<ServiceBookingRealtimeDto>)), HttpPost]
        [JsonValueValidation(typeof(ServiceBookingRealtimeParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceBooking_Realtime_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceBooking_Realtime_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult RealtimeCreate([FromBody]ServiceBookingRealtimeParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceBookingBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _serviceBookingBL.RealtimeCreate(data);

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
        /// Update Service Booking
        /// </summary>
        /// <param name="data">Service Booking Update Info</param>
        /// <remarks>Update Stall Master </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(ServiceBookingUpdateParameterDto), typeof(APIUpdateServiceBookingExample))]
        [ResponseType(typeof(ResponseBase<ServiceBookingDto>)), HttpPost]
        [JsonValueValidation(typeof(ServiceBookingUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceBooking_Update_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceBooking_Update)]
        public IHttpActionResult Update([FromBody]ServiceBookingUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceBookingBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _serviceBookingBL.Update(data);

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
        /// Update Realtime Service Booking
        /// </summary>
        /// <param name="data">Realtime Service Booking Update Info</param>
        /// <remarks>Update Realtime Service Booking </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Realtime/Update")]
        [SwaggerRequestExample(typeof(ServiceBookingRealtimeParameterDto), typeof(APIUpdateRealtimeServiceBookingExample))]
        [ResponseType(typeof(ResponseBase<ServiceBookingRealtimeDto>)), HttpPost]
        [JsonValueValidation(typeof(ServiceBookingRealtimeParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceBooking_Realtime_Update_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceBooking_Realtime_Update)]
        public IHttpActionResult RealtimeUpdate([FromBody]ServiceBookingRealtimeParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceBookingBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _serviceBookingBL.RealtimeUpdate(data);

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
        /// Service Booking Realtime Estimation Cost
        /// </summary>
        /// <param name="data">Service Booking Realtime Estimation Cost Info</param>
        /// <remarks>Service Booking Realtime Estimation Cost </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Realtime/EstimationCost")]
        [SwaggerRequestExample(typeof(VWI_ServiceCostEstimationParameterDto), typeof(APIServiceBookingEstimationCostExample))]
        [ResponseType(typeof(ResponseBase<VWI_ServiceCostEstimationDto>)), HttpPost]
        [JsonValueValidation(typeof(VWI_ServiceCostEstimationParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceBooking_Realtime_EstimationCost_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceBooking_Realtime_EstimationCost)]
        public IHttpActionResult EstimationCost([FromBody]VWI_ServiceCostEstimationParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceBookingBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _serviceBookingBL.EstimationCost(data);

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
        /// Service Booking Realtime Dealer Suggestion
        /// </summary>
        /// <param name="data">Service Booking Realtime Dealer Suggestion Info</param>
        /// <remarks>Service Booking Realtime Dealer Suggestion </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Realtime/DealerSuggestion")]
        [SwaggerRequestExample(typeof(DealerSuggestionServiceParameterDto), typeof(APIDealerSuggestionServiceExample))]
        [ResponseType(typeof(ResponseBase<DealerSuggestionServiceDto>)), HttpPost]
        [JsonValueValidation(typeof(DealerSuggestionServiceParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceBooking_Realtime_DealerSuggestion_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceBooking_Realtime_DealerSuggestion)]
        public async Task<IHttpActionResult> DealerSuggestion([FromBody]DealerSuggestionServiceParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceBookingBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = await _serviceBookingBL.DealerSuggestion(data);

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
        /// Service Booking Realtime Get Service Type
        /// </summary>
        /// <param name="data">Service Booking Realtime Get Service Type Info</param>
        /// <remarks>Service Booking Realtime Get Service Type </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Realtime/GetServiceType")]
        [SwaggerRequestExample(typeof(GetServiceTypeParameterDto), typeof(APIGetServiceTypeExample))]
        [ResponseType(typeof(ResponseBase<GetServiceTypeDto>)), HttpPost]
        [JsonValueValidation(typeof(GetServiceTypeParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceBooking_Realtime_GetServiceType_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceBooking_Realtime_GetServiceType)]
        public IHttpActionResult GetServiceType([FromBody]GetServiceTypeParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceBookingBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result =  _serviceBookingBL.GetServiceType(data);

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
        /// Service Booking Realtime Service Recommendation
        /// </summary>
        /// <param name="data">Service Booking Realtime Service Recommendation Info</param>
        /// <remarks>Service Booking Realtime Service Recommendation </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Realtime/ServiceRecommendation")]
        [SwaggerRequestExample(typeof(ServiceRecommendationParameterDto), typeof(APIServiceRecommendationExample))]
        [ResponseType(typeof(ResponseBase<ServiceRecommendationDto>)), HttpPost]
        [JsonValueValidation(typeof(ServiceRecommendationParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceBooking_Realtime_ServiceRecommendation_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceBooking_Realtime_ServiceRecommendation)]
        public IHttpActionResult ServiceRecommendation([FromBody]ServiceRecommendationParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceBookingBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _serviceBookingBL.ServiceRecommendation(data);

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
        /// Read Service Booking by Criteria
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
        [Route("RealtimeAll/Read")]
        [SwaggerRequestExample(typeof(ServiceBookingFilterDto), typeof(APIReadEmptyFilterExample))]
        [ResponseType(typeof(ResponseBase<List<ServiceBookingRealtimeReadDto>>)), HttpPost]
        [JsonValueValidation(typeof(ServiceBookingFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceBooking_RealtimeAll_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceBooking_RealtimeAll_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult RealtimeAll([FromBody]ServiceBookingFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceBookingBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _serviceBookingBL.RealtimeAll(data, PageSize);

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