#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_reasonRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 10:06:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_reason;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_reasonRepository : BaseDNetRepository<VWI_CRM_xts_reason>, IVWI_CRM_xts_reasonRepository<VWI_CRM_xts_reason, int>
    {
        #region Constructor
        public VWI_CRM_xts_reasonRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_reason
        /// <summary>
        /// Create VWI_CRM_xts_reason
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_reason entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_reason
        /// <summary>
        /// Update VWI_CRM_xts_reason
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_reason entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_reason
        /// <summary>
        /// Delete VWI_CRM_xts_reason
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_reason By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_reason Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_reason>(
                        VWI_CRM_xts_reasonQuery.GetVWI_CRM_xts_reasonById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_reason
        /// <summary>
        /// Get All VWI_CRM_xts_reason
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_reason> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_reason
        public List<VWI_CRM_xts_reason> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_reason>();
        }
        #endregion

        #region Search VWI_CRM_xts_reason        
        public new List<VWI_CRM_xts_reason> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_reason.rowstatus", "isnull(vwi_crm_xts_reason.rowstatus, 0)");

                List<VWI_CRM_xts_reason> result = SearchData<VWI_CRM_xts_reason>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_reason>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_reasonQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_reason.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_reasonQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_reason>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_reason vWI_CRM_xts_reason)
        {
            //vWI_CRM_xts_reason.CreatedBy = UserLogin;
            //vWI_CRM_xts_reason.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_reason);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_reason vWI_CRM_xts_reason)
        {
            //vWI_CRM_xts_reason.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_reason.LastUpdateTime = DateTime.Now;
        }
    }
}
