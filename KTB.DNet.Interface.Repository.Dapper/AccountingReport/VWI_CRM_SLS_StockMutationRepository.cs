#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_StockMutation repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SLS_StockMutation;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SLS_StockMutationRepository : BaseDNetRepository<VWI_CRM_SLS_StockMutation>, IVWI_CRM_SLS_StockMutationRepository<VWI_CRM_SLS_StockMutation, int>
    {
        #region Constructor
        public VWI_CRM_SLS_StockMutationRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SLS_StockMutation
        /// <summary>
        /// Create VWI_CRM_SLS_StockMutation
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SLS_StockMutation entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SLS_StockMutation
        /// <summary>
        /// Update VWI_CRM_SLS_StockMutation
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SLS_StockMutation entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SLS_StockMutation
        /// <summary>
        /// Delete VWI_CRM_SLS_StockMutation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SLS_StockMutation By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SLS_StockMutation Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SLS_StockMutation>(
                        VWI_CRM_SLS_StockMutationQuery.GetVWI_CRM_SLS_StockMutationById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SLS_StockMutation
        /// <summary>
        /// Get All VWI_CRM_SLS_StockMutation
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SLS_StockMutation> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SLS_StockMutation
        public List<VWI_CRM_SLS_StockMutation> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SLS_StockMutation>();
        }
        #endregion

		#region Search VWI_CRM_SLS_StockMutation        
        public new List<VWI_CRM_SLS_StockMutation> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SLS_StockMutation> result = Search<VWI_CRM_SLS_StockMutation>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SLS_StockMutation>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SLS_StockMutationQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SLS_StockMutation.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SLS_StockMutationQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SLS_StockMutation>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SLS_StockMutation vWI_CRM_SLS_StockMutation)
        {
            //vWI_CRM_SLS_StockMutation.CreatedBy = UserLogin;
            //vWI_CRM_SLS_StockMutation.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SLS_StockMutation);
        }

        protected void SetLastModifiedLog(VWI_CRM_SLS_StockMutation vWI_CRM_SLS_StockMutation)
        {
            //vWI_CRM_SLS_StockMutation.LastUpdateBy = UserLogin;
            //vWI_CRM_SLS_StockMutation.LastUpdateTime = DateTime.Now;
        }
    }
}