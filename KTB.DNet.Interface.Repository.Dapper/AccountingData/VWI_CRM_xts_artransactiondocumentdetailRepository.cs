#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_artransactiondocumentdetail repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_artransactiondocumentdetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_artransactiondocumentdetailRepository : BaseDNetRepository<VWI_CRM_xts_artransactiondocumentdetail>, IVWI_CRM_xts_artransactiondocumentdetailRepository<VWI_CRM_xts_artransactiondocumentdetail, int>
    {
        #region Constructor
        public VWI_CRM_xts_artransactiondocumentdetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_artransactiondocumentdetail
        /// <summary>
        /// Create VWI_CRM_xts_artransactiondocumentdetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_artransactiondocumentdetail entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_artransactiondocumentdetail
        /// <summary>
        /// Update VWI_CRM_xts_artransactiondocumentdetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_artransactiondocumentdetail entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_artransactiondocumentdetail
        /// <summary>
        /// Delete VWI_CRM_xts_artransactiondocumentdetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_artransactiondocumentdetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_artransactiondocumentdetail Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_artransactiondocumentdetail>(
                        VWI_CRM_xts_artransactiondocumentdetailQuery.GetVWI_CRM_xts_artransactiondocumentdetailById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_artransactiondocumentdetail
        /// <summary>
        /// Get All VWI_CRM_xts_artransactiondocumentdetail
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_artransactiondocumentdetail> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_artransactiondocumentdetail
        public List<VWI_CRM_xts_artransactiondocumentdetail> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_artransactiondocumentdetail>();
        }
        #endregion

		#region Search VWI_CRM_xts_artransactiondocumentdetail        
        public new List<VWI_CRM_xts_artransactiondocumentdetail> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_artransactiondocumentdetail.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_artransactiondocumentdetail> result = SearchData<VWI_CRM_xts_artransactiondocumentdetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_artransactiondocumentdetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_artransactiondocumentdetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_artransactiondocumentdetail.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_artransactiondocumentdetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_artransactiondocumentdetail>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_artransactiondocumentdetail vWI_CRM_xts_artransactiondocumentdetail)
        {
            //vWI_CRM_xts_artransactiondocumentdetail.CreatedBy = UserLogin;
            //vWI_CRM_xts_artransactiondocumentdetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_artransactiondocumentdetail);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_artransactiondocumentdetail vWI_CRM_xts_artransactiondocumentdetail)
        {
            //vWI_CRM_xts_artransactiondocumentdetail.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_artransactiondocumentdetail.LastUpdateTime = DateTime.Now;
        }
    }
}