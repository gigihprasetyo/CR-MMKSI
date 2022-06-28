#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SAPCustomer Controller
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
    /// SAP Customer API Controller
    /// </summary>
    [RoutePrefix("VehicleSales/Lead"), ApiGroup("Vehicle Sales")]
    public class SAPCustomerController : BaseController
    {
        private readonly ISAPCustomerBL _sapCustomerBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SAP Customer constructor with parameter
        /// </summary>
        /// <param name="sapCustomerBL"></param>
        public SAPCustomerController(ISAPCustomerBL sapCustomerBL)
        {
            _sapCustomerBL = sapCustomerBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create Lead
        /// </summary>
        /// <param name="data">SAP Customer Data</param>
        /// <remarks>Create SAP Customer</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(SAPCustomerParameterDto), typeof(APICreateLeadExample))]
        [ResponseType(typeof(ResponseBase<SAPCustomerDto>)), HttpPost]
        [JsonValueValidation(typeof(SAPCustomerParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_Lead_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_Lead_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]SAPCustomerParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (!string.IsNullOrEmpty(data.DealerCode) && data.DealerCode.Trim().ToUpper() != DealerCode.Trim().ToUpper())
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                _sapCustomerBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sapCustomerBL.Create(data);

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
        /// Update Lead
        /// </summary>
        /// <param name="data">SAP Customer Data</param>
        /// <remarks>Update SAP Customer</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(SAPCustomerUpdateParameterDto), typeof(APIUpdateLeadExample))]
        [ResponseType(typeof(ResponseBase<SAPCustomerDto>)), HttpPost]
        [JsonValueValidation(typeof(SAPCustomerUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_Lead_Update_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_Lead_Update)]
        public IHttpActionResult Update([FromBody]SAPCustomerUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (!string.IsNullOrEmpty(data.DealerCode) && data.DealerCode.Trim().ToUpper() != DealerCode.Trim().ToUpper())
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                _sapCustomerBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sapCustomerBL.Update(data);

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
        /// Delete Lead by ID
        /// </summary>
        /// <param name="id">SAP Customer ID</param>
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
        [ResponseType(typeof(ResponseBase<SAPCustomerDto>)), HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _sapCustomerBL.Initialize(UserName, DealerCode);

                var result = _sapCustomerBL.Delete(id);

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
        /// Get Lead list by criteria
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
        [SwaggerRequestExample(typeof(SAPCustomerFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SAPCustomerDto>>)), HttpPost]
        [JsonValueValidation(typeof(SAPCustomerFilterDto))]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Read([FromBody]SAPCustomerFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sapCustomerBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _sapCustomerBL.Read(data, PageSize);

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
        /// Get Lead Sales Force list by Criteria
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
        [Route("LeadSalesForce/Read")]
        [SwaggerRequestExample(typeof(VWI_LeadCustomerSalesForceFilterDto), typeof(APIReadVWI_LeadCustomerSalesForceExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_LeadCustomerSalesForceDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_LeadCustomerSalesForceFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_LeadSalesForce_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_LeadCustomerSalesForce_Read)]
        public IHttpActionResult LeadSalesForce([FromBody]VWI_LeadCustomerSalesForceFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                // set credentials
                _sapCustomerBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _sapCustomerBL.GetLeadCustomerSalesForceDapper(data, PageSize);

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
