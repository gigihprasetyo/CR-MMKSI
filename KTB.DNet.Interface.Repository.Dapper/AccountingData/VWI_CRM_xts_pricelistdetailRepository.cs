#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_pricelistdetail repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2020 10:16:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_pricelistdetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_pricelistdetailRepository : BaseDNetRepository<VWI_CRM_xts_pricelistdetail>, IVWI_CRM_xts_pricelistdetailRepository<VWI_CRM_xts_pricelistdetail, int>
    {
        #region Constructor
        public VWI_CRM_xts_pricelistdetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_pricelistdetail
        /// <summary>
        /// Create VWI_CRM_xts_pricelistdetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_pricelistdetail entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_pricelistdetail
        /// <summary>
        /// Update VWI_CRM_xts_pricelistdetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_pricelistdetail entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_pricelistdetail
        /// <summary>
        /// Delete VWI_CRM_xts_pricelistdetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_pricelistdetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_pricelistdetail Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_pricelistdetail>(
                        VWI_CRM_xts_pricelistdetailQuery.GetVWI_CRM_xts_pricelistdetailById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_pricelistdetail
        /// <summary>
        /// Get All VWI_CRM_xts_pricelistdetail
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_pricelistdetail> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_pricelistdetail
        public List<VWI_CRM_xts_pricelistdetail> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_pricelistdetail>();
        }
        #endregion

        #region Search VWI_CRM_xts_pricelistdetail        
        public new List<VWI_CRM_xts_pricelistdetail> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_pricelistdetail.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_pricelistdetail> result = SearchData<VWI_CRM_xts_pricelistdetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_pricelistdetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_pricelistdetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_pricelistdetail.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_pricelistdetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_pricelistdetail>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_pricelistdetail vWI_CRM_xts_pricelistdetail)
        {
            //vWI_CRM_xts_pricelistdetail.CreatedBy = UserLogin;
            //vWI_CRM_xts_pricelistdetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_pricelistdetail);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_pricelistdetail vWI_CRM_xts_pricelistdetail)
        {
            //vWI_CRM_xts_pricelistdetail.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_pricelistdetail.LastUpdateTime = DateTime.Now;
        }
    }
}