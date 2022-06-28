#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MsAppVersion Controller class
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
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebUI.Helper;
using KTB.DNet.Interface.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class MsAppVersionController : BaseController
    {
        #region Initialize
        private IMsAppVersionRepository<MsAppVersion, int> _msAppVersionRepo;
        private IMsApplicationRepository<MsApplication, Guid> _msAppRepo;
        #endregion

        #region Constructor
        public MsAppVersionController(
            IMsAppVersionRepository<MsAppVersion, int> msAppVersionRepo,
            IMsApplicationRepository<MsApplication, Guid> msAppRepo)
        {
            _msAppVersionRepo = msAppVersionRepo;
            _msAppRepo = msAppRepo;

            SetUserModifier(this.UserName);
        }
        #endregion

        #region Method Get
        ///// <summary>
        ///// Get MsApplication Version
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsAppVersion_Update)]
        public IHttpActionResult Get(int id)
        {
            MsAppVersion msAppVersion = _msAppVersionRepo.Get(id);
            MsAppVersionViewModel msAppVersionViewModel = msAppVersion.ConvertObject<MsAppVersionViewModel>();

            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = msAppVersionViewModel });
        }
        #endregion

        #region Method Create
        ///// <summary>
        ///// Create MsApplication Version
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsAppVersion_Create)]
        public IHttpActionResult Create(MsAppVersionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MsAppVersion msAppVersion = model.ConvertObject<MsAppVersion>();

                    ResponseMessage result = _msAppVersionRepo.Create(msAppVersion);
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
        ///// Edit MsApplication Version
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsAppVersion_Update)]
        public IHttpActionResult Update(MsAppVersionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var msAppVersion = model.ConvertObject<MsAppVersion>();

                    ResponseMessage result = _msAppVersionRepo.Update(msAppVersion);

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
        ///// Delete MsApplication Version
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpDelete]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsAppVersion_Delete)]
        public IHttpActionResult Delete(int id)
        {
            if (id > 0)
            {
                ResponseMessage result = _msAppVersionRepo.Delete(id);
                return Json(result);
            }

            return Json(new ResponseMessage()
            {
                Success = false,
                Status = ResponseStatus.Warning,
                Message = "MsApplication Version does not exist"
            });
        }
        #endregion

        #region Method Search
        ///// <summary>
        ///// Search MsApplication Versions
        ///// </summary>
        ///// <param name="postModel"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsAppVersion_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<MsAppVersion> listOfmsAppVersions = null;

                listOfmsAppVersions = _msAppVersionRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                return Json(new
                {
                    Records = listOfmsAppVersions.ConvertList<MsAppVersion, MsAppVersionViewModel>(),
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    Records = new List<MsAppVersion>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Method SetUserModifier
        /// <summary>
        /// Set Modifier for Created By and Updated By
        /// </summary>
        /// <param name="username"></param>
        public override void SetUserModifier(string username)
        {
            _msAppVersionRepo.SetUserLogin(username);
            _msAppRepo.SetUserLogin(username);
        }
        #endregion

        #region Method GetOptions
        /// <summary>
        /// Get Options for Create or Update User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_MsAppVersion_Read)]
        public IHttpActionResult GetOptions()
        {
            List<MsApplication> appOptions = _msAppRepo.GetAll();
            ResponseMessage response = new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = new
                {
                    AppOptions = appOptions
                }
            };

            return Json(response);
        }
        #endregion
    }
}