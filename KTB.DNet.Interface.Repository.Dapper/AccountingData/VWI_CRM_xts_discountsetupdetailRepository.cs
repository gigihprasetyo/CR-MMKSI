#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_discountsetupdetail repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 14:44:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_discountsetupdetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_discountsetupdetailRepository : BaseDNetRepository<VWI_CRM_xts_discountsetupdetail>, IVWI_CRM_xts_discountsetupdetailRepository<VWI_CRM_xts_discountsetupdetail, int>
    {
        #region Constructor
        public VWI_CRM_xts_discountsetupdetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_discountsetupdetail
        /// <summary>
        /// Create VWI_CRM_xts_discountsetupdetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_discountsetupdetail entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_discountsetupdetail
        /// <summary>
        /// Update VWI_CRM_xts_discountsetupdetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_discountsetupdetail entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_discountsetupdetail
        /// <summary>
        /// Delete VWI_CRM_xts_discountsetupdetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_discountsetupdetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_discountsetupdetail Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_discountsetupdetail>(
                        VWI_CRM_xts_discountsetupdetailQuery.GetVWI_CRM_xts_discountsetupdetailById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_discountsetupdetail
        /// <summary>
        /// Get All VWI_CRM_xts_discountsetupdetail
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_discountsetupdetail> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_discountsetupdetail
        public List<VWI_CRM_xts_discountsetupdetail> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_discountsetupdetail>();
        }
        #endregion

        #region Search VWI_CRM_xts_discountsetupdetail        
        public new List<VWI_CRM_xts_discountsetupdetail> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_discountsetupdetail.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_discountsetupdetail> result = SearchData<VWI_CRM_xts_discountsetupdetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_discountsetupdetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_discountsetupdetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_discountsetupdetail.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_discountsetupdetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_discountsetupdetail>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_discountsetupdetail vWI_CRM_xts_discountsetupdetail)
        {
            //vWI_CRM_xts_discountsetupdetail.CreatedBy = UserLogin;
            //vWI_CRM_xts_discountsetupdetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_discountsetupdetail);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_discountsetupdetail vWI_CRM_xts_discountsetupdetail)
        {
            //vWI_CRM_xts_discountsetupdetail.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_discountsetupdetail.LastUpdateTime = DateTime.Now;
        }
    }
}