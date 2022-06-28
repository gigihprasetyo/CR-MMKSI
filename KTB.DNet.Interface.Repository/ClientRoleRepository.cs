#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository
{
    public class ClientRoleRepository : BaseRepository, IClientRoleRepository<APIClientRole, int>
    {
        private RolePermissionRepository _rolePermissionRepo;

        /// <summary>
        /// Get all client UserToClient
        /// </summary>
        /// <returns></returns>
        public List<APIClientRole> GetAll()
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.ClientRoles.ToList();
            }
        }

        /// <summary>
        /// Get UserToClient by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIClientRole Get(int id)
        {
            APIClientRole client = null;
            if (id != 0)
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    client = db.ClientRoles.Include(c => c.Client).Include(c => c.Role).FirstOrDefault(x => x.Id == id);
                }
            }

            return client;
        }

        /// <summary>
        /// Get UserToClient by userid and clientid
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<APIClientRole> GetByClientId(Guid clientId)
        {
            List<APIClientRole> client = null;
            if (clientId != null)
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    client = db.ClientRoles.Where(x => x.ClientId == clientId).Include(c => c.Client).Include(c => c.Role).ToList();
                }
            }

            return client;
        }

        /// <summary>
        /// Create API Client Role
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIClientRole entity)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Update API Client Role
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIClientRole entity)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    Update(db, entity);
                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Client Role has been successfully updated." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Update API Client Role
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Update(DNETInterfaceDBContext db, APIClientRole entity)
        {
            try
            {
                var clientRole = db.ClientRoles.FirstOrDefault(e => e.Id == entity.Id);

                List<APIRolePermission> existingRolePermissions = db.RolePermissions.Where(p => p.ClientRoleId == entity.Id).ToList();

                List<int> selectedPermissionIds = entity.RolePermissions.Select(cp => cp.PermissionId).ToList();
                List<APIRolePermission> listOfSelectedRolePermissions = db.RolePermissions.Where(w => w.ClientRoleId == entity.Id && selectedPermissionIds.Contains(w.PermissionId)).ToList();

                List<int> existingRolePermissionIds = existingRolePermissions.Select(cp => cp.Id).ToList();
                List<int> existingPermissionIds = existingRolePermissions.Select(cp => cp.PermissionId).ToList();
                if (existingRolePermissionIds.Count() > 0)
                {

                    var additems = selectedPermissionIds.Where(permissionId => !existingPermissionIds.Contains(permissionId)).ToList();
                    if (additems.Count > 0)
                    {
                        //bulk add role permission
                        db.RolePermissions.AddRange(additems.Select(permissionId =>
                        {
                            var permission = db.EndpointPermissions.FirstOrDefault(e => e.Id == permissionId);

                            APIRolePermission clientPermission = new APIRolePermission
                            {
                                ClientRole = clientRole,
                                ClientRoleId = clientRole.Id,
                                PermissionId = permissionId,
                                Permission = permission
                            };

                            SetCreatedLog(clientPermission);
                            return clientPermission;
                        }));

                        // update user permission
                        List<APIUserRole> userRoles = db.UserRoles.Where(ur => ur.RoleId == clientRole.RoleId).ToList();
                        List<APIUserPermission> listOfUserPermission = new List<APIUserPermission>();
                        userRoles.ForEach(userRole =>
                        {
                            APIClientUser clientUser = db.ClientUsers.FirstOrDefault(cu => cu.UserId == userRole.UserId && cu.ClientId == clientRole.ClientId);

                            if (clientUser != null)
                            {
                                db.Entry(clientUser).State = EntityState.Unchanged;
                                List<int> listOfExistingPermissionId = db.UserPermissions.Include(up => up.ClientUser)
                                                                                 .Where(userPermission => userPermission.ClientUser.Id == clientUser.Id)
                                                                                 .Select(up => up.PermissionId).ToList();

                                // get permission from role permission 
                                List<int> listOfNewPermission = entity.RolePermissions
                                                                            .Where(rp => !listOfExistingPermissionId.Contains(rp.PermissionId))
                                                                            .Select(rp => { return rp.PermissionId; }).ToList();


                                db.UserPermissions.AddRange(listOfNewPermission.Select(permissionId =>
                                {
                                    var permission = db.EndpointPermissions.FirstOrDefault(e => e.Id == permissionId);
                                    APIUserPermission userPermission = new APIUserPermission
                                    {
                                        Permission = permission,
                                        ClientUser = clientUser
                                    };

                                    SetCreatedLog(userPermission);
                                    return userPermission;
                                }));

                            }

                        });
                    }


                    List<int> listOfSelectedRolePermissionIds = listOfSelectedRolePermissions.Select(cp => cp.Id).ToList();
                    var deleteitems = existingRolePermissionIds.Where(crPerm => !listOfSelectedRolePermissionIds.Contains(crPerm)).ToList();
                    if (deleteitems.Count > 0)
                    {

                        if (clientRole.ClientId.ToString().ToLower().Trim() == AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
                        {
                            throw new Exception("Administrator role permission is restricted to be deleted.");
                        }

                        db.RolePermissions.RemoveRange(db.RolePermissions.Where(up => deleteitems.Contains(up.Id)));

                        List<int> deletedPermissions = db.RolePermissions.Where(w => deleteitems.Contains(w.Id)).Select(s => s.PermissionId).ToList();


                        // get users who has the role which has the permission to be removed
                        List<APIUserRole> userRoles = db.UserRoles.Where(ur => ur.RoleId == clientRole.RoleId).ToList();

                        List<APIUserPermission> listOfRemovedUserPermission = new List<APIUserPermission>();
                        userRoles.ForEach(userRole =>
                        {

                            APIClientUser clientUser = db.ClientUsers.FirstOrDefault(cu => cu.UserId == userRole.UserId && cu.ClientId == clientRole.ClientId);

                            if (clientUser != null)
                            {
                                //get other user's roles
                                List<int> listOfUserRoleId = db.UserRoles.Where(ur => ur.UserId == userRole.UserId).Select(ur => ur.RoleId).ToList();


                                List<int> unRemovedClientRoleId = db.ClientRoles.Where(
                                    cr => cr.ClientId == clientUser.ClientId &&
                                        listOfUserRoleId.Contains(cr.RoleId) &&
                                        cr.RoleId != clientRole.RoleId).Select(cr => cr.Id).ToList();

                                List<int> unRemovedPermissionId = db.RolePermissions.Where(rp => unRemovedClientRoleId.Contains(rp.ClientRoleId)).Select(rp => rp.PermissionId).ToList();

                                listOfRemovedUserPermission.AddRange(db.UserPermissions.Where(up => up.ClientUserId == clientUser.Id && deletedPermissions.Contains(up.PermissionId) && !unRemovedPermissionId.Contains(up.PermissionId) && !up.IsCustomPermission && !up.IsDismantledPermission).ToList());

                            }

                        });
                        db.UserPermissions.RemoveRange(listOfRemovedUserPermission);
                    }

                }
                else
                {
                    db.RolePermissions.AddRange(entity.RolePermissions.Select(rolePermission =>
                    {
                        var permission = db.EndpointPermissions.FirstOrDefault(e => e.Id == rolePermission.PermissionId);
                        APIRolePermission clientRolePermission = new APIRolePermission
                        {
                            ClientRole = clientRole,
                            ClientRoleId = clientRole.Id,
                            PermissionId = rolePermission.PermissionId,
                            Permission = permission
                        };

                        SetCreatedLog(clientRolePermission);
                        return clientRolePermission;
                    }));


                    // update user permission
                    List<APIUserRole> userRoles = db.UserRoles.Where(ur => ur.RoleId == clientRole.RoleId).ToList();
                    List<APIUserPermission> listOfUserPermission = new List<APIUserPermission>();
                    userRoles.ForEach(userRole =>
                    {
                        APIClientUser clientUser = db.ClientUsers.FirstOrDefault(cu => cu.UserId == userRole.UserId && cu.ClientId == clientRole.ClientId);

                        if (clientUser != null)
                        {
                            db.Entry(clientUser).State = EntityState.Unchanged;
                            List<int> listOfExistingPermissionId = db.UserPermissions.Include(up => up.ClientUser)
                                                                             .Where(userPermission => userPermission.ClientUser.Id == clientUser.Id)
                                                                             .Select(up => up.PermissionId).ToList();

                            // get permission from role permission 
                            List<int> listOfNewPermission = entity.RolePermissions
                                                                        .Where(rp => !listOfExistingPermissionId.Contains(rp.PermissionId))
                                                                        .Select(rp => { return rp.PermissionId; }).ToList();


                            db.UserPermissions.AddRange(listOfNewPermission.Select(permissionId =>
                            {
                                var permission = db.EndpointPermissions.FirstOrDefault(e => e.Id == permissionId);
                                APIUserPermission userPermission = new APIUserPermission
                                {
                                    Permission = permission,
                                    ClientUser = clientUser
                                };

                                SetCreatedLog(userPermission);
                                return userPermission;
                            }));

                        }
                    });
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Delete API Client Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Search API Client Role
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APIClientRole> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        #region Private methods
        private void SaveBulkRolePermission(DNETInterfaceDBContext db, List<APIRolePermission> listOfRolePermission)
        {
            //add remove role permission
            if (listOfRolePermission != null && listOfRolePermission.Count() > 0)
            {
                _rolePermissionRepo.SaveBulkRolePermission(db, listOfRolePermission);
            }
        }
        #endregion

        public List<APIClientRole> GetClientRoleByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
