using KTB.DNet.Interface.Framework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IElmahErrorRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount, string appName);
        Task<TEntity> GetAsync(Guid id);
        Task<string> GetErrorDetailAsync(Guid id, string application);
        ResponseMessage Delete(DateTime from, DateTime to);
        Task<JToken> GetErrorLogSummaryPerApplication();
        Task<JToken> GetErrorLogMainInfo();
        Task<JToken> GetListOfApplication();
        Task<List<TEntity>> GetLatestErrorLog(int take, string applicationName, int severity);
    }
}
