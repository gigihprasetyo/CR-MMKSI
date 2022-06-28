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
    [RoutePrefix("FinishUnit/DSFLeasingClaim"), ApiGroup("Finish Unit")]
    public class DSFLeasingClaimController : BaseController
    {
        private readonly IDSFLeasingClaimBL _uploadLeasingClaimBL;
        private readonly JsonMediaTypeFormatter _json;

        public DSFLeasingClaimController(IDSFLeasingClaimBL uploadLeasingClaimBL)
        {
            _uploadLeasingClaimBL = uploadLeasingClaimBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        [Route("Create")]
        [SwaggerRequestExample(typeof(List<DSFLeasingClaimCreateParameter>), typeof(APIDSFLeasingClaimCreateSample))]
        [ResponseType(typeof(ResponseBase<List<DSFLeasingClaimCreateResponse>>)), HttpPost]
        [JsonValueValidation(typeof(List<DSFLeasingClaimCreateParameter>))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_FinishUnit_DSFLeasingClaim_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_FinishUnit_DSFLeasingClaim_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create(List<DSFLeasingClaimCreateParameter> data)
        {
            try
            {
                _uploadLeasingClaimBL.Initialize(GetUsername("SYSTEM", 0), DealerCode);
                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var dataForResponse = new List<DSFLeasingClaimCreateResponse>();
                bool result = _uploadLeasingClaimBL.Insert(data, out dataForResponse);
                runtimeLog.FinishedTime = DateTime.Now;
                LogTransactionRuntime(runtimeLog, result, result);
                return Json(dataForResponse);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
            }
        }

        [Route("Update")]
        [SwaggerRequestExample(typeof(List<ResubmitClaimParamater>), typeof(ResubmitClaimSample))]
        [ResponseType(typeof(ResponseBase<List<ResubmitClaimResponse>>)), HttpPost]
        [JsonValueValidation(typeof(List<ResubmitClaimParamater>))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_FinishUnit_DSFLeasingClaim_Update)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_FinishUnit_DSFLeasingClaim_Update_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Update(List<ResubmitClaimParamater> data)
        {
            try
            {
                _uploadLeasingClaimBL.Initialize(UserName, DealerCode);
                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var dataForResponse = new List<ResubmitClaimResponse>();
                bool result = _uploadLeasingClaimBL.Update(data, out dataForResponse);
                runtimeLog.FinishedTime = DateTime.Now;
                LogTransactionRuntime(runtimeLog, result, result);
                return Json(dataForResponse);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
            }
        }

        [Route("GetFile")]
        [HttpPost]
        [SwaggerRequestExample(typeof(DSFLeasingClaimGetFileParameter), typeof(APIReadExample))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_FinishUnit_DSFLeasingClaim_GetFile)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_FinishUnit_DSFLeasingClaim_GetFile_URL)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public HttpResponseMessage GetFile([FromBody] DSFLeasingClaimGetFileParameter param)
        {
            if (String.IsNullOrEmpty(param.regnumber) || String.IsNullOrEmpty(param.path))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            string fileName = string.Empty;
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(_uploadLeasingClaimBL.GetFile(param.regnumber, param.path, out fileName));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        #region backup
        //[Route("Create")]
        //[SwaggerRequestExample(typeof(DSFLeasingClaimCreateParameterDto), typeof(APIDSFLeasingClaimCreateSample))]
        //[ResponseType(typeof(ResponseBase<DSFLeasingClaimDto>)), HttpPost]
        //[JsonValueValidation(typeof(DSFLeasingClaimCreateParameterDto))]
        //[ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_FinishUnit_DSFLeasingClaim_Create_URL)]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_FinishUnit_DSFLeasingClaim_Create)]
        //[ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        //public IHttpActionResult Create([FromBody]DSFLeasingClaimCreateParameterDto data)
        //{
        //    if (!ModelState.IsValid)
        //        return InvalidModelState(_json);

        //    try
        //    {
        //        List<DSFLeasingClaimCreateParameterDto> dataFor = new List<DSFLeasingClaimCreateParameterDto>();
        //        dataFor.Add(data);

        //        _uploadLeasingClaimBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

        //        var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

        //        var result = new ResponseBase<DSFLeasingClaimDto>();
        //        foreach (DSFLeasingClaimCreateParameterDto obj in dataFor)
        //        {
        //            result = _uploadLeasingClaimBL.Create(obj);
        //        }


        //        runtimeLog.FinishedTime = DateTime.Now;

        //        LogTransactionRuntime(runtimeLog, result, result.success);

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
        #endregion


    }
}