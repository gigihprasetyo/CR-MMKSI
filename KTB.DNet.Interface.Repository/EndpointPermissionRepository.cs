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
    public class EndpointPermissionRepository : BaseRepository, IEndpointPermissionRepository<APIEndpointPermission, int>
    {
        public EndpointPermissionRepository()
        {
        }

        /// <summary>
        /// Get Endpoint Permission By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public APIEndpointPermission GetByName(string name)
        {
            try
            {
                APIEndpointPermission permission;

                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    permission = db.EndpointPermissions.FirstOrDefault(x => x.Name == name);
                }

                return GetPermissionWithRolePermission(permission);
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Get Endpoint Permission By URI
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public APIEndpointPermission GetByUri(string uri)
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.EndpointPermissions.FirstOrDefault(x => x.URI == uri);
            }
        }

        /// <summary>
        /// Get Client's Permissions
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> GetClientPermission(Guid clientId)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.ClientPermissions.Where(cp => cp.ClientId == clientId).Include(cp => cp.Permission).Select(cp => cp.Permission).ToList();
                }
            }
            catch (Exception)
            {

                return new List<APIEndpointPermission>();
            }
        }

        /// <summary>
        /// Search Endpoint Permissions By Client Role Id
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <param name="clientRoleId"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> SearchByClientRoleId(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, int? clientRoleId)
        {
            List<APIEndpointPermission> result = null;

            try
            {
                string keyword;
                PropertyInfo orderedProperty = null;
                int take, skip;
                bool orderDir;

                GetPostModelData<APIEndpointPermission>(model, out keyword, "Name", out orderedProperty, out orderDir, out take, out skip);

                // search the dbase taking into consideration table sorting and paging
                result = Filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount, clientRoleId);
                if (result == null)
                {
                    // empty collection...
                    return new List<APIEndpointPermission>();
                }

            }
            catch
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIEndpointPermission>();
            }

            return result;
        }

        /// <summary>
        /// Get List of Selected Endpoint Permissions by Id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> GetSelectedPermission(List<int> ids)
        {
            List<APIEndpointPermission> result = new List<APIEndpointPermission>();
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var list = db.EndpointPermissions.Where(w => ids.Contains(w.Id));
                    result = list.ToList();
                }

            }
            catch
            {
                return new List<APIEndpointPermission>();
            }


            return result;
        }

        /// <summary>
        /// Get List of Unselected Permission By Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> GetUnselectedPermission(List<int> ids)
        {
            List<APIEndpointPermission> result = new List<APIEndpointPermission>();
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var list = db.EndpointPermissions.Where(w => !ids.Contains(w.Id));
                    result = list.ToList();
                }

            }
            catch
            {
                return new List<APIEndpointPermission>();
            }


            return result;
        }

        /// <summary>
        /// Get total number of Permission
        /// </summary>
        /// <returns></returns>
        public int GetPermissionCount(int userId)
        {
            try
            {
                if (userId == AppConfigs.GetInt("DMSAdminUserId"))
                {
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        return db.EndpointPermissions.Count();
                    } 
                }
                else
                {
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        APIClientUser clientUser = db.ClientUsers.FirstOrDefault(cu => cu.UserId == userId);
                        return db.ClientPermissions.Where(cu => cu.ClientId == clientUser.ClientId).Count();
                    } 
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// Get Endpoint Permission by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIEndpointPermission Get(int id)
        {
            try
            {
                APIEndpointPermission permission;

                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    permission = db.EndpointPermissions.FirstOrDefault(x => x.Id == id);
                }

                return GetPermissionWithRolePermission(permission);
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Create Endpoint Permission
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIEndpointPermission entity)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    bool isExist = db.EndpointPermissions.Any(x => x.Name.Equals(entity.Name));
                    if (isExist)
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Permission already exist." };
                    }

                    SetCreatedLog(entity);
                    db.EndpointPermissions.Add(entity);
                    db.SaveChanges();
                }

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New Permission has been successully created.", Data = entity };
            }
            catch (Exception ex)
            {

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Failed to create Permission : " + GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Update Endpoint Permissions
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIEndpointPermission entity)
        {

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    APIEndpointPermission permission = db.EndpointPermissions.Find(entity.Id);

                    if (Constants.RestrictedPermissions.Contains(permission.Name))
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Permission is restricted to be updated." };
                    }

                    permission.Name = entity.Name;
                    permission.PermissionCode = entity.PermissionCode;
                    permission.URI = entity.URI;
                    permission.EndpointType = entity.EndpointType;
                    permission.OperationType = entity.OperationType;
                    permission.Description = entity.Description;
                    permission.IsScheduled = entity.IsScheduled;

                    SetLastModifiedLog(permission);

                    db.Entry(permission).CurrentValues.SetValues(entity);
                    db.SaveChanges();
                }
                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("Permission has been successfully updated: ", entity.Name), Data = entity };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Failed to update Permission : " + GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Delete Endpoint Permission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var permission = db.EndpointPermissions.SingleOrDefault(f => f.Id == id);
                    if (permission != null)
                    {
                        if (Constants.RestrictedPermissions.Contains(permission.Name))
                        {
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Permission cannot be deleted." };
                        }

                        db.UserPermissions.RemoveRange(db.UserPermissions.Where(up => up.PermissionId == id));
                        db.RolePermissions.RemoveRange(db.RolePermissions.Where(rp => rp.PermissionId == id));
                        db.ClientPermissions.RemoveRange(db.ClientPermissions.Where(cp => cp.PermissionId == id));

                        db.EndpointPermissions.Remove(permission);
                        db.SaveChanges();
                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("Permission has been successfully deleted. ") };
                    }
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = string.Format("Permission is not valid. ") };
                }
            }
            catch (Exception ex)
            {

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Failed to delete Permission : " + GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Get All Endpoint Permissions
        /// </summary>
        /// <returns></returns>
        public List<APIEndpointPermission> GetAll()
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.EndpointPermissions.OrderBy(e => e.PermissionCode).ToList();
                }
            }
            catch (Exception)
            {

                return new List<APIEndpointPermission>();
            }
        }

        /// <summary>
        /// Search Endpoint Permissions
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APIEndpointPermission> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, bool isScheduled = false)
        {
            try
            {
                string keyword;
                PropertyInfo orderedProperty = null;
                int take, skip;
                bool orderDir;

                GetPostModelData<APIEndpointPermission>(model, out keyword, "Name", out orderedProperty, out orderDir, out take, out skip);

                // search the dbase taking into consideration table sorting and paging
                var result = Filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount, null, isScheduled);
                if (result == null)
                {
                    // empty collection...
                    return new List<APIEndpointPermission>();
                }

                return result;
            }
            catch (Exception)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIEndpointPermission>();
            }
        }

        public List<APIEndpointPermission> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            return Search(model, out filteredResultsCount, out totalResultsCount, false);
        }

        /// <summary>
        /// Filter Endpoint Permissions
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <param name="clientRoleId"></param>
        /// <returns></returns>
        private List<APIEndpointPermission> Filter(string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount, int? clientRoleId = null, bool isScheduled = false)
        {
            try
            {

                List<APIEndpointPermission> result = new List<APIEndpointPermission>();

                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    IQueryable<APIEndpointPermission> queryablePermission;
                    if (isScheduled)
                        queryablePermission = db.EndpointPermissions.Where(w => w.IsScheduled == true);
                    else
                        queryablePermission = db.EndpointPermissions;

                    if (clientRoleId.HasValue)
                    {
                        var list = (from c in db.EndpointPermissions//.Include("APIRolePermission")
                                    join meta in db.RolePermissions on c.Id equals meta.PermissionId
                                    orderby c.Name descending
                                    where //(c.RolePermissions.Equals(clientRoleId) && 
                                    (c.Name.Contains(keyword)) && meta.ClientRoleId == clientRoleId
                                    select c).Skip(skip).Take(take);

                        result = list.ToList();// result.Where(w => w.RolePermissions.All(a => a.ClientRoleId == clientRoleId)).ToList();
                    }
                    else
                    {
                        result = queryablePermission.AsEnumerable()
                             .Where(
                                      u =>
                                      {
                                          bool keywordExists = !string.IsNullOrEmpty(keyword);

                                          return !keywordExists ||
                                              (keywordExists && (u.Name != null ? u.Name.ToUpper().Contains(keyword) : false));
                                      }
                                   )
                             .OrderByWithDirection(u => orderedProperty.GetValue(u, null), !orderDir)
                             .Skip(skip)
                             .Take(take)
                             .ToList();

                    }

                    // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
                    filteredResultsCount = queryablePermission
                                   .Where(
                                            u =>
                                            string.IsNullOrEmpty(keyword) ||
                                            u.Name.ToUpper().Contains(keyword)).Count();

                    totalResultsCount = queryablePermission.Count();
                }

                return result;
            }
            catch (Exception)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIEndpointPermission>();
            }
        }

        /// <summary>
        /// Get Endpoint Permission with Role Permission
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        private APIEndpointPermission GetPermissionWithRolePermission(APIEndpointPermission permission)
        {
            try
            {
                if (permission == null)
                {
                    return null;
                }

                permission.RolePermissionIds = permission.RolePermissions.Select(r => r.Id).ToList();

                // include many to many items
                // RolePermissions
                permission.RolePermissions = permission.RolePermissions.Select(r => new APIRolePermission
                {
                    Id = r.Id
                }).ToList();

                return permission;
            }
            catch (Exception)
            {

                return null;
            }
        }



        public List<APIEndpointPermission> GetEndpointWithNoThrottler()
        {
            List<APIEndpointPermission> result = new List<APIEndpointPermission>();

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    List<int> listOfThrottleEndpointId = db.Throttles.Select(t => t.EndpointId).ToList();
                    IQueryable<APIEndpointPermission> queryablePermission = db.EndpointPermissions;


                    result = queryablePermission.AsEnumerable()
                        .Where(
                                u =>
                                {
                                    // get permission which is not on throttler
                                    return !listOfThrottleEndpointId.Contains(u.Id);
                                }
                            )
                        .OrderBy(u => u.Name)
                        .ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }


        public List<APIEndpointPermission> SearchByClientRoleId(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, int clientRoleId)
        {
            throw new NotImplementedException();
        }
    }
}
