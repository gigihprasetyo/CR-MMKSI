#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_weighttax repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 15:38:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xjp_weighttax;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xjp_weighttaxRepository : BaseDNetRepository<VWI_CRM_xjp_weighttax>, IVWI_CRM_xjp_weighttaxRepository<VWI_CRM_xjp_weighttax, int>
    {
        #region Constructor
        public VWI_CRM_xjp_weighttaxRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xjp_weighttax
        /// <summary>
        /// Create VWI_CRM_xjp_weighttax
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xjp_weighttax entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xjp_weighttax
        /// <summary>
        /// Update VWI_CRM_xjp_weighttax
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xjp_weighttax entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xjp_weighttax
        /// <summary>
        /// Delete VWI_CRM_xjp_weighttax
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xjp_weighttax By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xjp_weighttax Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xjp_weighttax>(
                        VWI_CRM_xjp_weighttaxQuery.GetVWI_CRM_xjp_weighttaxById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xjp_weighttax
        /// <summary>
        /// Get All VWI_CRM_xjp_weighttax
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xjp_weighttax> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xjp_weighttax
        public List<VWI_CRM_xjp_weighttax> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xjp_weighttax>();
        }
        #endregion

        #region Search VWI_CRM_xjp_weighttax        
        public new List<VWI_CRM_xjp_weighttax> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xjp_weighttax.rowstatus", "isnull(vwi_crm_xjp_weighttax.rowstatus, 0)");

                List<VWI_CRM_xjp_weighttax> result = SearchData<VWI_CRM_xjp_weighttax>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xjp_weighttax>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xjp_weighttaxQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xjp_weighttax.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xjp_weighttaxQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xjp_weighttax>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xjp_weighttax vWI_CRM_xjp_weighttax)
        {
            //vWI_CRM_xjp_weighttax.CreatedBy = UserLogin;
            //vWI_CRM_xjp_weighttax.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xjp_weighttax);
        }

        protected void SetLastModifiedLog(VWI_CRM_xjp_weighttax vWI_CRM_xjp_weighttax)
        {
            //vWI_CRM_xjp_weighttax.LastUpdateBy = UserLogin;
            //vWI_CRM_xjp_weighttax.LastUpdateTime = DateTime.Now;
        }
    }
}