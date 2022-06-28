#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ApplicationConfig controller class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Elmah;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Framework.Enums;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebUI.Helper;
using KTB.DNet.Interface.WebUI.Models;
using KTB.DNet.Interface.WebUI.Models.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class ApplicationConfigController : BaseController
    {
        #region Initialize
        private IApplicationConfigRepository<ApplicationConfig, long> _applicationConfigRepo;
        #endregion

        #region Constructor
        public ApplicationConfigController(IApplicationConfigRepository<ApplicationConfig, long> applicationConfigRepo)
        {
            _applicationConfigRepo = applicationConfigRepo;
            _applicationConfigRepo.SetUserLogin(this.UserName);
        }
        #endregion

        #region Method Get
        ///// <summary>
        ///// Get Application Config
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_AppConfig_Update)]
        public IHttpActionResult Get(int id)
        {
            ApplicationConfig appConfig = _applicationConfigRepo.Get(id);
            ApplicationConfigViewModel appConfigViewModel = appConfig.ConvertObject<ApplicationConfigViewModel>();

            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = appConfigViewModel });
        }
        #endregion

        #region Method Create
        ///// <summary>
        ///// Create Application Config
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_AppConfig_Create)]
        public IHttpActionResult Create(ApplicationConfigViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationConfig appConfig = model.ConvertObject<ApplicationConfig>();

                    ResponseMessage result = _applicationConfigRepo.Create(appConfig);
                    return Json(result);
                }

                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Model is not valid",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
            }

        }
        #endregion

        #region Method Update
        ///// <summary>
        ///// Edit Application Config
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_AppConfig_Update)]
        public IHttpActionResult Update(ApplicationConfigViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var appConfig = model.ConvertObject<ApplicationConfig>();

                    ResponseMessage result = _applicationConfigRepo.Update(appConfig);

                    // call reset log memory cache
                    if (model.Name.StartsWith("WebAPI"))
                    {
                        var log = ResetServerCache();
                        if (log != null)
                        {
                            return Json(log);
                        }
                    }

                    return Json(result);
                }
                else
                {
                    return Json(new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Error,
                        Message = "Model is not valid",
                        ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
            }
        }
        #endregion

        #region Method Delete
        ///// <summary>
        ///// Delete Application Config
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpDelete]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_AppConfig_Delete)]
        public IHttpActionResult Delete(int id)
        {
            if (id > 0)
            {
                ResponseMessage result = _applicationConfigRepo.Delete(id);
                return Json(result);
            }

            return Json(new ResponseMessage()
            {
                Success = false,
                Status = ResponseStatus.Warning,
                Message = "Application config does not exist"
            });
        }
        #endregion

        #region Method Search
        ///// <summary>
        ///// Search application configs
        ///// </summary>
        ///// <param name="postModel"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_AppConfig_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<ApplicationConfig> listOfAppConfigs = null;

                listOfAppConfigs = _applicationConfigRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                return Json(new
                {
                    Records = listOfAppConfigs.ConvertList<ApplicationConfig, ApplicationConfigViewModel>(),
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    Records = new List<ApplicationConfig>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Method Get Options
        /// <summary>
        /// Get options for Endpoint Schedule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_AppConfig_Read)]
        public IHttpActionResult GetOptions()
        {
            List<string> listOfConstantConfigKey = ConstantHelper.GetListOfConstantValue<string>(typeof(Constants.ConfigKey));
            List<EnumViewModel> configKeyOptions = null;

            if (listOfConstantConfigKey.Count > 0)
            {
                List<string> unregisteredConfigKey = _applicationConfigRepo.GetUnregisteredConfigKey(listOfConstantConfigKey);
                if (unregisteredConfigKey.Count > 0)
                {
                    configKeyOptions = unregisteredConfigKey.Select(p => new EnumViewModel() { Value = p, Text = p }).ToList();
                }
            }

            List<EnumViewModel> dataTypeOptions = Enum.GetValues(typeof(ConfigDataType))
               .Cast<ConfigDataType>()
               .Select(t => new EnumViewModel
               {
                   Value = (int)t,
                   Text = t.ToString()
               }).ToList();


            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = new
                {
                    DataTypeOptions = dataTypeOptions,
                    ConfigKeyOptions = configKeyOptions
                }
            });
        }
        #endregion

        #region Method SetUserModifier
        /// <summary>
        /// Set Modifier for Created By and Updated By
        /// </summary>
        /// <param name="username"></param>
        public override void SetUserModifier(string username)
        {
            _applicationConfigRepo.SetUserLogin(username);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Reset memory cache in server
        /// </summary>
        private ResponseMessage ResetServerCache()
        {
            try
            {
                // initialize the httpclient
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://localhost/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // convert the input
                var content = new StringContent("reset", Encoding.UTF8, "application/json");

                // set tls
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                // ignore certificate ( need to refactor this )
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                // extract the end point
                bool isProduction = ConfigurationManager.AppSettings["ApiRoot"].Equals("https://interface.mitsubishi-motors.co.id/Web/");

                // define
                string requestUri = isProduction ? "Api/Log/Reset" : "WebApi/Log/Reset";

                // call the API
                Task<HttpResponseMessage> response = httpClient.PostAsync(requestUri, content);
                if (!response.Result.IsSuccessStatusCode)
                {
                    var result = new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Error,
                        Message = "Cache reset failed",
                        ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                // should be logged
                ErrorSignal.FromCurrentContext().Raise(GetInnerException(ex), HttpContext.Current);
            }

            return null;
        }
        #endregion
    }
}