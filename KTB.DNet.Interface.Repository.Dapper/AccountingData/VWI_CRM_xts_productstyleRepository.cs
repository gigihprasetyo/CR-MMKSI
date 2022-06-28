#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productstyleRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 13:53:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_productstyle;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_productstyleRepository : BaseDNetRepository<VWI_CRM_xts_productstyle>, IVWI_CRM_xts_productstyleRepository<VWI_CRM_xts_productstyle, int>
    {
        #region Constructor
        public VWI_CRM_xts_productstyleRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_productstyle
        /// <summary>
        /// Create VWI_CRM_xts_productstyle
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_productstyle entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_productstyle
        /// <summary>
        /// Update VWI_CRM_xts_productstyle
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_productstyle entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_productstyle
        /// <summary>
        /// Delete VWI_CRM_xts_productstyle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_productstyle By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_productstyle Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_productstyle>(
                        VWI_CRM_xts_productstyleQuery.GetVWI_CRM_xts_productstyleById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_productstyle
        /// <summary>
        /// Get All VWI_CRM_xts_productstyle
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_productstyle> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_productstyle
        public List<VWI_CRM_xts_productstyle> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_productstyle>();
        }
        #endregion

        #region Search VWI_CRM_xts_productstyle        
        public new List<VWI_CRM_xts_productstyle> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_productstyle.rowstatus", "isnull(vwi_crm_xts_productstyle.rowstatus, 0)");

                List<VWI_CRM_xts_productstyle> result = SearchData<VWI_CRM_xts_productstyle>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_productstyle>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_productstyleQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_productstyle.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_productstyleQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_productstyle>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_productstyle vWI_CRM_xts_productstyle)
        {
            //vWI_CRM_xts_productstyle.CreatedBy = UserLogin;
            //vWI_CRM_xts_productstyle.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_productstyle);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_productstyle vWI_CRM_xts_productstyle)
        {
            //vWI_CRM_xts_productstyle.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_productstyle.LastUpdateTime = DateTime.Now;
        }
    }
}
