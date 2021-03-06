#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_apvdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 02 Sep 2020 10:42:13
 ===========================================================================
*/
#endregion

using System.Collections;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ICRM_xts_apvdetailRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
        bool BulkInsert(List<TEntity> data);
        void SetCreatedLog(TEntity model);
        void SetLastModifiedLog(TEntity model);
    }
}
