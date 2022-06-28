#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_benefit repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_benefit;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_benefitRepository : BaseDNetRepository<VWI_CRM_ktb_benefit>, IVWI_CRM_ktb_benefitRepository<VWI_CRM_ktb_benefit, int>
    {
        #region Constructor
        public VWI_CRM_ktb_benefitRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_benefit
        /// <summary>
        /// Create VWI_CRM_ktb_benefit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_benefit entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_benefit
        /// <summary>
        /// Update VWI_CRM_ktb_benefit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_benefit entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_benefit
        /// <summary>
        /// Delete VWI_CRM_ktb_benefit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_benefit By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_benefit Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_benefit>(
                        VWI_CRM_ktb_benefitQuery.GetVWI_CRM_ktb_benefitById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_benefit
        /// <summary>
        /// Get All VWI_CRM_ktb_benefit
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_benefit> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_benefit
        public List<VWI_CRM_ktb_benefit> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_benefit>();
        }
        #endregion

        #region Search VWI_CRM_ktb_benefit        
        public new List<VWI_CRM_ktb_benefit> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_benefit.rowstatus", "isnull(vwi_crm_ktb_benefit.rowstatus, 0)");

                List<VWI_CRM_ktb_benefit> result = SearchData<VWI_CRM_ktb_benefit>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_benefit>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_benefitQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_benefit.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_benefitQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_benefit>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_benefit vWI_CRM_ktb_benefit)
        {
            //vWI_CRM_ktb_benefit.CreatedBy = UserLogin;
            //vWI_CRM_ktb_benefit.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_benefit);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_benefit vWI_CRM_ktb_benefit)
        {
            //vWI_CRM_ktb_benefit.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_benefit.LastUpdateTime = DateTime.Now;
        }
    }
}