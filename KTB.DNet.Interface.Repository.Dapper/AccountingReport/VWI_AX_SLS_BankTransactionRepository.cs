#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_BankTransaction repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 05/02/2020 9:17:56
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_AX_SLS_BankTransaction;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_AX_SLS_BankTransactionRepository : BaseDNetRepository<VWI_AX_SLS_BankTransaction>, IVWI_AX_SLS_BankTransactionRepository<VWI_AX_SLS_BankTransaction, int>
    {
        #region Constructor
        public VWI_AX_SLS_BankTransactionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_AX_SLS_BankTransaction
        /// <summary>
        /// Create VWI_AX_SLS_BankTransaction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_AX_SLS_BankTransaction entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_AX_SLS_BankTransaction
        /// <summary>
        /// Update VWI_AX_SLS_BankTransaction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_AX_SLS_BankTransaction entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_AX_SLS_BankTransaction
        /// <summary>
        /// Delete VWI_AX_SLS_BankTransaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_AX_SLS_BankTransaction By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_AX_SLS_BankTransaction Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_AX_SLS_BankTransaction>(
                        VWI_AX_SLS_BankTransactionQuery.GetVWI_AX_SLS_BankTransactionById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_AX_SLS_BankTransaction
        /// <summary>
        /// Get All VWI_AX_SLS_BankTransaction
        /// </summary>
        /// <returns></returns>
        public List<VWI_AX_SLS_BankTransaction> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_AX_SLS_BankTransaction
        public List<VWI_AX_SLS_BankTransaction> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_AX_SLS_BankTransaction>();
        }
        #endregion

		#region Search VWI_AX_SLS_BankTransaction        
        public new List<VWI_AX_SLS_BankTransaction> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_AX_SLS_BankTransaction> result = Search<VWI_AX_SLS_BankTransaction>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_AX_SLS_BankTransaction>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_AX_SLS_BankTransactionQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_AX_SLS_BankTransaction.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_AX_SLS_BankTransactionQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_AX_SLS_BankTransaction>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_AX_SLS_BankTransaction vWI_AX_SLS_BankTransaction)
        {
            //vWI_AX_SLS_BankTransaction.CreatedBy = UserLogin;
            //vWI_AX_SLS_BankTransaction.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_AX_SLS_BankTransaction);
        }

        protected void SetLastModifiedLog(VWI_AX_SLS_BankTransaction vWI_AX_SLS_BankTransaction)
        {
            //vWI_AX_SLS_BankTransaction.LastUpdateBy = UserLogin;
            //vWI_AX_SLS_BankTransaction.LastUpdateTime = DateTime.Now;
        }
    }
}