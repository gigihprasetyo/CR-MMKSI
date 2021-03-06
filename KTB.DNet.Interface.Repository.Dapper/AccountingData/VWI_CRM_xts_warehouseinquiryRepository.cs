#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_warehouseinquiryRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2020 11:13:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_warehouseinquiry;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_warehouseinquiryRepository : BaseDNetRepository<VWI_CRM_xts_warehouseinquiry>, IVWI_CRM_xts_warehouseinquiryRepository<VWI_CRM_xts_warehouseinquiry, int>
    {
        #region Constructor
        public VWI_CRM_xts_warehouseinquiryRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_warehouseinquiry
        /// <summary>
        /// Create VWI_CRM_xts_warehouseinquiry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_warehouseinquiry entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_warehouseinquiry
        /// <summary>
        /// Update VWI_CRM_xts_warehouseinquiry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_warehouseinquiry entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_warehouseinquiry
        /// <summary>
        /// Delete VWI_CRM_xts_warehouseinquiry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_warehouseinquiry By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_warehouseinquiry Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_warehouseinquiry>(
                        VWI_CRM_xts_warehouseinquiryQuery.GetVWI_CRM_xts_warehouseinquiryById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_warehouseinquiry
        /// <summary>
        /// Get All VWI_CRM_xts_warehouseinquiry
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_warehouseinquiry> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_warehouseinquiry
        public List<VWI_CRM_xts_warehouseinquiry> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_warehouseinquiry>();
        }
        #endregion

        #region Search VWI_CRM_xts_warehouseinquiry        
        public new List<VWI_CRM_xts_warehouseinquiry> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_warehouseinquiry.rowstatus", "isnull(vwi_crm_xts_warehouseinquiry.rowstatus, 0)");

                List<VWI_CRM_xts_warehouseinquiry> result = SearchData<VWI_CRM_xts_warehouseinquiry>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_warehouseinquiry>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_warehouseinquiryQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_warehouseinquiry.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_warehouseinquiryQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_warehouseinquiry>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_warehouseinquiry vWI_CRM_xts_warehouseinquiry)
        {
            //vWI_CRM_xts_warehouseinquiry.CreatedBy = UserLogin;
            //vWI_CRM_xts_warehouseinquiry.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_warehouseinquiry);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_warehouseinquiry vWI_CRM_xts_warehouseinquiry)
        {
            //vWI_CRM_xts_warehouseinquiry.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_warehouseinquiry.LastUpdateTime = DateTime.Now;
        }
    }
}
