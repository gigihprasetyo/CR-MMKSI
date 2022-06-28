#region Namespace Imports
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Helper.Throttle;
using KTB.DNet.Interface.WebApi.Models.Examples;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
using KTB.DNet.Interface.Framework.Enums;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers 
{
    [RoutePrefix("Master/SendASBSFID"), ApiGroup("Masters")]
    public class SendASBSFIDController : BaseController
    {
        private readonly ISendASBSFIDBL _sendASBSFIDBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Send ASB SFID constructor with parameter
        /// </summary>
        /// <param name="sendASBSFIDBL"></param>
        public SendASBSFIDController(ISendASBSFIDBL sendASBSFIDBL)
        {
            _sendASBSFIDBL = sendASBSFIDBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        [Route("SendCustomer")]
        [SwaggerRequestExample(typeof(SendASBSFIDParameterDto.CustomerSFIDParameterDto), typeof(APISendASBSFIDCustomerExample))]
        [ResponseType(typeof(ResponseBase<SendASBSFIDDto>)), HttpPost]
        [JsonValueValidation(typeof(SendASBSFIDParameterDto.CustomerSFIDParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_SendASBSFID_SendCustomer)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_SendASBSFID_SendCustomer)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody] SendASBSFIDParameterDto.CustomerSFIDParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sendASBSFIDBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sendASBSFIDBL.SendCustomer(data);

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

        [Route("SendSuspect")]
        [SwaggerRequestExample(typeof(SendASBSFIDParameterDto.SuspectSFIDParameterDto), typeof(APISendASBSFIDSuspectExample))]
        [ResponseType(typeof(ResponseBase<SendASBSFIDDto>)), HttpPost]
        [JsonValueValidation(typeof(SendASBSFIDParameterDto.SuspectSFIDParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_SendASBSFID_SendSuspect)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_SendASBSFID_SendSuspect)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)] 
        public IHttpActionResult SendSuspect([FromBody] SendASBSFIDParameterDto.SuspectSFIDParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sendASBSFIDBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sendASBSFIDBL.SendSuspect(data);

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

        [Route("SendSuspectContact")]
        [SwaggerRequestExample(typeof(SendASBSFIDParameterDto.SuspectContactSFIDParameterDto), typeof(APISendASBSFIDSuspectExample))]
        [ResponseType(typeof(ResponseBase<SendASBSFIDDto>)), HttpPost]
        [JsonValueValidation(typeof(SendASBSFIDParameterDto.SuspectContactSFIDParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_SendASBSFID_SendSuspectContact)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_SendASBSFID_SendSuspectContact)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult SendSuspectContact([FromBody] SendASBSFIDParameterDto.SuspectContactSFIDParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sendASBSFIDBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sendASBSFIDBL.SendSuspectContact(data);

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

        [Route("SendProspect")]
        [SwaggerRequestExample(typeof(SendASBSFIDParameterDto.SuspectSFIDParameterDto), typeof(APISendASBSFIDSuspectExample))]
        [ResponseType(typeof(ResponseBase<SendASBSFIDDto>)), HttpPost]
        [JsonValueValidation(typeof(SendASBSFIDParameterDto.SuspectSFIDParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_SendASBSFID_SendProspect)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_SendASBSFID_SendProspect)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult SendProspect([FromBody] SendASBSFIDParameterDto.SuspectSFIDParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sendASBSFIDBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sendASBSFIDBL.SendProspect(data);

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

        [Route("SendActivity")]
        [SwaggerRequestExample(typeof(SendASBSFIDParameterDto.ActivitySFIDParameterDto), typeof(APISendASBSFIDSuspectExample))]
        [ResponseType(typeof(ResponseBase<SendASBSFIDDto>)), HttpPost]
        [JsonValueValidation(typeof(SendASBSFIDParameterDto.ActivitySFIDParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_SendASBSFID_SendActivity)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_SendASBSFID_SendActivity)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult SendActivity([FromBody] SendASBSFIDParameterDto.ActivitySFIDParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sendASBSFIDBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sendASBSFIDBL.SendActivity(data);

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

        [Route("SendActivityContact")]
        [SwaggerRequestExample(typeof(SendASBSFIDParameterDto.ActivityContactSFIDParameterDto), typeof(APISendASBSFIDSuspectExample))]
        [ResponseType(typeof(ResponseBase<SendASBSFIDDto>)), HttpPost]
        [JsonValueValidation(typeof(SendASBSFIDParameterDto.ActivityContactSFIDParameterDto))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_SendASBSFID_SendActivityContact)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_SendASBSFID_SendActivityContact)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult SendActivityContact([FromBody] SendASBSFIDParameterDto.ActivityContactSFIDParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sendASBSFIDBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sendASBSFIDBL.SendActivityContact(data);

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

        [Route("SendActivitySuspect")]
        [SwaggerRequestExample(typeof(SendASBSFIDParameterDto.ActivitySuspectQualifiedSend), typeof(APISendASBSFIDSuspectExample))]
        [ResponseType(typeof(ResponseBase<SendASBSFIDDto>)), HttpPost]
        [JsonValueValidation(typeof(SendASBSFIDParameterDto.ActivitySuspectQualifiedSend))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_SendASBSFID_SendActivitySuspect)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_SendASBSFID_SendActivitySuspect)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult SendActivitySuspect([FromBody] SendASBSFIDParameterDto.ActivitySuspectQualifiedSend data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sendASBSFIDBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sendASBSFIDBL.SendActivitySuspect(data);

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


        [Route("SendProspectCreate")]
        [SwaggerRequestExample(typeof(SendASBSFIDParameterDto.CreateProspect), typeof(APISendASBSFIDSuspectExample))]
        [ResponseType(typeof(ResponseBase<SendASBSFIDDto>)), HttpPost]
        [JsonValueValidation(typeof(SendASBSFIDParameterDto.CreateProspect))]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_SendASBSFID_SendProspectCreate)]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_SendASBSFID_SendProspectCreate)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult SendProspectCreate([FromBody] SendASBSFIDParameterDto.CreateProspect data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sendASBSFIDBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sendASBSFIDBL.SendProspectCreate(data);

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