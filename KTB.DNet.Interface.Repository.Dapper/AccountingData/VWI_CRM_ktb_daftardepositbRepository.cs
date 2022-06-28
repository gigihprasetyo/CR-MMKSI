#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_daftardepositb repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_daftardepositb;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_daftardepositbRepository : BaseDNetRepository<VWI_CRM_ktb_daftardepositb>, IVWI_CRM_ktb_daftardepositbRepository<VWI_CRM_ktb_daftardepositb, int>
    {
        #region Constructor
        public VWI_CRM_ktb_daftardepositbRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_daftardepositb
        /// <summary>
        /// Create VWI_CRM_ktb_daftardepositb
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_daftardepositb entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_daftardepositb
        /// <summary>
        /// Update VWI_CRM_ktb_daftardepositb
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_daftardepositb entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_daftardepositb
        /// <summary>
        /// Delete VWI_CRM_ktb_daftardepositb
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_daftardepositb By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_daftardepositb Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_daftardepositb>(
                        VWI_CRM_ktb_daftardepositbQuery.GetVWI_CRM_ktb_daftardepositbById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_daftardepositb
        /// <summary>
        /// Get All VWI_CRM_ktb_daftardepositb
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_daftardepositb> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_daftardepositb
        public List<VWI_CRM_ktb_daftardepositb> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_daftardepositb>();
        }
        #endregion

        #region Search VWI_CRM_ktb_daftardepositb        
        public new List<VWI_CRM_ktb_daftardepositb> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_daftardepositb.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_ktb_daftardepositb> result = SearchData<VWI_CRM_ktb_daftardepositb>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_daftardepositb>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_daftardepositbQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_daftardepositb.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_daftardepositbQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_daftardepositb>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_daftardepositb vWI_CRM_ktb_daftardepositb)
        {
            //vWI_CRM_ktb_daftardepositb.CreatedBy = UserLogin;
            //vWI_CRM_ktb_daftardepositb.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_daftardepositb);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_daftardepositb vWI_CRM_ktb_daftardepositb)
        {
            //vWI_CRM_ktb_daftardepositb.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_daftardepositb.LastUpdateTime = DateTime.Now;
        }
    }
}