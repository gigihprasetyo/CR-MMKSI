#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_nvsqmiscellaneouscharge repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_nvsqmiscellaneouscharge;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_nvsqmiscellaneouschargeRepository : BaseDNetRepository<VWI_CRM_xts_nvsqmiscellaneouscharge>, IVWI_CRM_xts_nvsqmiscellaneouschargeRepository<VWI_CRM_xts_nvsqmiscellaneouscharge, int>
    {
        #region Constructor
        public VWI_CRM_xts_nvsqmiscellaneouschargeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_nvsqmiscellaneouscharge
        /// <summary>
        /// Create VWI_CRM_xts_nvsqmiscellaneouscharge
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_nvsqmiscellaneouscharge entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_nvsqmiscellaneouscharge
        /// <summary>
        /// Update VWI_CRM_xts_nvsqmiscellaneouscharge
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_nvsqmiscellaneouscharge entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_nvsqmiscellaneouscharge
        /// <summary>
        /// Delete VWI_CRM_xts_nvsqmiscellaneouscharge
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_nvsqmiscellaneouscharge By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_nvsqmiscellaneouscharge Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_nvsqmiscellaneouscharge>(
                        VWI_CRM_xts_nvsqmiscellaneouschargeQuery.GetVWI_CRM_xts_nvsqmiscellaneouschargeById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_nvsqmiscellaneouscharge
        /// <summary>
        /// Get All VWI_CRM_xts_nvsqmiscellaneouscharge
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_nvsqmiscellaneouscharge> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_nvsqmiscellaneouscharge
        public List<VWI_CRM_xts_nvsqmiscellaneouscharge> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_nvsqmiscellaneouscharge>();
        }
        #endregion

		#region Search VWI_CRM_xts_nvsqmiscellaneouscharge        
        public new List<VWI_CRM_xts_nvsqmiscellaneouscharge> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_nvsqmiscellaneouscharge.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_nvsqmiscellaneouscharge> result = SearchData<VWI_CRM_xts_nvsqmiscellaneouscharge>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_nvsqmiscellaneouscharge>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_nvsqmiscellaneouschargeQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_nvsqmiscellaneouscharge.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_nvsqmiscellaneouschargeQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_nvsqmiscellaneouscharge>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_nvsqmiscellaneouscharge vWI_CRM_xts_nvsqmiscellaneouscharge)
        {
            //vWI_CRM_xts_nvsqmiscellaneouscharge.CreatedBy = UserLogin;
            //vWI_CRM_xts_nvsqmiscellaneouscharge.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_nvsqmiscellaneouscharge);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_nvsqmiscellaneouscharge vWI_CRM_xts_nvsqmiscellaneouscharge)
        {
            //vWI_CRM_xts_nvsqmiscellaneouscharge.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_nvsqmiscellaneouscharge.LastUpdateTime = DateTime.Now;
        }
    }
}