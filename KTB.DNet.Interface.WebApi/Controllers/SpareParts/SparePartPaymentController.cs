#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPayment Controller
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 3/10/2018 12:26
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
    /// VWI_SparePartPayment Controller
    /// </summary>
    /// <summary>
    /// VWI_SparePartPayment Web API
    /// </summary>    
    [RoutePrefix("SparePart/SparePartPayment"), ApiGroup("Spare Part")]
    public class SparePartPaymentController : BaseController
    {
        private readonly IVWI_SparePartPaymentBL _sparePartPaymentBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// VWI_SparePartPayment constructor with parameter
        /// </summary>
        /// <param name="sparePartPaymentBL"></param>
        public SparePartPaymentController(IVWI_SparePartPaymentBL sparePartPaymentBL)
        {
            _sparePartPaymentBL = sparePartPaymentBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Read VWI_SparePartPayment by Criteria
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
        [SwaggerRequestExample(typeof(VWI_SparePartPaymentFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_SparePartPaymentDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_SparePartPaymentFilterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartPayment_Read)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartPayment_Read_URL)]
        public IHttpActionResult Read([FromBody]VWI_SparePartPaymentFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparePartPaymentBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _sparePartPaymentBL.Read(data, PageSize);

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

