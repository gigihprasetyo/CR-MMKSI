#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKCustomer Controller
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
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using System;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// SPKCustomer Controller
    /// </summary>
    /// <summary>
    /// SPKCustomer Web API
    /// </summary>
    //[RoutePrefix("SPK/SPKCustomer"), ApiGroup("SPK")]    
    public class SPKCustomerController : BaseController
    {
        private readonly ISPKCustomerBL _spkCustomerBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SPKCustomer constructor with parameter
        /// </summary>
        /// <param name="spkcustomerBL"></param>
        public SPKCustomerController(ISPKCustomerBL spkcustomerBL)
        {
            _spkCustomerBL = spkcustomerBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create SPK Customer
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        //[Route("Create")]
        [ResponseType(typeof(ResponseBase<SPKCustomerDto>)), HttpPost]
        [JsonValueValidation(typeof(SPKCustomerParameterDto))]
        [NonAction]
        public IHttpActionResult Create([FromBody]SPKCustomerParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _spkCustomerBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _spkCustomerBL.Create(data);

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
        /// Update SPK Customer
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        //[Route("Update")]
        [ResponseType(typeof(ResponseBase<SPKCustomerDto>)), HttpPost]
        [JsonValueValidation(typeof(SPKCustomerParameterDto))]
        [NonAction]
        public IHttpActionResult Update([FromBody]SPKCustomerParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _spkCustomerBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _spkCustomerBL.Create(data);

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

