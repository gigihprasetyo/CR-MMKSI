#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_nvsomiscellaneouscharge repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_nvsomiscellaneouscharge;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_nvsomiscellaneouschargeRepository : BaseDNetRepository<VWI_CRM_xts_nvsomiscellaneouscharge>, IVWI_CRM_xts_nvsomiscellaneouschargeRepository<VWI_CRM_xts_nvsomiscellaneouscharge, int>
    {
        #region Constructor
        public VWI_CRM_xts_nvsomiscellaneouschargeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_nvsomiscellaneouscharge
        /// <summary>
        /// Create VWI_CRM_xts_nvsomiscellaneouscharge
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_nvsomiscellaneouscharge entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_nvsomiscellaneouscharge
        /// <summary>
        /// Update VWI_CRM_xts_nvsomiscellaneouscharge
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_nvsomiscellaneouscharge entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_nvsomiscellaneouscharge
        /// <summary>
        /// Delete VWI_CRM_xts_nvsomiscellaneouscharge
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_nvsomiscellaneouscharge By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_nvsomiscellaneouscharge Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_nvsomiscellaneouscharge>(
                        VWI_CRM_xts_nvsomiscellaneouschargeQuery.GetVWI_CRM_xts_nvsomiscellaneouschargeById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_nvsomiscellaneouscharge
        /// <summary>
        /// Get All VWI_CRM_xts_nvsomiscellaneouscharge
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_nvsomiscellaneouscharge> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_nvsomiscellaneouscharge
        public List<VWI_CRM_xts_nvsomiscellaneouscharge> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_nvsomiscellaneouscharge>();
        }
        #endregion

		#region Search VWI_CRM_xts_nvsomiscellaneouscharge        
        public new List<VWI_CRM_xts_nvsomiscellaneouscharge> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_nvsomiscellaneouscharge.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_nvsomiscellaneouscharge> result = SearchData<VWI_CRM_xts_nvsomiscellaneouscharge>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_nvsomiscellaneouscharge>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_nvsomiscellaneouschargeQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_nvsomiscellaneouscharge.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_nvsomiscellaneouschargeQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_nvsomiscellaneouscharge>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_nvsomiscellaneouscharge vWI_CRM_xts_nvsomiscellaneouscharge)
        {
            //vWI_CRM_xts_nvsomiscellaneouscharge.CreatedBy = UserLogin;
            //vWI_CRM_xts_nvsomiscellaneouscharge.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_nvsomiscellaneouscharge);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_nvsomiscellaneouscharge vWI_CRM_xts_nvsomiscellaneouscharge)
        {
            //vWI_CRM_xts_nvsomiscellaneouscharge.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_nvsomiscellaneouscharge.LastUpdateTime = DateTime.Now;
        }
    }
}