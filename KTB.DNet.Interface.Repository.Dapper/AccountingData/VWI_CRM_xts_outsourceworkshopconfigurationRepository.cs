#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_outsourceworkshopconfiguration repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/28/2020 08:43:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_outsourceworkshopconfiguration;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_outsourceworkshopconfigurationRepository : BaseDNetRepository<VWI_CRM_xts_outsourceworkshopconfiguration>, IVWI_CRM_xts_outsourceworkshopconfigurationRepository<VWI_CRM_xts_outsourceworkshopconfiguration, int>
    {
        #region Constructor
        public VWI_CRM_xts_outsourceworkshopconfigurationRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_outsourceworkshopconfiguration
        /// <summary>
        /// Create VWI_CRM_xts_outsourceworkshopconfiguration
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_outsourceworkshopconfiguration entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_outsourceworkshopconfiguration
        /// <summary>
        /// Update VWI_CRM_xts_outsourceworkshopconfiguration
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_outsourceworkshopconfiguration entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_outsourceworkshopconfiguration
        /// <summary>
        /// Delete VWI_CRM_xts_outsourceworkshopconfiguration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_outsourceworkshopconfiguration By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_outsourceworkshopconfiguration Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_outsourceworkshopconfiguration>(
                        VWI_CRM_xts_outsourceworkshopconfigurationQuery.GetVWI_CRM_xts_outsourceworkshopconfigurationById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_outsourceworkshopconfiguration
        /// <summary>
        /// Get All VWI_CRM_xts_outsourceworkshopconfiguration
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_outsourceworkshopconfiguration> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_outsourceworkshopconfiguration
        public List<VWI_CRM_xts_outsourceworkshopconfiguration> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_outsourceworkshopconfiguration>();
        }
        #endregion

        #region Search VWI_CRM_xts_outsourceworkshopconfiguration        
        public new List<VWI_CRM_xts_outsourceworkshopconfiguration> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_outsourceworkshopconfiguration.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_outsourceworkshopconfiguration> result = SearchData<VWI_CRM_xts_outsourceworkshopconfiguration>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_outsourceworkshopconfiguration>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_outsourceworkshopconfigurationQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_outsourceworkshopconfiguration.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_outsourceworkshopconfigurationQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_outsourceworkshopconfiguration>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_outsourceworkshopconfiguration vWI_CRM_xts_outsourceworkshopconfiguration)
        {
            //vWI_CRM_xts_outsourceworkshopconfiguration.CreatedBy = UserLogin;
            //vWI_CRM_xts_outsourceworkshopconfiguration.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_outsourceworkshopconfiguration);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_outsourceworkshopconfiguration vWI_CRM_xts_outsourceworkshopconfiguration)
        {
            //vWI_CRM_xts_outsourceworkshopconfiguration.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_outsourceworkshopconfiguration.LastUpdateTime = DateTime.Now;
        }
    }
}