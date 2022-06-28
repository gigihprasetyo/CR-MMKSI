#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : User controller class
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
using LinqToExcel;
using LinqToExcel.Attributes;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using PermissionConstants = KTB.DNet.Interface.Framework.Constants.Permissions;
#endregion

namespace KTB.DNet.Interface.WebUI.Controllers
{
    public class UserController : BaseController
    {
        #region Initialize
        private IUserRepository<APIUser, int> _userRepo;
        private IRoleRepository<APIRole, int> _roleRepo;
        private IDealerRepository<Dealer, int> _dealerRepo;
        private IClientRepository<APIClient, Guid> _clientRepo;
        private IEndpointPermissionRepository<APIEndpointPermission, int> _permissionRepo;

        private IClientUserRepository<APIClientUser, int> _clientUserRepo;
        private IUserPermissionRepository<APIUserPermission, int> _userPermissionRepo;
        private IUserRoleRepository<APIUserRole, int> _userRoleRepo;
        #endregion

        #region Constructor
        public UserController(
            IUserRepository<APIUser, int> userRepo,
            IRoleRepository<APIRole, int> roleRepo,
            IDealerRepository<Dealer, int> dealerRepo,
            IClientRepository<APIClient, Guid> clientRepo,
            IEndpointPermissionRepository<APIEndpointPermission, int> permissionRepo,
            IClientUserRepository<APIClientUser, int> clientUserRepo,
            IUserPermissionRepository<APIUserPermission, int> userPermissionRepo,
            IUserRoleRepository<APIUserRole, int> userRoleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _dealerRepo = dealerRepo;
            _permissionRepo = permissionRepo;
            _clientRepo = clientRepo;
            _userPermissionRepo = userPermissionRepo;
            _clientUserRepo = clientUserRepo;
            _userRoleRepo = userRoleRepo;

        }
        #endregion

