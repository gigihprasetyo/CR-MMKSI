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
    public class UserRoleRepository : BaseRepository, IUserRoleRepository<APIUserRole, int>
    {
        /// <summary>
        /// Save User Role Separately
        /// </summary>
        /// <param name="user"></param>
        /// <param name="listOfUserRole"></param>
        /// <param name="listOfClientUser"></param>
        /// <returns></returns>
        public ResponseMessage SaveUserRole(int userId, List<int> listOfRoleId)
        {
            using (DNETInterfaceDBContext dbContext = new DNETInterfaceDBContext())
            {
                if (listOfRoleId != null && listOfRoleId.Count > 0)
                {
                    using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            APIUser existingUser = dbContext.Users.FirstOrDefault(u => u.Id == userId);
                            List<APIUserRole> listOfUserRole = new List<APIUserRole>();
                            List<APIClientUser> listOfClientUser = dbContext.ClientUsers.Where(cu => cu.UserId == userId).ToList();

                            foreach (int roleId in listOfRoleId)
                            {
                                APIRole role = dbContext.Roles.FirstOrDefault(ur => ur.Id == roleId);

                                // check if UserPermission already exist
                                APIUserRole existingUserRole = dbContext.UserRoles.FirstOrDefault(up => up.UserId == existingUser.Id && up.RoleId == roleId);
                                if (existingUserRole != null)
                                {
                                    listOfUserRole.Add(existingUserRole);
                                }

                                else
                                {
                                    // new UserClient
                                    APIUserRole newUserRole = new APIUserRole();
                                    newUserRole.UserId = existingUser.Id;
                                    newUserRole.RoleId = roleId;

                                    listOfUserRole.Add(newUserRole);
                                }

                            }
                            SaveUserRole(dbContext, existingUser, listOfUserRole, listOfClientUser);
                            transaction.Commit();
                            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "User Role has been successully created." };
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = GetInnerException(ex).Message };
                        }
                    }
                }
                else
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Role has not been selected or is not valid" };
                }
            }
        }

        /// <summary>
        /// Save user role together with User, Clients, and Permissions
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        /// <param name="listOfUserRole"></param>
        /// <param name="listOfClientUser"></param>
        public void SaveUserRole(DNETInterfaceDBContext dbContext, APIUser user, List<APIUserRole> listOfUserRole, List<APIClientUser> listOfClientUser)
        {
            try
            {
                if (listOfClientUser == null || listOfClientUser.Count() == 0)
                {
                    listOfClientUser = dbContext.ClientUsers.Where(cu => cu.UserId == user.Id).ToList();
                }

                foreach (APIClientUser clientUser in listOfClientUser)
                {
                    if (clientUser.UserId != user.Id)
                    {
                        throw new Exception("Client User and User does not match");
                    }
                }

                List<APIUserRole> listOfExistingRole = new List<APIUserRole>();
                listOfExistingRole = dbContext.UserRoles.Where(userRole => userRole.UserId == user.Id).ToList();

                List<int> listOfExistingRoleId = listOfExistingRole.Select(userRole => userRole.RoleId).ToList();
                List<APIClientUser> listOfExistingClient = new List<APIClientUser>();
                listOfExistingClient = dbContext.ClientUsers.Where(clientUser => clientUser.UserId == user.Id).ToList();

                List<APIClientUser> listOfCurrentClientUser = new List<APIClientUser>();

                List<Guid> listOfSelectedClientId = listOfClientUser.Select(c => c.ClientId).ToList();
                listOfExistingClient.ForEach(c =>
                {
                    if (listOfSelectedClientId.Contains(c.ClientId))
                    {
                        listOfCurrentClientUser.Add(c);
                    }

                    c.UpdatedBy = this.UserLogin;
                    c.UpdatedTime = DateTime.Now;
                });

                // add new role, except the administrator role
                List<APIUserRole> listOfNewUserRole = listOfUserRole.Where(userRole => !listOfExistingRoleId.Contains(userRole.RoleId))
                    .Select(ur =>
                    {
                        ur.UserId = user.Id;
                        return ur;
                    }).ToList();

                if (listOfNewUserRole != null && listOfNewUserRole.Count > 0)
                {
                    foreach (APIUserRole userRole in listOfNewUserRole)
                    {
                        userRole.CreatedBy = this.UserLogin;
                        userRole.CreatedTime = DateTime.Now;
                        userRole.UpdatedBy = this.UserLogin;
                        userRole.UpdatedTime = DateTime.Now;
                    }
                    // add user role
                    dbContext.UserRoles.AddRange(listOfNewUserRole);
                }
                dbContext.SaveChanges();

                listOfExistingRole.AddRange(listOfNewUserRole);

                // add user permission
                AddPermissionBasedOnAdditionalRole(dbContext, listOfCurrentClientUser, listOfNewUserRole, user.Id);
                dbContext.SaveChanges();

                if (user.Id != AppConfigs.GetInt("DMSAdminUserId"))
                {
                    List<int> listOfSelectedRoleId = listOfUserRole.Select(r => r.RoleId).ToList();
                    List<APIUserRole> listOfRemovedUserRole = listOfExistingRole.Where(r => !listOfSelectedRoleId.Contains(r.RoleId)).ToList();

                    RemovePermissionBasedOnRemovedRole(dbContext, listOfRemovedUserRole, listOfSelectedRoleId, listOfCurrentClientUser);
                    // remove client user
                    dbContext.UserRoles.RemoveRange(listOfRemovedUserRole);
                    dbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

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


        #region Not Implemented
        public APIUserRole Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(APIUserRole entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(APIUserRole entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<APIUserRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<APIUserRole> Search(Framework.DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
