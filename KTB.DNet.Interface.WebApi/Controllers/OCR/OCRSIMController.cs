#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : OCRSIM Controller
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
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;

#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// OCR Web API
    /// </summary>  
    [RoutePrefix("OCR/SIM"), ApiGroup("OCR Identity")]
    public class OCRSimpController : BaseController
    {
        private readonly IOCRIdentityBL _ocrBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// OCR constructor with parameter
        /// </summary>
        /// <param name="ocrBL"></param>
        public OCRSimpController(IOCRIdentityBL ocrBL)
        {
            _ocrBL = ocrBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Upload SIM OCR file to Server
        /// </summary>        
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Upload")]
        [ResponseType(typeof(ResponseBase<List<OCRIdentitySIMDto>>)), HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_OCR_UploadSIM)]
        [SwaggerParameter("SIMFile", "A SIM image file", Required = true, Type = "file")]
        [EnableCors(origins: "*", headers: Constants.HttpRequestConstants.CORS_Headers, methods: "*")]
        public IHttpActionResult Upload()
        {
            try
            {
                // set credentials
                _ocrBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                // get posted file
                HttpPostedFile postedFile = null;
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    postedFile = HttpContext.Current.Request.Files[0];
                }
                else
                {
                    runtimeLog.FinishedTime = DateTime.Now;
                    LogTransactionRuntime(runtimeLog, new MessageBase(ErrorCode.DataRequiredField, MessageResource.ErrorMsgNoPostedFile), false);
                    return Content(HttpStatusCode.BadRequest, new MessageBase(ErrorCode.DataRequiredField, MessageResource.ErrorMsgNoPostedFile), _json);
                }

                // validate
                if (postedFile.ContentLength > 0)
                {
                    var result = _ocrBL.UploadSIM(postedFile);

                    runtimeLog.FinishedTime = DateTime.Now;
                    LogTransactionRuntime(runtimeLog, result, result.success);

                    if (result.success)
                        return Json(result);
                    else
                        return Content(GetHttpCodeMsg(result.messages), result, _json);
                }
                else
                {
                    runtimeLog.FinishedTime = DateTime.Now;
                    LogTransactionRuntime(runtimeLog, new MessageBase(ErrorCode.DataContentCorrupt, MessageResource.ErrorMsgFileCorrupt), false);
                    return Content(HttpStatusCode.BadRequest, new MessageBase(ErrorCode.DataContentCorrupt, MessageResource.ErrorMsgFileCorrupt), _json);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);

                return Content(HttpStatusCode.InternalServerError, GetUnhandledExceptionMsg(ex.Message), _json);
            }
        }

        /// <summary>
        /// Check SIM OCR progress status from Server
        /// </summary>
        /// <param name="uploadID"></param>
        /// <remarks>Search by Criteria</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Progress")]
        [ResponseType(typeof(ResponseBase<OCRResultValueDto>)), HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_OCR_ProgressSIM)]
        [EnableCors(origins: "*", headers: Constants.HttpRequestConstants.CORS_Headers, methods: "*")]
        public IHttpActionResult Progress(string uploadID)
        {
            try
            {
                this.SetOriginalRequestData(uploadID, typeof(string));

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _ocrBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _ocrBL.ProgressSIM(uploadID);

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

        /// <summary>
        /// Get SIM OCR result from Server
        /// </summary>
        /// <param name="uploadID"></param>
        /// <remarks>Search by Criteria</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Data")]
        [ResponseType(typeof(ResponseBase<OCRSIMDataDto>)), HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_OCR_DataSIM)]
        [EnableCors(origins: "*", headers: Constants.HttpRequestConstants.CORS_Headers, methods: "*")]
        public IHttpActionResult Data(string uploadID)
        {
            try
            {
                this.SetOriginalRequestData(uploadID, typeof(string));

                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _ocrBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };
                var result = _ocrBL.DataSIM(uploadID);

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
