#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_perlengkapanstandard repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_perlengkapanstandard;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_perlengkapanstandardRepository : BaseDNetRepository<VWI_CRM_ktb_perlengkapanstandard>, IVWI_CRM_ktb_perlengkapanstandardRepository<VWI_CRM_ktb_perlengkapanstandard, int>
    {
        #region Constructor
        public VWI_CRM_ktb_perlengkapanstandardRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_perlengkapanstandard
        /// <summary>
        /// Create VWI_CRM_ktb_perlengkapanstandard
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_perlengkapanstandard entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_perlengkapanstandard
        /// <summary>
        /// Update VWI_CRM_ktb_perlengkapanstandard
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_perlengkapanstandard entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_perlengkapanstandard
        /// <summary>
        /// Delete VWI_CRM_ktb_perlengkapanstandard
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_ktb_perlengkapanstandard By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_perlengkapanstandard Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_perlengkapanstandard>(
                        VWI_CRM_ktb_perlengkapanstandardQuery.GetVWI_CRM_ktb_perlengkapanstandardById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_perlengkapanstandard
        /// <summary>
        /// Get All VWI_CRM_ktb_perlengkapanstandard
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_perlengkapanstandard> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_perlengkapanstandard
        public List<VWI_CRM_ktb_perlengkapanstandard> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_perlengkapanstandard>();
        }
        #endregion

		#region Search VWI_CRM_ktb_perlengkapanstandard        
        public new List<VWI_CRM_ktb_perlengkapanstandard> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_perlengkapanstandard.rowstatus", "isnull(vwi_crm_ktb_perlengkapanstandard.rowstatus, 0)");

                List<VWI_CRM_ktb_perlengkapanstandard> result = SearchData<VWI_CRM_ktb_perlengkapanstandard>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_perlengkapanstandard>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_perlengkapanstandardQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_perlengkapanstandard.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_perlengkapanstandardQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_perlengkapanstandard>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_perlengkapanstandard vWI_CRM_ktb_perlengkapanstandard)
        {
            //vWI_CRM_ktb_perlengkapanstandard.CreatedBy = UserLogin;
            //vWI_CRM_ktb_perlengkapanstandard.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_perlengkapanstandard);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_perlengkapanstandard vWI_CRM_ktb_perlengkapanstandard)
        {
            //vWI_CRM_ktb_perlengkapanstandard.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_perlengkapanstandard.LastUpdateTime = DateTime.Now;
        }
    }
}