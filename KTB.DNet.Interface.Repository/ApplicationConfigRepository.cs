using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence.Logging;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KTB.DNet.Interface.Repository
{
    public class ApplicationConfigRepository : BaseRepository, IApplicationConfigRepository<ApplicationConfig, int>
    {
        public ApplicationConfigRepository()
        {
        }

        /// <summary>
        /// Create Application Config 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(ApplicationConfig entity)
        {
            try
            {

                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    SetCreatedLog(entity);
                    db.Configurations.Add(entity);
                    db.SaveChanges();
                }

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New Application Config has been successfully created.", Data = entity };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Update Application Config 
        /// </summary>
        /// <param name="entity"></param>
        public ResponseMessage Update(ApplicationConfig entity)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    ApplicationConfig configItem = db.Configurations.Find(entity.Id);

                    if (configItem != null)
                    {
                        configItem.Name = entity.Name;
                        configItem.Value = entity.Value;
                        configItem.IsActive = entity.IsActive;
                        db.SaveChanges();

                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Application Config has been successfully updated.", Data = configItem };
                    }

                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Entity is not valid" };
                }

            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Delete Application Config Based on Id 
        /// </summary>
        /// <param name="id"></param>
        public ResponseMessage Delete(int id)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    var configItem = db.Configurations.SingleOrDefault(x => x.Id == id);
                    if (configItem != null)
                    {
                        db.Configurations.Remove(configItem);
                        db.SaveChanges();

                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Application Config has been successfully deleted." };
                    }

                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Application Config id is not valid" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Get Application Config Based on Id 
        /// </summary>
        /// <param name="id"></param>
        public ApplicationConfig Get(int id)
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                var configItem = db.Configurations.SingleOrDefault(x => x.Id == id);
                return configItem;
            }
        }

        /// <summary>
        /// Get Application Config Based on Its Name
        /// </summary>
        /// <param name="configName"></param>
        public ApplicationConfig GetByName(string configName)
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                return db.Configurations.SingleOrDefault(x => x.Name.Equals(configName, StringComparison.OrdinalIgnoreCase));
            }
        }

        /// <summary>
        /// Get All Application Config
        /// </summary>
        /// <returns></returns>
        public List<ApplicationConfig> GetAll()
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                return db.Configurations.ToList();
            }
        }

        /// <summary>
        /// Searching Application Config
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<ApplicationConfig> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            string keyword;
            PropertyInfo orderedProperty = null;
            int take, skip;
            bool orderDir;

            List<ApplicationConfig> result = null;

            try
            {
                GetPostModelData<ApplicationConfig>(model, out keyword, "Name", out orderedProperty, out orderDir, out take, out skip);

                // search the dbase taking into consideration table sorting and paging
                result = Filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);
                if (result == null)
                {
                    // empty collection...
                    return new List<ApplicationConfig>();
                }

            }
            catch
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<ApplicationConfig>();
            }

            return result;
        }

        /// <summary>
        /// Filter Application Config 
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        private List<ApplicationConfig> Filter(string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount)
        {

            List<ApplicationConfig> result = new List<ApplicationConfig>();

            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                IQueryable<ApplicationConfig> queryablePermission = db.Configurations;

                result = queryablePermission.AsEnumerable()
                           .Where(
                                    u =>
                                    {
                                        bool keywordExists = !string.IsNullOrEmpty(keyword);

                                        return !keywordExists || (keywordExists && (u.Name != null ? u.Name.ToUpper().Contains(keyword) : false));
                                    }
                                 )
                           .OrderByWithDirection(u => orderedProperty.GetValue(u, null), !orderDir)
                           .Skip(skip)
                           .Take(take)
                           .ToList();

                // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
                filteredResultsCount = db.Configurations
                               .Where(
                                        u =>
                                        string.IsNullOrEmpty(keyword) ||
                                        u.Name.ToUpper().Contains(keyword)).Count();

                totalResultsCount = db.Configurations.Count();
            }

            return result;
        }


        /// <summary>
        /// Get app config value 
        /// </summary>
        /// <param name="configName"></param>        
        public bool GetConfigStatus(string configName)
        {
            var config = GetByName(configName);

            return config == null ? false : config.IsActive;
        }

        /// <summary>
        /// Get app config value 
        /// </summary>
        /// <param name="configName"></param>        
        public int GetConfigValue(string configName)
        {
            var config = GetByName(configName);
            if (config != null)
            {
                int result;
                if (int.TryParse(config.Value, out result))
                {
                    return result;
                }
            }

            return 0;
        }

    }
}
