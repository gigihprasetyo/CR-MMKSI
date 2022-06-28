#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productinteriorcolor repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_productinteriorcolor;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_productinteriorcolorRepository : BaseDNetRepository<VWI_CRM_xts_productinteriorcolor>, IVWI_CRM_xts_productinteriorcolorRepository<VWI_CRM_xts_productinteriorcolor, int>
    {
        #region Constructor
        public VWI_CRM_xts_productinteriorcolorRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_productinteriorcolor
        /// <summary>
        /// Create VWI_CRM_xts_productinteriorcolor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_productinteriorcolor entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_productinteriorcolor
        /// <summary>
        /// Update VWI_CRM_xts_productinteriorcolor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_productinteriorcolor entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_productinteriorcolor
        /// <summary>
        /// Delete VWI_CRM_xts_productinteriorcolor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_productinteriorcolor By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_productinteriorcolor Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_productinteriorcolor>(
                        VWI_CRM_xts_productinteriorcolorQuery.GetVWI_CRM_xts_productinteriorcolorById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_productinteriorcolor
        /// <summary>
        /// Get All VWI_CRM_xts_productinteriorcolor
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_productinteriorcolor> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_productinteriorcolor
        public List<VWI_CRM_xts_productinteriorcolor> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_productinteriorcolor>();
        }
        #endregion

        #region Search VWI_CRM_xts_productinteriorcolor        
        public new List<VWI_CRM_xts_productinteriorcolor> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_productinteriorcolor.rowstatus", "isnull(vwi_crm_xts_productinteriorcolor.rowstatus, 0)");

                List<VWI_CRM_xts_productinteriorcolor> result = SearchData<VWI_CRM_xts_productinteriorcolor>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_productinteriorcolor>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_productinteriorcolorQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_productinteriorcolor.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_productinteriorcolorQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_productinteriorcolor>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_productinteriorcolor vWI_CRM_xts_productinteriorcolor)
        {
            //vWI_CRM_xts_productinteriorcolor.CreatedBy = UserLogin;
            //vWI_CRM_xts_productinteriorcolor.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_productinteriorcolor);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_productinteriorcolor vWI_CRM_xts_productinteriorcolor)
        {
            //vWI_CRM_xts_productinteriorcolor.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_productinteriorcolor.LastUpdateTime = DateTime.Now;
        }
    }
}