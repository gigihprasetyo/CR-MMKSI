#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_Purchase repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_PRT_Purchase;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_PurchaseRepository : BaseDNetRepository<VWI_CRM_PRT_Purchase>, IVWI_CRM_PRT_PurchaseRepository<VWI_CRM_PRT_Purchase, int>
    {
        #region Constructor
        public VWI_CRM_PRT_PurchaseRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_Purchase
        /// <summary>
        /// Create VWI_CRM_PRT_Purchase
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_Purchase entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_Purchase
        /// <summary>
        /// Update VWI_CRM_PRT_Purchase
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_Purchase entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_Purchase
        /// <summary>
        /// Delete VWI_CRM_PRT_Purchase
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_Purchase By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_Purchase Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_Purchase>(
                        VWI_CRM_PRT_PurchaseQuery.GetVWI_CRM_PRT_PurchaseById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_Purchase
        /// <summary>
        /// Get All VWI_CRM_PRT_Purchase
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_Purchase> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_Purchase
        public List<VWI_CRM_PRT_Purchase> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_Purchase>();
        }
        #endregion

		#region Search VWI_CRM_PRT_Purchase        
        public new List<VWI_CRM_PRT_Purchase> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_Purchase> result = Search<VWI_CRM_PRT_Purchase>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_Purchase>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_PurchaseQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_Purchase.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_PurchaseQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_Purchase>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_Purchase vWI_CRM_PRT_Purchase)
        {
            //vWI_CRM_PRT_Purchase.CreatedBy = UserLogin;
            //vWI_CRM_PRT_Purchase.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_Purchase);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_Purchase vWI_CRM_PRT_Purchase)
        {
            //vWI_CRM_PRT_Purchase.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_Purchase.LastUpdateTime = DateTime.Now;
        }
    }
}