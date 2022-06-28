using System.Collections;
using System.Collections.Generic;
using System;
namespace KTB.DNet.Interface.Repository.Interface
{
    public interface IVWI_AX_SLS_FlowReportVehicleRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount, DateTime dateFrom, DateTime dateTo);
    }
}