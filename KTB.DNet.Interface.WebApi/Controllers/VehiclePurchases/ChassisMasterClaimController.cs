#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterClaim Controller
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 10/9/2020 15:13
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
    /// ChassisMasterClaim Controller
    /// </summary>
    /// <summary>
    /// ChassisMasterClaim Web API
    /// </summary>    
    [RoutePrefix("VehiclePurchase/ChassisMasterClaim"), ApiGroup("Vehicle Purchase")]
    public class ChassisMasterClaimController : BaseController
    {
        private readonly IChassisMasterClaimHeaderBL _chassisMasterClaimHeaderBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// ChassisMasterClaim constructor with parameter
        /// </summary>
        /// <param name="chassisMasterClaimHeaderBL"></param>
        public ChassisMasterClaimController(IChassisMasterClaimHeaderBL chassisMasterClaimHeaderBL)
        {
            _chassisMasterClaimHeaderBL = chassisMasterClaimHeaderBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create ChassisMasterClaim
        /// </summary>
        /// <param name="data">ChassisMasterClaim Data</param>
        /// <remarks>Create ChassisMasterClaim</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(ChassisMasterClaimHeaderParameterDto), typeof(APICreateChassisMasterClaimExample))]
        [ResponseType(typeof(ResponseBase<ChassisMastertClaimHeaderCreateResponseDto>)), HttpPost]
        [JsonValueValidation(typeof(ChassisMasterClaimHeaderParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehiclePurchase_ChassisMasterClaim_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehiclePurchase_ChassisMasterClaim_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]ChassisMasterClaimHeaderParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }
                 _chassisMasterClaimHeaderBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _chassisMasterClaimHeaderBL.Create(data);

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
        /// Get Chassis Master Claim
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
        [SwaggerRequestExample(typeof(ChassisMasterClaimHeaderFilterDto), typeof(APIReadChassisMasterClaimExample))]
        [ResponseType(typeof(ResponseBase<List<ChassisMastertClaimHeaderDto>>)), HttpPost]
        [JsonValueValidation(typeof(ChassisMasterClaimHeaderFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehiclePurchase_ChassisMasterClaim_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehiclePurchase_ChassisMasterClaim_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]ChassisMasterClaimHeaderFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _chassisMasterClaimHeaderBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _chassisMasterClaimHeaderBL.ReadData(data, PageSize);

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
        /// Update Chassis Master Claim 
        /// </summary>
        /// <param name="data">Chassis Master Claim Update Info</param>
        /// <remarks>Update Chassis Master Claim </remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(ChassisMasterClaimHeaderUpdateParameterDto), typeof(APIUpdateChassisMasterClaimExample))]
        [ResponseType(typeof(ResponseBase<ChassisMastertClaimHeaderUpdateResponseDto>)), HttpPost]
        [JsonValueValidation(typeof(ChassisMasterClaimHeaderUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehiclePurchase_ChassisMasterClaim_Update_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehiclePurchase_ChassisMasterClaim_Update)]
        public IHttpActionResult Update([FromBody]ChassisMasterClaimHeaderUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _chassisMasterClaimHeaderBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _chassisMasterClaimHeaderBL.Update(data);

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