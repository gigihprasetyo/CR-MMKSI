using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace KTB.DNet.Interface.Repository
{
    public class ClientRepository : BaseRepository, IClientRepository<APIClient, Guid>
    {
        #region Public Method

        #region Base Interface
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIClient Get(Guid id)
        {

            try
            {
                if (id != null)
                {
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        APIClient client = db.Clients.FirstOrDefault(x => x.ClientId == id);
                        return client;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Get all client
        /// </summary>
        /// <returns></returns>
        public List<APIClient> GetAll()
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.Clients.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIClient>();
            }

        }

        /// <summary>
        /// Un-implemented Create (base interface)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIClient entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Un-implemented Update (base interface)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIClient entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete client
        /// </summary>
        /// <param name="id"></param>
        public ResponseMessage Delete(Guid id)
        {
            if (id != null)
            {
                if (id.ToString().ToLower() == AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Administrator client is restricted to be deleted." };
                }

                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {

                    var existingEntity = db.Clients.FirstOrDefault(x => x.ClientId == id);

                    if (existingEntity != null)
                    {
                        string clientName = existingEntity.Name;

                        var existInUser = db.ClientUsers.Any(x => x.ClientId == id);
                        if (existInUser)
                        {
                            db.ClientUsers.RemoveRange(db.ClientUsers.Where(cu => cu.ClientId == existingEntity.ClientId));
                        }

                        if (db.ClientRoles.Any(cr => cr.ClientId == existingEntity.ClientId))
                        {
                            DeleteClientRole(db, db.ClientRoles.Where(cr => cr.ClientId == existingEntity.ClientId).ToList(), existingEntity.ClientId);
                        }

                        DeleteClientPermission(db, db.ClientPermissions.Where(cp => cp.ClientId == existingEntity.ClientId).ToList(), existingEntity.ClientId);
                        db.Clients.Remove(existingEntity);

                        db.SaveChanges();

                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("The client with client {0} has successfully deleted", clientName) };
                    }
                    else
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = string.Format("Failed to delete client with clientid {0}. the client is not exist", id) };
                    }
                }

            }
            else
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "ClientId could not be null" };
            }

        }

        /// <summary>
        /// Filter AppClient
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<APIClient> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            string keyword;
            PropertyInfo orderedProperty = null;
            int take, skip;
            bool orderDir;

            GetPostModelData<APIClient>(model, out keyword, "Name", out orderedProperty, out orderDir, out take, out skip);

            // search the dbase taking into consideration table sorting and paging
            var result = Filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<APIClient>();
            }

            return result;
        }

        #endregion

        /// <summary>
        /// Create new client
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIClient entity, List<int> listOfSelectedRoleId, List<int> listOfSelectedPermissionId)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var isExist = db.Clients.Any(x => x.Name == entity.Name);

                    if (!isExist)
                    {
                        if (!(listOfSelectedRoleId != null && listOfSelectedRoleId.Count() > 0))
                        {
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Please select at least one role." };
                        }

                        if (!(listOfSelectedPermissionId != null && listOfSelectedPermissionId.Count() > 0))
                        {
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Please select at least one permission." };
                        }

                        APIClient dbClient = new APIClient();
                        dbClient.Name = entity.Name;
                        dbClient.ClientId = Guid.NewGuid();
                        dbClient.SecretKey = Guid.NewGuid();

                        #region add MsApp
                        MsApplication msApp = db.MsApplications.FirstOrDefault(p => entity.AppId == p.AppId);
                        dbClient.MsApplication = msApp;

                        #endregion

                        SetCreatedLog(dbClient);

                        db.Clients.Add(dbClient);

                        #region add permission
                        List<APIEndpointPermission> selectedPermissions = db.EndpointPermissions.Where(p => listOfSelectedPermissionId.Contains(p.Id)).ToList();

                        List<APIClientPermission> clientPermissions = selectedPermissions.Select(permission =>
                        {
                            APIClientPermission clientPermission = new APIClientPermission
                            {
                                PermissionId = permission.Id,
                                Permission = permission,
                                Client = dbClient,
                                ClientId = dbClient.ClientId
                            };

                            SetCreatedLog(clientPermission);
                            return clientPermission;
                        }).ToList();

                        db.ClientPermissions.AddRange(clientPermissions);
                        #endregion

                        #region add roles
                        List<APIRole> selectedRoles = db.Roles.Where(p => listOfSelectedRoleId.Contains(p.Id)).ToList();

                        List<APIClientRole> clientRoles = selectedRoles.Select(role =>
                        {
                            APIClientRole clientRole = new APIClientRole
                            {
                                RoleId = role.Id,
                                Role = role,
                                Client = dbClient,
                                ClientId = dbClient.ClientId
                            };

                            SetCreatedLog(clientRole);
                            return clientRole;

                        }).ToList();

                        db.ClientRoles.AddRange(clientRoles);
                        #endregion



                        db.SaveChanges();

                        entity.ClientId = dbClient.ClientId;
                        entity.SecretKey = dbClient.SecretKey;

                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New Client has been successfully created.", Data = entity };
                    }

                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("Client with name {0} has already exist.", entity.Name), Data = entity };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Failed to create new client : " + GetInnerException(ex).Message };
            }

        }

        /// <summary>
        /// Update client
        /// </summary>
        /// <param name="entity"></param>
        public ResponseMessage Update(APIClient entity, List<int> listOfSelectedRoleId, List<int> listOfSelectedPermissionId)
        {

            try
            {
                if (entity != null)
                {
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        var isExist = db.Clients.Any(x => x.ClientId == entity.ClientId);

                        if (isExist)
                        {
                            var existingEntity = db.Clients.FirstOrDefault(x => x.ClientId == entity.ClientId);

                            existingEntity.Name = entity.Name;

                            #region Update MsApp
                            MsApplication msApp = db.MsApplications.FirstOrDefault(p => entity.AppId == p.AppId);
                            existingEntity.MsApplication = msApp;
                            #endregion

                            SetLastModifiedLog(existingEntity);

                            if (!(listOfSelectedRoleId != null && listOfSelectedRoleId.Count() > 0))
                            {
                                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Please select at least one role." };
                            }

                            if (!(listOfSelectedPermissionId != null && listOfSelectedPermissionId.Count() > 0))
                            {
                                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Please select at least one permission." };
                            }


                            #region Update Client Role
                            List<APIClientRole> existingRoles = db.ClientRoles.Where(p => p.ClientId == existingEntity.ClientId).ToList();
                            List<APIRole> selectedRoles = db.Roles.Where(r => listOfSelectedRoleId.Contains(r.Id)).ToList();

                            if (listOfSelectedRoleId != null && listOfSelectedRoleId.Count() > 0)
                            {
                                List<int> existingRoleIds = existingRoles.Select(cp => cp.RoleId).ToList();

                                if (existingRoleIds.Count() > 0)
                                {
                                    var additems = selectedRoles.Where(role => !existingRoleIds.Contains(role.Id)).ToList();
                                    if (additems.Count > 0)
                                    {
                                        //bulk add
                                        db.ClientRoles.AddRange(additems.Select(role =>
                                        {
                                            APIClientRole clientRole = new APIClientRole
                                            {
                                                Client = existingEntity,
                                                ClientId = existingEntity.ClientId,
                                                Role = role,
                                                RoleId = role.Id
                                            };

                                            SetCreatedLog(clientRole);
                                            return clientRole;
                                        }));
                                    }


                                    var deleteitems = existingRoles.Where(role => !listOfSelectedRoleId.Contains(role.RoleId)).ToList();
                                    if (deleteitems.Count > 0 &&
                                           entity.ClientId.ToString().ToLower() != AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
                                    {
                                        // delete client's roles
                                        DeleteClientRole(db, deleteitems, existingEntity.ClientId);
                                    }

                                }
                                else
                                {
                                    db.ClientRoles.AddRange(selectedRoles.Select(role =>
                                    {
                                        APIClientRole clientRole = new APIClientRole
                                        {
                                            Client = existingEntity,
                                            ClientId = existingEntity.ClientId,
                                            Role = role,
                                            RoleId = role.Id
                                        };

                                        SetCreatedLog(clientRole);
                                        return clientRole;
                                    }));
                                }
                            }
                            else
                            {

                                if (existingRoles != null &&
                                        existingRoles.Count() > 0 &&
                                        entity.ClientId.ToString().ToLower() != AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
                                {
                                    // delete client's roles
                                    DeleteClientRole(db, existingRoles, existingEntity.ClientId);
                                }
                            }
                            #endregion

                            #region Update Client Permission
                            List<APIClientPermission> existingPermissions = db.ClientPermissions.Where(p => p.ClientId == existingEntity.ClientId).ToList();
                                List<APIEndpointPermission> selectedPermissions = db.EndpointPermissions.Where(p => listOfSelectedPermissionId.Contains(p.Id)).ToList();

                            if (listOfSelectedPermissionId != null && listOfSelectedPermissionId.Count() > 0)
                            {
                                List<int> existingPermissionIds = existingPermissions.Select(cp => cp.PermissionId).ToList();

                                if (existingPermissionIds.Count() > 0)
                                {
                                    var additems = selectedPermissions.Where(permission => !existingPermissionIds.Contains(permission.Id)).ToList();
                                    if (additems.Count > 0)
                                    {
                                        //bulk add
                                        db.ClientPermissions.AddRange(additems.Select(permission =>
                                        {
                                            APIClientPermission clientPermission = new APIClientPermission
                                            {
                                                Client = existingEntity,
                                                ClientId = existingEntity.ClientId,
                                                Permission = permission,
                                                PermissionId = permission.Id
                                            };

                                            SetCreatedLog(clientPermission);
                                            return clientPermission;
                                        }));
                                    }

                                    var deleteitems = existingPermissions.Where(permission => !listOfSelectedPermissionId.Contains(permission.PermissionId)).ToList();
                                    if (deleteitems.Count > 0 &&
                                            entity.ClientId.ToString().ToLower() != AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
                                    {
                                        DeleteClientPermission(db, deleteitems, existingEntity.ClientId);
                                    }

                                }
                                else
                                {
                                    db.ClientPermissions.AddRange(selectedPermissions.Select(permission =>
                                    {
                                        APIClientPermission clientPermission = new APIClientPermission
                                        {
                                            Client = existingEntity,
                                            ClientId = existingEntity.ClientId,
                                            Permission = permission,
                                            PermissionId = permission.Id
                                        };

                                        SetCreatedLog(clientPermission);
                                        return clientPermission;
                                    }));
                                }
                            }
                            else
                            {

                                if (existingPermissions != null &&
                                        existingPermissions.Count() > 0 &&
                                        entity.ClientId.ToString().ToLower() != AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
                                {
                                    DeleteClientPermission(db, existingPermissions, existingEntity.ClientId);
                                }

                            }
                            #endregion

                            db.SaveChanges();

                            entity.ClientId = existingEntity.ClientId;
                            entity.SecretKey = existingEntity.SecretKey;

                            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("The client with name {0} has been successfully updated", entity.Name), Data = entity };

                        }

                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("The client with clientid {0} does not exist", entity.ClientId) };
                    }


                }

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "The client cannot be null" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<APIClient> GetByAppId(Guid appId)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.Clients.Include(c => c.MsApplication).Where(c => c.AppId == appId).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIClient>();
            }
        }

        /// <summary>
        /// Get client permission
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> GetClientPermission(Guid clientId)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.ClientPermissions.Include(cp => cp.Permission).Where(cp => cp.ClientId == clientId).Select(cp => cp.Permission).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<APIEndpointPermission>();
            }
        }

        /// <summary>
        /// Get client role
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<APIRole> GetClientRole(Guid clientId)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.ClientRoles.Include(cp => cp.Role).Where(cp => cp.ClientId == clientId).Select(cp => cp.Role).ToList();
                }
            }
            catch (Exception ex)
            {

                return new List<APIRole>();
            }
        }

        /// <summary>
        /// Get client user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<APIClient> GetUserClient(APIUser user)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    List<APIClient> listOfClient = db.ClientUsers.Include(cu => cu.Client).Where(cu => cu.UserId == user.Id).Select(cu => cu.Client).ToList();

                    return listOfClient;
                }
            }
            catch (Exception ex)
            {

                return new List<APIClient>();
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Delete ClientRole
        /// </summary>
        /// <param name="listOfRemovedClientRole"></param>
        private void DeleteClientRole(DNETInterfaceDBContext db, List<APIClientRole> listOfRemovedClientRole, Guid clientId)
        {
            if (clientId.ToString().ToLower() == AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
            {
                throw new Exception("Administrator client role is restricted to be updated.");
            }

            // delete all role permission and user permission who has permission from the role which will be deleted
            List<int> listOfRemovedClientRoleId = listOfRemovedClientRole.Select(cr => cr.Id).ToList();
            List<APIClientUser> listOfClientUser = db.ClientUsers.Where(cu => cu.ClientId == clientId).ToList();

            List<APIUserPermission> listOfRemovedUserPermission = new List<APIUserPermission>();

            List<APIClientRole> allClientRole = db.ClientRoles.Where(cr => cr.ClientId == clientId).ToList();
            List<APIClientRole> unRemovedClientRole = allClientRole.Where(cr => !listOfRemovedClientRoleId.Contains(cr.Id)).ToList();

            //List<int> allRollId = allClientRole.Select(cr=> cr.RoleId).ToList();
            List<int> unRemovedRoleId = unRemovedClientRole.Select(cr => cr.RoleId).ToList();

            listOfClientUser.ForEach(clientUser =>
            {
                List<int> unRemovedUserRoleId = db.UserRoles.Where(ur => ur.UserId == clientUser.UserId && unRemovedRoleId.Contains(ur.RoleId)).Select(ur => ur.RoleId).ToList();
                List<int> unRemovedClientRoleId = unRemovedClientRole.Where(cr => unRemovedUserRoleId.Contains(cr.RoleId)).Select(cr => cr.Id).ToList();

                List<int> unRemovedPermissionId = db.RolePermissions.Where(rp => unRemovedClientRoleId.Contains(rp.ClientRoleId)).Select(rp => rp.PermissionId).ToList();

                listOfRemovedUserPermission.AddRange(db.UserPermissions.Where(up => up.ClientUserId == clientUser.Id && !unRemovedPermissionId.Contains(up.PermissionId) && !up.IsCustomPermission && !up.IsDismantledPermission).ToList());

            });


            db.UserPermissions.RemoveRange(listOfRemovedUserPermission);
            db.RolePermissions.RemoveRange(db.RolePermissions.Where(up => listOfRemovedClientRoleId.Contains(up.ClientRoleId)));
            db.ClientRoles.RemoveRange(listOfRemovedClientRole);

        }

        /// <summary>
        /// Delete ClientPermission
        /// </summary>
        /// <param name="listOfClientPermission"></param>
        private void DeleteClientPermission(DNETInterfaceDBContext db, List<APIClientPermission> listOfClientPermission, Guid clientId)
        {
            if (clientId.ToString().ToLower() == AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
            {
                throw new Exception("Administrator client permission is restricted to be updated.");
            }

            // delete all role permission and user permission who has permission from the permission which will be deleted
            List<int> listOfClientRoleId = db.ClientRoles.Where(cr => cr.ClientId == clientId).Select(cr => cr.Id).ToList();
            List<int> listOfClientUserId = db.ClientUsers.Where(cu => cu.ClientId == clientId).Select(cu => cu.Id).ToList();
            List<int> listOfPermissionId = listOfClientPermission.Select(cp => cp.PermissionId).ToList();

            db.UserPermissions.RemoveRange(db.UserPermissions.Where(up => listOfClientUserId.Contains(up.ClientUserId) && listOfPermissionId.Contains(up.PermissionId) && !up.IsCustomPermission && !up.IsDismantledPermission));
            db.RolePermissions.RemoveRange(db.RolePermissions.Where(up => listOfClientRoleId.Contains(up.ClientRoleId) && listOfPermissionId.Contains(up.PermissionId)));
            db.ClientPermissions.RemoveRange(listOfClientPermission);
        }

        /// <summary>
        /// filter client
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        private List<APIClient> Filter(string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount)
        {

            List<APIClient> result = new List<APIClient>();

            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                IQueryable<APIClient> queryablePermission = db.Clients;

                result = queryablePermission.Include(c => c.MsApplication).AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return !keywordExists ||
                                                (keywordExists && (u.Name != null ? u.Name.ToUpper().Contains(keyword) : false));
                                        }
                                     )
                               .OrderByWithDirection(u => orderedProperty.GetValue(u, null), !orderDir)
                               .ToList();

                filteredResultsCount = result.Count();

                totalResultsCount = queryablePermission.Count();

                result = result
                           .Skip(skip)
                           .Take(take)
                           .ToList();
            }

            return result;
        }

        #endregion


        public int GetClientCount(int userId)
        {
            try
            {
                if (userId == AppConfigs.GetInt("DMSAdminUserId"))
                {
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        return db.Clients.Count();
                    } 
                }
                else 
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public void DeleteClientPermissionByPermissionId(int permissionId)
        {
            throw new NotImplementedException();
        }


        public APIClient GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}

