#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_campaignresponse repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_campaignresponse;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_campaignresponseRepository : BaseDNetRepository<VWI_CRM_campaignresponse>, IVWI_CRM_campaignresponseRepository<VWI_CRM_campaignresponse, int>
    {
        #region Constructor
        public VWI_CRM_campaignresponseRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_campaignresponse
        /// <summary>
        /// Create VWI_CRM_campaignresponse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_campaignresponse entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_campaignresponse
        /// <summary>
        /// Update VWI_CRM_campaignresponse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_campaignresponse entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_campaignresponse
        /// <summary>
        /// Delete VWI_CRM_campaignresponse
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_campaignresponse By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_campaignresponse Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_campaignresponse>(
                        VWI_CRM_campaignresponseQuery.GetVWI_CRM_campaignresponseById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_campaignresponse
        /// <summary>
        /// Get All VWI_CRM_campaignresponse
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_campaignresponse> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_campaignresponse
        public List<VWI_CRM_campaignresponse> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_campaignresponse>();
        }
        #endregion

		#region Search VWI_CRM_campaignresponse        
        public new List<VWI_CRM_campaignresponse> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_campaignresponse.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_campaignresponse> result = SearchData<VWI_CRM_campaignresponse>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_campaignresponse>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_campaignresponseQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_campaignresponse.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_campaignresponseQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_campaignresponse>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_campaignresponse vWI_CRM_campaignresponse)
        {
            //vWI_CRM_campaignresponse.CreatedBy = UserLogin;
            //vWI_CRM_campaignresponse.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_campaignresponse);
        }

        protected void SetLastModifiedLog(VWI_CRM_campaignresponse vWI_CRM_campaignresponse)
        {
            //vWI_CRM_campaignresponse.LastUpdateBy = UserLogin;
            //vWI_CRM_campaignresponse.LastUpdateTime = DateTime.Now;
        }
    }
}