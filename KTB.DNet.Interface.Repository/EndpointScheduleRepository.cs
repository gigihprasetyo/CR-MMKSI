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
    public class EndpointScheduleRepository : BaseRepository, IEndpointScheduleRepository<APIEndpointSchedule, int>
    {
        public EndpointScheduleRepository()
        {
        }

        /// <summary>
        /// Add Endpoint Schedule in Bulk
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage AddBulkEndpointSchedule(List<APIEndpointSchedule> entity)
        {
            try
            {
                if (entity.Count > 0)
                {
                    using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                    {
                        foreach (APIEndpointSchedule endpointSchedule in entity)
                        {
                            SetCreatedLog(endpointSchedule);
                        }
                        db.EndpointSchedules.AddRange(entity);

                        db.SaveChanges();
                    }
                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New Endpoint Schedules have been successfully created." };
                }

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "No Endpoint Schedules inputted" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }

        }

        /// <summary>
        /// Get Endpoint Schedule by Endpoint Id
        /// </summary>
        /// <param name="endPointId"></param>
        /// <returns></returns>
        public List<APIEndpointSchedule> GetByEndpointId(int endPointId)
        {
            List<APIEndpointSchedule> result = new List<APIEndpointSchedule>();
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    //if (endPointId.HasValue)
                    //{
                    var list = db.EndpointSchedules.Where(w => w.EndpointId == (int)endPointId);
                    result = list.ToList();
                    //}
                    //else
                    //{
                    //    var list = db.EndpointSchedules;
                    //    result = list.ToList();
                    //}

                    return result;
                }
            }
            catch
            {
                return new List<APIEndpointSchedule>();
            }
        }

        /// <summary>
        /// Get Endpoint Schedule by Url Endpoint
        /// </summary>
        /// <param name="endpointUrl"></param>
        /// <returns></returns>
        public List<APIEndpointSchedule> GetByEndpointUrl(string endpointUrl)
        {
            var result = new List<APIEndpointSchedule>();

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    if (!String.IsNullOrEmpty(endpointUrl))
                    {
                        var endPoint = db.EndpointPermissions.FirstOrDefault(e => e.URI == endpointUrl);

                        if (endPoint != null)
                        {
                            result = db.EndpointSchedules.Where(es => es.EndpointId == endPoint.Id).ToList();
                        }
                    }
                }
            }
            catch
            {
                return new List<APIEndpointSchedule>();
            }

            return result;
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id"></param>
        public ResponseMessage Delete(int id)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var entity = db.EndpointSchedules.SingleOrDefault(f => f.Id == id);
                    if (entity != null)
                    {
                        db.EndpointSchedules.Remove(entity);
                        db.SaveChanges();

                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Endpoint Schedules has been successfully deleted." };
                    }

                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Invalid Endpoint Schedule id." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }


        /// <summary>
        /// Search By EndPoint ID
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <param name="endPointId"></param>
        /// <returns></returns>
        public List<APIEndpointSchedule> SearchByEndPointId(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, int endPointId)
        {
            List<APIEndpointSchedule> result = null;

            try
            {
                string keyword;
                PropertyInfo orderedProperty = null;
                int take, skip;
                bool orderDir;

                GetPostModelData<APIEndpointSchedule>(model, out keyword, "Id", out orderedProperty, out orderDir, out take, out skip);
                // search the dbase taking into consideration table sorting and paging
                result = filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount, endPointId);
                if (result == null)
                {
                    // empty collection...
                    return new List<APIEndpointSchedule>();
                }

            }
            catch
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIEndpointSchedule>();
            }

            return result;
        }

        /// <summary>
        /// filter endpoint
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <param name="endPointId"></param>
        /// <returns></returns>
        private List<APIEndpointSchedule> filter(string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount, int endPointId)
        {
            List<APIEndpointSchedule> result = new List<APIEndpointSchedule>();

            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                IQueryable<APIEndpointSchedule> queryableEndPoint = db.EndpointSchedules.Include(w => w.Schedule);

                result = queryableEndPoint.AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return (!keywordExists ||
                                                (keywordExists &&
                                                (
                                                    (u.Schedule.Name != null ? u.Schedule.Name.ToUpper().Contains(keyword) : false)
                                                ))) &&
                                                u.EndpointId.Equals(endPointId);
                                        }
                                     )
                               .OrderByWithDirection(u => orderedProperty.GetValue(u, null), orderDir)
                               .Skip(skip)
                               .Take(take)
                               .ToList();

                // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
                filteredResultsCount = db.EndpointSchedules.AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return !keywordExists ||
                                                (keywordExists &&
                                                (
                                                    (u.Schedule.Name != null ? u.Schedule.Name.ToUpper().Contains(keyword) : false)
                                                )) &&
                                                u.EndpointId.Equals(endPointId);
                                        }).Count();
                totalResultsCount = db.EndpointSchedules.Count();

            }

            return result;
        }

        /// <summary>
        /// Get EndpointSchedule by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIEndpointSchedule Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create Endpoint Schedule
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIEndpointSchedule entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update Endpoint Schedule
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(APIEndpointSchedule entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get All Endpoint Schedule
        /// </summary>
        /// <returns></returns>
        public List<APIEndpointSchedule> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Search Endpoint Schedule
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APIEndpointSchedule> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
    }
}
