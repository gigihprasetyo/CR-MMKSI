#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_newvehicleexteriorcolor repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_newvehicleexteriorcolor;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_newvehicleexteriorcolorRepository : BaseDNetRepository<VWI_CRM_xts_newvehicleexteriorcolor>, IVWI_CRM_xts_newvehicleexteriorcolorRepository<VWI_CRM_xts_newvehicleexteriorcolor, int>
    {
        #region Constructor
        public VWI_CRM_xts_newvehicleexteriorcolorRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_newvehicleexteriorcolor
        /// <summary>
        /// Create VWI_CRM_xts_newvehicleexteriorcolor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_newvehicleexteriorcolor entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_newvehicleexteriorcolor
        /// <summary>
        /// Update VWI_CRM_xts_newvehicleexteriorcolor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_newvehicleexteriorcolor entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_newvehicleexteriorcolor
        /// <summary>
        /// Delete VWI_CRM_xts_newvehicleexteriorcolor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_newvehicleexteriorcolor By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_newvehicleexteriorcolor Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_newvehicleexteriorcolor>(
                        VWI_CRM_xts_newvehicleexteriorcolorQuery.GetVWI_CRM_xts_newvehicleexteriorcolorById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_newvehicleexteriorcolor
        /// <summary>
        /// Get All VWI_CRM_xts_newvehicleexteriorcolor
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_newvehicleexteriorcolor> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_newvehicleexteriorcolor
        public List<VWI_CRM_xts_newvehicleexteriorcolor> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_newvehicleexteriorcolor>();
        }
        #endregion

		#region Search VWI_CRM_xts_newvehicleexteriorcolor        
        public new List<VWI_CRM_xts_newvehicleexteriorcolor> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_newvehicleexteriorcolor.rowstatus", "isnull(vwi_crm_xts_newvehicleexteriorcolor.rowstatus, 0)");

                List<VWI_CRM_xts_newvehicleexteriorcolor> result = SearchData<VWI_CRM_xts_newvehicleexteriorcolor>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_newvehicleexteriorcolor>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_newvehicleexteriorcolorQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_newvehicleexteriorcolor.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_newvehicleexteriorcolorQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_newvehicleexteriorcolor>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_newvehicleexteriorcolor vWI_CRM_xts_newvehicleexteriorcolor)
        {
            //vWI_CRM_xts_newvehicleexteriorcolor.CreatedBy = UserLogin;
            //vWI_CRM_xts_newvehicleexteriorcolor.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_newvehicleexteriorcolor);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_newvehicleexteriorcolor vWI_CRM_xts_newvehicleexteriorcolor)
        {
            //vWI_CRM_xts_newvehicleexteriorcolor.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_newvehicleexteriorcolor.LastUpdateTime = DateTime.Now;
        }
    }
}