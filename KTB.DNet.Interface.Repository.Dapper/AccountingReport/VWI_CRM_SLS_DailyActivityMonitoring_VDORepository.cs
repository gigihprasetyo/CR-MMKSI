#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_DailyActivityMonitoring_VDO repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SLS_DailyActivityMonitoring_VDO;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SLS_DailyActivityMonitoring_VDORepository : BaseDNetRepository<VWI_CRM_SLS_DailyActivityMonitoring_VDO>, IVWI_CRM_SLS_DailyActivityMonitoring_VDORepository<VWI_CRM_SLS_DailyActivityMonitoring_VDO, int>
    {
        #region Constructor
        public VWI_CRM_SLS_DailyActivityMonitoring_VDORepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SLS_DailyActivityMonitoring_VDO
        /// <summary>
        /// Create VWI_CRM_SLS_DailyActivityMonitoring_VDO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SLS_DailyActivityMonitoring_VDO entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SLS_DailyActivityMonitoring_VDO
        /// <summary>
        /// Update VWI_CRM_SLS_DailyActivityMonitoring_VDO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SLS_DailyActivityMonitoring_VDO entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SLS_DailyActivityMonitoring_VDO
        /// <summary>
        /// Delete VWI_CRM_SLS_DailyActivityMonitoring_VDO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SLS_DailyActivityMonitoring_VDO By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SLS_DailyActivityMonitoring_VDO Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SLS_DailyActivityMonitoring_VDO>(
                        VWI_CRM_SLS_DailyActivityMonitoring_VDOQuery.GetVWI_CRM_SLS_DailyActivityMonitoring_VDOById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SLS_DailyActivityMonitoring_VDO
        /// <summary>
        /// Get All VWI_CRM_SLS_DailyActivityMonitoring_VDO
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SLS_DailyActivityMonitoring_VDO> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SLS_DailyActivityMonitoring_VDO
        public List<VWI_CRM_SLS_DailyActivityMonitoring_VDO> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SLS_DailyActivityMonitoring_VDO>();
        }
        #endregion

		#region Search VWI_CRM_SLS_DailyActivityMonitoring_VDO        
        public new List<VWI_CRM_SLS_DailyActivityMonitoring_VDO> Search(string strCriteria, string strInnerCriteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SLS_DailyActivityMonitoring_VDO> result = Search<VWI_CRM_SLS_DailyActivityMonitoring_VDO>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SLS_DailyActivityMonitoring_VDO>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SLS_DailyActivityMonitoring_VDOQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SLS_DailyActivityMonitoring_VDO.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SLS_DailyActivityMonitoring_VDOQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SLS_DailyActivityMonitoring_VDO>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SLS_DailyActivityMonitoring_VDO vWI_CRM_SLS_DailyActivityMonitoring_VDO)
        {
            //vWI_CRM_SLS_DailyActivityMonitoring_VDO.CreatedBy = UserLogin;
            //vWI_CRM_SLS_DailyActivityMonitoring_VDO.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SLS_DailyActivityMonitoring_VDO);
        }

        protected void SetLastModifiedLog(VWI_CRM_SLS_DailyActivityMonitoring_VDO vWI_CRM_SLS_DailyActivityMonitoring_VDO)
        {
            //vWI_CRM_SLS_DailyActivityMonitoring_VDO.LastUpdateBy = UserLogin;
            //vWI_CRM_SLS_DailyActivityMonitoring_VDO.LastUpdateTime = DateTime.Now;
        }
    }
}