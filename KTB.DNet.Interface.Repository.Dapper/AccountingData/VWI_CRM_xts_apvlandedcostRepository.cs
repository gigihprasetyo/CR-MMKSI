#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_apvlandedcost repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_apvlandedcost;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_apvlandedcostRepository : BaseDNetRepository<VWI_CRM_xts_apvlandedcost>, IVWI_CRM_xts_apvlandedcostRepository<VWI_CRM_xts_apvlandedcost, int>
    {
        #region Constructor
        public VWI_CRM_xts_apvlandedcostRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_apvlandedcost
        /// <summary>
        /// Create VWI_CRM_xts_apvlandedcost
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_apvlandedcost entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_apvlandedcost
        /// <summary>
        /// Update VWI_CRM_xts_apvlandedcost
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_apvlandedcost entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_apvlandedcost
        /// <summary>
        /// Delete VWI_CRM_xts_apvlandedcost
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_apvlandedcost By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_apvlandedcost Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_apvlandedcost>(
                        VWI_CRM_xts_apvlandedcostQuery.GetVWI_CRM_xts_apvlandedcostById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_apvlandedcost
        /// <summary>
        /// Get All VWI_CRM_xts_apvlandedcost
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_apvlandedcost> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_apvlandedcost
        public List<VWI_CRM_xts_apvlandedcost> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_apvlandedcost>();
        }
        #endregion

		#region Search VWI_CRM_xts_apvlandedcost        
        public new List<VWI_CRM_xts_apvlandedcost> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_apvlandedcost.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_apvlandedcost> result = SearchData<VWI_CRM_xts_apvlandedcost>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_apvlandedcost>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_apvlandedcostQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_apvlandedcost.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_apvlandedcostQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_apvlandedcost>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_apvlandedcost vWI_CRM_xts_apvlandedcost)
        {
            //vWI_CRM_xts_apvlandedcost.CreatedBy = UserLogin;
            //vWI_CRM_xts_apvlandedcost.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_apvlandedcost);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_apvlandedcost vWI_CRM_xts_apvlandedcost)
        {
            //vWI_CRM_xts_apvlandedcost.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_apvlandedcost.LastUpdateTime = DateTime.Now;
        }
    }
}