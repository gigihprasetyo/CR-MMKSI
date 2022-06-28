#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_newvehicleinteriorcolorRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 10:20:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_newvehicleinteriorcolor;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_newvehicleinteriorcolorRepository : BaseDNetRepository<VWI_CRM_xts_newvehicleinteriorcolor>, IVWI_CRM_xts_newvehicleinteriorcolorRepository<VWI_CRM_xts_newvehicleinteriorcolor, int>
    {
        #region Constructor
        public VWI_CRM_xts_newvehicleinteriorcolorRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_newvehicleinteriorcolor
        /// <summary>
        /// Create VWI_CRM_xts_newvehicleinteriorcolor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_newvehicleinteriorcolor entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_newvehicleinteriorcolor
        /// <summary>
        /// Update VWI_CRM_xts_newvehicleinteriorcolor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_newvehicleinteriorcolor entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_newvehicleinteriorcolor
        /// <summary>
        /// Delete VWI_CRM_xts_newvehicleinteriorcolor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_newvehicleinteriorcolor By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_newvehicleinteriorcolor Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_newvehicleinteriorcolor>(
                        VWI_CRM_xts_newvehicleinteriorcolorQuery.GetVWI_CRM_xts_newvehicleinteriorcolorById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_newvehicleinteriorcolor
        /// <summary>
        /// Get All VWI_CRM_xts_newvehicleinteriorcolor
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_newvehicleinteriorcolor> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_newvehicleinteriorcolor
        public List<VWI_CRM_xts_newvehicleinteriorcolor> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_newvehicleinteriorcolor>();
        }
        #endregion

        #region Search VWI_CRM_xts_newvehicleinteriorcolor        
        public new List<VWI_CRM_xts_newvehicleinteriorcolor> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_newvehicleinteriorcolor.rowstatus", "isnull(vwi_crm_xts_newvehicleinteriorcolor.rowstatus, 0)");

                List<VWI_CRM_xts_newvehicleinteriorcolor> result = SearchData<VWI_CRM_xts_newvehicleinteriorcolor>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_newvehicleinteriorcolor>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_newvehicleinteriorcolorQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_newvehicleinteriorcolor.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_newvehicleinteriorcolorQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_newvehicleinteriorcolor>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_newvehicleinteriorcolor vWI_CRM_xts_newvehicleinteriorcolor)
        {
            //vWI_CRM_xts_newvehicleinteriorcolor.CreatedBy = UserLogin;
            //vWI_CRM_xts_newvehicleinteriorcolor.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_newvehicleinteriorcolor);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_newvehicleinteriorcolor vWI_CRM_xts_newvehicleinteriorcolor)
        {
            //vWI_CRM_xts_newvehicleinteriorcolor.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_newvehicleinteriorcolor.LastUpdateTime = DateTime.Now;
        }
    }
}
