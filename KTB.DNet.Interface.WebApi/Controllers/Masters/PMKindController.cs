﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PMKind Controller
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
using KTB.DNet.Interface.WebApi.Models;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// PMKind Controller
    /// </summary>
    /// <summary>
    /// PMKind Web API
    /// </summary>    
    [RoutePrefix("Master/PMKind"), ApiGroup("Masters")]
    public class PMKindController : BaseController
    {
        private readonly IPMKindBL _pmKindBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// PMKind constructor with parameter
        /// </summary>
        /// <param name="pmKindBL"></param>
        public PMKindController(IPMKindBL pmKindBL)
        {
            _pmKindBL = pmKindBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get PMKind list by Criteria
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
        [SwaggerRequestExample(typeof(PMKindFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<PMKindDto>>)), HttpPost]
        [JsonValueValidation(typeof(PMKindFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_PMKind_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_PMKind_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]PMKindFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _pmKindBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _pmKindBL.Read(data, PageSize);

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