        #region Method Get
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_User_Read)]
        public IHttpActionResult Get(int id)
        {

            APIUser user = _userRepo.Get(id);

            if (user != null)
            {
                List<ClientViewModel> clients = _clientRepo.GetUserClient(user).Select(c => new ClientViewModel() { ClientId = c.ClientId, Name = c.Name }).ToList();
                UserViewModel result = user.ConvertObject<UserViewModel>();

                if (user.Clients != null)
                {
                    result.ClientUsers = user.Clients.ToList().ConvertList<APIClientUser, ClientUserViewModel>();
                    result.ClientUsers.ForEach(cu =>
                    {
                        cu.Client = clients.FirstOrDefault(c => c.ClientId == cu.ClientId);
                    });
                }

                result.Clients = clients;
                result.Roles = _roleRepo.GetUserRole(user.Id).ConvertList<APIRole, RoleViewModel>();

                if (user.DealerId.HasValue)
                {
                    var dealer = _dealerRepo.Get((int)user.DealerId);

                    if (dealer != null)
                    {
                        result.DealerCode = dealer.DealerCode;
                    }
                }


                return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = result });
            }

            return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("User with id {0} does not exist", id) });

        }
        #endregion

        #region Method Delete
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_User_Delete)]
        public IHttpActionResult Delete(int id)
        {
            if (id == 0)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("User with id {0} does not exist", id) });
            }
            else
            {
                try
                {
                    ResponseMessage responseMessage = _userRepo.Delete(id);
                    return Json(responseMessage);
                }
                catch (Exception ex)
                {
                    return Json(new ResponseMessage { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
                }
            }
        }
        #endregion

        #region Method GetPermissionByClientUser
        /// <summary>
        /// Get Permission by ClientUser
        /// </summary>
        /// <param name="clientUser"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_User_Read)]
        public IHttpActionResult LoadUserPermissionOptions(ClientUserViewModel clientUser)
        {
            try
            {
                var userPermissions = _userPermissionRepo.GetUserPermissionByClientUserId(clientUser.Id).ConvertList<APIUserPermission, UserPermissionViewModel>();
                var permissions = _permissionRepo.GetClientPermission(clientUser.ClientId).ConvertList<APIEndpointPermission, EndpointPermissionViewModel>();
                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Data = new
                    {
                        UserPermissions = userPermissions,
                        PermissionOptions = permissions
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = GetInnerException(ex).Message
                });
            }
        }
        #endregion

        #region Method Create
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_User_Create)]
        public IHttpActionResult Create(UserViewModel user)
        {
            return Save(user, true);
        }
        #endregion

        #region Method Update
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_User_Update)]
        public IHttpActionResult Update(UserViewModel user)
        {
            return Save(user, false);
        }
        #endregion

        #region Method Save
        /// <summary>
        /// Save User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isNewData"></param>
        /// <returns></returns>
        private IHttpActionResult Save(UserViewModel user, bool isNewData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResponseMessage result;

                    string adminClientId = AppConfigs.GetString("DMSAdminClientId");

                    APIUser dbUser = user.ConvertObject<APIUser>();
                    dbUser.DealerId = this.IsDMSAdmin ? user.DealerId : Convert.ToInt16(this.DealerId);

                    List<APIUserPermission> selectedUserPermissions = new List<APIUserPermission>();
                    if (user.UserPermissions != null && user.UserPermissions.Any())
                    {
                        selectedUserPermissions = user.UserPermissions.ConvertList<UserPermissionViewModel, APIUserPermission>();
                    }

                    List<APIClient> listOfSelectedClient = user.Clients.ConvertList<ClientViewModel, APIClient>();
                    List<APIRole> listOfSelectedUserRole = user.Roles.ConvertList<RoleViewModel, APIRole>();

                    if (isNewData)
                    {
                        result = _userRepo.CreateWithAllClientRolePermission(dbUser, listOfSelectedClient, listOfSelectedUserRole, selectedUserPermissions);
                    }
                    else
                    {
                        if (dbUser.Id == this.DMSAdminUserId && this.UserId != this.DMSAdminUserId)
                        {
                            string errMessage = "DMS Admin can only be edited by DMS Admin!";
                            result = new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = errMessage };
                        }
                        else
                        {
                            result = _userRepo.UpdateWithAllClientRolePermission(dbUser, listOfSelectedClient, listOfSelectedUserRole, selectedUserPermissions);
                        }
                    }


                    if (result.Success)
                    {
                        dbUser = ((APIUser)result.Data);
                        List<ClientViewModel> clients = dbUser.Clients.Select(c => new ClientViewModel() { ClientId = c.ClientId, Name = c.Client == null ? string.Empty : c.Client.Name }).ToList();
                        UserViewModel resultViewModelData = dbUser.ConvertObject<UserViewModel>();
                        resultViewModelData.ClientUsers = dbUser.Clients.ToList().ConvertList<APIClientUser, ClientUserViewModel>();
                        resultViewModelData.Clients = clients;
                        resultViewModelData.Roles = _roleRepo.GetUserRole(dbUser.Id).ConvertList<APIRole, RoleViewModel>();

                        result.Data = resultViewModelData;
                        string operation = isNewData ? "created" : "updated";
                    }

                    return Json(result);
                }
                catch (Exception ex)
                {
                    return Json(new ResponseMessage { Success = false, Status = ResponseStatus.Error, Message = "Failed to save user! " + GetInnerException(ex).Message });
                }

            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to save user! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
        }
        #endregion

        #region Method GetUserProfile
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_User_Read)]
        public IHttpActionResult GetUserProfile()
        {
            APIUser user = _userRepo.Get(this.UserId);

            if (user != null)
            {
                List<ClientViewModel> clients = _clientRepo.GetUserClient(user).Select(c => new ClientViewModel() { ClientId = c.ClientId, Name = c.Name }).ToList();
                UserViewModel result = user.ConvertObject<UserViewModel>();

                if (user.Clients != null)
                {
                    result.ClientUsers = user.Clients.ToList().ConvertList<APIClientUser, ClientUserViewModel>();
                    result.ClientUsers.ForEach(cu =>
                    {
                        cu.Client = clients.FirstOrDefault(c => c.ClientId == cu.ClientId);
                    });
                }

                result.Clients = clients;
                result.Roles = _roleRepo.GetUserRole(user.Id).ConvertList<APIRole, RoleViewModel>();

                if (user.DealerId.HasValue)
                {
                    var dealer = _dealerRepo.Get((int)user.DealerId);

                    if (dealer != null)
                    {
                        result.DealerCode = dealer.DealerCode;
                    }
                }


                return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = result });
            }

            return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("User with id {0} does not exist", this.UserId) });

        }
        #endregion

        #region Method UpdateUserInfo
        /// <summary>
        /// Update User Info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateUserInfo(UserViewModel model)
        {
            ResponseMessage result = UpdateUser(model);
            if (result.Success)
            {
                string username = ((APIUser)result.Data).UserName;
                result.Data = ((APIUser)result.Data).ConvertObject<UserViewModel>();
            }
            return Json(result);
        }
        #endregion

        #region Update User's Permission
        /// <summary>
        /// Update User's Permission
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_User_Update)]
        public IHttpActionResult UpdateUserPermission(SaveUserPermissionViewModel param)
        {
            if (ModelState.IsValid)
            {
                ResponseMessage result = _userPermissionRepo.SaveUserPermission(param.UserId, param.ListOfUserPermission.ConvertList<UserPermissionViewModel, APIUserPermission>());
                return Json(result);
            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to update user's permissions! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
        }
        #endregion

        #region Update User's Clients
        /// <summary>
        /// Update User's Clients
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_User_Update)]
        public IHttpActionResult UpdateUserClient(SaveClientUserViewModel param)
        {
            if (ModelState.IsValid)
            {
                ResponseMessage result = _clientUserRepo.SaveClientUser(param.UserId, param.ListOfClientId);
                if (result.Success)
                {
                    result.Data = ((List<APIClientUser>)result.Data).ConvertList<APIClientUser, ClientUserViewModel>();
                }
                return Json(result);
            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to update user's clients! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }

        }
        #endregion

        #region Update User's Roles
        /// <summary>
        /// Update User's Roles
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_User_Update)]
        public IHttpActionResult UpdateUserRole(SaveUserRoleViewModel param)
        {
            if (ModelState.IsValid)
            {
                ResponseMessage result = _userRoleRepo.SaveUserRole(param.UserId, param.ListOfRoleId);
                return Json(result);
            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to update user's roles! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
        }
        #endregion

        #region Method UpdateUser
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ResponseMessage UpdateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseMessage result = _userRepo.Update(model.ConvertObject<APIUser>());
                return result;
            }
            else
            {
                return new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to save user! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                };
            }
        }
        #endregion

        #region Method Search
        /// <summary>
        /// Search User
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_User_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<APIUser> listOfUser = _userRepo.Search(postModel, this.IsDMSAdmin ? null : ((int?)this.DealerId), out filteredResultsCount, out totalResultsCount);
                List<UserViewModel> listOfUserViewModel = new List<UserViewModel>();

                listOfUser.ForEach(u =>
                {
                    List<ClientViewModel> clients = _clientRepo.GetUserClient(u).Select(c => new ClientViewModel() { ClientId = c.ClientId, Name = c.Name }).ToList();
                    UserViewModel user = u.ConvertObject<UserViewModel>();

                    if (u.Clients != null)
                    {
                        user.ClientUsers = u.Clients.ToList().ConvertList<APIClientUser, ClientUserViewModel>();
                        user.ClientUsers.ForEach(cu =>
                        {
                            cu.Client = clients.FirstOrDefault(c => c.ClientId == cu.ClientId);
                        });
                    }

                    user.Clients = clients;
                    user.Roles = _roleRepo.GetUserRole(u.Id).ConvertList<APIRole, RoleViewModel>();
                    listOfUserViewModel.Add(user);
                });

                return Json(new
                    {
                        Records = listOfUserViewModel,
                        TotalRecord = filteredResultsCount
                    }
                );
            }
            catch (Exception ex)
            {
                return Json(new
                    {
                        Records = new List<UserViewModel>(),
                        TotalRecord = 0
                    }
                );
            }
        }
        #endregion

        #region Method GetOptions
        /// <summary>
        /// Get Options for Create or Update User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_User_Read)]
        public IHttpActionResult GetOptions()
        {
            string adminClientId = AppConfigs.GetString("DMSAdminClientId");
            Guid adminGuidClientId = new Guid(adminClientId);
            int adminRoleId = AppConfigs.GetInt("DMSAdminRoleId");

            List<APIRole> roleOptions = _roleRepo.GetAll();
            List<DealerViewModel> dealerOptions = new List<DealerViewModel>();
            List<ClientViewModel> clientOptions = new List<ClientViewModel>();

            if (IsDMSAdmin)
            {
                clientOptions = _clientRepo.GetAll().OrderBy(o => o.Name).ToList().ConvertList<APIClient, ClientViewModel>();
                dealerOptions = _dealerRepo.GetActiveDealers()
                                        .Select(d =>
                                            new DealerViewModel
                                            {
                                                Id = d.Id,
                                                DealerCode = d.DealerCode,
                                                DealerName = d.DealerName,
                                                Status = d.Status,
                                                Title = d.Title
                                            }).ToList();
            }
            else
            {
                clientOptions = _clientRepo.GetUserClient(new APIUser() { Id = this.UserId }).ConvertList<APIClient, ClientViewModel>();
                dealerOptions = new List<DealerViewModel>();
                Dealer dealer = _dealerRepo.Get(this.DealerId);
                if (dealer != null)
                {
                    dealerOptions.Add(dealer.ConvertObject<DealerViewModel>());
                }
            }

            ResponseMessage response = new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = new
                {
                    RoleOptions = roleOptions,
                    ClientOptions = clientOptions,
                    DealerOptions = dealerOptions
                }
            };

            return Json(response);
        }
        #endregion

        #region Method CreateWithRepositories
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_User_Create)]
        public IHttpActionResult CreateWithRepositories(UserViewModel user)
        {
            return SaveWithRepositories(user, true);
        }
        #endregion

        #region Method SaveWithRepositories
        private IHttpActionResult SaveWithRepositories(UserViewModel user, bool isNewData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResponseMessage result;

                    string adminClientId = AppConfigs.GetString("DMSAdminClientId");

                    APIUser dbUser = user.ConvertObject<APIUser>();
                    dbUser.DealerId = this.IsDMSAdmin ? user.DealerId : Convert.ToInt16(this.DealerId);

                    List<APIUserPermission> selectedUserPermissions = new List<APIUserPermission>();
                    if (user.UserPermissions != null && user.UserPermissions.Any())
                    {
                        selectedUserPermissions = user.UserPermissions.ConvertList<UserPermissionViewModel, APIUserPermission>();
                    }

                    List<APIClient> listOfSelectedClient = user.Clients.ConvertList<ClientViewModel, APIClient>();
                    List<APIRole> listOfSelectedUserRole = user.Roles.ConvertList<RoleViewModel, APIRole>();
                    if (listOfSelectedClient.Count == 0)
                    {
                        return Json(new ResponseMessage { Success = false, Status = ResponseStatus.Error, Message = "Failed to save user! Please insert correct Client for user: " + dbUser.UserName });
                    }

                    if (listOfSelectedUserRole.Count == 0)
                    {
                        return Json(new ResponseMessage { Success = false, Status = ResponseStatus.Error, Message = "Failed to save user! Please insert correct Role for user: " + dbUser.UserName });
                    }
                    if (isNewData)
                    {
                        result = _userRepo.CreateWithSeparateClientRolePermission(dbUser, listOfSelectedClient, listOfSelectedUserRole, selectedUserPermissions);
                    }
                    else
                    {
                        if (dbUser.Id == this.DMSAdminUserId && this.UserId != this.DMSAdminUserId)
                        {
                            string errMessage = "DMS Admin can only be edited by DMS Admin!";
                            result = new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = errMessage };
                        }
                        else
                        {
                            result = _userRepo.UpdateWithSeparateClientRolePermission(dbUser, listOfSelectedClient, listOfSelectedUserRole, selectedUserPermissions);
                        }
                    }


                    if (result.Success)
                    {
                        dbUser = ((APIUser)result.Data);
                        List<ClientViewModel> clients = dbUser.Clients.Select(c => new ClientViewModel() { ClientId = c.ClientId, Name = c.Client == null ? string.Empty : c.Client.Name }).ToList();
                        UserViewModel resultViewModelData = dbUser.ConvertObject<UserViewModel>();
                        resultViewModelData.ClientUsers = dbUser.Clients.ToList().ConvertList<APIClientUser, ClientUserViewModel>();
                        resultViewModelData.Clients = clients;
                        resultViewModelData.Roles = _roleRepo.GetUserRole(dbUser.Id).ConvertList<APIRole, RoleViewModel>();

                        result.Data = resultViewModelData;

                        string operation = isNewData ? "created" : "updated";

                    }

                    return Json(result);
                }
                catch (Exception ex)
                {
                    return Json(new ResponseMessage { Success = false, Status = ResponseStatus.Error, Message = "Failed to save user! " + GetInnerException(ex).Message });
                }

            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to save user! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
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
            _userRepo.SetUserLogin(username);
            _roleRepo.SetUserLogin(username);
            _dealerRepo.SetUserLogin(username);
            _permissionRepo.SetUserLogin(username);
            _clientRepo.SetUserLogin(username);
            _userPermissionRepo.SetUserLogin(username);
            _clientUserRepo.SetUserLogin(username);
        }
        #endregion

        #region Method Get Permission
        [HttpPost]
        public IHttpActionResult GetUserPermissions(LoginUserViewModel user)
        {
            if (user == null)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "User cannot be null" });
            }

            try
            {
                APIClientUser clientUser = _clientUserRepo.GetByUserIdAndClientId(user.UserId, new Guid(user.ClientId));

                if (clientUser != null)
                {
                    List<EndpointPermissionViewModel> userPermissions = _userPermissionRepo.GetUserPermissionByClientUserId(clientUser.Id).Select(up =>
                    {
                        if (up.Permission != null && !up.IsDismantledPermission)
                        {
                            return new EndpointPermissionViewModel() { Id = up.Permission.Id, PermissionCode = up.Permission.PermissionCode };
                        }

                        return null;
                    }).ToList();

                    if (!IsDMSAdmin)
                    {
                        userPermissions = userPermissions.Where(p => !Constants.RestrictedPermissions.Contains(p.PermissionCode)).ToList();
                    }

                    return Json(new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Data = userPermissions
                    });
                }

                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "ClientId and UserId is not valid" });
            }
            catch (Exception ex)
            {

                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = ex.Message });
            }
        }
        #endregion

        #region Upload Template
        [HttpGet]
        public IHttpActionResult GetUploadTemplate()
        {
            MemoryStream documentStream = GenerateTemplateData();
 
            // Make Sure Document is Loaded
            if (documentStream != null & documentStream.Length > 0)
            {
                // Generate dynamic name for BOL document "BOL-XXXXXX.PDF or .XLSX"
                return ReturnStreamAsFile(documentStream, "UploadUserTemplate.xlsx");
            }
 
            // If something fails or somebody calls invalid URI, throw error.
            return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Error generate template" });
        }

        private MemoryStream GenerateTemplateData()
        {
            try
            {
                // Template File
                string templateDocument =
                    HttpContext.Current.Server.MapPath("~/Uploads/TemplateUserUpload.xlsx");

                // Results Output
                MemoryStream output = new MemoryStream();

                // Read Template
                using (FileStream templateDocumentStream = File.OpenRead(templateDocument))
                {
                    // Create Excel EPPlus Package based on template stream
                    using (ExcelPackage package = new ExcelPackage(templateDocumentStream))
                    {
                        // Grab the sheet with the template, sheet name is "BOL".
                        ExcelWorksheet sheetClients = package.Workbook.Worksheets["Clients Data"];
                        ExcelWorksheet sheetRoles = package.Workbook.Worksheets["Roles Data"];

                        var clientsData = _clientRepo.GetAll();
                        for (int i = 1; i <= clientsData.Count; i++)
                        {
                            sheetClients.Cells[i + 1, 1].Value = clientsData[i - 1].Name;
                        }

                        var rolesData = _roleRepo.GetAll();
                        for (int i = 1; i <= rolesData.Count; i++)
                        {
                            sheetRoles.Cells[i + 1, 1].Value = rolesData[i - 1].Name;
                        }

                        package.SaveAs(output);
                    }
                    return output;
                }
            }
            catch (Exception e)
            {
                // Log Exception
                return null;
            }
        }
        
        private IHttpActionResult ReturnStreamAsFile(MemoryStream stream, string filename)
        {
            // Set HTTP Status Code
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            // Reset Stream Position
            stream.Position = 0;
            result.Content = new StreamContent(stream);

            // Generic Content Header
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

            //Set Filename sent to client
            result.Content.Headers.ContentDisposition.FileName = filename;

            return ResponseMessage(result);
        }

        #endregion

        #region Upload User
        [HttpPost]
        public IHttpActionResult Upload()
        {
            try
            {
                //Create the Directory.
                string path = HttpContext.Current.Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the Files.                
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                string fileName = path + Guid.NewGuid().ToString() + ".xlsx";
                postedFile.SaveAs(fileName);
                
                //Read User Data
                var userData = GetUserData(fileName);

                foreach (UserViewModel uvm in userData)
                {
                    IHttpActionResult message = this.SaveWithRepositories(uvm, true);
                    JsonResult<ResponseMessage> responseResult = (JsonResult<ResponseMessage>)message;
                    ResponseMessage responseMessage = responseResult.Content;

                    if (!responseMessage.Success)
                    {
                        return Json(new ResponseMessage()
                        {
                            Success = false,
                            Status = ResponseStatus.Error,
                            Data = uvm
                        });
                    }
                }

                //Send OK Response to Client.
                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Data = userData
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = ex.Message });
            }
        }

        private List<UserViewModel> GetUserData(string path)
        {
            List<UserViewModel> lstUserVM = new List<UserViewModel>();
            var excelFile = new ExcelQueryFactory(path);
            var UserInfo = (from a in excelFile.Worksheet<ExcelUserInfo>("User Info") select a).ToList<ExcelUserInfo>();            
            var UserClient = (from a in excelFile.Worksheet<ExcelUserClient>("User Clients") select a).ToList<ExcelUserClient>();
            var UserRole = (from a in excelFile.Worksheet<ExcelUserRole>("User Roles") select a).ToList<ExcelUserRole>();

            var dealerCodes = (from a in UserInfo select a.DealerCode).Distinct().ToList();
            Dictionary<string, Dealer> dealerDict = new Dictionary<string, Dealer>();
            foreach (string dealerCode in dealerCodes)
            {
                Dealer deal = _dealerRepo.GetByCode(dealerCode);
                dealerDict.Add(dealerCode, deal);
            }

            var clientsData = _clientRepo.GetAll();
            Dictionary<string, APIClient> clientsDict = new Dictionary<string, APIClient>();
            foreach( APIClient cli in clientsData)
            {
                clientsDict.Add(cli.Name, cli);
            }

            var rolesData = _roleRepo.GetAll();
            Dictionary<string, APIRole> rolesDict = new Dictionary<string, APIRole>();
            foreach (APIRole role in rolesData)
            {
                rolesDict.Add(role.Name, role);
            }

            foreach (ExcelUserInfo ui in UserInfo)
            {
                ui.NewPassword = "test123";
                ui.ConfirmPassword = ui.NewPassword;

                Dealer d;
                if (dealerDict.TryGetValue(ui.DealerCode, out d))
                {
                    ui.DealerId = d.Id;
                    //ui.Dealer = d;
                }

                var listClients = (from b in UserClient where b.Username == ui.UserName select b.Client).ToList<string>();
                ui.Clients = new List<APIClient>();
                foreach (string cli in listClients)
                {
                    APIClient item;
                    if (clientsDict.TryGetValue(cli,out item))
                    {
                        ui.Clients.Add(item);
                    }
                }

                var listRoles = (from b in UserRole where b.Username == ui.UserName select b.Role).ToList<string>();
                ui.Roles = new List<APIRole>();
                foreach (string role in listRoles)
                {
                    APIRole item;
                    if (rolesDict.TryGetValue(role, out item))
                    {
                        ui.Roles.Add(item);
                    }
                }

                UserViewModel uvm = ui.ConvertObject<UserViewModel>();
                uvm.UserPermissions = new List<UserPermissionViewModel>();
                lstUserVM.Add(uvm);
            }
            return lstUserVM;

        }
        #endregion

        [HttpGet]
        //[PermissionAuthorize(PermissionName = FrameworkConstants.Permissions.WebUI_ClientRolePermission_Read)]
        public IHttpActionResult GetUnassignedUsers(Guid clientId)
        {
            ResponseMessage response = new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = _userRepo.GetUnassignedUsers(clientId)
            };
            return Json(response);
        }

        [HttpGet]
        //[PermissionAuthorize(PermissionName = FrameworkConstants.Permissions.WebUI_ClientRolePermission_Read)]
        public IHttpActionResult GetUsersByClientId(Guid clientId)
        {
            ResponseMessage response = new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = _userRepo.GetUsersByClientId(clientId)
            };
            return Json(response);
        }
        
    }

    public class ExcelUserInfo
    {
        [ExcelColumn("Username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [ExcelColumn("First Name")] 
        public string FirstName { get; set; }
        [ExcelColumn("Last Name")] 
        public string LastName { get; set; }
        [ExcelColumn("Phone")]
        public string PhoneNumber { get; set; }
        [ExcelColumn("Street")] 
        public string Street1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [ExcelColumn("Postal Code")] 
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }
        [ExcelColumn("Dealer Code")] 
        public string DealerCode { get; set; }
        public short DealerId { get; set; }
        //public Dealer Dealer { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public List<APIClient> Clients { get; set; }
        public List<APIRole> Roles { get; set; }
    }

    public class ExcelUserClient
    {
        public string Username { get; set; }
        public string Client { get; set; }
    }

    public class ExcelUserRole
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }

}
