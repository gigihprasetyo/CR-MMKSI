
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
    /// Carroserie Detail Web Api
    /// </summary>
    [RoutePrefix("VehiclePurchase/CarrosserieDetail"), ApiGroup("Vehicle Purchase")]
    public class CarrosserieDetailController : BaseController
    {
        private readonly ICarrosserieDetailBL _carDetailBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="carDetailBL"></param>
        public CarrosserieDetailController(ICarrosserieDetailBL carDetailBL)
        {
            _carDetailBL = carDetailBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Update CarrosserieDetail entity
        /// </summary>
        /// <param name="value">CarrosserieDetail Parameter </param>
        /// <remarks>Update 'CarrosserieDetail' entity</remarks>
        /// <returns></returns>
        [Route("Update")]
        [SwaggerRequestExample(typeof(CarrosserieDetailUpdateParameterDto), typeof(APIUpdateCarrosserieDetailExample))]
        [ResponseType(typeof(ResponseBase<CarrosserieDetailDto>)), HttpPost]
        public IHttpActionResult Update([FromBody]CarrosserieDetailUpdateParameterDto value)
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
                _carDetailBL.Initialize(GetUsername(value.UpdatedBy), DealerCode);

                var result = _carDetailBL.Update(value);

                var logModel = new DataLogModel { 
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
        /// Create CarrosserieDetail Data 
        /// </summary>
        /// <param name="value">CarrosserieDetail Data </param>
        /// <remarks>Create CarrosserieDetail Data </remarks>
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(CarrosserieDetailParameterDto), typeof(APICreateCarrosserieDetailExample))]
        [ResponseType(typeof(ResponseBase<CarrosserieDetailDto>)), HttpPost]
        public IHttpActionResult Create([FromBody] CarrosserieDetailParameterDto value)
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
                _carDetailBL.Initialize(GetUsername(value.UpdatedBy), DealerCode);

                var result = _carDetailBL.Create(value);

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
        /// Delete CarrosserieDetail by ID
        /// </summary>
        /// <param name="id">bank ID</param>
        /// <remarks>Delete by id</remarks>
        /// <returns></returns>
        [ActionWebApiFilterAttribute]
        [Route("Delete")]
        [ResponseType(typeof(ResponseBase<CarrosserieDetailDto>)), HttpDelete]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {

                _carDetailBL.Initialize(UserName, DealerCode);

                var result = _carDetailBL.Delete(id);

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
        /// Get CarrosserieDetail list by Criteria
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>Search by Criteria</remarks>
        /// <returns></returns>
        [Route("Read")]
        [SwaggerRequestExample(typeof(CarrosserieDetailFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<CarrosserieDetailDto>>)), HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IHttpActionResult Read([FromBody]CarrosserieDetailFilterDto value)
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
                _carDetailBL.Initialize(UserName, DealerCode);

                var pageSize = Convert.ToInt32(WebConfigurationManager.AppSettings["PageSize"]);

                var result = _carDetailBL.Read(value, pageSize);

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
