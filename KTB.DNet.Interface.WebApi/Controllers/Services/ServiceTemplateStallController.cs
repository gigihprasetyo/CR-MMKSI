#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : Service Template Stall Controller
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2021 19:21:36
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
    [RoutePrefix("Services/ServiceTemplate"), ApiGroup("Services")]
    public class ServiceTemplateStallController : BaseController
    {
        private readonly IServiceTemplateStallBL _serviceTemplateBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceTemplateBL"></param>
        public ServiceTemplateStallController(IServiceTemplateStallBL serviceTemplateBL)
        {
            this._serviceTemplateBL = serviceTemplateBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get Service Template list by Criteria
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
        [SwaggerRequestExample(typeof(ServiceTemplateHeaderFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<ServiceTemplateHeaderDto>>)), HttpPost]
        [JsonValueValidation(typeof(ServiceTemplateHeaderFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_ServiceTemplate_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_ServiceTemplate_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]ServiceTemplateHeaderFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _serviceTemplateBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _serviceTemplateBL.ReadList(data, PageSize, ListDealer, DealerCompanyCode);

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
