#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_department repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 09:38:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_department;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_departmentRepository : BaseDNetRepository<VWI_CRM_xts_department>, IVWI_CRM_xts_departmentRepository<VWI_CRM_xts_department, int>
    {
        #region Constructor
        public VWI_CRM_xts_departmentRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_department
        /// <summary>
        /// Create VWI_CRM_xts_department
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_department entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_department
        /// <summary>
        /// Update VWI_CRM_xts_department
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_department entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_department
        /// <summary>
        /// Delete VWI_CRM_xts_department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_department By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_department Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_department>(
                        VWI_CRM_xts_departmentQuery.GetVWI_CRM_xts_departmentById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_department
        /// <summary>
        /// Get All VWI_CRM_xts_department
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_department> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_department
        public List<VWI_CRM_xts_department> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_department>();
        }
        #endregion

        #region Search VWI_CRM_xts_department        
        public new List<VWI_CRM_xts_department> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_department.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_department> result = SearchData<VWI_CRM_xts_department>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_department>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_departmentQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_department.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_departmentQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_department>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_department vWI_CRM_xts_department)
        {
            //vWI_CRM_xts_department.CreatedBy = UserLogin;
            //vWI_CRM_xts_department.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_department);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_department vWI_CRM_xts_department)
        {
            //vWI_CRM_xts_department.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_department.LastUpdateTime = DateTime.Now;
        }
    }
}