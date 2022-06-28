#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_standardplate repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 08:56:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xjp_standardplate;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xjp_standardplateRepository : BaseDNetRepository<VWI_CRM_xjp_standardplate>, IVWI_CRM_xjp_standardplateRepository<VWI_CRM_xjp_standardplate, int>
    {
        #region Constructor
        public VWI_CRM_xjp_standardplateRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xjp_standardplate
        /// <summary>
        /// Create VWI_CRM_xjp_standardplate
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xjp_standardplate entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xjp_standardplate
        /// <summary>
        /// Update VWI_CRM_xjp_standardplate
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xjp_standardplate entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xjp_standardplate
        /// <summary>
        /// Delete VWI_CRM_xjp_standardplate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xjp_standardplate By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xjp_standardplate Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xjp_standardplate>(
                        VWI_CRM_xjp_standardplateQuery.GetVWI_CRM_xjp_standardplateById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xjp_standardplate
        /// <summary>
        /// Get All VWI_CRM_xjp_standardplate
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xjp_standardplate> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xjp_standardplate
        public List<VWI_CRM_xjp_standardplate> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xjp_standardplate>();
        }
        #endregion

        #region Search VWI_CRM_xjp_standardplate        
        public new List<VWI_CRM_xjp_standardplate> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xjp_standardplate.rowstatus", "isnull(vwi_crm_xjp_standardplate.rowstatus, 0)");

                List<VWI_CRM_xjp_standardplate> result = SearchData<VWI_CRM_xjp_standardplate>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xjp_standardplate>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xjp_standardplateQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xjp_standardplate.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xjp_standardplateQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xjp_standardplate>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xjp_standardplate vWI_CRM_xjp_standardplate)
        {
            //vWI_CRM_xjp_standardplate.CreatedBy = UserLogin;
            //vWI_CRM_xjp_standardplate.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xjp_standardplate);
        }

        protected void SetLastModifiedLog(VWI_CRM_xjp_standardplate vWI_CRM_xjp_standardplate)
        {
            //vWI_CRM_xjp_standardplate.LastUpdateBy = UserLogin;
            //vWI_CRM_xjp_standardplate.LastUpdateTime = DateTime.Now;
        }
    }
}