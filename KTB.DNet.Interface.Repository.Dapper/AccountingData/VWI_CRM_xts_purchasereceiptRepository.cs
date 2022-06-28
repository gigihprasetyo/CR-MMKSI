#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchasereceipt repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 03/02/2020 17:40:59
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_purchasereceipt;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_purchasereceiptRepository : BaseDNetRepository<VWI_CRM_xts_purchasereceipt>, IVWI_CRM_xts_purchasereceiptRepository<VWI_CRM_xts_purchasereceipt, int>
    {
        #region Constructor
        public VWI_CRM_xts_purchasereceiptRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_purchasereceipt
        /// <summary>
        /// Create VWI_CRM_xts_purchasereceipt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_purchasereceipt entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_purchasereceipt
        /// <summary>
        /// Update VWI_CRM_xts_purchasereceipt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_purchasereceipt entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_purchasereceipt
        /// <summary>
        /// Delete VWI_CRM_xts_purchasereceipt
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_purchasereceipt By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_purchasereceipt Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_purchasereceipt>(
                        VWI_CRM_xts_purchasereceiptQuery.GetVWI_CRM_xts_purchasereceiptById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_purchasereceipt
        /// <summary>
        /// Get All VWI_CRM_xts_purchasereceipt
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_purchasereceipt> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_purchasereceipt
        public List<VWI_CRM_xts_purchasereceipt> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_purchasereceipt>();
        }
        #endregion

		#region Search VWI_CRM_xts_purchasereceipt        
        public new List<VWI_CRM_xts_purchasereceipt> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_purchasereceipt.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_purchasereceipt> result = SearchData<VWI_CRM_xts_purchasereceipt>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_purchasereceipt>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_purchasereceiptQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_purchasereceipt.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_purchasereceiptQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_purchasereceipt>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_purchasereceipt vWI_CRM_xts_purchasereceipt)
        {
            //vWI_CRM_xts_purchasereceipt.CreatedBy = UserLogin;
            //vWI_CRM_xts_purchasereceipt.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_purchasereceipt);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_purchasereceipt vWI_CRM_xts_purchasereceipt)
        {
            //vWI_CRM_xts_purchasereceipt.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_purchasereceipt.LastUpdateTime = DateTime.Now;
        }
    }
}