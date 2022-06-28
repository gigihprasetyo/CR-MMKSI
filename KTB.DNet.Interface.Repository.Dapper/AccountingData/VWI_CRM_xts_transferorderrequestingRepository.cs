#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_transferorderrequesting repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 14:16:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_transferorderrequesting;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_transferorderrequestingRepository : BaseDNetRepository<VWI_CRM_xts_transferorderrequesting>, IVWI_CRM_xts_transferorderrequestingRepository<VWI_CRM_xts_transferorderrequesting, int>
    {
        #region Constructor
        public VWI_CRM_xts_transferorderrequestingRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_transferorderrequesting
        /// <summary>
        /// Create VWI_CRM_xts_transferorderrequesting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_transferorderrequesting entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_transferorderrequesting
        /// <summary>
        /// Update VWI_CRM_xts_transferorderrequesting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_transferorderrequesting entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_transferorderrequesting
        /// <summary>
        /// Delete VWI_CRM_xts_transferorderrequesting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_transferorderrequesting By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_transferorderrequesting Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_transferorderrequesting>(
                        VWI_CRM_xts_transferorderrequestingQuery.GetVWI_CRM_xts_transferorderrequestingById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_transferorderrequesting
        /// <summary>
        /// Get All VWI_CRM_xts_transferorderrequesting
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_transferorderrequesting> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_transferorderrequesting
        public List<VWI_CRM_xts_transferorderrequesting> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_transferorderrequesting>();
        }
        #endregion

        #region Search VWI_CRM_xts_transferorderrequesting        
        public new List<VWI_CRM_xts_transferorderrequesting> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_transferorderrequesting.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_transferorderrequesting> result = SearchData<VWI_CRM_xts_transferorderrequesting>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_transferorderrequesting>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_transferorderrequestingQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_transferorderrequesting.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_transferorderrequestingQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_transferorderrequesting>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_transferorderrequesting vWI_CRM_xts_transferorderrequesting)
        {
            //vWI_CRM_xts_transferorderrequesting.CreatedBy = UserLogin;
            //vWI_CRM_xts_transferorderrequesting.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_transferorderrequesting);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_transferorderrequesting vWI_CRM_xts_transferorderrequesting)
        {
            //vWI_CRM_xts_transferorderrequesting.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_transferorderrequesting.LastUpdateTime = DateTime.Now;
        }
    }
}