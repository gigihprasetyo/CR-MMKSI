using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IAssistPartStockRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        Task<bool> BulkInsertAsync(List<TEntity> data);
        bool BulkInsert(List<TEntity> data);
        Task<List<TEntity>> GetDuplicateDataAsync(string dealerCode, List<string> listOfBranchCode, List<string> listOfMonth, List<string> listOfYear, List<string> listOfPartNo);
        List<TEntity> GetDuplicateData(string dealerCode, List<string> listOfBranchCode, List<string> listOfMonth, List<string> listOfYear, List<string> listOfPartNo);
    }
}
