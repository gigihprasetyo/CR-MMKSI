#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingTransaction repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_PRT_IncomingTransaction;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_IncomingTransactionRepository : BaseDNetRepository<VWI_CRM_PRT_IncomingTransaction>, IVWI_CRM_PRT_IncomingTransactionRepository<VWI_CRM_PRT_IncomingTransaction, int>
    {
        #region Constructor
        public VWI_CRM_PRT_IncomingTransactionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_IncomingTransaction
        /// <summary>
        /// Create VWI_CRM_PRT_IncomingTransaction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_IncomingTransaction entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_IncomingTransaction
        /// <summary>
        /// Update VWI_CRM_PRT_IncomingTransaction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_IncomingTransaction entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_IncomingTransaction
        /// <summary>
        /// Delete VWI_CRM_PRT_IncomingTransaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_IncomingTransaction By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_IncomingTransaction Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_IncomingTransaction>(
                        VWI_CRM_PRT_IncomingTransactionQuery.GetVWI_CRM_PRT_IncomingTransactionById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_IncomingTransaction
        /// <summary>
        /// Get All VWI_CRM_PRT_IncomingTransaction
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_IncomingTransaction> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_IncomingTransaction
        public List<VWI_CRM_PRT_IncomingTransaction> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_IncomingTransaction>();
        }
        #endregion

		#region Search VWI_CRM_PRT_IncomingTransaction        
        public new List<VWI_CRM_PRT_IncomingTransaction> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_IncomingTransaction> result = Search<VWI_CRM_PRT_IncomingTransaction>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_IncomingTransaction>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_IncomingTransactionQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_IncomingTransaction.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_IncomingTransactionQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_IncomingTransaction>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_IncomingTransaction vWI_CRM_PRT_IncomingTransaction)
        {
            //vWI_CRM_PRT_IncomingTransaction.CreatedBy = UserLogin;
            //vWI_CRM_PRT_IncomingTransaction.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_IncomingTransaction);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_IncomingTransaction vWI_CRM_PRT_IncomingTransaction)
        {
            //vWI_CRM_PRT_IncomingTransaction.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_IncomingTransaction.LastUpdateTime = DateTime.Now;
        }
    }
}