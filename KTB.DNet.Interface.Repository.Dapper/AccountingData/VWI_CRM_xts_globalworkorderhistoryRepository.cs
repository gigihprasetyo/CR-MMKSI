#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_globalworkorderhistory repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2020 10:42:01
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_globalworkorderhistory;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_globalworkorderhistoryRepository : BaseDNetRepository<VWI_CRM_xts_globalworkorderhistory>, IVWI_CRM_xts_globalworkorderhistoryRepository<VWI_CRM_xts_globalworkorderhistory, int>
    {
        #region Constructor
        public VWI_CRM_xts_globalworkorderhistoryRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_globalworkorderhistory
        /// <summary>
        /// Create VWI_CRM_xts_globalworkorderhistory
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_globalworkorderhistory entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_globalworkorderhistory
        /// <summary>
        /// Update VWI_CRM_xts_globalworkorderhistory
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_globalworkorderhistory entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_globalworkorderhistory
        /// <summary>
        /// Delete VWI_CRM_xts_globalworkorderhistory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_globalworkorderhistory By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_globalworkorderhistory Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_globalworkorderhistory>(
                        VWI_CRM_xts_globalworkorderhistoryQuery.GetVWI_CRM_xts_globalworkorderhistoryById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_globalworkorderhistory
        /// <summary>
        /// Get All VWI_CRM_xts_globalworkorderhistory
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_globalworkorderhistory> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_globalworkorderhistory
        public List<VWI_CRM_xts_globalworkorderhistory> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_globalworkorderhistory>();
        }
        #endregion

		#region Search VWI_CRM_xts_globalworkorderhistory        
        public new List<VWI_CRM_xts_globalworkorderhistory> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_globalworkorderhistory.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_globalworkorderhistory> result = SearchData<VWI_CRM_xts_globalworkorderhistory>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_globalworkorderhistory>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_globalworkorderhistoryQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_globalworkorderhistory.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_globalworkorderhistoryQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_globalworkorderhistory>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_globalworkorderhistory vWI_CRM_xts_globalworkorderhistory)
        {
            //vWI_CRM_xts_globalworkorderhistory.CreatedBy = UserLogin;
            //vWI_CRM_xts_globalworkorderhistory.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_globalworkorderhistory);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_globalworkorderhistory vWI_CRM_xts_globalworkorderhistory)
        {
            //vWI_CRM_xts_globalworkorderhistory.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_globalworkorderhistory.LastUpdateTime = DateTime.Now;
        }
    }
}