
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
    /// Carroserie Header web api
    /// </summary>    
    [RoutePrefix("VehiclePurchase/CarrosserieHeader"), ApiGroup("Vehicle Purchase")]
    public class CarrosserieHeaderController : BaseController
    {
        private readonly ICarrosserieHeaderBL _carHeaderBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carHeaderBL"></param>
        public CarrosserieHeaderController(ICarrosserieHeaderBL carHeaderBL)
        {
            _carHeaderBL = carHeaderBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Update CarrosserieHeader entity
        /// </summary>
        /// <param name="value">CarrosserieHeader Parameter </param>
        /// <remarks>Update 'CarrosserieHeader' entity</remarks>
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(CarrosserieHeaderUpdateParameterDto), typeof(APIUpdateCarrosserieHeaderExample))]
        [ResponseType(typeof(ResponseBase<CarrosserieHeaderDto>)), HttpPost]
        public IHttpActionResult Update([FromBody]CarrosserieHeaderUpdateParameterDto value)
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
                _carHeaderBL.Initialize(GetUsername(value.UpdatedBy), DealerCode);

                var result = _carHeaderBL.Update(value);

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
        /// Create CarrosserieHeader Data 
        /// </summary>
        /// <param name="value">CarrosserieHeader Data </param>
        /// <remarks>Create CarrosserieHeader Data </remarks>
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(CarrosserieHeaderParameterDto), typeof(APICreateCarrosserieHeaderExample))]
        [ResponseType(typeof(ResponseBase<CarrosserieHeaderDto>)), HttpPost]
        public IHttpActionResult Create([FromBody] CarrosserieHeaderParameterDto value)
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
                _carHeaderBL.Initialize(GetUsername(value.UpdatedBy), DealerCode);

                var result = _carHeaderBL.Create(value);

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
        /// Delete CarrosserieHeader by ID
        /// </summary>
        /// <param name="id">bank ID</param>
        /// <remarks>Delete by id</remarks>
        /// <returns></returns>
        [ActionWebApiFilterAttribute]
        [Route("Delete")]
        [ResponseType(typeof(ResponseBase<CarrosserieHeaderDto>)), HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _carHeaderBL.Initialize(UserName, DealerCode);

                var result = _carHeaderBL.Delete(id);

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
        /// Get CarrosserieHeader list by Criteria
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>Search by Criteria</remarks>
        /// <returns></returns>
        [Route("Read")]
        [SwaggerRequestExample(typeof(CarrosserieHeaderFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<CarrosserieHeaderDto>>)), HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Read([FromBody]CarrosserieHeaderFilterDto value)
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
                _carHeaderBL.Initialize(UserName, DealerCode);

                var pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);

                var result = _carHeaderBL.Read(value, pageSize);

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
