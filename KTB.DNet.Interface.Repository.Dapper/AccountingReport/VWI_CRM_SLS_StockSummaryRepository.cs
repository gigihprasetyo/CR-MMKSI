#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_StockSummary repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 03/02/2020 15:09:45
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SLS_StockSummary;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SLS_StockSummaryRepository : BaseDNetRepository<VWI_CRM_SLS_StockSummary>, IVWI_CRM_SLS_StockSummaryRepository<VWI_CRM_SLS_StockSummary, int>
    {
        #region Constructor
        public VWI_CRM_SLS_StockSummaryRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SLS_StockSummary
        /// <summary>
        /// Create VWI_CRM_SLS_StockSummary
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SLS_StockSummary entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SLS_StockSummary
        /// <summary>
        /// Update VWI_CRM_SLS_StockSummary
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SLS_StockSummary entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SLS_StockSummary
        /// <summary>
        /// Delete VWI_CRM_SLS_StockSummary
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SLS_StockSummary By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SLS_StockSummary Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SLS_StockSummary>(
                        VWI_CRM_SLS_StockSummaryQuery.GetVWI_CRM_SLS_StockSummaryById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SLS_StockSummary
        /// <summary>
        /// Get All VWI_CRM_SLS_StockSummary
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SLS_StockSummary> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SLS_StockSummary
        public List<VWI_CRM_SLS_StockSummary> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SLS_StockSummary>();
        }
        #endregion

		#region Search VWI_CRM_SLS_StockSummary        
        public new List<VWI_CRM_SLS_StockSummary> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SLS_StockSummary> result = Search<VWI_CRM_SLS_StockSummary>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SLS_StockSummary>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SLS_StockSummaryQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SLS_StockSummary.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SLS_StockSummaryQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SLS_StockSummary>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SLS_StockSummary vWI_CRM_SLS_StockSummary)
        {
            //vWI_CRM_SLS_StockSummary.CreatedBy = UserLogin;
            //vWI_CRM_SLS_StockSummary.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SLS_StockSummary);
        }

        protected void SetLastModifiedLog(VWI_CRM_SLS_StockSummary vWI_CRM_SLS_StockSummary)
        {
            //vWI_CRM_SLS_StockSummary.LastUpdateBy = UserLogin;
            //vWI_CRM_SLS_StockSummary.LastUpdateTime = DateTime.Now;
        }
    }
}