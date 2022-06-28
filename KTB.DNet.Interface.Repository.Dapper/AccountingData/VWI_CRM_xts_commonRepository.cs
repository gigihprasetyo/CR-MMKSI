#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_common repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 03/02/2020 17:40:59
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_common;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_commonRepository : BaseDNetRepository<VWI_CRM_xts_common>, IVWI_CRM_xts_commonRepository<VWI_CRM_xts_common, int>
    {
        #region Constructor
        public VWI_CRM_xts_commonRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_common
        /// <summary>
        /// Create VWI_CRM_xts_common
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_common entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_common
        /// <summary>
        /// Update VWI_CRM_xts_common
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_common entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_common
        /// <summary>
        /// Delete VWI_CRM_xts_common
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_common By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_common Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_common>(
                        VWI_CRM_xts_commonQuery.GetVWI_CRM_xts_commonById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_common
        /// <summary>
        /// Get All VWI_CRM_xts_common
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_common> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_common
        public List<VWI_CRM_xts_common> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_common>();
        }
        #endregion

		#region Search VWI_CRM_xts_common        
        public new List<VWI_CRM_xts_common> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_common.rowstatus", "isnull(vwi_crm_xts_common.rowstatus, 0)");

                List<VWI_CRM_xts_common> result = SearchData<VWI_CRM_xts_common>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_common>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_commonQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_common.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_commonQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_common>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_common vWI_CRM_xts_common)
        {
            //vWI_CRM_xts_common.CreatedBy = UserLogin;
            //vWI_CRM_xts_common.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_common);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_common vWI_CRM_xts_common)
        {
            //vWI_CRM_xts_common.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_common.LastUpdateTime = DateTime.Now;
        }
    }
}