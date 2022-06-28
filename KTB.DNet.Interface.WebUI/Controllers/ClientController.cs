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
    public class ClientController : BaseController
    {
        #region Initialize
        private IClientRepository<APIClient, Guid> _clientRepo;
        private IEndpointPermissionRepository<APIEndpointPermission, int> _permissionRepo;
        private IRoleRepository<APIRole, int> _roleRepo;
        private IMsApplicationRepository<MsApplication, Guid> _msAppRepo;
        #endregion

        #region Constructor
        public ClientController(IClientRepository<APIClient, Guid> clientRepo, IEndpointPermissionRepository<APIEndpointPermission, int> permissionRepo, IRoleRepository<APIRole, int> roleRepo, IMsApplicationRepository<MsApplication, Guid> msAppRepo)
        {
            _clientRepo = clientRepo;
            _permissionRepo = permissionRepo;
            _roleRepo = roleRepo;
            _msAppRepo = msAppRepo;

        }
        #endregion

        #region Method Get
        /// <summary>
        /// Get client application by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        //[BreadCrumb(Clear = false, Label = "Edit Client", Order = 2)]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Client_Read)]
        public IHttpActionResult Get(string id)
        {
            ClientViewModel result = null;

            try
            {
                Guid clientId = new Guid(id);
                if (!string.IsNullOrEmpty(id))
                {
                    APIClient client = _clientRepo.Get(clientId);

                    if (client != null)
                    {
                        List<EndpointPermissionViewModel> listOfClientPermisssion = _clientRepo.GetClientPermission(clientId).ToList().ConvertList<APIEndpointPermission, EndpointPermissionViewModel>();
                        result = new ClientViewModel()
                        {
                            ClientId = client.ClientId,
                            Name = client.Name,
                            AppId = client.AppId,
                            Permissions = listOfClientPermisssion,
                            ListOfSelectedRoleId = _clientRepo.GetClientRole(clientId).Select(p => p.Id).ToList(),
                            ListOfSelectedPermissionId = listOfClientPermisssion.Select(p => p.Id).ToList(),

                        };
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = result });
        }
        #endregion

        #region Method Get List By Id
        /// <summary>
        /// Get client application by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        //[BreadCrumb(Clear = false, Label = "Edit Client", Order = 2)]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Client_Read)]
        public IHttpActionResult GetListById(List<string> clientIdList)
        {
            List<ClientViewModel> result =  new List<ClientViewModel>();

            try
            {
                if (clientIdList.Count > 0)
                {
                    List<APIClient> clientList = _clientRepo.GetListById(clientIdList);
                    foreach (var client in clientList)
                    {
                        var clientViewModel = new ClientViewModel()
                        {
                            ClientId = client.ClientId,
                            Name = client.Name,
                            AppId = client.AppId
                        };
                        result.Add(clientViewModel);
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = result });
        }
        #endregion

        #region Method Create
        /// <summary>
        /// Create client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Client_Create)]
        public IHttpActionResult Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = new APIClient()
                    {
                        Name = model.Name,
                        AppId = model.AppId
                    };

                    ResponseMessage result = _clientRepo.Create(client, model.ListOfSelectedRoleId, model.ListOfSelectedPermissionId);
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

        #region Method Update
        /// <summary>
        /// Update client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Client_Update)]
        public IHttpActionResult Update(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = new APIClient()
                    {
                        ClientId = model.ClientId,
                        Name = model.Name,
                        AppId = model.AppId
                    };

                    ResponseMessage result = _clientRepo.Update(client, model.ListOfSelectedRoleId, model.ListOfSelectedPermissionId);
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

        #region Method Delete
        /// <summary>
        /// Delete client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Client_Delete)]
        public IHttpActionResult Delete(string id)
        {
            if (id != null)
            {
                try
                {
                    ResponseMessage result = _clientRepo.Delete(new Guid(id));
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

        #region Method GetOptions and GetAppOptions
        /// <summary>
        /// Get options list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Client_Read)]
        public IHttpActionResult GetOptions(string appId)
        {
            List<APIEndpointPermission> listOfPermission = _msAppRepo.GetPermission(Guid.Parse(appId)).OrderBy(o => o.Name).ToList();
            List<APIRole> listOfRole = (_roleRepo.GetAll()).ToList();
            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = new { ListOfPermission = listOfPermission, ListOfRole = listOfRole }
            });
        }

        /// <summary>
        /// Get App option
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Client_Read)]
        public IHttpActionResult GetAppOptions()
        {
            List<MsApplication> listOfApplication = _msAppRepo.GetAll();
            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = listOfApplication
            });

        }
        #endregion

        #region Method Search
        /// <summary>
        /// Load client application
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Client_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<APIClient> listOfClient = _clientRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                return Json(new
                {
                    Records = listOfClient.ConvertList<APIClient, ClientViewModel>(),
                    TotalRecord = filteredResultsCount
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Records = new List<APIClient>(),
                    TotalRecord = 0
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
            _clientRepo.SetUserLogin(username);
            _permissionRepo.SetUserLogin(username);
            _roleRepo.SetUserLogin(username);
        }
        #endregion

    }
}
