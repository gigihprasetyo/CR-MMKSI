#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehicletransactionhistory repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_vehicletransactionhistory;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_vehicletransactionhistoryRepository : BaseDNetRepository<VWI_CRM_xts_vehicletransactionhistory>, IVWI_CRM_xts_vehicletransactionhistoryRepository<VWI_CRM_xts_vehicletransactionhistory, int>
    {
        #region Constructor
        public VWI_CRM_xts_vehicletransactionhistoryRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_vehicletransactionhistory
        /// <summary>
        /// Create VWI_CRM_xts_vehicletransactionhistory
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_vehicletransactionhistory entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_vehicletransactionhistory
        /// <summary>
        /// Update VWI_CRM_xts_vehicletransactionhistory
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_vehicletransactionhistory entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_vehicletransactionhistory
        /// <summary>
        /// Delete VWI_CRM_xts_vehicletransactionhistory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_vehicletransactionhistory By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_vehicletransactionhistory Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_vehicletransactionhistory>(
                        VWI_CRM_xts_vehicletransactionhistoryQuery.GetVWI_CRM_xts_vehicletransactionhistoryById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_vehicletransactionhistory
        /// <summary>
        /// Get All VWI_CRM_xts_vehicletransactionhistory
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_vehicletransactionhistory> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_vehicletransactionhistory
        public List<VWI_CRM_xts_vehicletransactionhistory> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_vehicletransactionhistory>();
        }
        #endregion

		#region Search VWI_CRM_xts_vehicletransactionhistory        
        public new List<VWI_CRM_xts_vehicletransactionhistory> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_vehicletransactionhistory.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_vehicletransactionhistory> result = SearchData<VWI_CRM_xts_vehicletransactionhistory>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_vehicletransactionhistory>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_vehicletransactionhistoryQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_vehicletransactionhistory.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_vehicletransactionhistoryQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_vehicletransactionhistory>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_vehicletransactionhistory vWI_CRM_xts_vehicletransactionhistory)
        {
            //vWI_CRM_xts_vehicletransactionhistory.CreatedBy = UserLogin;
            //vWI_CRM_xts_vehicletransactionhistory.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_vehicletransactionhistory);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_vehicletransactionhistory vWI_CRM_xts_vehicletransactionhistory)
        {
            //vWI_CRM_xts_vehicletransactionhistory.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_vehicletransactionhistory.LastUpdateTime = DateTime.Now;
        }
    }
}