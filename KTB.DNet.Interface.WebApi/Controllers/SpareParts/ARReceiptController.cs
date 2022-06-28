#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ARReceipt Controller
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
    /// ARReceipt Controller
    /// </summary>
    /// <summary>
    /// ARReceipt Web API
    /// </summary>
    [RoutePrefix("SparePart/ARReceipt"), ApiGroup("Spare Part")]
    public class ARReceiptController : BaseController
    {
        private readonly IARReceiptBL _aRReceiptBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// ARReceipt constructor with parameter
        /// </summary>
        /// <param name="aRReceiptBL"></param>
        public ARReceiptController(IARReceiptBL aRReceiptBL)
        {
            _aRReceiptBL = aRReceiptBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create New ARReceipt
        /// </summary>
        /// <param name="data">ARReceipt Param</param>
        /// <remarks>Create ARReceipt</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(ARReceiptParameterDto), typeof(APIARReceiptExample))]
        [ResponseType(typeof(ResponseBase<ARReceiptDto>)), HttpPost]
        [JsonValueValidation(typeof(ARReceiptParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_ARReceipt_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_ARReceipt_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]ARReceiptParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _aRReceiptBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _aRReceiptBL.Create(data);

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

