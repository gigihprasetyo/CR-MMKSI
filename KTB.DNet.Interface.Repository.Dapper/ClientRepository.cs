#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Client repository class
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
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.Client;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class ClientRepository : BaseRepository<APIClient>, IClientRepository<APIClient, Guid>
    {

        #region Constructor
        public ClientRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Method Get Client Permission
        /// <summary>
        /// Get client permission
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> GetClientPermission(Guid clientId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIEndpointPermission>(ClientQuery.GetClientPermission, new { clientId = clientId }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIEndpointPermission>();
            }
        }
        #endregion

        #region Method Get Client Role
        /// <summary>
        /// Get client role
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<APIRole> GetClientRole(Guid clientId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIRole>(ClientQuery.GetClientRole, new { clientId = clientId }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIRole>();
            }
        }
        #endregion

        #region Method Get User Client
        /// <summary>
        /// Get client user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<APIClient> GetUserClient(APIUser user)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClient>(ClientQuery.GetUserClient, new { userId = user.Id }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIClient>();
            }
        }
        #endregion

        #region Method Get Client By App Id
        /// <summary>
        /// GetByAppId
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<APIClient> GetByAppId(Guid appId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClient>(ClientQuery.GetByAppId, new { appId = appId }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIClient>();
            }
        }
        #endregion

        #region Method Get Client By Name
        /// <summary>
        /// Get Client By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public APIClient GetByName(string name)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<APIClient>(ClientQuery.GetByName, new { @name = name }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Method Get Total Client
        /// <summary>
        /// GetClientCount
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public int GetTotalClient(bool isDMSAdmin, int userId)
        {
            try
            {
                if (isDMSAdmin)
                {
                    using (var cn = Connection)
                    {
                        return cn.ExecuteScalar<int>(ClientQuery.GetTotalClient);
                    }
                }
                else
                {
                    ClientUserRepository clientUserRepo = new ClientUserRepository(this._connectionString);
                    return clientUserRepo.GetTotalClientByUserId(userId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Base Interface

        #region Method Get
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIClient Get(Guid id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClient>(ClientQuery.GetByClientId, new { clientId = id }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Method Delete
        /// <summary>
        /// Delete client
        /// </summary>
        /// <param name="id"></param>
        public ResponseMessage Delete(Guid id)
        {
            try
            {
                if (id != null && id != Guid.Empty)
                {
                    if (id.ToString().ToLower() == AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Administrator client is restricted to be deleted." };
                    }

                    APIClient client = Get(id);

                    if (client != null)
                    {
                        ClientUserRepository clientUserRepo = new ClientUserRepository(this._connectionString);
                        clientUserRepo.SetUserLogin(this.UserLogin);
                        List<APIClientUser> listOfClientUser = clientUserRepo.GetByClientId(id);

                        ClientRoleRepository clientRoleRepo = new ClientRoleRepository(this._connectionString);
                        clientRoleRepo.SetUserLogin(this.UserLogin);
                        List<APIClientRole> listOfClientRole = clientRoleRepo.GetByClientId(id);

                        ClientPermissionRepository clientPermissionRepo = new ClientPermissionRepository(this._connectionString);
                        clientPermissionRepo.SetUserLogin(this.UserLogin);

                        ExecuteTransaction(Connection, (connection, transaction) =>
                        {
                            foreach (APIClientUser clientUser in listOfClientUser)
                            {
                                clientRoleRepo.Delete(connection, transaction, clientUser.ClientId, listOfClientRole, false);
                            }

                            clientUserRepo.RemoveListOfClientUser(connection, transaction, listOfClientUser);

                            clientPermissionRepo.RemoveByClientId(connection, transaction, id);

                            return connection.Execute(ClientQuery.DeleteByClientId, new { ClientId = id }, transaction);

                        });

                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Client has been deleted successfully" };
                    }
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Success, Message = "Client could not be found" };
                }
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Success, Message = "Client could not be found" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }
        #endregion

        #region Method Create
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listOfSelectedRoleId"></param>
        /// <param name="listOfSelectedPermissionId"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIClient entity, List<int> listOfSelectedRoleId, List<int> listOfSelectedPermissionId)
        {
            try
            {
                #region User Input Validation
                if (!(listOfSelectedRoleId != null && listOfSelectedRoleId.Count() > 0))
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Please select at least one role." };
                }

                if (!(listOfSelectedPermissionId != null && listOfSelectedPermissionId.Count() > 0))
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Please select at least one permission." };
                }

                APIClient existingClient = GetByName(entity.Name);
                if (existingClient != null)
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("Client with name {0} has already exist.", entity.Name), Data = entity };
                }
                #endregion

                #region Initialize Other Repository
                ClientPermissionRepository clientPermissionRepo = new ClientPermissionRepository(this._connectionString);
                clientPermissionRepo.SetUserLogin(this.UserLogin);

                ClientRoleRepository clientRoleRepo = new ClientRoleRepository(this._connectionString);
                clientRoleRepo.SetUserLogin(this.UserLogin);
                #endregion

                entity.ClientId = Guid.NewGuid();
                entity.SecretKey = Guid.NewGuid();
                SetCreatedLog(entity);

                #region List Of Client Permission
                List<APIClientPermission> listOfClientPermission = listOfSelectedPermissionId.Select(permissionId =>
                {
                    APIClientPermission clientPermission = new APIClientPermission
                    {
                        PermissionId = permissionId,
                        ClientId = entity.ClientId
                    };

                    SetCreatedLog(clientPermission);
                    return clientPermission;
                }).ToList();
                #endregion

                #region List Of Client Role
                List<APIClientRole> listOfClientRole = listOfSelectedRoleId.Select(roleId =>
                {
                    APIClientRole clientRole = new APIClientRole
                    {
                        RoleId = roleId,
                        ClientId = entity.ClientId
                    };

                    SetCreatedLog(clientRole);
                    return clientRole;

                }).ToList();
                #endregion

                ExecuteTransaction(Connection, (connection, transaction) =>
                {

                    var result = connection.Execute(ClientQuery.InsertClient, entity, transaction);
                    clientPermissionRepo.AddClientPermission(connection, transaction, listOfClientPermission);
                    clientRoleRepo.AddListOfClientRole(connection, transaction, listOfClientRole);
                    return result;
                });

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New Client has been successfully created.", Data = entity };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Failed to create new client : " + GetInnerException(ex).Message };
            }
        }
        #endregion

        #region Method Update
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listOfSelectedRoleId"></param>
        /// <param name="listOfSelectedPermissionId"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIClient entity, List<int> listOfSelectedRoleId, List<int> listOfSelectedPermissionId)
        {
            try
            {
                if (entity != null)
                {
                    #region User input validation
                    if (!(listOfSelectedRoleId != null && listOfSelectedRoleId.Count() > 0))
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Please select at least one role." };
                    }

                    if (!(listOfSelectedPermissionId != null && listOfSelectedPermissionId.Count() > 0))
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Please select at least one permission." };
                    }

                    APIClient existingData = Get(entity.ClientId);
                    if (existingData == null)
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("The client with clientid {0} does not exist", entity.ClientId) };
                    }
                    #endregion

                    #region Client Role Data Changes
                    ClientRoleRepository clientRoleRepo = new ClientRoleRepository(this._connectionString);
                    clientRoleRepo.SetUserLogin(this.UserLogin);

                    List<APIClientRole> listOfExistingClientRole = clientRoleRepo.GetByClientId(entity.ClientId);
                    List<int> listOfExistingRoleId = listOfExistingClientRole.Select(cr => cr.RoleId).ToList();
                    List<APIClientRole> listOfNewClientRole = listOfSelectedRoleId.Where(roleId => !listOfExistingRoleId.Contains(roleId))
                                                                .Select(roleId =>
                                                                {
                                                                    APIClientRole clientRole = new APIClientRole() { ClientId = existingData.ClientId, RoleId = roleId };
                                                                    SetCreatedLog(clientRole);
                                                                    return clientRole;
                                                                }).ToList();

                    List<APIClientRole> listOfRemovedClientRole = listOfExistingClientRole.Where(cr => !listOfSelectedRoleId.Contains(cr.RoleId)).ToList();
                    #endregion

                    #region Client Permission Data Changes
                    ClientPermissionRepository clientPermissionRepo = new ClientPermissionRepository(this._connectionString);
                    clientPermissionRepo.SetUserLogin(this.UserLogin);

                    List<APIClientPermission> listOfExistingClientPermission = clientPermissionRepo.GetByClientId(existingData.ClientId);
                    List<int> listOfExistingPermissionId = listOfExistingClientPermission.Select(cp => cp.PermissionId).ToList();
                    List<APIClientPermission> listOfNewClientPermission = listOfSelectedPermissionId.Where(permissionId => !listOfExistingPermissionId.Contains(permissionId))
                                                                            .Select(permissionId =>
                                                                            {
                                                                                APIClientPermission clientPermission = new APIClientPermission() { ClientId = existingData.ClientId, PermissionId = permissionId };
                                                                                SetCreatedLog(clientPermission);
                                                                                return clientPermission;
                                                                            }).ToList();
                    List<APIClientPermission> listOfRemovedClientPermission = listOfExistingClientPermission.Where(cp => !listOfSelectedPermissionId.Contains(cp.PermissionId)).ToList();
                    #endregion

                    bool isDMSAdminClient = existingData.ClientId.ToString().ToLower().Trim() == AppConfigs.GetString("DMSAdminClientId").ToLower().Trim();

                    existingData.Name = entity.Name;
                    existingData.AppId = entity.AppId;
                    SetLastModifiedLog(existingData);

                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        #region Add/Remove Client Role
                        // Client Role from DMS Admin Client should not be removed
                        if (!isDMSAdminClient)
                        {
                            clientRoleRepo.Delete(connection, transaction, existingData.ClientId, listOfRemovedClientRole, true, listOfExistingClientRole);
                        }

                        clientRoleRepo.AddListOfClientRole(connection, transaction, listOfNewClientRole);
                        #endregion

                        #region Add/Remove Client Permission
                        // Client Permission from DMS Admin Client should not be removed
                        if (!isDMSAdminClient)
                        {
                            clientPermissionRepo.RemoveListOfClientPermission(connection, transaction, existingData.ClientId, listOfRemovedClientPermission.Select(cp => cp.PermissionId).ToList(), true);
                        }

                        clientPermissionRepo.AddClientPermission(connection, transaction, listOfNewClientPermission);
                        #endregion

                        return connection.Execute(ClientQuery.UpdateClient, existingData, transaction);
                    });

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("The client with name {0} has been successfully updated", entity.Name), Data = entity };

                }
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "The client cannot be null" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }
        #endregion

        #region Method Search
        /// <summary>
        /// Filter AppClient
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<APIClient> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "APIClient.Name ASC", out keyword, out orderBy);
                List<APIClient> result = Search<APIClient>((connection, query, sqlParams) =>
                {
                    // query should be wrapped because APIClient and MsApplication have the same column name [Name]
                    query = string.Format("SELECT ClientId, Name, AppId, [MsApplication.Name] AS Name FROM ({0}) A", query);

                    return connection.Query<APIClient, MsApplication, APIClient>(
                    query,
                    (apiclient, msapplication) =>
                    {
                        apiclient.MsApplication = msapplication;
                        return apiclient;
                    },
                    sqlParams,
                    splitOn: "AppId")
                    .Distinct()
                    .ToList();
                },
                Connection, // connection 
                ClientQuery.SearchClient, // query
                "APIClient.Name",                   // default identifier/sorting 
                new { Keyword = keyword }, // sqlParams
                orderBy,                // sorting by (optional) 
                out filteredResultsCount, // total result 
                model.Start,            // start index 
                model.Length            // length of data will be retrieved
                );

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIClient>();
            }
        }
        #endregion

        #endregion

        #region Get All Client
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public List<APIClient> GetAll()
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<APIClient>(ClientQuery.GetAll).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region
        public List<APIClient> GetListById(List<string> clientIdList)
        {
            var guidIdList = new List<Guid>();
            foreach (var clientId in clientIdList)
            {
                var guidId = new Guid(clientId);
                guidIdList.Add(guidId);
            }
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<APIClient>(ClientQuery.GetListById,
                        new
                        {
                            @clientIds = guidIdList
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Not Implemented
        public ResponseMessage Create(APIClient entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(APIClient entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
