#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_locationRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 10:49:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_location;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_locationRepository : BaseDNetRepository<VWI_CRM_xts_location>, IVWI_CRM_xts_locationRepository<VWI_CRM_xts_location, int>
    {
        #region Constructor
        public VWI_CRM_xts_locationRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_location
        /// <summary>
        /// Create VWI_CRM_xts_location
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_location entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_location
        /// <summary>
        /// Update VWI_CRM_xts_location
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_location entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_location
        /// <summary>
        /// Delete VWI_CRM_xts_location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_location By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_location Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_location>(
                        VWI_CRM_xts_locationQuery.GetVWI_CRM_xts_locationById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_location
        /// <summary>
        /// Get All VWI_CRM_xts_location
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_location> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_location
        public List<VWI_CRM_xts_location> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_location>();
        }
        #endregion

        #region Search VWI_CRM_xts_location        
        public new List<VWI_CRM_xts_location> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_location.rowstatus", "isnull(vwi_crm_xts_location.rowstatus, 0)");

                List<VWI_CRM_xts_location> result = SearchData<VWI_CRM_xts_location>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_location>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_locationQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_location.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_locationQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_location>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_location vWI_CRM_xts_location)
        {
            //vWI_CRM_xts_location.CreatedBy = UserLogin;
            //vWI_CRM_xts_location.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_location);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_location vWI_CRM_xts_location)
        {
            //vWI_CRM_xts_location.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_location.LastUpdateTime = DateTime.Now;
        }
    }
}
