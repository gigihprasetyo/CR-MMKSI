#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_goodsreceipt repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 08:37:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_goodsreceipt;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_goodsreceiptRepository : BaseDNetRepository<VWI_CRM_xts_goodsreceipt>, IVWI_CRM_xts_goodsreceiptRepository<VWI_CRM_xts_goodsreceipt, int>
    {
        #region Constructor
        public VWI_CRM_xts_goodsreceiptRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_goodsreceipt
        /// <summary>
        /// Create VWI_CRM_xts_goodsreceipt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_goodsreceipt entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_goodsreceipt
        /// <summary>
        /// Update VWI_CRM_xts_goodsreceipt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_goodsreceipt entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_goodsreceipt
        /// <summary>
        /// Delete VWI_CRM_xts_goodsreceipt
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_goodsreceipt By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_goodsreceipt Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_goodsreceipt>(
                        VWI_CRM_xts_goodsreceiptQuery.GetVWI_CRM_xts_goodsreceiptById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_goodsreceipt
        /// <summary>
        /// Get All VWI_CRM_xts_goodsreceipt
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_goodsreceipt> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_goodsreceipt
        public List<VWI_CRM_xts_goodsreceipt> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_goodsreceipt>();
        }
        #endregion

        #region Search VWI_CRM_xts_goodsreceipt        
        public new List<VWI_CRM_xts_goodsreceipt> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_goodsreceipt.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_goodsreceipt> result = SearchData<VWI_CRM_xts_goodsreceipt>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_goodsreceipt>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_goodsreceiptQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_goodsreceipt.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_goodsreceiptQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_goodsreceipt>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_goodsreceipt vWI_CRM_xts_goodsreceipt)
        {
            //vWI_CRM_xts_goodsreceipt.CreatedBy = UserLogin;
            //vWI_CRM_xts_goodsreceipt.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_goodsreceipt);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_goodsreceipt vWI_CRM_xts_goodsreceipt)
        {
            //vWI_CRM_xts_goodsreceipt.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_goodsreceipt.LastUpdateTime = DateTime.Now;
        }
    }
}