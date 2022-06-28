#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MsApplication controller class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion

#region Namespace imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebUI.Helper;
using KTB.DNet.Interface.WebUI.Models;
using KTB.DNet.Interface.WebUI.Models.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class MsApplicationController : BaseController
    {
        #region Initialize
        private IMsApplicationRepository<MsApplication, Guid> _msApplicationRepo;
        private IEndpointPermissionRepository<APIEndpointPermission, int> _permissionRepo;
        #endregion

        #region Constructor
        public MsApplicationController(
            IMsApplicationRepository<MsApplication, Guid> msApplicationRepo,
            IEndpointPermissionRepository<APIEndpointPermission, int> permissionRepo)
        {
            _msApplicationRepo = msApplicationRepo;
            _permissionRepo = permissionRepo;

            _msApplicationRepo.SetUserLogin(UserName);
            _permissionRepo.SetUserLogin(UserName);
        }
        #endregion

        #region Method Get
        /// <summary>
        /// Get MsApplication - GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_MsApplication_Read)]
        public IHttpActionResult Get(Guid id)
        {
            try
            {
                MsApplication msApplication = _msApplicationRepo.Get(id);

                if (msApplication != null)
                {
                    MsApplicationViewModel model = msApplication.ConvertObject<MsApplicationViewModel>();
                    List<EndpointPermissionViewModel> listOfApplicationPermisssion = _msApplicationRepo.GetPermission(id).ConvertList<APIEndpointPermission, EndpointPermissionViewModel>();

                    model.Permissions = listOfApplicationPermisssion;
                    model.ListOfSelectedPermissionId = listOfApplicationPermisssion.Select(p => p.Id).ToList();
                    return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = model });
                }

                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Id is invalid" });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = ex.Message }); ;
            }

        }
        #endregion

        #region Method Create
        /// <summary>
        /// Create MsApplication - POST
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsApplication_Create)]
        public IHttpActionResult Create(MsApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MsApplication msApplication = new MsApplication()
                    {
                        Name = model.Name,
                        DeploymentJenkinsJobName = model.DeploymentJenkinsJobName,
                        DeploymentBackupFolder = model.DeploymentBackupFolder
                    };

                    ResponseMessage result = _msApplicationRepo.Create(msApplication, model.ListOfSelectedPermissionId);
                    return Json(result);
                }
                catch (Exception ex)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = ex.Message });
                }
            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to create MsApplication! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
        }
        #endregion

        #region Method Update
        /// <summary>
        /// Update MsApplication - POST
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsApplication_Update)]
        public IHttpActionResult Update(MsApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MsApplication msApplication = new MsApplication()
                    {
                        AppId = model.AppId,
                        Name = model.Name,
                        DeploymentJenkinsJobName = model.DeploymentJenkinsJobName,
                        DeploymentBackupFolder = model.DeploymentBackupFolder
                    };

                    ResponseMessage result = _msApplicationRepo.Update(msApplication, model.ListOfSelectedPermissionId);
                    return Json(result);
                }
                catch (Exception ex)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = ex.Message });
                }
            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to update MsApplication! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
        }
        #endregion

        #region Method Delete
        /// <summary>
        /// Delete MsApplication - DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsApplication_Delete)]
        public IHttpActionResult Delete(string id)
        {
            if (id != null)
            {
                try
                {
                    ResponseMessage result = _msApplicationRepo.Delete(new Guid(id));
                    return Json(result);
                }
                catch (Exception ex)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = ex.Message });
                }
            }

            return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "id could not be null" });
        }
        #endregion

        #region Method Search
        /// <summary>
        /// Search MsApplication using PostModel - POST
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsApplication_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<MsApplication> listOfMsApplication = _msApplicationRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                return Json(new
                {
                    Records = listOfMsApplication.ConvertList<MsApplication, MsApplicationViewModel>(),
                    TotalRecord = filteredResultsCount
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Records = new List<MsApplication>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Method Get Options
        /// <summary>
        /// Get Permission option
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsApplication_Read)]
        public IHttpActionResult GetPermissionOptions()
        {
            List<APIEndpointPermission> listOfPermission = _permissionRepo.GetAll();
            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = listOfPermission
            });
        }

        /// <summary>
        /// Get jenkins job options
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsApplication_Read)]
        public async Task<IHttpActionResult> GetJenkinsJobOptions()
        {
            string jenkinsClientUrl = ConfigurationManager.AppSettings["JenkinsClientUrl"];
            string jenkinsUserName = ConfigurationManager.AppSettings["JenkinsUserName"];
            string jenkinsApiToken = ConfigurationManager.AppSettings["JenkinsApiToken"];
            try
            {
                JenkinsClient.Client client = JenkinsClient.Client.Create(
                   jenkinsClientUrl, jenkinsUserName, jenkinsApiToken);

                var jenkins = await client.GetJobsAsync();
                var result = new List<EnumViewModel>();

                result.Add(new EnumViewModel { Text = "", Value = "" });

                foreach (var item in jenkins.Where(w => w.name.Contains("MMKSI")))
                {
                    result.Add(new EnumViewModel { Text = item.name, Value = item.name });
                }

                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Failed to load Jenkins jobs. " + GetInnerException(ex).Message
                });
            }

        }
        #endregion

        #region Method SetUserModifier
        /// <summary>
        /// Set modifier for Created By and Updated By
        /// </summary>
        /// <param name="username"></param>
        public override void SetUserModifier(string username)
        {
            _msApplicationRepo.SetUserLogin(username);
            _permissionRepo.SetUserLogin(username);
        }
        #endregion
    }
}
