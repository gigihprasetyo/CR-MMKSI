#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SalesmanLevel Controller
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

#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// SalesmanLevel Web API
    /// </summary>
    [RoutePrefix("Master/SalesmanLevel"), ApiGroup("Masters")]
    public class SalesmanLevelController : BaseController
    {
        private readonly ISalesmanLevelBL _salesmanLevelBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SalesmanArea constructor with parameter
        /// </summary>
        /// <param name="salesmanLevelBL"></param>
        public SalesmanLevelController(ISalesmanLevelBL salesmanLevelBL)
        {
            _salesmanLevelBL = salesmanLevelBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get SalesmanLevel list by Criteria
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
        [SwaggerRequestExample(typeof(FilterDtoBase), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SalesmanLevelDto>>)), HttpPost]
        [JsonValueValidation(typeof(FilterDtoBase))]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebAPI_Master_SalesmanLevel_Read)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_SalesmanLevel_Read_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]FilterDtoBase data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _salesmanLevelBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _salesmanLevelBL.Read(data, PageSize);

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
