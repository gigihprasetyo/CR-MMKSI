#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPRFromVendor Controller
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
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper;
using KTB.DNet.Interface.WebApi.Helper.CustomAttribute;
using KTB.DNet.Interface.WebApi.Helper.Throttle;
using KTB.DNet.Interface.WebApi.Models.Examples;
using Swashbuckle.Examples;
using System;
using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    [RoutePrefix("SparePart/SparePartPRFromVendor"), ApiGroup("Spare Part")]
    public class SparePartPRFromVendorController : BaseController
    {
        private readonly ISparePartPRFromVendorBL _sparePartPRFromVendorBL;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// SparePartPRFromVendor constructor with parameter
        /// </summary>
        /// <param name="SparePartPRFromVendorBL"></param>
        public SparePartPRFromVendorController(ISparePartPRFromVendorBL SparePartPRFromVendorBL)
        {
            _sparePartPRFromVendorBL = SparePartPRFromVendorBL;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Create New Spare Part Purchase Receipt From Vendor
        /// </summary>
        /// <param name="data">SparePartPRFromVendor Param</param>
        /// <remarks>Create SparePartPRFromVendor</remarks>
        /// <response code="200">OK</response> 
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="403">Forbidden</response> 
        /// <response code="404">Not Found</response> 
        /// <response code="500">Internal Server Error</response> 
        /// <returns></returns>
        [Route("Create")]
        [SwaggerRequestExample(typeof(SparePartPRFromVendorParameterDto), typeof(APISparePartPRFromVendorExample))]
        [ResponseType(typeof(ResponseBase<SparePartPRFromVendorDto>)), HttpPost]
        [JsonValueValidation(typeof(SparePartPRFromVendorParameterDto))]
        [ScheduleAuthorize(ControllerMethodName = Constants.EndpointUrl.WebAPI_SparePart_SparePartPRFromVendor_Create_URL)]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_SparePart_SparePartPRFromOtherVendor_Create)]
        [ThrottleFilterAttribute(ThrottleIdentifier.Username)]
        public IHttpActionResult Create([FromBody]SparePartPRFromVendorParameterDto data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return InvalidModelState(_json);

                if (data.DealerCode != DealerCode)
                {
                    return InvalidDealerCode(_json, data.DealerCode);
                }

                foreach (SparePartPRDetailFromVendorParameterDto i in data.SparePartPRDetailFromVendors)
                {
                    if (i.DealerCode != DealerCode)
                        return InvalidDealerCode(_json, i.DealerCode);
                }

                _sparePartPRFromVendorBL.Initialize(GetUsername(data.ResendBy, data.LogId), DealerCode);

                var runtimeLog = new TransactionRuntime { StartedTime = DateTime.Now };

                var result = _sparePartPRFromVendorBL.Create(data);

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
