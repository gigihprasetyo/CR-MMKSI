using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence;
using KTB.DNet.Interface.Persistence.Stores;
using KTB.DNet.Interface.Repository.Interface;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository
{
    public class UserRepository : BaseRepository, IUserRepository<APIUser, int>
    {
        /// <summary>
        ///     Get all user
        /// </summary>
        /// <returns>An IQueryable of all 'ApplicationUser' entities.</returns>
        public List<APIUser> GetAll()
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
                {
                    return userManager.Users.ToList();
                }
            }
            catch (Exception ex)
            {

                return new List<APIUser>();
            }
        }

        /// <summary>
        ///     Gets a ApplicationUser by it's unique ID.
        /// </summary>
        /// <param name="id">The id of the ApplicationUser.</param>
        /// <returns>A single 'ApplicationUser' entity.</returns>
        public APIUser Get(int id)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
                {
                    return userManager.FindUserById(id);
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIUser user)
        {
            if (user == null)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Please input user data. User could not be null." };
            }

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
                {
                    APIUser existingEntity = _taskFactory
                                            .StartNew(() => userManager.FindByIdAsync(user.Id))
                                            .Unwrap()
                                            .GetAwaiter()
                                            .GetResult();

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
                    existingEntity.RoleNames = user.RoleNames;
                    existingEntity.IsActive = user.IsActive;
                    existingEntity.UserName = user.UserName;
                    existingEntity.UpdatedBy = this.UserLogin;
                    existingEntity.UpdatedTime = DateTime.Now;

                    if (!string.IsNullOrEmpty(user.NewPassword))
                    {
                        existingEntity.PasswordHash = userManager.PasswordHasher.HashPassword(user.NewPassword);
                    }

                    IdentityResult result = _taskFactory
                                            .StartNew(() => userManager.UpdateAsync(existingEntity))
                                            .Unwrap()
                                            .GetAwaiter()
                                            .GetResult();
                    if (result.Succeeded)
                    {
                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("User {0} has successfully updated", existingEntity.UserName), Data = existingEntity };
                    }

                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = string.Join(";", result.Errors) };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Get by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public APIUser GetByName(string userName)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
                {
                    return userManager.FindByName(userName);
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public APIUser GetAuthenticatedUser(string username, string password)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
                {

                    APIUser user = _taskFactory
                        .StartNew(() => userManager.FindAsync(username, password))
                        .Unwrap()
                        .GetAwaiter()
                        .GetResult();

                    if (user != null)
                    {
                        if (user.DealerId.HasValue)
                        {
                            var dealer = db.Dealers.FirstOrDefault(d => d.Id == user.DealerId.Value);

                            if (dealer != null)
                            {
                                user.DealerCode = dealer.DealerCode;
                            }


                        }
                    }

                    return user;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// Find user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public APIUser GetAuthenticatedUser(string userName, string password, string dealerCode)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
                {
                    APIUser user = _taskFactory
                                        .StartNew(() => userManager.FindAsync(userName, password))
                                        .Unwrap()
                                        .GetAwaiter()
                                        .GetResult();

                    if (user != null)
                    {
                        if (!user.DealerId.HasValue)
                        {
                            user = null;
                        }
                        else
                        {
                            //check dealer code 
                            var dealer = db.Dealers.FirstOrDefault(d => d.Id == user.DealerId.Value);

                            if (!String.IsNullOrEmpty(dealer.DealerCode) && dealer.DealerCode == dealerCode) //exist and valid
                            {
                                user.DealerCode = dealer.DealerCode;
                            }
                            else
                            {
                                user = null;
                            }
                        }
                    }

                    return user;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// Get user count
        /// </summary>
        /// <returns></returns>
        public int GetUserCount(int userId, int dealerId)
        {
            try
            {
                if (userId == AppConfigs.GetInt("DMSAdminUserId"))
                {
                    //DMS Admin - Get All Users
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
                    {
                        return userManager.Users.Count();
                    }
                }
                else
                {
                    string keyword = "";
                    PropertyInfo orderedProperty = null;
                    int take = 0, skip = 0;
                    bool orderDir = true;
                    int filteredResultsCount;
                    int totalResultsCount;
                    // search the dbase taking into consideration table sorting and paging
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
                    {
                        var result = userManager.Filter(dealerId, keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);
                        if (result == null)
                        {
                            // empty collection...
                            return 0;
                        }

                        return filteredResultsCount;
                    }
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        ///     Deletes a single ApplicationUser.
        /// </summary>
        /// <param name="id">The id of the ApplicationUser to delete.</param>
        public ResponseMessage Delete(int id)
        {
            if (id == AppConfigs.GetInt("DMSAdminUserId"))
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Administrator user is restricted to be deleted." };
            }

            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            using (var transaction = db.Database.BeginTransaction())
            using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
            {
                try
                {
                    var dbUser = _taskFactory
                                            .StartNew(() => userManager.FindByIdAsync(id))
                                            .Unwrap()
                                            .GetAwaiter()
                                            .GetResult();
                    if (dbUser != null)
                    {
                        List<APIClientUser> clients = db.ClientUsers.Where(cu => cu.UserId == id).ToList();
                        RemoveClientUser(db, clients);

                        var result = _taskFactory
                                            .StartNew(() => userManager.DeleteAsync(dbUser))
                                            .Unwrap()
                                            .GetAwaiter()
                                            .GetResult();
                        if (result.Succeeded)
                        {
                            transaction.Commit();
                        }
                    }

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "User with username " + dbUser.UserName + " has successfully deleted." };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
                }

            }

        }

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

        /// <summary>
        /// Filter user
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<APIUser> Search(DataTablePostModel model, int? dealerId, out int filteredResultsCount, out int totalResultsCount)
        {
            string keyword;
            PropertyInfo orderedProperty = null;
            int take, skip;
            bool orderDir;

            GetPostModelData<APIUser>(model, out keyword, "UserName", out orderedProperty, out orderDir, out take, out skip);

            // search the dbase taking into consideration table sorting and paging
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
            {
                var result = userManager.Filter(dealerId, keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);
                if (result == null)
                {
                    // empty collection...
                    return new List<APIUser>();
                }

                return result;
            }
        }

        /// <summary>
        /// Create user with all its clients, roles, and permissions
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResponseMessage CreateWithAllClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission)
        {
            if (user == null)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Please input user data. User could not be null." };
            }

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var isExist = db.Users.Any(x => x.UserName == user.UserName);
                    if (isExist)
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("User with username '{0}' is already exist.", user.UserName) };
                    }

                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            user.Clients = null;
                            user.UserRoles = null;

                            var result = _taskFactory
                                        .StartNew(() => Create(db, user))
                                        .Unwrap()
                                        .GetAwaiter()
                                        .GetResult();


                            if (!result.Succeeded)
                            {
                                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = string.Join(";", result.Errors) };
                            }

                            db.SaveChanges();

                            List<APIClientUser> listOfSelectedClientUser = GetListOfSelectedClient(db, user, listOfSelectedClient);
                            List<APIUserRole> listOfSelectedUserRole = GetListOfSelectedUserRole(db, user, listOfSelectedRole);
                            List<APIUserPermission> listOfUserPermission = GetUserPermission(db, listOfSelectedPermission);

                            SaveClientRolePermission(db, user, listOfSelectedClientUser, listOfSelectedUserRole, listOfUserPermission);

                            transaction.Commit();

                            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New User has successfully created.", Data = user };
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }

        }

        /// <summary>
        /// Update user with all its clients, roles, and permissions
        /// </summary>
        /// <param name="repoModelUser"></param>
        /// <returns></returns>
        public ResponseMessage UpdateWithAllClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission)
        {
            if (user == null)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Please input user data. User could not be null." };
            }

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
                {
                    //Find the user to update
                    var existingEntity = _taskFactory
                                        .StartNew(() => userManager.FindByIdAsync(user.Id))
                                        .Unwrap()
                                        .GetAwaiter()
                                        .GetResult();

                    if (existingEntity == null)
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = string.Format("User with username '{0}' could not be found.", user.UserName) };
                    }

                    using (var transaction = db.Database.BeginTransaction())
                    {
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
                        existingEntity.RoleNames = user.RoleNames;
                        existingEntity.IsActive = user.IsActive;
                        existingEntity.UserName = user.UserName;

                        if (!string.IsNullOrEmpty(user.NewPassword))
                        {
                            existingEntity.PasswordHash = userManager.PasswordHasher.HashPassword(user.NewPassword);
                        }

                        try
                        {

                            var result = _taskFactory
                                        .StartNew(() => userManager.UpdateAsync(existingEntity))
                                        .Unwrap()
                                        .GetAwaiter()
                                        .GetResult();

                            if (!result.Succeeded)
                            {
                                throw new Exception(string.Join(";", result.Errors));
                            }

                            db.SaveChanges();

                            List<APIClientUser> listOfSelectedClientUser = GetListOfSelectedClient(db, existingEntity, listOfSelectedClient);
                            List<APIUserRole> listOfSelectedUserRole = GetListOfSelectedUserRole(db, existingEntity, listOfSelectedRole);
                            List<APIUserPermission> listOfUserPermission = GetUserPermission(db, listOfSelectedPermission);

                            SaveClientRolePermission(db, existingEntity, listOfSelectedClientUser, listOfSelectedUserRole, listOfUserPermission);

                            transaction.Commit();

                            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "User data has successfully updated.", Data = existingEntity };
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }

        }

        public ResponseMessage CreateWithSeparateClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission)
        {
            if (user == null)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Please input user data. User could not be null." };
            }

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var isExist = db.Users.Any(x => x.UserName == user.UserName);
                    if (isExist)
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("User with username '{0}' is already exist.", user.UserName) };
                    }

                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            user.Clients = null;
                            user.UserRoles = null;
                            user.CreatedBy = this.UserLogin;
                            user.CreatedTime = DateTime.Now;
                            user.UpdatedBy = this.UserLogin;
                            user.UpdatedTime = DateTime.Now;

                            var result = _taskFactory
                                        .StartNew(() => Create(db, user))
                                        .Unwrap()
                                        .GetAwaiter()
                                        .GetResult();


                            if (!result.Succeeded)
                            {
                                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = string.Join(";", result.Errors) };
                            }

                            db.SaveChanges();

                            List<APIClientUser> listOfSelectedClientUser = GetListOfSelectedClient(db, user, listOfSelectedClient);
                            List<APIUserRole> listOfSelectedUserRole = GetListOfSelectedUserRole(db, user, listOfSelectedRole);
                            List<APIUserPermission> listOfUserPermission = GetUserPermission(db, listOfSelectedPermission);

                            if (listOfSelectedClientUser == null || listOfSelectedClientUser.Count() < 1)
                            {
                                throw new Exception("Client has not been selected or is not valid");
                            }

                            if (listOfSelectedUserRole == null || listOfSelectedUserRole.Count() < 1)
                            {
                                throw new Exception("Role has not been selected or is not valid");
                            }


                            SaveClientRolePermissionWithRepositories(db, user, listOfSelectedClientUser, listOfSelectedUserRole, listOfUserPermission);

                            transaction.Commit();

                            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New User has successfully created.", Data = user };
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        public ResponseMessage UpdateWithSeparateClientRolePermission(APIUser user, List<APIClient> listOfSelectedClient, List<APIRole> listOfSelectedRole, List<APIUserPermission> listOfSelectedPermission)
        {
            if (user == null)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Please input user data. User could not be null." };
            }

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
                {
                    //Find the user to update
                    var existingEntity = _taskFactory
                                        .StartNew(() => userManager.FindByIdAsync(user.Id))
                                        .Unwrap()
                                        .GetAwaiter()
                                        .GetResult();

                    if (existingEntity == null)
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = string.Format("User with username '{0}' could not be found.", user.UserName) };
                    }

                    using (var transaction = db.Database.BeginTransaction())
                    {
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
                        existingEntity.RoleNames = user.RoleNames;
                        existingEntity.IsActive = user.IsActive;
                        existingEntity.UserName = user.UserName;
                        existingEntity.UpdatedBy = this.UserLogin;
                        existingEntity.UpdatedTime = DateTime.Now;

                        if (!string.IsNullOrEmpty(user.NewPassword))
                        {
                            existingEntity.PasswordHash = userManager.PasswordHasher.HashPassword(user.NewPassword);
                        }

                        try
                        {

                            var result = _taskFactory
                                        .StartNew(() => userManager.UpdateAsync(existingEntity))
                                        .Unwrap()
                                        .GetAwaiter()
                                        .GetResult();

                            if (!result.Succeeded)
                            {
                                throw new Exception(string.Join(";", result.Errors));
                            }

                            db.SaveChanges();

                            transaction.Commit();

                            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "User data has successfully updated.", Data = existingEntity };
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Get List of Permmission by User Name 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<APIUserPermission> GetPermission(string userName)
        {
            var permissions = new List<APIUserPermission>();
            var user = GetByName(userName);
            if (user != null)
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.UserPermissions.Where(p => p.ClientUser.UserId == user.Id).Include(p => p.Permission).ToList();
                }
            }
            return permissions;
        }

        /// <summary>
        /// Get List of Permmission by User Name and clientId 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<APIUserPermission> GetPermission(string userName, Guid clientId)
        {
            var permissions = new List<APIUserPermission>();
            var user = GetByName(userName);
            if (user != null)
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.UserPermissions.Where(p => p.ClientUser.UserId == user.Id && p.ClientUser.ClientId == clientId).Include(p => p.Permission).ToList();
                }
            }
            return permissions;
        }

        /// <summary>
        /// Get client user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<APIClientUser> GetClientUser(int id)
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.ClientUsers.Where(u => u.UserId == id).ToList();
            }
        }

        public ResponseMessage Create(APIUser entity)
        {
            throw new NotImplementedException();
        }

        #region Private Methods
        private void SaveClientRolePermissionWithRepositories(DNETInterfaceDBContext db, APIUser entity, List<APIClientUser> listOfSelectedClientUser, List<APIUserRole> listOfSelectedUserRole, List<APIUserPermission> listOfUserPermission)
        {
            #region add/ remove user permission
            if (listOfUserPermission != null && listOfUserPermission.Count() > 0)
            {
                UserPermissionRepository userPermissionRepo = new UserPermissionRepository();
                userPermissionRepo.SaveUserPermission(db, entity, listOfUserPermission);
            }
            #endregion

            #region add/remove client user
            if (listOfSelectedClientUser != null && listOfSelectedClientUser.Count() > 0)
            {
                ClientUserRepository clientUserRepo = new ClientUserRepository();
                clientUserRepo.SaveClientUser(db, entity, listOfSelectedClientUser);
            }
            #endregion

            #region add/remove roles
            if (listOfSelectedUserRole != null && listOfSelectedUserRole.Count() > 0)
            {
                UserRoleRepository userRoleRepo = new UserRoleRepository();
                userRoleRepo.SaveUserRole(db, entity, listOfSelectedUserRole, listOfSelectedClientUser);
            }
            #endregion
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="db"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<IdentityResult> Create(DNETInterfaceDBContext db, APIUser user)
        {
            using (var userManager = new ApplicationUserManager(new APIUserStore(db)))
            {
                var password = user.NewPassword;
                if (string.IsNullOrEmpty(password))
                {
                    return await userManager.CreateAsync(user);
                }

                return await userManager.CreateAsync(user, password);
            }
        }

        ///// <summary>
        ///// Create User
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        private void SaveClientRolePermission(DNETInterfaceDBContext db, APIUser entity, List<APIClientUser> listOfSelectedClientUser, List<APIUserRole> listOfSelectedUserRole, List<APIUserPermission> listOfUserPermission)
        {
            #region add/ remove user permission
            if (listOfUserPermission != null && listOfUserPermission.Count() > 0)
            {
                int clientUserId = listOfUserPermission[0].ClientUserId;
                // add new custom permission/ update permission
                APIClientUser clientUser = db.ClientUsers.FirstOrDefault(cu => cu.Id == clientUserId);
                listOfUserPermission.ForEach(userPermission =>
                {
                    userPermission.ClientUser = clientUser;

                    APIUserPermission existingUserPermission = db.UserPermissions.FirstOrDefault(up => up.ClientUserId == userPermission.ClientUserId && up.PermissionId == userPermission.PermissionId);
                    if (existingUserPermission != null)
                    {
                        userPermission.Id = existingUserPermission.Id;
                        db.Entry(existingUserPermission).CurrentValues.SetValues(userPermission);
                    }
                    else
                    {
                        db.UserPermissions.Add(userPermission);
                    }

                });
                db.SaveChanges();

                if (entity.Id != AppConfigs.GetInt("DMSAdminUserId"))
                {
                    List<int> listOfSelectedUserPermissionId = listOfUserPermission.Select(up => up.PermissionId).ToList();
                    List<APIUserPermission> listOfRemovedUserPermission = db.UserPermissions.Where(cu => cu.ClientUserId == clientUser.Id && !listOfSelectedUserPermissionId.Contains(cu.PermissionId)).ToList();
                    db.UserPermissions.RemoveRange(listOfRemovedUserPermission);
                    db.SaveChanges();
                }



            }
            #endregion

            #region add/remove client user
            List<APIClientUser> listOfExistingClient = new List<APIClientUser>();
            List<APIUserRole> listOfExistingRole = new List<APIUserRole>();

            // add client user
            listOfExistingClient = db.ClientUsers.Where(clientUser => clientUser.UserId == entity.Id).ToList();
            List<Guid> listOfExistingClientId = listOfExistingClient.Select(clientUser => clientUser.ClientId).ToList();

            // add new client, except the administrator client
            List<APIClientUser> listOfNewClientUser = listOfSelectedClientUser.Where(
                                                                clientUser => !listOfExistingClientId.Contains(clientUser.ClientId)
                                                                ).ToList();

            AddClientUser(db, listOfNewClientUser);
            db.SaveChanges();

            listOfExistingClient.AddRange(listOfNewClientUser);

            listOfExistingRole = db.UserRoles.Where(userRole => userRole.UserId == entity.Id).ToList();

            // add user permission
            AddPermissionBasedOnAdditionalClient(db, listOfNewClientUser, listOfExistingRole);
            db.SaveChanges();

            List<Guid> listOfSelectedClientId = listOfSelectedClientUser.Select(c => c.ClientId).ToList();
            if (entity.Id != AppConfigs.GetInt("DMSAdminUserId"))
            {
                List<APIClientUser> removedClientUser = listOfExistingClient.Where(c => !listOfSelectedClientId.Contains(c.ClientId)).ToList();
                List<Guid> listOfRemovedClientId = removedClientUser.Select(c => c.ClientId).ToList();

                RemoveClientUser(db, removedClientUser);
                db.SaveChanges();
            }

            List<APIClientUser> listOfCurrentClientUser = new List<APIClientUser>();
            listOfExistingClient.ForEach(c =>
            {
                if (listOfSelectedClientId.Contains(c.ClientId))
                {
                    listOfCurrentClientUser.Add(c);
                }

            });

            #endregion

            #region add/remove roles

            List<int> listOfExistingRoleId = listOfExistingRole.Select(userRole => userRole.RoleId).ToList();

            // add new client, except the administrator role
            List<APIUserRole> listOfNewUserRole = listOfSelectedUserRole.Where(userRole => !listOfExistingRoleId.Contains(userRole.RoleId))
                .Select(ur =>
                {
                    ur.UserId = entity.Id;
                    return ur;
                }).ToList();

            AddUserRole(db, listOfNewUserRole);
            db.SaveChanges();

            listOfExistingRole.AddRange(listOfNewUserRole);

            // add user permission
            AddPermissionBasedOnAdditionalRole(db, listOfCurrentClientUser, listOfNewUserRole, entity.Id);
            db.SaveChanges();

            if (entity.Id != AppConfigs.GetInt("DMSAdminUserId"))
            {
                List<int> listOfSelectedRoleId = listOfSelectedUserRole.Select(r => r.RoleId).ToList();
                List<APIUserRole> removedUserRole = listOfExistingRole.Where(r => !listOfSelectedRoleId.Contains(r.RoleId)).ToList();

                RemoveUserRole(db, removedUserRole, listOfSelectedRoleId, listOfCurrentClientUser);
                db.SaveChanges();
            }

            #endregion

        }

        /// <summary>
        /// Get list of selected client
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbUser"></param>
        /// <param name="clients"></param>
        /// <returns></returns>
        private List<APIClientUser> GetListOfSelectedClient(DNETInterfaceDBContext db, APIUser dbUser, List<APIClient> clients)
        {
            List<APIClientUser> listOfSelectedClient = new List<APIClientUser>();


            clients.ForEach(c =>
            {
                APIClient client = db.Clients.FirstOrDefault(cl => cl.ClientId == c.ClientId);
                APIClientUser clientUser = new APIClientUser();
                APIClientUser existingClientUser = db.ClientUsers.Include(cu => cu.Client).Include(cu => cu.User).FirstOrDefault(cu => cu.UserId == dbUser.Id && cu.ClientId == client.ClientId);
                if (existingClientUser != null)
                {
                    clientUser = existingClientUser;
                }

                clientUser.User = dbUser;
                clientUser.UserId = dbUser.Id;
                clientUser.Client = client;
                clientUser.ClientId = c.ClientId;

                //release restriction for DMS Admin
                //if (clientUser.ClientId != new Guid(adminClientId))
                //{
                //    listOfSelectedClient.Add(clientUser);
                //}

                // can insert DMS Admin Client for now
                listOfSelectedClient.Add(clientUser);

            });

            return listOfSelectedClient;
        }

        /// <summary>
        /// Get list of selected user role
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbUser"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        private List<APIUserRole> GetListOfSelectedUserRole(DNETInterfaceDBContext db, APIUser dbUser, List<APIRole> roles)
        {
            List<APIUserRole> listOfSelectedUserRole = new List<APIUserRole>();
            List<APIUserRole> listOfUserRole = db.UserRoles.Where(ur => ur.UserId == dbUser.Id).ToList();
            roles.ForEach(r =>
            {
                APIUserRole userRole;
                APIUserRole existingUserRole = listOfUserRole.FirstOrDefault(ur => ur.RoleId == r.Id);

                if (existingUserRole != null)
                {
                    userRole = existingUserRole;
                }
                else
                {
                    userRole = new APIUserRole() { UserId = dbUser.Id, RoleId = r.Id };
                }
                listOfSelectedUserRole.Add(userRole);
            });

            return listOfSelectedUserRole;
        }

        /// <summary>
        /// Get Domain User Permission 
        /// </summary>
        /// <param name="listOfUserPermission"></param>
        /// <returns></returns>
        private List<APIUserPermission> GetUserPermission(DNETInterfaceDBContext db, List<APIUserPermission> listOfUserPermission)
        {
            List<APIUserPermission> selectedUserPermissions = new List<APIUserPermission>();

            if (listOfUserPermission != null && listOfUserPermission.Count() > 0)
            {
                int clientUserId = listOfUserPermission[0].ClientUserId;
                // add new custom permission/ update permission
                APIClientUser clientUser = db.ClientUsers.Include(c => c.Client).Include(c => c.User).FirstOrDefault(x => x.Id == clientUserId);
                //clientUser.User = dbUser;
                if (clientUser != null)
                {
                    List<APIUserPermission> existingUserPermissions = db.UserPermissions.Where(up => up.ClientUserId == clientUser.Id).Include(up => up.Permission).ToList();

                    listOfUserPermission.ForEach(userPermission =>
                    {

                        APIUserPermission existingUserPermission = existingUserPermissions.FirstOrDefault(up => up.PermissionId == userPermission.PermissionId);
                        APIEndpointPermission endpointPermission = db.EndpointPermissions.FirstOrDefault(ep => ep.Id == userPermission.PermissionId);
                        if (existingUserPermission != null)
                        {
                            existingUserPermission.ClientUser = clientUser;
                            existingUserPermission.Permission = endpointPermission;
                            existingUserPermission.IsCustomPermission = userPermission.IsCustomPermission;
                            existingUserPermission.IsDismantledPermission = userPermission.IsDismantledPermission;

                            selectedUserPermissions.Add(existingUserPermission);
                        }
                        else
                        {
                            APIUserPermission newUserPermission = userPermission.ConvertObject<APIUserPermission>();
                            newUserPermission.ClientUser = clientUser;
                            newUserPermission.Permission = endpointPermission;
                            selectedUserPermissions.Add(newUserPermission);
                        }

                    });
                }

            }

            return selectedUserPermissions;
        }

        /// <summary>
        /// Add User Role
        /// </summary>
        /// <param name="listOfNewUserRole"></param>
        private void AddUserRole(DNETInterfaceDBContext db, List<APIUserRole> listOfNewUserRole)
        {
            if (listOfNewUserRole != null && listOfNewUserRole.Count > 0)
            {
                // add client user
                db.UserRoles.AddRange(listOfNewUserRole);
            }

        }

        /// <summary>
        /// Remove User Role
        /// </summary>
        /// <param name="listOfRemovedUserRole"></param>
        private void RemoveUserRole(DNETInterfaceDBContext db, List<APIUserRole> listOfRemovedUserRole, List<int> listOfUnRemovedRoleId, List<APIClientUser> listOfExistingClientUser)
        {
            // remove user permission
            RemovePermissionBasedOnRemovedRole(db, listOfRemovedUserRole, listOfUnRemovedRoleId, listOfExistingClientUser);

            // remove client user
            db.UserRoles.RemoveRange(listOfRemovedUserRole);
        }

        /// <summary>
        /// Add User Client
        /// </summary>
        /// <param name="listOfNewClientUser"></param>
        private void AddClientUser(DNETInterfaceDBContext db, List<APIClientUser> listOfNewClientUser)
        {
            if (listOfNewClientUser != null && listOfNewClientUser.Count > 0)
            {
                // add client user
                db.ClientUsers.AddRange(listOfNewClientUser);
            }

        }

        /// <summary>
        /// Remove User Client
        /// </summary>
        /// <param name="listOfClientUser"></param>
        private void RemoveClientUser(DNETInterfaceDBContext db, List<APIClientUser> listOfClientUser)
        {
            if (listOfClientUser != null && listOfClientUser.Count() > 0)
            {
                // remove user permission
                RemovePermissionBasedOnRemovedClient(db, listOfClientUser);

                // remove client user
                db.ClientUsers.RemoveRange(listOfClientUser);
            }

        }

        /// <summary>
        /// Delete User Permission
        /// </summary>
        /// <param name="listOfRemovedClientUser"></param>
        private void RemovePermissionBasedOnRemovedClient(DNETInterfaceDBContext db, List<APIClientUser> listOfRemovedClientUser)
        {
            // remove all user permission
            var listOfClientUserId = listOfRemovedClientUser.Select(cu => cu.Id).ToList();
            db.UserPermissions.RemoveRange(db.UserPermissions.Where(up => listOfClientUserId.Contains(up.ClientUserId)
&& !up.IsCustomPermission && !up.IsDismantledPermission));
        }

        /// <summary>
        /// Delete User Permission
        /// </summary>
        /// <param name="listOfRemovedUserRole"></param>
        private void RemovePermissionBasedOnRemovedRole(DNETInterfaceDBContext db, List<APIUserRole> listOfRemovedUserRole, List<int> listOfUnRemovedRoleId, List<APIClientUser> listOfExistingClientUser)
        {
            // remove all user permission based on role permission except the special permission (custom or dismantled)
            if (listOfRemovedUserRole != null && listOfRemovedUserRole.Count() > 0)
            {
                List<int> listOfRemovedRoleId = listOfRemovedUserRole.Select(ur => ur.RoleId).ToList();
                List<APIUserPermission> listOfRemovedUserPermission = new List<APIUserPermission>();

                listOfExistingClientUser.ForEach(clientUser =>
                {
                    List<APIClientRole> unRemovedClientRole = db.ClientRoles.Include(cr => cr.Role).Where(cr => cr.ClientId == clientUser.ClientId && listOfUnRemovedRoleId.Contains(cr.RoleId) && !listOfRemovedRoleId.Contains(cr.Role.Id)).ToList();

                    List<int> unRemovedClientRoleId = unRemovedClientRole.Select(cr => cr.Id).ToList();

                    List<int> unRemovedPermissionId = db.RolePermissions.Where(rp => unRemovedClientRoleId.Contains(rp.ClientRoleId)).Select(rp => rp.PermissionId).ToList();

                    listOfRemovedUserPermission.AddRange(db.UserPermissions.Where(up => up.ClientUserId == clientUser.Id && !unRemovedPermissionId.Contains(up.PermissionId) && !up.IsCustomPermission && !up.IsDismantledPermission).ToList());

                });

                db.UserPermissions.RemoveRange(listOfRemovedUserPermission);
            }

        }

        /// <summary>
        /// Add User Permission
        /// </summary>
        /// <param name="listOfClientUser"></param>
        private void AddPermissionBasedOnAdditionalClient(DNETInterfaceDBContext db, List<APIClientUser> listOfNewClientUser, List<APIUserRole> listOfUserRole)
        {
            List<APIUserPermission> listOfUserPermission = new List<APIUserPermission>();
            List<int> listOfUserRoleId = listOfUserRole.Select(userRole => userRole.RoleId).ToList();

            listOfNewClientUser.ForEach(clientUser =>
            {
                // get distinct permission from role permission based on list of client role
                List<APIEndpointPermission> listOfPermission = db.RolePermissions
                                                            .Include(rp => rp.ClientRole)
                                                            .Where(rp => rp.ClientRole.ClientId == clientUser.ClientId && listOfUserRoleId.Contains(rp.ClientRole.RoleId))
                                                            .GroupBy(rp => rp.PermissionId)
                                                            .Select(rp => rp.FirstOrDefault()).Select(rp => rp.Permission).ToList();
                listOfPermission.ForEach(p =>
                {
                    listOfUserPermission.Add(new APIUserPermission() { Permission = p, ClientUser = clientUser });
                });
            });

            db.UserPermissions.AddRange(listOfUserPermission);

        }

        /// <summary>
        /// Add User Permission
        /// </summary>
        /// <param name="listOfNewClientUser"></param>
        private void AddPermissionBasedOnAdditionalRole(DNETInterfaceDBContext db, List<APIClientUser> listOfClientUser, List<APIUserRole> listOfNewUserRole, int userId)
        {

            List<APIUserPermission> listOfUserPermission = new List<APIUserPermission>();
            List<int> listOfNewUserRoleId = listOfNewUserRole.Select(userRole => userRole.RoleId).ToList();

            listOfClientUser.ForEach(clientUser =>
            {
                List<int> listOfExistingPermissionId = db.UserPermissions.Include(up => up.ClientUser)
                                                                     .Where(userPermission => userPermission.ClientUser.UserId == userId && userPermission.ClientUser.Id == clientUser.Id)
                                                                     .Select(up => up.PermissionId).ToList();

                // get distinct permission from role permission based on list of client role
                List<APIEndpointPermission> listOfNewPermission = db.RolePermissions
                                                            .Include(rp => rp.ClientRole)
                                                            .Where(rp => rp.ClientRole.ClientId == clientUser.ClientId && listOfNewUserRoleId.Contains(rp.ClientRole.RoleId) && !listOfExistingPermissionId.Contains(rp.PermissionId))
                                                            .GroupBy(rp => rp.PermissionId)
                                                            .Select(rp => rp.FirstOrDefault()).Select(rp => rp.Permission).ToList();
                listOfNewPermission.ForEach(p =>
                {
                    listOfUserPermission.Add(new APIUserPermission() { Permission = p, ClientUser = clientUser });
                });
            });

            db.UserPermissions.AddRange(listOfUserPermission);
        }

        #endregion

    }
}
