#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_lkppdetail repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_lkppdetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_lkppdetailRepository : BaseDNetRepository<VWI_CRM_ktb_lkppdetail>, IVWI_CRM_ktb_lkppdetailRepository<VWI_CRM_ktb_lkppdetail, int>
    {
        #region Constructor
        public VWI_CRM_ktb_lkppdetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_lkppdetail
        /// <summary>
        /// Create VWI_CRM_ktb_lkppdetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_lkppdetail entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_lkppdetail
        /// <summary>
        /// Update VWI_CRM_ktb_lkppdetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_lkppdetail entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_lkppdetail
        /// <summary>
        /// Delete VWI_CRM_ktb_lkppdetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_lkppdetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_lkppdetail Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_lkppdetail>(
                        VWI_CRM_ktb_lkppdetailQuery.GetVWI_CRM_ktb_lkppdetailById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_lkppdetail
        /// <summary>
        /// Get All VWI_CRM_ktb_lkppdetail
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_lkppdetail> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_lkppdetail
        public List<VWI_CRM_ktb_lkppdetail> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_lkppdetail>();
        }
        #endregion

        #region Search VWI_CRM_ktb_lkppdetail        
        public new List<VWI_CRM_ktb_lkppdetail> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_lkppdetail.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "c.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "c.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "c.msdyn_companycode");
                strCriteria = strCriteria.ToLower().Replace("a.ktb_nopengadaan", "b.ktb_nopengadaan");

                List<VWI_CRM_ktb_lkppdetail> result = SearchData<VWI_CRM_ktb_lkppdetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_lkppdetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_lkppdetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_lkppdetail.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_lkppdetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_lkppdetail>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_lkppdetail vWI_CRM_ktb_lkppdetail)
        {
            //vWI_CRM_ktb_lkppdetail.CreatedBy = UserLogin;
            //vWI_CRM_ktb_lkppdetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_lkppdetail);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_lkppdetail vWI_CRM_ktb_lkppdetail)
        {
            //vWI_CRM_ktb_lkppdetail.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_lkppdetail.LastUpdateTime = DateTime.Now;
        }
    }
}