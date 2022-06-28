﻿#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_dimension4Repository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 09:46:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_dimension4;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_dimension4Repository : BaseDNetRepository<VWI_CRM_xts_dimension4>, IVWI_CRM_xts_dimension4Repository<VWI_CRM_xts_dimension4, int>
    {
        #region Constructor
        public VWI_CRM_xts_dimension4Repository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_dimension4
        /// <summary>
        /// Create VWI_CRM_xts_dimension4
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_dimension4 entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_dimension4
        /// <summary>
        /// Update VWI_CRM_xts_dimension4
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_dimension4 entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_dimension4
        /// <summary>
        /// Delete VWI_CRM_xts_dimension4
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_dimension4 By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_dimension4 Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_dimension4>(
                        VWI_CRM_xts_dimension4Query.GetVWI_CRM_xts_dimension4ById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_dimension4
        /// <summary>
        /// Get All VWI_CRM_xts_dimension4
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_dimension4> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_dimension4
        public List<VWI_CRM_xts_dimension4> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_dimension4>();
        }
        #endregion

        #region Search VWI_CRM_xts_dimension4        
        public new List<VWI_CRM_xts_dimension4> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_dimension4.rowstatus", "isnull(vwi_crm_xts_dimension4.rowstatus, 0)");

                List<VWI_CRM_xts_dimension4> result = SearchData<VWI_CRM_xts_dimension4>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_dimension4>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_dimension4Query.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_dimension4.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_dimension4Query.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_dimension4>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_dimension4 vWI_CRM_xts_dimension4)
        {
            //vWI_CRM_xts_dimension4.CreatedBy = UserLogin;
            //vWI_CRM_xts_dimension4.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_dimension4);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_dimension4 vWI_CRM_xts_dimension4)
        {
            //vWI_CRM_xts_dimension4.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_dimension4.LastUpdateTime = DateTime.Now;
        }
    }
}
