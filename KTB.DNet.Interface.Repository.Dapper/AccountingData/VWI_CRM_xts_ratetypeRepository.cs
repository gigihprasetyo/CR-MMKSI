#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_ratetype repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_ratetype;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_ratetypeRepository : BaseDNetRepository<VWI_CRM_xts_ratetype>, IVWI_CRM_xts_ratetypeRepository<VWI_CRM_xts_ratetype, int>
    {
        #region Constructor
        public VWI_CRM_xts_ratetypeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_ratetype
        /// <summary>
        /// Create VWI_CRM_xts_ratetype
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_ratetype entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_ratetype
        /// <summary>
        /// Update VWI_CRM_xts_ratetype
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_ratetype entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_ratetype
        /// <summary>
        /// Delete VWI_CRM_xts_ratetype
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_ratetype By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_ratetype Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_ratetype>(
                        VWI_CRM_xts_ratetypeQuery.GetVWI_CRM_xts_ratetypeById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_ratetype
        /// <summary>
        /// Get All VWI_CRM_xts_ratetype
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_ratetype> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_ratetype
        public List<VWI_CRM_xts_ratetype> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_ratetype>();
        }
        #endregion

        #region Search VWI_CRM_xts_ratetype        
        public new List<VWI_CRM_xts_ratetype> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_ratetype.rowstatus", "isnull(vwi_crm_xts_ratetype.rowstatus, 0)");

                List<VWI_CRM_xts_ratetype> result = SearchData<VWI_CRM_xts_ratetype>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_ratetype>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_ratetypeQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_ratetype.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_ratetypeQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_ratetype>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_ratetype vWI_CRM_xts_ratetype)
        {
            //vWI_CRM_xts_ratetype.CreatedBy = UserLogin;
            //vWI_CRM_xts_ratetype.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_ratetype);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_ratetype vWI_CRM_xts_ratetype)
        {
            //vWI_CRM_xts_ratetype.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_ratetype.LastUpdateTime = DateTime.Now;
        }
    }
}