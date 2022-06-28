
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Helper.Messages;
using KTB.DNet.Interface.WebApi.Models;
using KTB.DNet.Interface.WebApi.Models.Examples;
using KTB.DNet.Interface.WebApi.Parameters;
using KTB.DNet.Resources;
using KTB.DNET.BusinessLogic.Interfaces;
using Newtonsoft.Json;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Description;

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// Vehicle Purchase Header Web Api
    /// </summary>
    [JwtAuthentication]
    [RoutePrefix("VehiclePurchase/Header"), ApiGroup("Vehicle Purchase")]
    public class VehiclePurchaseHeaderController : BaseController
    {
        private readonly IVehiclePurchaseHeaderBL _vHeaderBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehPurchaseDetailBL"></param>
        public VehiclePurchaseHeaderController(IVehiclePurchaseHeaderBL vehPurchaseDetailBL)
        {
            _vHeaderBL = vehPurchaseDetailBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Update VehiclePurchaseHeader entity
        /// </summary>
        /// <param name="value">VehiclePurchaseHeader Parameter </param>
        /// <remarks>Update 'VehiclePurchaseHeader' entity</remarks>
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(VehiclePurchaseHeaderUpdateParameterDto), typeof(APIUpdateVehiclePurchaseHeaderExample))]
        [ResponseType(typeof(ResponseBase<VehiclePurchaseHeaderDto>)), HttpPost]
        public IHttpActionResult Update([FromBody]VehiclePurchaseHeaderUpdateParameterDto value)
        {
            if (!ModelState.IsValid)
            {
                List<MessageBase> messages = HttpCodeMessage.GetErrorMessage(ModelState);
                var result = HttpCodeMessage.BuildErrorResult(messages);
                HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(messages);

                return Content(httpStatusCode, result, _json);
            }

            try
            {
                _vHeaderBL.Initialize(GetUsername(value.UpdatedBy), DealerCode);

                var result = _vHeaderBL.Update(value);

                var logModel = new DataLogModel
                {
                    Result = result,
                    Value = value,
                    Succeed = result.success,
                    Update = false
                };


                if (!result.success)
                {
                    HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(result.messages);

                    LogError(new Exception(JsonConvert.SerializeObject(result.messages)));

                    return Content(httpStatusCode, result, _json);
                }

                LogTransaction(logModel);

                return Json(result);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, new MessageBase { errorcode = Convert.ToInt32(MessageResource.ErrorCodePRGUnhandle), message = String.Format(MessageResource.ErrorMsgPRGUnhandle, ex.Message) }, _json);
            }
        }

        /// <summary>
        /// Create VehiclePurchaseHeader Data 
        /// </summary>
        /// <param name="value">VehiclePurchaseHeader Data </param>
        /// <remarks>Create VehiclePurchaseHeader Data </remarks>
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(VehiclePurchaseHeaderParameterDto), typeof(APICreateVehiclePurchaseHeaderExample))]
        [ResponseType(typeof(ResponseBase<VehiclePurchaseHeaderDto>)), HttpPost]
        public IHttpActionResult Create([FromBody] VehiclePurchaseHeaderParameterDto value)
        {
            if (!ModelState.IsValid)
            {
                List<MessageBase> messages = HttpCodeMessage.GetErrorMessage(ModelState);
                var errorResult = HttpCodeMessage.BuildErrorResult(messages);
                HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(messages);

                return Content(httpStatusCode, errorResult, _json);
            }

            try
            {
                _vHeaderBL.Initialize(GetUsername(value.UpdatedBy), DealerCode);

                var result = _vHeaderBL.Create(value);

                var logModel = new DataLogModel
                {
                    Result = result,
                    Value = value,
                    Succeed = result.success,
                    Update = false
                };

                if (!result.success)
                {
                    HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(result.messages);

                    LogError(new Exception(JsonConvert.SerializeObject(result.messages)));

                    return Content(httpStatusCode, result, _json);
                }

                LogTransaction(logModel);

                return Json(result);
            }

            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, new MessageBase { errorcode = Convert.ToInt32(MessageResource.ErrorCodePRGUnhandle), message = String.Format(MessageResource.ErrorMsgPRGUnhandle, ex.Message) }, _json);
            }
        }

        /// <summary>
        /// Delete VehiclePurchaseHeader by ID
        /// </summary>
        /// <param name="id">bank ID</param>
        /// <remarks>Delete by id</remarks>
        /// <returns></returns>
        [ActionWebApiFilterAttribute]
        [Route("Delete")]
        [ResponseType(typeof(ResponseBase<VehiclePurchaseHeaderDto>)), HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _vHeaderBL.Delete(id);

                if (!result.success)
                {
                    HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(result.messages);

                    LogError(new Exception(JsonConvert.SerializeObject(result.messages)));

                    return Content(httpStatusCode, result, _json);
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, new MessageBase { errorcode = Convert.ToInt32(MessageResource.ErrorCodePRGUnhandle), message = String.Format(MessageResource.ErrorMsgPRGUnhandle, ex.Message) }, _json);
            }
        }

        /// <summary>
        /// Get VehiclePurchaseHeader list by Criteria
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>Search by Criteria</remarks>
        /// <returns></returns>
        [Route("Read")]
        [SwaggerRequestExample(typeof(VehiclePurchaseHeaderFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VehiclePurchaseHeaderDto>>)), HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Read([FromBody]VehiclePurchaseHeaderFilterDto value)
        {
            if (!ModelState.IsValid)
            {
                List<MessageBase> messages = HttpCodeMessage.GetErrorMessage(ModelState);
                var errorResult = HttpCodeMessage.BuildErrorResult(messages);
                HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(messages);

                return Content(httpStatusCode, errorResult, _json);
            }

            try
            {
                var pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);

                var result = _vHeaderBL.Read(value, pageSize);

                if (!result.success)
                {
                    HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(result.messages);

                    LogError(new Exception(JsonConvert.SerializeObject(result.messages)));

                    return Content(httpStatusCode, result, _json);
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, new MessageBase { errorcode = Convert.ToInt32(MessageResource.ErrorCodePRGUnhandle), message = String.Format(MessageResource.ErrorMsgPRGUnhandle, ex.Message) }, _json);
            }
        }
    }
}
