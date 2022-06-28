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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// CeilingUpdate Controller
    /// </summary>
    /// <summary>
    /// CeilingUpdate Web API
    /// </summary>    
    [RoutePrefix("FinishUnit/Ceilling"), ApiGroup("Finish Unit")]
    public class DSFCeilingUpdateController : BaseController
    {
        private readonly IDSFCeilingUpdateBL _sparePartPaymentBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// VWI_FinishUnitPayment constructor with parameter
        /// </summary>
        /// <param name="sparePartPaymentBL"></param>
        public DSFCeilingUpdateController(IDSFCeilingUpdateBL sparePartPaymentBL)
        {
            _sparePartPaymentBL = sparePartPaymentBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Update VWI_FinishUnitCeiling by Criteria
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
        [Route("Update")]
        [SwaggerRequestExample(typeof(DSFCeilingParameterDto), typeof(APIDSFCeilingSample))]
        [ResponseType(typeof(ResponseBase<DSFCeilingUpdateDto>)), HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebAPI_FinishUnit_Ceilling_Update)]
        [JsonValueValidation(typeof(DSFCeilingParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_FinishUnit_Ceilling_Update_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]DSFCeilingParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparePartPaymentBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparePartPaymentBL.Update(data);

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