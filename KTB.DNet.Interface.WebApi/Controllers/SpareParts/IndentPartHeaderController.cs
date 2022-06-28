#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : IndentPartHeader Controller
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
using KTB.DNet.Interface.WebApi.Models;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// IndentPartHeader Controller
    /// </summary>
    /// <summary>
    /// IndentPartHeader Web API
    /// </summary>
    [RoutePrefix("SparePart/IndentPart"), ApiGroup("Spare Part")]
    public class IndentPartHeaderController : BaseController
    {
        private readonly IIndentPartHeaderBL _indentpartheaderBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// IndentPartHeader constructor with parameter
        /// </summary>
        /// <param name="indentpartheaderBL"></param>
        public IndentPartHeaderController(IIndentPartHeaderBL indentpartheaderBL)
        {
            _indentpartheaderBL = indentpartheaderBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get Indent Part Header list by Criteria
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
        [SwaggerRequestExample(typeof(IndentPartHeaderFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<IndentPartHeaderDto>>)), HttpPost]
        [JsonValueValidation(typeof(IndentPartHeaderFilterDto))]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Read([FromBody]IndentPartHeaderFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _indentpartheaderBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _indentpartheaderBL.Read(data, PageSize);

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
        /// Create New Indent Part
        /// </summary>
        /// <param name="data">Indent Part Header Param</param>
        /// <remarks>Create Indent Part</remarks>
        /// <returns>Response Base Indent Part response</returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(IndentPartHeaderCreateParameterDto), typeof(APICreateIndentPartHeaderExample))]
        [ResponseType(typeof(ResponseBase<IndentPartHeaderCreateResponseDto>)), HttpPost]
        [JsonValueValidation(typeof(IndentPartHeaderCreateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_IndentPart_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_IndentPart_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]IndentPartHeaderCreateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                _indentpartheaderBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _indentpartheaderBL.Create(data);

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
        /// Update Indent Part
        /// </summary>
        /// <param name="data">Indent Part Data</param>
        /// <remarks>Update Indent Part</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        //[Route("Update")]
        [SwaggerRequestExample(typeof(IndentPartHeaderUpdateParameterDto), typeof(APIUpdateIndentPartHeaderExample))]
        [ResponseType(typeof(ResponseBase<IndentPartHeaderParameterDto>)), HttpPost]
        [JsonValueValidation(typeof(IndentPartHeaderUpdateParameterDto))]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Update([FromBody]IndentPartHeaderUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                _indentpartheaderBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _indentpartheaderBL.Update(data);

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

