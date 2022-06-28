#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerCase Controller
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
using KTB.DNet.Interface.WebApi.Models.Examples;
using Swashbuckle.Examples;
using System;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// CustomerCase Controller
    /// </summary>
    /// <summary>
    /// CustomerCase Web API
    /// </summary>    
    [RoutePrefix("Services/CustomerCase"), ApiGroup("Services")]
    public class CustomerCaseController : BaseController
    {
        private readonly ICustomerCaseBL _customerCaseBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// CustomerCase constructor with parameter
        /// </summary>
        ///<param name="customerCaseBL"></param>
        public CustomerCaseController(ICustomerCaseBL customerCaseBL)
        {
            _customerCaseBL = customerCaseBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create CustomerCase
        /// </summary>
        /// <param name="data">CustomerCase Data</param>
        /// <remarks>Create CustomerCase</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(CustomerCaseParameterDto), typeof(APICreateCustomerCaseExample))]
        [ResponseType(typeof(ResponseBase<CustomerCaseDto>)), HttpPost]
        [JsonValueValidation(typeof(CustomerCaseParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Services_CustomerCase_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Service_CustomerCase_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]CustomerCaseParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _customerCaseBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _customerCaseBL.Create(data);

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

