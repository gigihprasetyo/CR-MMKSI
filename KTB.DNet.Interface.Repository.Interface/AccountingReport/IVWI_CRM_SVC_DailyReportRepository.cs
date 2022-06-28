using System.Collections;
using System.Collections.Generic;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IVWI_CRM_SVC_DailyReportRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(string strSPParameter, out int filteredResultsCount, out int totalResultsCount);
    }
}