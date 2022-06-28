using KTB.DNet.Interface.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ISAPCustomerLeadRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
        bool BulkInsert(List<TEntity> data);
        void SetCreatedLog(TEntity model);
        //void SetLastModifiedLog(TEntity model);
        void SetLastModifiedLog(TEntity model);
        ResponseMessage UpdateQueue(TEntity model);

    }
}
