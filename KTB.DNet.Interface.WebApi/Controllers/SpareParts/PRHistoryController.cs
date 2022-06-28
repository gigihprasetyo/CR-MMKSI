#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PRHistory Controller
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
using KTB.DNet.Interface.WebApi.Models.Examples;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// PRHistory Controller
    /// </summary>
    /// <summary>
    /// APPayment Web API
    /// </summary>
    [RoutePrefix("SparePart/PRHistory"), ApiGroup("Spare Part")]
    public class PRHistoryController : BaseController
    {
        private readonly IVWI_PRHistoryBL _pRHistoryBL;
        private readonly IVWI_PRHistoryIndentStatusCancelBL _vwi_PRHistoryIndentStatusCancelBL;
        private readonly IVWI_PRHistoryPOStatusCancelBL _vwi_PRHistoryPOStatusCancelBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// PRHistory constructor with parameter
        /// </summary>
        /// <param name="pRHistoryBL"></param>
        /// <param name="vwi_PRHistoryIndentStatusCancelBL"></param>
        public PRHistoryController(IVWI_PRHistoryBL pRHistoryBL, IVWI_PRHistoryIndentStatusCancelBL vwi_PRHistoryIndentStatusCancelBL, IVWI_PRHistoryPOStatusCancelBL vwi_PRHistoryPOStatusCancelBL)
        {
            _pRHistoryBL = pRHistoryBL;
            _vwi_PRHistoryIndentStatusCancelBL = vwi_PRHistoryIndentStatusCancelBL;
            _vwi_PRHistoryPOStatusCancelBL = vwi_PRHistoryPOStatusCancelBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get PR History SO list by Criteria
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
        [Route("GetPRHistorySO")]
        [SwaggerRequestExample(typeof(VWI_PRHistorySOFilterDto), typeof(APIPRHistorySOExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_PRHistorySODto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_PRHistorySOFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_PRHistory_GetPRHistorySO_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_PRHistorySO_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult GetPRHistorySO([FromBody]VWI_PRHistorySOFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _pRHistoryBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                
                var result = _pRHistoryBL.GetPRHistorySO(data, PageSize);

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
        /// Get PR History DO list by Criteria
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
        [Route("GetPRHistoryDO")]
        [SwaggerRequestExample(typeof(VWI_PRHistoryDOFilterDto), typeof(APIPRHistoryDOExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_PRHistoryDODto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_PRHistoryDOFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_PRHistory_GetPRHistoryDO_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_PRHistoryDO_Read)]
        public IHttpActionResult GetPRHistoryDO([FromBody]VWI_PRHistoryDOFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _pRHistoryBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _pRHistoryBL.GetPRHistoryDO(data, PageSize);

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
        /// Get PR History Indent Status Cancel by Criteria
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("PRHistoryIndentStatusCancel")]
        [SwaggerRequestExample(typeof(VWI_PRHistoryIndentStatusCancelFilterDto), typeof(APIReadPRHistoryIndentStatusExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_PRHistoryIndentStatusCancelDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_PRHistoryIndentStatusCancelFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_PRHistory_PRHistoryIndentStatusCancel_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_PRHistoryIndentStatusCancel_Read)]
        public IHttpActionResult GetPRHistoryIndentStatus([FromBody] VWI_PRHistoryIndentStatusCancelFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vwi_PRHistoryIndentStatusCancelBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _vwi_PRHistoryIndentStatusCancelBL.Read(data, PageSize);

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
        /// Get PR History PO Status Cancel by Criteria
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("PRHistoryPOStatusCancel")]
        [SwaggerRequestExample(typeof(VWI_PRHistoryPOStatusCancelFilterDto), typeof(APIReadEmptyFilterExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_PRHistoryPOStatusCancelDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_PRHistoryPOStatusCancelFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_PRHistory_PRHistoryPOStatusCancel_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_PRHistoryPOStatusCancel_Read)]
        public IHttpActionResult GetPRHistoryPOStatus([FromBody] VWI_PRHistoryPOStatusCancelFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vwi_PRHistoryPOStatusCancelBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _vwi_PRHistoryPOStatusCancelBL.Read(data, PageSize);

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

