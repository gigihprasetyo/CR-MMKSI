#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountreceivablereceiptotherexpense repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:25:51
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.xts_accountreceivablereceiptotherexpense;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_accountreceivablereceiptotherexpenseRepository : BaseDNetRepository<VWI_CRM_xts_accountreceivablereceiptotherexpense>, IVWI_CRM_xts_accountreceivablereceiptotherexpenseRepository<VWI_CRM_xts_accountreceivablereceiptotherexpense, int>
    {
        #region Constructor
        public VWI_CRM_xts_accountreceivablereceiptotherexpenseRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_accountreceivablereceiptotherexpense
        /// <summary>
        /// Create VWI_CRM_xts_accountreceivablereceiptotherexpense
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_accountreceivablereceiptotherexpense entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_accountreceivablereceiptotherexpense
        /// <summary>
        /// Update VWI_CRM_xts_accountreceivablereceiptotherexpense
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_accountreceivablereceiptotherexpense entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_accountreceivablereceiptotherexpense
        /// <summary>
        /// Delete VWI_CRM_xts_accountreceivablereceiptotherexpense
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_accountreceivablereceiptotherexpense By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_accountreceivablereceiptotherexpense Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_accountreceivablereceiptotherexpense>(
                        VWI_CRM_xts_accountreceivablereceiptotherexpenseQuery.GetVWI_CRM_xts_accountreceivablereceiptotherexpenseById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_accountreceivablereceiptotherexpense
        /// <summary>
        /// Get All VWI_CRM_xts_accountreceivablereceiptotherexpense
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_accountreceivablereceiptotherexpense> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_accountreceivablereceiptotherexpense
        public List<VWI_CRM_xts_accountreceivablereceiptotherexpense> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_accountreceivablereceiptotherexpense>();
        }
        #endregion

		#region Search VWI_CRM_xts_accountreceivablereceiptotherexpense        
        public new List<VWI_CRM_xts_accountreceivablereceiptotherexpense> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_accountreceivablereceiptotherexpense.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_accountreceivablereceiptotherexpense> result = SearchData<VWI_CRM_xts_accountreceivablereceiptotherexpense>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_accountreceivablereceiptotherexpense>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_accountreceivablereceiptotherexpenseQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_accountreceivablereceiptotherexpense.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_accountreceivablereceiptotherexpenseQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_accountreceivablereceiptotherexpense>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_accountreceivablereceiptotherexpense vWI_CRM_xts_accountreceivablereceiptotherexpense)
        {
            //vWI_CRM_xts_accountreceivablereceiptotherexpense.CreatedBy = UserLogin;
            //vWI_CRM_xts_accountreceivablereceiptotherexpense.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_accountreceivablereceiptotherexpense);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_accountreceivablereceiptotherexpense vWI_CRM_xts_accountreceivablereceiptotherexpense)
        {
            //vWI_CRM_xts_accountreceivablereceiptotherexpense.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_accountreceivablereceiptotherexpense.LastUpdateTime = DateTime.Now;
        }
    }
}