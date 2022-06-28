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
    public class UserPermissionRepository : BaseRepository, IUserPermissionRepository<APIUserPermission, int>
    {
        /// <summary>
        /// UserPermissionRepository Constructor
        /// </summary>
        public UserPermissionRepository() { }

        /// <summary>
        /// Filter AppClient
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<APIUserPermission> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            string keyword;
            PropertyInfo orderedProperty = null;
            int take, skip;
            bool orderDir;

            GetPostModelData<APIUserPermission>(model, out keyword, "Id", out orderedProperty, out orderDir, out take, out skip);
            // search the dbase taking into consideration table sorting and paging
            var result = Filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<APIUserPermission>();
            }

            return result;
        }

        /// <summary>
        /// Get filtered user permission
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        private List<APIUserPermission> Filter(string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount)
        {

            List<APIUserPermission> result = new List<APIUserPermission>();

            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                IQueryable<APIUserPermission> queryablePermission = db.UserPermissions.Include(up => up.Permission);

                result = queryablePermission.AsEnumerable()
                           .Where(
                                    u =>
                                    {
                                        bool keywordExists = !string.IsNullOrEmpty(keyword);

                                        return !keywordExists ||
                                            (keywordExists && (u.Permission.Name != null ? u.Permission.Name.ToUpper().Contains(keyword) : false));
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

        /// <summary>
        /// Create User Permission
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIUserPermission entity)
        {
            var userPermission = entity;

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    db.UserPermissions.Add(userPermission);
                    db.SaveChanges();
                }

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New User Permission has been successully created.", Data = userPermission };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }

        }

        /// <summary>
        /// Update User Permission
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIUserPermission entity)
        {
            APIUserPermission userPermission = null;

            try
            {
                if (entity != null)
                {
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        var existingEntity = db.UserPermissions.FirstOrDefault(x => x.Id == entity.Id);
                        var isExist = db.UserPermissions.Any(x => x.Id == entity.Id);

                        if (isExist)
                        {
                            db.Entry(existingEntity).CurrentValues.SetValues(entity);

                            db.SaveChanges();
                            userPermission = entity;

                            return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "User Permission has been successully created.", Data = userPermission };
                        }

                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "User Permission does not exist" };
                    }
                }

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = "Entity cannot be null" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }

        }

        /// <summary>
        /// Get User Permission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIUserPermission Get(int id)
        {
            var permission = new APIUserPermission();
            if (id > 0)
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    permission = db.UserPermissions.FirstOrDefault(x => x.Id == id);
                }
            }

            return permission;
        }

        /// <summary>
        /// Get All User Permission
        /// </summary>
        /// <returns></returns>
        public List<APIUserPermission> GetAll()
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.UserPermissions.ToList();
            }
        }

        /// <summary>
        /// Get User Permission by Client User
        /// </summary>
        /// <param name="clientUser"></param>
        /// <returns></returns>
        public List<APIUserPermission> GetUserPermissionByClientUserId(int clientUserId)
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.UserPermissions.Where(up => up.ClientUserId == clientUserId).Include(up => up.Permission).OrderBy(up => up.Permission.PermissionCode).ToList();
            }
        }

        public ResponseMessage SaveUserPermission(int userId, List<APIUserPermission> listOfSelectedUserPermission)
        {

            using (DNETInterfaceDBContext dbContext = new DNETInterfaceDBContext())
            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    APIUser existingUser = dbContext.Users.FirstOrDefault(u => u.Id == userId);
                    if (existingUser == null)
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("User with id {0} does not exist", userId) };
                    }
                    List<APIUserPermission> listOfUserPermission = new List<APIUserPermission>();
                    foreach (APIUserPermission userPermission in listOfSelectedUserPermission)
                    {
                        APIUserPermission existingUserPermission = dbContext.UserPermissions
                            .Include(up => up.ClientUser)
                            .Include(up => up.Permission)
                            .FirstOrDefault(up => up.ClientUserId == userPermission.ClientUserId
                                && up.PermissionId == userPermission.PermissionId);

                        // check if UserPermission already exist
                        if (existingUserPermission != null)
                        {

                            existingUserPermission.IsCustomPermission = userPermission.IsCustomPermission;
                            existingUserPermission.IsDismantledPermission = userPermission.IsDismantledPermission;

                            SetLastModifiedLog(existingUserPermission);
                            listOfUserPermission.Add(existingUserPermission);
                        }
                        else
                        {
                            APIEndpointPermission endpointPermission = dbContext.EndpointPermissions.FirstOrDefault(up => up.Id == userPermission.PermissionId);
                            APIClientUser clientUser = dbContext.ClientUsers.FirstOrDefault(cu => cu.Id == userPermission.ClientUserId);

                            if (clientUser != null)
                            {
                                userPermission.ClientUser = clientUser;
                                userPermission.Permission = endpointPermission;

                                SetCreatedLog(userPermission);
                                listOfUserPermission.Add(userPermission);
                            }

                        }
                    }
                    SaveUserPermission(dbContext, existingUser, listOfUserPermission);
                    transaction.Commit();
                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "User Permission has been successully saved." };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = GetInnerException(ex).Message };
                }
            }

        }

        public void SaveUserPermission(DNETInterfaceDBContext dbContext, APIUser user, List<APIUserPermission> listOfUserPermission)
        {
            try
            {
                foreach (APIUserPermission userPermission in listOfUserPermission)
                {

                    if (userPermission.ClientUser.UserId != user.Id)
                    {
                        throw new Exception("Client User is not the same with User");
                    }

                    APIUserPermission existingUserPermission = dbContext.UserPermissions.FirstOrDefault(x => x.Id == userPermission.Id);

                    if (existingUserPermission != null)
                    {
                        // update
                        SetLastModifiedLog(userPermission);
                        dbContext.Entry(existingUserPermission).CurrentValues.SetValues(userPermission);
                        dbContext.SaveChanges();

                    }
                    else
                    {
                        // add new
                        SetCreatedLog(userPermission);
                        dbContext.UserPermissions.Add(userPermission);
                    }
                    dbContext.SaveChanges();
                }

                // remove unselected permissions
                if (user.Id != AppConfigs.GetInt("DMSAdminUserId"))
                {
                    List<int> listOfSelectedUserPermissionId = listOfUserPermission.Select(up => up.PermissionId).ToList();
                    List<APIUserPermission> listOfRemovedUserPermission = dbContext.UserPermissions.Where(up => up.ClientUser.UserId == user.Id && !listOfSelectedUserPermissionId.Contains(up.PermissionId)).ToList();
                    dbContext.UserPermissions.RemoveRange(listOfRemovedUserPermission);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete by Id - Not Implemented
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
