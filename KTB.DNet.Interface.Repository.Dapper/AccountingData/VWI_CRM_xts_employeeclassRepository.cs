#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_employeeclassRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 10:00:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_employeeclass;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_employeeclassRepository : BaseDNetRepository<VWI_CRM_xts_employeeclass>, IVWI_CRM_xts_employeeclassRepository<VWI_CRM_xts_employeeclass, int>
    {
        #region Constructor
        public VWI_CRM_xts_employeeclassRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_employeeclass
        /// <summary>
        /// Create VWI_CRM_xts_employeeclass
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_employeeclass entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_employeeclass
        /// <summary>
        /// Update VWI_CRM_xts_employeeclass
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_employeeclass entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_employeeclass
        /// <summary>
        /// Delete VWI_CRM_xts_employeeclass
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_employeeclass By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_employeeclass Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_employeeclass>(
                        VWI_CRM_xts_employeeclassQuery.GetVWI_CRM_xts_employeeclassById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_employeeclass
        /// <summary>
        /// Get All VWI_CRM_xts_employeeclass
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_employeeclass> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_employeeclass
        public List<VWI_CRM_xts_employeeclass> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_employeeclass>();
        }
        #endregion

        #region Search VWI_CRM_xts_employeeclass        
        public new List<VWI_CRM_xts_employeeclass> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_employeeclass.rowstatus", "isnull(vwi_crm_xts_employeeclass.rowstatus, 0)");

                List<VWI_CRM_xts_employeeclass> result = SearchData<VWI_CRM_xts_employeeclass>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_employeeclass>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_employeeclassQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_employeeclass.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_employeeclassQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_employeeclass>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_employeeclass vWI_CRM_xts_employeeclass)
        {
            //vWI_CRM_xts_employeeclass.CreatedBy = UserLogin;
            //vWI_CRM_xts_employeeclass.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_employeeclass);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_employeeclass vWI_CRM_xts_employeeclass)
        {
            //vWI_CRM_xts_employeeclass.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_employeeclass.LastUpdateTime = DateTime.Now;
        }
    }
}
