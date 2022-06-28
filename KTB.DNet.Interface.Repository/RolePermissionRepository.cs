using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace KTB.DNet.Interface.Repository
{
    public class RolePermissionRepository : BaseRepository, IRolePermissionRepository<APIRolePermission, int>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RolePermissionRepository()
        {
        }

        /// <summary>
        /// Construct Role Permission
        /// </summary>
        /// <param name="rolePermission"></param>
        /// <param name="constructMany"></param>
        /// <returns></returns>
        public virtual APIRolePermission ConstructRolePermission(APIRolePermission rolePermission, bool constructMany)
        {
            if (rolePermission == null) return null;

            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                var constructedEntity = new APIRolePermission
                    {
                        Id = rolePermission.Id,
                        PermissionId = rolePermission.PermissionId,
                        ClientRoleId = rolePermission.ClientRoleId,
                        ClientRole = db.ClientRoles.FirstOrDefault(x => x.Id == rolePermission.ClientRoleId),
                        Permission = db.EndpointPermissions.FirstOrDefault(x => x.Id == rolePermission.PermissionId)
                    };

                return constructedEntity;
            }
        }

        /// <summary>
        /// Get API Role Permission By Role Name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<APIRolePermission> GetByRoleName(string roleName)
        {
            var rolePermissions = new List<APIRolePermission>();

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var rolePermissionItems = db.RolePermissions.Where(x => x.ClientRole.Role.Name == roleName);

                    foreach (var item in rolePermissionItems)
                    {
                        var entity = ConstructRolePermission(item, false);
                        rolePermissions.Add(entity);
                    }
                }

                return rolePermissions;
            }
            catch (Exception)
            {

                return new List<APIRolePermission>();
            }
        }

        /// <summary>
        /// Get Role Permission By Role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<APIRolePermission> GetByRoleId(int roleId)
        {
            try
            {
                var rolePermissions = new List<APIRolePermission>();

                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var rolePermissionItems = db.RolePermissions.Where(x => x.ClientRole.Role.Id == roleId);

                    foreach (var item in rolePermissionItems)
                    {
                        var entity = ConstructRolePermission(item, false);
                        rolePermissions.Add(entity);
                    }
                }

                return rolePermissions;
            }
            catch (Exception)
            {

                return new List<APIRolePermission>();
            }
        }

        /// <summary>
        /// Get Role Permission By Client Role Id
        /// </summary>
        /// <param name="clientRoleId"></param>
        /// <returns></returns>
        public List<APIRolePermission> GetByClientRoleId(int clientRoleId)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    List<APIRolePermission> rolePermissions = db.RolePermissions.Where(u => u.ClientRoleId == clientRoleId).Include(u => u.Permission).ToList();
                    return rolePermissions;
                }
            }
            catch
            {
                return new List<APIRolePermission>();
            }
        }

        /// <summary>
        /// Add Role Permission
        /// </summary>
        /// <param name="listOfRolePermission"></param>
        /// <returns></returns>
        public ResponseMessage AddRolePermission(List<APIRolePermission> listOfRolePermission)
        {
            if (listOfRolePermission != null && listOfRolePermission.Count() > 0)
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        APIClientRole clientRole = listOfRolePermission[0].ClientRole;

                        db.Entry(clientRole).State = EntityState.Unchanged;


                        if (clientRole != null)
                        {
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
                                    List<APIEndpointPermission> listOfNewPersmission = listOfRolePermission
                                                                                .Where(rp => !listOfExistingPermissionId.Contains(rp.PermissionId))
                                                                                .Select(rp => { db.Entry(rp.Permission).State = EntityState.Unchanged; return rp.Permission; }).ToList();

                                    listOfNewPersmission.ForEach(p =>
                                    {
                                        listOfUserPermission.Add(new APIUserPermission() { Permission = p, ClientUser = clientUser });
                                    });

                                }

                            });
                            foreach (APIUserPermission userPermission in listOfUserPermission)
                            {
                                SetCreatedLog(userPermission);
                            }
                            db.UserPermissions.AddRange(listOfUserPermission);
                        }

                        listOfRolePermission.ForEach(rolePermission => { db.Entry(rolePermission.Permission).State = EntityState.Unchanged; });
                        db.RolePermissions.AddRange(listOfRolePermission);

                        db.SaveChanges();
                        transaction.Commit();
                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Role Permission has been successfully updated." };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ExceptionDispatchInfo.Capture(ex).Throw();
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
                    }
                }
            }

            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "List of Role Permission is not valid." };
        }


        public ResponseMessage SaveBulkRolePermission(DNETInterfaceDBContext db, List<APIRolePermission> entities)
        {
            // TO DO: kalo dapper nya udah siap ajahh
            // Add / Delete role permssion, cek dulu dari existing, filter yang mo di add, filter yang mo didelete. OK. Bye
            if (entities.Count() > 0)
            {
                List<APIRolePermission> existing = db.RolePermissions.Where(w => w.ClientRoleId == entities.FirstOrDefault().ClientRoleId).ToList();
            }

            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Role Permission has been successfully updated." };

        }

        /// <summary>
        /// Remove Role Permission bulk
        /// </summary>
        /// <param name="listOfRemovedRolePermission"></param>
        /// <returns></returns>
        public ResponseMessage RemoveRolePermission(List<APIRolePermission> listOfRemovedRolePermission)
        {

            //    // remove all user permission based on role permission except the special permission (custom or dismantled)
            if (listOfRemovedRolePermission != null && listOfRemovedRolePermission.Count() > 0)
            {
                APIClientRole clientRole = listOfRemovedRolePermission[0].ClientRole;

                if (clientRole.ClientId.ToString().ToLower().Trim() == AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Administrator role permission is restricted to be deleted." };
                }

                // list of removed permission id
                List<int> listOfRemovedPermissionId = listOfRemovedRolePermission.Select(rp => rp.PermissionId).ToList();

                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    // get users who has the role which has the permission to be removed
                    List<APIUserRole> userRoles = db.UserRoles.Where(ur => ur.RoleId == clientRole.RoleId).ToList();

                    List<APIUserPermission> listOfRemovedUserPermission = new List<APIUserPermission>();
                    userRoles.ForEach(userRole =>
                    {

                        APIClientUser clientUser = db.ClientUsers.FirstOrDefault(cu => cu.UserId == userRole.UserId && cu.ClientId == clientRole.ClientId);

                        //get other user's roles
                        List<int> listOfUserRoleId = db.UserRoles.Where(ur => ur.UserId == userRole.UserId).Select(ur => ur.RoleId).ToList();


                        List<int> unRemovedClientRoleId = db.ClientRoles.Where(
                            cr => cr.ClientId == clientUser.ClientId &&
                                listOfUserRoleId.Contains(cr.RoleId) &&
                                cr.RoleId != clientRole.RoleId).Select(cr => cr.Id).ToList();

                        List<int> unRemovedPermissionId = db.RolePermissions.Where(rp => unRemovedClientRoleId.Contains(rp.ClientRoleId)).Select(rp => rp.PermissionId).ToList();

                        listOfRemovedUserPermission.AddRange(db.UserPermissions.Where(up => up.ClientUserId == clientUser.Id && listOfRemovedPermissionId.Contains(up.PermissionId) && !unRemovedPermissionId.Contains(up.PermissionId) && !up.IsCustomPermission && !up.IsDismantledPermission).ToList());

                    });

                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.UserPermissions.RemoveRange(listOfRemovedUserPermission);

                            db.RolePermissions.RemoveRange(listOfRemovedRolePermission);

                            db.SaveChanges();

                            transaction.Commit();
                            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Role Permission has been successfully removed." };
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ExceptionDispatchInfo.Capture(ex).Throw();
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
                        }
                    }
                }
            }

            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "List of Role Permission is not valid" };
        }

        /// <summary>
        /// Remove Role Permission Single
        /// </summary>
        /// <param name="removedRolePermission"></param>
        /// <returns></returns>
        public ResponseMessage RemoveRolePermission(APIRolePermission removedRolePermission)
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                removedRolePermission = db.RolePermissions.FirstOrDefault(rp => rp.Id == removedRolePermission.Id);
                // remove all user permission based on role permission except the special permission (custom or dismantled)
                if (removedRolePermission != null)
                {
                    APIClientRole clientRole = db.ClientRoles.FirstOrDefault(cr => cr.Id == removedRolePermission.ClientRoleId);

                    if (clientRole != null)
                    {
                        if (clientRole.ClientId.ToString().ToLower().Trim() == AppConfigs.GetString("DMSAdminClientId").ToLower().Trim())
                        {
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Administrator role permission is restricted to be deleted." };
                        }

                        List<APIUserPermission> listOfRemovedUserPermission = new List<APIUserPermission>();
                        List<APIClientUser> listOfClientUser = db.ClientUsers.Where(cu => cu.ClientId == clientRole.ClientId).ToList();
                        listOfClientUser.ForEach(clientUser =>
                        {

                            APIUserRole userRole = db.UserRoles.FirstOrDefault(ur => ur.UserId == clientUser.UserId && ur.RoleId == clientRole.RoleId);

                            if (userRole != null)
                            {
                                //get other user's roles
                                List<int> listOfUserRoleId = db.UserRoles.Where(ur => ur.UserId == clientUser.UserId).Select(ur => ur.RoleId).ToList();


                                List<int> unRemovedClientRoleId = db.ClientRoles.Where(
                                    cr => cr.ClientId == clientUser.ClientId &&
                                        listOfUserRoleId.Contains(cr.RoleId) &&
                                        cr.RoleId != clientRole.RoleId).Select(cr => cr.Id).ToList();

                                List<int> unRemovedPermissionId = db.RolePermissions.Where(rp => unRemovedClientRoleId.Contains(rp.ClientRoleId)).Select(rp => rp.PermissionId).ToList();

                                listOfRemovedUserPermission.AddRange(db.UserPermissions.Where(up => up.ClientUserId == clientUser.Id && removedRolePermission.PermissionId == up.PermissionId && !unRemovedPermissionId.Contains(up.PermissionId) && !up.IsCustomPermission && !up.IsDismantledPermission).ToList());
                            }
                        });

                        using (var transaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                db.UserPermissions.RemoveRange(listOfRemovedUserPermission);

                                db.RolePermissions.Remove(removedRolePermission);

                                db.SaveChanges();

                                transaction.Commit();
                                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Role Permission has been successfully removed." };
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                ExceptionDispatchInfo.Capture(ex).Throw();
                                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
                            }

                        }
                    }
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Client role is not valid" };
                }
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Role Permission is not valid" };
            }
        }

        /// <summary>
        /// Get Role Permission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIRolePermission Get(int id)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    APIRolePermission rolePermission = db.RolePermissions.FirstOrDefault(x => x.Id == id);
                    if (rolePermission == null) { return rolePermission; }

                    rolePermission.ClientRole = db.ClientRoles.FirstOrDefault(x => x.Id == rolePermission.ClientRoleId);
                    rolePermission.Permission = db.EndpointPermissions.FirstOrDefault(x => x.Id == rolePermission.PermissionId);

                    return rolePermission;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Create Role Permission
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIRolePermission entity)
        {
            try
            {
                var rolePermission = entity;

                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    db.RolePermissions.Add(rolePermission); ;
                    db.SaveChanges();
                }

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Role Permission has been created successfully" };

            }
            catch (Exception ex)
            {

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Update Role Permission
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIRolePermission entity)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    APIRolePermission rolePermission = db.RolePermissions.Find(entity.Id);
                    rolePermission.ClientRole = entity.ClientRole;
                    rolePermission.ClientRoleId = entity.ClientRoleId;
                    rolePermission.Permission = entity.Permission;
                    rolePermission.PermissionId = entity.PermissionId;

                    SetLastModifiedLog(rolePermission);
                    db.SaveChanges();
                }

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Role Permission has been updated successfully" };
            }
            catch (Exception ex)
            {

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Delete Role Permission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var rolePermission = db.RolePermissions.FirstOrDefault(x => x.Id == id);
                    if (rolePermission != null)
                    {
                        db.RolePermissions.Remove(rolePermission);
                        db.SaveChanges();
                    }

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Role Permission has been deleted successfully" };
                }
            }
            catch (Exception ex)
            {

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Get All Role Permission
        /// </summary>
        /// <returns></returns>
        public List<APIRolePermission> GetAll()
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.RolePermissions.ToList();
                }
            }
            catch (Exception)
            {

                return new List<APIRolePermission>();
            }
        }

        /// <summary>
        /// Search - Not implemented
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APIRolePermission> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }


        public void DeleteByPermissionId(int permissionId)
        {
            throw new NotImplementedException();
        }


        public void BulkInsert(List<APIRolePermission> rolePermissions)
        {
            throw new NotImplementedException();
        }

        public void BulkDelete(List<int> rolePermissionIds)
        {
            throw new NotImplementedException();
        }
    }
}
