﻿#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_Zombie_WOTimeRegisterController Controller
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-13
//
// ===========================================================================	
#endregion

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

    [RoutePrefix("DMSData/DQIZombieWOTimeRegister"), ApiGroup("DMS Data")]
    public class VWI_Zombie_WOTimeRegisterController : BaseController
    {
        private readonly IVWI_Zombie_WOTimeRegisterBL _VWI_Zombie_WOTimeRegisterBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="VWI_Zombie_WOTimeRegisterBL"></param>
        public VWI_Zombie_WOTimeRegisterController(IVWI_Zombie_WOTimeRegisterBL VWI_Zombie_WOTimeRegisterBL)
        {
            this._VWI_Zombie_WOTimeRegisterBL = VWI_Zombie_WOTimeRegisterBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Get DQI Zombie WOTimeRegister list by Criteria
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
        [SwaggerRequestExample(typeof(VWI_Zombie_WOTimeRegisterFilterDto), typeof(APIReadExample))]
        [ResponseType(typeof(ResponseBase<List<VWI_Zombie_WOTimeRegisterDto>>)), HttpPost]
        [JsonValueValidation(typeof(VWI_Zombie_WOTimeRegisterFilterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_VWI_Zombie_WOTimeRegister_Read_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_VWI_Zombie_WOTimeRegister_Read)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Read([FromBody]VWI_Zombie_WOTimeRegisterFilterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                _VWI_Zombie_WOTimeRegisterBL.Initialize(UserName, DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _VWI_Zombie_WOTimeRegisterBL.ReadList(data, PageSize, ListDealer, DealerCompanyCode);

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
