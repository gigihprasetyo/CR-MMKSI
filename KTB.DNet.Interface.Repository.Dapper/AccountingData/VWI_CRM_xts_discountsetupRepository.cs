#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_discountsetup repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 14:40:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_discountsetup;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_discountsetupRepository : BaseDNetRepository<VWI_CRM_xts_discountsetup>, IVWI_CRM_xts_discountsetupRepository<VWI_CRM_xts_discountsetup, int>
    {
        #region Constructor
        public VWI_CRM_xts_discountsetupRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_discountsetup
        /// <summary>
        /// Create VWI_CRM_xts_discountsetup
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_discountsetup entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_discountsetup
        /// <summary>
        /// Update VWI_CRM_xts_discountsetup
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_discountsetup entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_discountsetup
        /// <summary>
        /// Delete VWI_CRM_xts_discountsetup
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_discountsetup By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_discountsetup Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_discountsetup>(
                        VWI_CRM_xts_discountsetupQuery.GetVWI_CRM_xts_discountsetupById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_discountsetup
        /// <summary>
        /// Get All VWI_CRM_xts_discountsetup
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_discountsetup> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_discountsetup
        public List<VWI_CRM_xts_discountsetup> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_discountsetup>();
        }
        #endregion

        #region Search VWI_CRM_xts_discountsetup        
        public new List<VWI_CRM_xts_discountsetup> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try 
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_discountsetup.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_discountsetup> result = SearchData<VWI_CRM_xts_discountsetup>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_discountsetup>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_discountsetupQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_discountsetup.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_discountsetupQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_discountsetup>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_discountsetup vWI_CRM_xts_discountsetup)
        {
            //vWI_CRM_xts_discountsetup.CreatedBy = UserLogin;
            //vWI_CRM_xts_discountsetup.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_discountsetup);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_discountsetup vWI_CRM_xts_discountsetup)
        {
            //vWI_CRM_xts_discountsetup.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_discountsetup.LastUpdateTime = DateTime.Now;
        }
    }
}