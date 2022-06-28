#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Log Controller
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
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;
//using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebApi.Controllers
{
    /// <summary>
    /// Log Web API
    /// </summary>
    [RoutePrefix("Log")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LogController : ApiController
    {
        private readonly ILoggerService _log;
        private readonly JsonMediaTypeFormatter _json;

        /// <summary>
        /// Log constructor with parameter
        /// </summary>
        /// <param name="loggerService"></param>
        public LogController(ILoggerService loggerService)
        {
            _log = loggerService;
            _json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            _json.UseDataContractJsonSerializer = true;
        }

        /// <summary>
        /// Reset log memory cache
        /// </summary>                
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [Route("Reset")]
        //[PermissionAuthorize(PermissionName = PermissionConstants.WebAPI_Log_Reset)]
        public IHttpActionResult Reset()
        {
            try
            {
                _log.LogCachingReset();

                return this.Ok(new { message = "Log configuration cache has been reset successfully." });
            }
            catch
            {
                return BadRequest(ModelState);
            }
        }
    }
}
