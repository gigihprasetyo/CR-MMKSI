using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence;
using KTB.DNet.Interface.Persistence.Stores;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KTB.DNet.Interface.Repository
{
    public class RoleRepository : BaseRepository, IRoleRepository<APIRole, int>
    {
        /// <summary>
        /// Roles repository
        /// </summary>
        /// <param name="ctx"></param>
        public RoleRepository()
        {
        }

        /// <summary>
        /// Get List of API Role By IDs
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<APIRole> GetByIds(List<int> ids)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.Roles.Where(w => ids.Contains(w.Id)).ToList();
                }
            }
            catch (Exception)
            {

                return new List<APIRole>();
            }
        }



        /// <summary>
        /// Get total number of roles
        /// </summary>
        /// <returns></returns>
        public int GetTotalRoles(int userId)
        {
            try
            {
                if (userId == AppConfigs.GetInt("DMSAdminUserId"))
                {
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        return db.Roles.Count();
                    } 
                }
                else
                {
                    return GetUserRole(userId).Count();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// Get User Role
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<APIRole> GetUserRole(int userId)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    List<int> listOfRoleId = db.UserRoles.Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
                    return db.Roles.Where(r => listOfRoleId.Contains(r.Id)).ToList();
                }
            }
            catch (Exception)
            {

                return new List<APIRole>();
            }
        }

        /// <summary>
        /// Create API Role
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIRole entity)
        {
            APIRole role = null;

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    bool isExist = db.Roles.Any(x => x.Name == entity.Name);

                    if (!isExist)
                    {
                        APIRole apiRole = new APIRole();
                        apiRole.Name = entity.Name;
                        apiRole.Level = entity.Level;

                        SetCreatedLog(apiRole);
                        db.Roles.Add(apiRole);
                        db.SaveChanges();
                    }

                    role = db.Roles.FirstOrDefault(x => x.Name == entity.Name);

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("Role has been successfully created"), Data = entity };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }

        }


        /// <summary>
        /// Get API Role By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIRole Get(int id)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    return db.Roles.FirstOrDefault(x => x.Id == id);
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Update API Role
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIRole entity)
        {
            try
            {    
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    APIRole apiRole = db.Roles.Find(entity.Id);

                    apiRole.Name = entity.Name;
                    apiRole.Level = entity.Level;

                    SetLastModifiedLog(apiRole);
                    db.SaveChanges();

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("Role has been successfully updated"), Data = apiRole };
                }


            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }

        }

        /// <summary>
        /// Delete API Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            if (id > 0)
            {
                if (id == AppConfigs.GetInt("DMSAdminRoleId"))
                {
                    throw new Exception("Administrator role is restricted to be deleted.");
                }

                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var existingEntity = db.Roles.FirstOrDefault(x => x.Id == id);

                    if (existingEntity != null)
                    {
                        var existInUser = db.Users.Any(x => x.Roles.Any(u => u.RoleId == id));
                        if (existInUser)
                        {
                            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Role already exist in user" };
                        }

                        db.Roles.Remove(existingEntity);
                        db.SaveChanges();

                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Role has been successfully deleted" };
                    }
                }

            }


            return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Role id is invalid" };
        }


        /// <summary>
        /// Search List of API Role
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APIRole> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            string keyword;
            PropertyInfo orderedProperty = null;
            int take, skip;
            bool orderDir;

            GetPostModelData<APIRole>(model, out keyword, "Name", out orderedProperty, out orderDir, out take, out skip);

            // search the dbase taking into consideration table sorting and paging
            List<APIRole> result = Filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<APIRole>();
            }

            return result;
        }


        /// <summary>
        /// Get all Role
        /// </summary>
        /// <returns></returns>
        public List<APIRole> GetAll()
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    ApplicationRoleManager roleManager = new ApplicationRoleManager(new APIRoleStore(db));

                    return RunSync<List<APIRole>>(roleManager.GetAll);
                }
            }
            catch (Exception)
            {

                return new List<APIRole>();
            }
        }

        /// <summary>
        /// Set Created By and Created Date
        /// </summary>
        /// <param name="apiRole"></param>
        private void SetCreatedLog(APIRole apiRole)
        {
            apiRole.CreatedBy = UserLogin;
            apiRole.CreatedTime = DateTime.Now;
            SetLastModifiedLog(apiRole);
        }

        /// <summary>
        /// Set Updated By and Updated Time
        /// </summary>
        /// <param name="apiRole"></param>
        private void SetLastModifiedLog(APIRole apiRole)
        {
            apiRole.UpdatedBy = UserLogin;
            apiRole.UpdatedTime = DateTime.Now;
        }

        ///// <summary>
        ///// Filter roles
        ///// </summary>
        ///// <param name="searchBy"></param>
        ///// <param name="take"></param>
        ///// <param name="skip"></param>
        ///// <param name="sortBy"></param>
        ///// <param name="sortDir"></param>
        ///// <param name="filteredResultsCount"></param>
        ///// <param name="totalResultsCount"></param>
        ///// <returns></returns>
        private List<APIRole> Filter(string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount)
        {
            var result = new List<APIRole>();
            
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    IQueryable<APIRole> queryableRoles = db.Roles;

                    List<APIRole> netRoles = new List<APIRole>();

                    var roles = queryableRoles.AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return !keywordExists ||
                                                (keywordExists && (u.Name != null ? u.Name.ToUpper().Contains(keyword) : false));
                                        }
                                     )
                                     .OrderByWithDirection(p => orderedProperty.GetValue(p, null), !orderDir)
                                     .ToList()
                              ;

                    netRoles = roles.Skip(skip).Take(take).ToList();

                    // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
                    filteredResultsCount = roles.Count();
                    totalResultsCount = queryableRoles.Count();

                    for (var i = 0; i < netRoles.Count; i++)
                    {
                        var role = new APIRole();
                        role = Map(netRoles[i], role);

                        result.Add(role);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return result;
        }

        ///// <summary>
        ///// Map user role with net role
        ///// </summary>
        ///// <param name="netRole"></param>
        ///// <param name="userRole"></param>
        ///// <returns></returns>
        private APIRole Map(APIRole netRole, APIRole userRole)
        {
            userRole.Id = netRole.Id;
            userRole.Name = netRole.Name;
            userRole.Level = netRole.Level;

            return userRole;
        }


    }
}
