#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EndpointPermission controller class
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
using KTB.DNet.Interface.Framework.Enums;
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
    public class EndpointPermissionController : BaseController
    {
        #region Initialize
        private IEndpointPermissionRepository<APIEndpointPermission, int> _permissionRepo;
        private IRolePermissionRepository<APIRolePermission, int> _rolePermissionRepo;
        private IStandardCodeRepository<StandardCode, int> _standardCodeRepo;
        #endregion

        #region Constructor
        public EndpointPermissionController(
            IEndpointPermissionRepository<APIEndpointPermission, int> permissionRepo,
            IRolePermissionRepository<APIRolePermission, int> rolePermissionRepo,
            IStandardCodeRepository<StandardCode, int> standardCodeRepo)
        {
            _permissionRepo = permissionRepo;
            _rolePermissionRepo = rolePermissionRepo;
            _standardCodeRepo = standardCodeRepo;

            _permissionRepo.SetUserLogin(UserName);
            _rolePermissionRepo.SetUserLogin(UserName);
            _standardCodeRepo.SetUserLogin(UserName);
        }
        #endregion

        #region Method Get
        /// <summary>
        /// Get Endpoint Permission By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Permission_Read)]
        public IHttpActionResult Get(int id)
        {
            APIEndpointPermission _permission = _permissionRepo.Get(id);
            EndpointPermissionViewModel permissionViewModel = new EndpointPermissionViewModel
            {
                Id = _permission.Id,
                Name = _permission.Name,
                Description = _permission.Description,
                PermissionCode = _permission.PermissionCode,
                URI = _permission.URI,
                EndpointGroup = _permission.EndpointGroup,
                EndpointType = _permission.EndpointType,
                OperationType = _permission.OperationType,
                IsScheduled = _permission.IsScheduled,
                IsLogEnabled = _permission.IsLogEnabled,
                IsRuntimeLogEnabled = _permission.IsRuntimeLogEnabled,
                CreatedBy = _permission.CreatedBy,
                CreatedTime = _permission.CreatedTime,
                UpdatedBy = _permission.UpdatedBy,
                UpdatedTime = _permission.UpdatedTime
            };
            return Json(new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Data = permissionViewModel });
        }
        #endregion

        #region Method Create
        /// <summary>
        /// Create Endpoint Permission
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Permission_Create)]
        public IHttpActionResult Create(EndpointPermissionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var permission = new APIEndpointPermission()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        PermissionCode = model.PermissionCode,
                        URI = model.URI,
                        EndpointGroup = model.EndpointGroup,
                        EndpointType = model.EndpointType,
                        OperationType = model.OperationType,
                        IsScheduled = model.IsScheduled,
                        IsLogEnabled = model.IsLogEnabled,
                        IsRuntimeLogEnabled = model.IsRuntimeLogEnabled
                    };

                    ResponseMessage result = _permissionRepo.Create(permission);

                    return Json(result);
                }
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Model is not valid!",
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
        /// <summary>
        /// Update Endpoint Permission
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Permission_Update)]
        public IHttpActionResult Update(EndpointPermissionViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var permission = new APIEndpointPermission()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description,
                        PermissionCode = model.PermissionCode,
                        URI = model.URI,
                        EndpointGroup = model.EndpointGroup,
                        EndpointType = model.EndpointType,
                        OperationType = model.OperationType,
                        IsScheduled = model.IsScheduled,
                        IsLogEnabled = model.IsLogEnabled,
                        IsRuntimeLogEnabled = model.IsRuntimeLogEnabled
                    };

                    ResponseMessage result = _permissionRepo.Update(permission);

                    return Json(result);
                }

                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });

            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message });
            }
        }
        #endregion

        #region Method Delete
        ///// <summary>
        ///// Delete client
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        [HttpDelete]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Permission_Delete)]
        public IHttpActionResult Delete(int id)
        {
            if (id > 0)
            {
                ResponseMessage result = _permissionRepo.Delete(id);
                return Json(result);
            }

            return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Id is not valid" });
        }
        #endregion

        #region Method Search
        /// <summary>
        /// Search Endpoint Permission
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Permission_Read)]
        public IHttpActionResult Search(DataTablePostModel postModel)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<APIEndpointPermission> listOfUser = _permissionRepo.Search(postModel, out filteredResultsCount, out totalResultsCount);

                return Json(new
                {
                    Records = listOfUser,
                    TotalRecord = filteredResultsCount
                });
            }
            catch
            {
                return Json(new
                {
                    Records = new List<APIEndpointPermission>(),
                    TotalRecord = 0
                });
            }
        }

        #endregion

        #region Method Get Options
        /// <summary>
        /// Get options for Endpoint Schedule
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Permission_Read)]
        public IHttpActionResult GetOptions()
        {
            List<string> listOfConstantPermissionCode = ConstantHelper.GetListOfConstantValue<string>(typeof(PermissionConstants));
            List<EnumViewModel> listOfPermissionCodeOptions = null;

            if (listOfConstantPermissionCode.Count > 0)
            {
                List<string> unregisteredPermissionCode = _permissionRepo.GetUnregisteredPermissionCode(listOfConstantPermissionCode);
                if (unregisteredPermissionCode.Count > 0)
                {
                    listOfPermissionCodeOptions = unregisteredPermissionCode.Select(p => new EnumViewModel() { Value = p, Text = p }).ToList();
                }
            }

            List<StandardCode> endpointGroups = _standardCodeRepo.GetByCategory("EndpointGroup");
            List<EnumViewModel> listOfEndpointGroupOptions = null;
            if (endpointGroups.Count > 0)
            {
                listOfEndpointGroupOptions = endpointGroups.Select(s => new EnumViewModel() { Value = s.ValueId, Text = s.ValueDesc }).ToList();
            }

            List<EnumViewModel> transactionTypeOptions = Enum.GetValues(typeof(TransactionType))
               .Cast<TransactionType>()
               .Select(t => new EnumViewModel
               {
                   Value = (int)t,
                   Text = t.ToString()
               }).ToList();

            List<EnumViewModel> operationTypeOptions = Enum.GetValues(typeof(OperationType))
               .Cast<OperationType>()
               .Select(t => new EnumViewModel
               {
                   Value = (int)t,
                   Text = t.ToString()
               }).ToList();


            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = new
                {
                    ListOfEndpointTypeOptions = transactionTypeOptions,
                    ListOfOperationTypeOptions = operationTypeOptions,
                    ListOfPermissionCodeOptions = listOfPermissionCodeOptions,
                    ListOfEndpointGroupOptions = listOfEndpointGroupOptions,
                }
            });
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
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Permission_Read)]
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

        #region Method Get Selected Permission
        /// <summary>
        /// Get Selected Permission List
        /// </summary>
        /// <param name="clientRoleId"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Permission_Read)]
        public IHttpActionResult GetSelectedPermission(int clientRoleId)
        {
            try
            {
                List<APIRolePermission> selectedRolePermission = _rolePermissionRepo.GetByClientRoleId(clientRoleId);
                List<int> selectedIds = new List<int>();

                foreach (APIRolePermission rolePermission in selectedRolePermission)
                {
                    if (rolePermission != null)
                    {
                        selectedIds.Add(rolePermission.Id);
                    }
                }
                List<APIEndpointPermission> listOfData = _permissionRepo.GetSelectedPermission(selectedIds);


                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Data = listOfData
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                });
            }
        }
        #endregion

        #region Method Search Selected Permission
        /// <summary>
        /// Search Selected Permission List
        /// </summary>
        /// <param name="postModel"></param>
        /// <param name="clientRoleId"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Permission_Read)]
        public IHttpActionResult SearchSelectedPermission(DataTablePostModel postModel, int clientRoleId = 0)
        {
            try
            {
                int filteredResultsCount;
                int totalResultsCount;

                List<APIEndpointPermission> listOfUser = _permissionRepo.SearchByClientRoleId(postModel, out filteredResultsCount, out totalResultsCount, clientRoleId);

                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Data = new
                    {
                        // this is what datatables wants sending back
                        recordsTotal = totalResultsCount,
                        recordsFiltered = filteredResultsCount,
                        data = listOfUser
                    }
                });

            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message,
                    Data = new
                    {
                        // this is what datatables wants sending back
                        recordsTotal = 0,
                        recordsFiltered = 0,
                        data = new List<APIEndpointPermission>()
                    }
                });

            }
        }
        #endregion

        #region Get Unselected Permission
        /// <summary>
        /// Get Unselected Permission
        /// </summary>
        /// <param name="clientRoleId"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = PermissionConstants.WebUI_Permission_Read)]
        public IHttpActionResult GetUnselectedPermission(int clientRoleId)
        {
            try
            {
                List<APIRolePermission> selectedRolePermission = _rolePermissionRepo.GetByClientRoleId(clientRoleId);
                List<int> selectedIds = new List<int>();

                foreach (APIRolePermission rolePermission in selectedRolePermission)
                {
                    if (rolePermission != null)
                    {
                        selectedIds.Add(rolePermission.Id);
                    }
                }

                List<APIEndpointPermission> listOfData = _permissionRepo.GetUnselectedPermission(selectedIds);

                return Json(new ResponseMessage()
                {
                    Success = true,
                    Status = ResponseStatus.Success,
                    Data = listOfData
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                });
            }
        }
        #endregion

        #region Method SetUserModifier
        /// <summary>
        /// Set modifier for Created By and Updated By
        /// </summary>
        public override void SetUserModifier(string username)
        {
            _permissionRepo.SetUserLogin(username);
            _rolePermissionRepo.SetUserLogin(username);
        }
        #endregion

        #region Method Get Endpoint Group Options
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Permission_Read)]
        public IHttpActionResult GetEndpointGroupOptions()
        {
            List<StandardCode> endpointGroups = _standardCodeRepo.GetByCategory("EndpointGroup");
            List<EnumViewModel> listOfEndpointGroupOptions = null;
            if (endpointGroups.Count > 0)
            {
                listOfEndpointGroupOptions = endpointGroups.Select(s => new EnumViewModel() { Value = s.ValueId, Text = s.ValueDesc }).ToList();
            }

            return Json(new ResponseMessage()
            {
                Success = true,
                Status = ResponseStatus.Success,
                Data = listOfEndpointGroupOptions
            });
        }
        #endregion

        #region Method Get Endpoint Permissions by Endpoint Group
        /// <summary>
        /// Create client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Permission_Read)]
        public IHttpActionResult GetEndpointsByEndpointGroup(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<int> result = _permissionRepo.GetPermissionByEndpointGroup(id);
                    return Json(new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Data = result
                    });
                }

                catch (Exception ex)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message, Data = new List<APIEndpointPermission>() });
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

        #region Method Get Endpoint Permissions by Endpoint Type
        /// <summary>
        /// Create client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Permission_Read)]
        public IHttpActionResult GetEndpointsByEndpointType(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<int> result = _permissionRepo.GetPermissionByEndpointType(id);
                    return Json(new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Data = result
                    });
                }

                catch (Exception ex)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message, Data = new List<APIEndpointPermission>() });
                }
            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to get permission! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
        }
        #endregion

        #region Method Get Endpoint Permissions by Operation Type
        /// <summary>
        /// Create client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Permission_Read)]
        public IHttpActionResult GetEndpointsByOperationType(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<int> result = _permissionRepo.GetPermissionByOperationType(id);
                    return Json(new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Data = result
                    });
                }

                catch (Exception ex)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message, Data = new List<APIEndpointPermission>() });
                }
            }
            else
            {
                return Json(new ResponseMessage
                {
                    Success = false,
                    Status = ResponseStatus.Warning,
                    Message = "Failed to get permission! Model is not valid!",
                    ModelState = ModelStateHelper.GetParseableModelState(ModelState)
                });
            }
        }
        #endregion
        
        #region Method Save Endpoint Permission's Group in Bulk
        /// <summary>
        /// Create client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Permission_Create)]
        public IHttpActionResult SaveEndpointPermissionGroup(EndpointPermissionGroupBulkViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResponseMessage result = _permissionRepo.UpdateEndpointPermissionGroup(model.EndpointIds, model.EndpointGroupId, this.UserName);
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

        #region Method Save Endpoint Permission's Type in Bulk
        /// <summary>
        /// Create client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Permission_Create)]
        public IHttpActionResult SaveEndpointType(EndpointPermissionTypeBulkViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResponseMessage result = _permissionRepo.UpdateEndpointPermissionType(model.EndpointIds, model.EndpointTypeId, this.UserName);
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

        #region Method Save Endpoint Permission's Operation Type in Bulk
        /// <summary>
        /// Create client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Permission_Create)]
        public IHttpActionResult SaveOperationType(EndpointPermissionOperationTypeBulkViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResponseMessage result = _permissionRepo.UpdateOperationType(model.EndpointIds, model.OperationTypeId, this.UserName);
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

        #region Method Get Unselected Endpoint Permissions by Endpoint Group
        /// <summary>
        /// Create client application
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorize(PermissionName = Constants.Permissions.WebUI_Permission_Read)]
        public IHttpActionResult GetAllPermission()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<APIEndpointPermission> result = _permissionRepo.GetAllPermission();
                    return Json(new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Data = result
                    });
                }

                catch (Exception ex)
                {
                    return Json(new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message, Data = new List<APIEndpointPermission>() });
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

    }
}
