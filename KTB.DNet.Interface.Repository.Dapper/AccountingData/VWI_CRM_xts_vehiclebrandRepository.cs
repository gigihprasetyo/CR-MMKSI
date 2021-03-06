#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehiclebrandRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2020 09:23:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_vehiclebrand;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_vehiclebrandRepository : BaseDNetRepository<VWI_CRM_xts_vehiclebrand>, IVWI_CRM_xts_vehiclebrandRepository<VWI_CRM_xts_vehiclebrand, int>
    {
        #region Constructor
        public VWI_CRM_xts_vehiclebrandRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_vehiclebrand
        /// <summary>
        /// Create VWI_CRM_xts_vehiclebrand
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_vehiclebrand entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_vehiclebrand
        /// <summary>
        /// Update VWI_CRM_xts_vehiclebrand
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_vehiclebrand entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_vehiclebrand
        /// <summary>
        /// Delete VWI_CRM_xts_vehiclebrand
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_vehiclebrand By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_vehiclebrand Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_vehiclebrand>(
                        VWI_CRM_xts_vehiclebrandQuery.GetVWI_CRM_xts_vehiclebrandById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_vehiclebrand
        /// <summary>
        /// Get All VWI_CRM_xts_vehiclebrand
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_vehiclebrand> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_vehiclebrand
        public List<VWI_CRM_xts_vehiclebrand> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_vehiclebrand>();
        }
        #endregion

        #region Search VWI_CRM_xts_vehiclebrand        
        public new List<VWI_CRM_xts_vehiclebrand> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_vehiclebrand.rowstatus", "isnull(vwi_crm_xts_vehiclebrand.rowstatus, 0)");

                List<VWI_CRM_xts_vehiclebrand> result = SearchData<VWI_CRM_xts_vehiclebrand>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_vehiclebrand>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_vehiclebrandQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_vehiclebrand.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_vehiclebrandQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_vehiclebrand>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_vehiclebrand vWI_CRM_xts_vehiclebrand)
        {
            //vWI_CRM_xts_vehiclebrand.CreatedBy = UserLogin;
            //vWI_CRM_xts_vehiclebrand.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_vehiclebrand);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_vehiclebrand vWI_CRM_xts_vehiclebrand)
        {
            //vWI_CRM_xts_vehiclebrand.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_vehiclebrand.LastUpdateTime = DateTime.Now;
        }
    }
}
