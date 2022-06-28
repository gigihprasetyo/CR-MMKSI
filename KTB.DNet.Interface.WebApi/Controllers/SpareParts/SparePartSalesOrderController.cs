#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartSalesOrder Controller
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
using Swashbuckle.Examples;
using System;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// SparePartSalesOrder Controller
    /// </summary>
    /// <summary>
    /// SparePartSalesOrder Web API
    /// </summary>
    [RoutePrefix("SparePart/SparePartSalesOrder"), ApiGroup("Spare Part")]
    public class SparePartSalesOrderController : BaseController
    {
        private readonly ISparePartSalesOrderBL _sparepartsalesorderBL;
        private readonly ILoggerService _log;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SparePartSalesOrder constructor with parameter
        /// </summary>
        /// <param name="sparepartsalesorderBL"></param>
        /// <param name="loggerService"></param>
        public SparePartSalesOrderController(ISparePartSalesOrderBL sparepartsalesorderBL, ILoggerService loggerService)
        {
            _sparepartsalesorderBL = sparepartsalesorderBL;
            _log = loggerService;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create New SparePartSalesOrder
        /// </summary>
        /// <param name="data">SparePartSalesOrder Param</param>
        /// <remarks>Create SparePartSalesOrder</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(SparePartSalesOrderParameterDto), typeof(APISparePartSalesOrderExample))]
        [ResponseType(typeof(ResponseBase<SparePartSalesOrderDto>)), HttpPost]
        [JsonValueValidation(typeof(SparePartSalesOrderParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartSalesOrder_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartSalesOrder_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]SparePartSalesOrderParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                foreach (SparePartSalesOrderDetailParameterDto i in data.SparePartSalesOrderDetails)
                {
                    if (i.KodeDealer != DealerCode)
                        return InvalidDealerCode(_json, i.KodeDealer);
                }

                _sparepartsalesorderBL.Initialize(GetUsername(data.ResendBy, data.LogId), UserName);

                data.DealerCode = DealerCode;

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparepartsalesorderBL.Create(data);

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

