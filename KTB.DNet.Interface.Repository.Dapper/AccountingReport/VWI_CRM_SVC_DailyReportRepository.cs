#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_DailyReport repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2020 10:45:39
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SVC_DailyReport;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SVC_DailyReportRepository : BaseDNetRepository<VWI_CRM_SVC_DailyReport>, IVWI_CRM_SVC_DailyReportRepository<VWI_CRM_SVC_DailyReport, int>
    {
        #region Constructor
        public VWI_CRM_SVC_DailyReportRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SVC_DailyReport
        /// <summary>
        /// Create VWI_CRM_SVC_DailyReport
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SVC_DailyReport entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SVC_DailyReport
        /// <summary>
        /// Update VWI_CRM_SVC_DailyReport
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SVC_DailyReport entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SVC_DailyReport
        /// <summary>
        /// Delete VWI_CRM_SVC_DailyReport
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SVC_DailyReport By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SVC_DailyReport Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SVC_DailyReport>(
                        VWI_CRM_SVC_DailyReportQuery.GetVWI_CRM_SVC_DailyReportById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SVC_DailyReport
        /// <summary>
        /// Get All VWI_CRM_SVC_DailyReport
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SVC_DailyReport> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SVC_DailyReport
        public List<VWI_CRM_SVC_DailyReport> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SVC_DailyReport>();
        }
        #endregion

		#region Search VWI_CRM_SVC_DailyReport        
        public new List<VWI_CRM_SVC_DailyReport> Search(string strSPParameter, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {                
                filteredResultsCount = 0;

                List<VWI_CRM_SVC_DailyReport> result = SearchFetchPagingSP<VWI_CRM_SVC_DailyReport>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SVC_DailyReport>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SVC_DailyReportQuery.SelectQuery,
                                                strSPParameter)
                ,null, out filteredResultsCount,
                string.Format(VWI_CRM_SVC_DailyReportQuery.GetTotalQuery,
                                                strSPParameter));

                totalResultsCount = filteredResultsCount;

                return result;
            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SVC_DailyReport>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SVC_DailyReport vWI_CRM_SVC_DailyReport)
        {
            //vWI_CRM_SVC_DailyReport.CreatedBy = UserLogin;
            //vWI_CRM_SVC_DailyReport.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SVC_DailyReport);
        }

        protected void SetLastModifiedLog(VWI_CRM_SVC_DailyReport vWI_CRM_SVC_DailyReport)
        {
            //vWI_CRM_SVC_DailyReport.LastUpdateBy = UserLogin;
            //vWI_CRM_SVC_DailyReport.LastUpdateTime = DateTime.Now;
        }
    }
}