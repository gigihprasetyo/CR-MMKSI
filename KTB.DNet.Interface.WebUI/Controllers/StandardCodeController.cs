#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCode Controller class
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
    public class StandardCodeController : BaseController
    {
        #region Initialize
        private IStandardCodeRepository<StandardCode, int> _standardCodeRepo;
        #endregion

        #region Constructor
        public StandardCodeController(
            IStandardCodeRepository<StandardCode, int> standardCodeRepo)
        {
            _standardCodeRepo = standardCodeRepo;

            SetUserModifier(this.UserName);
        }
        #endregion

        #region Method Get
        ///// <summary>
        ///// Get StandardCode
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_StandardCode_Update)]
        public IHttpActionResult Get(int id)
        {
            StandardCode standardCode = _standardCodeRepo.Get(id);
            StandardCodeViewModel standardCodeViewModel = standardCode.ConvertObject<StandardCodeViewModel>();

            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = standardCodeViewModel });
        }
        #endregion

        #region Method Create
        ///// <summary>
        ///// Create StandardCode
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_StandardCode_Create)]
        public IHttpActionResult Create(StandardCodeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StandardCode standardCode = model.ConvertObject<StandardCode>();

                    ResponseMessage result = _standardCodeRepo.Create(standardCode);
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
        ///// Edit StandardCode
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_StandardCode_Update)]
        public IHttpActionResult Update(StandardCodeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var standardCode = model.ConvertObject<StandardCode>();

                    ResponseMessage result = _standardCodeRepo.Update(standardCode);

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
        ///// Delete StandardCode
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpDelete]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_StandardCode_Delete)]
        public IHttpActionResult Delete(int id)
        {
            if (id > 0)
            {
                ResponseMessage result = _standardCodeRepo.Delete(id);
                return Json(result);
            }

            return Json(new ResponseMessage()
            {
                Success = false,
                Status = ResponseStatus.Warning,
                Message = "StandardCode does not exist"
            });
        }
        #endregion

        #region Method Search
        ///// <summary>
        ///// Search Standard Code
        ///// </summary>
        ///// <param name="postModel"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_StandardCode_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<StandardCode> listOfstandardCode = null;
                listOfstandardCode = _standardCodeRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                return Json(new
                {
                    Records = listOfstandardCode.ConvertList<StandardCode, StandardCodeViewModel>(),
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    Records = new List<StandardCode>(),
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
            _standardCodeRepo.SetUserLogin(username);
        }
        #endregion
    }
}