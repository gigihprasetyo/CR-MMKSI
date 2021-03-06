#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EmployeeSales Controller
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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// Employee Sales Master data controller
    /// </summary>
    [RoutePrefix("DMSData/EmployeeSales"), ApiGroup("DMS Data")]
    public class DNet_EmployeeSalesController : BaseController
    {
        private readonly IVWI_EmployeeSalesBL _vwi_employeesalesBL;
        private readonly ISalesmanHeaderBL _salesmanHeaderBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// VWI_EmployeeSales controller constructor
        /// </summary>
        public DNet_EmployeeSalesController(IVWI_EmployeeSalesBL vwi_employeesalesBL, ISalesmanHeaderBL salesmanHeaderBL)
        {
            _vwi_employeesalesBL = vwi_employeesalesBL;
            _salesmanHeaderBL = salesmanHeaderBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Filter Employee sales by criterias
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("ResignData/Read")]
        [SwaggerRequestExample(typeof(VWI_EmployeeSalesResignFilterDto), typeof(APIReadSalesmanHeaderResignExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_EmployeeSalesDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_EmployeeSalesResignFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_EmployeeSales_ResignData_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_EmployeeSales_ResignData_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [EnableCors(origins: "*", headers: Constants.HttpRequestConstants.CORS_Headers, methods: "*")]
        public IHttpActionResult ReadResignData(VWI_EmployeeSalesResignFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _vwi_employeesalesBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _vwi_employeesalesBL.ReadDataResign(data.SalesmanCode, data.NoKTP);

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
