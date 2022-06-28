#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_villageandstreetRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 08:51:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_villageandstreet;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_villageandstreetRepository : BaseDNetRepository<VWI_CRM_xts_villageandstreet>, IVWI_CRM_xts_villageandstreetRepository<VWI_CRM_xts_villageandstreet, int>
    {
        #region Constructor
        public VWI_CRM_xts_villageandstreetRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_villageandstreet
        /// <summary>
        /// Create VWI_CRM_xts_villageandstreet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_villageandstreet entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_villageandstreet
        /// <summary>
        /// Update VWI_CRM_xts_villageandstreet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_villageandstreet entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_villageandstreet
        /// <summary>
        /// Delete VWI_CRM_xts_villageandstreet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_villageandstreet By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_villageandstreet Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_villageandstreet>(
                        VWI_CRM_xts_villageandstreetQuery.GetVWI_CRM_xts_villageandstreetById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_villageandstreet
        /// <summary>
        /// Get All VWI_CRM_xts_villageandstreet
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_villageandstreet> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_villageandstreet
        public List<VWI_CRM_xts_villageandstreet> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_villageandstreet>();
        }
        #endregion

        #region Search VWI_CRM_xts_villageandstreet        
        public new List<VWI_CRM_xts_villageandstreet> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_villageandstreet.rowstatus", "isnull(vwi_crm_xts_villageandstreet.rowstatus, 0)");

                List<VWI_CRM_xts_villageandstreet> result = SearchData<VWI_CRM_xts_villageandstreet>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_villageandstreet>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_villageandstreetQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_villageandstreet.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_villageandstreetQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_villageandstreet>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_villageandstreet vWI_CRM_xts_villageandstreet)
        {
            //vWI_CRM_xts_villageandstreet.CreatedBy = UserLogin;
            //vWI_CRM_xts_villageandstreet.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_villageandstreet);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_villageandstreet vWI_CRM_xts_villageandstreet)
        {
            //vWI_CRM_xts_villageandstreet.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_villageandstreet.LastUpdateTime = DateTime.Now;
        }
    }
}
