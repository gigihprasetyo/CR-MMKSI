#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ClientRole repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 6/12/2018 15:18
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.ClientRole;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class ClientRoleRepository : BaseRepository<APIClientRole>, IClientRoleRepository<APIClientRole, int>
    {
        #region Constructor
        public ClientRoleRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Get By ClientId And RoleId
        /// <summary>
        /// Get Client Role by ClientId and RoleId
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public APIClientRole GetClientRoleByClientIdAndRoleId(Guid clientId, int roleId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClientRole>(ClientRoleQuery.GetClientRoleByClientIdAndRoleId, new { ClientId = clientId, RoleId = roleId }).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get By RoleId
        /// <summary>
        /// Get Client Role by RoleId
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<APIClientRole> GetClientRoleByRoleId(int roleId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClientRole>(ClientRoleQuery.GetClientRoleByRoleId, new { RoleId = roleId }).AsList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Method Get Client Roe By Client Id
        /// <summary>
        /// Method Client Role Get By Client Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<APIClientRole> GetByClientId(Guid clientId)
        {
            try
            {
                try
                {
                    using (var cn = Connection)
                    {
                        return cn.Query<APIClientRole, APIClient, APIRole, APIClientRole>(
                            ClientRoleQuery.GetClientRoleByClientId,
                                (apiClientRole, apiClient, apiRole) =>
                                {
                                    apiClientRole.Client = apiClient;
                                    apiClientRole.Role = apiRole;

                                    return apiClientRole;
                                },
                                new { ClientId = clientId },
                                splitOn: "Id, ClientId, Id"
                            ).AsList();
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return new List<APIClientRole>();
            }
        }
        #endregion

        #region Method Get By Id
        /// <summary>
        /// Get Client Role By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIClientRole Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIClientRole, APIClient, APIRole, APIClientRole>(
                        ClientRoleQuery.GetClientRoleById,
                            (apiClientRole, apiClient, apiRole) =>
                            {
                                apiClientRole.Client = apiClient;
                                apiClientRole.Role = apiRole;

                                return apiClientRole;
                            },
                            new { Id = id },
                            splitOn: "Id, ClientId, Id"
                        )
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method Update
        /// <summary>
        /// Update API Client Role
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIClientRole clientRole)
        {
            try
            {
                APIClientRole clientRoleOnDB = Get(clientRole.Id);
                if (clientRoleOnDB != null)
                {
                    #region Initialize Other Repository
                    var rolePermissionRepo = new RolePermissionRepository(this._connectionString);
                    var userRoleRepository = new UserRoleRepository(this._connectionString);
                    var clientUserRepository = new ClientUserRepository(this._connectionString);
                    var userPermissionRepository = new UserPermissionRepository(this._connectionString);

                    rolePermissionRepo.SetUserLogin(this.UserLogin);
                    userRoleRepository.SetUserLogin(this.UserLogin);
                    clientUserRepository.SetUserLogin(this.UserLogin);
                    userPermissionRepository.SetUserLogin(this.UserLogin);
                    #endregion

                    // selected role permission
                    List<APIRolePermission> listOfSelectedRolePermission = clientRole.RolePermissions.ToList();
                    List<int> listOfSelectedPermissionId = listOfSelectedRolePermission.Select(cp => cp.PermissionId).ToList();

                    // existing role permission on db
                    List<APIRolePermission> listOfExistingPermission = rolePermissionRepo.GetByClientRoleId(clientRoleOnDB.Id);
                    List<int> listOfExistingPermissionId = listOfExistingPermission.Select(cp => cp.PermissionId).ToList();

                    // new role permission
                    List<APIRolePermission> listOfNewRolePermission = listOfSelectedRolePermission.Where(selectedPermission => !listOfExistingPermissionId.Contains(selectedPermission.PermissionId)).ToList();

                    // removed role permission
                    List<APIRolePermission> listOfRemovedRolePermission = listOfExistingPermission.Where(existingPermission => !listOfSelectedPermissionId.Contains(existingPermission.PermissionId)).ToList();
                    List<int> listOfRemovedPermissionId = listOfRemovedRolePermission.Select(r => r.PermissionId).ToList();

                    bool anyAdditionalRolePermission = listOfNewRolePermission != null && listOfNewRolePermission.Count() > 0;
                    bool anyRemovalPermission = listOfRemovedPermissionId != null && listOfRemovedPermissionId.Count() > 0;

                    if (anyRemovalPermission)
                    {
                        if (clientRoleOnDB.ClientId.ToString().ToLower().Trim() == AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
                        {
                            throw new Exception("Administrator role permission is restricted to be deleted.");
                        }
                    }

                    var result = ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        #region Check User Permission
                        List<APIUserRole> listOfUserWhoHasRole = userRoleRepository.GetByRoleId(clientRoleOnDB.RoleId);
                        List<APIUserPermission> listOfAdditionalUserPermission = new List<APIUserPermission>();

                        // unmodified role from user
                        // Key : clientUserId
                        // Value : list of other role which user has (to get unremoved permission from other role)
                        Dictionary<int, List<int>> listOfUnmodifiedUserRole = new Dictionary<int, List<int>>();

                        listOfUserWhoHasRole.ForEach(userRole =>
                        {
                            APIClientUser clientUser = clientUserRepository.GetByUserIdAndClientId(userRole.UserId, clientRoleOnDB.ClientId);

                            if (clientUser != null)
                            {
                                List<int> listOfExistingUserPermissionId = userPermissionRepository.GetUserPermissionByClientUserId(clientUser.Id)
                                                                                 .Select(up => up.PermissionId).ToList();
                                #region Get Additional User Permission
                                if (anyAdditionalRolePermission)
                                {
                                    // get permission from role permission 
                                    List<int> listOfNewPermissionId = listOfNewRolePermission
                                                                                .Where(rp => !listOfExistingUserPermissionId.Contains(rp.PermissionId))
                                                                                .Select(rp => { return rp.PermissionId; }).ToList();

                                    listOfAdditionalUserPermission.AddRange(listOfNewPermissionId.Select(permissionId =>
                                    {
                                        APIUserPermission userPermission = new APIUserPermission
                                        {
                                            PermissionId = permissionId,
                                            ClientUserId = clientUser.Id
                                        };

                                        SetCreatedLog(userPermission);
                                        return userPermission;
                                    }));
                                }
                                #endregion

                                #region Get Other role
                                if (anyRemovalPermission)
                                {
                                    List<int> listOfUnremovedRoleId = userRoleRepository.GetByUserId(userRole.UserId).Where(ur => ur.RoleId != userRole.RoleId).Select(ur => ur.RoleId).ToList();
                                    listOfUnmodifiedUserRole.Add(clientUser.Id, listOfUnremovedRoleId);
                                }
                                #endregion
                            }
                        });
                        #endregion

                        #region Insert Additional Role Permission
                        if (anyAdditionalRolePermission)
                        {
                            listOfNewRolePermission = listOfNewRolePermission.Select(rolePermission =>
                            {
                                SetCreatedLog(rolePermission);
                                return rolePermission;
                            }).ToList();

                            rolePermissionRepo.AddListOfRolePermission(connection, transaction, listOfNewRolePermission);
                        }
                        #endregion

                        #region Insert Additional User Permission
                        if (listOfAdditionalUserPermission != null && listOfAdditionalUserPermission.Count > 0)
                        {
                            userPermissionRepository.InsertUserPermission(connection, transaction, listOfAdditionalUserPermission);
                        }
                        #endregion

                        #region Remove User Permission
                        if (anyRemovalPermission)
                        {
                            foreach (KeyValuePair<int, List<int>> unmodifiedUserRole in listOfUnmodifiedUserRole)
                            {
                                userPermissionRepository.RemovedPermissionButExcludePermissionFromUnremovedRole(connection, transaction, unmodifiedUserRole.Key, listOfRemovedPermissionId, unmodifiedUserRole.Value);
                            }
                        }
                        #endregion

                        #region Remove Client Role Permission
                        if (anyRemovalPermission)
                        {
                            rolePermissionRepo.RemoveListOfRolePermission(connection, transaction, listOfRemovedRolePermission.Select(cr => cr.Id).ToList());
                        }
                        #endregion

                        SetLastModifiedLog(clientRoleOnDB);
                        return connection.ExecuteScalar(ClientRoleQuery.UpdateClientRole, clientRoleOnDB, transaction);

                    });
                }
                else
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Client Role not found." };
                }

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Client Role has been successfully updated." };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = ex.Message };
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="clientId"></param>
        /// <param name="listOfRemovedClientRole"></param>
        /// <param name="withRemovingUserPermission"></param>
        /// <param name="listOfExistingClientRole"></param>
        public void Delete(IDbConnection connection, IDbTransaction transaction, Guid clientId, List<APIClientRole> listOfRemovedClientRole, bool withRemovingUserPermission, List<APIClientRole> listOfExistingClientRole = null)
        {

            if (listOfRemovedClientRole != null && listOfRemovedClientRole.Count() > 0)
            {
                List<int> listOfRemovedClientRoleId = listOfRemovedClientRole.Select(cr => cr.Id).ToList();

                if (withRemovingUserPermission)
                {
                    if (listOfExistingClientRole == null)
                    {
                        listOfExistingClientRole = GetByClientId(clientId);
                    }

                    UserRoleRepository userRoleRepo = new UserRoleRepository(this._connectionString);
                    ClientUserRepository clientUserRepo = new ClientUserRepository(this._connectionString);

                    userRoleRepo.SetUserLogin(this.UserLogin);
                    clientUserRepo.SetUserLogin(this.UserLogin);

                    List<APIClientRole> listOfUnremovedClientRole = listOfExistingClientRole.Where(cr => !listOfRemovedClientRoleId.Contains(cr.Id)).ToList();
                    List<int> listOfUnremovedRoleId = (listOfUnremovedClientRole != null && listOfUnremovedClientRole.Count() > 0) ? listOfUnremovedClientRole.Select(cr => cr.RoleId).ToList() : new List<int>() { -1 };

                    List<APIClientUser> listOfClientUser = clientUserRepo.GetByClientId(clientId);
                    userRoleRepo.RemovePermissionBasedOnRemovedRole(connection, transaction, listOfUnremovedRoleId, listOfClientUser);

                }

                RolePermissionRepository rolePermissionRepo = new RolePermissionRepository(this._connectionString);
                rolePermissionRepo.SetUserLogin(this.UserLogin);

                rolePermissionRepo.RemoveBasedOnRemovedClientRole(connection, transaction, listOfRemovedClientRole);

                connection.Execute(ClientRoleQuery.DeleteListOfClientRole, new { listOfId = listOfRemovedClientRoleId }, transaction);

            }
        }
        #endregion

        #region Add list of client role
        /// <summary>
        /// AddListOfClientRole
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="listOfClientRole"></param>
        public void AddListOfClientRole(IDbConnection connection, IDbTransaction transaction, List<APIClientRole> listOfClientRole)
        {
            if (listOfClientRole != null && listOfClientRole.Count() > 0)
            {
                connection.Execute(ClientRoleQuery.InsertClientRole, listOfClientRole, transaction);
            }
        }
        #endregion

        #region Not Implemented
        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<APIClientRole> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(APIClientRole entity)
        {
            throw new NotImplementedException();
        }

        public List<APIClientRole> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
