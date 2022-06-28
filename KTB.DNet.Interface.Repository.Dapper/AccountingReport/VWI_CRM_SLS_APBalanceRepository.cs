#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_APBalance repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SLS_APBalance;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SLS_APBalanceRepository : BaseDNetRepository<VWI_CRM_SLS_APBalance>, IVWI_CRM_SLS_APBalanceRepository<VWI_CRM_SLS_APBalance, int>
    {
        #region Constructor
        public VWI_CRM_SLS_APBalanceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SLS_APBalance
        /// <summary>
        /// Create VWI_CRM_SLS_APBalance
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SLS_APBalance entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SLS_APBalance
        /// <summary>
        /// Update VWI_CRM_SLS_APBalance
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SLS_APBalance entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SLS_APBalance
        /// <summary>
        /// Delete VWI_CRM_SLS_APBalance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SLS_APBalance By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SLS_APBalance Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SLS_APBalance>(
                        VWI_CRM_SLS_APBalanceQuery.GetVWI_CRM_SLS_APBalanceById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SLS_APBalance
        /// <summary>
        /// Get All VWI_CRM_SLS_APBalance
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SLS_APBalance> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SLS_APBalance
        public List<VWI_CRM_SLS_APBalance> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SLS_APBalance>();
        }
        #endregion

		#region Search VWI_CRM_SLS_APBalance        
        public new List<VWI_CRM_SLS_APBalance> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SLS_APBalance> result = Search<VWI_CRM_SLS_APBalance>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SLS_APBalance>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SLS_APBalanceQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SLS_APBalance.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SLS_APBalanceQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SLS_APBalance>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SLS_APBalance vWI_CRM_SLS_APBalance)
        {
            //vWI_CRM_SLS_APBalance.CreatedBy = UserLogin;
            //vWI_CRM_SLS_APBalance.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SLS_APBalance);
        }

        protected void SetLastModifiedLog(VWI_CRM_SLS_APBalance vWI_CRM_SLS_APBalance)
        {
            //vWI_CRM_SLS_APBalance.LastUpdateBy = UserLogin;
            //vWI_CRM_SLS_APBalance.LastUpdateTime = DateTime.Now;
        }
    }
}