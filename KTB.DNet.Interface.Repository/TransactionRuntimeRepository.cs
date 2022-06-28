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
    public class TransactionRuntimeRepository : BaseRepository, ITransactionRuntimeRepository<TransactionRuntime, long>
    {
        public TransactionRuntimeRepository()
        {
        }

        public TransactionRuntime Get(long id)
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                return db.TransactionRuntimes.FirstOrDefault(x => x.Id == id);
            }
        }

        public ResponseMessage Create(TransactionRuntime entity)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    db.TransactionRuntimes.Add(entity);
                    db.SaveChanges();

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("ThreadLog has been successfully created"), Data = entity };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        public ResponseMessage Update(TransactionRuntime entity)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    var threadLogItem = db.TransactionRuntimes.FirstOrDefault(x => x.Id == entity.Id);

                    threadLogItem.StartedTime = entity.StartedTime;
                    threadLogItem.FinishedTime = entity.FinishedTime;
                    threadLogItem.TransactionLogId = entity.TransactionLogId;
                    threadLogItem.MethodName = entity.MethodName;
                    threadLogItem.TransactionLog = entity.TransactionLog;
                    db.SaveChanges();

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = string.Format("ThreadLog has been successfully updated"), Data = threadLogItem };
                }


            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        public ResponseMessage Delete(long id)
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                var threadLogItem = db.TransactionRuntimes.SingleOrDefault(x => x.Id == id);
                if (threadLogItem != null)
                {
                    db.TransactionRuntimes.Remove(threadLogItem);
                    db.SaveChanges();

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "ThreadLog has been successfully deleted" };
                }

                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "ThreadLog id is invalid" };
            }
        }

        public List<TransactionRuntime> GetAll()
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                return db.TransactionRuntimes.ToList();
            }
        }

        /// <summary>
        /// Searching log
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<TransactionRuntime> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            List<TransactionRuntime> result = null;

            try
            {
                string keyword;
                PropertyInfo orderedProperty = null;
                int take, skip;
                bool orderDir;

                GetPostModelData<TransactionRuntime>(model, out keyword, "MethodName", out orderedProperty, out orderDir, out take, out skip);
                // search the dbase taking into consideration table sorting and paging
                result = Filter(keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount);
                if (result == null)
                {
                    // empty collection...
                    return new List<TransactionRuntime>();
                }

                return result;

            }
            catch
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<TransactionRuntime>();
            }
        }

        /// <summary>
        /// Filter Thread log
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        private List<TransactionRuntime> Filter(string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount)
        {
            List<TransactionRuntime> result = new List<TransactionRuntime>();

            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                IQueryable<TransactionRuntime> queryablePermission = db.TransactionRuntimes;

                result = queryablePermission.AsEnumerable()
                               .Where(
                                        u =>
                                        {
                                            bool keywordExists = !string.IsNullOrEmpty(keyword);

                                            return !keywordExists ||
                                                (keywordExists && (u.MethodName != null ? u.MethodName.ToUpper().Contains(keyword) : false));
                                        }
                                     )
                               .OrderByWithDirection(u => orderedProperty.GetValue(u, null), !orderDir)
                               .Skip(skip)
                               .Take(take)
                               .ToList();

                // get the execution time
                foreach (var item in result)
                {
                    item.ExecutionTime = (int)item.FinishedTime.Subtract(item.StartedTime).TotalMilliseconds;
                }

                // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
                filteredResultsCount = db.TransactionRuntimes
                               .Where(
                                        u =>
                                        string.IsNullOrEmpty(keyword) ||
                                        u.MethodName.ToUpper().Contains(keyword)).Count();
                totalResultsCount = db.TransactionRuntimes.Count();
            }

            return result;
        }
    }
}
