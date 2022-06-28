#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Role controller class
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
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class RoleController : BaseController
    {
        #region Initialize
        private IRoleRepository<APIRole, int> _roleRepo;
        private IEndpointPermissionRepository<APIEndpointPermission, int> _permissionRepo;
        private IClientRoleRepository<APIClientRole, int> _clientRoleRepo;
        #endregion

        #region Constructor
        public RoleController(
            IRoleRepository<APIRole, int> roleRepo,
            IEndpointPermissionRepository<APIEndpointPermission, int> permissionRepo,
            IClientRoleRepository<APIClientRole, int> clientRoleRepo
            )
        {
            _roleRepo = roleRepo;
            _permissionRepo = permissionRepo;
            _clientRoleRepo = clientRoleRepo;
        }
        #endregion

        #region Method Get
        /// <summary>
        /// Get role by id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Role_Read)]
        public IHttpActionResult Get(int id)
        {
            try
            {
                APIRole role = _roleRepo.Get(id);


                if (role != null)
                {
                    var model = role.ConvertObject<RoleViewModel>();

                    model.Permissions = _permissionRepo.GetAll().ConvertList<APIEndpointPermission, EndpointPermissionViewModel>();

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
        ///// <summary>
        ///// Create role
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Role_Create)]
        public IHttpActionResult Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                APIRole role = new APIRole
                {
                    Name = model.Name,
                    Level = model.Level
                };

                ResponseMessage result = _roleRepo.Create(role);
                if (result != null)
                {
                    return Json(result);
                }


            }

            return Json(new ResponseMessage
            {
                Success = false,
                Status = ResponseStatus.Warning,
                Message = "Failed to create Role! Model is not valid!",
                ModelState = ModelStateHelper.GetParseableModelState(ModelState)
            });
        }
        #endregion

        #region Method Update
        ///// <summary>
        ///// Update role
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Role_Update)]
        public IHttpActionResult Update(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new APIRole
                {
                    Id = model.Id,
                    Name = model.Name,
                    Level = model.Level
                };

                try
                {
                    ResponseMessage result = _roleRepo.Update(role);
                    return Json(result);
                }
                catch (Exception ex)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = ex.Message });
                }
            }

            return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Model is not valid!" });
        }
        #endregion

        #region Method Delete
        ///// <summary>
        ///// Delete Role
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpDelete]
        //[ValidateAntiForgeryToken]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Role_Delete)]
        public IHttpActionResult Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    ResponseMessage result = _roleRepo.Delete(id);
                    return Json(result);
                }
                catch (Exception ex)
                {
                    return Json(new ResponseMessage { Success = false, Status = ResponseStatus.Error, Message = ex.Message });
                }

            }

            return Json(new ResponseMessage { Success = false, Status = ResponseStatus.Error, Message = "Id is not valid" });
        }
        #endregion

        #region Method Search
        ///// <summary>
        ///// Search roles
        ///// </summary>
        ///// <param name="postModel"></param>
        ///// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Role_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<APIRole> listOfClient = _roleRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                return Json(new
                {
                    Records = listOfClient.ConvertList<APIRole, RoleViewModel>(),
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    Records = new List<APIRole>(),
                    TotalRecord = 0
                });
            }
        }
        #endregion

        #region Method Get Role By Client Id
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Role_Read)]
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

        #region Method SetUserModifier
        /// <summary>
        /// Set modifier for Created By and Updated By
        /// </summary>
        /// <param name="username"></param>
        public override void SetUserModifier(string username)
        {
            _roleRepo.SetUserLogin(username);
            _permissionRepo.SetUserLogin(username);
            _clientRoleRepo.SetUserLogin(username);
        }
        #endregion

    }
}