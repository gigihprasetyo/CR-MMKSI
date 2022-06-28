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
    /// <summary>
    /// Theottle repository class
    /// </summary>
    public class ThrottleRepository : BaseRepository, IThrottleRepository<APIThrottle, int>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public ThrottleRepository()
        {
        }

        /// <summary>
        /// Get throttle interface
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public APIThrottle Get(int id)
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.Throttles.Include(t => t.Endpoint).FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// Get throttle by URI
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public APIThrottle GetByUri(string uri)
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.Throttles.Include(t => t.Endpoint).FirstOrDefault(x => x.Endpoint.URI == uri);
            }
        }

        /// <summary>
        /// Create throttle
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(APIThrottle entity)
        {

            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var existingThrottle = db.Throttles.FirstOrDefault(x => x.EndpointId == entity.EndpointId);

                    if (existingThrottle != null)
                    {
                        return new ResponseMessage()
                        {
                            Success = false,
                            Status = ResponseStatus.Warning,
                            Message = "Throttle already exist.",
                            Data = existingThrottle
                        };
                    }
                    APIEndpointPermission endpoint = db.EndpointPermissions.FirstOrDefault(ep => ep.Id == entity.EndpointId);

                    if (endpoint == null)
                    {
                        return new ResponseMessage()
                        {
                            Success = false,
                            Status = ResponseStatus.Warning,
                            Message = "Invalid Endpoint",
                            Data = existingThrottle
                        };
                    }

                    entity.Endpoint = endpoint;
                    SetCreatedLog(entity);

                    db.Throttles.Add(entity);
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Message = "Throttle Created Successfully.",
                        Data = entity
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = GetInnerException(ex).Message
                };
            }

        }

        /// <summary>
        /// Update throttle
        /// </summary>
        /// <param name="entity"></param>
        public ResponseMessage Update(APIThrottle entity)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var existingThrottle = db.Throttles.Include(t => t.Endpoint).FirstOrDefault(t => t.Id == entity.Id);

                    if (existingThrottle == null)
                    {
                        return new ResponseMessage()
                        {
                            Success = false,
                            Status = ResponseStatus.Error,
                            Message = string.Format("Throttle with Id {0} does not exist", entity.Id),
                            Data = entity
                        };
                    }
                    existingThrottle.RequestLimit = entity.RequestLimit;
                    existingThrottle.TimeInSeconds = entity.TimeInSeconds;
                    existingThrottle.Enable = entity.Enable;

                    SetLastModifiedLog(existingThrottle);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Message = "Throttle has been updated successfully.",
                        Data = existingThrottle
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = GetInnerException(ex).Message,
                    Data = entity
                };
            }
        }

        /// <summary>
        /// Delete throttle
        /// </summary>
        /// <param name="id"></param>
        public ResponseMessage Delete(int id)
        {
            try
            {
                using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
                {
                    var throttle = db.Throttles.SingleOrDefault(x => x.Id == id);
                    if (throttle != null)
                    {
                        db.Throttles.Remove(throttle);
                        db.SaveChanges();
                    }

                    return new ResponseMessage()
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Message = "Throttle has been deleted successfully."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = GetInnerException(ex).Message
                };
            }
        }

        /// <summary>
        /// Get all throttles
        /// </summary>
        /// <returns></returns>
        public List<APIThrottle> GetAll()
        {
            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                return db.Throttles.Include(t => t.Endpoint).ToList();
            }
        }

        /// <summary>
        /// Searching throttle
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<APIThrottle> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            List<APIThrottle> result = null;

            try
            {
                string keyword;
                PropertyInfo orderedProperty = null;
                int take, skip;
                bool orderDir;

                GetPostModelData<APIThrottle>(model, out keyword, "Name", out orderedProperty, out orderDir, out take, out skip);

                // search the dbase taking into consideration table sorting and paging
                result = Filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);
                if (result == null)
                {
                    // empty collection...
                    return new List<APIThrottle>();
                }

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<APIThrottle>();
            }

            return result;
        }


        /// <summary>
        /// Filter throttle
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        private List<APIThrottle> Filter(string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount)
        {
            List<APIThrottle> result = new List<APIThrottle>();

            using (DNETInterfaceDBContext db = new DNETInterfaceDBContext())
            {
                IQueryable<APIThrottle> queryablePermission = db.Throttles.Include(t => t.Endpoint);

                result = queryablePermission.AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return !keywordExists ||
                                                (keywordExists && (u.Endpoint.Name != null ? u.Endpoint.Name.ToUpper().Contains(keyword) : false));
                                        }
                                     )
                                     .OrderByWithDirection(u => orderedProperty != null ? orderedProperty.GetValue(u, null) : u.Endpoint.Name, !orderDir)
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

    }
}
