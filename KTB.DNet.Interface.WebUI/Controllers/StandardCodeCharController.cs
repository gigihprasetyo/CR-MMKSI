#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StandardCodeChar Controller class
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
    public class StandardCodeCharController : BaseController
    {
        #region Initialize
        private IStandardCodeRepository<StandardCodeChar, int> _standardCodeCharRepo;
        #endregion

        #region Constructor
        public StandardCodeCharController(
            IStandardCodeRepository<StandardCodeChar, int> standardCodeCharCharRepo)
        {
            _standardCodeCharRepo = standardCodeCharCharRepo;

            SetUserModifier(this.UserName);
        }
        #endregion

        #region Method Get
        ///// <summary>
        ///// Get StandardCodeChar
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_StandardCodeChar_Update)]
        public IHttpActionResult Get(int id)
        {
            StandardCodeChar standardCodeChar = _standardCodeCharRepo.Get(id);
            StandardCodeCharViewModel standardCodeCharViewModel = standardCodeChar.ConvertObject<StandardCodeCharViewModel>();

            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = standardCodeCharViewModel });
        }
        #endregion

        #region Method Create
        ///// <summary>
        ///// Create StandardCodeChar
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_StandardCodeChar_Create)]
        public IHttpActionResult Create(StandardCodeCharViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StandardCodeChar standardCodeChar = model.ConvertObject<StandardCodeChar>();

                    ResponseMessage result = _standardCodeCharRepo.Create(standardCodeChar);
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
        ///// Edit StandardCodeChar
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_StandardCodeChar_Update)]
        public IHttpActionResult Update(StandardCodeCharViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var standardCodeChar = model.ConvertObject<StandardCodeChar>();

                    ResponseMessage result = _standardCodeCharRepo.Update(standardCodeChar);

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
        ///// Delete StandardCodeChar
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpDelete]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_StandardCodeChar_Delete)]
        public IHttpActionResult Delete(int id)
        {
            if (id > 0)
            {
                ResponseMessage result = _standardCodeCharRepo.Delete(id);
                return Json(result);
            }

            return Json(new ResponseMessage()
            {
                Success = false,
                Status = ResponseStatus.Warning,
                Message = "StandardCodeChar does not exist"
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
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_StandardCodeChar_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount = 0;
                int totalResultsCount = 0;

                List<StandardCodeChar> listOfstandardCodeChar = null;

                listOfstandardCodeChar = _standardCodeCharRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                return Json(new
                {
                    Records = listOfstandardCodeChar.ConvertList<StandardCodeChar, StandardCodeCharViewModel>(),
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    Records = new List<StandardCodeChar>(),
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
            _standardCodeCharRepo.SetUserLogin(username);
        }
        #endregion
    }
}