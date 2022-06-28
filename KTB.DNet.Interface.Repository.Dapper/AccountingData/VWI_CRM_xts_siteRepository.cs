#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_site repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_site;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_siteRepository : BaseDNetRepository<VWI_CRM_xts_site>, IVWI_CRM_xts_siteRepository<VWI_CRM_xts_site, int>
    {
        #region Constructor
        public VWI_CRM_xts_siteRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_site
        /// <summary>
        /// Create VWI_CRM_xts_site
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_site entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_site
        /// <summary>
        /// Update VWI_CRM_xts_site
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_site entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_site
        /// <summary>
        /// Delete VWI_CRM_xts_site
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_site By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_site Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_site>(
                        VWI_CRM_xts_siteQuery.GetVWI_CRM_xts_siteById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_site
        /// <summary>
        /// Get All VWI_CRM_xts_site
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_site> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_site
        public List<VWI_CRM_xts_site> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_site>();
        }
        #endregion

		#region Search VWI_CRM_xts_site        
        public new List<VWI_CRM_xts_site> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_site.rowstatus", "isnull(vwi_crm_xts_site.rowstatus, 0)");

                List<VWI_CRM_xts_site> result = SearchData<VWI_CRM_xts_site>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_site>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_siteQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_site.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_siteQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_site>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_site vWI_CRM_xts_site)
        {
            //vWI_CRM_xts_site.CreatedBy = UserLogin;
            //vWI_CRM_xts_site.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_site);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_site vWI_CRM_xts_site)
        {
            //vWI_CRM_xts_site.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_site.LastUpdateTime = DateTime.Now;
        }
    }
}