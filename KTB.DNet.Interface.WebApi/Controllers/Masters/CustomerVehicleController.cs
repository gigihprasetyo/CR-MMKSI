#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerVehicle Controller
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
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// CustomerVehicle Controller
    /// </summary>
    /// <summary>
    /// CustomerVehicle Web API
    /// </summary>    
    [RoutePrefix("Master/CustomerVehicle"), ApiGroup("Masters")]
    public class CustomerVehicleController : BaseController
    {
        private readonly IVWI_CustomerVehicleBL _vwiCustomerVehicleBL;
        private readonly ICustomerVehicleBL _customerVehicleBL;
        private readonly JsonMediaTypeFormatter _json;

        public CustomerVehicleController()
        {

        }

        /// <summary>
        /// CustomerVehicle constructor with parameter
        /// </summary>
        /// <param name="vWI_provinceBL"></param>
        /// <param name="customerVehicleBL"></param>
        public CustomerVehicleController(IVWI_CustomerVehicleBL vWI_provinceBL, ICustomerVehicleBL customerVehicleBL)
        {
            _vwiCustomerVehicleBL = vWI_provinceBL;
            _customerVehicleBL = customerVehicleBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get CustomerVehicle list by Criteria
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
        [SwaggerRequestExample(typeof(VWI_CustomerVehicleFilterDto), typeof(APIReadCustomerVehicleExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_CustomerVehicleDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_CustomerVehicleFilterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_CustomerVehicle_Read)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_CustomerVehicle_Read_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]VWI_CustomerVehicleFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vwiCustomerVehicleBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _vwiCustomerVehicleBL.Read(data, PageSize);

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
        /// Create customer vehicle
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(CustomerVehicleParameterDto), typeof(APICreateCustomerVehicleExample))]
        [ResponseType(typeof(ResponseBase<CustomerVehicleDto>)), HttpPost]
        [JsonValueValidation(typeof(CustomerVehicleParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_CustomerVehicle_Create)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_CustomerVehicle_Create_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]CustomerVehicleParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode.Trim().ToUpper() != DealerCode.Trim().ToUpper())
                    return InvalidDealerCode(_json, data.DealerCode);

                _customerVehicleBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _customerVehicleBL.Create(data);

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
        /// Update customer vehicle
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(CustomerVehicleUpdateParameterDto), typeof(APIUpdateCustomerVehicleExample))]
        [ResponseType(typeof(ResponseBase<CustomerVehicleDto>)), HttpPost]
        [JsonValueValidation(typeof(CustomerVehicleUpdateParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_CustomerVehicle_Update)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_CustomerVehicle_Update_URL)]
        public IHttpActionResult Update([FromBody]CustomerVehicleUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode.Trim().ToUpper() != DealerCode.Trim().ToUpper())
                    return InvalidDealerCode(_json, data.DealerCode);

                _customerVehicleBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _customerVehicleBL.Update(data);

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
        /// Upload customer vehicle image
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("UploadImage")]
        [SwaggerRequestExample(typeof(CustomerUploadImageParameterDto), typeof(APICustomerVehicleUploadImageExample))]
        [ResponseType(typeof(ResponseBase<CustomerVehicleUploadImageDto>)), HttpPost]
        [JsonValueValidation(typeof(CustomerUploadImageParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_CustomerVehicle_UploadImage)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_Customervehicle_UploadImage_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult UploadImage([FromBody]CustomerUploadImageParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode.Trim().ToUpper() != DealerCode.Trim().ToUpper())
                    return InvalidDealerCode(_json, data.DealerCode);

                _customerVehicleBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _customerVehicleBL.UploadImage(data);

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