#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Client controller class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 18/10/2018 17:05
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
using System.Linq;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class ClientUserController : BaseController
    {
        #region Initialize
        private IClientUserRepository<APIClientUser, int> _clientUserRepo;
        #endregion

        #region Constructor
        public ClientUserController(IClientUserRepository<APIClientUser, int> clientUserRepo)
        {
            _clientUserRepo = clientUserRepo;
        }
        #endregion

        
        #region Method Save Client User
        /// <summary>
        /// Create client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Client_Create)]
        public IHttpActionResult SaveClientUser(ClientUserBulkViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var listOfUsers = new List<APIUser>();
                    foreach (var userId in model.UserIds)
                    {
                        var user = new APIUser()
                        {
                            Id = userId
                        };
                        listOfUsers.Add(user);
                    }
                    
                    ResponseMessage result = _clientUserRepo.SaveClientUserInBulk(model.ClientId, listOfUsers);
                    return Json(result);
                }

                catch (Exception ex)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
                }
            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to create client! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
        }
        #endregion

        #region Method SetUserModifier
        /// <summary>
        /// Set Modifier for Created By and Updated By
        /// </summary>
        public override void SetUserModifier(string username)
        {
            _clientUserRepo.SetUserLogin(username);
        }
        #endregion

    }
}
