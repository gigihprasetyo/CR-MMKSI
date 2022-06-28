#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_cityRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/15/2020 11:28:00 AM
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_city;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_cityRepository : BaseDNetRepository<VWI_CRM_xts_city>, IVWI_CRM_xts_cityRepository<VWI_CRM_xts_city, int>
    {
        #region Constructor
        public VWI_CRM_xts_cityRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_city
        /// <summary>
        /// Create VWI_CRM_xts_city
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_city entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_city
        /// <summary>
        /// Update VWI_CRM_xts_city
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_city entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_city
        /// <summary>
        /// Delete VWI_CRM_xts_city
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_city By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_city Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_city>(
                        VWI_CRM_xts_cityQuery.GetVWI_CRM_xts_cityById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_city
        /// <summary>
        /// Get All VWI_CRM_xts_city
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_city> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_city
        public List<VWI_CRM_xts_city> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_city>();
        }
        #endregion

        #region Search VWI_CRM_xts_city        
        public new List<VWI_CRM_xts_city> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_city.rowstatus", "isnull(vwi_crm_xts_city.rowstatus, 0)");

                List<VWI_CRM_xts_city> result = SearchData<VWI_CRM_xts_city>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_city>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_cityQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_city.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_cityQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_city>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_city vWI_CRM_xts_city)
        {
            //vWI_CRM_xts_city.CreatedBy = UserLogin;
            //vWI_CRM_xts_city.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_city);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_city vWI_CRM_xts_city)
        {
            //vWI_CRM_xts_city.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_city.LastUpdateTime = DateTime.Now;
        }
    }
}
