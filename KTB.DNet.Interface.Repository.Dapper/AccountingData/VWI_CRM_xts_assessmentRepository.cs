#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_assessment repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 15:05:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_assessment;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_assessmentRepository : BaseDNetRepository<VWI_CRM_xts_assessment>, IVWI_CRM_xts_assessmentRepository<VWI_CRM_xts_assessment, int>
    {
        #region Constructor
        public VWI_CRM_xts_assessmentRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_assessment
        /// <summary>
        /// Create VWI_CRM_xts_assessment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_assessment entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_assessment
        /// <summary>
        /// Update VWI_CRM_xts_assessment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_assessment entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_assessment
        /// <summary>
        /// Delete VWI_CRM_xts_assessment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_assessment By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_assessment Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_assessment>(
                        VWI_CRM_xts_assessmentQuery.GetVWI_CRM_xts_assessmentById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_assessment
        /// <summary>
        /// Get All VWI_CRM_xts_assessment
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_assessment> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_assessment
        public List<VWI_CRM_xts_assessment> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_assessment>();
        }
        #endregion

        #region Search VWI_CRM_xts_assessment        
        public new List<VWI_CRM_xts_assessment> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_assessment.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_assessment> result = SearchData<VWI_CRM_xts_assessment>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_assessment>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_assessmentQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_assessment.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_assessmentQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_assessment>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_assessment vWI_CRM_xts_assessment)
        {
            //vWI_CRM_xts_assessment.CreatedBy = UserLogin;
            //vWI_CRM_xts_assessment.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_assessment);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_assessment vWI_CRM_xts_assessment)
        {
            //vWI_CRM_xts_assessment.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_assessment.LastUpdateTime = DateTime.Now;
        }
    }
}