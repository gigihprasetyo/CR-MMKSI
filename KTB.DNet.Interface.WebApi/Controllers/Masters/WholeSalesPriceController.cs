#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : WholeSalesPrice Controller
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
using KTB.DNet.Interface.WebApi.Models;
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
    /// WholeSalesPrice Controller
    /// </summary>
    /// <summary>
    /// WholeSalesPrice Web API
    /// </summary>    
    [RoutePrefix("Master/WholesalesPrice"), ApiGroup("Masters")]
    public class WholeSalesPriceController : BaseController
    {
        private readonly IVWI_WholeSalesPriceBL _wholeSalesPriceBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// WholeSalesPrice constructor with parameter
        /// </summary>
        /// <param name="wholeSalesPriceBL"></param>
        public WholeSalesPriceController(IVWI_WholeSalesPriceBL wholeSalesPriceBL)
        {
            _wholeSalesPriceBL = wholeSalesPriceBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Read WholeSalesPrice by Criteria
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
        [SwaggerRequestExample(typeof(VWI_WholeSalesPriceFilterDto), typeof(APIReadEmptyFilterExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_WholeSalesPriceDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_WholeSalesPriceFilterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_WholeSalesPrice_Read)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_WholeSalesPrice_Read_URL)]
        public IHttpActionResult Read([FromBody]VWI_WholeSalesPriceFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _wholeSalesPriceBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _wholeSalesPriceBL.Read(data, PageSize);

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

