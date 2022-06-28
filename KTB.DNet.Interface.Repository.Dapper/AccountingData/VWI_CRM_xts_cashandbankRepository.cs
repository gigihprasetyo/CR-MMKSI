#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_cashandbank repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_cashandbank;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_cashandbankRepository : BaseDNetRepository<VWI_CRM_xts_cashandbank>, IVWI_CRM_xts_cashandbankRepository<VWI_CRM_xts_cashandbank, int>
    {
        #region Constructor
        public VWI_CRM_xts_cashandbankRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_cashandbank
        /// <summary>
        /// Create VWI_CRM_xts_cashandbank
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_cashandbank entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_cashandbank
        /// <summary>
        /// Update VWI_CRM_xts_cashandbank
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_cashandbank entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_cashandbank
        /// <summary>
        /// Delete VWI_CRM_xts_cashandbank
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_cashandbank By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_cashandbank Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_cashandbank>(
                        VWI_CRM_xts_cashandbankQuery.GetVWI_CRM_xts_cashandbankById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_cashandbank
        /// <summary>
        /// Get All VWI_CRM_xts_cashandbank
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_cashandbank> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_cashandbank
        public List<VWI_CRM_xts_cashandbank> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_cashandbank>();
        }
        #endregion

		#region Search VWI_CRM_xts_cashandbank        
        public new List<VWI_CRM_xts_cashandbank> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_cashandbank.rowstatus", "isnull(vwi_crm_xts_cashandbank.rowstatus, 0)");

                List<VWI_CRM_xts_cashandbank> result = SearchData<VWI_CRM_xts_cashandbank>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_cashandbank>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_cashandbankQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_cashandbank.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_cashandbankQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_cashandbank>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_cashandbank vWI_CRM_xts_cashandbank)
        {
            //vWI_CRM_xts_cashandbank.CreatedBy = UserLogin;
            //vWI_CRM_xts_cashandbank.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_cashandbank);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_cashandbank vWI_CRM_xts_cashandbank)
        {
            //vWI_CRM_xts_cashandbank.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_cashandbank.LastUpdateTime = DateTime.Now;
        }
    }
}