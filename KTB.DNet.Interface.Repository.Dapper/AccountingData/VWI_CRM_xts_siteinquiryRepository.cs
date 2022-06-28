#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_siteinquiryRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/22/2020 17:30:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_siteinquiry;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_siteinquiryRepository : BaseDNetRepository<VWI_CRM_xts_siteinquiry>, IVWI_CRM_xts_siteinquiryRepository<VWI_CRM_xts_siteinquiry, int>
    {
        #region Constructor
        public VWI_CRM_xts_siteinquiryRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_siteinquiry
        /// <summary>
        /// Create VWI_CRM_xts_siteinquiry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_siteinquiry entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_siteinquiry
        /// <summary>
        /// Update VWI_CRM_xts_siteinquiry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_siteinquiry entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_siteinquiry
        /// <summary>
        /// Delete VWI_CRM_xts_siteinquiry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_siteinquiry By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_siteinquiry Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_siteinquiry>(
                        VWI_CRM_xts_siteinquiryQuery.GetVWI_CRM_xts_siteinquiryById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_siteinquiry
        /// <summary>
        /// Get All VWI_CRM_xts_siteinquiry
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_siteinquiry> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_siteinquiry
        public List<VWI_CRM_xts_siteinquiry> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_siteinquiry>();
        }
        #endregion

        #region Search VWI_CRM_xts_siteinquiry        
        public new List<VWI_CRM_xts_siteinquiry> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_siteinquiry.rowstatus", "isnull(vwi_crm_xts_siteinquiry.rowstatus, 0)");

                List<VWI_CRM_xts_siteinquiry> result = SearchData<VWI_CRM_xts_siteinquiry>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_siteinquiry>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_siteinquiryQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_siteinquiry.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_siteinquiryQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_siteinquiry>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_siteinquiry vWI_CRM_xts_siteinquiry)
        {
            //vWI_CRM_xts_siteinquiry.CreatedBy = UserLogin;
            //vWI_CRM_xts_siteinquiry.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_siteinquiry);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_siteinquiry vWI_CRM_xts_siteinquiry)
        {
            //vWI_CRM_xts_siteinquiry.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_siteinquiry.LastUpdateTime = DateTime.Now;
        }
    }
}
