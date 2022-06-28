using System.Collections;
using System.Collections.Generic;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IVWI_CRM_xts_inventtransRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
		List<TEntity> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
    }
}