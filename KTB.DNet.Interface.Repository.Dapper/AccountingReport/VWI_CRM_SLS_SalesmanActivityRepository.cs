#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_SalesmanActivity repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SLS_SalesmanActivity;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SLS_SalesmanActivityRepository : BaseDNetRepository<VWI_CRM_SLS_SalesmanActivity>, IVWI_CRM_SLS_SalesmanActivityRepository<VWI_CRM_SLS_SalesmanActivity, int>
    {
        #region Constructor
        public VWI_CRM_SLS_SalesmanActivityRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SLS_SalesmanActivity
        /// <summary>
        /// Create VWI_CRM_SLS_SalesmanActivity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SLS_SalesmanActivity entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SLS_SalesmanActivity
        /// <summary>
        /// Update VWI_CRM_SLS_SalesmanActivity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SLS_SalesmanActivity entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SLS_SalesmanActivity
        /// <summary>
        /// Delete VWI_CRM_SLS_SalesmanActivity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SLS_SalesmanActivity By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SLS_SalesmanActivity Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SLS_SalesmanActivity>(
                        VWI_CRM_SLS_SalesmanActivityQuery.GetVWI_CRM_SLS_SalesmanActivityById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SLS_SalesmanActivity
        /// <summary>
        /// Get All VWI_CRM_SLS_SalesmanActivity
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SLS_SalesmanActivity> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SLS_SalesmanActivity
        public List<VWI_CRM_SLS_SalesmanActivity> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SLS_SalesmanActivity>();
        }
        #endregion

		#region Search VWI_CRM_SLS_SalesmanActivity        
        public new List<VWI_CRM_SLS_SalesmanActivity> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SLS_SalesmanActivity> result = Search<VWI_CRM_SLS_SalesmanActivity>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SLS_SalesmanActivity>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SLS_SalesmanActivityQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SLS_SalesmanActivity.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SLS_SalesmanActivityQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SLS_SalesmanActivity>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SLS_SalesmanActivity vWI_CRM_SLS_SalesmanActivity)
        {
            //vWI_CRM_SLS_SalesmanActivity.CreatedBy = UserLogin;
            //vWI_CRM_SLS_SalesmanActivity.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SLS_SalesmanActivity);
        }

        protected void SetLastModifiedLog(VWI_CRM_SLS_SalesmanActivity vWI_CRM_SLS_SalesmanActivity)
        {
            //vWI_CRM_SLS_SalesmanActivity.LastUpdateBy = UserLogin;
            //vWI_CRM_SLS_SalesmanActivity.LastUpdateTime = DateTime.Now;
        }
    }
}