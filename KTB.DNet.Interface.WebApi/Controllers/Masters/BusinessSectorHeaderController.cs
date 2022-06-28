#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BusinessSectorHeader Controller
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
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// Business Sector Header API
    /// </summary>    
    [RoutePrefix("Master/BusinessSectorHeader"), ApiGroup("Masters")]
    public class BusinessSectorHeaderController : BaseController
    {
        private readonly IBusinessSectorHeaderBL _businessSectorHeaderBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="businessSectorHeaderBL"></param>
        public BusinessSectorHeaderController(IBusinessSectorHeaderBL businessSectorHeaderBL)
        {
            _businessSectorHeaderBL = businessSectorHeaderBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get BusinessSectorHeader list by Criteria
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
        [SwaggerRequestExample(typeof(BusinessSectorHeaderFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<BusinessSectorHeaderDto>>)), HttpPost]
        [JsonValueValidation(typeof(BusinessSectorHeaderFilterDto))]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_BusinessSectorHeader_Read)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_BusinessSectorHeader_Read_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        [NonAction]
        public IHttpActionResult Read([FromBody]BusinessSectorHeaderFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _businessSectorHeaderBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _businessSectorHeaderBL.Read(data, PageSize);

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
