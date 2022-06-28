#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : TransactionLog repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 6/12/2018 15:18
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.SqlQuery.TransactionLog;
using KTB.DNet.Interface.Repository.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper
{
    public class TransactionLogRepository : BaseRepository<TransactionLog>, ITransactionLogRepository<TransactionLog, long>
    {
        #region Constructor
        public TransactionLogRepository(string connectionString)
            : base(connectionString) { }
        #endregion

        #region Search
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <param name="failedTransactionOnly"></param>
        /// <param name="includeAPIRead"></param>
        /// <returns></returns>
        public List<TransactionLog> Search(TransactionDataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, bool failedTransactionOnly = false, bool includeAPIRead = true)
        {
            try
            {
                int filterId = -1;
                string filterInput = string.Empty;
                string filterOutput = string.Empty;
                string filterSenderIP = string.Empty;
                int filterStatus = failedTransactionOnly ? 0 : -1;
                string filterEndpoint = string.Empty;
                string filterCreatedBy = string.Empty;
                string filterUserName = string.Empty;

                DateTime StartDate = Constants.DATETIME_DEFAULT_VALUE;
                DateTime EndDate = Constants.DATETIME_DEFAULT_VALUE;

                if (model.searchParams != null)
                {
                    List<string> successStrings = new List<string> { "true", "success", "1" };
                    List<string> failedStrings = new List<string> { "false", "failed", "fail", "0" };

                    foreach (KeyValuePair<object, object> param in model.searchParams)
                    {
                        string paramValue = param.Value.ToString();
                        string paramKey = param.Key.ToString();

                        if (!string.IsNullOrEmpty(paramValue))
                        {
                            if (paramKey.Count() > 6 && paramKey.Contains("search"))
                            {
                                string key = paramKey.Substring(6);
                                switch (key)
                                {
                                    case ("Id"):
                                        if (!int.TryParse(paramValue, out filterId))
                                        {
                                            filterId = -1;
                                        }
                                        break;
                                    case ("StatusStr"):
                                        string val = paramValue.ToLower();
                                        filterStatus = successStrings.Contains(val) ? 1 :
                                                        (failedStrings.Contains(val) ? 0 : -1);
                                        break;
                                    case ("Endpoint"):
                                        filterEndpoint = paramValue;
                                        break;
                                    case ("CreatedBy"):
                                        filterCreatedBy = paramValue;
                                        break;
                                    case ("ErrorMessage"):
                                        filterOutput = paramValue;
                                        break;
                                    default:
                                        // todo : sb.Where(param.Key.ToString().ToString().Substring(6) + " LIKE '%" + param.Value.ToString() + "%'");
                                        break;
                                }
                            }
                            else if (paramKey.ToLower() == "begindate")
                            {
                                StartDate = ((DateTime)param.Value).Date;
                            }
                            else if ((param.Key.ToString().ToLower() == "enddate"))
                            {
                                EndDate = ((DateTime)param.Value).Date;
                            }
                            else if (paramKey.ToLower() == "input")
                            {
                                filterInput = paramValue;
                            }
                            else if (paramKey.ToLower() == "output")
                            {
                                filterOutput = paramValue;
                            }
                            else if (paramKey.ToLower() == "senderip")
                            {
                                filterSenderIP = paramValue;
                            }
                            else if (paramKey.ToLower() == "username")
                            {
                                filterUserName = paramValue;
                            }
                        }
                    }
                }


                var parameters = new
                {
                    Id = filterId,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    Status = filterStatus,
                    IncludeAPIRead = includeAPIRead ? 1 : 0,
                    UserName = filterUserName,
                    Endpoint = filterEndpoint,
                    CreatedBy = filterCreatedBy,
                    Input = filterInput,
                    Output = filterOutput,
                    SenderIp = filterSenderIP,
                    DealerCode = !string.IsNullOrEmpty(model.dealerCode) ? model.dealerCode : string.Empty,
                    AppId = model.appId != Guid.Empty ? model.appId : Guid.Empty,
                    ClientId = model.clientId != Guid.Empty ? model.clientId : Guid.Empty
                };


                string keyword = string.Empty;
                List<string> orderBy = null;
                filteredResultsCount = 0;
                List<TransactionLog> listOfResendTransaction = new List<TransactionLog>();

                GetPostModelData(model, "Id DESC", out keyword, out orderBy);
                List<TransactionLog> result = Search<TransactionLog>((connection, query, sqlParams) =>
                {
                    List<TransactionLog> listCheckParent = connection.Query<TransactionLog>(TransactionLogQuery.CheckTransactionLogParentID, null, null, true, Timeout).AsList();

                    List<TransactionLog> listOfTransaction = connection.Query<TransactionLog>(query, sqlParams,null,true,Timeout).AsList();
                    if (listOfTransaction != null && listOfTransaction.Count > 0 && listCheckParent.Count > 0)
                    {
                        List<long> listOfParentId = listOfTransaction.Select(e => e.Id).ToList();
                        listOfResendTransaction = connection.Query<TransactionLog>(TransactionLogQuery.GetResendTransactionByListOfParentId, new { ListOfParentId = listOfParentId },null,true,Timeout).AsList();

                    }

                    return listOfTransaction;
                },
                Connection, // connection 
                TransactionLogQuery.SearchTransactionLog, // query
                "Id",                   // default identifier/sorting 
                parameters, // sqlParams
                orderBy,                // sorting by (optional) 
                out filteredResultsCount, // total result 
                model.Start,            // start index 
                model.Length,           // length of data will be retrieved
                TransactionLogQuery.TotalSearchTransactionLog
                );

                // update the resloved status
                foreach (var transction in result)
                {
                    if (!transction.Status)
                    {
                        transction.IsResolved = listOfResendTransaction.Any(resendTransaction => resendTransaction.ParentId.Value.Equals(transction.Id) && resendTransaction.Status);
                    }
                    else
                    {
                        transction.IsResolved = true;
                    }
                }

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                throw ex;
            }
        }
        #endregion

        #region GetTopRankedApi
        /// <summary>
        /// GetTopRankedApi
        /// </summary>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<TransactionLog> GetTopRankedApi(int take)
        {
            try
            {
                using (var connection = Connection)
                {
                    return connection.Query<TransactionLog>(TransactionLogQuery.GetTopRankedApi, new { Take = take },null,true,Timeout).AsList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SearchTopRankedApi
        /// <summary>
        /// SearchTopRankedApi
        /// </summary>
        /// <param name="model"></param>
        /// <param name="filteredResultsCount"></param>
        /// <param name="totalResultsCount"></param>
        /// <returns></returns>
        public List<TransactionLog> SearchTopRankedApi(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string keyword = string.Empty;
                List<string> orderBy = new List<string>() { "Total DESC" };
                filteredResultsCount = 0;

                List<TransactionLog> result = Search<TransactionLog>((connection, query, sqlParams) =>
                {
                    return connection.Query<TransactionLog>(query, sqlParams,null,true,Timeout).ToList();
                },
                Connection, // connection 
                TransactionLogQuery.SearchTopRankedApi, // query
                "Total",                   // default identifier/sorting 
                null, // sqlParams
                orderBy,                // sorting by (optional) 
                out filteredResultsCount, // total result 
                model.Start,            // start index 
                model.Length            // length of data will be retrieved
                );

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetLatestTransaction
        /// <summary>
        /// GetLatestTransaction
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<TransactionLog> GetLatestTransaction(string dealerCode, int take)
        {
            try
            {
                dealerCode = string.IsNullOrEmpty(dealerCode) ? string.Empty : dealerCode;
                using (var connection = Connection)
                {
                    List<TransactionLog> listOfTransaction = connection.Query<TransactionLog>(TransactionLogQuery.GetLatestTransaction, new { Take = take, DealerCode = dealerCode },null,true,Timeout).AsList();
                    List<TransactionLog> listOfResendTransaction = new List<TransactionLog>();
                    if (listOfTransaction != null && listOfTransaction.Count > 0)
                    {
                        List<long> listOfParentId = listOfTransaction.Select(e => e.Id).ToList();
                        listOfResendTransaction = connection.Query<TransactionLog>(TransactionLogQuery.GetResendTransactionByListOfParentId, new { ListOfParentId = listOfParentId },null,true,Timeout).AsList();

                    }

                    // update the resloved status
                    foreach (var transaction in listOfTransaction)
                    {
                        if (!transaction.Status)
                        {
                            transaction.IsResolved = listOfResendTransaction.Any(resendTransaction => resendTransaction.ParentId.Value.Equals(transaction.Id) && resendTransaction.Status);
                        }
                        else
                        {
                            transaction.IsResolved = true;
                        }
                    }
                    return listOfTransaction;
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetLatestErrorTransaction
        /// <summary>
        /// GetLatestErrorTransaction
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<TransactionLog> GetLatestErrorTransaction(string dealerCode, int take)
        {
            try
            {
                List<TransactionLog> listOfErrorTransaction = new List<TransactionLog>();
                List<TransactionLog> listOfResendTransaction = new List<TransactionLog>();

                using (var connection = Connection)
                {
                    
                    listOfErrorTransaction = connection.Query<TransactionLog>(TransactionLogQuery.GetLatestErrorTransaction, new { Take = take, DealerCode = dealerCode },null,true,Timeout).AsList();

                    if (listOfErrorTransaction != null && listOfErrorTransaction.Count > 0)
                    {
                        List<long> listOfParentId = listOfErrorTransaction.Select(t => t.Id).ToList();
                        listOfResendTransaction = connection.Query<TransactionLog>(TransactionLogQuery.GetResendTransactionByListOfParentId, new { ListOfParentId = listOfParentId },null,true,Timeout).AsList();
                    }
                }

                // update the resloved status
                foreach (var transction in listOfErrorTransaction)
                {
                    transction.IsResolved = listOfResendTransaction.Any(resendTransaction => resendTransaction.ParentId.Value.Equals(transction.Id) && resendTransaction.Status);
                }

                return listOfErrorTransaction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetResendTransaction
        /// <summary>
        /// GetResendTransaction
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<TransactionLog> GetResendTransaction(int Id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<TransactionLog>(TransactionLogQuery.GetResendTransaction, new { ParentId = Id },null,true,Timeout).AsList();
                }
            }
            catch (Exception ex)
            {
                return new List<TransactionLog>();
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionLog Get(long id)
        {
            try
            {
                if (id != 0)
                {
                    using (var cn = Connection)
                    {
                        return cn.Query<TransactionLog>(TransactionLogQuery.GetTransactionById, new { Id = id },null,true,Timeout).FirstOrDefault();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(TransactionLog entity)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                SetCreatedLog(entity);
                var result = ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.ExecuteScalar<long>(TransactionLogQuery.InsertTransaction, entity, transaction);
                });

                entity.Id = Convert.ToInt64(result);

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = "New TransactionLog has been successfully created";
                responseMessage.Data = entity;
            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to create TransactionLog. " + GetInnerException(ex).Message;
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
        public ResponseMessage Update(TransactionLog entity)
        {
            try
            {
                TransactionLog transactionLog = Get(entity.Id);
                if (transactionLog == null)
                {
                    return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = "Transaction Log not found in database." };
                }

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

                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(TransactionLogQuery.UpdateTransactionLog, transactionLog, transaction);
                });

                return new ResponseMessage() { Success = true, Status = ResponseStatus.Success, Message = "Transaction Log has been successfully updated.", Data = transactionLog };
            }
            catch (Exception ex)
            {
                return new ResponseMessage() { Success = false, Status = ResponseStatus.Error, Message = GetInnerException(ex).Message };
            }
        }
        #endregion

        #region Get Failed Transaction Summary Per Dealer
        /// <summary>
        /// GetFailedTransactionSummaryPerDealer
        /// </summary>
        /// <param name="transactionDate"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        public JToken GetFailedTransactionSummaryPerDealer(DateTime transactionDate, string dealerCode = null)
        {
            transactionDate = transactionDate <= Constants.DATETIME_DEFAULT_VALUE ? DateTime.Now : transactionDate;

            try
            {
                List<TransactionLog> failedTransaction = null;

                using (var connection = Connection)
                {
                    failedTransaction = connection.Query<TransactionLog>(TransactionLogQuery.GetFailedTransactionSummaryPerDealer, new { DealerCode = dealerCode, Date = transactionDate.Date },null,true,Timeout).ToList();
                }

                JArray result = new JArray();

                List<string> colors = new List<string> { 
                    "#FFEBEE", "#FFCDD2", "#EF9A9A", "#E57373", "#EF5350", "#F44336", "#E53935", "#D32F2F", "#C62828", "#B71C1C", "#FF8A80", "#FF5252", "#FF1744", "#D50000", //red
                    "#FBE9E7" ,"#FFCCBC" ,"#FFAB91" ,"#FF8A65" ,"#FF7043" ,"#FF5722" ,"#F4511E" ,"#E64A19" ,"#D84315" ,"#BF360C" ,"#FF9E80" ,"#FF6E40" ,"#FF3D00" ,"#DD2C00", //deep orange
                    "#FCE4EC", "#F8BBD0", "#F48FB1", "#F06292", "#EC407A", "#E91E63", "#D81B60", "#C2185B", "#AD1457", "#880E4F", "#FF80AB", "#FF4081", "#F50057", "#C51162", // pink
                    "#FFFDE7", "#FFF9C4", "#FFF59D", "#FFF176", "#FFEE58", "#FFEB3B", "#FDD835", "#FBC02D", "#F9A825", "#F57F17", "#FFFF8D", "#FFFF00", "#FFEA00", "#FFD600" //amber
                };

                int colorLength = colors.Count();
                int colorIndex;
                Random random = new Random();

                foreach (var transaction in failedTransaction)
                {
                    string color = "red";

                    #region Get Random Color
                    if (colorLength > 0)
                    {
                        colorIndex = random.Next(colorLength);
                        color = colors[colorIndex];

                        colors.RemoveAt(colorIndex);
                        colorLength--;
                    }
                    else
                    {
                        Color pieColor = Color.FromArgb(random.Next(0, 255), random.Next(23, 167), random.Next(23, 167));
                        color = "#" + pieColor.R.ToString("X2") + pieColor.G.ToString("X2") + pieColor.B.ToString("X2");
                    }
                    #endregion

                    JObject failedTransactionPerDealer = new JObject();
                    failedTransactionPerDealer.Add("DealerCode", transaction.DealerCode);
                    failedTransactionPerDealer.Add("Total", transaction.Total);
                    failedTransactionPerDealer.Add("Color", color);

                    result.Add(failedTransactionPerDealer);
                }

                return result;
            }
            catch (Exception ex)
            {
                return new JArray();
            }

        }
        #endregion

        #region Get Transaction Summary Per Dealer
        /// <summary>
        /// GetTransactionSummaryPerDealer
        /// </summary>
        /// <param name="transactionDate"></param>
        /// <returns></returns>
        public JToken GetTransactionSummaryPerDealer(DateTime transactionDate)
        {
            transactionDate = transactionDate <= Constants.DATETIME_DEFAULT_VALUE ? DateTime.Now : transactionDate;

            try
            {
                JArray result = new JArray();
                List<TransactionLog> transactionSummary = null;

                using (var connection = Connection)
                {
                    transactionSummary = connection.Query<TransactionLog>(TransactionLogQuery.GetTransactionSummaryPerDealer, new { Date = transactionDate.Date },null,true,Timeout).ToList();
                }

                if (transactionSummary != null && transactionSummary.Count() > 0)
                {
                    List<string> dealerCodes = transactionSummary.GroupBy(t => t.DealerCode).Select(t => t.FirstOrDefault()).Select(t => t.DealerCode).ToList();

                    foreach (string dealerCode in dealerCodes)
                    {
                        JObject transactionSummaryPerDealer = new JObject();

                        TransactionLog successLog = transactionSummary.Where(t => t.DealerCode == dealerCode && t.Status == true).FirstOrDefault();
                        TransactionLog failedLog = transactionSummary.Where(t => t.DealerCode == dealerCode && t.Status == false).FirstOrDefault();

                        transactionSummaryPerDealer.Add("DealerCode", dealerCode);
                        transactionSummaryPerDealer.Add("Success", successLog == null ? 0 : successLog.Total);
                        transactionSummaryPerDealer.Add("Failed", failedLog == null ? 0 : failedLog.Total);

                        result.Add(transactionSummaryPerDealer);
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
        /// <summary>
        /// GetTransactionSummary
        /// </summary>
        /// <param name="transactionDate"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        public JToken GetTransactionSummary(DateTime transactionDate, string dealerCode = null)
        {
            transactionDate = transactionDate <= Constants.DATETIME_DEFAULT_VALUE ? DateTime.Now : transactionDate;

            int totalSuccessTransaction = 0;
            int totalFailedTransaction = 0;
            int totalResolvedTransaction = 0;

            try
            {
                JArray result = new JArray();
                using (var connection = Connection)
                {
                    object parameters = new { DealerCode = string.IsNullOrEmpty(dealerCode) ? string.Empty : dealerCode, Date = transactionDate.Date };

                    Dictionary<bool, int> transactionSummary = connection.Query(TransactionLogQuery.GetTransactionSummary, parameters,null,true,Timeout).ToDictionary(row => (bool)row.Status, row => (int)row.Total);

                    transactionSummary.TryGetValue(true, out totalSuccessTransaction);

                    transactionSummary.TryGetValue(false, out totalFailedTransaction);

                    totalResolvedTransaction = connection.ExecuteScalar<int>(TransactionLogQuery.GetResolvedTransactionSummary, parameters);
                }

                JObject successTransaction = new JObject();
                successTransaction.Add("TransactionType", "Success");
                successTransaction.Add("Total", totalSuccessTransaction);
                successTransaction.Add("Color", "#a6c733");
                result.Add(successTransaction);

                JObject failedTransaction = new JObject();
                failedTransaction.Add("TransactionType", "Failed");
                failedTransaction.Add("Total", totalFailedTransaction - totalResolvedTransaction);
                failedTransaction.Add("Color", "#fa5655");
                result.Add(failedTransaction);

                JObject resolvedTransaction = new JObject();
                resolvedTransaction.Add("TransactionType", "Resolved");
                resolvedTransaction.Add("Total", totalResolvedTransaction);
                resolvedTransaction.Add("Color", "#57bcda");
                result.Add(resolvedTransaction);

                return result;
            }
            catch (Exception ex)
            {
                return new JArray();
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete Transaction log with interval
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public ResponseMessage Delete(DateTime from, DateTime to, string dealerCode = null)
        {
            ResponseMessage responseMessage = new ResponseMessage() { Success = false };

            try
            {
                from = from.Date;
                to = to.Date.AddDays(1); // date < to
                dealerCode = string.IsNullOrEmpty(dealerCode) ? string.Empty : dealerCode;

                ExecuteTransaction(Connection, (connection, transaction) =>
                {
                    return connection.Execute(TransactionLogQuery.DeleteTransactionLogWithInterval, new
                    {
                        From = from,
                        To = to,
                        DealerCode = dealerCode

                    }, transaction);
                });

                responseMessage.Success = true;
                responseMessage.Status = ResponseStatus.Success;
                responseMessage.Message = string.Format("Transaction log has been successfully deleted");

            }
            catch (Exception ex)
            {
                responseMessage.Status = ResponseStatus.Error;
                responseMessage.Message = "Failed to delete transaction log " + GetInnerException(ex).Message;
            }

            return responseMessage;
        }
        #endregion

        #region not implemented
        public List<TransactionLog> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public List<TransactionLog> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(long id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
