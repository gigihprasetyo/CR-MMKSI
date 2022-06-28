#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingTransaction repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_PRT_OutgoingTransaction;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_OutgoingTransactionRepository : BaseDNetRepository<VWI_CRM_PRT_OutgoingTransaction>, IVWI_CRM_PRT_OutgoingTransactionRepository<VWI_CRM_PRT_OutgoingTransaction, int>
    {
        #region Constructor
        public VWI_CRM_PRT_OutgoingTransactionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_OutgoingTransaction
        /// <summary>
        /// Create VWI_CRM_PRT_OutgoingTransaction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_OutgoingTransaction entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_OutgoingTransaction
        /// <summary>
        /// Update VWI_CRM_PRT_OutgoingTransaction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_OutgoingTransaction entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_OutgoingTransaction
        /// <summary>
        /// Delete VWI_CRM_PRT_OutgoingTransaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_OutgoingTransaction By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_OutgoingTransaction Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_OutgoingTransaction>(
                        VWI_CRM_PRT_OutgoingTransactionQuery.GetVWI_CRM_PRT_OutgoingTransactionById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_OutgoingTransaction
        /// <summary>
        /// Get All VWI_CRM_PRT_OutgoingTransaction
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_OutgoingTransaction> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_OutgoingTransaction
        public List<VWI_CRM_PRT_OutgoingTransaction> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_OutgoingTransaction>();
        }
        #endregion

		#region Search VWI_CRM_PRT_OutgoingTransaction        
        public new List<VWI_CRM_PRT_OutgoingTransaction> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_OutgoingTransaction> result = Search<VWI_CRM_PRT_OutgoingTransaction>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_OutgoingTransaction>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_OutgoingTransactionQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_OutgoingTransaction.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_OutgoingTransactionQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_OutgoingTransaction>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_OutgoingTransaction vWI_CRM_PRT_OutgoingTransaction)
        {
            //vWI_CRM_PRT_OutgoingTransaction.CreatedBy = UserLogin;
            //vWI_CRM_PRT_OutgoingTransaction.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_OutgoingTransaction);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_OutgoingTransaction vWI_CRM_PRT_OutgoingTransaction)
        {
            //vWI_CRM_PRT_OutgoingTransaction.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_OutgoingTransaction.LastUpdateTime = DateTime.Now;
        }
    }
}