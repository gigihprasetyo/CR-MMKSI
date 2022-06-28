
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace KTB.DNet.Interface.Repository
{
    public class ClientUserRepository : BaseRepository, IClientUserRepository<APIClientUser, int>
    {
        /// <summary>
        /// ClientUserRepository Constructor
        /// </summary>
        public ClientUserRepository()
        {
        }

        /// <summary>
        /// Get all client UserToClient
        /// </summary>
        /// <returns></returns>
        public List<APIClientUser> GetAll()
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.ClientUsers.ToList();
            }
        }

        /// <summary>
        /// Get UserToClient by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIClientUser Get(int id)
        {
            APIClientUser client = null;
            if (id != 0)
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    client = db.ClientUsers.Include(c => c.Client).Include(c => c.User).FirstOrDefault(x => x.Id == id);
                }
            }

            return client;
        }

        /// <summary>
        /// Get UserToClient by userid and clientid
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public APIClientUser GetByUserIdAndClientId(int userId, Guid clientId)
        {
            APIClientUser client = null;
            if (userId != 0 && clientId != null)
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    client = db.ClientUsers.Include(c => c.Client).Include(c => c.User).FirstOrDefault(x => x.UserId == userId && x.ClientId == clientId);
                }
            }

            return client;
        }

        /// <summary>
        /// Get ClientUser by User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<APIClientUser> GetByUserId(int userId)
        {
            List<APIClientUser> clients = null;
            if (userId != 0)
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    clients = db.ClientUsers.Include(c => c.Client).Include(c => c.User).Where(x => x.UserId == userId).ToList();
                }
            }

            return clients;
        }

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
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    clients = db.ClientUsers.Include(c => c.Client).Include(c => c.Client.MsApplication).Include(c => c.User).Where(x => (x.UserId == userId) && (x.Client.MsApplication.Name.Contains(name))).ToList();
                }
            }

            return clients;
        }

        /// <summary>
        /// Validate token expire
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsTokenExpired(APIClientUser user, DateTime today)
        {
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

        /// <summary>
        /// Update ClientUser
        /// </summary>
        /// <param name="entity"></param>
        public ResponseMessage Update(APIClientUser entity)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var clientUser = db.ClientUsers.Find(entity.Id);

                    clientUser.LastActivity = entity.LastActivity;
                    clientUser.LastLogin = entity.LastLogin;
                    clientUser.Token = entity.Token;
                    clientUser.TokenExpired = entity.TokenExpired;

                    SetLastModifiedLog(clientUser);

                    db.SaveChanges();
                }

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "User's clients has been successfully updated" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update Client User Separately
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        /// <param name="listOfUserPermission"></param>
        /// <returns></returns>
        public ResponseMessage SaveClientUser(int userId, List<Guid> listOfClientId)
        {
            if (listOfClientId != null && listOfClientId.Count > 0)
            {
                using (DNETInterfaceDBContext dbContext = new DNETInterfaceDBContext())
                {
                    using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            APIUser existingUser = dbContext.Users.FirstOrDefault(u => u.Id == userId);
                            List<APIClientUser> listOfClientUser = new List<APIClientUser>();
                            foreach (Guid clientUserGuid in listOfClientId)
                            {
                                APIClient client = dbContext.Clients.FirstOrDefault(c => c.ClientId == clientUserGuid);

                                // check if UserClient already exist
                                APIClientUser existingClientUser = dbContext.ClientUsers.FirstOrDefault(cu => cu.UserId == userId && cu.ClientId == client.ClientId);
                                if (existingClientUser != null)
                                {
                                    listOfClientUser.Add(existingClientUser);
                                }

                                else
                                {
                                    // new UserClient
                                    APIClientUser newClientUser = new APIClientUser();
                                    newClientUser.User = existingUser;
                                    newClientUser.UserId = existingUser.Id;
                                    newClientUser.Client = client;
                                    newClientUser.ClientId = client.ClientId;

                                    listOfClientUser.Add(newClientUser);
                                }

                            }
                            SaveClientUser(dbContext, existingUser, listOfClientUser);
                            transaction.Commit();
                            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Client User has been successully created.", Data = listOfClientUser };
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = GetInnerException(ex).Message };
                        }
                    }
                }
            }
            else
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Client has not been selected or is not valid" };
            }
        }

        /// <summary>
        /// Save Client User together with User, Roles, and Permissions
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        /// <param name="listOfUserPermission"></param>
        /// <returns></returns>
        public void SaveClientUser(DNETInterfaceDBContext dbContext, APIUser user, List<APIClientUser> listOfClientUser)
        {

            try
            {
                foreach (APIClientUser clientUser in listOfClientUser)
                {
                    if (clientUser.UserId != user.Id)
                    {
                        throw new Exception("Client User and User does not match");
                    }
                }

                List<APIClientUser> listOfExistingClient = new List<APIClientUser>();
                List<APIUserRole> listOfExistingRole = new List<APIUserRole>();

                // add client user
                listOfExistingClient = dbContext.ClientUsers.Where(clientUser => clientUser.UserId == user.Id).ToList();
                List<Guid> listOfExistingClientId = listOfExistingClient.Select(clientUser => clientUser.ClientId).ToList();

                // add new client, except the administrator client
                List<APIClientUser> listOfNewClientUser = listOfClientUser.Where(
                                                                    clientUser => !listOfExistingClientId.Contains(clientUser.ClientId)
                                                                    ).ToList();

                if (listOfNewClientUser != null && listOfNewClientUser.Count > 0)
                {
                    // add client user
                    dbContext.ClientUsers.AddRange(listOfNewClientUser);
                }

                dbContext.SaveChanges();

                listOfExistingClient.AddRange(listOfNewClientUser);
                listOfExistingRole = dbContext.UserRoles.Where(userRole => userRole.UserId == user.Id).ToList();

                // add user permission
                AddPermissionBasedOnAdditionalClient(dbContext, listOfNewClientUser, listOfExistingRole);
                dbContext.SaveChanges();

                List<Guid> listOfSelectedClientId = listOfClientUser.Select(c => c.ClientId).ToList();
                if (user.Id != AppConfigs.GetInt("DMSAdminUserId"))
                {
                    List<APIClientUser> listOfRemovedClientUser = listOfExistingClient.Where(c => !listOfSelectedClientId.Contains(c.ClientId)).ToList();
                    List<Guid> listOfRemovedClientId = listOfRemovedClientUser.Select(c => c.ClientId).ToList();

                    if (listOfClientUser != null && listOfClientUser.Count() > 0)
                    {
                        // remove user permission
                        // remove all user permission
                        List<int> listOfClientUserId = listOfRemovedClientUser.Select(cu => cu.Id).ToList();
                        dbContext.UserPermissions.RemoveRange(dbContext.UserPermissions.Where(up => listOfClientUserId.Contains(up.ClientUserId) && !up.IsCustomPermission && !up.IsDismantledPermission));

                        // remove client user
                        dbContext.ClientUsers.RemoveRange(listOfRemovedClientUser);
                    }
                    dbContext.SaveChanges();
                }

                List<APIClientUser> listOfCurrentClientUser = new List<APIClientUser>();
                listOfExistingClient.ForEach(c =>
                {
                    if (listOfSelectedClientId.Contains(c.ClientId))
                    {
                        listOfCurrentClientUser.Add(c);
                    }

                });

            }
            catch (Exception ex)
            {
                throw ex;
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
