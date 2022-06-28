#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : User repository class
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
using KTB.DNet.Domain;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.Dealer;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.User;
using KTB.DNet.Interface.Repository.Interface;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class UserRepository : BaseRepository<APIUser>, IUserRepository<APIUser, int>
    {
        private IPasswordHasher passwordHasher = new PasswordHasher();

        #region Constructor
        public UserRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region GetAll
        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        public List<APIUser> GetAll()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIUser>(UserQuery.GetAllUser).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIUser>();
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIUser Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    APIUser user = cn.Query<APIUser>(UserQuery.GetUserById, new { Id = id }).SingleOrDefault();

                    if (user != null)
                    {
                        ClientUserRepository clientUserRepo = new ClientUserRepository(this._connectionString);
                        user.Clients = clientUserRepo.GetByUserId(user.Id);
                    }

                    return user;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIUser user)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            if (user == null)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Please input user data. User could not be null." };
            }

            try
            {
                APIUser existingEntity = Get(user.Id);

                if (existingEntity == null)
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("User with id {0} does not exist", user.Id) }; ;
                }

                existingEntity.Id = user.Id;
                existingEntity.FirstName = user.FirstName;
                existingEntity.LastName = user.LastName;
                existingEntity.PhoneNumber = user.PhoneNumber;
                existingEntity.Email = user.Email;
                existingEntity.Street1 = user.Street1;
                existingEntity.Street2 = user.Street2;
                existingEntity.Street3 = user.Street3;
                existingEntity.City = user.City;
                existingEntity.State = user.State;
                existingEntity.PostalCode = user.PostalCode;
                existingEntity.Country = user.Country;
                existingEntity.Company = user.Company;
                existingEntity.Status = user.Status;
                existingEntity.DealerId = user.DealerId;
                existingEntity.DealerCompanyId = user.DealerCompanyId;
                existingEntity.GroupDealerId = user.GroupDealerId;
                existingEntity.RoleNames = user.RoleNames;
                existingEntity.IsActive = user.IsActive;
                existingEntity.UserName = user.UserName;

                SetLastModifiedLog(existingEntity);

                if (!string.IsNullOrEmpty(user.NewPassword))
                {
                    existingEntity.PasswordHash = passwordHasher.HashPassword(user.NewPassword);
                }

                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(UserQuery.UpdateUser, existingEntity, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("User {0} has successfully updated", existingEntity.UserName);
                responseMessage.Data = existingEntity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update user. " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region GetByName
        /// <summary>
        /// GetByName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public APIUser GetByName(string userName)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIUser>(UserQuery.GetUserByName, new { Username = userName }).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region GetAuthenticatedUser(string username, string password)
        /// <summary>
        /// GetAuthenticatedUser
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public APIUser GetAuthenticatedUser(string username, string password)
        {
            try
            {
                using (var cn = Connection)
                {
                    APIUser user = cn.Query<APIUser>(UserQuery.GetAuthenticatedUser, new { Username = username, DealerCode = string.Empty }).SingleOrDefault();
                    if (user != null)
                    {
                        PasswordVerificationResult verificationPasswordResult = passwordHasher.VerifyHashedPassword(user.PasswordHash, password);
                        if (verificationPasswordResult == PasswordVerificationResult.Failed)
                        {
                            user = null;
                        }
                        else
                        {
                            ClientUserRepository clientUserRepo = new ClientUserRepository(this._connectionString);
                            user.Clients = clientUserRepo.GetByUserId(user.Id);
                        }

                        user.PasswordHash = null;
                    }

                    return user;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region GetAuthenticatedUser(string username, string password, string dealerCode)
        /// <summary>
        /// GetAuthenticatedUser
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        public APIUser GetAuthenticatedUser(string username, string password, string dealerCode)
        {
            try
            {
                if (string.IsNullOrEmpty(dealerCode))
                {
                    return null;
                }

                using (var cn = Connection)
                {
                    APIUser user = cn.Query<APIUser>(UserQuery.GetAuthenticatedUser, new { Username = username, DealerCode = dealerCode }).SingleOrDefault();
                    if (user != null)
                    {
                        //ClientUserRepository clientUserRepo = new ClientUserRepository(this._connectionString);
                        //user.Clients = clientUserRepo.GetByUserId(user.Id);
                        PasswordVerificationResult verificationPasswordResult = passwordHasher.VerifyHashedPassword(user.PasswordHash, password);
                        if (verificationPasswordResult == PasswordVerificationResult.Failed)
                        {
                            user = null;
                        }
                        else
                        {
                            ClientUserRepository clientUserRepo = new ClientUserRepository(this._connectionString);
                            user.Clients = clientUserRepo.GetByUserId(user.Id);
                        }

                        user.PasswordHash = null;
                    }

                    return user;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region GetCount
        /// <summary>
        /// GetCount
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dealerId"></param>
        /// <returns></returns>
        public int GetUserCount(int userId, int dealerId)
        {
            try
            {
                using (var cn = Connection)
                {
                    int? _dealerId = userId == AppConfigs.GetInt("DMSAdminUserId") ? null : (int?)dealerId;
                    return cn.ExecuteScalar<int>(UserQuery.GetUserCount, new { DealerId = _dealerId });
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                if (id == AppConfigs.GetInt("DMSAdminUserId"))
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Administrator user is restricted to be deleted." };
                }

                APIUser user = Get(id);

                if (user != null)
                {

                    ExecuteTransaction(Connection, (connection, transaction) =>
                    {
                        return connection.Execute(UserQuery.DeleteUser, new
                        {
                            Id = id
                        }, transaction);
                    });
                }

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("User with id {0} has been successfully deleted", id);

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete user. " + GetInnerException(ex).Message;
            }
            return responseMessage;
        }
        #endregion

        #region Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        /// <summary>
        /// Search user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APIUser> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            return Search(model, null, out filteredResultsCount, out totalResultsCount);
        }
        #endregion

        #region Search(DataTablePostModel model, int? dealerId, out int filteredResultsCount, out int totalResultsCount)
        /// <summary>
        /// Filter user
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<APIUser> Search(DataTablePostModel model, int? dealerId, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "UserName", out keyword, out orderBy);
                List<APIUser> result = Search<APIUser>((connection, query, sqlParams) =>
                {
                    return connection.Query<APIUser>(query, sqlParams).ToList();
                }, Connection, UserQuery.SearchUser
                , "Id", new { Keyword = keyword, DealerId = dealerId }, orderBy, out filteredResultsCount, model.Start, model.Length);

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIUser>();
            }
        }
        #endregion

        #region CreateWithAllClientRolePermission
        /// <summary>
        /// CreateWithAllClientRolePermission
        /// </summary>
        /// <param name="user"></param>
        /// <param name="listOfSelectedClient"></param>
        /// <param name="listOfSelectedRole"></param>
        /// <param name="listOfSelectedUserPermission"></param>
        /// <returns></returns>
        public ResponseMessage CreateWithAllClientRolePermission(APIUser user,
            List<APIClient> listOfSelectedClient,
            List<APIRole> listOfSelectedRole,
            List<APIUserPermission> listOfSelectedUserPermission)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            if (user == null)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Please input user data. User could not be null." };
            }

            if (listOfSelectedClient == null || listOfSelectedClient.Count() < 1)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Client has not been selected or is not valid." };
            }

            if (listOfSelectedRole == null || listOfSelectedRole.Count() < 1)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Role has not been selected or is not valid." };
            }

            try
            {
                user.CreatedBy = this.UserLogin;
                user.CreatedTime = DateTime.Now;
                user.UpdatedBy = this.UserLogin;
                user.UpdatedTime = DateTime.Now;

                user.PasswordHash = passwordHasher.HashPassword(user.NewPassword);

                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    user.Id = connection.ExecuteScalar<int>(UserQuery.InsertUser, user, transaction);

                    SaveClientRolePermission(connection, transaction, user, listOfSelectedClient, listOfSelectedRole, listOfSelectedUserPermission);
                    return user.Id;
                });

                user.Id = Convert.ToInt32(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New User has been successfully created.";
                responseMessage.Data = user;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create user " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region UpdateWithAllClientRolePermission
        /// <summary>
        /// UpdateWithAllClientRolePermission
        /// </summary>
        /// <param name="user"></param>
        /// <param name="listOfSelectedClient"></param>
        /// <param name="listOfSelectedRole"></param>
        /// <param name="listOfSelectedPermission"></param>
        /// <returns></returns>
        public ResponseMessage UpdateWithAllClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient,
            List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission)
        {
            var responseMessage = new ResponseMessage()
            {
                Success = false,
                Status = ResponseStatus.Error,
                Message = "Please input user data. User could not be null."
            };

            if (user == null)
            {
                return new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = "Please input user data. User could not be null."
                };
            }

            try
            {
                var existingEntity = Get(user.Id);

                if (existingEntity == null)
                {
                    responseMessage = new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Error,
                        Message = string.Format("User with Id '{0}' could not be found.", user.Id)
                    };
                }

                existingEntity.FirstName = user.FirstName;
                existingEntity.LastName = user.LastName;
                existingEntity.PhoneNumber = user.PhoneNumber;
                existingEntity.Email = user.Email;
                existingEntity.Street1 = user.Street1;
                existingEntity.Street2 = user.Street2;
                existingEntity.Street3 = user.Street3;
                existingEntity.City = user.City;
                existingEntity.State = user.State;
                existingEntity.PostalCode = user.PostalCode;
                existingEntity.Country = user.Country;
                existingEntity.Company = user.Company;
                existingEntity.Status = user.Status;
                existingEntity.DealerId = user.DealerId;
                existingEntity.RoleNames = user.RoleNames;
                existingEntity.IsActive = user.IsActive;
                existingEntity.UserName = user.UserName;
                existingEntity.UpdatedBy = this.UserLogin;
                existingEntity.UpdatedTime = DateTime.Now;

                if (!string.IsNullOrEmpty(user.NewPassword))
                {
                    existingEntity.PasswordHash = user.PasswordHash = passwordHasher.HashPassword(user.NewPassword);
                }

                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    var qResult = connection.Execute(UserQuery.UpdateUser, existingEntity, transaction);

                    SaveClientRolePermission(connection, transaction, existingEntity, listOfSelectedClient, listOfSelectedRole, listOfSelectedPermission);
                    return qResult;
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "User has been successfully updated.";
                responseMessage.Data = existingEntity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to update user " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region GetPermission(string userName)
        /// <summary>
        /// Get List of Permmission by User Name 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<APIUserPermission> GetPermission(string userName)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIUserPermission, APIEndpointPermission, APIUserPermission>(
                        UserQuery.GetUserPermissionByUsername,
                        (userPermission, permission) => { userPermission.Permission = permission; return userPermission; },
                        new { Username = userName, ClientId = string.Empty },
                        null, true, "Id,Id").ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIUserPermission>();
            }
        }
        #endregion

        #region GetPermission(string userName, Guid clientId)
        /// <summary>
        /// GetPermission
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<APIUserPermission> GetPermission(string userName, Guid clientId)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<APIUserPermission, APIEndpointPermission, APIUserPermission>(
                        UserQuery.GetUserPermissionByUsername,
                        (userPermission, permission) => { userPermission.Permission = permission; return userPermission; },
                        new { Username = userName, ClientId = clientId.ToString() },
                        null, true, "Id,Id").ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIUserPermission>();
            }
        }
        #endregion

        #region GetClientUser
        /// <summary>
        /// GetClientUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<APIClientUser> GetClientUser(int id)
        {
            ClientUserRepository clientUserRepo = new ClientUserRepository(this._connectionString);
            return clientUserRepo.GetByUserId(id);
        }
        #endregion

        #region Get All Dealer Groups
        public List<DealerGroup> GetAllDealerGroups()
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<DealerGroup>(DealerQuery.GetDealerGroups).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<DealerGroup>();
            }
        }
        #endregion

        #region Get Dealer Company Options
        public List<DealerCompany> GetDealerCompanies(int dealerGroupId = 0)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<DealerCompany>(DealerQuery.GetDealerCompanies,
                        new
                        {
                            @DealerGroupID = dealerGroupId
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<DealerCompany>();
            }
        }
        #endregion

        #region Not Implemented
        public ResponseMessage Create(APIUser entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #region SaveClientRolePermission
        ///// <summary>
        ///// Create User
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        private void SaveClientRolePermission(IDbConnection connection, IDbTransaction transaction, APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfUserPermission)
        {
            #region add/ remove user permission
            UserPermissionRepository userPermissionRepo = new UserPermissionRepository(this._connectionString);
            userPermissionRepo.SetUserLogin(this.UserLogin);
            userPermissionRepo.SaveUserPermission(connection, transaction, user.Id, listOfUserPermission);
            #endregion

            #region add/remove client user
            ClientUserRepository clientUserRepo = new ClientUserRepository(this._connectionString);
            clientUserRepo.SetUserLogin(this.UserLogin);
            List<APIClientUser> listOfExistingClientUser = clientUserRepo.SaveClientUser(connection, transaction, user.Id, listOfSelectedClient);
            user.Clients = listOfExistingClientUser;
            #endregion

            #region add/remove roles
            List<APIUserRole> listOfUserRole = listOfSelectedRole.Select(role => new APIUserRole() { UserId = user.Id, RoleId = role.Id }).ToList();
            UserRoleRepository userRoleRepo = new UserRoleRepository(this._connectionString);
            userRoleRepo.SetUserLogin(this.UserLogin);
            userRoleRepo.SaveUserRole(connection, transaction, user.Id, listOfUserRole, listOfExistingClientUser);
            #endregion
        }
        #endregion
        #endregion

        public ResponseMessage CreateWithSeparateClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission)
        {
            return CreateWithAllClientRolePermission(user, listOfSelectedClient, listOfSelectedRole, listOfSelectedPermission);
        }

        public ResponseMessage UpdateWithSeparateClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission)
        {
            return UpdateWithAllClientRolePermission(user, listOfSelectedClient, listOfSelectedRole, listOfSelectedPermission);
        }

        #region For DNet Use
        public List<APIUser> GetUnassignedUsers(Guid clientId)
        {
            // use Dnet Connection String
            string interfaceConnectionString = AppConfigs.ConnectionString(KTB.DNet.Interface.Framework.Constants.ConnectionStringName.InterfaceConnection);
            // declare connection
            IDbConnection dnetConnection = new SqlConnection(interfaceConnectionString);

            try
            {
                using (var cn = dnetConnection)
                {
                    return cn.Query<APIUser>(UserQuery.GetUnassignedUsers,
                        new
                        {
                            @ClientId = clientId
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIUser>();
            }
        }

        public List<int> GetUsersByClientId(Guid clientId)
        {
            // use Dnet Connection String
            string interfaceConnectionString = AppConfigs.ConnectionString(KTB.DNet.Interface.Framework.Constants.ConnectionStringName.InterfaceConnection);
            // declare connection
            IDbConnection dnetConnection = new SqlConnection(interfaceConnectionString);

            try
            {
                using (var cn = dnetConnection)
                {
                    List<APIUser> userList = cn.Query<APIUser>(UserQuery.GetUsersByClientId,
                        new
                        {
                            @ClientId = clientId
                        }).ToList();
                    return userList.Select(u => u.Id).ToList();
                }

            }
            catch (Exception ex)
            {
                return new List<int>();
            }
        }
        #endregion
    }
}
