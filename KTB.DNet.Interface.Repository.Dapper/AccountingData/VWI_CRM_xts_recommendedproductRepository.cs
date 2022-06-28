#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_recommendedproduct repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 16:42:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_recommendedproduct;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_recommendedproductRepository : BaseDNetRepository<VWI_CRM_xts_recommendedproduct>, IVWI_CRM_xts_recommendedproductRepository<VWI_CRM_xts_recommendedproduct, int>
    {
        #region Constructor
        public VWI_CRM_xts_recommendedproductRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_recommendedproduct
        /// <summary>
        /// Create VWI_CRM_xts_recommendedproduct
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_recommendedproduct entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_recommendedproduct
        /// <summary>
        /// Update VWI_CRM_xts_recommendedproduct
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_recommendedproduct entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_recommendedproduct
        /// <summary>
        /// Delete VWI_CRM_xts_recommendedproduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_recommendedproduct By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_recommendedproduct Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_recommendedproduct>(
                        VWI_CRM_xts_recommendedproductQuery.GetVWI_CRM_xts_recommendedproductById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_recommendedproduct
        /// <summary>
        /// Get All VWI_CRM_xts_recommendedproduct
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_recommendedproduct> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_recommendedproduct
        public List<VWI_CRM_xts_recommendedproduct> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_recommendedproduct>();
        }
        #endregion

        #region Search VWI_CRM_xts_recommendedproduct        
        public new List<VWI_CRM_xts_recommendedproduct> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_recommendedproduct.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_recommendedproduct> result = SearchData<VWI_CRM_xts_recommendedproduct>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_recommendedproduct>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_recommendedproductQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_recommendedproduct.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_recommendedproductQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_recommendedproduct>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_recommendedproduct vWI_CRM_xts_recommendedproduct)
        {
            //vWI_CRM_xts_recommendedproduct.CreatedBy = UserLogin;
            //vWI_CRM_xts_recommendedproduct.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_recommendedproduct);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_recommendedproduct vWI_CRM_xts_recommendedproduct)
        {
            //vWI_CRM_xts_recommendedproduct.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_recommendedproduct.LastUpdateTime = DateTime.Now;
        }
    }
}