﻿#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_dimension1Repository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 08:41:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_dimension1;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_dimension1Repository : BaseDNetRepository<VWI_CRM_xts_dimension1>, IVWI_CRM_xts_dimension1Repository<VWI_CRM_xts_dimension1, int>
    {
        #region Constructor
        public VWI_CRM_xts_dimension1Repository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_dimension1
        /// <summary>
        /// Create VWI_CRM_xts_dimension1
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_dimension1 entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_dimension1
        /// <summary>
        /// Update VWI_CRM_xts_dimension1
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_dimension1 entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_dimension1
        /// <summary>
        /// Delete VWI_CRM_xts_dimension1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_dimension1 By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_dimension1 Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_dimension1>(
                        VWI_CRM_xts_dimension1Query.GetVWI_CRM_xts_dimension1ById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_dimension1
        /// <summary>
        /// Get All VWI_CRM_xts_dimension1
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_dimension1> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_dimension1
        public List<VWI_CRM_xts_dimension1> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_dimension1>();
        }
        #endregion

        #region Search VWI_CRM_xts_dimension1        
        public new List<VWI_CRM_xts_dimension1> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_dimension1.rowstatus", "isnull(vwi_crm_xts_dimension1.rowstatus, 0)");

                List<VWI_CRM_xts_dimension1> result = SearchData<VWI_CRM_xts_dimension1>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_dimension1>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_dimension1Query.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_dimension1.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_dimension1Query.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_dimension1>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_dimension1 vWI_CRM_xts_dimension1)
        {
            //vWI_CRM_xts_dimension1.CreatedBy = UserLogin;
            //vWI_CRM_xts_dimension1.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_dimension1);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_dimension1 vWI_CRM_xts_dimension1)
        {
            //vWI_CRM_xts_dimension1.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_dimension1.LastUpdateTime = DateTime.Now;
        }
    }
}
