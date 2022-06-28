#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_transactioncurrencyRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 08:41:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_transactioncurrency;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_transactioncurrencyRepository : BaseDNetRepository<VWI_CRM_transactioncurrency>
    {
        #region Constructor
        public VWI_CRM_transactioncurrencyRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_transactioncurrency
        /// <summary>
        /// Create VWI_CRM_transactioncurrency
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_transactioncurrency entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_transactioncurrency
        /// <summary>
        /// Update VWI_CRM_transactioncurrency
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_transactioncurrency entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_transactioncurrency
        /// <summary>
        /// Delete VWI_CRM_transactioncurrency
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_transactioncurrency By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_transactioncurrency Get(string id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_transactioncurrency>(
                        VWI_CRM_transactioncurrencyQuery.GetVWI_CRM_transactioncurrencyById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_transactioncurrency
        /// <summary>
        /// Get All VWI_CRM_transactioncurrency
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_transactioncurrency> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_transactioncurrency
        public List<VWI_CRM_transactioncurrency> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_transactioncurrency>();
        }
        #endregion

        #region Search VWI_CRM_transactioncurrency        
        public new List<VWI_CRM_transactioncurrency> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_transactioncurrency.rowstatus", "isnull(vwi_crm_transactioncurrency.rowstatus, 0)");

                List<VWI_CRM_transactioncurrency> result = SearchData<VWI_CRM_transactioncurrency>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_transactioncurrency>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_transactioncurrencyQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_transactioncurrency.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_transactioncurrencyQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_transactioncurrency>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_transactioncurrency VWI_CRM_transactioncurrency)
        {
            //VWI_CRM_transactioncurrency.CreatedBy = UserLogin;
            //VWI_CRM_transactioncurrency.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_CRM_transactioncurrency);
        }

        protected void SetLastModifiedLog(VWI_CRM_transactioncurrency VWI_CRM_transactioncurrency)
        {
            //VWI_CRM_transactioncurrency.LastUpdateBy = UserLogin;
            //VWI_CRM_transactioncurrency.LastUpdateTime = DateTime.Now;
        }
    }
}
