#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicetemplateactivity repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_servicetemplateactivity;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_servicetemplateactivityRepository : BaseDNetRepository<VWI_CRM_xts_servicetemplateactivity>, IVWI_CRM_xts_servicetemplateactivityRepository<VWI_CRM_xts_servicetemplateactivity, int>
    {
        #region Constructor
        public VWI_CRM_xts_servicetemplateactivityRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_servicetemplateactivity
        /// <summary>
        /// Create VWI_CRM_xts_servicetemplateactivity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_servicetemplateactivity entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_servicetemplateactivity
        /// <summary>
        /// Update VWI_CRM_xts_servicetemplateactivity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_servicetemplateactivity entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_servicetemplateactivity
        /// <summary>
        /// Delete VWI_CRM_xts_servicetemplateactivity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_servicetemplateactivity By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_servicetemplateactivity Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_servicetemplateactivity>(
                        VWI_CRM_xts_servicetemplateactivityQuery.GetVWI_CRM_xts_servicetemplateactivityById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_servicetemplateactivity
        /// <summary>
        /// Get All VWI_CRM_xts_servicetemplateactivity
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_servicetemplateactivity> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_servicetemplateactivity
        public List<VWI_CRM_xts_servicetemplateactivity> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_servicetemplateactivity>();
        }
        #endregion

        #region Search VWI_CRM_xts_servicetemplateactivity        
        public new List<VWI_CRM_xts_servicetemplateactivity> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_servicetemplateactivity.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_servicetemplateactivity> result = SearchData<VWI_CRM_xts_servicetemplateactivity>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_servicetemplateactivity>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_servicetemplateactivityQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_servicetemplateactivity.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_servicetemplateactivityQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_servicetemplateactivity>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_servicetemplateactivity vWI_CRM_xts_servicetemplateactivity)
        {
            //vWI_CRM_xts_servicetemplateactivity.CreatedBy = UserLogin;
            //vWI_CRM_xts_servicetemplateactivity.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_servicetemplateactivity);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_servicetemplateactivity vWI_CRM_xts_servicetemplateactivity)
        {
            //vWI_CRM_xts_servicetemplateactivity.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_servicetemplateactivity.LastUpdateTime = DateTime.Now;
        }
    }
}