#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EndpointPermission repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:42
//
// ===========================================================================	
#endregion


#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.EndpointPermission;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FrameworkConstants = KTB.DNet.Interface.Framework.Constants;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class EndpointPermissionRepository : BaseRepository<APIEndpointPermission>, IEndpointPermissionRepository<APIEndpointPermission, int>
    {
        #region Constructor
        public EndpointPermissionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Method Get By Name
        /// <summary>
        /// Get Endpoint By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public APIEndpointPermission GetByName(string name)
        {
            using (var cn = Connection)
            {
                return cn.Query<APIEndpointPermission>(EndpointPermissionQuery.GetEndpointPermissionByName, new { Name = name }).SingleOrDefault();
            }
        }
        #endregion

        #region Method Get By Uri
        /// <summary>
        /// Get Endpoint by Uri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public APIEndpointPermission GetByUri(string uri)
        {
            using (var cn = Connection)
            {
                return cn.Query<APIEndpointPermission>(EndpointPermissionQuery.GetEndpointPermissionByUri, new { Uri = uri }).SingleOrDefault();
            }
        }
        #endregion

        #region Method Get Client Permission
        /// <summary>
        /// Get Client Permission
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> GetClientPermission(Guid clientId)
        {
            using (var connection = Connection)
            {
                return connection.Query<APIEndpointPermission>(EndpointPermissionQuery.GetClientPermission, new { ClientId = clientId }).ToList();
            }
        }
        #endregion

        #region Method Search By Client Role Id
        /// <summary>
        /// Search Endpoint by Client Role Id
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <param name="clientRoleId"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> SearchByClientRoleId(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, int clientRoleId)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "Name", out keyword, out orderBy);

                List<APIEndpointPermission> result = Search<APIEndpointPermission>((connection, query, sqlParams) =>
                {
                    return connection.Query<APIEndpointPermission>(query, sqlParams).ToList();
                },
                Connection,
                EndpointPermissionQuery.SearchEndpointPermissionByClientId,
                "Id",
                new
                {
                    Keyword = keyword,
                    ClientRoleId = clientRoleId
                },
                orderBy,
                out filteredResultsCount,
                model.Start,
                model.Length);

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIEndpointPermission>();
            }
        }
        #endregion

        #region Method Search
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <param name="isScheduled"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, bool isScheduled = false)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "Name", out keyword, out orderBy);

                List<APIEndpointPermission> result = Search<APIEndpointPermission>((connection, query, sqlParams) =>
                {
                    return connection.Query<APIEndpointPermission>(query, sqlParams).ToList();
                },
                Connection,
                EndpointPermissionQuery.SearchEndpointPermission,
                "Id",
                new
                {
                    Keyword = keyword,
                    IsScheduled = isScheduled
                },
                orderBy,
                out filteredResultsCount,
                model.Start,
                model.Length);

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIEndpointPermission>();
            }
        }
        #endregion

        #region Method Get Selected Permission
        /// <summary>
        /// Get Selected Permission
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> GetSelectedPermission(List<int> ids)
        {
            using (var cn = Connection)
            {
                return cn.Query<APIEndpointPermission>(EndpointPermissionQuery.GetSelectedPermissions, new { Ids = ids }).ToList();
            }
        }
        #endregion

        #region Method Get Unselected Permission
        /// <summary>
        /// Get Unselected Permission
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> GetUnselectedPermission(List<int> ids)
        {
            using (var cn = Connection)
            {
                return cn.Query<APIEndpointPermission>(EndpointPermissionQuery.GetSelectedPermissions, new { Ids = ids }).ToList();
            }
        }
        #endregion

        #region Method Get Endpoint with no Throttler
        /// <summary>
        /// Get Endpoint with no throttler
        /// </summary>
        /// <returns></returns>
        public List<APIEndpointPermission> GetEndpointWithNoThrottler()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIEndpointPermission>(EndpointPermissionQuery.GetEndpointWithNoThrottler).ToList();
                }
            }
            catch (Exception)
            {

                return new List<APIEndpointPermission>();
            }
        }
        #endregion

        #region Method Get Permission Count
        /// <summary>
        /// Get Permission Count
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetPermissionCount(int userId)
        {
            try
            {
                using (var cn = Connection)
                {
                    if (userId == AppConfigs.GetInt("DMSAdminUserId"))
                    {
                        return cn.Query<int>(EndpointPermissionQuery.GetPermissionCount, new { IsAdmin = 1, UserId = userId }).FirstOrDefault();
                    }
                    else
                    {
                        return cn.Query<int>(EndpointPermissionQuery.GetPermissionCount, new { IsAdmin = 0, UserId = userId }).FirstOrDefault();
                    }
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }
        #endregion

        #region Method Get By Id
        /// <summary>
        /// Method Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIEndpointPermission Get(int id)
        {
            using (var cn = Connection)
            {
                return cn.Query<APIEndpointPermission>(EndpointPermissionQuery.GetEndpointPermissionById, new { Id = id }).SingleOrDefault();
            }
        }
        #endregion

        #region Method Create
        /// <summary>
        /// Method Create
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIEndpointPermission entity)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                SetCreatedLog(entity);
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar<int>(EndpointPermissionQuery.InsertEndpointPermission, entity, transaction);
                });

                entity.Id = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New Endpoint Permission has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Success = false;
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Endpoint Permission. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Method Update
        /// <summary>
        /// Method Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIEndpointPermission entity)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                APIEndpointPermission existingEndpointPermission = Get((int)entity.Id);
                if (existingEndpointPermission != null)
                {
                    SetLastModifiedLog(entity);
                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(EndpointPermissionQuery.UpdateEndpointPermission, entity, transaction);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("Endpoint Permission with name {0} has been successfully updated", entity.Name);
                    responseMessage.Data = entity;
                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "Endpoint Permission does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Endpoint Permission. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Method Delete
        /// <summary>
        /// Method Delete Endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                var permission = Get(id);

                if (permission != null)
                {
                    if (FrameworkConstants.RestrictedPermissions.Contains(permission.Name))
                    {
                        responseMessage.Success = false;
                        responseMessage.Status = ResponseStatus.Warning;
                        responseMessage.Message = "Permission cannot be deleted";
                    }

                    try
                    {
                        ExecuteTransaction(Connection, (connection, transaction) =>
                        {
                            return connection.Execute(EndpointPermissionQuery.DeleteEndpointPermission, new
                            {
                                Id = id
                            }, transaction);
                        });

                        responseMessage.Success = true;
                        responseMessage.Status = ResponseStatus.Success;
                        responseMessage.Message = string.Format("Endpoint Permission with id {0} has been successfully deleted", id);

                    }
                    catch (Exception ex)
                    {
                        responseMessage.Success = false;
                        responseMessage.Status = ResponseStatus.Error;
                        responseMessage.Message = "Failed to delete endpoint permission. " + GetInnerException(ex).Message;
                    }

                    return responseMessage;
                }
                else
                {
                    responseMessage = new ResponseMessage
                                        {
                                            Success = false,
                                            Status = ResponseStatus.Warning,
                                            Message = "Endpoint Permission does not exist."
                                        };
                }
            }
            catch (Exception ex)
            {
                responseMessage = new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                };
            }

            return responseMessage;
        }
        #endregion

        #region Method GetAll
        /// <summary>
        /// Method GetAll Endpoint Permission
        /// </summary>
        /// <returns></returns>
        public List<APIEndpointPermission> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIEndpointPermission>(EndpointPermissionQuery.GetAllEndpointPermission).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIEndpointPermission>();
            }
        }
        #endregion

        #region Method Search
        /// <summary>
        /// Method Search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            return Search(model, out filteredResultsCount, out totalResultsCount, false);
        }
        #endregion

        #region Get permission by client and user
        /// <summary>
        /// Get permission by client and user
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="listOfRoleId"></param>
        /// <returns></returns>
        public List<int> GetPermissionByClientAndUserRoles(Guid clientId, List<int> listOfRoleId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<int>(EndpointPermissionQuery.GetPermissionByClientIdAndUserRoles, new { ClientId = clientId, ListOfRoleId = listOfRoleId }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<int>();
            }
        }
        #endregion

        #region Get unregistered permission code
        /// <summary>
        /// Get unregistered permission code
        /// </summary>
        /// <param name="listOfConstantPermissionCode"></param>
        /// <returns></returns>
        public List<string> GetUnregisteredPermissionCode(List<string> listOfConstantPermissionCode)
        {
            try
            {
                List<string> unregisteredPermissionCode = new List<string>();
                List<string> listOfRegisteredPermissionCode = GetAll().Select(p => p.PermissionCode).ToList();
                if (listOfRegisteredPermissionCode != null && listOfRegisteredPermissionCode.Count > 0)
                {
                    unregisteredPermissionCode = listOfConstantPermissionCode.Where(p => !listOfRegisteredPermissionCode.Contains(p)).OrderBy(p => p).ToList();
                }

                return unregisteredPermissionCode;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        #endregion

        #region Get Permission By Endpoint Group
        public List<int> GetPermissionByEndpointGroup(int endpointGroupId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<int>(EndpointPermissionQuery.GetEndpointPermissionByEndpointGroup, new { @EndpointGroup = endpointGroupId }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<int>();
            }
        }
        #endregion

        #region Get Permission By Endpoint Type
        public List<int> GetPermissionByEndpointType(int endpointTypeId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<int>(EndpointPermissionQuery.GetEndpointPermissionByEndpointType, new { @EndpointType = endpointTypeId }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<int>();
            }
        }
        #endregion

        #region Get Permission By Operation Type
        public List<int> GetPermissionByOperationType(int operationTypeId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<int>(EndpointPermissionQuery.GetEndpointPermissionByOperationType, new { @OperationType = operationTypeId }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<int>();
            }
        }
        #endregion

        #region Get All Permission
        public List<APIEndpointPermission> GetAllPermission()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIEndpointPermission>(EndpointPermissionQuery.GetAllEndpointPermission).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIEndpointPermission>();
            }
        }
        #endregion

        #region Update Endpoint Permission Group
        public ResponseMessage UpdateEndpointPermissionGroup(List<int> endpointIdList, int endpointGroupId, string username)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(EndpointPermissionQuery.UpdateEndpointPermissionGroupInBulk, 
                        new 
                        {
                            @EndpointGroupId = endpointGroupId,
                            @UpdatedBy = username,
                            @EndpointIdList = endpointIdList
                        },
                        transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "Endpoint Permissions Group has been successfully updated";
                
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Endpoint Permissions Group. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Update Endpoint Permission Type
        public ResponseMessage UpdateEndpointPermissionType(List<int> endpointIdList, int endpointTypeId, string username)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(EndpointPermissionQuery.UpdateEndpointTypeInBulk,
                        new
                        {
                            @EndpointTypeId = endpointTypeId,
                            @UpdatedBy = username,
                            @EndpointIdList = endpointIdList
                        },
                        transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "Endpoint Permissions Type has been successfully updated";

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Endpoint Permissions Type. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Update Endpoint Operation Type
        public ResponseMessage UpdateOperationType(List<int> endpointIdList, int operationTypeId, string username)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(EndpointPermissionQuery.UpdateOperationTypeInBulk,
                        new
                        {
                            @OperationTypeId = operationTypeId,
                            @UpdatedBy = username,
                            @EndpointIdList = endpointIdList
                        },
                        transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "Endpoint Permissions Operation Type has been successfully updated";

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update Endpoint Permissions Operation Type. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

    }
}

