#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ClientRolePermission controller class
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
using KTB.DNet.Interface.WebUI.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FrameworkConstants = KTB.DNet.Interface.Framework.Constants;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class ClientRolePermissionController : BaseController
    {
        #region Initialize
        private IClientRepository<APIClient, Guid> _clientRepo;
        private IRolePermissionRepository<APIRolePermission, int> _rolePermissionRepo;
        private IClientRoleRepository<APIClientRole, int> _clientRoleRepo;
        private IEndpointPermissionRepository<APIEndpointPermission, int> _permissionRepo;

        #endregion

        #region Constructor

        public ClientRolePermissionController(
            IClientRepository<APIClient, Guid> clientRepo,
            IEndpointPermissionRepository<APIEndpointPermission, int> permissionRepo,
            IClientRoleRepository<APIClientRole, int> clientRoleRepo,
            IRolePermissionRepository<APIRolePermission, int> rolePermissionRepo
            )
        {
            _clientRepo = clientRepo;
            _permissionRepo = permissionRepo;
            _clientRoleRepo = clientRoleRepo;
            _rolePermissionRepo = rolePermissionRepo;

        }
        #endregion

        #region Method GetRolePermissionIdsByClientRoleId
        /// <summary>
        /// Search role permission
        /// </summary>
        /// <param name="clientRoleId"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = FrameworkConstants.Permissions.WebUI_ClientRolePermission_Read)]
        public IHttpActionResult GetSelectedPermissionIdsByClientRoleId(int clientRoleId)
        {
            try
            {
                List<int> listOfRolePermission = _rolePermissionRepo.GetByClientRoleId(clientRoleId).Select(s => s.PermissionId).ToList();

                return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = listOfRolePermission });

            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
            }
        }

        #endregion

        #region GetClientOptions
        /// <summary>
        /// Get Options
        /// </summary>
        /// <param name="model"></param>
        [HttpGet]
        [PermissionAuthorize(PermissionName = FrameworkConstants.Permissions.WebUI_ClientRolePermission_Read)]
        public IHttpActionResult GetClientOptions()
        {
            try
            {
                List<ClientViewModel> clientViewModel = new List<ClientViewModel>();
                if (this.IsDMSAdmin)
                {
                    clientViewModel = _clientRepo.GetAll().ConvertList<APIClient, ClientViewModel>();
                }
                else
                {
                    clientViewModel = _clientRepo.GetUserClient(new APIUser() { Id = this.UserId }).ConvertList<APIClient, ClientViewModel>();
                }

                return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = clientViewModel });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
            }
        }
        #endregion

        #region Method Get Role By Client Id
        [HttpGet]
        [PermissionAuthorize(PermissionName = FrameworkConstants.Permissions.WebUI_ClientRolePermission_Read)]
        public IHttpActionResult GetRolesByClientId(string clientId)
        {
            try
            {
                Guid cid = Guid.Empty;
                if (Guid.TryParse(clientId, out cid))
                {
                    List<APIClientRole> listOfData = _clientRoleRepo.GetByClientId(cid);

                    var roles = listOfData.Select(e =>
                                      new EnumViewModel
                                      {
                                          Value = e.Id,
                                          Text = e.Role.Name
                                      }).OrderBy(o => o.Text);

                    return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = roles });
                }
                else
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Client Id is not valid!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = ex.Message });
            }
        }
        #endregion

        #region Get Permissions By ClientId
        /// <summary>
        /// Get unselected client permission
        /// </summary>
        /// <param name="clientRoleId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = FrameworkConstants.Permissions.WebUI_ClientRolePermission_Read)]
        public IHttpActionResult GetAllPermissionsByClientId(Guid clientId)
        {
            List<APIEndpointPermission> listOfClientPermission = _permissionRepo.GetClientPermission(clientId);

            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = listOfClientPermission
            });

        }
        #endregion

        #region Method SetUserModifier
        /// <summary>
        /// Set modifier for Created By and Updated By
        /// </summary>
        public override void SetUserModifier(string username)
        {
            _clientRepo.SetUserLogin(username);
            _permissionRepo.SetUserLogin(username);
            _clientRoleRepo.SetUserLogin(username);
            _rolePermissionRepo.SetUserLogin(username);
        }
        #endregion

        #region Method Update Client Role
        /// <summary>
        /// Update client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = FrameworkConstants.Permissions.WebUI_ClientRolePermission_Update)]
        public IHttpActionResult UpdateClientRole(ClientRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<APIRolePermission> rolepermissions = new List<APIRolePermission>();
                    foreach (int permissionId in model.PermissionIds)
                    {
                        APIRolePermission rolePermission = new APIRolePermission { PermissionId = permissionId, ClientRoleId = model.ClientRoleId };
                        rolepermissions.Add(rolePermission);
                    }
                    var clientRole = new APIClientRole()
                    {
                        Id = model.ClientRoleId,
                        RolePermissions = rolepermissions
                    };

                    ResponseMessage result = _clientRoleRepo.Update(clientRole);
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
                    Message = "Failed to update client! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
        }
        #endregion
    }
}