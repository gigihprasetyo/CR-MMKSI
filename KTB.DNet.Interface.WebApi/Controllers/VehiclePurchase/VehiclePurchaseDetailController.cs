
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
    /// Vehicle Purchase Detail Api
    /// </summary>    
    [RoutePrefix("VehiclePurchase/Detail"), ApiGroup("Vehicle Purchase")]
    public class VehiclePurchaseDetailController : BaseController
    {
        private readonly IVehiclePurchaseDetailBL _vPruchaseBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehiclePuchaseDetailBL"></param>
        public VehiclePurchaseDetailController(IVehiclePurchaseDetailBL vehiclePuchaseDetailBL)
        {
            _vPruchaseBL = vehiclePuchaseDetailBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Update VehiclePurchaseDetail entity
        /// </summary>
        /// <param name="value">VehiclePurchaseDetail Parameter </param>
        /// <remarks>Update 'VehiclePurchaseDetail' entity</remarks>
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(VehiclePurchaseDetailUpdateParameterDto), typeof(APIUpdateVehiclePurchaseDetailExample))]
        [ResponseType(typeof(ResponseBase<VehiclePurchaseDetailDto>)), HttpPost]
        public IHttpActionResult Update([FromBody]VehiclePurchaseDetailUpdateParameterDto value)
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
                _vPruchaseBL.Initialize(GetUsername(value.UpdatedBy), DealerCode);

                var result = _vPruchaseBL.Update(value);

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
        /// Create VehiclePurchaseDetail Data 
        /// </summary>
        /// <param name="value">VehiclePurchaseDetail Data </param>
        /// <remarks>Create VehiclePurchaseDetail Data </remarks>
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(VehiclePurchaseDetailParameterDto), typeof(APICreateVehiclePurchaseDetailExample))]
        [ResponseType(typeof(ResponseBase<VehiclePurchaseDetailDto>)), HttpPost]
        public IHttpActionResult Create([FromBody] VehiclePurchaseDetailParameterDto value)
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
                _vPruchaseBL.Initialize(GetUsername(value.UpdatedBy), DealerCode);

                var result = _vPruchaseBL.Create(value);

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
        /// Delete VehiclePurchaseDetail by ID
        /// </summary>
        /// <param name="id">bank ID</param>
        /// <remarks>Delete by id</remarks>
        /// <returns></returns>
        [ActionWebApiFilterAttribute]
        [Route("Delete")]
        [ResponseType(typeof(ResponseBase<VehiclePurchaseDetailDto>)), HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _vPruchaseBL.Delete(id);

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
        /// Get VehiclePurchaseDetail list by Criteria
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>Search by Criteria</remarks>
        /// <returns></returns>
        [Route("Read")]
        [SwaggerRequestExample(typeof(VehiclePurchaseDetailFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VehiclePurchaseDetailDto>>)), HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Read([FromBody]VehiclePurchaseDetailFilterDto value)
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

                var result = _vPruchaseBL.Read(value, pageSize);

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
