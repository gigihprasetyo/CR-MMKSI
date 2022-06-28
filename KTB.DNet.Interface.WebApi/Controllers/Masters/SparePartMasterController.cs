#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartMaster Controller
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
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Models;
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
    /// SparePart Master API Controller
    /// </summary>    
    [RoutePrefix("Master/Sparepart"), ApiGroup("Masters")]
    public class SparePartMasterController : BaseController
    {
        private readonly ISparePartMasterBL _sparePartMasterBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SparePart Master Constructor with parameter
        /// </summary>
        /// <param name="sparePartMasterBL"></param>
        public SparePartMasterController(ISparePartMasterBL sparePartMasterBL)
        {
            _sparePartMasterBL = sparePartMasterBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create SparePart Master
        /// </summary>
        /// <param name="data">SparePart Master Data</param>
        /// <remarks>Create SparePart Master</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [ResponseType(typeof(ResponseBase<SparePartMasterDto>)), HttpPost]
        [JsonValueValidation(typeof(SparePartMasterParameterDto))]
        [NonAction]
        public IHttpActionResult Create([FromBody]SparePartMasterParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparePartMasterBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var log = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparePartMasterBL.Create(data);

                log.FinishedTime = DateTime.Now;

                LogTransactionRuntime(log, result, result.success);

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
        /// Delete SparePart Master by ID
        /// </summary>
        /// <param name="id">SparePart Master ID</param>
        /// <remarks>Delete by id</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [ActionWebApiFilterAttribute]
        [Route("Delete")]
        [ResponseType(typeof(ResponseBase<SparePartMasterDto>)), HttpDelete]
        [NonAction]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _sparePartMasterBL.Initialize(UserName, DealerCode);

                var result = _sparePartMasterBL.Delete(id);

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
        /// Get SparePart Master list by Criteria
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>Search by Criteria</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Read")]
        [SwaggerRequestExample(typeof(SparePartMasterFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<SparePartMasterDto>>)), HttpPost]
        [JsonValueValidation(typeof(SparePartMasterFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_Master_Sparepart_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Master_SparePart_Read)]
        public IHttpActionResult Read([FromBody]SparePartMasterFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _sparePartMasterBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparePartMasterBL.Read(data, PageSize);

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
