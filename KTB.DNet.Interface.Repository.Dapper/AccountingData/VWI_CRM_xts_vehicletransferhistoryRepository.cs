#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehicletransferhistory repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_vehicletransferhistory;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_vehicletransferhistoryRepository : BaseDNetRepository<VWI_CRM_xts_vehicletransferhistory>, IVWI_CRM_xts_vehicletransferhistoryRepository<VWI_CRM_xts_vehicletransferhistory, int>
    {
        #region Constructor
        public VWI_CRM_xts_vehicletransferhistoryRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_vehicletransferhistory
        /// <summary>
        /// Create VWI_CRM_xts_vehicletransferhistory
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_vehicletransferhistory entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_vehicletransferhistory
        /// <summary>
        /// Update VWI_CRM_xts_vehicletransferhistory
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_vehicletransferhistory entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_vehicletransferhistory
        /// <summary>
        /// Delete VWI_CRM_xts_vehicletransferhistory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_vehicletransferhistory By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_vehicletransferhistory Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_vehicletransferhistory>(
                        VWI_CRM_xts_vehicletransferhistoryQuery.GetVWI_CRM_xts_vehicletransferhistoryById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_vehicletransferhistory
        /// <summary>
        /// Get All VWI_CRM_xts_vehicletransferhistory
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_vehicletransferhistory> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_vehicletransferhistory
        public List<VWI_CRM_xts_vehicletransferhistory> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_vehicletransferhistory>();
        }
        #endregion

		#region Search VWI_CRM_xts_vehicletransferhistory        
        public new List<VWI_CRM_xts_vehicletransferhistory> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_vehicletransferhistory.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_vehicletransferhistory> result = SearchData<VWI_CRM_xts_vehicletransferhistory>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_vehicletransferhistory>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_vehicletransferhistoryQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_vehicletransferhistory.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_vehicletransferhistoryQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_vehicletransferhistory>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_vehicletransferhistory vWI_CRM_xts_vehicletransferhistory)
        {
            //vWI_CRM_xts_vehicletransferhistory.CreatedBy = UserLogin;
            //vWI_CRM_xts_vehicletransferhistory.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_vehicletransferhistory);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_vehicletransferhistory vWI_CRM_xts_vehicletransferhistory)
        {
            //vWI_CRM_xts_vehicletransferhistory.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_vehicletransferhistory.LastUpdateTime = DateTime.Now;
        }
    }
}