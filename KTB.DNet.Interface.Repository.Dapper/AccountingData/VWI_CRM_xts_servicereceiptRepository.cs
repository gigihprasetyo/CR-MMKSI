#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicereceipt repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_servicereceipt;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_servicereceiptRepository : BaseDNetRepository<VWI_CRM_xts_servicereceipt>, IVWI_CRM_xts_servicereceiptRepository<VWI_CRM_xts_servicereceipt, int>
    {
        #region Constructor
        public VWI_CRM_xts_servicereceiptRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_servicereceipt
        /// <summary>
        /// Create VWI_CRM_xts_servicereceipt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_servicereceipt entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_servicereceipt
        /// <summary>
        /// Update VWI_CRM_xts_servicereceipt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_servicereceipt entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_servicereceipt
        /// <summary>
        /// Delete VWI_CRM_xts_servicereceipt
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_servicereceipt By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_servicereceipt Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_servicereceipt>(
                        VWI_CRM_xts_servicereceiptQuery.GetVWI_CRM_xts_servicereceiptById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_servicereceipt
        /// <summary>
        /// Get All VWI_CRM_xts_servicereceipt
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_servicereceipt> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_servicereceipt
        public List<VWI_CRM_xts_servicereceipt> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_servicereceipt>();
        }
        #endregion

        #region Search VWI_CRM_xts_servicereceipt        
        public new List<VWI_CRM_xts_servicereceipt> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_servicereceipt.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_servicereceipt> result = SearchData<VWI_CRM_xts_servicereceipt>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_servicereceipt>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_servicereceiptQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_servicereceipt.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_servicereceiptQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_servicereceipt>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_servicereceipt vWI_CRM_xts_servicereceipt)
        {
            //vWI_CRM_xts_servicereceipt.CreatedBy = UserLogin;
            //vWI_CRM_xts_servicereceipt.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_servicereceipt);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_servicereceipt vWI_CRM_xts_servicereceipt)
        {
            //vWI_CRM_xts_servicereceipt.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_servicereceipt.LastUpdateTime = DateTime.Now;
        }
    }
}