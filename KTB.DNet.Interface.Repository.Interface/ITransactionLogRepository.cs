using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ITransactionLogRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<TransactionLog> Search(TransactionDataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, bool failedTransactionOnly = false, bool includeAPIRead = true);
        List<TransactionLog> GetTopRankedApi(int take);
        List<TransactionLog> SearchTopRankedApi(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount);
        List<TransactionLog> GetLatestTransaction(string dealerCode, int take);
        List<TransactionLog> GetLatestErrorTransaction(string dealerCode, int take);
        List<TransactionLog> GetResendTransaction(int Id);
        JToken GetFailedTransactionSummaryPerDealer(DateTime transactionDate, string dealerCode = null);
        JToken GetTransactionSummary(DateTime transactionDate, string dealerCode = null);
        JToken GetTransactionSummaryPerDealer(DateTime transactionDate);
        ResponseMessage Delete(DateTime from, DateTime to, string dealerCode = null);
    }
}
