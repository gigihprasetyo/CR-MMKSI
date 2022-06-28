#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_styleRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2020 16:11:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_style;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_styleRepository : BaseDNetRepository<VWI_CRM_xts_style>, IVWI_CRM_xts_styleRepository<VWI_CRM_xts_style, int>
    {
        #region Constructor
        public VWI_CRM_xts_styleRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_style
        /// <summary>
        /// Create VWI_CRM_xts_style
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_style entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_style
        /// <summary>
        /// Update VWI_CRM_xts_style
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_style entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_style
        /// <summary>
        /// Delete VWI_CRM_xts_style
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_style By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_style Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_style>(
                        VWI_CRM_xts_styleQuery.GetVWI_CRM_xts_styleById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_style
        /// <summary>
        /// Get All VWI_CRM_xts_style
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_style> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_style
        public List<VWI_CRM_xts_style> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_style>();
        }
        #endregion

        #region Search VWI_CRM_xts_style        
        public new List<VWI_CRM_xts_style> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_style.rowstatus", "isnull(vwi_crm_xts_style.rowstatus, 0)");

                List<VWI_CRM_xts_style> result = SearchData<VWI_CRM_xts_style>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_style>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_styleQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_style.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_styleQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_style>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_style vWI_CRM_xts_style)
        {
            //vWI_CRM_xts_style.CreatedBy = UserLogin;
            //vWI_CRM_xts_style.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_style);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_style vWI_CRM_xts_style)
        {
            //vWI_CRM_xts_style.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_style.LastUpdateTime = DateTime.Now;
        }
    }
}
