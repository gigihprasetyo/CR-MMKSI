#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_LedgerTransaction repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 11/02/2020 16:48:10
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_AX_SLS_LedgerTransaction;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_AX_SLS_LedgerTransactionRepository : BaseDNetRepository<VWI_AX_SLS_LedgerTransaction>, IVWI_AX_SLS_LedgerTransactionRepository<VWI_AX_SLS_LedgerTransaction, int>
    {
        #region Constructor
        public VWI_AX_SLS_LedgerTransactionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_AX_SLS_LedgerTransaction
        /// <summary>
        /// Create VWI_AX_SLS_LedgerTransaction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_AX_SLS_LedgerTransaction entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_AX_SLS_LedgerTransaction
        /// <summary>
        /// Update VWI_AX_SLS_LedgerTransaction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_AX_SLS_LedgerTransaction entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_AX_SLS_LedgerTransaction
        /// <summary>
        /// Delete VWI_AX_SLS_LedgerTransaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_AX_SLS_LedgerTransaction By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_AX_SLS_LedgerTransaction Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_AX_SLS_LedgerTransaction>(
                        VWI_AX_SLS_LedgerTransactionQuery.GetVWI_AX_SLS_LedgerTransactionById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_AX_SLS_LedgerTransaction
        /// <summary>
        /// Get All VWI_AX_SLS_LedgerTransaction
        /// </summary>
        /// <returns></returns>
        public List<VWI_AX_SLS_LedgerTransaction> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_AX_SLS_LedgerTransaction
        public List<VWI_AX_SLS_LedgerTransaction> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_AX_SLS_LedgerTransaction>();
        }
        #endregion

		#region Search VWI_AX_SLS_LedgerTransaction        
        public new List<VWI_AX_SLS_LedgerTransaction> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strInnerCriteria = strInnerCriteria.Replace("VWI_AX_SLS_LedgerTransaction.", "");

                List<VWI_AX_SLS_LedgerTransaction> result = SearchData<VWI_AX_SLS_LedgerTransaction>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_AX_SLS_LedgerTransaction>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_AX_SLS_LedgerTransactionQuery.SelectQuery,
                                                strInnerCriteria,
                                                strCriteria)
                , "VWI_AX_SLS_LedgerTransaction.ModifiedDate desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_AX_SLS_LedgerTransactionQuery.GetTotalQuery,
                                                strInnerCriteria,
                                                strCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_AX_SLS_LedgerTransaction>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_AX_SLS_LedgerTransaction vWI_AX_SLS_LedgerTransaction)
        {
            //vWI_AX_SLS_LedgerTransaction.CreatedBy = UserLogin;
            //vWI_AX_SLS_LedgerTransaction.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_AX_SLS_LedgerTransaction);
        }

        protected void SetLastModifiedLog(VWI_AX_SLS_LedgerTransaction vWI_AX_SLS_LedgerTransaction)
        {
            //vWI_AX_SLS_LedgerTransaction.LastUpdateBy = UserLogin;
            //vWI_AX_SLS_LedgerTransaction.LastUpdateTime = DateTime.Now;
        }
    }
}