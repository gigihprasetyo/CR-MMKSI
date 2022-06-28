#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_karoseri repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_karoseri;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_karoseriRepository : BaseDNetRepository<VWI_CRM_ktb_karoseri>, IVWI_CRM_ktb_karoseriRepository<VWI_CRM_ktb_karoseri, int>
    {
        #region Constructor
        public VWI_CRM_ktb_karoseriRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_karoseri
        /// <summary>
        /// Create VWI_CRM_ktb_karoseri
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_karoseri entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_karoseri
        /// <summary>
        /// Update VWI_CRM_ktb_karoseri
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_karoseri entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_karoseri
        /// <summary>
        /// Delete VWI_CRM_ktb_karoseri
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_karoseri By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_karoseri Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_karoseri>(
                        VWI_CRM_ktb_karoseriQuery.GetVWI_CRM_ktb_karoseriById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_karoseri
        /// <summary>
        /// Get All VWI_CRM_ktb_karoseri
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_karoseri> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_karoseri
        public List<VWI_CRM_ktb_karoseri> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_karoseri>();
        }
        #endregion

        #region Search VWI_CRM_ktb_karoseri        
        public new List<VWI_CRM_ktb_karoseri> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_karoseri.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_ktb_karoseri> result = SearchData<VWI_CRM_ktb_karoseri>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_karoseri>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_karoseriQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_karoseri.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_karoseriQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_karoseri>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_karoseri vWI_CRM_ktb_karoseri)
        {
            //vWI_CRM_ktb_karoseri.CreatedBy = UserLogin;
            //vWI_CRM_ktb_karoseri.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_karoseri);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_karoseri vWI_CRM_ktb_karoseri)
        {
            //vWI_CRM_ktb_karoseri.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_karoseri.LastUpdateTime = DateTime.Now;
        }
    }
}