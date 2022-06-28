using System.Collections.Generic;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ISparePartMasterRepository<TEntity, TKey> : IBaseDNetRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> GetByPartNumbersAsync(List<string> listOfPartNumber);
        List<TEntity> GetByPartNumbers(List<string> listOfPartNumber);
    }
}
