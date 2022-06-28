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
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    [RoutePrefix("FinishUnit/DSFLeasingClaimDAPP"), ApiGroup("Finish Unit")]
    public class DSFLeasingClaimDAPPController : BaseController
    {
        private readonly IDSFLeasingClaimBL _uploadLeasingClaimBL;
        private readonly JsonMediaTypeFormatter _json;

        public DSFLeasingClaimDAPPController(IDSFLeasingClaimBL uploadLeasingClaimBL)
        {
            _uploadLeasingClaimBL = uploadLeasingClaimBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        //[Route("Read")]
        //[SwaggerRequestExample(typeof(DSFLeasingClaimDAPPFilterDto), typeof(APIReadExample))]
        //[ResponseType(typeof(ResponseBase<List<DSFLeasingClaimDAPPDto>>)), HttpPost]
        //[JsonValueValidation(typeof(DSFLeasingClaimDAPPFilterDto))]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_FinishUnit_LeasingDAPP_Read)]
        //[ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_FinishUnit_LeasingDAPP_Read_URL)]
        //[ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        //public IHttpActionResult Read([FromBody]DSFLeasingClaimDAPPFilterDto data)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return InvalidModelState(_json);

        //        _uploadLeasingClaimBL.Initialize(UserName, DealerCode);

        //        var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
        //        var result = _uploadLeasingClaimBL.ReadLeasingDAPP(data, PageSize);

        //        runtimeLog.FinishedTime = DateTime.Now;
        //        LogTransactionRuntime(runtimeLog, result.success, result.success);

        //        if (result.success)
        //            return Json(result);
        //        else
        //            return Content(GetHttpCodeMsg(result.messages), result, _json);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(ex);

        //        return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
        //    }
        //}

        [Route("Read")]
        [HttpPost]
        [SwaggerRequestExample(typeof(DSFLeasingClaimDAPPParameter), typeof(APIReadExample))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_FinishUnit_LeasingDAPP_Read)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_FinishUnit_LeasingDAPP_Read_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]DSFLeasingClaimDAPPParameter param)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _uploadLeasingClaimBL.Initialize(UserName, DealerCode);
                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                if (String.IsNullOrEmpty(param.Status))
                    return Content(HttpStatusCode.BadRequest, "Parameter Status not found", _json);
                if (String.IsNullOrEmpty(param.SourceData))
                    return Content(HttpStatusCode.BadRequest, "Parameter SourceData not found", _json);
                if (String.IsNullOrEmpty(param.LastUpdateTime))
                    return Content(HttpStatusCode.BadRequest, "Parameter LastUpdateTime not found", _json);

                var result = _uploadLeasingClaimBL.ReadLeasingDAPP(param.Status, param.SourceData, param.LastUpdateTime);

                runtimeLog.FinishedTime = DateTime.Now;
                LogTransactionRuntime(runtimeLog, result.success, result.success);

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