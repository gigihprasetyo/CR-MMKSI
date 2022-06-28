#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_servicemms repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 09:55:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_servicemms;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_servicemmsRepository : BaseDNetRepository<VWI_CRM_xts_servicemms>, IVWI_CRM_xts_servicemmsRepository<VWI_CRM_xts_servicemms, int>
    {
        #region Constructor
        public VWI_CRM_xts_servicemmsRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_servicemms
        /// <summary>
        /// Create VWI_CRM_xts_servicemms
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_servicemms entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_servicemms
        /// <summary>
        /// Update VWI_CRM_xts_servicemms
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_servicemms entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_servicemms
        /// <summary>
        /// Delete VWI_CRM_xts_servicemms
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_servicemms By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_servicemms Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_servicemms>(
                        VWI_CRM_xts_servicemmsQuery.GetVWI_CRM_xts_servicemmsById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_servicemms
        /// <summary>
        /// Get All VWI_CRM_xts_servicemms
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_servicemms> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_servicemms
        public List<VWI_CRM_xts_servicemms> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_servicemms>();
        }
        #endregion

        #region Search VWI_CRM_xts_servicemms        
        public new List<VWI_CRM_xts_servicemms> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_servicemms.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_servicemms> result = SearchData<VWI_CRM_xts_servicemms>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_servicemms>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_servicemmsQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_servicemms.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_servicemmsQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_servicemms>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_servicemms vWI_CRM_xts_servicemms)
        {
            //vWI_CRM_xts_servicemms.CreatedBy = UserLogin;
            //vWI_CRM_xts_servicemms.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_servicemms);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_servicemms vWI_CRM_xts_servicemms)
        {
            //vWI_CRM_xts_servicemms.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_servicemms.LastUpdateTime = DateTime.Now;
        }
    }
}