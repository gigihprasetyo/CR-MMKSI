#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : TransactionRunTime repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 4/12/2018 20:18
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.TransactionRuntime;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class TransactionRuntimeRepository : BaseRepository<TransactionRuntime>, ITransactionRuntimeRepository<TransactionRuntime, long>
    {
        #region Constructor
        public TransactionRuntimeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Get
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionRuntime Get(long id)
        {
            using (var connection = Connection)
            {
                var result = connection.Query<TransactionRuntime, TransactionLog, TransactionRuntime>
                    (TransactionRuntimeQuery.GetById,
                    (transactionRunTime, transactionLog) =>
                    {
                        transactionRunTime.TransactionLog = transactionLog;

                        return transactionRunTime;
                    }, new { @id = id }, splitOn: "Id").FirstOrDefault();
                return result;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(TransactionRuntime entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                SetCreatedLog(entity);
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar<long>(TransactionRuntimeQuery.Insert, entity, transaction);
                });

                entity.Id = Convert.ToInt64(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New Transaction Runtime has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create Transaction Runtime " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(TransactionRuntime entity)
        {
            var responseMessage = new ResponseMessage();
            try
            {
                var transactionRuntime = Get(entity.Id);

                if (transactionRuntime == null)
                    responseMessage = new ResponseMessage
                    {
                        Success = true,
                        Status = ResponseStatus.Warning,
                        Message = "Transaction Runtime does not exist.",
                        Data = entity
                    };

                transactionRuntime.ExecutionTime = entity.ExecutionTime;
                transactionRuntime.FinishedTime = entity.FinishedTime;
                transactionRuntime.MethodName = entity.MethodName;
                transactionRuntime.StartedTime = entity.StartedTime;
                transactionRuntime.TransactionLogId = entity.TransactionLogId;

                SetLastModifiedLog(transactionRuntime);

                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(TransactionRuntimeQuery.Update, transactionRuntime, transaction);
                });

                responseMessage = new ResponseMessage
                                   {
                                       Success = true,
                                       Status = ResponseStatus.Success,
                                       Message = "Transaction Runtime has been updated successfully.",
                                       Data = entity
                                   };

            }
            catch (Exception ex)
            {
                responseMessage = new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                };
            }

            return responseMessage;
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(long id)
        {
            var responseMessage = new ResponseMessage();

            try
            {
                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(TransactionRuntimeQuery.Delete, new { @id = id });
                });


                responseMessage = new ResponseMessage
                    {
                        Success = true,
                        Status = ResponseStatus.Success,
                        Message = "Transaction Runtime has been deleted successfully."
                    };

            }
            catch (Exception ex)
            {
                responseMessage = new ResponseMessage()
                {
                    Success = false,
                    Status = ResponseStatus.Error,
                    Message = ex.Message
                };
            }

            return responseMessage;
        }
        #endregion

        #region GetAll
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public List<TransactionRuntime> GetAll()
        {
            using (var connection = Connection)
            {
                var result = connection.Query<TransactionRuntime, TransactionLog, TransactionRuntime>
                            (TransactionRuntimeQuery.GetAll,
                            (transactionRunTime, transactionLog) =>
                            {
                                transactionRunTime.TransactionLog = transactionLog;

                                return transactionRunTime;
                            }, splitOn: "Id").ToList();
                return result;
            }
        }
        #endregion

        #region Search
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<TransactionRuntime> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;

                GetPostModelData(model, "StartedTime DESC", out keyword, out orderBy);

                List<TransactionRuntime> result = Search<TransactionRuntime>((connection, query, sqlParams) =>
                {
                    return connection.Query<TransactionRuntime>(query, sqlParams).ToList();
                }, Connection, TransactionRuntimeQuery.Search
                , "StartedTime DESC", new { MethodName = keyword }, orderBy, out filteredResultsCount, model.Start, model.Length);

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<TransactionRuntime>();
            }
        }
        #endregion
    }
}
