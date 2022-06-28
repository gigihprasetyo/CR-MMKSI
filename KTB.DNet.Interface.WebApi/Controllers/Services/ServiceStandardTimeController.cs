
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
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// ServiceStandardTime Controller
    /// </summary>
    /// <summary>
    /// ServiceBooking Web API
    /// </summary>    
    [RoutePrefix("Services/ServiceStandardTime"), ApiGroup("Services")]
    public class ServiceStandardTimeController : BaseController
    {
        private readonly IServiceStandardTimeBL _serviceStandardTimeBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// ServiceStandardTimeController constructor with parameter
        /// </summary>
        /// <param name="serviceStandardTimeBL"></param>
        public ServiceStandardTimeController(IServiceStandardTimeBL serviceStandardTimeBL)
        {
            _serviceStandardTimeBL = serviceStandardTimeBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get Service Standard Time list by Criteria
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
        [SwaggerRequestExample(typeof(ServiceStandardTimeFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<ServiceStandardTimeDto>>)), HttpPost]
        [JsonValueValidation(typeof(ServiceStandardTimeFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceStandardTime_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceStandardTime_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody] ServiceStandardTimeFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceStandardTimeBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _serviceStandardTimeBL.Read(data, PageSize);

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