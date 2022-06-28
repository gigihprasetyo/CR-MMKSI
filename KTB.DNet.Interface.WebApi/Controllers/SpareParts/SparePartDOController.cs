#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDO Controller
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
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// SparePartDO Controller
    /// </summary>
    /// <summary>
    /// SparePartDO Web API
    /// </summary>
    [RoutePrefix("SparePart/PurchaseReceipt"), ApiGroup("Spare Part")]
    public class SparePartDOController : BaseController
    {
        private readonly ISparePartDOBL _sparepartdoBL;
        private readonly ISparePartDOExpeditionBL _sparePartExpeditionBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SparePartDO constructor with parameter
        /// </summary>
        /// <param name="sparepartdoBL"></param>
        /// <param name="sparePartExpeditionBL"></param>
        public SparePartDOController(ISparePartDOBL sparepartdoBL, ISparePartDOExpeditionBL sparePartExpeditionBL)
        {
            _sparepartdoBL = sparepartdoBL;
            _sparePartExpeditionBL = sparePartExpeditionBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Delete SparePartDO by ID
        /// </summary>
        /// <param name="id">sparepartdo ID</param>
        /// <remarks>Delete by id</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [ActionWebApiFilterAttribute]
        [Route("Delete")]
        [ResponseType(typeof(ResponseBase<SparePartDODto>)), HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _sparepartdoBL.Initialize(UserName, DealerCode);

                var result = _sparepartdoBL.Delete(id);

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
        /// Update Purchase Receipt
        /// </summary>
        /// <param name="data">Purchase Receipt Data</param>
        /// <remarks>Update  Purchase Receipt</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(SPDOExpeditionUpdateATAParameterDto), typeof(APIUpdatePurchaseReceiptExample))]
        [ResponseType(typeof(ResponseBase<SparePartDOExpeditionDto>)), HttpPost]
        [JsonValueValidation(typeof(SPDOExpeditionUpdateATAParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_PurchaseReceipt_Update_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_PurchaseReceipt_Update)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]SPDOExpeditionUpdateATAParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparePartExpeditionBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparePartExpeditionBL.UpdateATA(data);

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
        /// Get Purchase Receipt list by Criteria
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
        [Route("~/SparePart/PurchaseReceiptOri/Read")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [SwaggerRequestExample(typeof(SparePartDOFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<DeliveryOrderBillingResponseDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartDOFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_PurchaseReceipt_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_PurchaseReceipt_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [NonAction]
        public IHttpActionResult Read([FromBody]SparePartDOFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparepartdoBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparepartdoBL.ReadDeliveryOrderBilling(data, PageSize);

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
        /// Get Purchase Receipt list by Criteria
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
        //[Route("Read")]
        [SwaggerRequestExample(typeof(SparePartDOFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<DeliveryOrderBillingResponseDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartDOFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_PurchaseReceipt_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_PurchaseReceipt_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [NonAction]
        public IHttpActionResult Read1([FromBody]SparePartDOFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparepartdoBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparepartdoBL.ReadDeliveryOrderBilling1(data, PageSize);

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
        /// Get Purchase Receipt list by Criteria
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
        [SwaggerRequestExample(typeof(SparePartDOFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<DeliveryOrderBillingResponseDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartDOFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_PurchaseReceipt_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_PurchaseReceipt_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult ReadDeliveryOrderBillingWithCriteria([FromBody]SparePartDOFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparepartdoBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparepartdoBL.ReadDeliveryOrderBillingWithCriteria(data, PageSize);

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
        /// Get Purchase Receipt list by Criteria
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
        [Route("~/SparePart/PurchaseReceipt2/Read")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [SwaggerRequestExample(typeof(SparePartDOFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<DeliveryOrderBillingResponseDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartDOFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_PurchaseReceipt_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_PurchaseReceipt_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [NonAction]
        public IHttpActionResult Read2([FromBody]SparePartDOFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparepartdoBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparepartdoBL.ReadDeliveryOrderBilling2(data, PageSize);

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

