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
    /// VWI_MobileServiceReminder Controller
    /// </summary>
    /// <summary>
    /// VWI_MobileServiceReminder Web API
    /// </summary>    
    [RoutePrefix("Services/MobileServiceReminder"), ApiGroup("Services")]
    public class  MobileServiceReminderController : BaseController
    {
        private readonly IVWI_MobileServiceReminderBL _VWI_MobileServiceReminderBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// VWI_MobileServiceReminder constructor with parameter
        /// </summary>
        /// <param name="VWI_MobileServiceReminderBL"></param>
        public MobileServiceReminderController(IVWI_MobileServiceReminderBL VWI_MobileServiceReminderBL)
        {
            _VWI_MobileServiceReminderBL = VWI_MobileServiceReminderBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Read VWI_MobileServiceReminder by Criteria
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
        [SwaggerRequestExample(typeof(VWI_MobileServiceReminderFilterDto), typeof(APIReadEmptyFilterExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_MobileServiceReminderDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_MobileServiceReminderFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Service_MobileServiceReminder_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_MobileServiceReminder_Read)]
        //[ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]VWI_MobileServiceReminderFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _VWI_MobileServiceReminderBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _VWI_MobileServiceReminderBL.Read(data, 100);

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