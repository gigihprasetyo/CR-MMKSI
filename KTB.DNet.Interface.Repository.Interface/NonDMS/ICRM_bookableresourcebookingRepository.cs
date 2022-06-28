﻿#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : ICRM_bookableresourcebookingRepository class
 GENERATED BY  : Admin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 06 Okt 2021 
 ===========================================================================
*/
#endregion

using System.Collections;
using System.Collections.Generic;

namespace KTB.DNet.Interface.Repository.Interface
{
    public interface ICRM_bookableresourcebookingRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        List<TEntity> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount);
        bool BulkInsert(List<TEntity> data);
        void SetCreatedLog(TEntity model);
        void SetLastModifiedLog(TEntity model);
    }
}
