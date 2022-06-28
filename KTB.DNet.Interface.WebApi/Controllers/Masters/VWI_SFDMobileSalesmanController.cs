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
    /// VWI_SFDMobileSalesman Controller
    /// </summary>
    /// <summary>
    /// VWI_SFDMobileSalesman Web API
    /// </summary>    
    [RoutePrefix("Master/SFDMobileSalesman"), ApiGroup("Masters")]
    public class VWI_SFDMobileSalesmanController : BaseController
    {
        private readonly IVWI_SFDMobileSalesmanBL _VWI_SFDMobileSalesmanBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// VWI_SFDMobileSalesman constructor with parameter
        /// </summary>
        /// <param name="VWI_SFDMobileSalesmanBL"></param>
        public VWI_SFDMobileSalesmanController(IVWI_SFDMobileSalesmanBL VWI_SFDMobileSalesmanBL)
        {
            _VWI_SFDMobileSalesmanBL = VWI_SFDMobileSalesmanBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Read VWI_SFDMobileSalesman by Criteria
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
        [SwaggerRequestExample(typeof(VWI_SFDMobileSalesmanFilterDto), typeof(APIReadEmptyFilterExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_SFDMobileSalesmanDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_SFDMobileSalesmanFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_VWI_SFDMobileSalesman_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_SFDMobileSalesman_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]VWI_SFDMobileSalesmanFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _VWI_SFDMobileSalesmanBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _VWI_SFDMobileSalesmanBL.Read(data, PageSize);

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