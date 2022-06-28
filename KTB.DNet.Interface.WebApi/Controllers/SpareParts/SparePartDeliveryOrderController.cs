#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDeliveryOrder Controller
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
    /// SparePart Delivery Order API Controller
    /// </summary>    
    [RoutePrefix("SparePart/DeliveryOrder"), ApiGroup("Spare Part")]
    public class SparePartDeliveryOrderController : BaseController
    {
        private readonly ISparePartDeliveryOrderBL _sparePartDeliveryOrderBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SparePart Delivery Order Constructor with parameter
        /// </summary>
        /// <param name="sparePartDeliveryOrderBL"></param>        
        public SparePartDeliveryOrderController(ISparePartDeliveryOrderBL sparePartDeliveryOrderBL)
        {
            _sparePartDeliveryOrderBL = sparePartDeliveryOrderBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// SparePart Delivery Order
        /// </summary>
        /// <param name="data">SparePart Delivery Order Data</param>
        /// <remarks>SparePart Delivery Order</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns> 
        [Route("Create")]
        [SwaggerRequestExample(typeof(SparePartDeliveryOrderParameterDto), typeof(APICreateSparePartDeliveryOrderExample))]
        [ResponseType(typeof(ResponseBase<SparePartDeliveryOrderDto>)), HttpPost]
        [JsonValueValidation(typeof(SparePartDeliveryOrderParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_DeliveryOrder_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_DeliveryOrder_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]SparePartDeliveryOrderParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparePartDeliveryOrderBL.Initialize(GetUsername(data.ResendBy, data.LogId), UserName);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparePartDeliveryOrderBL.Create(data);

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
        /// Update SparePart Delivery Order
        /// </summary>
        /// <param name="data">SparePart Delivery Order Data</param>
        /// <remarks>Update SparePart Delivery Order</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        //[SwaggerRequestExample(typeof(SparePartDeliveryOrderParameterDto), typeof(APICreateSparePartDeliveryOrderExample))]
        [ResponseType(typeof(ResponseBase<SparePartDeliveryOrderDto>)), HttpPost]
        [JsonValueValidation(typeof(SparePartDeliveryOrderParameterDto))]
        [NonAction]
        public IHttpActionResult Update([FromBody]SparePartDeliveryOrderParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparePartDeliveryOrderBL.Initialize(GetUsername(data.ResendBy, data.LogId), UserName);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparePartDeliveryOrderBL.Update(data);

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
        /// Delete SparePart Delivery Order by ID
        /// </summary>
        /// <param name="id">SparePart Delivery Order ID</param>
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
        [ResponseType(typeof(ResponseBase<SparePartDeliveryOrderDto>)), HttpDelete]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _sparePartDeliveryOrderBL.Delete(id);

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
        /// Get SparePart Delivery Order list by Criteria
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
        [SwaggerRequestExample(typeof(SparePartDeliveryOrderFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SparePartDeliveryOrderDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartDeliveryOrderFilterDto))]
        [NonAction]
        public IHttpActionResult Read([FromBody]SparePartDeliveryOrderFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _sparePartDeliveryOrderBL.Read(data, PageSize);

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
