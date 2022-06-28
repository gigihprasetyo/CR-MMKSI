#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartSales Controller
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
    /// Assist Part Sales API Controller
    /// </summary>        
    [RoutePrefix("Sparepart/AssistPartSales"), ApiGroup("Spare Part")]
    public class AssistPartSalesController : BaseController
    {
        private readonly IAssistPartSalesBL _assistPartSalesBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Assist Part Sales Constructor with parameter
        /// </summary>
        /// <param name="assistPartSalesBL"></param>
        public AssistPartSalesController(IAssistPartSalesBL assistPartSalesBL)
        {
            _assistPartSalesBL = assistPartSalesBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create Assist Part Sales
        /// </summary>
        /// <param name="data">Assist Part Sales Data</param>
        /// <remarks>Create Assist Part Sales</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns> 
        [Route("Create")]
        [SwaggerRequestExample(typeof(AssistPartSalesParameterDto), typeof(APICreateAssistPartSalesExample))]
        [ResponseType(typeof(ResponseBase<AssistPartSalesDto>)), HttpPost]
        [JsonValueValidation(typeof(AssistPartSalesParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_AssistPartSales_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_AssistPartSales_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]AssistPartSalesParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                    return InvalidDealerCode(_json, data.DealerCode);

                _assistPartSalesBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _assistPartSalesBL.Create(data);

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
        /// Create List Assist Part Sales
        /// </summary>
        /// <param name="data">List Assist Part Sales Data</param>
        /// <remarks>Create List Assist Part Sales</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns> 
        [Route("CreateList")]
        [SwaggerRequestExample(typeof(AssistPartSalesParameterDto), typeof(APICreateAssistPartSalesExample))]
        [ResponseType(typeof(ResponseBase<List<AssistPartSalesDto>>)), HttpPost]
        [JsonValueValidation(typeof(List<AssistPartSalesParameterDto>))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_AssistPartSales_CreateList_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_AssistPartSales_CreateList)]
        public IHttpActionResult CreateList([FromBody]List<AssistPartSalesParameterDto> data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                foreach (var item in data)
                {
                    if (item.DealerCode != DealerCode)
                    {
                        return InvalidDealerCode(_json, item.DealerCode);
                    }
                }

                _assistPartSalesBL.Initialize(GetUsername(data[0].ResendBy, data[0].LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _assistPartSalesBL.Create(data);

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
        /// Create List Assist Part Stock
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>Create List Assist Part Stock</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("CreateListAsync")]
        [SwaggerRequestExample(typeof(AssistPartSalesParameterDto), typeof(APICreateAssistPartSalesExample))]
        [ResponseType(typeof(ResponseBase<List<AssistPartSalesDto>>)), HttpPost]
        [JsonValueValidation(typeof(List<AssistPartSalesParameterDto>))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_AssistPartSales_CreateListAsync_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_AssistPartSales_CreateList)]
        [NonAction]
        public async Task<IHttpActionResult> CreateListAsync([FromBody]List<AssistPartSalesParameterDto> data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                foreach (var item in data)
                {
                    if (item.DealerCode != DealerCode)
                    {
                        return InvalidDealerCode(_json, item.DealerCode);
                    }
                }

                _assistPartSalesBL.Initialize(GetUsername(data[0].ResendBy, data[0].LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = await _assistPartSalesBL.BulkCreateAsync(data);

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
        /// Update AssistPartSales entity
        /// </summary>
        /// <param name="data">AssistPartSales Parameter </param>
        /// <remarks>Update 'AssistPartSales' entity</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(AssistPartSalesUpdateParameterDto), typeof(APIUpdateAssistPartSalesExample))]
        [ResponseType(typeof(ResponseBase<AssistPartSalesDto>)), HttpPost]
        [JsonValueValidation(typeof(AssistPartSalesParameterDto))]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Update([FromBody]AssistPartSalesUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                // set credentials
                _assistPartSalesBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _assistPartSalesBL.Update(data);

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
        /// Delete Assist Part Sales by ID
        /// </summary>
        /// <param name="id">Assist Part Sales ID</param>
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
        [ResponseType(typeof(ResponseBase<AssistPartSalesDto>)), HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _assistPartSalesBL.Initialize(UserName, DealerCode);

                var result = _assistPartSalesBL.Delete(id);

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
        /// Get Assist Part Sales list by Criteria
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
        [SwaggerRequestExample(typeof(AssistPartSalesFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<AssistPartSalesReadDto>>)), HttpPost]
        [JsonValueValidation(typeof(AssistPartSalesFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_AssistPartSales_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_AssistPartSales_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]AssistPartSalesFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _assistPartSalesBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _assistPartSalesBL.ReadData(data, PageSize);

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
