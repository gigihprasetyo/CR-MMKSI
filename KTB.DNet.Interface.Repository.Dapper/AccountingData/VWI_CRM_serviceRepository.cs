#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_service repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-02-11 10:39:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_service;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_serviceRepository : BaseDNetRepository<VWI_CRM_service> //, IVWI_CRM_serviceRepository<VWI_CRM_service, int>
    {
        #region Constructor
        public VWI_CRM_serviceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_service
        /// <summary>
        /// Create VWI_CRM_service
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_service entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_service
        /// <summary>
        /// Update VWI_CRM_service
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_service entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_service
        /// <summary>
        /// Delete VWI_CRM_service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_service By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_service Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_service>(
                        VWI_CRM_serviceQuery.GetVWI_CRM_serviceById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_service
        /// <summary>
        /// Get All VWI_CRM_service
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_service> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_service
        public List<VWI_CRM_service> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_service>();
        }
        #endregion

        #region Search VWI_CRM_service        
        public new List<VWI_CRM_service> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_service.rowstatus", "isnull(vwi_crm_service.rowstatus, 0)");

                List<VWI_CRM_service> result = SearchData<VWI_CRM_service>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_service>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_serviceQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_service.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_serviceQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_service>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_service vWI_CRM_service)
        {
            //vWI_CRM_service.CreatedBy = UserLogin;
            //vWI_CRM_service.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_service);
        }

        protected void SetLastModifiedLog(VWI_CRM_service vWI_CRM_service)
        {
            //vWI_CRM_service.LastUpdateBy = UserLogin;
            //vWI_CRM_service.LastUpdateTime = DateTime.Now;
        }
    }
}