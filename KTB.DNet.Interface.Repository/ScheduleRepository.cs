using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KTB.DNet.Interface.Repository
{
    public class ScheduleRepository : BaseRepository, IScheduleRepository<APISchedule, int>
    {
        /// <summary>
        /// No parameter constructor
        /// </summary>
        public ScheduleRepository()
        {
        }

        /// <summary>
        /// Get Schedule By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APISchedule Get(int id)
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.Schedules.FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// Get all API Schedules
        /// </summary>
        /// <returns></returns>
        public List<APISchedule> GetAll()
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.Schedules.ToList();
            }
        }

        /// <summary>
        /// Creates API Schedule
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APISchedule entity)
        {
            try
            {
                APISchedule schedule = entity;

                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {

                    SetCreatedLog(schedule);

                    db.Schedules.Add(schedule);
                    db.SaveChanges();
                }

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New Schedule has been successully created.", Data = schedule };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }


        /// <summary>
        /// Delete a single Schedule.
        /// </summary>
        /// <param name="id">The id of the Schedule to delete.</param>
        public ResponseMessage Delete(int id)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var schedule = db.Schedules.SingleOrDefault(f => f.Id == id);
                    if (schedule != null)
                    {
                        db.Schedules.Remove(schedule);
                        db.SaveChanges();
                    }

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Schedule has been successfully deleted" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = ex.Message };
            }
        }

        /// <summary>
        /// Searching schedule
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APISchedule> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            List<APISchedule> result = new List<APISchedule>();
            string keyword;
            PropertyInfo orderedProperty = null;
            int take, skip;
            bool orderDir;

            GetPostModelData<APISchedule>(model, out keyword, "Id", out orderedProperty, out orderDir, out take, out skip);

            try
            {
                // search the dbase taking into consideration table sorting and paging
                result = Filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);
                if (result == null)
                {
                    // empty collection...
                    return new List<APISchedule>();
                }

            }
            catch
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APISchedule>();
            }

            return result;
        }


        /// <summary>
        ///     Updates a single 'Schedule' entity.
        /// </summary>
        /// <param name="entity">The Schedule to update.</param>
        public ResponseMessage Update(APISchedule entity)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    APISchedule schedule = db.Schedules.FirstOrDefault(x => x.Id == entity.Id);

                    if (schedule == null)
                    {
                        return new ResponseMessage() { Success = false, Status = ResponseStatus.Warning, Message = string.Format("Schedule Id is invalid") };
                    }
                    schedule.DealerCode = entity.DealerCode;
                    schedule.Interval = entity.Interval;
                    schedule.MonthDay = entity.MonthDay;
                    schedule.Name = entity.Name;
                    schedule.ScheduleDay = entity.ScheduleDay;
                    schedule.Id = entity.Id;
                    schedule.ScheduleTime = entity.ScheduleTime;
                    schedule.ScheduleType = entity.ScheduleType;

                    SetLastModifiedLog(schedule);

                    db.SaveChanges();

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("Schedule has been successfully updated: ", entity.Name), Data = schedule };
                }

            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = ex.Message };
            }
        }

        /// <summary>
        /// Filter schedule
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        private List<APISchedule> Filter(string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount)
        {
            List<APISchedule> result = new List<APISchedule>();

            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {

                IQueryable<APISchedule> queryableEndPoint = db.Schedules;

                result = queryableEndPoint.AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return !keywordExists ||
                                                (keywordExists &&
                                                (
                                                    (u.Name != null ? u.Name.ToUpper().Contains(keyword) : false) ||
                                                    (u.DealerCode != null ? u.DealerCode.ToUpper().Contains(keyword) : false)
                                                ));
                                                
                                        }
                                     )
                               .OrderByWithDirection(u => orderedProperty.GetValue(u, null), orderDir)
                               .Skip(skip)
                               .Take(take)
                               .ToList();

                // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
                filteredResultsCount = db.Schedules.AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return !keywordExists ||
                                                (keywordExists &&
                                                (
                                                    (u.Name != null ? u.Name.ToUpper().Contains(keyword) : false) ||
                                                    (u.DealerCode != null ? u.DealerCode.ToUpper().Contains(keyword) : false)
                                                ));
                                        }).Count();
                totalResultsCount = db.Schedules.Count();

            }

            return result;
        }

    }
}
