using System.Collections;
using System.Collections.Generic;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IVWI_CRM_SLS_DailyActivityMonitoring_SPKRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
		List<TEntity> Search(string strCriteria, string strInnerCriteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
    }
}