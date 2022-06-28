#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Auth controller class
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
using KTB.DNet.Interface.WebUI.Models;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class AuthController : ApiController
    {
        private IUserRepository<APIUser, int> _userRepo;
        private IClientRepository<APIClient, Guid> _clientRepo;
        private IUserPermissionRepository<APIUserPermission, int> _userPermissionRepo;
        private IClientUserRepository<APIClientUser, int> _clientUserRepo;

        public AuthController(
            IUserRepository<APIUser, int> userRepo,
            IClientRepository<APIClient, Guid> clientRepo,
            IUserPermissionRepository<APIUserPermission, int> userPermissionRepo,
            IClientUserRepository<APIClientUser, int> clientUserRepo)
        {
            _userRepo = userRepo;
            _clientRepo = clientRepo;
            _userPermissionRepo = userPermissionRepo;
            _clientUserRepo = clientUserRepo;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult Login(LoginUserViewModel user)
        {
            if (user == null)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = MessageResource.ErrorMsgAuthInvalidUsernameOrPassword });
            }

            try
            {
                APIUser authenticatedUser = _userRepo.GetAuthenticatedUser(user.Username, user.Password);
                if (authenticatedUser != null)
                {
                    List<APIClient> listOfClient = _clientRepo.GetUserClient(authenticatedUser);
                    List<ClientViewModel> listOfClientVM = new List<ClientViewModel>();
                    Guid adminClientId = new Guid(AppConfigs.GetString("DMSAdminClientId"));
                    bool isDMSAdmin = false;
                    if (listOfClient != null)
                    {
                        listOfClientVM = listOfClient.Where(c =>
                        {
                            if (c.ClientId == adminClientId)
                            {
                                isDMSAdmin = true;
                                return true;
                            }

                            string name = c.Name.ToUpper();
                            return name.Contains("WEBUI") || name.Contains("UI");
                        }).Select(c => new ClientViewModel() { ClientId = c.ClientId, Name = c.Name }).ToList();
                    }

                    UserViewModel userVM = new UserViewModel()
                    {
                        Id = authenticatedUser.Id,
                        Email = authenticatedUser.Email,
                        FirstName = authenticatedUser.FirstName,
                        LastName = authenticatedUser.LastName,
                        UserName = authenticatedUser.UserName,
                        DealerCode = authenticatedUser.DealerCode,
                        DealerId = authenticatedUser.DealerId,
                        IsDMSAdmin = isDMSAdmin
                    };

                    return Json(new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Data = new
                        {
                            User = userVM,
                            Clients = listOfClientVM
                        }
                    });
                }

                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = MessageResource.ErrorMsgAuthInvalidUsernameOrPassword });
            }
            catch (Exception ex)
            {

                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = MessageResource.ErrorMsgAuthInvalidUsernameOrPassword });
            }
        }
    }
}
