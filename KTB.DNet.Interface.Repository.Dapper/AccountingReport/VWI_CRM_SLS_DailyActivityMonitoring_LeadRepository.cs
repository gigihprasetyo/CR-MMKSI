#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_DailyActivityMonitoring_Lead repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SLS_DailyActivityMonitoring_Lead;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SLS_DailyActivityMonitoring_LeadRepository : BaseDNetRepository<VWI_CRM_SLS_DailyActivityMonitoring_Lead>, IVWI_CRM_SLS_DailyActivityMonitoring_LeadRepository<VWI_CRM_SLS_DailyActivityMonitoring_Lead, int>
    {
        #region Constructor
        public VWI_CRM_SLS_DailyActivityMonitoring_LeadRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SLS_DailyActivityMonitoring_Lead
        /// <summary>
        /// Create VWI_CRM_SLS_DailyActivityMonitoring_Lead
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SLS_DailyActivityMonitoring_Lead entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SLS_DailyActivityMonitoring_Lead
        /// <summary>
        /// Update VWI_CRM_SLS_DailyActivityMonitoring_Lead
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SLS_DailyActivityMonitoring_Lead entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SLS_DailyActivityMonitoring_Lead
        /// <summary>
        /// Delete VWI_CRM_SLS_DailyActivityMonitoring_Lead
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SLS_DailyActivityMonitoring_Lead By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SLS_DailyActivityMonitoring_Lead Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SLS_DailyActivityMonitoring_Lead>(
                        VWI_CRM_SLS_DailyActivityMonitoring_LeadQuery.GetVWI_CRM_SLS_DailyActivityMonitoring_LeadById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SLS_DailyActivityMonitoring_Lead
        /// <summary>
        /// Get All VWI_CRM_SLS_DailyActivityMonitoring_Lead
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SLS_DailyActivityMonitoring_Lead> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SLS_DailyActivityMonitoring_Lead
        public List<VWI_CRM_SLS_DailyActivityMonitoring_Lead> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SLS_DailyActivityMonitoring_Lead>();
        }
        #endregion

		#region Search VWI_CRM_SLS_DailyActivityMonitoring_Lead        
        public new List<VWI_CRM_SLS_DailyActivityMonitoring_Lead> Search(string strCriteria, string strInnerCriteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SLS_DailyActivityMonitoring_Lead> result = Search<VWI_CRM_SLS_DailyActivityMonitoring_Lead>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SLS_DailyActivityMonitoring_Lead>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SLS_DailyActivityMonitoring_LeadQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SLS_DailyActivityMonitoring_Lead.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SLS_DailyActivityMonitoring_LeadQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SLS_DailyActivityMonitoring_Lead>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SLS_DailyActivityMonitoring_Lead vWI_CRM_SLS_DailyActivityMonitoring_Lead)
        {
            //vWI_CRM_SLS_DailyActivityMonitoring_Lead.CreatedBy = UserLogin;
            //vWI_CRM_SLS_DailyActivityMonitoring_Lead.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SLS_DailyActivityMonitoring_Lead);
        }

        protected void SetLastModifiedLog(VWI_CRM_SLS_DailyActivityMonitoring_Lead vWI_CRM_SLS_DailyActivityMonitoring_Lead)
        {
            //vWI_CRM_SLS_DailyActivityMonitoring_Lead.LastUpdateBy = UserLogin;
            //vWI_CRM_SLS_DailyActivityMonitoring_Lead.LastUpdateTime = DateTime.Now;
        }
    }
}