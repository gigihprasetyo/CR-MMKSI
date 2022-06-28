#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xid_registrationmonitoring repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xid_registrationmonitoring;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xid_registrationmonitoringRepository : BaseDNetRepository<VWI_CRM_xid_registrationmonitoring>, IVWI_CRM_xid_registrationmonitoringRepository<VWI_CRM_xid_registrationmonitoring, int>
    {
        #region Constructor
        public VWI_CRM_xid_registrationmonitoringRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xid_registrationmonitoring
        /// <summary>
        /// Create VWI_CRM_xid_registrationmonitoring
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xid_registrationmonitoring entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xid_registrationmonitoring
        /// <summary>
        /// Update VWI_CRM_xid_registrationmonitoring
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xid_registrationmonitoring entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xid_registrationmonitoring
        /// <summary>
        /// Delete VWI_CRM_xid_registrationmonitoring
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xid_registrationmonitoring By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xid_registrationmonitoring Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xid_registrationmonitoring>(
                        VWI_CRM_xid_registrationmonitoringQuery.GetVWI_CRM_xid_registrationmonitoringById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xid_registrationmonitoring
        /// <summary>
        /// Get All VWI_CRM_xid_registrationmonitoring
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xid_registrationmonitoring> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xid_registrationmonitoring
        public List<VWI_CRM_xid_registrationmonitoring> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xid_registrationmonitoring>();
        }
        #endregion

		#region Search VWI_CRM_xid_registrationmonitoring        
        public new List<VWI_CRM_xid_registrationmonitoring> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xid_registrationmonitoring.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xid_registrationmonitoring> result = SearchData<VWI_CRM_xid_registrationmonitoring>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xid_registrationmonitoring>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xid_registrationmonitoringQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xid_registrationmonitoring.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xid_registrationmonitoringQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xid_registrationmonitoring>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xid_registrationmonitoring vWI_CRM_xid_registrationmonitoring)
        {
            //vWI_CRM_xid_registrationmonitoring.CreatedBy = UserLogin;
            //vWI_CRM_xid_registrationmonitoring.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xid_registrationmonitoring);
        }

        protected void SetLastModifiedLog(VWI_CRM_xid_registrationmonitoring vWI_CRM_xid_registrationmonitoring)
        {
            //vWI_CRM_xid_registrationmonitoring.LastUpdateBy = UserLogin;
            //vWI_CRM_xid_registrationmonitoring.LastUpdateTime = DateTime.Now;
        }
    }
}