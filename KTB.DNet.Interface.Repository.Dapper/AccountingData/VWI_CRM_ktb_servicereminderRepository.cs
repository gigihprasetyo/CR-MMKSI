#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// PURPOSE       : VWI_CRM_ktb_servicereminder  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 15/03/2022 10:02:47
//
// ===========================================================================	
#endregion


#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_servicereminder;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_servicereminderRepository : BaseDNetRepository<VWI_CRM_ktb_servicereminder>, IVWI_CRM_ktb_servicereminderRepository<VWI_CRM_ktb_servicereminder, int>
    {
        #region Constructor
        public VWI_CRM_ktb_servicereminderRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_servicereminder
        /// <summary>
        /// Create VWI_CRM_ktb_servicereminder
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_servicereminder entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_servicereminder
        /// <summary>
        /// Update VWI_CRM_ktb_servicereminder
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_servicereminder entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_servicereminder
        /// <summary>
        /// Delete VWI_CRM_ktb_servicereminder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_servicereminder By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_servicereminder Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_servicereminder>(
                        VWI_CRM_ktb_servicereminderQuery.GetVWI_CRM_ktb_servicereminderById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_servicereminder
        /// <summary>
        /// Get All VWI_CRM_ktb_servicereminder
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_servicereminder> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_servicereminder
        public List<VWI_CRM_ktb_servicereminder> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_servicereminder>();
        }
        #endregion

        #region Search VWI_CRM_ktb_servicereminder        
        public new List<VWI_CRM_ktb_servicereminder> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_servicereminder.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_ktb_servicereminder> result = SearchFetchPaging<VWI_CRM_ktb_servicereminder>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_servicereminder>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_servicereminderQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "CRM_ktb_servicereminder.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_servicereminderQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_servicereminder>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_servicereminder VWI_CRM_ktb_servicereminder)
        {
            //VWI_CRM_ktb_servicereminder.CreatedBy = UserLogin;
            //VWI_CRM_ktb_servicereminder.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_CRM_ktb_servicereminder);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_servicereminder VWI_CRM_ktb_servicereminder)
        {
            //VWI_CRM_ktb_servicereminder.LastUpdateBy = UserLogin;
            //VWI_CRM_ktb_servicereminder.LastUpdateTime = DateTime.Now;
        }
    }
}