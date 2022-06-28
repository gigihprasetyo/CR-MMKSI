#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_ARBalance repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SLS_ARBalance;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SLS_ARBalanceRepository : BaseDNetRepository<VWI_CRM_SLS_ARBalance>, IVWI_CRM_SLS_ARBalanceRepository<VWI_CRM_SLS_ARBalance, int>
    {
        #region Constructor
        public VWI_CRM_SLS_ARBalanceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SLS_ARBalance
        /// <summary>
        /// Create VWI_CRM_SLS_ARBalance
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SLS_ARBalance entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SLS_ARBalance
        /// <summary>
        /// Update VWI_CRM_SLS_ARBalance
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SLS_ARBalance entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SLS_ARBalance
        /// <summary>
        /// Delete VWI_CRM_SLS_ARBalance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SLS_ARBalance By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SLS_ARBalance Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SLS_ARBalance>(
                        VWI_CRM_SLS_ARBalanceQuery.GetVWI_CRM_SLS_ARBalanceById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SLS_ARBalance
        /// <summary>
        /// Get All VWI_CRM_SLS_ARBalance
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SLS_ARBalance> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SLS_ARBalance
        public List<VWI_CRM_SLS_ARBalance> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SLS_ARBalance>();
        }
        #endregion

		#region Search VWI_CRM_SLS_ARBalance        
        public new List<VWI_CRM_SLS_ARBalance> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SLS_ARBalance> result = Search<VWI_CRM_SLS_ARBalance>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SLS_ARBalance>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SLS_ARBalanceQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SLS_ARBalance.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SLS_ARBalanceQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SLS_ARBalance>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SLS_ARBalance vWI_CRM_SLS_ARBalance)
        {
            //vWI_CRM_SLS_ARBalance.CreatedBy = UserLogin;
            //vWI_CRM_SLS_ARBalance.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SLS_ARBalance);
        }

        protected void SetLastModifiedLog(VWI_CRM_SLS_ARBalance vWI_CRM_SLS_ARBalance)
        {
            //vWI_CRM_SLS_ARBalance.LastUpdateBy = UserLogin;
            //vWI_CRM_SLS_ARBalance.LastUpdateTime = DateTime.Now;
        }
    }
}