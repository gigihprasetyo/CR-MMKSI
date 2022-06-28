#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_consumptiontax repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_consumptiontax;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_consumptiontaxRepository : BaseDNetRepository<VWI_CRM_xts_consumptiontax>, IVWI_CRM_xts_consumptiontaxRepository<VWI_CRM_xts_consumptiontax, int>
    {
        #region Constructor
        public VWI_CRM_xts_consumptiontaxRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_consumptiontax
        /// <summary>
        /// Create VWI_CRM_xts_consumptiontax
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_consumptiontax entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_consumptiontax
        /// <summary>
        /// Update VWI_CRM_xts_consumptiontax
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_consumptiontax entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_consumptiontax
        /// <summary>
        /// Delete VWI_CRM_xts_consumptiontax
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_consumptiontax By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_consumptiontax Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_consumptiontax>(
                        VWI_CRM_xts_consumptiontaxQuery.GetVWI_CRM_xts_consumptiontaxById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_consumptiontax
        /// <summary>
        /// Get All VWI_CRM_xts_consumptiontax
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_consumptiontax> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_consumptiontax
        public List<VWI_CRM_xts_consumptiontax> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_consumptiontax>();
        }
        #endregion

		#region Search VWI_CRM_xts_consumptiontax        
        public new List<VWI_CRM_xts_consumptiontax> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_consumptiontax.rowstatus", "isnull(vwi_crm_xts_consumptiontax.rowstatus, 0)");

                List<VWI_CRM_xts_consumptiontax> result = SearchData<VWI_CRM_xts_consumptiontax>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_consumptiontax>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_consumptiontaxQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_consumptiontax.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_consumptiontaxQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_consumptiontax>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_consumptiontax vWI_CRM_xts_consumptiontax)
        {
            //vWI_CRM_xts_consumptiontax.CreatedBy = UserLogin;
            //vWI_CRM_xts_consumptiontax.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_consumptiontax);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_consumptiontax vWI_CRM_xts_consumptiontax)
        {
            //vWI_CRM_xts_consumptiontax.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_consumptiontax.LastUpdateTime = DateTime.Now;
        }
    }
}