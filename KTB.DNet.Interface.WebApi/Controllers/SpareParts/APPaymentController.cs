#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APPayment Controller
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
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// APPayment Controller
    /// </summary>
    /// <summary>
    /// APPayment Web API
    /// </summary>
    [RoutePrefix("SparePart/APPayment"), ApiGroup("Spare Part")]
    public class APPaymentController : BaseController
    {
        private readonly IAPPaymentBL _appaymentBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// APPayment constructor with parameter
        /// </summary>
        /// <param name="appaymentBL"></param>
        public APPaymentController(IAPPaymentBL appaymentBL)
        {
            _appaymentBL = appaymentBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create New APPayment
        /// </summary>
        /// <param name="data">APPayment Param</param>
        /// <remarks>Create APPayment</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(APPaymentParameterDto), typeof(APIAPPaymentExample))]
        [ResponseType(typeof(ResponseBase<APPaymentDto>)), HttpPost]
        [JsonValueValidation(typeof(APPaymentParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_APPayment_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_APPayment_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]APPaymentParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _appaymentBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _appaymentBL.Create(data);

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

