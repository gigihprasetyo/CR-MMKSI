#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : CRM_xts_outsourceworkshopconfiguration Controller
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/28/2020 08:34:00
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
    [RoutePrefix("DMSData/outsourceworkshopconfiguration"), ApiGroup("DMS Data")]
    public class VWI_CRM_xts_outsourceworkshopconfigurationController : BaseController
    {
        private readonly IVWI_CRM_xts_outsourceworkshopconfigurationBL _vWI_CRM_xts_outsourceworkshopconfigurationBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vWI_CRM_xts_outsourceworkshopconfigurationBL"></param>
        public VWI_CRM_xts_outsourceworkshopconfigurationController(IVWI_CRM_xts_outsourceworkshopconfigurationBL vWI_CRM_xts_outsourceworkshopconfigurationBL)
        {
            this._vWI_CRM_xts_outsourceworkshopconfigurationBL = vWI_CRM_xts_outsourceworkshopconfigurationBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get outsourceworkshopconfiguration list by Criteria
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
        [SwaggerRequestExample(typeof(VWI_CRM_xts_outsourceworkshopconfigurationFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_CRM_xts_outsourceworkshopconfigurationDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_CRM_xts_outsourceworkshopconfigurationFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VWI_CRM_xts_outsourceworkshopconfiguration_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VWI_CRM_xts_outsourceworkshopconfiguration_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]VWI_CRM_xts_outsourceworkshopconfigurationFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vWI_CRM_xts_outsourceworkshopconfigurationBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _vWI_CRM_xts_outsourceworkshopconfigurationBL.ReadList(data, PageSize, ListDealer, DealerCompanyCode);

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