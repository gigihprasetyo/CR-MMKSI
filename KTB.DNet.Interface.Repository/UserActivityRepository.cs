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
    public class UserActivityRepository : BaseRepository, IUserActivityRepository<UserActivity, long>
    {
        /// <summary>
        /// UserActivityRepository Constructor
        /// </summary>
        public UserActivityRepository()
        {
        }

        /// <summary>
        /// Get By UserActivityId 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserActivity Get(long id)
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                return db.UserActivities.FirstOrDefault(x => x.Id == id);
            }
        }


        /// <summary>
        ///     Creates and saves a new 'UserActivity' entity.
        /// </summary>
        /// <param name="entity">The UserActivity to save.</param>
        /// <returns>The id (int) of the newly created UserActivity.</returns>
        public ResponseMessage Create(UserActivity entity)
        {
            try
            {
                SetCreatedLog(entity);
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    db.UserActivities.Add(entity);
                    db.SaveChanges();

                }
                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New User Activity has been successully created.", Data = entity };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }


        /// <summary>
        ///     Updates a single 'UserActivity' entity.
        /// </summary>
        /// <param name="entity">The UserActivity to update.</param>
        public ResponseMessage Update(UserActivity entity)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    UserActivity userActivity = db.UserActivities.Find(entity.Id);
                    userActivity.Username = entity.Username;
                    userActivity.Endpoint = entity.Endpoint;
                    userActivity.Activity = entity.Activity;
                    userActivity.ActivityTime = entity.ActivityTime;
                    userActivity.ActivityDesc = entity.ActivityDesc;
                    userActivity.DealerCode = entity.DealerCode;
                    userActivity.AppId = entity.AppId;

                    SetLastModifiedLog(userActivity);
                    db.SaveChanges();

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "User Activity has been successfully updated.", Data = userActivity };
                }

            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }


        /// <summary>
        ///     Deletes a single UserActivity.
        /// </summary>
        /// <param name="id">The id of the UserActivity to delete.</param>
        public ResponseMessage Delete(long id)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    UserActivity userActivity = db.UserActivities.SingleOrDefault(f => f.Id == id);
                    if (userActivity != null)
                    {
                        db.UserActivities.Remove(userActivity);
                        db.SaveChanges();
                    }

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "User Activity has been deleted.", Data = userActivity };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }


        /// <summary>
        ///     Gets all 'UserActivity' entities as an IQueryable.
        /// </summary>
        /// <returns>An IQueryable of all 'UserActivity' entities.</returns>
        public List<UserActivity> GetAll()
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                return db.UserActivities.ToList();
            }
        }

        /// <summary>
        /// Get UserActivity count
        /// </summary>
        /// <returns></returns>
        public int GetUserActivitiesCount()
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                return db.UserActivities.Count();
            }
        }

        /// <summary>
        /// Filter userActivity
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<UserActivity> Search(string dealerCode, DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            string keyword;
            PropertyInfo orderedProperty = null;
            int take, skip;
            bool orderDir;

            GetPostModelData<UserActivity>(model, out keyword, "ActivityTime", out orderedProperty, out orderDir, out take, out skip);

            // search the dbase taking into consideration table sorting and paging
            List<UserActivity> result = Filter(dealerCode, keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);


            if (result == null)
            {
                // empty collection...
                return new List<UserActivity>();
            }

            return result;
        }

        private List<UserActivity> Filter(string dealerCode, string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount)
        {
            List<UserActivity> result = new List<UserActivity>();
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                IQueryable<UserActivity> queryableEndPoint = db.UserActivities;

                result = queryableEndPoint.AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return (!keywordExists ||
                                                (keywordExists &&
                                                (
                                                    (u.Username != null ? u.Username.ToUpper().Contains(keyword) : false) ||
                                                    (u.ActivityDesc != null ? u.Username.ToUpper().Contains(keyword) : false)
                                                ))) && 
                                                (dealerCode == string.Empty || u.DealerCode == dealerCode);
                                        }
                                     )
                               .OrderByWithDirection(u => orderedProperty.GetValue(u, null), orderDir)
                               .Skip(skip)
                               .Take(take)
                               .ToList();

                // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
                filteredResultsCount = db.UserActivities.AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return (!keywordExists ||
                                                (keywordExists &&
                                                (
                                                    (u.Username != null ? u.Username.ToUpper().Contains(keyword) : false) ||
                                                    (u.ActivityDesc != null ? u.ActivityDesc.ToUpper().Contains(keyword) : false)
                                                ))) &&
                                                (dealerCode == string.Empty || u.DealerCode == dealerCode);
                                        }).Count();
                totalResultsCount = db.UserActivities.Count();
            }

            return result;
        }




        public List<UserActivity> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
    }
}
