#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EmployeeService Controller
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
    /// Employee Service Controller master Data
    /// </summary>
    [RoutePrefix("Master/EmployeeService"), ApiGroup("Masters")]
    public class EmployeeServiceController : BaseController
    {
        private readonly ITrTraineeBL _trtraineeBL;
        private readonly IVWI_EmployeeServiceBL _employeeServiceBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Employee Service constructor
        /// </summary>
        /// <param name="trtraineeBL"></param>
        /// <param name="employeeServiceBL"></param>
        public EmployeeServiceController(ITrTraineeBL trtraineeBL, IVWI_EmployeeServiceBL employeeServiceBL)
        {
            _trtraineeBL = trtraineeBL;
            _employeeServiceBL = employeeServiceBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create Employee Service
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
        [SwaggerRequestExample(typeof(TrTraineeParameterDto), typeof(APICreateEmployeeServiceExample))]
        [ResponseType(typeof(ResponseBase<TrTraineeDto>)), HttpPost]
        [JsonValueValidation(typeof(TrTraineeParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_EmployeeService_Create)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_EmployeeService_Create_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]TrTraineeParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode.Trim().ToUpper() != DealerCode.Trim().ToUpper())
                    return InvalidDealerCode(_json, data.DealerCode);

                _trtraineeBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _trtraineeBL.Create(data);

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
        /// Update Employee Service
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
        [SwaggerRequestExample(typeof(TrTraineeUpdateParameterDto), typeof(APIUpdateEmployeeServiceExample))]
        [ResponseType(typeof(ResponseBase<TrTraineeDto>)), HttpPost]
        [JsonValueValidation(typeof(TrTraineeUpdateParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_EmployeeService_Update)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_EmployeeService_Update_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]TrTraineeUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                //change validation to dealergroup in business logic
                //if (data.DealerCode.Trim().ToUpper() != DealerCode.Trim().ToUpper())
                //    return InvalidDealerCode(_json, data.DealerCode);

                _trtraineeBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _trtraineeBL.Update(data);

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
        /// Filter Employee Service by criterias
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Read")]
        [SwaggerRequestExample(typeof(VWI_EmployeeServiceFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_EmployeeServiceDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_EmployeeServiceFilterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_EmployeeService_Read)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_EmployeeService_Read_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read(VWI_EmployeeServiceFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _employeeServiceBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _employeeServiceBL.ReadWithProfile(data, PageSize);

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

        /// <summary>Reads the resign.</summary>
        /// <param name="data">The data.</param>
        /// /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Resign/Read")]
        [SwaggerRequestExample(typeof(VWI_EmployeeResignFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_EmployeeServiceResignDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_EmployeeResignFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_EmployeeService_Resign_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_EmployeeService_Resign_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult ReadResign(VWI_EmployeeResignFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _employeeServiceBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _employeeServiceBL.ReadResignEmployee(data, PageSize);

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

        /// <summary>Reads the resign data.</summary>
        /// <param name="data">The data.</param>
        /// /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("ResignData/Read")]
        [SwaggerRequestExample(typeof(VWI_EmployeeServicesResignFilterDto), typeof(APIReadSalesmanIDResignExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_EmployeeServiceDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_EmployeeServicesResignFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_EmployeeService_ResignData_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_EmployeeService_ResignData_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult ReadResignData(VWI_EmployeeServicesResignFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _employeeServiceBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _employeeServiceBL.ReadDataResign(data.SalesmanID);

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
