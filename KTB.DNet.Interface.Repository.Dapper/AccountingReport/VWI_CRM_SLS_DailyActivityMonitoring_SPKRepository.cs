#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_DailyActivityMonitoring_SPK repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:03
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SLS_DailyActivityMonitoring_SPK;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SLS_DailyActivityMonitoring_SPKRepository : BaseDNetRepository<VWI_CRM_SLS_DailyActivityMonitoring_SPK>, IVWI_CRM_SLS_DailyActivityMonitoring_SPKRepository<VWI_CRM_SLS_DailyActivityMonitoring_SPK, int>
    {
        #region Constructor
        public VWI_CRM_SLS_DailyActivityMonitoring_SPKRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SLS_DailyActivityMonitoring_SPK
        /// <summary>
        /// Create VWI_CRM_SLS_DailyActivityMonitoring_SPK
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SLS_DailyActivityMonitoring_SPK entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SLS_DailyActivityMonitoring_SPK
        /// <summary>
        /// Update VWI_CRM_SLS_DailyActivityMonitoring_SPK
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SLS_DailyActivityMonitoring_SPK entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SLS_DailyActivityMonitoring_SPK
        /// <summary>
        /// Delete VWI_CRM_SLS_DailyActivityMonitoring_SPK
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SLS_DailyActivityMonitoring_SPK By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SLS_DailyActivityMonitoring_SPK Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SLS_DailyActivityMonitoring_SPK>(
                        VWI_CRM_SLS_DailyActivityMonitoring_SPKQuery.GetVWI_CRM_SLS_DailyActivityMonitoring_SPKById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SLS_DailyActivityMonitoring_SPK
        /// <summary>
        /// Get All VWI_CRM_SLS_DailyActivityMonitoring_SPK
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SLS_DailyActivityMonitoring_SPK> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SLS_DailyActivityMonitoring_SPK
        public List<VWI_CRM_SLS_DailyActivityMonitoring_SPK> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SLS_DailyActivityMonitoring_SPK>();
        }
        #endregion

		#region Search VWI_CRM_SLS_DailyActivityMonitoring_SPK        
        public new List<VWI_CRM_SLS_DailyActivityMonitoring_SPK> Search(string strCriteria, string strInnerCriteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SLS_DailyActivityMonitoring_SPK> result = Search<VWI_CRM_SLS_DailyActivityMonitoring_SPK>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SLS_DailyActivityMonitoring_SPK>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SLS_DailyActivityMonitoring_SPKQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SLS_DailyActivityMonitoring_SPK.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SLS_DailyActivityMonitoring_SPKQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SLS_DailyActivityMonitoring_SPK>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SLS_DailyActivityMonitoring_SPK vWI_CRM_SLS_DailyActivityMonitoring_SPK)
        {
            //vWI_CRM_SLS_DailyActivityMonitoring_SPK.CreatedBy = UserLogin;
            //vWI_CRM_SLS_DailyActivityMonitoring_SPK.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SLS_DailyActivityMonitoring_SPK);
        }

        protected void SetLastModifiedLog(VWI_CRM_SLS_DailyActivityMonitoring_SPK vWI_CRM_SLS_DailyActivityMonitoring_SPK)
        {
            //vWI_CRM_SLS_DailyActivityMonitoring_SPK.LastUpdateBy = UserLogin;
            //vWI_CRM_SLS_DailyActivityMonitoring_SPK.LastUpdateTime = DateTime.Now;
        }
    }
}