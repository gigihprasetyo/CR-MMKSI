#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_businessunitinquiryRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/22/2020 17:53:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_businessunitinquiry;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_businessunitinquiryRepository : BaseDNetRepository<VWI_CRM_xts_businessunitinquiry>, IVWI_CRM_xts_businessunitinquiryRepository<VWI_CRM_xts_businessunitinquiry, int>
    {
        #region Constructor
        public VWI_CRM_xts_businessunitinquiryRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_businessunitinquiry
        /// <summary>
        /// Create VWI_CRM_xts_businessunitinquiry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_businessunitinquiry entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_businessunitinquiry
        /// <summary>
        /// Update VWI_CRM_xts_businessunitinquiry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_businessunitinquiry entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_businessunitinquiry
        /// <summary>
        /// Delete VWI_CRM_xts_businessunitinquiry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_businessunitinquiry By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_businessunitinquiry Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_businessunitinquiry>(
                        VWI_CRM_xts_businessunitinquiryQuery.GetVWI_CRM_xts_businessunitinquiryById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_businessunitinquiry
        /// <summary>
        /// Get All VWI_CRM_xts_businessunitinquiry
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_businessunitinquiry> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_businessunitinquiry
        public List<VWI_CRM_xts_businessunitinquiry> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_businessunitinquiry>();
        }
        #endregion

        #region Search VWI_CRM_xts_businessunitinquiry        
        public new List<VWI_CRM_xts_businessunitinquiry> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(vwi_crm_xts_businessunitinquiry.rowstatus, 0)");

                List<VWI_CRM_xts_businessunitinquiry> result = SearchData<VWI_CRM_xts_businessunitinquiry>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_businessunitinquiry>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_businessunitinquiryQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_businessunitinquiry.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_businessunitinquiryQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_businessunitinquiry>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_businessunitinquiry vWI_CRM_xts_businessunitinquiry)
        {
            //vWI_CRM_xts_businessunitinquiry.CreatedBy = UserLogin;
            //vWI_CRM_xts_businessunitinquiry.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_businessunitinquiry);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_businessunitinquiry vWI_CRM_xts_businessunitinquiry)
        {
            //vWI_CRM_xts_businessunitinquiry.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_businessunitinquiry.LastUpdateTime = DateTime.Now;
        }
    }
}
