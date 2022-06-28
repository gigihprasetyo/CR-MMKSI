#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DMSWorkOrderWSCStatus Controller
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
    /// DMSWorkOrderWSCStatus
    /// </summary>
    [RoutePrefix("Services/DMSWorkOrderWSCStatus"), ApiGroup("Services")]
    public class DMSWorkOrderWSCStatusController : BaseController
    {
        private readonly IVWI_DMSWorkOrderWSCStatusBL _dmsWorkOrderWSCStatusBL;
        private readonly ILoggerService _log;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Vehicle sales open faktur controller
        /// </summary>
        /// <param name="dmsWorkOrderWSCStatusBL"></param>
        /// <param name="loggerService"></param>
        public DMSWorkOrderWSCStatusController(IVWI_DMSWorkOrderWSCStatusBL dmsWorkOrderWSCStatusBL, ILoggerService loggerService)
        {
            _dmsWorkOrderWSCStatusBL = dmsWorkOrderWSCStatusBL;
            _log = loggerService;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get Open Facture list by Criteria
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
        [SwaggerRequestExample(typeof(VWI_DMSWorkOrderWSCStatusFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_DMSWorkOrderWSCStatusDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_DMSWorkOrderWSCStatusFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_DMSWorkOrderWSCStatus_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_DMSWorkOrderWSCStatus_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]VWI_DMSWorkOrderWSCStatusFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _dmsWorkOrderWSCStatusBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _dmsWorkOrderWSCStatusBL.Read(data, PageSize);

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
