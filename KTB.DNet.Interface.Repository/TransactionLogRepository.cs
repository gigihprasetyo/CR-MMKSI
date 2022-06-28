using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Persistence.Logging;
using KTB.DNet.Interface.Repository.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace KTB.DNet.Interface.Repository
{
    public class TransactionLogRepository : BaseRepository, ITransactionLogRepository<TransactionLog, long>
    {
        public TransactionLogRepository()
        {
        }

        /// <summary>
        /// Get Transaction Log b 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionLog Get(long id)
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                return db.TransactionLogs.FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// Create Transaction Log 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(TransactionLog entity)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    SetCreatedLog(entity);
                    db.TransactionLogs.Add(entity);
                    db.SaveChanges();

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "New TransactionLog has been successfully created.", Data = entity };
                }

            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Update Transactiobn Log 
        /// </summary>
        /// <param name="entity"></param>
        public ResponseMessage Update(TransactionLog entity)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    TransactionLog transactionLog = db.TransactionLogs.FirstOrDefault(x => x.Id == entity.Id);
                    transactionLog.Username = entity.Username;
                    transactionLog.Token = entity.Token;
                    transactionLog.Endpoint = entity.Endpoint;
                    transactionLog.SenderIP = entity.SenderIP;
                    transactionLog.Input = entity.Input;
                    transactionLog.Output = entity.Output;
                    transactionLog.Status = entity.Status;
                    transactionLog.ParentId = entity.ParentId;
                    transactionLog.DealerCode = entity.DealerCode;
                    transactionLog.AppId = entity.AppId;
                    transactionLog.ClientId = entity.ClientId;
                    transactionLog.IsResolved = entity.IsResolved;
                    SetLastModifiedLog(transactionLog);
                    db.SaveChanges();

                    return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "TransactionLog has been successfully updated.", Data = transactionLog };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Delete Transaction Log Based on Id 
        /// </summary>
        /// <param name="id"></param>
        public ResponseMessage Delete(long id)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    var transLogItem = db.TransactionLogs.SingleOrDefault(x => x.Id == id);
                    if (transLogItem != null)
                    {
                        db.TransactionLogs.Remove(transLogItem);
                        db.SaveChanges();

                        return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "TransactionLog has been successfully deleted." };
                    }

                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "TransactionLog not found in database." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }

        /// <summary>
        /// Get All Transaction Log
        /// </summary>
        /// <returns></returns>
        public List<TransactionLog> GetAll()
        {
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                return db.TransactionLogs.ToList();
            }
        }

        /// <summary>
        /// Get Top 5 API
        /// </summary>
        /// <returns></returns>
        public List<TopApiModel> GetTopApiList(int take)
        {
            try
            {
                List<TopApiModel> result = new List<TopApiModel>();
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    var list = (from c in db.TransactionLogs
                                //where !c.Endpoint.Contains("localhost")
                                group c by c.Endpoint into g
                                orderby g.Count() descending
                                select new { g.Key, Count = g.Count() }).Take(take);

                    var listData = list.ToList();
                    int no = 1;
                    foreach (var item in listData)
                    {
                        result.Add(new TopApiModel(no++, item.Key, item.Count));
                    }
                }

                return result;
            }
            catch
            {
                return new List<TopApiModel>();
            }
        }

        /// <summary>
        /// Get full ranked api list
        /// </summary>
        /// <returns></returns>
        public List<TopApiModel> GetRankedApiList(DataTablePostModel model, out int filteredCount, out int totalCount)
        {
            filteredCount = 0;
            totalCount = 0;
            try
            {
                List<TopApiModel> result = new List<TopApiModel>();
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    var list = (from c in db.TransactionLogs
                                //where !c.Endpoint.Contains("localhost")
                                group c by c.Endpoint into g
                                orderby g.Count() descending
                                select new { g.Key, Count = g.Count() });

                    var listData = list.ToList();
                    int no = 1;
                    foreach (var item in listData)
                    {
                        result.Add(new TopApiModel(no++, item.Key, item.Count));
                    }
                }

                filteredCount = result.Count;
                totalCount = result.Count;

                return result;
            }
            catch
            {
                return new List<TopApiModel>();
            }
        }


        /// <summary>
        /// Searching log
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<TransactionLog> Search(string dealerCode, DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, bool isGetErrorList = false, bool isShowRead = true)
        {
            List<TransactionLog> result = null;

            try
            {
                string keyword;
                PropertyInfo orderedProperty = null;
                int take, skip;
                bool orderDir;

                GetPostModelData<TransactionLog>(model, out keyword, "Id", out orderedProperty, out orderDir, out take, out skip);

                // search the dbase taking into consideration table sorting and paging
                result = Filter(dealerCode, keyword, take, skip, orderedProperty, orderDir, out filteredResultsCount, out totalResultsCount, isGetErrorList);
                if (result == null)
                {
                    // empty collection...
                    return new List<TransactionLog>();
                }

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<TransactionLog>();
            }

            return result;
        }

        /// <summary>
        /// Filter transaction log
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortDir"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        private List<TransactionLog> Filter(string dealerCode, string keyword, int take, int skip, PropertyInfo orderedProperty, bool orderDir, out int filteredResultsCount, out int totalResultsCount, bool isGetErrorList = false)
        {
            List<TransactionLog> result = new List<TransactionLog>();

            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {
                if (isGetErrorList)
                {
                    IQueryable<TransactionLog> queryablePermission = db.TransactionLogs;

                    result = queryablePermission.AsEnumerable()
                                   .Where(
                                            u =>
                                            {
                                                bool keywordExists = !string.IsNullOrEmpty(keyword);

                                                return (!keywordExists ||
                                                    (keywordExists && (u.CreatedBy != null ? u.CreatedBy.ToUpper().Contains(keyword) : false) ||
                                                        (u.Endpoint != null ? u.Endpoint.ToUpper().Contains(keyword) : false) ||
                                                        u.Id.ToString().Contains(keyword))) &&
                                                    u.ParentId == null &&
                                                    u.Status.Equals(false) &&
                                                    (dealerCode == string.Empty || u.DealerCode == dealerCode);
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

                    // update the status
                    foreach (var item in result)
                    {
                        item.IsResolved = db.TransactionLogs.Any(x => x.ParentId != null && x.ParentId.Value.Equals(item.Id) && x.Status);
                    }


                }
                else
                {
                    IQueryable<TransactionLog> queryablePermission = db.TransactionLogs;

                    result = queryablePermission.AsEnumerable()
                                   .Where(
                                            u =>
                                            {
                                                bool keywordExists = !string.IsNullOrEmpty(keyword);

                                                return (!keywordExists ||
                                                    (keywordExists && (u.CreatedBy != null ? u.CreatedBy.ToUpper().Contains(keyword) : false) ||
                                                    (u.Endpoint != null ? u.Endpoint.ToUpper().Contains(keyword) : false) ||
                                                        u.Id.ToString().Contains(keyword))) &&
                                                    u.ParentId == null &&
                                                    (dealerCode == string.Empty || u.DealerCode == dealerCode);
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

                    // update the status
                    foreach (var item in result)
                    {
                        if (item.Status)
                            item.IsResolved = true;
                        else
                            item.IsResolved = db.TransactionLogs.Any(x => x.ParentId != null && x.ParentId.Value.Equals(item.Id) && x.Status);
                    }
                }

                // get total of filtered rows
                filteredResultsCount = db.TransactionLogs.Where(u => string.IsNullOrEmpty(keyword) || u.CreatedBy.ToUpper().Contains(keyword) || u.Endpoint.Contains(keyword) || u.Id.ToString().Contains(keyword)).Count();

                // get total of transaction log
                totalResultsCount = db.TransactionLogs.Count();
            }

            return result;
        }

        /// <summary>
        /// Get 5 latest transaction
        /// </summary>
        /// <returns></returns>
        public List<TransactionLog> GetLatestTrans(string dealerCode, int take)
        {
            try
            {
                int filterCount, totalCount;

                return Filter(dealerCode, "", take, 0, typeof(TransactionLog).GetProperty("Id"), false, out filterCount, out totalCount);
            }
            catch
            {
                return new List<TransactionLog>();
            }
        }

        /// <summary>
        /// Get latest recent error
        /// </summary>
        /// <returns></returns>
        public List<TransactionLog> GetRecentFailedTrans(string dealerCode, int take)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    var list = (from c in db.TransactionLogs
                                where c.Status.Equals(false) && c.ParentId.Equals(null)
                                 && (dealerCode == string.Empty || c.DealerCode == dealerCode)
                                orderby c.Id descending
                                select c).Take(take);

                    var tempList = list.ToList();

                    foreach (var item in tempList)
                    {
                        item.IsResolved = db.TransactionLogs.Any(x => x.ParentId != null && x.ParentId.Value.Equals(item.Id) && x.Status);
                    }

                    return tempList;
                }
            }
            catch
            {
                return new List<TransactionLog>();
            }
        }

        /// <summary>
        /// Get resend transaction list by ID
        /// </summary>
        /// <returns></returns>
        public List<TransactionLog> GetResendTransByID(int Id)
        {
            try
            {
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    var list = (from c in db.TransactionLogs
                                where c.ParentId == Id
                                orderby c.Id descending
                                select c);

                    return list.ToList();
                }
            }
            catch
            {
                return new List<TransactionLog>();
            }
        }

        /// <summary>
        /// Get top dealer transaction list
        /// </summary>
        /// <returns></returns>
        public List<string> GetTopDealerTransactionList()
        {
            try
            {
                List<string> result = new List<string>();
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    var list = (from c in db.TransactionLogs
                                where c.ParentId.Equals(null)
                                group c by c.CreatedBy into dealer
                                orderby dealer.Count() descending
                                select new
                                {
                                    DealerCode = dealer.Key,
                                    TotalCount = dealer.Count()
                                }).Take(5);

                    var listData = list.OrderBy(x => x.DealerCode).ToList();

                    foreach (var item in listData)
                    {
                        int succeedCount = db.TransactionLogs.Count(x => x.CreatedBy.Equals(item.DealerCode) && x.Status);
                        int failedCount = db.TransactionLogs.Count(x => x.CreatedBy.Equals(item.DealerCode) && !x.Status);
                        result.Add(item.DealerCode + "|" + succeedCount + "|" + failedCount + "|" + item.TotalCount);
                    }
                }

                return result;
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Search Transaction Log
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<TransactionLog> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        #region Method Get Daily Transaction Count
        /// <summary>
        /// Get Daily Transaction Status Count
        /// </summary>
        /// <returns></returns>

        public List<TransactionLog> GetDailyTransactionCount()
        {
            List<TransactionLog> result = new List<TransactionLog>();
            using (DNetLoggingDBContext db = new DNetLoggingDBContext())
            {

                IQueryable<TransactionLog> queryablePermission = db.TransactionLogs;

                result = queryablePermission.AsEnumerable()
                               .Where(
                                        u => u.CreatedTime.Value.Date == DateTime.Now.Date
                                     )
                               .OrderByWithDirection(u => u.CreatedTime.Value, true)
                               .ToList();

            }

            return result;
        }
        #endregion


        #region Get Failed Transaction Summary Per Dealer
        public JToken GetFailedTransactionSummaryPerDealer(DateTime transactionDate, string dealerCode = null)
        {
            transactionDate = transactionDate <= Constants.DATETIME_DEFAULT_VALUE ? DateTime.Now : transactionDate;

            try
            {
                JArray result = new JArray();
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    var summaryData = (from t in db.TransactionLogs
                                       where t.DealerCode != null && (string.IsNullOrEmpty(dealerCode) || (!string.IsNullOrEmpty(dealerCode) && dealerCode == t.DealerCode)) && (t.ParentId == null || (t.ParentId.HasValue && t.ParentId.Value == 0)) && t.Status == false && (t.CreatedTime.HasValue && DbFunctions.TruncateTime(t.CreatedTime.Value) == transactionDate.Date)
                                       group t by t.DealerCode into g
                                       select new { g.Key, Count = g.Count() }).ToList();

                    foreach (var item in summaryData)
                    {
                        JObject failedTransactionPerDealer = new JObject();
                        failedTransactionPerDealer.Add("DealerCode", item.Key);
                        failedTransactionPerDealer.Add("Total", item.Count);

                        result.Add(failedTransactionPerDealer);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return new JArray();
            }

        }
        #endregion


        #region Get Transaction Summary
        public JToken GetTransactionSummary(DateTime transactionDate, string dealerCode = null)
        {
            transactionDate = transactionDate <= Constants.DATETIME_DEFAULT_VALUE ? DateTime.Now : transactionDate;

            try
            {
                JArray result = new JArray();
                using (DNetLoggingDBContext db = new DNetLoggingDBContext())
                {
                    // scalar squery to get total success transaction
                    string totalSuccessTransactionQuery = "SELECT COUNT(Id) FROM TransactionLog " +
                                                    "WHERE DealerCode IS NOT NULL AND " +
                                                    "(@dealerCode = '' OR @dealerCode = DealerCode) AND " +
                                                    "(ParentId IS NULL OR ParentId = 0) AND " +
                                                    "Status = 1 AND " +
                                                    "CONVERT(VARCHAR(10), CreatedTime, 120) = @date";

                    SqlParameter successTransactionDateParam = new SqlParameter("date", SqlDbType.Char);
                    successTransactionDateParam.Value = transactionDate.ToString("yyyy-MM-dd");
                    SqlParameter successDealerCodeParam = new SqlParameter("dealerCode", SqlDbType.Char);
                    successDealerCodeParam.Value = dealerCode;

                    int totalSuccessTransaction = db.Database.SqlQuery<int>(totalSuccessTransactionQuery, successTransactionDateParam, successDealerCodeParam).Single();
                    JObject successTransaction = new JObject();
                    successTransaction.Add("TransactionType", "Success");
                    successTransaction.Add("Total", totalSuccessTransaction);
                    successTransaction.Add("Color", "#05ff12");

                    result.Add(successTransaction);

                    // scalar squery to get total failed transaction which has been resolved
                    string totalResolvedTransactionQuery = "SELECT COUNT(Id) FROM TransactionLog " +
                                                    "WHERE DealerCode IS NOT NULL AND " +
                                                    "(@dealerCode = '' OR @dealerCode = DealerCode) AND " +
                                                    "(ParentId IS NOT NULL OR ParentId != 0) AND " +
                                                    "Status = 1 AND " +
                                                    "CONVERT(VARCHAR(10), CreatedTime, 120) = @date";

                    SqlParameter resolvedTransactionDateParam = new SqlParameter("date", SqlDbType.Char);
                    resolvedTransactionDateParam.Value = transactionDate.ToString("yyyy-MM-dd");
                    SqlParameter resolvedDealerCodeParam = new SqlParameter("dealerCode", SqlDbType.Char);
                    resolvedDealerCodeParam.Value = dealerCode;

                    int totalResolvedTransaction = db.Database.SqlQuery<int>(totalResolvedTransactionQuery, resolvedTransactionDateParam, resolvedDealerCodeParam).Single();
                    JObject resolvedTransaction = new JObject();
                    resolvedTransaction.Add("TransactionType", "Resolved");
                    resolvedTransaction.Add("Total", totalResolvedTransaction);
                    resolvedTransaction.Add("Color", "#10c2ff");

                    result.Add(resolvedTransaction);

                    // scalar squery to get total failed transaction
                    string totalFailedTransactionQuery = "SELECT COUNT(Id) FROM TransactionLog " +
                                                    "WHERE DealerCode IS NOT NULL AND " +
                                                    "(@dealerCode = '' OR @dealerCode = DealerCode) AND " +
                                                    "(ParentId IS NULL OR ParentId = 0) AND " +
                                                    "Status = 0 AND " +
                                                    "CONVERT(VARCHAR(10), CreatedTime, 120) = @date";

                    SqlParameter failedTransactionDateParam = new SqlParameter("date", SqlDbType.Char);
                    failedTransactionDateParam.Value = transactionDate.ToString("yyyy-MM-dd");
                    SqlParameter failedDealerCodeParam = new SqlParameter("dealerCode", SqlDbType.Char);
                    failedDealerCodeParam.Value = dealerCode;

                    int totalFailedTransaction = db.Database.SqlQuery<int>(totalFailedTransactionQuery, failedTransactionDateParam, failedDealerCodeParam).Single();
                    JObject failedTransaction = new JObject();
                    failedTransaction.Add("TransactionType", "Failed");
                    failedTransaction.Add("Total", totalFailedTransaction - totalResolvedTransaction);
                    failedTransaction.Add("Color", "#ff3030");
                    result.Add(failedTransaction);
                }

                return result;
            }
            catch (Exception ex)
            {
                return new JArray();
            }
        }
        #endregion


        public List<TransactionLog> SearchByParam(TransactionDataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, bool isGetErrorList = false, bool isShowRead = true)
        {
            throw new NotImplementedException();
        }


        public JToken GetTransactionSummaryPerDealer(DateTime transactionDate)
        {
            throw new NotImplementedException();
        }
    }
}
