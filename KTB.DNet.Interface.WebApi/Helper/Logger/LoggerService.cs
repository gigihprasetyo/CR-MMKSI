#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : LoggerService class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Elmah;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Runtime.Caching;
using System.Threading;
using System.Web;
#endregion

namespace KTB.DNet.Interface.WebApi
{
    /// <summary>
    /// Log <see cref="Exception"/> objects.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        #region Variables
        private IApplicationConfigRepository<ApplicationConfig, long> _applicationConfigRepo;
        private ITransactionLogRepository<TransactionLog, long> _transactionLogRepo;
        private ITransactionRuntimeRepository<TransactionRuntime, long> _transactionRuntimeRepo;

        MemoryCache memCache = MemoryCache.Default;
        private static readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        #endregion

        #region Properties
        /// <summary>
        /// IsRuntimeLogEnabled
        /// </summary>
        protected bool IsRuntimeLogEnabled
        {
            get { return GetLoggingAvailability(Constants.ConfigKey.WebAPI_TransactionRuntime_Enable, true); }
        }
        /// <summary>
        /// IsTransationLogEnabled
        /// </summary>
        protected bool IsTransationLogEnabled
        {
            get { return GetLoggingAvailability(Constants.ConfigKey.WebAPI_TransactionLogging_Enable, true); }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LoggerService()
        {
            _applicationConfigRepo = new ApplicationConfigRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.LogConnection));
            _transactionLogRepo = new TransactionLogRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.LogConnection));
            _transactionRuntimeRepo = new TransactionRuntimeRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.LogConnection));

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Set Modifier for Created By and Updated By
        /// </summary>
        public void SetUserModifier(string username)
        {
            _applicationConfigRepo.SetUserLogin(username);
            _transactionLogRepo.SetUserLogin(username);
            _transactionRuntimeRepo.SetUserLogin(username);
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Log(Exception exception)
        {
            try
            {
                // Log to Elmah.
                ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
            }
            catch
            {
                // do nothing
            }
        }

        /// <summary>
        /// LogTransaction
        /// </summary>
        /// <param name="dataLog"></param>
        /// <param name="dealerCode"></param>
        public long LogTransaction(DataLogModel dataLog, string dealerCode)
        {
            try
            {
                if (IsTransationLogEnabled)
                {
                    TransactionLog transactionLog = ExtractDataLog(dataLog, dealerCode);

                    ResponseMessage response = _transactionLogRepo.Create(transactionLog);

                    if (response.Success)
                    {
                        return ((TransactionLog)response.Data).Id;
                    }
                }

                return -1;
            }
            catch
            {
                // do nothing
                return -1;
            }
        }

        /// <summary>
        /// LogTransactionRuntime
        /// </summary>
        /// <param name="transactionRuntime"></param>
        /// <returns></returns>
        public long LogTransactionRuntime(TransactionRuntime transactionRuntime)
        {
            try
            {
                if (IsRuntimeLogEnabled)
                {
                    long threadId = 0;
                    transactionRuntime.MethodName = HttpContext.Current.Request.Url.AbsoluteUri;
                    ResponseMessage result = _transactionRuntimeRepo.Create(transactionRuntime);

                    if (result.Success)
                    {
                        threadId = ((TransactionRuntime)result.Data).Id;
                    }

                    return threadId;
                }

                return -1;
            }
            catch
            {
                // do nothing
                return -1;
            }
        }

        /// <summary>
        /// Service Get Thread Log By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TransactionRuntime GetTransactionRuntime(long Id)
        {
            return _transactionRuntimeRepo.Get(Id);
        }

        /// <summary>
        /// Reset log status from memory cache
        /// </summary>
        public void LogCachingReset()
        {
            try
            {
                memCache.Remove(Constants.ConfigKey.WebAPI_TransactionRuntime_Enable);
                memCache.Remove(Constants.ConfigKey.WebAPI_TransactionLogging_Enable);
                memCache.Remove(Constants.ConfigKey.WebAPI_LoggingOnlyForFailedTransaction_Enable);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// GetLoggingAvailability
        /// </summary>
        /// <param name="configKey"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private bool GetLoggingAvailability(string configKey, bool defaultValue)
        {
            // dafault value, enable logging if there's no configuration
            bool result = defaultValue;

            // read lock
            cacheLock.EnterReadLock();

            try
            {
                // get from memory cache if any
                CacheItem cacheItem = memCache.GetCacheItem(configKey);
                if (cacheItem != null)
                {
                    // convert to bool
                    bool.TryParse(cacheItem.Value.ToString(), out result);

                    return result;
                }
            }
            finally
            {
                cacheLock.ExitReadLock();
            }

            // update lock
            cacheLock.EnterWriteLock();

            try
            {
                // get from database
                var config = _applicationConfigRepo.GetByKey(configKey);

                // get status value
                bool isConfigActive = config == null ? false : config.IsActive;

                if (isConfigActive)
                {
                    result = config.Value.ToBool();
                }

                // define cache policy
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.UtcNow.AddHours(1);

                // add to memory cache
                memCache.Add(configKey, result, cacheItemPolicy);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }

            return result;
        }

        /// <summary>
        /// Parse request data
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        private string ParseRequestData(object requestData)
        {
            string parsedRequestData = string.Empty;

            if (requestData is String)
            {
                parsedRequestData = (string)requestData;
            }
            else
            {
                parsedRequestData = JsonConvert.SerializeObject(requestData);
            }

            return parsedRequestData;
        }

        /// <summary>
        /// Extract data log
        /// </summary>
        /// <param name="dataLog"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        private TransactionLog ExtractDataLog(DataLogModel dataLog, string dealerCode)
        {
            string input = ParseRequestData(dataLog.RequestData);

            TransactionLog transactionLog = new TransactionLog
            {
                SenderIP = dataLog.SenderIP,
                Username = dataLog.Username,
                Token = dataLog.Token,
                AppId = dataLog.AppId,
                ClientId = dataLog.ClientId,
                DealerCode = dealerCode,
                Endpoint = HttpContext.Current.Request.Url.AbsoluteUri,
                Output = JsonConvert.SerializeObject(dataLog.ResponseData),
                Input = input,
                Status = dataLog.Succeed,
                CreatedTime = DateTime.Now,
                CreatedBy = dataLog.Username,
                UpdatedTime = DateTime.Now,
                UpdatedBy = dataLog.Username
            };

            if (dataLog.IsResend)
            {
                var parentLog = _transactionLogRepo.Get(dataLog.ParentId == null ? 0 : dataLog.ParentId.Value);
                if (parentLog != null)
                {
                    transactionLog.ParentId = dataLog.ParentId;
                    transactionLog.UpdatedBy = dataLog.UpdatedBy;
                    transactionLog.CreatedTime = parentLog.CreatedTime;
                    transactionLog.CreatedBy = parentLog.CreatedBy;
                }
            }

            return transactionLog;
        }
        #endregion
    }
}
