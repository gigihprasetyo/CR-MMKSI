#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ClientUser repository class
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
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.ClientUser;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.EndpointPermission;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class ClientUserRepository : BaseRepository<APIClientUser>, IClientUserRepository<APIClientUser, int>
    {
        #region Constructor
        public ClientUserRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Save Client User
        /// <summary>
        /// Save Client User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listOfClientId"></param>
        /// <returns></returns>
        public ResponseMessage SaveClientUser(int userId, List<Guid> listOfClientId)
        {
            if (listOfClientId != null && listOfClientId.Count > 0)
            {
                ResponseMessage responseMessage = new ResponseMessage() { Success = false };

                try
                {
                    List<APIClient> listOfClient = new List<APIClient>();

                    listOfClientId.ForEach(clientId =>
                    {
                        listOfClient.Add(new APIClient() { ClientId = clientId });
                    });

                    List<APIClientUser> result = (List<APIClientUser>)ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return SaveClientUser(connection, transaction, userId, listOfClient);
                    });

                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = "User's clients has been successfully saved";
                    responseMessage.Data = result;
                }
                catch (Exception ex)
                {
                    responseMessage.Status = ResponseStatus.Error;
                    responseMessage.Message = "Failed to save user's clients " + GetInnerException(ex).Message;
                }

                return responseMessage;
            }
            else
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Client has not been selected or is not valid" };
            }
        }
        #endregion

        #region Save Client User
        /// <summary>
        /// Save Client User
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="userId"></param>
        /// <param name="listOfClient"></param>
        /// <returns></returns>
        public List<APIClientUser> SaveClientUser(IDbConnection connection, IDbTransaction transaction, int userId, List<APIClient> listOfClient)
        {
            ClientRepository clientRepo = new ClientRepository(this._connectionString);
            clientRepo.SetUserLogin(this.UserLogin);

            List<APIClientUser> listOfExistingClient = new List<APIClientUser>();
            List<APIUserRole> listOfExistingRole = new List<APIUserRole>();

            // get existing client
            listOfExistingClient = GetByUserId(userId);
            List<Guid> listOfExistingClientId = listOfExistingClient.Select(clientUser => clientUser.ClientId).ToList();

            // filter new client
            List<APIClientUser> listOfNewClientUser = listOfClient.Where(
                                                                client => !listOfExistingClientId.Contains(client.ClientId)
                                                                ).Select(c => new APIClientUser() { ClientId = c.ClientId, UserId = userId }).ToList();

            if (listOfNewClientUser != null && listOfNewClientUser.Count > 0)
            {
                // add new
                listOfNewClientUser.ForEach(clientUser =>
                {
                    SetCreatedLog(clientUser);
                    clientUser.Id = connection.ExecuteScalar<int>(ClientUserQuery.InsertClientUser, clientUser, transaction);
                    clientUser.Client = clientRepo.Get(clientUser.ClientId);
                });
            }

            listOfExistingClient.AddRange(listOfNewClientUser);

            UserRoleRepository userRoleRepo = new UserRoleRepository(this._connectionString);
            userRoleRepo.SetUserLogin(this.UserLogin);

            listOfExistingRole = userRoleRepo.GetByUserId(userId);

            // add user permission
            AddPermissionBasedOnAdditionalClient(connection, transaction, listOfNewClientUser, listOfExistingRole);

            List<Guid> listOfSelectedClientId = listOfClient.Select(c => c.ClientId).ToList();
            if (userId != AppConfigs.GetInt("DMSAdminUserId"))
            {
                List<APIClientUser> removedClientUser = listOfExistingClient.Where(c => !listOfSelectedClientId.Contains(c.ClientId)).ToList();
                List<Guid> listOfRemovedClientId = removedClientUser.Select(c => c.ClientId).ToList();

                if (removedClientUser != null && removedClientUser.Count() > 0)
                {
                    RemoveListOfClientUser(connection, transaction, removedClientUser);

                    listOfExistingClient = listOfExistingClient.Where(cu => !listOfRemovedClientId.Contains(cu.ClientId)).ToList();
                }
            }

            return listOfExistingClient;
        }
        #endregion

        #region RemoveListOfClientUser
        /// <summary>
        /// RemoveListOfClientUser
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="removedClientUser"></param>
        public void RemoveListOfClientUser(IDbConnection connection, IDbTransaction transaction, List<APIClientUser> removedClientUser)
        {
            UserPermissionRepository userPermissionRepo = new UserPermissionRepository(this._connectionString);

            // remove all user permission
            userPermissionRepo.RemoveBasedOnRemovedClientUser(connection, transaction, removedClientUser);

            // remove client user
            connection.Execute(ClientUserQuery.RemoveListOfClientUser, new
            {
                ListOfClientUserId = removedClientUser.Select(cu => cu.Id)
            }, transaction);
        }
        #endregion

        #region GetByUserIdAndClientId
        /// <summary>
        /// GetByUserIdAndClientId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public APIClientUser GetByUserIdAndClientId(int userId, Guid clientId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClientUser, APIUser, APIClient, APIClientUser>(
                        ClientUserQuery.GetClientUserByClientIdAndUserId,
                        (clientUser, user, client) =>
                        {
                            clientUser.User = user;
                            clientUser.Client = client;
                            return clientUser;
                        },
                        new { @UserId = userId, ClientId = clientId }
                        , null, true, "Id,Id,ClientId").SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region IsTokenExpired
        /// <summary>
        /// Check if token has already expired
        /// </summary>
        /// <param name="user"></param>
        /// <param name="today"></param>
        /// <returns></returns>
        public bool IsTokenExpired(APIClientUser user, DateTime today)
        {
            if (user == null)
            { return true; }

            if (string.IsNullOrEmpty(user.Token))
            { return true; }

            if (!user.LastLogin.HasValue || !user.TokenExpired.HasValue) { return true; }

            // user should login again if there's no activity after 7 days
            int tokenLifeTimeWithNoActivity = AppConfigs.GetInt("TokenLifeTimeWithNoActivity");

            var loginDiff = (today - (
                user.LastActivity.HasValue ?
                user.LastActivity.Value :
                user.LastLogin.Value)).TotalDays;

            if (loginDiff > tokenLifeTimeWithNoActivity) { return true; }

            // check token expired date
            int tokenLifeTime = AppConfigs.GetInt("TokenLifeTime");

            var dateDiff = (user.TokenExpired.Value - today).TotalDays;
            if (dateDiff <= 0) { return true; }

            return false;
        }
        #endregion

        #region GetTotalClientByUserId
        /// <summary>
        /// GetTotalClientByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetTotalClientByUserId(int userId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.ExecuteScalar<int>(
                        ClientUserQuery.GetTotalClientByUserId,
                        new { UserId = userId });
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region Get client user by userid
        /// <summary>
        /// Get Client User by userid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<APIClientUser> GetByUserId(int userId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClientUser, APIUser, APIClient, APIClientUser>(
                        ClientUserQuery.GetClientUserByUserId,
                        (clientUser, user, client) =>
                        {
                            clientUser.User = user;
                            clientUser.Client = client;
                            return clientUser;
                        },
                        new { @UserId = userId }
                        , null, true, "Id,Id,ClientId").ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIClientUser>();
            }
        }
        #endregion

        #region Get client user by clientID
        /// <summary>
        /// GetByClientId
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<APIClientUser> GetByClientId(Guid clientId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClientUser>(
                        ClientUserQuery.GetClientUserByClientId,
                        new { @ClientId = clientId }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIClientUser>();
            }
        }
        #endregion

        #region Add User Permission
        /// <summary>
        /// Add User Permission
        /// </summary>
        /// <param name="listOfClientUser"></param>
        private void AddPermissionBasedOnAdditionalClient(IDbConnection connection, IDbTransaction transaction, List<APIClientUser> listOfNewClientUser, List<APIUserRole> listOfUserRole)
        {
            if (listOfUserRole == null ||
                listOfNewClientUser == null ||
                listOfUserRole.Count < 1 ||
                listOfNewClientUser.Count < 1)
            { return; }

            List<int> listOfUserRoleId = listOfUserRole.Select(userRole => userRole.RoleId).ToList();
            EndpointPermissionRepository endpointRepo = new EndpointPermissionRepository(this._connectionString);
            UserPermissionRepository userPermissionRepo = new UserPermissionRepository(this._connectionString);

            endpointRepo.SetUserLogin(this.UserLogin);
            userPermissionRepo.SetUserLogin(this.UserLogin);

            listOfNewClientUser.ForEach(clientUser =>
            {
                // get distinct permission from role permission based on list of client role
                List<int> listOfPermission = endpointRepo.GetPermissionByClientAndUserRoles(clientUser.ClientId, listOfUserRoleId);

                if (listOfPermission != null && listOfPermission.Count > 0)
                {
                    userPermissionRepo.SaveUserPermission(connection, transaction, clientUser.UserId, listOfPermission.Select(permissionId => new APIUserPermission() { ClientUserId = clientUser.Id, PermissionId = permissionId }).ToList(), false);
                }
            });
        }
        #endregion

        #region GetAll
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public List<APIClientUser> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClientUser>(ClientUserQuery.GetAllClientUser).ToList();
                }
            }
            catch (Exception)
            {
                return new List<APIClientUser>();
            }
        }
        #endregion

        #region Get by id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIClientUser Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClientUser, APIUser, APIClient, APIClientUser>(
                        ClientUserQuery.GetClientUserById,
                        (clientUser, user, client) =>
                        {
                            clientUser.User = user;
                            clientUser.Client = client;
                            return clientUser;
                        },
                        new { @Id = id }
                        , null, true, "Id,Id,ClientId").SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get ClientUser by User Id and AppName
        /// <summary>
        /// Get ClientUser by User Id and AppName
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<APIClientUser> GetByUserIdAndAppName(int userId, string name)
        {
            List<APIClientUser> clients = null;
            if (userId != 0)
            {
                try
                {
                    using (var cn = Connection)
                    {
                        return cn.Query<APIClientUser, APIUser, APIClient, APIClientUser>(
                            ClientUserQuery.GetClientUserByUserIdAndAppName,
                            (clientUser, user, client) =>
                            {
                                clientUser.User = user;
                                clientUser.Client = client;
                                return clientUser;
                            },
                            new { UserId = userId, AppName = name }
                            , null, true, "Id,Id,ClientId").ToList();
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return clients;
        }
        #endregion

        #region Update Client User return response message
        /// <summary>
        /// Update Client User return response message
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIClientUser entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                APIClientUser clientUser = Get(entity.Id);
                if (clientUser != null)
                {
                    clientUser.LastActivity = entity.LastActivity;
                    clientUser.LastLogin = entity.LastLogin;
                    clientUser.Token = entity.Token;
                    clientUser.TokenExpired = entity.TokenExpired;

                    SetLastModifiedLog(clientUser);

                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(ClientUserQuery.UpdateClientUser, clientUser, transaction);
                    });


                    responseMessage.Success = true;
                    responseMessage.Status = ResponseStatus.Success;
                    responseMessage.Message = string.Format("client user with userId [{0}] and clientId [{1}] has been successfully updated", entity.UserId, entity.ClientId);

                }
                else
                {
                    responseMessage.Status = ResponseStatus.Warning;
                    responseMessage.Message = "client user does not exist";
                }
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update clientUser. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region For DNet Use

        #region Save Client Users in Bulk
        public ResponseMessage SaveClientUserInBulk(Guid clientId, List<APIUser> listOfSelectedUsers)
        {
            // use Dnet Connection String
            var interfaceConnectionString = AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection);
            // declare connection
            IDbConnection dnetConnection = new SqlConnection(interfaceConnectionString);
            var loginuser = this.UserLogin;
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };
            try
            {
                if (listOfSelectedUsers != null)
                {
                    ExecuteTransaction(dnetConnection, (connection, transaction) =>
                    {
                        SaveClientUserInBulk(connection, transaction, clientId, listOfSelectedUsers);
                        return 1;
                    });
                }

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Users for clientId [{0}] has been successully saved", clientId);
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update user's permissions " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region SaveUserClientsInBulk
        /// <summary>
        /// Save User Clients in Bulk
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="clientId"></param>
        /// <param name="listOfUsers"></param>
        /// <param name="doCheckExisting"></param>
        /// <param name="doNothingIfExist"></param>
        private void SaveClientUserInBulk(IDbConnection connection, IDbTransaction transaction, Guid clientId, List<APIUser> listOfUsers, bool doCheckExisting = true)
        {
            var userLogin = this.UserLogin;
            if (listOfUsers != null && listOfUsers.Count() > 0)
            {
                listOfUsers.ForEach(user =>
                {
                    // get existing client user
                    APIClientUser existingClientUser = doCheckExisting ?
                        connection.Query<APIClientUser>(ClientUserQuery.GetClientUserByClientIdAndUserId,
                            new
                            {
                                @ClientId = clientId,
                                @UserId = user.Id
                            },
                            transaction)
                            .SingleOrDefault()
                        : null;

                    if (existingClientUser == null)
                    {
                        // add new
                        var newClientUser = new APIClientUser()
                        {
                            ClientId = clientId,
                            UserId = user.Id
                        };
                        SetCreatedLog(newClientUser);
                        connection.Execute(ClientUserQuery.InsertClientUser,
                            new
                            {
                                @ClientId = newClientUser.ClientId,
                                @UserId = newClientUser.UserId,
                                @CreatedBy = newClientUser.CreatedBy,
                                @CreatedTime = newClientUser.CreatedTime,
                                @UpdatedBy = newClientUser.UpdatedBy,
                                @UpdatedTime = newClientUser.UpdatedTime
                            }
                            , transaction);
                        // get endpoint permission by client id
                        var listOfPermissions = connection.Query<APIEndpointPermission>(EndpointPermissionQuery.GetClientPermission, 
                            new 
                            { 
                                ClientId = clientId 
                            }, transaction).ToList();
                        // get list of user permission
                        var insertedClientUser = connection.Query<APIClientUser, APIUser, APIClient, APIClientUser>(
                        ClientUserQuery.GetClientUserByClientIdAndUserId,
                        (clientUser, existingUser, client) =>
                        {
                            clientUser.User = existingUser;
                            clientUser.Client = client;
                            return clientUser;
                        },
                        new { @UserId = user.Id, ClientId = clientId }
                        , transaction, true, "Id,Id,ClientId").SingleOrDefault();
                        if (insertedClientUser != null)
                        {
                            var listOfUserPermissions = new List<APIUserPermission>();
                            foreach (var permission in listOfPermissions)
                            {
                                var userPermission = new APIUserPermission()
                                {
                                    ClientUserId = insertedClientUser.Id,
                                    PermissionId = permission.Id
                                };
                                listOfUserPermissions.Add(userPermission);
                            }
                            //insert user permission
                            UserPermissionRepository userPermissionRepo = new UserPermissionRepository(connection.ConnectionString);
                            userPermissionRepo.SetUserLogin(this.UserLogin);
                            userPermissionRepo.SaveUserPermission(connection, transaction, user.Id, listOfUserPermissions);
                        }
                        
                    }

                });
            }
            // delete unselected users from the Client, except if the user is DMS Admin User
            List<int> listOfSelectedUserIds = listOfUsers.Select(user => user.Id).ToList();
            
            connection.Execute(ClientUserQuery.RemoveListOfUnselectedClientUser,
                new
                {
                    @ClientId = clientId,
                    @ListOfSelectedUserIds = listOfSelectedUserIds,
                    @DMSUserId = AppConfigs.GetInt("DMSAdminUserId")
                },
                transaction);

            // delete user permissions based on removed client
            
            
        }
        #endregion

        #region Save Client User Permission

        #endregion
        #region Save Client User Permission
        private void SaveClientUserPermission(IDbConnection connection, IDbTransaction transaction, APIUser user, List<APIUserPermission> listOfUserPermission)
        {
            var interfaceConnectionString = AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection);

            #region add/ remove user permission
            UserPermissionRepository userPermissionRepo = new UserPermissionRepository(interfaceConnectionString);
            userPermissionRepo.SetUserLogin(this.UserLogin);
            userPermissionRepo.SaveUserPermission(connection, transaction, user.Id, listOfUserPermission);
            #endregion

        }
        #endregion
        #endregion

        #region Not Implemented
        public ResponseMessage Create(APIClientUser entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<APIClientUser> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
