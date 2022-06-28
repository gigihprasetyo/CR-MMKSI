#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartPurchase repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_PRT_SparepartPurchase;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_SparepartPurchaseRepository : BaseDNetRepository<VWI_CRM_PRT_SparepartPurchase>, IVWI_CRM_PRT_SparepartPurchaseRepository<VWI_CRM_PRT_SparepartPurchase, int>
    {
        #region Constructor
        public VWI_CRM_PRT_SparepartPurchaseRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_SparepartPurchase
        /// <summary>
        /// Create VWI_CRM_PRT_SparepartPurchase
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_SparepartPurchase entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_SparepartPurchase
        /// <summary>
        /// Update VWI_CRM_PRT_SparepartPurchase
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_SparepartPurchase entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_SparepartPurchase
        /// <summary>
        /// Delete VWI_CRM_PRT_SparepartPurchase
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_SparepartPurchase By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_SparepartPurchase Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_SparepartPurchase>(
                        VWI_CRM_PRT_SparepartPurchaseQuery.GetVWI_CRM_PRT_SparepartPurchaseById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_SparepartPurchase
        /// <summary>
        /// Get All VWI_CRM_PRT_SparepartPurchase
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_SparepartPurchase> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_SparepartPurchase
        public List<VWI_CRM_PRT_SparepartPurchase> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_SparepartPurchase>();
        }
        #endregion

		#region Search VWI_CRM_PRT_SparepartPurchase        
        public new List<VWI_CRM_PRT_SparepartPurchase> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_SparepartPurchase> result = Search<VWI_CRM_PRT_SparepartPurchase>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_SparepartPurchase>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_SparepartPurchaseQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_SparepartPurchase.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_SparepartPurchaseQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_SparepartPurchase>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_SparepartPurchase vWI_CRM_PRT_SparepartPurchase)
        {
            //vWI_CRM_PRT_SparepartPurchase.CreatedBy = UserLogin;
            //vWI_CRM_PRT_SparepartPurchase.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_SparepartPurchase);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_SparepartPurchase vWI_CRM_PRT_SparepartPurchase)
        {
            //vWI_CRM_PRT_SparepartPurchase.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_SparepartPurchase.LastUpdateTime = DateTime.Now;
        }
    }
}