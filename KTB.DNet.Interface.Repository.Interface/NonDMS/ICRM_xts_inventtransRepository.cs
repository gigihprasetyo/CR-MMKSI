#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_inventtrans class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 10:18:19
 ===========================================================================
*/
#endregion

using System.Collections;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ICRM_xts_inventtransRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
        bool BulkInsert(List<TEntity> data);
        void SetCreatedLog(TEntity model);
        void SetLastModifiedLog(TEntity model);
    }
}
