#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_serviceproportionalinvoice class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 09:41:06
 ===========================================================================
*/
#endregion

using System.Collections;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ICRM_xts_serviceproportionalinvoiceRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
        bool BulkInsert(List<TEntity> data);
        void SetCreatedLog(TEntity model);
        void SetLastModifiedLog(TEntity model);
    }
}
