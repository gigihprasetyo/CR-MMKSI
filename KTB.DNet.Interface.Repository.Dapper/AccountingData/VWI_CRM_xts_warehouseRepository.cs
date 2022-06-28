#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_warehouse repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_warehouse;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_warehouseRepository : BaseDNetRepository<VWI_CRM_xts_warehouse>, IVWI_CRM_xts_warehouseRepository<VWI_CRM_xts_warehouse, int>
    {
        #region Constructor
        public VWI_CRM_xts_warehouseRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_warehouse
        /// <summary>
        /// Create VWI_CRM_xts_warehouse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_warehouse entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_warehouse
        /// <summary>
        /// Update VWI_CRM_xts_warehouse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_warehouse entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_warehouse
        /// <summary>
        /// Delete VWI_CRM_xts_warehouse
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_warehouse By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_warehouse Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_warehouse>(
                        VWI_CRM_xts_warehouseQuery.GetVWI_CRM_xts_warehouseById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_warehouse
        /// <summary>
        /// Get All VWI_CRM_xts_warehouse
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_warehouse> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_warehouse
        public List<VWI_CRM_xts_warehouse> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_warehouse>();
        }
        #endregion

		#region Search VWI_CRM_xts_warehouse        
        public new List<VWI_CRM_xts_warehouse> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_warehouse.rowstatus", "isnull(vwi_crm_xts_warehouse.rowstatus, 0)");

                List<VWI_CRM_xts_warehouse> result = SearchData<VWI_CRM_xts_warehouse>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_warehouse>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_warehouseQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_warehouse.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_warehouseQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_warehouse>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_warehouse vWI_CRM_xts_warehouse)
        {
            //vWI_CRM_xts_warehouse.CreatedBy = UserLogin;
            //vWI_CRM_xts_warehouse.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_warehouse);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_warehouse vWI_CRM_xts_warehouse)
        {
            //vWI_CRM_xts_warehouse.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_warehouse.LastUpdateTime = DateTime.Now;
        }
    }
}