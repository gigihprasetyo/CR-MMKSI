#region Namespace Imports
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Helper.Throttle;
using KTB.DNet.Interface.WebApi.Models.Examples;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Domain;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    [RoutePrefix("Services/MSPExMembership"), ApiGroup("Services")]
    public class MSPExMembershipController : BaseController
    {
        private readonly IVWI_MSPExMembershipBL _mspExMembershipBL;
        private readonly ILoggerService _log;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// MSP Extended Membershio Controller
        /// </summary>
        /// <param name="mspExMembershipBL"></param>
        /// <param name="loggerService"></param>
        public MSPExMembershipController(IVWI_MSPExMembershipBL mspExMembershipBL, ILoggerService loggerService)
        {
            _mspExMembershipBL = mspExMembershipBL;
            _log = loggerService;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Method Read for MSP Extended Membership
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
        [SwaggerRequestExample(typeof(VWI_MSPExMembershipFilterDto), typeof(APIReadMSPExMembershipExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_MSPExMembershipDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_MSPExMembershipFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_MSPExMembership_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_MSPExMembership_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]VWI_MSPExMembershipFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _mspExMembershipBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _mspExMembershipBL.Read(data, PageSize);

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