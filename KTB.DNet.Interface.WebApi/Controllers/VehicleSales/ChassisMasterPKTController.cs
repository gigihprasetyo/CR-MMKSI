#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterPKT Controller
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
using KTB.DNet.Interface.Model.Parameters;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Helper.Throttle;
using KTB.DNet.Interface.WebApi.Models;
using KTB.DNet.Interface.WebApi.Models.Examples;
using KTB.DNet.Interface.WebApi.Parameters;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
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
    /// Chassis Master PKT Controller
    /// </summary>
    /// <summary>
    /// PKTDate Web API
    /// </summary>
    [RoutePrefix("VehicleSales/ChassisMasterPKT"), ApiGroup("Vehicle Sales")]
    public class ChassisMasterPKTController : BaseController
    {
        private readonly IChassisMasterPKTBL _pktdateBL;
        private readonly IVWI_ChassisMasterPKTBL _pktBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Chassis Master PKT constructor with parameter
        /// </summary>
        /// <param name="pktdateBL"></param>
        /// <param name="pktBL"></param>
        public ChassisMasterPKTController(IChassisMasterPKTBL pktdateBL, IVWI_ChassisMasterPKTBL pktBL)
        {
            _pktdateBL = pktdateBL;
            _pktBL = pktBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create Chassis Master PKT
        /// </summary>
        /// <param name="data">PKT Date</param>
        /// /// <remarks>Create PKT Date</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(ChassisMasterPKTParameterDto), typeof(APICreateChassisMasterPKTExample))]
        [ResponseType(typeof(ResponseBase<ChassisMasterPKTDto>)), HttpPost]
        [JsonValueValidation(typeof(ChassisMasterPKTParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_ChassisMasterPKT_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_ChassisMasterPKT_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]ChassisMasterPKTParameterDto data)
        {
            if (!ModelState.IsValid)
                return InvalidModelState(_json);

            if (data.DealerCode.Trim().ToUpper() != DealerCode.Trim().ToUpper())
            {
                return InvalidDealerCode(_json, data.DealerCode);
            }

            try
            {
                _pktdateBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _pktdateBL.Create(data);

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
        /// Update Chassis Master
        /// </summary>
        /// <param name="data">PKT Date Data</param>
        /// /// <remarks>Update PKT Date</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(ChassisMasterPKTUpdateParameterDto), typeof(APIUpdateChassisMasterPKTExample))]
        [ResponseType(typeof(ResponseBase<ChassisMasterPKTDto>)), HttpPost]
        [JsonValueValidation(typeof(ChassisMasterPKTUpdateParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_ChassisMasterPKT_Update_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_ChassisMasterPKT_Update)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update([FromBody]ChassisMasterPKTUpdateParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode.Trim().ToUpper() != DealerCode.Trim().ToUpper())
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                _pktdateBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _pktdateBL.Update(data);

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
        /// Get Chassis Master PKT list by Criteria
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
        [SwaggerRequestExample(typeof(ChassisMasterPKTFilterDto), typeof(APIReadChassisMasterPKTExample))]
        [ResponseType(typeof(ResponseBase<List<ChassisMasterPKTDto>>)), HttpPost]
        [JsonValueValidation(typeof(ChassisMasterPKTFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VehicleSales_ChassisMasterPKT_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VehicleSales_ChassisMasterPKT_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[NonAction]
        public IHttpActionResult Read([FromBody]ChassisMasterPKTFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _pktBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                DatabaseSettings settings = (DatabaseSettings)ConfigurationManager.GetConfiguration("dataConfiguration");
                ConnectionStringData connString = settings.ConnectionStrings[Constants.ConnectionStringName.DNetConnection];
                string connectionString = AppConfigs.ConnectionString(
                                            connString.Parameters["server"].Value,
                                            connString.Parameters["database"].Value,
                                            connString.Parameters["uid"].Value,
                                            connString.Parameters["password"].Value
                                            );

                //var result = _pktdateBL.Read(data, PageSize);
                var result = _pktBL.ReadList(data, PageSize, ListDealer, DealerCompanyCode, connectionString);

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